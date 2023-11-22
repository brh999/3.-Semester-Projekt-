using DesktopClient.Security.ApiAuthentication;

namespace DesktopClient.Service
{
    public interface ITokenService
    {
        Task<string?> GetNewToken(ApiAccount accountToUse);
    }

}
