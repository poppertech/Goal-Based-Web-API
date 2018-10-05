using Api.Logic;
using Api.Models.Network;
using CsvHelper;
using Goal_Based_Web_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiTests.Controllers
{
    [TestClass]
    public class CashFlowControllerTest
    {
        [TestMethod]
        public void GetCashFlowsOnSuccessRetuurnsFile()
        {
            //arrange
            var networkId = 1;
            var cost = 100D;
            var cashFlow = new CashFlow() {Cost = cost };
            var cashFlows = new[] { cashFlow };
            var service = new Mock<ICashFlowService>();
            service.Setup(s => s.GetCashFlowsByNetworkId(It.Is<int>(id => id == networkId))).Returns(cashFlows);
            var csvDeserializer = new CsvFileDeserializer<CashFlow>();
            var controller = new CashFlowController(service.Object);

            //act
            var file = (FileStreamResult)controller.GetCashFlows(networkId);
            var results = ReadFile(file.FileStream);
            var result = results[0];

            //assert
            Assert.AreEqual(cost, result.Cost);
            service.Verify(s => s.GetCashFlowsByNetworkId(It.Is<int>(id => id == networkId)));
        }

        private static IList<CashFlow> ReadFile(Stream input)
        {
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    using (var csv = new CsvReader(streamReader))
                    {
                        csv.Configuration.HasHeaderRecord = true;
                        return csv.GetRecords<CashFlow>().ToList();
                    }
                }
            }
        }

    }
}
