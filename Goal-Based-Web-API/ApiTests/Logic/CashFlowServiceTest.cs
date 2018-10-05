using Api.Logic;
using Api.Models.Network;
using Api.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace ApiTests.Logic
{
    [TestClass]
    public class CashFlowServiceTest
    {
        [TestMethod]
        public void GetCashFlowsByNetworkIdReturnsCashFlows()
        {
            //arrange
            var networkId = 1;
            var cost = 100D;
            var cashFlow = new CashFlow { Cost = cost };
            var cashFlows = new[] { cashFlow };

            var repository = new Mock<ICashFlowRepository>();
            repository.Setup(r => r.GetCashFlowsByNetworkId(It.Is<int>(id => id == networkId))).Returns(cashFlows);

            var service = new CashFlowService(repository.Object);

            //act
            var results = service.GetCashFlowsByNetworkId(networkId);
            var result = results.First();

            //assert
            Assert.AreEqual(cost, result.Cost);
            repository.Verify(r => r.GetCashFlowsByNetworkId(It.Is<int>(id => id == networkId)));
        }
    }
}
