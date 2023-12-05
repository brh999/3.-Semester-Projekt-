using Models;
using System.Net;

namespace DesktopClient.Service
{
    public interface ICurrencyService
    {
        Task<List<Currency>?>? GetCurrencies();
        Task<int> SaveCurrency(Currency currencyToSave);
        HttpStatusCode CurrentHttpStatusCode { get; set; }

    }
}
