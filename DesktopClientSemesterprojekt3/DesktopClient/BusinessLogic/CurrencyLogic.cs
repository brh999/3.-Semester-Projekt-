using DesktopClient.Security;
using DesktopClient.Service;
using Models;

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
        public async Task<bool> CreateCurrency(string name, double value)
        {
            Currency currency = new Currency
            {
                Type = name,
                Exchange = new Exchange
                {
                    Value = value,
                    Date = DateTime.UtcNow,
                }
            };
            bool success = await _currencyService.SaveCurrency(currency); 
            return success;
        }
        /// <summary>
        /// Used to fetch a list of all currencies from the database
        /// </summary>
        /// <returns>all currencies</returns>
        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            List<Currency>? foundCurrencies = null;

            foundCurrencies = await _currencyService.GetCurrencies();

            if(foundCurrencies == null)
            {
                foundCurrencies = new List<Currency>();
            }
            return foundCurrencies;
        }
    }
}
