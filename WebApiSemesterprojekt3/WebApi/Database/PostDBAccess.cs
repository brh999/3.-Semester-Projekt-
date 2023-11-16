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
            _connectionString = _configuration.GetConnectionString("hildur");
        }


        public IEnumerable<Bid> GetBidPosts()
        {
            List<Bid> foundBids = new List<Bid>();
            string queryString = "SELECT * FROM posts INNER JOIN currencies ON posts.currency_id_fk = currencies.exchange_id_fk WHERE posts.type = 'bid'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = CreateCurrency((string)reader["type"]);
                        Post bids = new Bid
                        {
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                        };
                        foundBids.Add((Bid)bids);
                    }
                }
                return foundBids;
            }
        }

        public IEnumerable<Offer> GetOfferPosts()
        {

            List<Offer> foundOffers = new List<Offer>();
            string queryString = "SELECT * FROM posts INNER JOIN currencies ON posts.currency_id_fk = currencies.exchange_id_fk WHERE posts.type = 'offer'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency generatedCurrency = CreateCurrency((string)reader["type"]);
                        Post offer = new Offer
                        {
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"],
                            Currency = generatedCurrency,
                        };
                        foundOffers.Add((Offer)offer);
                    }
                }
                return foundOffers;
            }
        }

        private Currency CreateCurrency(string inType)
        {
            Currency res = null;

            CurrencyEnum currency;
            if (Enum.TryParse<CurrencyEnum>(inType, out currency))
            {
                res = new Currency(currency, new List<Exchange>());
            }
            else
            {
                throw new ArgumentException("Currency does not exist");
            }
            return res;
        }

        //TODO contiue implementing this!!
        public Bid InsertBid(Bid bid)
        {
            string queryString = "INSERT INTO POSTS(amount, price, isComplete, type, account_id_fk, currency_id_fk) " +
                "OUTPUT INSERTED.ID VALUES (@amount, @price, @isComplete, @type, @, 1);";

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand insertCommand = new SqlCommand(queryString, conn);
            {
                conn.Open();

            }
            throw new NotImplementedException();

        }

        public Offer InsertOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
    }
}