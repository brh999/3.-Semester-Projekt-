namespace WebAppSemesterProject.Servicelayer {
    public interface IServiceConnection {

        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public HttpClient HttpEnabler { get; set; }

        Task<HttpResponseMessage?> CallServicePost(HttpRequestMessage postRequest);

        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete();

    }
}
