using Models;
using System.Collections.Specialized;

namespace WebAppWithAuthentication.Service
{
    public class CurrencyService : ICurrencyService
    {
        readonly IServiceConnection _currencyService;
        readonly string? _serviceUseUrl;
        readonly string? _serviceBaseUrl;
        private readonly NameValueCollection _appConfig;

        public CurrencyService()
        {
            _appConfig = System.Configuration.ConfigurationManager.AppSettings;
            _serviceBaseUrl = _appConfig.Get("BaseUrl");
            if (!string.IsNullOrEmpty(_serviceBaseUrl) )
            {
                _serviceUseUrl = _serviceBaseUrl + "api/";
            }
            _currencyService = new ServiceConnection(_serviceUseUrl);
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            List<Currency> currencies = new();

            _currencyService.UseUrl = _currencyService.BaseUrl + "currency/";
            var httpResponse = await _currencyService.CallServiceGet();
            if(httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
            var content = await httpResponse.Content.ReadAsAsync<List<Currency>>();

                if (content == null)
                {
                    currencies = new();
                }
                else
                {
                    currencies = content;
                }

            }
            return currencies;
        }
    }
}
