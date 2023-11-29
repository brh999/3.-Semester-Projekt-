using Dapper;
using System.Data.SqlClient;
using Models;
using Microsoft.AspNetCore.Http;
using Models.DTO;

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

        public Account GetAccountById(int id)
        {
            Account account = null;

            string queryStringAccount = "SELECT email,username,discount FROM AspNetUsers JOIN Accounts ON Accounts.AspNetUsers_id_fk = AspNetUsers.Id WHERE Accounts.id = @id";
            string queryStringWallet = "select * from CurrencyLines JOIN Currencies ON CurrencyLines.currency_id_fk = Currencies.id where Account_id_fk = @AccountId";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string email = "",
                        username = "";
                double discount = 0;

                List<CurrencyLine> wallet = new List<CurrencyLine>();
                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = queryStringAccount;
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            email = (string)reader["email"];
                            username = (string)reader["username"];
                            discount = (double)reader["discount"];
                        }

                    }

                    cmd.CommandText = queryStringWallet;
                    cmd.Parameters.AddWithValue("AccountId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string currencyType = (string)reader["currencytype"];
                            double amount = (double)reader["amount"];

                            Currency currency = new(currencyType);

                            CurrencyLine line = new CurrencyLine(amount, currency);

                            wallet.Add(line);
                        }
                    }

                    account = new Account(id, discount, username, email, wallet, new List<Post>());

                }
            }


            return account;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> foundAccounts = new List<Account>();

            string queryString = "SELECT email, username, discount, AspNetUsers_id_fk FROM AspNetUsers JOIN Accounts ON AspNetUsers.Id = Accounts.AspNetUsers_id_fk";

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
                            Username = (string)reader["AspNetUsers_id_fk"],
                            Discount = (double)reader["Discount"],
                            Email = (string)reader["email"]
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
    }
}
