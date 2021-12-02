using NUnit.Framework;
using PromotionEngine.Models;
using PromotionEngine.Services;
using System.Collections.Generic;
using System.Linq;

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
        [TestCase('A', 1, 50)]
        [TestCase('A', 2, 100)]
        [TestCase('A', 3, 130)]
        [TestCase('A', 5, 230)]
        public void QuantityPromotionTest(char sku, int quantity, decimal total)
        {
            // Arrange
            (char Sku, double price) = DbProductsList.FirstOrDefault(p => p.Sku.Equals(sku));

            var item = new BasketItem
            {
                Sku = sku,
                Quantity = quantity,
                Price = price
            };

            var quantityPromotion = new QuantityPromotion("3 of A's for 130", 'A', 3, 130);

            IPromotionService promotionService = new PromotionService();


            // Act 
            BasketPromotionItem? result = promotionService.TryApplyQuantityPromotionOnItem(item, quantityPromotion);


            // Assert
            Assert.Fail();
        }

        [Test]
        [TestCaseSource(nameof(GetCombinationPromotion_Case_1))]
        [TestCaseSource(nameof(GetCombinationPromotion_Case_2))]
        [TestCaseSource(nameof(GetCombinationPromotion_Case_3))]
        [TestCaseSource(nameof(GetCombinationPromotion_Case_4))]
        public void CombinationPromotionTest(IList<(char Sku, int price)> data, double total)
        {
            Assert.Fail();
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


        private static IEnumerable<object[]> GetCombinationPromotion_Case_1()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('C', 1), ('D', 1) }, 30.0 };
        }
        private static IEnumerable<object[]> GetCombinationPromotion_Case_2()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('C', 3), ('D', 2) }, 80.0 };
        }
        private static IEnumerable<object[]> GetCombinationPromotion_Case_3()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('A', 1), ('B', 1), ('C', 1), ('D', 1) }, 110.0 };
        }
        private static IEnumerable<object[]> GetCombinationPromotion_Case_4()
        {
            yield return new object[] { new List<(char Sku, int price)>() { ('A', 2), ('B', 2), ('C', 2), ('D', 4) }, 250.0 };
        }
    }
}