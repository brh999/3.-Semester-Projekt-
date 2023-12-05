using DesktopClient.Service;
using System.Collections.Specialized;
using System.Configuration;

namespace DesktopClient.Security
{
    public class TokenManager : ITokenManager
    {

        private readonly NameValueCollection _tokenAdminValues;          // To hold AppSettings

        public TokenManager()
        {
            _tokenAdminValues = ConfigurationManager.AppSettings;
        }

        public async Task<string?> GetToken()
        {
            string? foundToken = null;
            TokenState currentState = GetJWTState();
            if (currentState == TokenState.Valid)
            {
                foundToken = GetTokenExisting();
            }
            if (currentState == TokenState.Invalid)
            {
                foundToken = await GetTokenNew();
            }
            return foundToken;
        }

        private TokenState GetJWTState()
        {
            JWT.TokenState = TokenState.Invalid;
            string? currentJWT = JWT.CurrentJWT;
            if (currentJWT != null)
            {
                TokenService tSA = new TokenService();
                bool hasTokenExpired = tSA.HasTokenExpired(currentJWT);

                if (!hasTokenExpired)
                {
                    JWT.TokenState = TokenState.Valid;
                }
            }
            return JWT.TokenState;
        }

        /// <summary>
        /// Used in GetToken() to return an existing, valid JWT token
        /// </summary>
        /// <returns>JWT token</returns>
        private string? GetTokenExisting()
        {
            string? foundToken = JWT.CurrentJWT;
            return foundToken;
        }

        /// <summary>
        /// Used in GetToken() to create and return a JWT token
        /// user data is fetched and a token is generated with
        /// that data
        /// </summary>
        /// <returns>the very same JWT token</returns>
        private async Task<string?> GetTokenNew()
        {
            string? foundToken;

            // Get AccountData
            ApiAccount accounddata = GetApiAccountCredentials();

            // Access a new Token from service (Web API)
            TokenService tokenServiceAccess = new TokenService();
            foundToken = await tokenServiceAccess.GetNewToken(accounddata);

            if (foundToken != null)
            {
                JWT.CurrentJWT = foundToken;
            }

            return foundToken;
        }

        // Get application credentials from configuration (AppSettings)
        /// <summary>
        /// Used int GetTokenNew() to fetch data which is
        /// used to generate a new JWT
        /// </summary>
        /// <returns> the ApiAccount used to generate the token</returns>
        private ApiAccount GetApiAccountCredentials()
        {
            ApiAccount foundData = new ApiAccount();

            if (_tokenAdminValues.HasKeys())
            {
                foundData.Username = _tokenAdminValues.Get("Username");
                foundData.Password = _tokenAdminValues.Get("Password");
                foundData.GrantType = _tokenAdminValues.Get("GrantType");
            }

            return foundData;
        }
    }
}
