using DesktopClient.Security;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;

namespace DesktopClient.Service
{
    public class TokenService : ITokenService
    {

        private HttpClient _httpClient;
        private Uri _uri;
        private readonly NameValueCollection _apiUrl;
        readonly String _serviceBaseUrl;


        public TokenService()
        {
            _apiUrl = System.Configuration.ConfigurationManager.AppSettings;
            _serviceBaseUrl = _apiUrl.Get("BaseUrl");
            _httpClient = new HttpClient();
            _uri = new Uri(_serviceBaseUrl);
        }


        public async Task<string?> GetNewToken(ApiAccount accountToUse)
        {
            string? retrievedToken = null;
            string? uriToUse = _serviceBaseUrl + "token/";
            var uriToken = new Uri(string.Format(uriToUse));

            HttpContent appAdminLogin = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", accountToUse.GrantType),
                new KeyValuePair<string, string>("username", accountToUse.Username),
                new KeyValuePair<string, string>("password", accountToUse.Password)
            });

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uriToken,
                Content = appAdminLogin
            };

            try
            {
                var response = await _httpClient.PostAsync(uriToUse, appAdminLogin).ConfigureAwait(false);
                response?.EnsureSuccessStatusCode();
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
        public bool HasTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
            var now = DateTime.Now.ToUniversalTime();
            bool result = now >= tokenDate;
            return result;
        }

    }
}
