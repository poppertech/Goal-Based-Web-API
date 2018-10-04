using Api.Models.Network;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface INetworkRepository
    {
        INetwork GetNetworkById(int id);
        INetwork GetNetwork(Node tree, IList<CashFlow> cashFlows);
    }

    public class NetworkRepository: INetworkRepository
    {
        private readonly INodeRepository _nodeRepository;
        private readonly ICashFlowRepository _cashFlowRepository;
        private readonly INetwork _network;
        public NetworkRepository(INetwork network, INodeRepository nodeRepository, ICashFlowRepository cashFlowRepository)
        {
            _network = network;
            _nodeRepository = nodeRepository;
            _cashFlowRepository = cashFlowRepository;
        }

        public INetwork GetNetworkById(int id)
        {
            IDictionary<int, Node> nodeDictionary = _nodeRepository.GetNodesByNetworkId(id);
            var cashFlows = _cashFlowRepository.GetCashFlowsByNetworkId(id);
            _network.Calculate(ref nodeDictionary, cashFlows);
            return _network;
        }

        public INetwork GetNetwork(Node tree, IList<CashFlow> cashFlows)
        {
            _network.Calculate(tree, cashFlows);
            return _network;
        }
    }
}
