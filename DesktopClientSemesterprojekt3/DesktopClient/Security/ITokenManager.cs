namespace DesktopClient.Security
{
    public interface ITokenManager
    {
        Task<string?> GetToken();
    }
}
