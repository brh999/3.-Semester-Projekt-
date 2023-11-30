using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    internal interface IAccountService
    {
        Task<List<Account>?>? GetAccounts();
        Task<IEnumerable<CurrencyLine>?>? GetRelatedCurrencyLines(Account item);
    }
}
