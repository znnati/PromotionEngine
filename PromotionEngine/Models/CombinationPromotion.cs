namespace PromotionEngine.Models
{
    public class CombinationPromotion : Promotion
    {
        public double Price { get; set; }
        public IList<(char sku, int nbr)> SkuQuantityList { get; set; }

        public CombinationPromotion(string name, IList<(char, int)> skuQuntities, double price) : base(name)
        {
            Price = price;
            SkuQuantityList = skuQuntities;
        }
    }
}
