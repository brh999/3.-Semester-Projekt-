using Dapper;
using System.Data.SqlClient;
using Models;
using Microsoft.AspNetCore.Http;
using Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

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
            Account account = null;

            string queryStringAccount = "SELECT Accounts.id,email, username, discount FROM Accounts JOIN AspNetUsers ON Accounts.AspNetUsers_id_fk = AspNetUsers.Id WHERE Accounts.AspNetUsers_id_fk = @id";
            string queryStringWallet = "SELECT * FROM CurrencyLines JOIN Currencies ON CurrencyLines.currency_id_fk = Currencies.id JOIN Posts ON CurrencyLines.Account_id_fk = Posts.Account_id_fk WHERE CurrencyLines.Account_id_fk = @accountId;";


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


                            //    //sets the values for an accounts posts

                            //    double offerAmount = Convert.ToDouble(reader[7]);
                            //    double offerPrice = (double)reader["price"];
                            //    //bool offerIsComplete = (bool)reader["isComplete"];

                            //    Currency offerCurr = new Currency("USD");

                            //    //TestTransaction
                            //    TransactionLine tl = new TransactionLine();



                            //    Post offerPost = new Offer
                            //    {
                            //        Amount = offerAmount,
                            //        Price = offerPrice,
                            //        Currency = offerCurr,
                            //        Transactions = new List<TransactionLine>()  


                            //    };

                            //    postList.Add(offerPost);

                        }
                    }
                    PostDBAccess pa = new PostDBAccess(_configuration);
                    List<Post> po = (List<Post>)pa.GetOfferPosts();

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
                "CurrencyLines.currency_id_fk = Currencies.id JOIN Exchanges ON Currencies.exchange_id_fk = Exchanges.id WHERE account_id_fk = @id";
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
    }
}
