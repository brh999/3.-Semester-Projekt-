using Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;

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
        _serviceBaseUrl = _appConfig.Get("BaseUrl");
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
    public async Task<List<Currency>?>? GetCurrencies()
    {
        List<Currency>? res = null;

        _currencyService.UseUrl = _currencyService.BaseUrl;
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
    public async Task<bool> SaveCurrency(Currency currency)
    {
        bool success = false;
        //Create the use url to the call.
        _currencyService.UseUrl = _currencyService.BaseUrl;
        _currencyService.UseUrl = _currencyService.BaseUrl + "currency/";


        //Serialize the currency object
        var json = JsonConvert.SerializeObject(currency);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var serviceResponse = await _currencyService.CallServicePost(content);

        //Check response
        if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
        {
            success = true;
        }
        return success;
    }
}
