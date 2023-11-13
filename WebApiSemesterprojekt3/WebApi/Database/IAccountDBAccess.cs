using WebApi.Model;

namespace WebApi.Database
{
    public interface IAccountDBAccess
    {
        public IEnumerable<Account> GetAllAccounts();

        public Account GetAccountById(int id);

        public bool DeleteAccountById(int id);

        public bool UpdateAccountById(int id);
    }
}
