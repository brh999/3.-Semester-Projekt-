using Models;
using System.Net;

namespace DesktopClient.Service
{
    public interface ICurrencyService
    {
        Task<List<Currency>?>? GetCurrencies();
        Task<bool> SaveCurrency(Currency currencyToSave);
        HttpStatusCode CurrentHttpStatusCode { get; set; }

    }
}
