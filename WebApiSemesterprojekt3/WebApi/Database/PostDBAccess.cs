using Microsoft.Extensions.Configuration;
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
        private IEnumerable<Post> GetAllPosts(string inType)
        {
            List<Offer> foundOffers = new List<Offer>();
            //TODO find a better way to dertermine bid or offer.
            string queryString = "";
            if (inType.Equals("bid"))
            {
                string queryStringBid = "SELECT * FROM posts WHERE type ='bid' ";
            }
            else if (inType.Equals("offer"))
            {
                string queryStringBid = "SELECT * FROM posts WHERE type ='offer' ";
            } else
            {
                throw new ArgumentException();
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Post offer = new Offer
                        {
                            Amount = (int)reader["amount"],
                            Price = (double)reader["price"]

                        };
                        foundOffers.Add((Offer)offer);
                    }
                }
                return foundOffers;
            }

        }

        public IEnumerable<Bid> GetBidPosts()
        {
           return (IEnumerable<Bid>)GetAllPosts("bid");
        }

        public IEnumerable<Offer> GetOfferPosts()
        {
            return (IEnumerable<Offer>)GetAllPosts("offer");
        }
    }
}
