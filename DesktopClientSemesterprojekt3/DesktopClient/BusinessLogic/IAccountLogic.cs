using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal interface IAccountLogic
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<IEnumerable<CurrencyLine>> GetRelatedCurrencyLines(Account item);
    }
}
