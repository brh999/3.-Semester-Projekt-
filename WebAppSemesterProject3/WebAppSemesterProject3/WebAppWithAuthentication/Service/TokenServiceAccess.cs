using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace WebAppWithAuthentication.Service
{
    public class TokenServiceAccess : ITokenServiceAccess
    {
        readonly IServiceConnection _tokenService;
        readonly String _serviceBaseUrl = "http://localhost:5042";
        public TokenServiceAccess()
        {
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
    }
}
