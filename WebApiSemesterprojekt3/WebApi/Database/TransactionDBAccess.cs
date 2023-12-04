using Models;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class TransactionDBAccess : ITransactionDBAccess
    {
        private string? _connectionString;
        private IConfiguration _configuration { get; set; }

        public TransactionDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        // Create transaction for offers
        public bool CreateTransaction(Post inPost)
        {
            bool res = false;
            string query = "insert into Transactions values(@date, @offerID, @bidID, @amount, @exchangeID)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@offerID", inPost.Id);
                    cmd.Parameters.AddWithValue("@bidID", 1); // Hardcoding bid ID
                    cmd.Parameters.AddWithValue("@amount", inPost.Amount);
                    cmd.Parameters.AddWithValue("@exchangeID", 1); // Hardcoding exchange ID
                    var scalarResult = cmd.ExecuteScalar();
                    if (scalarResult != null) {
                        res = true;
                    }
                }
            }
            return res;
        }

    }
}
