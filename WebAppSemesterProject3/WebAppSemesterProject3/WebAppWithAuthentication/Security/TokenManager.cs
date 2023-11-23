using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWithAuthentication.Models;
using WebAppWithAuthentication.Service;

namespace PersonServiceClientDesktop.Security
{
    public class TokenManager : ITokenManager
    {
        private readonly NameValueCollection _tokenAdminValues;

        public TokenManager()
        {
            _tokenAdminValues = ConfigurationManager.AppSettings;
        }

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
            }
            foundData.Username = GetApplicationAssemblyName();

            return foundData;
        }

        private string? GetApplicationAssemblyName()
        {
            string? assemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
            return assemblyName;
        }

    }
}
