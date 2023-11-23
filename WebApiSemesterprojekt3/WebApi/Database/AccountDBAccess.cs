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

            string queryStringAccount = "select * from account";
            string queryStringWallet = "select * from CurrencyLines where Account_Wallet_fk = @accountId";


            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                account = conn.Query<Account>(queryStringAccount).Single();




                AccountDto accountDto = new AccountDto();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText = queryStringWallet;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while(reader.Read())
                        {
                            string currencyType = (string)reader["type"];
                            double amount = (double)reader["amount"];

                            Currency currency = new(new List<Exchange>(), currencyType);

                            CurrencyLine line = new CurrencyLine(amount,currency);

                            account.AddCurrencyLine(line);
                        }
                    }
                }
            }


            return account;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> res = null;
            

            //TODO lav "*" om til individuelle kollonner
            string queryString = "select * from accounts";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                //TODO add error chekcing mayhaps?
                //res = conn.Query<Account,Post,Account>(queryString, (account, post)=> {account.post = post return account }).ToList();

            }
            return res;
        }

        public bool UpdateAccountById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
