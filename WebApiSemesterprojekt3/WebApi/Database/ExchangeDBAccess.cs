using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class ExchangeDBAccess : IExchangeDBAccess
    {
        private string? _connectionString;
        private IConfiguration _configuration { get; set; }

        public ExchangeDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        
        public bool CreateExchange(double value)
        {
            bool res = false;
            DateTime now = DateTime.Now;
            string query = "insert into Exchanges values(@value, @date)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@date", now);
                    cmd.ExecuteScalar();
                }

            }
                    return res;
        }

    }
}
