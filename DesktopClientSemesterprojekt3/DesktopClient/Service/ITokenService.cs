using DesktopClient.Security;

namespace DesktopClient.Service
{
    public interface ITokenService
    {
        Task<string?> GetNewToken(ApiAccount accountToUse);
    }

}
