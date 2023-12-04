using Models;
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
                            Type = (string)reader["type"],
                            IsComplete = (bool)reader["isComplete"],
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
        public bool InsertOffer(Post offer, string aspNetUserId)
        {
            CurrencyDBAccess currencyDBaccess = new(this._configuration);
            int changes = 0;
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
                        
                        insertCommand.Parameters.AddWithValue("aspNetId", aspNetUserId);
                        insertCommand.Parameters.AddWithValue("cType", offer.Currency.Type);
                        changes = insertCommand.ExecuteNonQuery();
                    }

                }

            }
            return changes > 0;
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


        public bool DeleteOffer(int id)
        {
            bool res = false;
            int changes = 0;
            string queryString = "DELETE FROM Posts WHERE id=@id";

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
                return res;

            }
        }


        public bool BuyOffer(Post inPost, string aspNetUserId)
        {
            bool res = false;
            AccountDBAccess accDB = new(_configuration);
            Account seller = GetAssociatedAccount(inPost.Id);
            Account buyer = accDB.GetAccountById(aspNetUserId);
            bool isComplete = CompletePost(inPost, buyer);
            if(seller != null && buyer != null)
            {
                
            }
            if (isComplete)
            {
                res = true;
            }
            return res;
        }

        public Account GetAssociatedAccount(int postId)
        {
            Account res = null;
            string queryString = "SELECT Accounts.AspNetUsers_id_fk FROM Posts INNER JOIN Accounts ON Posts.account_id_fk=accounts.id WHERE Posts.id = @POSTID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new(queryString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@POSTID", postId);

                var accountId = cmd.ExecuteScalar();

                AccountDBAccess accDB = new(_configuration);

                res = accDB.GetAccountById((string)accountId);
            }

            return res;
        }

        private bool CompletePost(Post inPost, Account buyer)
        {
            bool res = false;
            int id = inPost.Id;
            string query = "update Posts set isComplete = 1, account_id_fk = @buyerID where id = @id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("buyerID", buyer.Id);
                    int row = cmd.ExecuteNonQuery();
                    if(row != null)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

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

            string queryString = "select * from posts where account_id_fk = (select Accounts.id from Accounts where Accounts.AspNetUsers_id_fk = @aspNetUser)";

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

                        post = new Post(amount, price, currencyType, postId, type);
                        foundPosts.Add(post);
                    }
                }
                return foundPosts;
            }
        }

    }
}




