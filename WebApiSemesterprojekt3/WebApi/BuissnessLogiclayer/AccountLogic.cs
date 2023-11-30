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

        public Account? GetAccountById(string id)
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

        public List<CurrencyLine> GetRelatedCurrencyLines(int id)
        {
            IEnumerable<CurrencyLine> foundCurrencyLines;

            foundCurrencyLines = _dataAccess.GetCurrencyLines(id);

            return (List<CurrencyLine>)foundCurrencyLines;
        }
    }
}
