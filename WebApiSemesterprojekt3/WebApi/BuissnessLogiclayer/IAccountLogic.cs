using Models;
using WebApi.Database;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountControl
    {
        List<Account> GetAllAccounts();
    }
}