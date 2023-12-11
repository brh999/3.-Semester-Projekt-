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

            string queryString = "select * from Currencies JOIN Exchanges on Currencies.id = Exchanges.currencies_id_fk WHERE Currencies.id = @currencyID";

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
            string insertCurrency = "INSERT INTO Currencies OUTPUT INSERTED.id VALUES(@type)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                conn.Open();

                using (SqlCommand insertCommand = conn.CreateCommand())
                {
                    insertCommand.CommandText = insertCurrency;
                    insertCommand.Parameters.AddWithValue("type", currency.Type);

                    int returnedId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    if (returnedId > 0)
                    {
                        isSuccess = CreateExchange(currency, returnedId, conn);
                    }
                }
            }
            return isSuccess;
        }

        public bool UpdateAllCurencyValues()
        {
            bool res = false;
            string updateExchanges = "INSERT INTO Exchanges (value,date,currencies_id_fk) SELECT P.currencies_id_fk, GETDATE() AS Date, AVG(P.price)" +
                " AS value FROM Posts AS P WHERE P.isComplete = 1 GROUP BY P.currencies_id_fk";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = conn.CreateCommand())
            {
                conn.Open();
                updateCommand.CommandText = updateExchanges;

                int returnedId = Convert.ToInt32(updateCommand.ExecuteScalar());
                if (returnedId > 0)
                {
                    res = true;
                }
            }
            return res;


        }

    

    private bool CreateExchange(Currency currency, int currencyId, SqlConnection conn)
    {
        bool res = false;
        string insertExchange = "INSERT INTO Exchanges VALUES(@value, @date, @currencies_fk)";
        using (SqlCommand insertCommand = conn.CreateCommand())
        {
            insertCommand.CommandText = insertExchange;
            insertCommand.Parameters.AddWithValue("value", currency.Exchange.Value);
            insertCommand.Parameters.AddWithValue("date", currency.Exchange.Date);
            insertCommand.Parameters.AddWithValue("currencies_fk", currencyId);

            int rowsAffected = insertCommand.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                res = true;
            }
        }
        return res;
    }

    private Exchange GetExchangesForCurrency(string currencyType)
    {
        Exchange res = null;

        string queryString = "SELECT * FROM Currencies JOIN Exchanges ON currencies.id = exchanges.currencies_id_fk WHERE currencytype = @type";

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



