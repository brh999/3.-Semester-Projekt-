using Models.DTO;

namespace WebAppWithAuthentication.Service
{
    public interface IAccountService
    {
        Task<AccountDto?> GetAccountById(string aspNetId);
    }
}
