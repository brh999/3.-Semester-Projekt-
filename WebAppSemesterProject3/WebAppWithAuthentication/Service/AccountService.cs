using Models;

namespace WebAppWithAuthentication.Service
{

    public class AccountService : IAccountService
    {

        IServiceConnection _connection;
        public AccountService(IServiceConnection connection)
        {
            _connection = connection;
        }


        public async Task<Account?> GetAccountById(string aspNetId)
        {
            Account account = null;

            if (!string.IsNullOrEmpty(aspNetId))
            {
                _connection.UseUrl = _connection.BaseUrl + "account/" + aspNetId;
                var httpResponse = await _connection.CallServiceGet();
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    var content = await httpResponse.Content.ReadAsAsync<Account>();

                    if (content != null)
                    {
                        account = content;
                    }

                }
            }
            return account;
        }



    }
}
