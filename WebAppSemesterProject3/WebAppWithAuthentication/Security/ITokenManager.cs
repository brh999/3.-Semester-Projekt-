namespace WebAppWithAuthentication.Security
{
    public interface ITokenManager
    {
        Task<string?> GetToken();
    }
}
