using Microsoft.Extensions.Configuration;
using Models;
using System.Net;

namespace DesktopClient.Service;
public class CurrencyService : ICurrencyService
{

    readonly IServiceConnection _currencyService;
    readonly string? _serviceUseUrl;
    static readonly string authenType = "Bearer";
    public HttpStatusCode CurrentHttpStatusCode { get; set; }
    readonly IConfiguration _configuration;

    public CurrencyService(IConfiguration configuration)
    {
        _configuration = configuration;
        string? baseUrl = _configuration.GetConnectionString("BaseUrl");

        if (baseUrl is not null) { _serviceUseUrl = baseUrl + "api/"; }

        _currencyService = new ServiceConnection(_serviceUseUrl);
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


        throw new NotImplementedException();
    }

    public async Task<int> SaveCurrency(string tokenToUse, Currency personToSave)
    {
        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

        throw new NotImplementedException();
    }
}
