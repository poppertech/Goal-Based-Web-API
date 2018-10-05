using Api.Logic;
using Api.Models.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.Logic
{
    [TestClass]
    public class SimulationEvaluatorTest
    {
        [TestMethod]
        public void EvaluateOnSuccessReturnsSimulation()
        {
            //arrange
            var distributionIndex = 0;
            var uniformRandom = .01D;
            var price = 2D;
            var cdfProbability = 1D;
            var cdfProbabilities = new[] { cdfProbability };
            var distribution = new Mock<IDistribution>();
            distribution.Setup(d => d.CdfProbabilities).Returns(cdfProbabilities);
            distribution.Setup(d => d.GetPrice(It.Is<double>(rand => rand == uniformRandom), It.Is<int>(ix => ix == distributionIndex))).Returns(price);
            var distributions = new[] { distribution.Object };

            var evaluator = new SimulationEvaluator();
            evaluator.Init(distributions);

            //act
            var result = evaluator.Evaluate(distributionIndex, uniformRandom);

            //assert
            Assert.AreEqual(distributionIndex, result.DistributionIndex);
            Assert.AreEqual(price, result.Price);
        }
    }
}
