using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Security;

namespace WebApi.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly int ttlInMinutes = 10;

        private readonly IConfiguration _configuration;


        // Fetches configuration from more sources
        public TokenController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        [Route("/token")]
        [HttpPost]
        // Generate and return a JWT token
        public IActionResult Create(string username, string password, string grant_type)
        {
            IActionResult foundToken;
            bool hasInput = ((!String.IsNullOrWhiteSpace(username)) && (!String.IsNullOrWhiteSpace(password)));
            // Only return JWT token if credentials are valid
            SecurityHelper secUtil = new SecurityHelper(_configuration);
            if (hasInput && secUtil.IsValidUsernameAndPassword(username, password))
            {
                string jwtToken = GenerateToken(username, grant_type);
                foundToken = new ObjectResult(jwtToken);
            }
            else
            {
                foundToken = StatusCode(401); //unauthorized
            }
            return foundToken;
        }

        private string GenerateToken(string username, string grantType)
        {
            string tokenString;
            SecurityHelper secUtil = new SecurityHelper(_configuration);

            // Create header with algorithm and token type - and secret added
            SymmetricSecurityKey? SIGNING_KEY = secUtil.GetSecurityKey();
            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(credentials);

            // Time to live for newly created JWT Token
            DateTime expiry = DateTime.UtcNow.AddMinutes(ttlInMinutes);
            int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            JwtPayload payload = null;

            if (username == _configuration["AllowDesktopApp:Username"])
            {
                payload = new JwtPayload {
                    { "sub", "Desktop Authentication" },
                    { "Name", username },
                    { "email", "exchangetest@testmail.com" },
                    { "granttype", grantType },
                    { "exp", ts },
                    { "iss", "http://localhost:5042" }, // Issuer - the party that generates the JWT
                    { "aud", "http://localhost:5042" }  // Audience - The address of the resource
                };
            }
            else if (username == _configuration["AllowWebApp:Username"])
            {
                payload = new JwtPayload {
                    { "sub", "Web App Authentication" },
                    { "Name", username },
                    { "email", "exchangetest@testmail.com" },
                    { "granttype", grantType },
                    { "exp", ts },
                    { "iss", "http://localhost:5042" }, // Issuer - the party that generates the JWT
                    { "aud", "http://localhost:5042" }  // Audience - The address of the resource
                };
            }

            JwtSecurityToken secToken = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            tokenString = handler.WriteToken(secToken);

            return tokenString;
        }


    }
}
