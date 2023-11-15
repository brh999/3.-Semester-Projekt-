using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class BidDBAccess : IBidDBAccess
    {
        private IConfiguration _configuration { get; set; }

        public BidDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Bid> GetAllBids()
        {
            List<Bid> res = null;
            string? connectionString = _configuration.GetConnectionString("hildur");

            //TODO lav "*" om til individuelle kollonner
            string queryString = "select * from accounts";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //TODO add error chekcing mayhaps?

            }
            return res;
        }
    }
