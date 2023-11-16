using Microsoft.AspNetCore.Http;
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
        

        public IEnumerable<Bid> GetBidPosts()
        {
            List<Bid> foundBids = new List<Bid>();
            String queryString = "SELECT * FROM posts WHERE type ='bid' ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Post bids = new Bid
                        {
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"]

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
            String queryString = "SELECT * FROM posts WHERE type ='offer'";

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
                            Amount = (double)reader["amount"],
                            Price = (double)reader["price"]

                        };
                        foundOffers.Add((Offer)offer);
                    }
                }
                return foundOffers;
            }
        }

        public Bid InsertBid(Bid bid)
        {
            
        }

        public Offer InsertOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
    }
}