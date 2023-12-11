using Models;

namespace WebAppWithAuthentication.Service
{
    public interface IAccountService
    {
        Task<Account?> GetAccountById(string aspNetId);
    }
}
