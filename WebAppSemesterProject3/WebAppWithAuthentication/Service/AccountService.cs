using Models;
using System.Collections.Specialized;

namespace WebAppWithAuthentication.Service
{

    public class AccountService : IAccountService
    {

        readonly IServiceConnection _accountService;
        readonly string? _serviceUseUrl;
        readonly string? _serviceBaseUrl;
        private readonly NameValueCollection _appConfig;
        public AccountService()
        {
            _appConfig = System.Configuration.ConfigurationManager.AppSettings;
            _serviceBaseUrl = _appConfig.Get("BaseUrl");
            if (!string.IsNullOrEmpty(_serviceBaseUrl))
            {
                _serviceUseUrl = _serviceBaseUrl + "api/";
            }
            _accountService = new ServiceConnection(_serviceUseUrl);
        }


        public async Task<Account?> GetAccountById(string aspNetId)
        {
            Account account = null;

            if (!string.IsNullOrEmpty(aspNetId))
            {
                _accountService.UseUrl = _accountService.BaseUrl + "account/" + aspNetId;
                var httpResponse = await _accountService.CallServiceGet();
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    var content = await httpResponse.Content.ReadAsAsync<Account>();

                    if (content != null)
                    {
                        account = content;
                    }

                }
            }
            return account;
        }



    }
}
