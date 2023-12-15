using Models;
using System.Data.SqlClient;

namespace WebApi.Database
{
    public interface IAccountDBAccess
    {
        List<Account> GetAllAccounts();

        Account GetAccountById(string id);

        bool DeleteAccountById(int id);

        bool UpdateAccountById(int id);

        IEnumerable<CurrencyLine> GetCurrencyLines(int id);

        bool CheckCurrencyLine(string aspDotNetId, CurrencyLine currencyLine, SqlConnection conn, SqlTransaction tran);
        bool UpdateCurrencyLine(string aspDotNetId, CurrencyLine currencyLine, SqlConnection conn, SqlTransaction tran);
        bool InsertCurrencyLine(string aspDotNetId, CurrencyLine currencyLine, SqlConnection conn, SqlTransaction tran);
    }
}
