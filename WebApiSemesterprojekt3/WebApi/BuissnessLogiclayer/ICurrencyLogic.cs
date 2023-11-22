using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface ICurrencyLogic
    {

        int GetCurrencyId(Currency item);

        List<Currency> GetCurrencyList();
    }
}
