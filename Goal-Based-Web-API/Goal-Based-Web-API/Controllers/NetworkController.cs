using System.Collections.Generic;
using Api.Models.Network;
using Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly INetwork _network;
        public NetworkController(INetwork network)
        {
            _network = network;
        }

        [HttpGet]
        public INetwork Get()
        {
            var networkId = 1;
            var cashFlowRepository = new CashFlowRepository();
            var nodeRepository = new NodeRepository();
            IDictionary<int, Node> nodeDictionary = nodeRepository.GetNodesByNetworkId(networkId);
            var cashFlows = cashFlowRepository.GetCashFlowsByNetworkId(networkId);

            _network.Calculate(ref nodeDictionary, cashFlows);
            return _network;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }

        [HttpPost]
        public Network Post([FromBody]Network network)
        {
            return network;
        }

        [HttpPut("{id}")]
        public Network Put(int id, [FromBody]Network network)
        {
            return network;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            return;
        }
    }
}
