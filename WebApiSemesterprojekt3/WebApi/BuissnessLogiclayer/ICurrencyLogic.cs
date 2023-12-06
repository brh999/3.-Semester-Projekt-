using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface ICurrencyLogic
    {

        int GetCurrencyId(Currency item);

        List<Currency> GetCurrencyList();

        Currency GetCurrencyById(int id); 

        bool InsertCurrency(Currency currency);
    }
}
