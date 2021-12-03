namespace PromotionEngine.Models
{
    public class PercentagePromotion : Promotion
    {
        public char Sku { get; set; }
        public int Percentage { get; set; }

        public PercentagePromotion(string name, char sku, int percentage) : base(name)
        {
            Sku = sku;
            Percentage = percentage;
        }
    }
}
