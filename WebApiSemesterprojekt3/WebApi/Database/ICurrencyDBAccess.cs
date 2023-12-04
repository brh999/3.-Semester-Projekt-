using Models;

namespace WebApi.Database
{
    public interface ICurrencyDBAccess
    {
        int GetCurrencyID(Currency item);

        Currency? GetCurrencyById(int currencyID);

        IEnumerable<Currency> GetCurrencyList();
    }
}
