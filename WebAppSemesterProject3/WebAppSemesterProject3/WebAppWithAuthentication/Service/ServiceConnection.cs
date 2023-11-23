namespace WebAppWithAuthentication.Service {
    public class ServiceConnection : IServiceConnection {
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; set; }

        public ServiceConnection(String inBaseUrl) {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }


        public async Task<HttpResponseMessage?> CallServiceGet() {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.GetAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await HttpEnabler.PostAsync(UseUrl, postJson);
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

        public Task<HttpResponseMessage?> CallServicePut(StringContent postJson) {
            throw new NotImplementedException();
        }
        public Task<HttpResponseMessage?> CallServiceDelete() {
            throw new NotImplementedException();
        }

    
    }
}
