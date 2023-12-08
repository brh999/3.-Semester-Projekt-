using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountLogic
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(string id);
        List<CurrencyLine> GetRelatedCurrencyLines(int id);

        bool InsertCurrencyLine(string aspDotNetId,CurrencyLine currencyLine);
    }
}
