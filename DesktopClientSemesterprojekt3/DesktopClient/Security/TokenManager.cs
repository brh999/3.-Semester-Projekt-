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

        // Relay for calling appropriate method according to TokenState
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

        // Get existing JWT token
        private string? GetTokenExisting()
        {
            string? foundToken = JWT.CurrentJWT;
            return foundToken;
        }

        // Manage retrieval and persistence of new token value
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
