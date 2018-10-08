using Api.Models.Network;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Api.Repository
{
    public interface INetworkRepository
    {
        INetwork GetNetworkById(int id);
        INetwork GetNetwork(Node tree, IList<CashFlow> cashFlows);
        IEnumerable<NetworkViewModel> GetNetworks();
    }

    public class NetworkRepository: INetworkRepository
    {
        private readonly string _connectionString;
        private readonly INodeRepository _nodeRepository;
        private readonly ICashFlowRepository _cashFlowRepository;
        private readonly INetwork _network;
        public NetworkRepository(IOptions<ApiOptions> optionsAccessor, INetwork network, INodeRepository nodeRepository, ICashFlowRepository cashFlowRepository)
        {
            _connectionString = optionsAccessor.Value.ConnString;
            _network = network;
            _nodeRepository = nodeRepository;
            _cashFlowRepository = cashFlowRepository;
        }

        public IEnumerable<NetworkViewModel> GetNetworks()
        {
            var networks = new List<NetworkViewModel>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("GetNetworks", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var network = new NetworkViewModel();
                        network.Id = (int)reader["Id"];
                        network.Name = (string)reader["Name"];
                        networks.Add(network);
                    }
                }
            }
            return networks;
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
