namespace WebAppWithAuthentication.Service
{
    public interface ITokenServiceAccess
    {
        Task<string?> GetNewToken(ApiAccount accountToUse);
    }
}

