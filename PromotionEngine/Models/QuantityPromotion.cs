namespace PromotionEngine.Models
{
    public class QuantityPromotion : Promotion
    {
        public char Sku { get; set; }
        public int Nbr { get; set; }
        public double Price { get; set; }

        public QuantityPromotion(string name, char sku, int nbr, double price) : base(name)
        {
            Sku = sku;
            Nbr = nbr;
            Price = price;
        }
    }
}
