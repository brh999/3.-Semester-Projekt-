using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountLogic
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(int id);
    }
}
