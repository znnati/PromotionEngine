using Moq;
using NUnit.Framework;
using PromotionEngine.Models;
using PromotionEngine.Services;
using System.Collections.Generic;

namespace PromotionEngine.Test
{

    // Only happy fllow tests are included.
    [TestFixture]
    internal class BasketServiceTests
    {
        private Mock<IDbRepository> mockDbRepository { get; set; } = new Mock<IDbRepository>();
        private Mock<IPromotionService> mockPromotionService { get; set; } = new Mock<IPromotionService>();

        [SetUp]
        public void Setup()
        {
            mockDbRepository = new Mock<IDbRepository>();
            mockDbRepository.Setup(x => x.GetProducts()).Returns(new List<(char Sku, double Price)> { ('A', 50), ('B', 30), ('C', 20), ('D', 15) });
            mockDbRepository.Setup(x => x.GetActivePromotions()).Returns(new List<Promotion>()
            {
                new QuantityPromotion(name: "3 of A's for 130", sku: 'A', nbr: 3, price: 130),
                new QuantityPromotion("2 of B's for 45", 'B', 2, 45),
                new CombinationPromotion("C & D for 30", skuQuntities: new [] {('C', 1), ('D', 1)}, price: 30)
            });

            mockPromotionService = new Mock<PromotionService>() { CallBase = true }.As<IPromotionService>();
        }

        [TearDown]
        public void TearDown()
        {
            mockDbRepository.Reset();
        }

        [Test]
        [TestCaseSource(nameof(GetScenario_A_Data))]
        [TestCaseSource(nameof(GetScenario_B_Data))]
        [TestCaseSource(nameof(GetScenario_C_Data))]
        public void Scenario_A_Test(IList<(char Sku, int price)> data, double total)
        {
            // Arrange
            var basketService = new BasketService(mockDbRepository.Object, mockPromotionService.Object);
            basketService.AddProductsToBasket(data);

            // Act 
            basketService.ApplyPromotion();

            // Assert
            Assert.AreEqual(total, basketService.GetBasketTotal());

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
