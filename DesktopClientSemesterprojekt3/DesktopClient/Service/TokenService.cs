using DesktopClient.Security;
using System.Collections.Specialized;
using System.Configuration;

namespace DesktopClient.Service
{
    public class TokenService : ITokenService
    {

        readonly IServiceConnection _tokenService;
        readonly string _serviceUseUrl;
        readonly string? _serviceBaseUrl;
        private readonly NameValueCollection _appConfig;


        public TokenService()
        {
            _appConfig = ConfigurationManager.AppSettings;
            _serviceBaseUrl = _appConfig.Get("BaseUrl");
            if (_serviceBaseUrl is not null)
            {
                _serviceUseUrl = _serviceBaseUrl;

            }
            _tokenService = new ServiceConnection(_serviceUseUrl);
        }

        /// <summary>
        /// Used to generate a JWT token
        /// </summary>
        /// <param name="accountToUse">An account which needs a JWT token to access the data</param>
        /// <returns>a new JWT token which is used later</returns>
        public async Task<string?> GetNewToken(ApiAccount accountToUse)
        {
            string? retrievedToken = null;

            /* Create elements for HTTP request */
            _tokenService.UseUrl = _tokenService.BaseUrl;
            _tokenService.UseUrl += "token/";
            var uriToken = new Uri(string.Format(_tokenService.UseUrl));

            // Provide username, password and grant_type for the authentication. Content (body data) are posted in. 
            HttpContent appAdminLogin = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", accountToUse.GrantType),
                new KeyValuePair<string, string>("username", accountToUse.Username),
                new KeyValuePair<string, string>("password", accountToUse.Password)
            });

            /* Assemble HTTP request */
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uriToken,
                Content = appAdminLogin
            };

            /* Call service */
            try
            {
                var response = await _tokenService.CallServicePost(request);

                response?.EnsureSuccessStatusCode();     // Throws exception if not successful

                if (response != null)
                {
                    retrievedToken = await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                retrievedToken = null;
            }
            return retrievedToken;
        }
    }

}
