using Api.Controllers;
using Api.Logic;
using Api.Models.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.Controllers
{
    [TestClass]
    public class NetworkControllerTest
    {
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
            var viewModel = new NetworkViewModel();
            var network = new Mock<INetwork>();

            var service = new Mock<INetworkService>();
            service.Setup(s => s.GetNetwork(It.Is<NetworkViewModel>(vm => vm == viewModel))).Returns(network.Object);

            var controller = new NetworkController(service.Object);

            //act
            var response = controller.Post(viewModel);

            //assert
            Assert.AreSame(network.Object, response);
            service.Verify(s => s.GetNetwork(It.Is<NetworkViewModel>(vm => vm == viewModel)));
        }
    }
}
