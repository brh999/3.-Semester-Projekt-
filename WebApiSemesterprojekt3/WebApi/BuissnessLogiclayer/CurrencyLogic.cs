using Models;
using WebApi.Database;

namespace WebApi.BuissnessLogiclayer
{
    public class CurrencyLogic : ICurrencyLogic

    {
        private readonly ICurrencyDBAccess _dataAccess;

        public CurrencyLogic(ICurrencyDBAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public int GetCurrencyId(Currency item)
        {
            int res;
            res = _dataAccess.GetCurrencyID(item);

            return res;
        }
    }
}
