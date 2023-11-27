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
        /// <summary>
        /// Used to fetch a JWT token
        /// </summary>
        /// <param name="currentState">determines if a new Token needs to be
        /// created and returned or if we can reuse the newest token</param>
        /// <returns> the JWT token new or old</returns>
        public async Task<string?> GetToken(TokenState currentState)
        {
            string? foundToken = null;
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

        // Get the process (project) assembly name (applied as application username) 
        private string? GetApplicationAssemblyName()
        {
            string? assemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
            return assemblyName;
        }
    }

}
