using Models;
using WebApi.Database;

namespace WebApi.BuissnessLogiclayer
{
    public class CurrencyLogic : ICurrencyLogic

    {
        private readonly ICurrencyDBAccess _dataAccess;

        public CurrencyLogic(ICurrencyDBAccess inDataAccess)
        {
            _dataAccess = inDataAccess;
        }
        public int GetCurrencyId(Currency item)
        {
            int res;
            res = _dataAccess.GetCurrencyID(item);

            return res;
        }

        public List<Currency> GetCurrencyList()
        {
            return (List<Currency>)_dataAccess.GetCurrencyList();
        }
    }
}
