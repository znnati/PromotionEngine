using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    public interface IBasketService
    {
        /// <summary>
        /// Add items to basket.
        /// </summary>
        /// <param name="items"></param>
        void AddProductsToBasket(IList<(char sku, int quantity)> items);

        /// <summary>
        /// Apply the given <see cref="Promotion"/> on the items in <see cref="Basket"/>
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="promotion"></param>
        void ApplyPromotion();

        /// <summary>
        /// Get total price for the baske.
        /// </summary>
        /// <returns></returns>
        double GetBasketTotal();
    }
}
