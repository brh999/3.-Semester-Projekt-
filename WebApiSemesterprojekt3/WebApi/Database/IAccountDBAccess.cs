using Models;

namespace WebApi.Database
{
    public interface IAccountDBAccess
    {
        List<Account> GetAllAccounts();

        Account GetAccountById(string id);

        bool DeleteAccountById(int id);

        bool UpdateAccountById(int id);

        IEnumerable<CurrencyLine> GetCurrencyLines(int id);

        bool CheckCurrencyLine(string aspDotNetId, CurrencyLine currencyLine);
        bool UpdateCurrencyLine(string aspDotNetId, CurrencyLine currencyLine);
        bool InsertCurrencyLine(string aspDotNetId, CurrencyLine currencyLine);
    }
}
