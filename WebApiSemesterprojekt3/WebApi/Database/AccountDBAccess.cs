using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public class AccountDBAccess : IAccountDBAccess
    {
        private string? _connectionString;
        private IConfiguration _configuration { get; set; }

        public AccountDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        public bool DeleteAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(string AspNetId)
        {
            PostDBAccess pa = new PostDBAccess(_configuration);
            Account account = null;

            string queryStringAccount = "SELECT Accounts.id,email, username, discount FROM Accounts JOIN AspNetUsers ON Accounts.AspNetUsers_id_fk = AspNetUsers.Id WHERE Accounts.AspNetUsers_id_fk = @id";
            string queryStringWallet = "SELECT amount,currencytype FROM CurrencyLines JOIN Currencies ON CurrencyLines.currency_id_fk = Currencies.id WHERE CurrencyLines.Account_id_fk = @accountId;";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string email = "",
                        username = "";
                double discount = 0;
                int accountId = -1;

                List<CurrencyLine> wallet = new List<CurrencyLine>();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = queryStringAccount;
                    cmd.Parameters.AddWithValue("id", AspNetId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountId = (int)reader["id"];
                            email = (string)reader["email"];
                            username = (string)reader["username"];
                            discount = (double)reader["discount"];
                        }

                    }

                    cmd.CommandText = queryStringWallet;
                    cmd.Parameters.AddWithValue("AccountId", accountId);
                    List<Post> postList = new List<Post>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            //sets the values for the accounts wallet
                            string currencyType = (string)reader["currencytype"];
                            double amount = (double)reader["amount"];

                            Currency currency = new(currencyType);
                            CurrencyLine line = new CurrencyLine(amount, currency);
                            wallet.Add(line);

                        }
                    }

                    List<Post> po = (List<Post>)pa.GetOfferPostsById(AspNetId);

                    account = new Account(accountId, discount, username, email, wallet, po);

                }
            }


            return account;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> foundAccounts = new List<Account>();

            string queryString = "SELECT email,Accounts.id, username, discount, AspNetUsers_id_fk FROM AspNetUsers JOIN Accounts ON AspNetUsers.Id = Accounts.AspNetUsers_id_fk";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();



                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Account account = new Account
                        {
                            Username = (string)reader["username"],
                            Discount = (double)reader["Discount"],
                            Email = (string)reader["email"],
                            Id = (int)reader["id"],
                            Wallet = new List<CurrencyLine>(),
                        };
                        foundAccounts.Add((Account)account);
                    }
                }
                return foundAccounts;
            }
        }

        public bool UpdateAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CurrencyLine> GetCurrencyLines(int id)
        {
            List<CurrencyLine> found = new List<CurrencyLine>();
            string queryString = "SELECT CurrencyLines.amount,Currencies.currencyType,Exchanges.value FROM CurrencyLines JOIN Currencies ON " +
                "CurrencyLines.currency_id_fk = Currencies.id JOIN Exchanges ON Currencies.id = Exchanges.currencies_id_fk WHERE account_id_fk = @id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, conn))
            {
                conn.Open();
                readCommand.Parameters.AddWithValue("id", id);

                using (SqlDataReader reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CurrencyLine line = new CurrencyLine
                        {
                            Amount = (double)reader["amount"],
                            Currency = new Currency
                            {
                                Type = (string)reader["currencytype"],
                                Exchange = new Exchange
                                {
                                    Value = (double)reader["value"],
                                    Date = DateTime.UtcNow
                                }
                            }
                        };
                        found.Add(line);
                    }
                    return found;
                }
            }
        }

        internal string GetAspnetUserId(int id)
        {
            string query = "SELECT AspNetUsers_id_fk FROM accounts WHERE id = @id";
            string aspnetUserId = "NOTFOUND";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                aspnetUserId = (string)reader["AspNetUsers_id_fk"];
                            }
                        }
                    }
                }

            }
            catch (SqlException)
            {
                throw new DatabaseException("Could not find aspnetuser id with the given input");
            }

            return aspnetUserId;
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

        public bool UpdateCurrencyLine(string aspDotNetId, CurrencyLine currencyLine)
        {
            bool res = false;
            string queryString = "UPDATE CurrencyLines SET amount = 999 WHERE account_id_fk = (SELECT Accounts.id FROM Accounts JOIN AspNetUsers ON Accounts.AspNetUsers_id_fk = AspNetUsers.id WHERE AspNetUsers.Id = @id";



        }
    }
}
