namespace DesktopClient.Service
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(String? inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }


        public HttpClient HttpEnabler { get; init; }
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        
        public async Task<HttpResponseMessage?> CallServiceGet()
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
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
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
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
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
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
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.DeleteAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await HttpEnabler.SendAsync(postRequest);
            }
            return hrm;
        }


    }
}

