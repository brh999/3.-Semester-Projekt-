using Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;

namespace DesktopClient.Service;
public class CurrencyService : ICurrencyService
{

    readonly IServiceConnection _currencyService;
    readonly string? _serviceUseUrl;
    readonly string? _serviceBaseUrl;
    static readonly string authenType = "Bearer";
    public HttpStatusCode CurrentHttpStatusCode { get; set; }
    private readonly NameValueCollection _appConfig;

    public CurrencyService()
    {
        _appConfig = ConfigurationManager.AppSettings;
        _serviceBaseUrl = ConfigurationManager.AppSettings.Get("BaseUrl");
        if (!string.IsNullOrEmpty(_serviceBaseUrl))
        {
            _serviceUseUrl = _serviceBaseUrl + "api/";
        }
        _currencyService = new ServiceConnection(_serviceUseUrl);
    }

    // Method to retrieve Person(s)
    public async Task<List<Currency>?>? GetCurrencies(string tokenToUse)
    {
        List<Currency>? res = null;

        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);


        _currencyService.UseUrl = _currencyService.BaseUrl + "currency/";

        var serviceResponse = await _currencyService.CallServiceGet();

        if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
        {
            var responseCurrencies = await serviceResponse.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<List<Currency>>(responseCurrencies);
        }

        if (res == null)
        {
            res = new List<Currency>();
        }

        return res;


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
