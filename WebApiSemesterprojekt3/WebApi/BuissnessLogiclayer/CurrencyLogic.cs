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

        public Currency GetCurrencyById(int id)
        {
            return _dataAccess.GetCurrencyById(id);
        }

        public bool InsertCurrency(Currency currency)
        {
            try 
            {
                bool success = _dataAccess.InsertCurrency(currency);
                return success;
            }
            catch (Exception ex) 
            {
                throw;

            }
        }
    }
}
