using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    /// <summary>
    /// Promotion service responsable for, calculating promotion price and applaying promotions on items.
    /// </summary>
    public interface IPromotionService
    {
        /// <summary>
        /// Try applying given quantity promotion <see cref="QuantityPromotion"/> on given item <see cref="BasketItem"/>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantityPromotion"></param>
        /// <returns>a new <see cref="BasketPromotionItem"/> if promotion applied</returns>
        BasketPromotionItem? TryApplyQuantityPromotionOnItem(BasketItem item, QuantityPromotion quantityPromotion);

        /// <summary>
        /// Try applying given combination promotion <see cref="QuantityPromotion"/> on given list of items <see cref="BasketItem"/>
        /// </summary>
        /// <param name="items"></param>
        /// <param name="quantityPromotion"></param>
        /// <returns>a list of new items <see cref="BasketPromotionItem"/> where promotion applied</returns>
        IList<BasketPromotionItem>? TryApplyCombinationPromotion(IList<BasketItem> items, CombinationPromotion quantityPromotion);

        /// <summary>
        /// Try applying given promotion list <see cref="Promotion"/> on given items <see cref="BasketItem"/> where they applies.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="activePromotions"></param>
        /// <returns>a list of new items <see cref="BasketPromotionItem"/> where promotion applied</returns>
        IList<BasketPromotionItem> TyrApplyPromotionsOnItems(IList<BasketItem> items, IList<Promotion> activePromotions);

        /// <summary>
        /// Try applying given perentage promotion <see cref="PercentagePromotion"/> on given item <see cref="BasketItem"/>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="percentagePromotion"></param>
        /// <returns>a new <see cref="BasketPromotionItem"/> if promotion is applied.</returns>
        BasketPromotionItem? TryApplyPercentagePromotionOnItem(BasketItem item, PercentagePromotion percentagePromotion);
    }
}
