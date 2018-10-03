using Api.Models.Network;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Api.Repository
{
    public interface ICashFlowRepository
    {
        IList<CashFlow> GetCashFlowsByNetworkId(int networkId);
    }

    public class CashFlowRepository : ICashFlowRepository
    {
        public IList<CashFlow> GetCashFlowsByNetworkId(int networkId)
        {
            var cashFlows = new List<CashFlow>();
            var connectionString = @"Data Source=BWYNNE\SQLEXPRESS;Initial Catalog=Goal-Based-Database;Integrated Security=true";
            using (var connection = new SqlConnection(connectionString))
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
