using Api.Models.Network;
using Api.Repository;

namespace Api.Logic
{
    public interface INetworkService
    {
        INetwork GetNetworkById(int id);
        INetwork GetNetwork(NetworkViewModel network);
    }

    public class NetworkService: INetworkService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly ICsvFileDeserializer<CashFlow> _csvFileDeserializer;
        private readonly IJsonFileDeserializer<Node> _jsonFileDeserializer;

        public NetworkService(INetworkRepository networkRepository, ICsvFileDeserializer<CashFlow> csvFileDeserializer, IJsonFileDeserializer<Node> jsonFileDeserializer)
        {
            _networkRepository = networkRepository;
            _csvFileDeserializer = csvFileDeserializer;
            _jsonFileDeserializer = jsonFileDeserializer;
        }
        public INetwork GetNetworkById(int id)
        {
            return _networkRepository.GetNetworkById(id);
        }

        public INetwork GetNetwork(NetworkViewModel network)
        {
            _csvFileDeserializer.ReadFile(network.CashFlows);
            var cashFlows = _csvFileDeserializer.GetRecords();

            _jsonFileDeserializer.ReadFile(network.Tree);
            var tree = _jsonFileDeserializer.GetInstance();

            return _networkRepository.GetNetwork(tree, cashFlows);
        }

    }
}
