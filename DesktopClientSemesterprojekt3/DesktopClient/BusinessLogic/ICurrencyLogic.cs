using Models;

namespace DesktopClient.BusinessLogic
{
    internal interface ICurrencyLogic
    {
        bool CreateCurrency(string name);

        public Task<IEnumerable<Currency>> GetAllCurrencies();


    }



}
