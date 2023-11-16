using Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace WebApi.Database
{
    public class OfferDBAccess : IOfferDBAccess
    {

        private IConfiguration _configuration { get; set; }
        private string? connectionString; 

        public OfferDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("hildur");
        }
        public List<Offer> GetAllOffers()
        {
            List<Offer> foundOffers = new List<Offer>();


            string queryString = "SELECT * FROM posts WHERE type ='offer'";

            using (SqlConnection conn = new SqlConnection(connectionString))
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


    }
}

