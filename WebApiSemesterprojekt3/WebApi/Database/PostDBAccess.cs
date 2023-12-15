using Microsoft.AspNetCore.Http;
using Models;
using System.Data.SqlClient;
using System.Threading.Channels;
using System.Transactions;

namespace WebApi.Database
{
    public class PostDBAccess : IPostDBAccess
    {
        private readonly IConfiguration _configuration;
        private string? _connectionString;

        public PostDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        public PostDBAccess() { }


        /// <summary>
        /// Get all bid Post
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetBidPosts()
        {
            List<Post> foundBids = new List<Post>();
            string queryString = "SELECT * FROM Posts JOIN currencies ON Posts.currencies_id_fk = currencies.id JOIN Exchanges ON Exchanges.currencies_id_fk = currencies.id WHERE posts.type = 'Bid' AND isComplete = 0";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = new((string)reader["currencytype"]);

                        Post bids = new Post
                        {
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                            Id = (int)reader["id"],
                            Type = (string)reader["type"],
                        };
                        foundBids.Add(bids);
                    }
                }
                return foundBids;
            }
        }

        /// <summary>
        /// Get all Offer posts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetOfferPosts()
        {
            List<Post> foundOffers = new List<Post>();

            string queryString = "SELECT * FROM Posts INNER JOIN currencies ON Posts.currencies_id_fk = currencies.id JOIN Exchanges ON Exchanges.currencies_id_fk = currencies.id WHERE posts.type = 'offer'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = new((string)reader["currencytype"]);
                        Post offer = new Post
                        {
                            Id = (int)reader["id"],
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                            Type = (string)reader["type"],
                            IsComplete = (bool)reader["isComplete"],
                        };
                        foundOffers.Add(offer);
                    }
                }
                return foundOffers;
            }
        }


        /// <summary>
        /// Insert the Bid into the database
        /// </summary>
        /// <param name="bid"></param>
        //TODO contiue implementing this!!
        public bool InsertBid(Post bid, string aspNetUserId)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand insertCommand = conn.CreateCommand())
                {
                    result = 0 < InsertBidAux(insertCommand, bid, aspNetUserId);
                }
            }
            return result;
        }
        /// <summary>
        /// The takes an sqlconnection and creates a SqlCommand.
        /// The SqlCommand uses the current transaction from the connection,
        /// to insert the bid into the database.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="aspNetUserId"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public int InsertBidReturnBidId(Post post, string aspNetUserId, SqlConnection conn, SqlTransaction tran)
        {
            int result = 0;
            using (SqlCommand insertCommand = conn.CreateCommand())
            {

                insertCommand.Transaction = tran;
                result = InsertBidAux(insertCommand, post, aspNetUserId);
            }
            return result;
        }


        /// <summary>
        /// This method inserts the given bid, into the database using the gives sqlCommand.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="bid"></param>
        /// <param name="aspNetUserID"></param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        private int InsertBidAux(SqlCommand cmd, Post bid, string aspNetUserID)
        {
            string queryString = "INSERT INTO POSTS(amount, price, isComplete, type, account_id_fk, Currencies_id_fk)" +
              "OUTPUT INSERTED.ID VALUES(@amount, @price, @isComplete, @type, (select id from accounts where aspnetusers_id_fk = @aspNetId), (select id from currencies where currencytype = @cType))";
            int result = 0;
            try
            {
                cmd.CommandText = queryString;
                cmd.Parameters.AddWithValue("amount", bid.Amount);
                cmd.Parameters.AddWithValue("price", bid.Price);
                cmd.Parameters.AddWithValue("isComplete", bid.IsComplete);
                cmd.Parameters.AddWithValue("type", bid.Type);
                cmd.Parameters.AddWithValue("aspNetId", aspNetUserID);
                cmd.Parameters.AddWithValue("cType", bid.Currency.Type);
                object obj = cmd.ExecuteScalar();
                result = Convert.ToInt32(obj);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Could not insert bid");
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex, "Could not convert id");
            }

            return result;
        }

        /// <summary>
        /// Insert the Offer into the database
        /// </summary>
        /// <param name="bid"></param>
        public bool InsertOffer(Post offer, string aspNetUserId)
        {
            CurrencyDBAccess currencyDBaccess = new(this._configuration);
            int changes = 0;
            string queryString = "INSERT INTO POSTS(amount, price, isComplete, type, account_id_fk, Currencies_id_fk)" +
              "OUTPUT INSERTED.ID VALUES(@amount, @price, @isComplete, @type, (select id from accounts where aspnetusers_id_fk = @aspNetId), (select id from currencies where currencytype = @cType))";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    using (SqlCommand insertCommand = conn.CreateCommand())
                    {
                        {
                            //Parameter binding
                            insertCommand.CommandText = queryString;
                            insertCommand.Parameters.AddWithValue("amount", offer.Amount);
                            insertCommand.Parameters.AddWithValue("price", offer.Price);
                            insertCommand.Parameters.AddWithValue("isComplete", offer.IsComplete);
                            insertCommand.Parameters.AddWithValue("type", "Offer");
                            insertCommand.Parameters.AddWithValue("aspNetId", aspNetUserId);
                            insertCommand.Parameters.AddWithValue("cType", offer.Currency.Type);
                            changes = insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Offer could not be inserted");
            }

            return changes > 0;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            List<Post> foundPosts = new List<Post>();
            string queryString = "SELECT * FROM posts INNER JOIN currencies ON posts.currencies_id_fk = currencies.id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = new((string)reader["currencytype"]);

                        Post offer = new Post
                        {
                            Id = (int)reader["id"],
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Type = (string)reader["type"],
                            Currency = generatedCurrency,
                            IsComplete = (bool)reader["isComplete"]
                        };
                        foundPosts.Add(offer);
                    }
                }
                return foundPosts;
            }
        }

        public IEnumerable<TransactionLine> GetTransactionLines(int id)
        {

            List<TransactionLine> foundLines = new List<TransactionLine>();
            string queryString = "SELECT Transactions.amount,price,date,post_bid_id_fk FROM Transactions" +
                " JOIN Posts ON Transactions.Post_offer_id_fk = Posts.id WHERE Transactions.Post_offer_id_fk = @id";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    conn.Open();
                    readCommand.Parameters.AddWithValue("id", id);

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


        public bool DeleteOffer(int id)
        {
            bool res = false;
            int changes = 0;
            string queryString = "DELETE FROM Posts WHERE id=@id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(queryString, conn))
                {
                    conn.Open();
                    deleteCommand.Parameters.AddWithValue("id", id);
                    changes = deleteCommand.ExecuteNonQuery();
                    if (changes > 0)
                    {
                        res = true;
                    }


                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Could not delete the offer");
            }
            return res;

        }


        public bool BuyOffer(Post inPost, string aspNetUserId)
        {

            AccountDBAccess accDB = new(_configuration);
            Account seller = accDB.GetAssociatedAccount(inPost.Id);
            Account buyer = accDB.GetAccountById(aspNetUserId);
            bool isComplete = CompleteOffer(inPost, buyer);
            if (seller != null && buyer != null)
            {
                //TODO update wallet for seller & buyer
            }
            return isComplete;
        }





        private bool CompleteOffer(Post inOffer, Account buyer)
        {
            bool res = false;
            int id = inOffer.Id;
            string updatePosts = "UPDATE Posts SET isComplete = 1 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
                using (SqlCommand updateCommand = conn.CreateCommand())
                {
                    try
                    {

                        updateCommand.CommandText = updatePosts;
                        updateCommand.Transaction = tran;
                        updateCommand.Parameters.AddWithValue("id", id);
                        bool complete = IsOfferComplete(id, conn, tran);
                        if (!complete)
                        {
                            int changes = updateCommand.ExecuteNonQuery();
                            complete = changes <= 0;
                        }
                        AccountDBAccess accountDBAccess = new(_configuration);

                        int postId = 0; //garbage value
                        double amount = inOffer.Amount,
                                price = inOffer.Price;
                        Currency currency = inOffer.Currency;

                        Post bid = new Post(amount, price, currency, postId, "Bid");
                        bid.IsComplete = true;

                        string aspnetUserId = accountDBAccess.GetAspnetUserId(buyer.Id, conn, tran);

                        if (!complete)
                        {
                            int bidId = InsertBidReturnBidId(bid, aspnetUserId, conn, tran);
                            complete = bidId <= 0;
                            bid.Id = bidId;
                        }
                        if (!complete)
                        {
                            //Create and persist transactionLine
                            TransactionLine transactionLine = new TransactionLine(DateTime.Now, inOffer.Amount, bid, inOffer);
                            TransactionDBAccess transactionDBAccess = new(_configuration);
                            complete = !transactionDBAccess.InsertTransactionLine(transactionLine, conn, tran);
                        }

                        if (!complete)
                        {
                            //Very ugly, but working is better than perfect in this instance. This part of the transaction only
                            //updates the buyer wallet. A better solution would be sending a delegate event to another method that
                            //updates the seller wallet.
                            CurrencyLine lineToSave = new CurrencyLine
                            {
                                Amount = inOffer.Amount,
                                Currency = inOffer.Currency,

                            };
                            bool exists = false;
                            exists = accountDBAccess.CheckCurrencyLine(aspnetUserId, lineToSave, conn, tran);
                            if (exists)
                            {
                                complete = !accountDBAccess.UpdateCurrencyLine(aspnetUserId, lineToSave, conn, tran);
                            }
                            else
                            {
                                complete = !accountDBAccess.InsertCurrencyLine(aspnetUserId, lineToSave, conn, tran);

                            }
                        }
                        if (!complete)
                        {
                            res = true;
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        throw new DatabaseException(ex, "Could not complete post");
                    }
                    catch (DatabaseException ex)
                    {
                        tran.Rollback();
                        throw new DatabaseException(ex, "Could not complete post");
                    }
                }
            }
            return res;
        }

        private bool IsOfferComplete(int id, SqlConnection conn, SqlTransaction tran)
        {
            bool res = false;
            string query = "select isComplete from Posts where id = @id";

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Transaction = tran;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool isComplete = (bool)reader["isComplete"];
                        if (isComplete)
                        {
                            res = true;
                            break;
                        }
                    }
                }

            }
            return res;
        }


        // Get offers by account/aspnetuser ID
        public IEnumerable<Post?> GetOfferPostsById(string aspNetUser)
        {
            CurrencyDBAccess cu = new CurrencyDBAccess(_configuration);
            List<Post> foundPosts = new List<Post>();
            Post post = null;
            Currency currencyType = new Currency();

            double amount = 0d;
            double price = 0d;
            bool isComplete = false;

            string type = null;
            int postId = 0;

            string queryString = "SELECT * FROM posts WHERE account_id_fk = (SELECT Accounts.id FROM Accounts WHERE Accounts.AspNetUsers_id_fk = @aspNetUser) AND type ='Offer'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();
                readCommand.Parameters.AddWithValue("aspNetUser", aspNetUser);

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        amount = (double)reader["amount"];
                        price = (double)reader["price"];
                        isComplete = (bool)reader["isComplete"];
                        type = (string)reader["type"];
                        currencyType = cu.GetCurrencyById((int)reader["currencies_id_fk"]);

                        post = new Post(amount, price, currencyType, postId, type, isComplete);
                        foundPosts.Add(post);
                    }
                }
                return foundPosts;
            }
        }

    }
}




