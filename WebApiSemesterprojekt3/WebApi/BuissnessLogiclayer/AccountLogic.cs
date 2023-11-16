using WebApi.Database;
using Models;

namespace WebApi.BuissnessLogiclayer
{
    public class AccountLogic : IAccount
    {
        private readonly IAccountDBAccess _dataAccess;
        public AccountLogic(IAccountDBAccess inDataAccess)
        {
            _dataAccess = inDataAccess;
        }
        public List<Account> GetAllAccounts()
        {
            List<Account> foundAccounts;

            foundAccounts = _dataAccess.GetAllAccounts();

            return foundAccounts;
        }
    }
}
