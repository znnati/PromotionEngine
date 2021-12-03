using PromotionEngine.Models;

namespace PromotionEngine.Services
{
    public interface IDbRepository
    {
        /// <summary>
        /// Gets products from db.
        /// </summary>
        /// <returns></returns>
        IEnumerable<(char Sku, double Price)> GetProducts();

        /// <summary>
        /// Gets active promotions from db.
        /// </summary>
        /// <returns></returns>
        IList<Promotion> GetActivePromotions();
    }
}
