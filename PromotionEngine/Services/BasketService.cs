using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    public class BasketService : IBasketService, IDisposable
    {
        private readonly IDbRepository dbRepository;
        private readonly IPromotionService promotionService;

        private IList<BasketItem> basketItems { get; set; }
        private IList<BasketPromotionItem> basketPromotionItems { get; set; }

        public BasketService(IDbRepository dbRepository, IPromotionService promotionService)
        {
            this.dbRepository = dbRepository;
            this.promotionService = promotionService;
            
            basketItems = new List<BasketItem>();
            basketPromotionItems = new List<BasketPromotionItem>();
        }

        public void AddProductsToBasket(IList<(char sku, int quantity)> items)
        {
            var dbProducts = dbRepository.GetProducts();
            
            basketItems = items.Select(i => new BasketItem
            {
                Sku = i.sku,
                Quantity = i.quantity,
                Price = dbProducts.FirstOrDefault(u => u.Sku.Equals(i.sku)).Price
            }).ToList();
        }

        public void ApplyPromotion()
        {
            basketPromotionItems = promotionService.TyrApplyPromotionsOnItems(basketItems, dbRepository.GetActivePromotions());
        }

        public double GetBasketTotal()
        {
            return basketPromotionItems.Sum(i => i.Total)
                + basketItems.Sum(i => i.Price * i.Quantity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
