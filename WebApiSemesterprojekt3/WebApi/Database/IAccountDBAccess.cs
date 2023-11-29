using Models;

namespace WebApi.Database
{
    public interface IAccountDBAccess
    {
        public List<Account> GetAllAccounts();

        public Account GetAccountById(string id);

        public bool DeleteAccountById(int id);

        public bool UpdateAccountById(int id);
    }
}
