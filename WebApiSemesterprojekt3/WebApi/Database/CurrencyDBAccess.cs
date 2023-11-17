using Dapper;
using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class CurrencyDBAccess : ICurrencyDBAccess
    {
        private IConfiguration _configuration { get; set; }
        private string? _connectionString;

        public CurrencyDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur");
        }

        public int GetCurrencyID(Currency item)
        {

            int res;
            string queryString = "SELECT id FROM Currencies WHERE currencytype = @type";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand insertCommand = conn.CreateCommand())
                {

                    insertCommand.CommandText = queryString;
                    insertCommand.Parameters.AddWithValue("type", item.Type.ToString());
                    SqlDataReader reader = insertCommand.ExecuteReader();

                    res = reader.GetInt32(reader.GetOrdinal("id"));
                }
            }
            return res;
        }
    }
}
