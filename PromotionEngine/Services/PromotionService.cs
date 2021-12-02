using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    public class PromotionService : IPromotionService
    {
        public IList<BasketPromotionItem>? TryApplyCombinationPromotion(IEnumerable<BasketItem> items, CombinationPromotion quantityPromotion)
        {
            throw new NotImplementedException();
        }

        public BasketPromotionItem? TryApplyQuantityPromotionOnItem(BasketItem item, QuantityPromotion quantityPromotion)
        {
            throw new NotImplementedException();
        }

        public IList<BasketPromotionItem> TyrApplyPromotionsOnItems(IEnumerable<BasketItem> items, IList<Promotion> activePromotions)
        {
            throw new NotImplementedException();
        }
    }
}
