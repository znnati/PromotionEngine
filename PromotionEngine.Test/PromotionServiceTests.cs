using NUnit.Framework;
using PromotionEngine.Models;
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
        [TestCaseSource(nameof(GetScenario_A_Data))]
        [TestCaseSource(nameof(GetScenario_B_Data))]
        [TestCaseSource(nameof(GetScenario_C_Data))]
        public void ScenarioTest(IList<(char Sku, int price)> data, double total)
        {
            Assert.Fail();
        }

        private static IEnumerable<object[]> GetScenario_A_Data()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('A', 1), ('B', 1), ('C', 1) }, 100.0 };
        }

        private static IEnumerable<object[]> GetScenario_B_Data()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('A', 5), ('B', 5), ('C', 1) }, 370.0 };
        }
        private static IEnumerable<object[]> GetScenario_C_Data()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('A', 3), ('B', 5), ('C', 1), ('D', 1) }, 280.0 };
        }
    }
}