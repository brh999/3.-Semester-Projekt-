using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class BidDBAccess : IBidDBAccess
    {
        private IConfiguration _configuration { get; set; }
        private string? connectionString;
        public BidDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("hildur");
        }
        public IEnumerable<Bid> GetAllBids()
        {
            List<Bid> res = null;

            //TODO lav "*" om til individuelle kollonner
            string queryString = "select * from posts where type = 'bid'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new(queryString, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Post bid = new Bid();

                    }

                }
            }
            return res;
        }
    }
}
