using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountControl
    {
        List<Account> GetAllAccounts();
    }
}