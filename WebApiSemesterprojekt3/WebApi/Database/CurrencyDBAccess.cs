using Dapper;
using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class CurrencyDBAccess : ICurrencyDBAccess
    {
        private IConfiguration _configuration;
        private string? _connectionString;

        public CurrencyDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        public int GetCurrencyID(Currency item)
        {

            int itemId = 0;
            string queryString = "SELECT id FROM Currencies WHERE currencytype = @type";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand insertCommand = conn.CreateCommand())
                {

                    insertCommand.CommandText = queryString;
                    string itemType = item.Type.ToString();
                    insertCommand.Parameters.AddWithValue("type", itemType);
                    var result = insertCommand.ExecuteScalar();

                    if (result != null)
                    {
                        itemId = Convert.ToInt32(result);

                    }
                }
                return itemId;
            }
        }

        public IEnumerable<Currency> GetCurrencyList()
        {
            List<Currency> currencies = new List<Currency>();
            string queryString = "SELECT * FROM Currencies";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand selectCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();
                

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Currency currency = new Currency()
                        { 
                            
                            Type = (string)reader["currencytype"]

                        };
                        currencies.Add(currency);
                    }
                }
            }

            return currencies;
        }

    }
}



