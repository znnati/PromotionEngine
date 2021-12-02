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
            // Check if item has the given quantity promotion. Return null if not.
            if (!HasQuantityPromotion(item, quantityPromotion))
                return null;

            // Reduce the item quantity by the nbr used in the promotion.
            item.Quantity -= quantityPromotion.Nbr;

            // Keep trying applyin the promotion and get a new object while it is applicable
            var promotedItem = TryApplyQuantityPromotionOnItem(item, quantityPromotion);

            // Return the result of the method's concurrency
            return new BasketPromotionItem { Sku = item.Sku, Quantity = (promotedItem?.Quantity ?? 0) + quantityPromotion.Nbr, Total = (promotedItem?.Total ?? 0) + quantityPromotion.Price };
        }

        public IList<BasketPromotionItem> TyrApplyPromotionsOnItems(IEnumerable<BasketItem> items, IList<Promotion> activePromotions)
        {
            throw new NotImplementedException();
        }


        private bool HasQuantityPromotion(BasketItem item, QuantityPromotion promotion)
        {
            return false;
        }
    }
}
