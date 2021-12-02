﻿using PromotionEngine.Models;

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

        public IList<BasketPromotionItem> TyrApplyPromotionsOnItems(IList<BasketItem> items, IList<Promotion> activePromotions)
        {
            throw new NotImplementedException();
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
    }
}
