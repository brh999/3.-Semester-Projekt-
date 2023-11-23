using Models;
using System.Net;

namespace DesktopClient.Service
{
    public interface ICurrencyService
    {
        Task<List<Currency>?>? GetCurrencies(string tokenToUse);
        Task<int> SavePerson(string tokenToUse, Currency currencyToSave);
        HttpStatusCode CurrentHttpStatusCode { get; set; }

    }
}
