using WebApi.Database;
using Models;

namespace WebApi.BuissnessLogiclayer
{
    public class AccountLogic  : IAccountLogic
    {
        private readonly IAccountDBAccess _dataAccess;
        public AccountLogic(IAccountDBAccess inDataAccess)
        {
            _dataAccess = inDataAccess;
        }

        public Account? GetAccountById(int id)
        {
            Account? account = null;

            account = _dataAccess.GetAccountById(id);

            return account;

        }

        public List<Account> GetAllAccounts()
        {
            List<Account> foundAccounts;

            foundAccounts = _dataAccess.GetAllAccounts();

            return foundAccounts;
        }
    }
}
