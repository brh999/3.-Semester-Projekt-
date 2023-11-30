using DesktopClient.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal class AccountLogic : IAccountLogic
    {

        private IAccountService _accountService;

        public AccountLogic()
        {
            _accountService = new AccountService();
        }
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            IEnumerable<Account>? foundAccounts;
            foundAccounts = await _accountService.GetAccounts();
            if (foundAccounts == null)
            {
                foundAccounts = new List<Account>();
            }



            return foundAccounts;
        }

        public async Task<IEnumerable<CurrencyLine>> GetRelatedCurrencyLines(Account item)
        {
            IEnumerable<CurrencyLine> foundCurrencyLines;
            foundCurrencyLines = await _accountService.GetRelatedCurrencyLines(item);
            if (foundCurrencyLines == null)
            {
                foundCurrencyLines = new List<CurrencyLine>();
            }
            return foundCurrencyLines;
        }
    }
}
