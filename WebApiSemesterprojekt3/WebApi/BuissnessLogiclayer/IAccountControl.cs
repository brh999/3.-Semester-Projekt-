using WebApi.Model;

namespace WebApi.BuissnessLogiclayer
{
    public interface IAccountControl
    {
        List<Account> GetAllAccounts();
    }
}