using WebAppWithAuthentication.Security;

namespace WebAppWithAuthentication.Service
{
    public class ServiceConnection : IServiceConnection
    {
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; set; }
        private Uri _url;


        public ServiceConnection(String inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
            _url = new Uri(BaseUrl);
            HttpEnabler.BaseAddress = _url;
        }



        public async Task<HttpResponseMessage?> CallServiceGet()
        {
            bool shouldRun = SetupHttpRequest().Result;
            HttpResponseMessage? hrm = null;
            if (UseUrl != null && shouldRun)
            {
                hrm = await HttpEnabler.GetAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson)
        {
            bool shouldRun = SetupHttpRequest().Result;
            HttpResponseMessage? hrm = null;
            if (UseUrl != null && shouldRun)
            {
                hrm = await HttpEnabler.PostAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest)
        {
            bool shouldRun = SetupHttpRequest().Result;
            HttpResponseMessage? hrm = null;
            if (UseUrl != null && shouldRun)
            {
                hrm = await HttpEnabler.SendAsync(postRequest);
            }
            return hrm;
        }

        public Task<HttpResponseMessage?> CallServicePut(StringContent postJson)
        {
            throw new NotImplementedException();
        }
        public Task<HttpResponseMessage?> CallServiceDelete()
        {
            throw new NotImplementedException();
        }

        private async Task<string?> GetToken()
        {
            TokenManager tokenHelp = new TokenManager();
            string? foundToken = await tokenHelp.GetToken();
            return foundToken;
        }

        private async Task<bool> SetupHttpRequest()
        {
            bool result = false;
            string? tokenValue = await GetToken();

            if (!String.IsNullOrEmpty(tokenValue))
            {
                string bearerTokenValue = "Bearer" + " " + tokenValue;
                HttpEnabler.DefaultRequestHeaders.Remove("Authorization");
                HttpEnabler.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);
                result = true;
            }

            return result;
        }


    }
}
