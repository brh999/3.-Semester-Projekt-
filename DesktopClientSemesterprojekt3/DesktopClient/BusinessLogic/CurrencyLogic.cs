using DesktopClient.Security;
using DesktopClient.Service;
using Models;
using System.Net;

namespace DesktopClient.BusinessLogic
{
    internal class CurrencyLogic : ICurrencyLogic
    {
        readonly TokenManager _tokenManager;
        readonly CurrencyService _currencyService;
        public CurrencyLogic()
        {
            _tokenManager = new();
            _currencyService = new();
        }
        public bool CreateCurrency(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Used to fetch a list of all currencies from the database
        /// </summary>
        /// <returns>all currencies</returns>
        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            List<Currency>? foundCurrencies = null;

            // Get token
            TokenState currentState = TokenState.Valid;        // Presumed state
            string? tokenValue = await _tokenManager.GetToken(currentState);
            if (tokenValue != null)
            {
                foundCurrencies = await _currencyService.GetCurrencies(tokenValue);
                if (_currencyService.CurrentHttpStatusCode == HttpStatusCode.Unauthorized)
                {
                    currentState = TokenState.Invalid;
                }
            }
            else
            {
                currentState = TokenState.Invalid;
            }
            if (currentState == TokenState.Invalid)
            {
                tokenValue = await _tokenManager.GetToken(currentState);
                if (tokenValue != null)
                {
                    foundCurrencies = await _currencyService.GetCurrencies(tokenValue);
                }
            }
            return foundCurrencies;


        }
    }
}
