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

            foundCurrencies = await _currencyService.GetCurrencies();

            return foundCurrencies;
        }
    }
}
