﻿using DesktopClient.Security.ApiAuthentication;

namespace DesktopClient.Service
{
    public class TokenService : ITokenService
    {

        readonly IServiceConnection _tokenService;
        readonly String _serviceBaseUrl = "https://localhost:5042";     // Insert your own port no
        public TokenService()
        {
            _tokenService = new ServiceConnection(_serviceBaseUrl);
        }

        // Fetch service from service
        public async Task<string?> GetNewToken(ApiAccount accountToUse)
        {
            string? retrievedToken = null;

            /* Create elements for HTTP request */
            _tokenService.UseUrl = _tokenService.BaseUrl;
            _tokenService.UseUrl += "/" + "token";
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
                // If response not null
                response?.EnsureSuccessStatusCode();     // Throws exception if not successful
                // If success                // If success
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