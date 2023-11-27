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


    /// <summary>
    /// Used to fetch a list of all currencies from the database
    /// </summary>
    /// <param name="tokenToUse">JWT for Authorization</param>
    /// <returns> A list of all currencies</returns>
    public async Task<List<Currency>?>? GetCurrencies(string tokenToUse)
    {
        List<Currency>? res = null;

        _currencyService.UseUrl = _currencyService.BaseUrl;
        // START JWT
        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);
        // END JWT
        
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


    /// <summary>
    /// Used to save a new currencytype in the database to be viewed later
    /// </summary>
    /// <param name="tokenToUse"> JWT token for Authorization</param>
    /// <param name="currencyToSave"> The currency object to be persisted in the database</param>
    /// <returns>The ID of the newly inserted currency</returns>
    /// <exception cref="NotImplementedException"> Not implemented functionality</exception>
    public async Task<int> SaveCurrency(string tokenToUse, Currency currencyToSave)
    {
        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

        throw new NotImplementedException();
    }
}
