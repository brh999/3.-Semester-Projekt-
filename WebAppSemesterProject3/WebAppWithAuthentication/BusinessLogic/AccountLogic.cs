using Models;
using Models.DTO;
using WebAppWithAuthentication.Service;

namespace WebAppWithAuthentication.BusinessLogic
{
    
    public class AccountLogic
    {

        IServiceConnection _connection;
        public AccountLogic(IServiceConnection connection) {
            _connection = connection;
        }


        public async Task<AccountDto?> GetAccountById(string aspNetId)
        {
            AccountDto account = null;

            if (!string.IsNullOrEmpty(aspNetId))
            {
                _connection.UseUrl = _connection.BaseUrl + "account/" + aspNetId;
                var httpResponse = await _connection.CallServiceGet();
                if (httpResponse != null && httpResponse.IsSuccessStatusCode) 
                {
                    var content = await httpResponse.Content.ReadAsAsync<AccountDto>();
                    
                    if(content != null)
                    {
                        account = content;
                    }

                }
            }
            return account;
        }



    }
}
