using Models;

namespace WebAppWithAuthentication.Service
{
    public interface ICurrencyService
    {

        Task<IEnumerable<Currency>> GetAllCurrencies();
    }
}
