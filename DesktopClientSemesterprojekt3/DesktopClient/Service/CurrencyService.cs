<<<<<<< HEAD
﻿using Microsoft.Extensions.Configuration;
using Models;
=======
﻿using Models;
using Newtonsoft.Json;
>>>>>>> d0ff48fbc5a8830e81ad187c6f2443edfac5d9a7
using System.Net;

namespace DesktopClient.Service;
public class CurrencyService : ICurrencyService
{

    readonly IServiceConnection _currencyService;
<<<<<<< HEAD
    readonly string? _serviceUseUrl;
=======
    readonly String _serviceBaseUrl = "http://localhost:5042/api";
>>>>>>> d0ff48fbc5a8830e81ad187c6f2443edfac5d9a7
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
        List<Currency>? res = null;

        _currencyService.UseUrl = _currencyService.BaseUrl;

        // Must add Bearer token to request header
        string bearerTokenValue = authenType + " " + tokenToUse;
        _currencyService.HttpEnabler.DefaultRequestHeaders.Remove("Authorization");   // To avoid more Authorization headers
        _currencyService.HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);


        _currencyService.UseUrl = _currencyService.BaseUrl + "/currency";

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
