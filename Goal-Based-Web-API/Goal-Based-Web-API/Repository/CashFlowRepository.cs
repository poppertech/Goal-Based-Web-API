using Api.Models.Network;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Repository
{
    public interface ICashFlowRepository
    {
        IList<CashFlow> GetCashFlowsByNetworkId(int networkId);
    }

    public class CashFlowRepository : ICashFlowRepository
    {
        private readonly string _connectionString;

        public CashFlowRepository(IOptions<ApiOptions> optionsAccessor)
        {
            _connectionString = optionsAccessor.Value.ConnString;
        }

        public IList<CashFlow> GetCashFlowsByNetworkId(int networkId)
        {
            var cashFlows = new List<CashFlow>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("GetCashFlowsByNetworkId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NetworkId", networkId);

                connection.Open();

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cashFlow = new CashFlow();
                        cashFlow.Id = (int)reader["CashFlowId"];
                        cashFlow.Cost = (double)reader["Cost"];
                        cashFlows.Add(cashFlow);
                    }
                }
            }
            return cashFlows;
        }
    }
}
