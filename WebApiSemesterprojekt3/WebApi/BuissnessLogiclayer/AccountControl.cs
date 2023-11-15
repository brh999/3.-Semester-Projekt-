using WebApi.Database;
using WebApi.Model;

namespace WebApi.BuissnessLogiclayer
{
    public class AccountControl : IAccountControl
    {
        private readonly IAccountDBAccess _dataAccess;
        public AccountControl(IAccountDBAccess inDataAccess)
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
