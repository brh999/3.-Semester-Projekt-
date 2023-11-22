using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Security
{
    public class SecurityHelper
    {
        private readonly IConfiguration _configuration;
        public SecurityHelper(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        public SymmetricSecurityKey? GetSecurityKey()
        {
            SymmetricSecurityKey? SIGNING_KEY = null;
            if (_configuration is not null)
            {
                string SECRET_KEY = _configuration["SECRET_KEY"];
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }
            return SIGNING_KEY;
        }

        public bool IsValidUsernameAndPassword(string username, string password)
        {
            string[] allowedUsernames = { _configuration["AllowDesktopApp:Username"], _configuration["AllowWebApp:Username"] };
            string[] allowedPasswords = { _configuration["AllowDesktopApp:Password"], _configuration["AllowWebApp:Password"] };
            bool credentialsOk = false;
            for (int i = 0; i < allowedUsernames.Length && !credentialsOk; i++)
            {
                credentialsOk = (username.Equals(allowedUsernames[i])) && (password.Equals(allowedPasswords[i]));
            }
            return credentialsOk;
        }
    }
}
