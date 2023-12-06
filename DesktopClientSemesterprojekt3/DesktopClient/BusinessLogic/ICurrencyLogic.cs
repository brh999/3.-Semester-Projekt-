using Models;

namespace DesktopClient.BusinessLogic
{
    internal interface ICurrencyLogic
    {
        Task<bool> CreateCurrency(string name, double value);

        Task<IEnumerable<Currency>> GetAllCurrencies();


    }



}
