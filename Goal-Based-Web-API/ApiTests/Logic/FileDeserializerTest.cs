using Api.Logic;
using Api.Models.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Logic
{
    [TestClass]
    public class FileDeserializerTest
    {
        [TestMethod]
        public void CsvFileDeserializerOnSuccessReturnsRecords()
        {
            //arrange
            var cashFlow = new CashFlow { Id = 1, Cost = 100 };
            var cashFlows = new[] { cashFlow };
            var file = Helper.WriteFile(cashFlows);

            var csvDeserializer = new CsvFileDeserializer<CashFlow>();

            //act
            csvDeserializer.ReadFile(file);
            var records = csvDeserializer.GetRecords();

            //assert
            Assert.AreEqual(cashFlow.Id, records[0].Id);
            Assert.AreEqual(cashFlow.Cost, records[0].Cost);
        }

        [TestMethod]
        public void JsonFileDeserializerOnSuccessReturnsInstance()
        {
            //arrange
            var distribution = new Distribution(1, 2, 3, 4, 5, 6);
            var node = new Node { Id = 1, InitialPrice = 2, InitialInvestment = 3, IsPortfolioComponent = true, Distributions = new[] { distribution } };
            var file = Helper.WriteFile(node);

            var jsonDeserializer = new JsonFileDeserializer<Node>();

            //act
            jsonDeserializer.ReadFile(file);
            var instance = jsonDeserializer.GetInstance();

            //assert
            Assert.AreEqual(node.Id, instance.Id);
            Assert.AreEqual(node.InitialPrice, instance.InitialPrice);
            Assert.AreEqual(node.InitialInvestment, instance.InitialInvestment);
            Assert.IsTrue(instance.IsPortfolioComponent);

            Assert.AreEqual(distribution.Id, instance.Distributions[0].Id);
            Assert.AreEqual(distribution.Minimum, instance.Distributions[0].Minimum);
            Assert.AreEqual(distribution.Worst, instance.Distributions[0].Worst);
            Assert.AreEqual(distribution.Likely, instance.Distributions[0].Likely);
            Assert.AreEqual(distribution.Best, instance.Distributions[0].Best);
            Assert.AreEqual(distribution.Maximum, instance.Distributions[0].Maximum);
        }

    }
}
