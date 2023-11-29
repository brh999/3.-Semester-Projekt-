using System.IdentityModel.Tokens.Jwt;

namespace WebAppWithAuthentication.Service
{
    public class TokenServiceAccess : ITokenServiceAccess
    {
        readonly IServiceConnection _tokenService;
        IConfiguration _configuration;
        readonly String _serviceBaseUrl;
        public TokenServiceAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBaseUrl = configuration.GetConnectionString("BaseUrl");

            _tokenService = new ServiceConnection(_serviceBaseUrl);

        }


        public async Task<string?> GetNewToken(ApiAccount accountToUse)
        {
            string? retrievedToken = null;

            _tokenService.UseUrl = _tokenService.BaseUrl;
            _tokenService.UseUrl += "/" + "token";
            var uriToken = new Uri(string.Format(_tokenService.UseUrl));

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
                var response = await _tokenService.CallServicePost(request);
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
