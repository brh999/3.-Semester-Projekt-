using Models;
using System.Text;

using Newtonsoft.Json;

using static System.Net.WebRequestMethods;
using System.Linq.Expressions;


namespace DesktopClient.Service
{

    public class PostService : IPostService
    {
        private string apiurl = "http://localhost:5042/api/bid/";
        private readonly IServiceConnection _connection;

        public PostService()
        {
            _connection = new ServiceConnection(apiurl);
        }

        public async Task<bool> SavePost(Post item)
        {
            bool savedOk = false;
            try
            {
                if (_connection != null)
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var serviceResponse = await _connection.CallServicePost(content);
                    if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {
                        savedOk = true;
                    }

                }
            }
            catch
            {
                savedOk = false;
            }
            return savedOk;
        }
    }

}



