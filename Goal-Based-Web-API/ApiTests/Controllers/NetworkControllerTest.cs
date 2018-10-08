﻿using Api.Controllers;
using Api.Logic;
using Api.Models.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace ApiTests.Controllers
{
    [TestClass]
    public class NetworkControllerTest
    {
        [TestMethod]
        public void GetNetworksOnSuccessReturnsNetworks()
        {
            //arrange
            var network = new NetworkViewModel { Id = 1 };
            var networks = new[] { network };

            var service = new Mock<INetworkService>();
            service.Setup(s => s.GetNetworks()).Returns(networks);

            var controller = new NetworkController(service.Object);

            //act
            var result = controller.Get();

            //assert
            Assert.AreEqual(network.Id, result.First().Id);
        }

        [TestMethod]
        public void GetByIdReturnsNetwork()
        {
            //arrange
            var networkId = 1;
            var network = new Mock<INetwork>();
            var service = new Mock<INetworkService>();
            service.Setup(s => s.GetNetworkById(It.Is<int>(id => id == networkId))).Returns(network.Object);

            var controller = new NetworkController(service.Object);

            //act
            var response = controller.Get(networkId);

            //assert
            Assert.AreSame(network.Object, response);
            service.Verify(s => s.GetNetworkById(It.Is<int>(id => id == networkId)));
        }

        [TestMethod]
        public void PostReturnsNetwork()
        {
            //arrange
            var viewModel = new PostNetworkViewModel();
            var network = new Mock<INetwork>();

            var service = new Mock<INetworkService>();
            service.Setup(s => s.GetNetwork(It.Is<PostNetworkViewModel>(vm => vm == viewModel))).Returns(network.Object);

            var controller = new NetworkController(service.Object);

            //act
            var response = controller.Post(viewModel);

            //assert
            Assert.AreSame(network.Object, response);
            service.Verify(s => s.GetNetwork(It.Is<PostNetworkViewModel>(vm => vm == viewModel)));
        }
    }
}
