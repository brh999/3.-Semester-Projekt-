namespace WebAppWithAuthentication.Security
{
    internal static class JWT
    {
        public static string? CurrentJWT { get; set; }
        public static TokenState TokenState { get; set; }
    }
}
