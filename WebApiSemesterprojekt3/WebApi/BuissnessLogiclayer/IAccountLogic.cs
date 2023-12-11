using Models;
using Models.DTO;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountLogic
    {
        List<AccountDto> GetAllAccounts();
        AccountDto GetAccountById(string id);
        List<CurrencyLine> GetRelatedCurrencyLines(int id);

        bool InsertCurrencyLine(string aspDotNetId,CurrencyLine currencyLine);
    }
}
