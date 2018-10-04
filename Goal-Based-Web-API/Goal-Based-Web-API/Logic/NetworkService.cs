using Api.Models.Network;
using Api.Repository;

namespace Api.Logic
{
    public interface INetworkService
    {
        INetwork GetNetworkById(int id);
    }

    public class NetworkService: INetworkService
    {
        private readonly INetworkRepository _networkRepository;

        public NetworkService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }
        public INetwork GetNetworkById(int id)
        {
            return _networkRepository.GetNetworkById(id);
        }
    }
}
