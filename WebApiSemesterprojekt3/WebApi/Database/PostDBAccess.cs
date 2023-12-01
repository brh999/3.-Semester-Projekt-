using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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


        /// <summary>
        /// Get all bid Post
        /// </summary>
        /// <returns></returns>

        public PostDBAccess()
        {

        }


        public IEnumerable<Post> GetBidPosts()
        {
            List<Post> foundBids = new List<Post>();
            string queryString = "SELECT * FROM posts INNER JOIN currencies ON posts.currencies_id_fk = currencies.exchange_id_fk WHERE posts.type = 'bid'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = CreateCurrency((string)reader["currencytype"]);
                        Post bids = new Post
                        {
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                            Id = (int)reader["id"],
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
            string queryString = "SELECT * FROM posts JOIN currencies ON posts.currencies_id_fk = currencies.exchange_id_fk WHERE posts.type = 'offer'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = CreateCurrency((string)reader["currencytype"]);
                        Post offer = new Post
                        {
                            Id = (int)reader["id"],
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                        };
                        foundOffers.Add(offer);
                    }
                }
                return foundOffers;
            }
        }

        private Currency CreateCurrency(string inType)
        {
            Currency res = new Currency(new Exchange(), inType);

            return res;

        }
        /// <summary>
        /// Insert the Bid into the database
        /// </summary>
        /// <param name="bid"></param>
        //TODO contiue implementing this!!
        public void InsertBid(Post bid)
        {
            CurrencyDBAccess currencyDBaccess = new(this._configuration);
            int res = 0;
            string queryString = "INSERT INTO POSTS(amount, price, isComplete, type, account_id_fk, currency_id_fk) " +
                "OUTPUT INSERTED.ID VALUES (@amount, @price, @isComplete, @type, @account_id_fk, @currency_id_fk);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                using (SqlCommand insertCommand = conn.CreateCommand())
                {
                    {
                        insertCommand.Transaction = transaction;

                        int currencyType = currencyDBaccess.GetCurrencyID(bid.Currency);
                        insertCommand.CommandText = queryString;
                        insertCommand.Parameters.AddWithValue("amount", bid.Amount);
                        insertCommand.Parameters.AddWithValue("price", bid.Price);
                        insertCommand.Parameters.AddWithValue("isComplete", bid.IsComplete);
                        insertCommand.Parameters.AddWithValue("type", "Bid");
                        //TODO: actually add an account
                        insertCommand.Parameters.AddWithValue("account_id_fk", "1");
                        insertCommand.Parameters.AddWithValue("currency_id_fk", currencyType);
                        insertCommand.ExecuteNonQuery();
                        transaction.Commit();

                    }

                }

            }
        }

        /// <summary>
        /// Insert the Offer into the database
        /// </summary>
        /// <param name="bid"></param>
        public void InsertOffer(Post offer,string aspNetUserId)
        {
            CurrencyDBAccess currencyDBaccess = new(this._configuration);
            int res = 0;
            string queryString = "INSERT INTO POSTS(amount, price, isComplete, type, account_id_fk, Currencies_id_fk)" +
              "OUTPUT INSERTED.ID VALUES(@amount, @price, @isComplete, @type, (select id from accounts where aspnetusers_id_fk = @aspNetId), (select id from currencies where currencytype = @cType))";

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
                        //TODO: actually add an account
                        insertCommand.Parameters.AddWithValue("aspNetId", aspNetUserId);
                        insertCommand.Parameters.AddWithValue("cType", offer.Currency.Type);
                        insertCommand.ExecuteScalar();
                    }

                }

            }
        }

        public IEnumerable<Post> GetAllPosts()
        {
            List<Post> foundPosts = new List<Post>();
            string queryString = "SELECT * FROM posts INNER JOIN currencies ON posts.currencies_id_fk = currencies.exchange_id_fk";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = CreateCurrency((string)reader["currencytype"]);

                        Post offer = new Post
                        {
                            Id = (int)reader["id"],
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                            Type = (string)reader["type"],
                        };
                        foundPosts.Add(offer);
                    }
                }
                return foundPosts;
            }
        }

        public IEnumerable<TransactionLine> GetTransactionLines(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("id cannot be less than 1");
            }
            

            List<TransactionLine> foundLines = new List<TransactionLine>();
            string queryString = "SELECT Transactions.amount,price,date,post_bid_id_fk FROM Transactions" +
                " JOIN Posts ON Transactions.Post_offer_id_fk = Posts.id WHERE Transactions.Post_offer_id_fk = @id";
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
                    return foundLines;
                }
            }
        }
    }
}



