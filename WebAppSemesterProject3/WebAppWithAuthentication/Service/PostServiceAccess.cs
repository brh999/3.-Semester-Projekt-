using Models;

namespace WebAppWithAuthentication.Service
{
    public class PostServiceAccess : IPostServiceAccess
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceConnection _connection;

        public PostServiceAccess(IConfiguration configuration)
        {
            _configuration = configuration;

            //Configure the base API url
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {
                _connection = new ServiceConnection(url + "Api/");
            }
            else
            {
                throw new Exception("Could not find");
            }

        }

        public async Task<IEnumerable<Post>> GetAllOffers()
        {
            List<Post> res = new();
            _connection.UseUrl = _connection.BaseUrl + "offer/";

            var response = _connection.CallServiceGet();
            response.Wait();
            var result = response.Result;

            if (result != null)
            {
                if (result.IsSuccessStatusCode)
                {
                    res = (List<Post>)await result.Content.ReadAsAsync<IList<Post>>();
                }
            }
            else
            {
                res = (List<Post>)Enumerable.Empty<Post>();
            }
            return res;
        }

        public async Task<IEnumerable<Post>> GetAllBids()
        {
            List<Post> res = new();
            _connection.UseUrl = _connection.BaseUrl + "bid/";

            var response = _connection.CallServiceGet();
            response.Wait();
            var result = response.Result;

            if (result != null)
            {
                if (result.IsSuccessStatusCode)
                {
                    res = (List<Post>)await result.Content.ReadAsAsync<IList<Post>>();
                }
            }
            else
            {
                res = (List<Post>)Enumerable.Empty<Post>();
            }
            return res;
        }
    }
}
