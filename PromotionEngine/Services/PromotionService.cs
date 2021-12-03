using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    public class PromotionService : IPromotionService
    {
        public IList<BasketPromotionItem>? TryApplyCombinationPromotion(IList<BasketItem> items, CombinationPromotion combinationPromotion)
        {
            if (!HasCombinedPromotion(items, combinationPromotion))
                return new List<BasketPromotionItem>();

            var itemToBePaid = combinationPromotion.SkuQuantityList.OrderBy(s => s.sku).LastOrDefault();
            var list = new List<BasketPromotionItem>();
            foreach ((char sku, int nbr) in combinationPromotion.SkuQuantityList.OrderBy(s => s.sku))
            {
                var basketItem = items.FirstOrDefault(i => i.Sku.Equals(sku));
                if (basketItem == null)
                    throw new Exception();

                basketItem.Quantity -= nbr;

                double price = sku.Equals(itemToBePaid.sku) ? combinationPromotion.Price : 0;
                list.Add(new BasketPromotionItem { Sku = sku, Quantity = nbr, Total = price });
            }

            // Keep trying applyin the promotion and get a new object while it is applicable
            return TryApplyCombinationPromotion(items, combinationPromotion)?.Concat(list)
                .GroupBy(i => i.Sku)
                .Select(x => new BasketPromotionItem { Sku = x.Key, Quantity = x.Sum(s => s.Quantity), Total = x.Sum(s => s.Total) })
                .ToList();
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

        public BasketPromotionItem? TryApplyPercentagePromotionOnItem(BasketItem item, PercentagePromotion percentagePromotion)
        {
            if (!HasPercentagePromotion(item, percentagePromotion))
                return null;

            int itemQuantity = item.Quantity;
            item.Quantity = 0;

            return new BasketPromotionItem { Sku = item.Sku, Quantity = itemQuantity, Total = itemQuantity * item.Price * (100 - percentagePromotion.Percentage) / 100 };
        }

        public IList<BasketPromotionItem> TyrApplyPromotionsOnItems(IList<BasketItem> items, IList<Promotion> activePromotions)
        {
            var promotedItems = new List<BasketPromotionItem>();
           
            // Apply quantity promotions first.
            foreach (QuantityPromotion promotion in activePromotions.Where(p => p is QuantityPromotion))
            {
                foreach (var item in items)
                {
                    BasketPromotionItem? result = TryApplyQuantityPromotionOnItem(item, promotion);
                    if (result != null)
                        promotedItems.Add(result);
                }
            }

            // Apply combination promtions second.
            foreach (CombinationPromotion promotion in activePromotions.Where(p => p is CombinationPromotion))
            {
                IList<BasketPromotionItem>? result = TryApplyCombinationPromotion(items, promotion);
                if (result?.Any() == true)
                    promotedItems.AddRange(result);
            }

            return promotedItems;
        }


        private bool HasQuantityPromotion(BasketItem item, QuantityPromotion promotion)
        {
            return item.Sku.Equals(promotion.Sku) && item.Quantity >= promotion.Nbr;
        }
        private bool HasCombinedPromotion(IEnumerable<BasketItem> items, CombinationPromotion promotion)
        {
            return promotion.SkuQuantityList.All(p =>
            items.Any(item => item.Sku.Equals(p.sku) && p.nbr <= item.Quantity));
        }
        private bool HasPercentagePromotion(BasketItem item, PercentagePromotion percentagePromotion)
        {
            return percentagePromotion?.Sku.Equals(item.Sku) ?? false;
        }
    }
}
