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
        public bool InsertTransactionLine(TransactionLine transactionLine, SqlConnection conn, SqlTransaction tran)
        {
            bool res = false;
            string query = "INSERT INTO Transactions OUTPUT INSERTED.post_bid_id_fk VALUES(@date, @offerID, @bidID, @amount)";

            using (SqlCommand cmd = conn.CreateCommand())
            {

                cmd.Transaction = tran;
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

            return res;
        }

        public IEnumerable<TransactionLine> GetTransactionLines(int postId)
        {
            List<TransactionLine> foundLines = new List<TransactionLine>();
            string queryString = "SELECT Transactions.amount,price,date,post_bid_id_fk FROM Transactions" +
                " JOIN Posts ON Transactions.Post_offer_id_fk = Posts.id WHERE Transactions.Post_offer_id_fk = @postId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    conn.Open();
                    readCommand.Parameters.AddWithValue("postId", postId);

                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int buyerId = (int)reader["post_bid_id_fk"];
                            TransactionLine line = new TransactionLine
                            {
                                Date = (DateTime)reader["date"],
                                Amount = (double)reader["amount"],
                                Buyer = new Post()
                                {
                                    Id = buyerId,
                                },
                                Seller = null,
                            };

                            foundLines.Add(line);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Could not find any transactionsLines");
            }

            return foundLines;
        }

    }
}
