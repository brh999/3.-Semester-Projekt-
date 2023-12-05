using DesktopClient.Security;

namespace DesktopClient.Service
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(String inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
            _url = new Uri(BaseUrl);
            HttpEnabler.BaseAddress = _url;
        }


        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; set; }
        private Uri _url;

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

        /// <summary>
        /// Used to send data to the API, which handles it and calls a post method
        /// </summary>
        /// <param name="postJson">our data which has been serialized into Json</param>
        /// <returns>a response message, 200 is what we want</returns>
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

        /// <summary>
        /// Used to send data to the API, which handles and calls a put, as in update, method
        /// </summary>
        /// <param name="postJson">our data which has been serialized into Json</param>
        /// <returns>a response message, 200 is what we want</returns>
        public async Task<HttpResponseMessage?> CallServicePut(StringContent postJson)
        {
            bool shouldRun = SetupHttpRequest().Result;
            HttpResponseMessage? hrm = null;
            if (UseUrl != null && shouldRun)
            {
                hrm = await HttpEnabler.PutAsync(UseUrl, postJson);
            }
            return hrm;
        }

        /// <summary>
        /// Used to send a delete request to the API
        /// </summary>
        /// <returns>a response message, 200 is what we want</returns>
        public async Task<HttpResponseMessage?> CallServiceDelete()
        {
            bool shouldRun = SetupHttpRequest().Result;
            HttpResponseMessage? hrm = null;
            if (UseUrl != null && shouldRun)
            {
                hrm = await HttpEnabler.DeleteAsync(UseUrl);
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

