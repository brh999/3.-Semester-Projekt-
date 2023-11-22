using Models;

namespace WebApi.Database
{
    public interface ICurrencyDBAccess
    {
        int GetCurrencyID(Currency item);

        IEnumerable<Currency> GetCurrencyList();
    }
}
