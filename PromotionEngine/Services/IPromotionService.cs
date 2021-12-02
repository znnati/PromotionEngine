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
        /// <returns>a new <see cref="BasketPromotionItem"/> if promotion applies</returns>
        BasketPromotionItem? TryApplyQuantityPromotionOnItem(BasketItem item, QuantityPromotion quantityPromotion);
    }
}
