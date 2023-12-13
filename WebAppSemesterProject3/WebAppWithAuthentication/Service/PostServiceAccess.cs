using Models;
using Newtonsoft.Json;
using System.Text;

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
                _connection = new ServiceConnection(url + "Api/");
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

        public bool CreateOffer(Post inPost, string aspNetId)
        {
            bool isSuccessful = true;

            // Quick fix, at this point offer doesn't need to have an exchange or any transaction.
            // But the API doesn't comprehend that these values could be null
            // therefore we create 'Empty' instances of objects.
            // TODO refractor this in a later sprint.
            if (inPost.Currency.Exchange == null)
            {
                inPost.Currency.Exchange = new Exchange();
            }
            if (inPost.Transactions == null)
            {
                inPost.Transactions = new List<TransactionLine>();
            }
            inPost.Type = "offer";


            //Validate input
            //TODO: create error handling if the input is not valid.
            //Either at the browser level or Control
            bool goOn = true;
            if ((inPost.Amount <= 0) || (inPost.Price <= 0))
            {
                goOn = false;
            }

            if (String.IsNullOrEmpty(inPost.Currency.Type))
            {
                goOn = false;
            }

            if (goOn)
            {
                //Create the use url to this call.
                _connection.UseUrl = _connection.BaseUrl + "offer/" + aspNetId;

                //Serialize the offer object
                var json = JsonConvert.SerializeObject(inPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var serviceResponse = _connection.CallServicePost(content);
                serviceResponse.Wait();
                var apiRes = serviceResponse.Result;

                //Check response from API
                if (apiRes is not null && apiRes.IsSuccessStatusCode)
                {
                    isSuccessful = true;
                }
            }
            return isSuccessful;
        }
    }
}
