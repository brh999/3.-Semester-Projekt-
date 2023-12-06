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

        public Currency? GetCurrencyById(int currencyID)
        {
            Currency? res = null;

            string queryString = "select * from Currencies JOIN Exchanges on Exchanges.id = Currencies.exchange_id_fk WHERE Currencies.id = @currencyID";



            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand selectCommand = new SqlCommand(queryString, conn))
            {

                selectCommand.Parameters.AddWithValue("@currencyID", currencyID);

                conn.Open();
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        double value = (double)reader["value"];
                        DateTime date = (DateTime)reader["date"];
                        res = new Currency()
                        {
                            Type = (string)reader["currencytype"],
                            Exchange = new Exchange(value, date)
                        };

                    }

                }
            }
            return res;
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

                            Type = (string)reader["currencytype"],
                            Exchange = GetExchangesForCurrency((string)reader["currencytype"])
                        };
                        currencies.Add(currency);
                    }
                }
            }

            return currencies;
        }

        public bool InsertCurrency(Currency currency)
        {
            bool isSuccess = false;
            string insertCurrency = "INSERT INTO Currencies VALUES(@type,@exchange_id)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                
                conn.Open();
                
                using (SqlCommand insertCommand = conn.CreateCommand())
                {
                    insertCommand.CommandText = insertCurrency;
                    int exchangeid = createExchange(currency, conn);
                    insertCommand.Parameters.AddWithValue("type", currency.Type);
                    insertCommand.Parameters.AddWithValue("exchange_id", exchangeid);
                    int modified = insertCommand.ExecuteNonQuery();
                    if(modified > 0)
                    {
                        isSuccess = true;
                    }
                }
            }
            return isSuccess;
        }

        private int createExchange(Currency currency,SqlConnection conn)
        {
            int id = 0;
            string insertExchange = "INSERT INTO Exchanges OUTPUT INSERTED.id VALUES(@value, @date)";
            using (SqlCommand insertCommand = conn.CreateCommand())
            {
                insertCommand.CommandText = insertExchange;
                insertCommand.Parameters.AddWithValue("value", currency.Exchange.Value);
                insertCommand.Parameters.AddWithValue("date", currency.Exchange.Date);
                var res = insertCommand.ExecuteScalar();
                if(res != null)
                {
                    id = Convert.ToInt32(res);
                }
            }
            return id;
             
        }

        private Exchange GetExchangesForCurrency(string currencyType)
        {
            Exchange res = null;

            string queryString = " SELECT * FROM Exchanges INNER JOIN Currencies ON Exchanges.ID = Currencies.Exchange_id_fk WHERE currencytype = @type";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand selectCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();
                selectCommand.Parameters.AddWithValue("type", currencyType);

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Exchange exchange = new Exchange()
                        {
                            Value = (double)reader["value"],
                            Date = (DateTime)reader["date"],

                        };
                        res = exchange;
                    }
                }
                return res;
            }


        }

    }
}



