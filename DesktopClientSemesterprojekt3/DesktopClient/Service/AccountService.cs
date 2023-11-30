using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    internal class AccountService : IAccountService
    {

        readonly IServiceConnection _accountService;
        readonly string? _serviceUseUrl;
        readonly string? _serviceBaseUrl;
        private readonly NameValueCollection _appConfig;


        public AccountService()
        {
            _appConfig = ConfigurationManager.AppSettings;
            _serviceBaseUrl = _appConfig.Get("BaseUrl");
            if (!string.IsNullOrEmpty(_serviceBaseUrl))
            {
                _serviceUseUrl = _serviceBaseUrl + "api/";
            }
            _accountService = new ServiceConnection(_serviceUseUrl);
        }
        public async Task<List<Account>?>? GetAccounts()
        {
            List<Account>? res = null;
            _accountService.UseUrl = _accountService.BaseUrl + "account/";

            var serviceResponse = await _accountService.CallServiceGet();

            if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
            {
                var responseAccounts = await serviceResponse.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Account>>(responseAccounts);
            }
            return res;
        }

        public async Task<IEnumerable<CurrencyLine>?>? GetRelatedCurrencyLines(Account item)
        {
            List<CurrencyLine>? res = null;
            string id = item.Id.ToString();
            _accountService.UseUrl = _accountService.BaseUrl + $"account/{id}/wallet";

            var serviceResponse = await _accountService.CallServiceGet();

            if(serviceResponse != null && serviceResponse.IsSuccessStatusCode)
            {
                var responseCurrencyLines = await serviceResponse.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<CurrencyLine>>(responseCurrencyLines);
            }
            return res;

        }
    }
}
