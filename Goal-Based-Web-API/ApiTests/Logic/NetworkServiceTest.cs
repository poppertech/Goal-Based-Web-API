using Api.Logic;
using Api.Models.Network;
using Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ApiTests.Logic
{
    [TestClass]
    public class NetworkServiceTest
    {
        [TestMethod]
        public void GetNetworkByIdReturnNetwork()
        {
            //arrange
            var networkId = 1;
            var network = new Network(null, null);

            var repository = new Mock<INetworkRepository>();
            repository.Setup(r => r.GetNetworkById(It.Is<int>(id => id == networkId))).Returns(network);

            var service = new NetworkService(repository.Object, null, null);

            //act
            var result = service.GetNetworkById(networkId);

            //assert
            Assert.AreSame(network, result);
            repository.Verify(r => r.GetNetworkById(It.Is<int>(id => id == networkId)));
        }

        [TestMethod]
        public void GetNetworkOnSuccessReturnsNetwork()
        {
            //arrange
            var node = new Node();
            var network = new Network(null, null);
            var cashFlow = new CashFlow();
            var cashFlows = new[] { cashFlow };
            var cashFlowFile = new Mock<IFormFile>();
            var viewModel = new NetworkViewModel {CashFlows = cashFlowFile.Object };

            var csvDeserializer = new Mock<ICsvFileDeserializer<CashFlow>>();
            csvDeserializer.Setup(d => d.GetRecords()).Returns(cashFlows);

            var jsonDeserializer = new Mock<IJsonFileDeserializer<Node>>();
            jsonDeserializer.Setup(d => d.GetInstance()).Returns(node);

            var repository = new Mock<INetworkRepository>();
            repository.Setup(r => r.GetNetwork(It.IsAny<Node>(), It.IsAny<IList<CashFlow>>())).Returns(network);

            var service = new NetworkService(repository.Object, csvDeserializer.Object, jsonDeserializer.Object);

            //act
            var result = service.GetNetwork(viewModel);

            //assert
            Assert.AreSame(network, result);
            csvDeserializer.Verify(d => d.ReadFile(It.IsAny<IFormFile>()));
            jsonDeserializer.Verify(d => d.ReadFile(It.IsAny<IFormFile>()));
            repository.Verify(r => r.GetNetwork(It.IsAny<Node>(), It.IsAny<IList<CashFlow>>()));
        }
    }
}
