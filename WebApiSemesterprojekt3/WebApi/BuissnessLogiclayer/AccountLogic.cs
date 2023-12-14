using WebApi.Database;
using Models;
using DTO;

namespace WebApi.BuissnessLogiclayer
{
    public class AccountLogic  : IAccountLogic
    {
        private readonly IAccountDBAccess _dataAccess;
        public AccountLogic(IAccountDBAccess inDataAccess)
        {
            _dataAccess = inDataAccess;
        }

        public AccountDto? GetAccountById(string id)
        {
            Account? account = null;
            AccountDto? result = null;
            account = _dataAccess.GetAccountById(id);
            if(account != null)
            {
                result = new AccountDto(account);
            }
            return result;
        }

        public List<AccountDto> GetAllAccounts()
        {
            List<Account> foundAccounts;
            List<AccountDto> accounts = new List<AccountDto>();
            foundAccounts = _dataAccess.GetAllAccounts();
            foreach (Account a in foundAccounts)
            {
                if (a != null) {
                    AccountDto aD = new AccountDto(a);
                    accounts.Add(aD);
                }
            }
            return accounts;
        }

        public List<CurrencyLine> GetRelatedCurrencyLines(int id)
        {
            IEnumerable<CurrencyLine> foundCurrencyLines;

            foundCurrencyLines = _dataAccess.GetCurrencyLines(id);

            return (List<CurrencyLine>)foundCurrencyLines;
        }

        public bool InsertCurrencyLine(string aspDotNetId, CurrencyLine currencyLine)
        {
            bool res = false;
            bool exists = false;
            exists = _dataAccess.CheckCurrencyLine(aspDotNetId, currencyLine);
            if (exists)
            {
                
                res = _dataAccess.UpdateCurrencyLine(aspDotNetId, currencyLine);
            }
            else
            {
                res = _dataAccess.InsertCurrencyLine(aspDotNetId, currencyLine);
            }
            
            return res;
        }
    }
}
