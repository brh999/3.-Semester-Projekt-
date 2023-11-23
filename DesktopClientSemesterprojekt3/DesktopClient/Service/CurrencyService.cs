using Models;
using System.Net;

namespace DesktopClient.Service;
public class CurrencyService : ICurrencyService
{

    readonly IServiceConnection _currencyService;
    readonly String _serviceBaseUrl = "https://localhost:5042/api/currency/";
    static readonly string authenType = "Bearer";

    public HttpStatusCode CurrentHttpStatusCode { get; set; }

    public CurrencyService()
    {
        _currencyService = new ServiceConnection(_serviceBaseUrl);
    }

    // Method to retrieve Person(s)
    public async Task<List<Currency>?>? GetCurrencies(string tokenToUse)
    {
        List<Currency>? currenciesFromService = null;

        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);



    }

    public async Task<int> SaveCurrency(string tokenToUse, Currency personToSave)
    {
        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);


    }
}
