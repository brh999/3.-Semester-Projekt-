using System.Collections.Specialized;
using WebAppWithAuthentication.Service;

namespace WebAppWithAuthentication.Security
{
    public class TokenManager : ITokenManager
    {
        private readonly NameValueCollection _tokenAdminValues;


        public TokenManager()
        {
            _tokenAdminValues = System.Configuration.ConfigurationManager.AppSettings;
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


        // Checks the current JWT and see if it's valid
        private TokenState GetJWTState()
        {
            JWT.TokenState = TokenState.Invalid;
            string? currentJWT = JWT.CurrentJWT;
            if (currentJWT != null)
            {
                TokenServiceAccess tSA = new TokenServiceAccess();
                bool hasTokenExpired = tSA.HasTokenExpired(currentJWT);

                if (!hasTokenExpired)
                {
                    JWT.TokenState = TokenState.Valid;
                }
            }
            return JWT.TokenState;
        }


        private string? GetTokenExisting()
        {
            string? foundToken = JWT.CurrentJWT;
            return foundToken;
        }


        private async Task<string?> GetTokenNew()
        {
            string? foundToken;
            ApiAccount accountdata = GetApiAccountCredentials();

            TokenServiceAccess tokenServiceAccess = new TokenServiceAccess();
            foundToken = await tokenServiceAccess.GetNewToken(accountdata);

            if (foundToken != null)
            {
                JWT.CurrentJWT = foundToken;
            }
            return foundToken;
        }

        private ApiAccount GetApiAccountCredentials()
        {
            ApiAccount foundData = new ApiAccount();

            if (_tokenAdminValues.HasKeys())
            {
                foundData.Password = _tokenAdminValues.Get("Password");
                foundData.GrantType = _tokenAdminValues.Get("GrantType");
                foundData.Username = _tokenAdminValues.Get("Username");
            }

            return foundData;
        }

        private string? GetApplicationAssemblyName()
        {
            string? assemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
            return assemblyName;
        }

    }
}
