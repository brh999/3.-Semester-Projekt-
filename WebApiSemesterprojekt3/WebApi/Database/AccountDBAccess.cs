using Dapper;
using System.Data.SqlClient;
using Models;

namespace WebApi.Database
{
    public class AccountDBAccess : IAccountDBAccess
    {
        private IConfiguration _configuration { get; set; }

        public AccountDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool DeleteAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> res = null;
            string? connectionString = _configuration.GetConnectionString("hildur");

            //TODO lav "*" om til individuelle kollonner
            string queryString = "select * from accounts";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
