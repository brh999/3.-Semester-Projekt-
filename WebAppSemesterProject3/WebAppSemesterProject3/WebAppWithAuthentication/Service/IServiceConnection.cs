namespace WebAppWithAuthentication.Service
{
    public interface IServiceConnection
    {
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; set; }
        Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest);

    }
}
