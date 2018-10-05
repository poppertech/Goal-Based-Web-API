using Api.Models.Network;
using Api.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Api.Logic
{
    public interface ICashFlowService
    {
        IEnumerable<CashFlow> GetCashFlowsByNetworkId(int networkId);
    }

    public class CashFlowService: ICashFlowService
    {
        private readonly ICashFlowRepository _repository;

        public CashFlowService(ICashFlowRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<CashFlow> GetCashFlowsByNetworkId(int networkId)
        {
            return _repository.GetCashFlowsByNetworkId(networkId);
        }
    }
}
