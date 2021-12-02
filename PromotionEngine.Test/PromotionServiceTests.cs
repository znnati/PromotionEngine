using NUnit.Framework;
using System.Collections.Generic;

namespace PromotionEngine.Test
{
    public class Tests
    {
        IList<Promotion> ActivePromotions { get; set; } = new List<Promotion>();

        IList<(char Sku, double Price)> DbProductsList { get; set; } = new List<(char Sku, double Price)>();

        [SetUp]
        public void Setup()
        {
            ActivePromotions = new List<Promotion>()
            {
                new QuantityPromotion(name: "3 of A's for 130", sku: 'A', nbr: 3, price: 130),
                new QuantityPromotion("2 of B's for 45", 'B', 2, 45),
                new CombinationPromotion("C & D for 30", skuQuntities: new [] {('C', 1), ('D', 1)}, price: 30)
            };

        DbProductsList = new List<(char Sku, double Price)> { ('A', 50), ('B', 30), ('C', 20), ('D', 15) };
    }

    [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}