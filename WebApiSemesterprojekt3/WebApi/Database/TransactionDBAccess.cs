using Models;
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

        /// <summary>
        /// Create a business transaction.
        /// </summary>
        /// <param name="inOffer"></param>
        /// <param name="inBid"></param>
        /// <returns></returns>
        public bool InsertTransactionLine(TransactionLine transactionLine, SqlTransaction? transaction = null)
        {
            bool res = false;
            string query = "insert into Transactions values(@date, @offerID, @bidID, @amount)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@offerID", transactionLine.Seller.Id);
                    cmd.Parameters.AddWithValue("@bidID", transactionLine.Buyer.Id);
                    cmd.Parameters.AddWithValue("@amount", transactionLine.Seller.Amount);
                    var scalarResult = cmd.ExecuteScalar();
                    if (scalarResult != null)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

    }
}
