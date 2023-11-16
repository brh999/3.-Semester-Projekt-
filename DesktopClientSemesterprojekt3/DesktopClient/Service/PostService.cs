using Models;
using System.Text;

using Newtonsoft.Json;

using static System.Net.WebRequestMethods;
using System.Linq.Expressions;

namespace DesktopClient.Service
{

    public class PostService : IPostService
    {
        private string baseUrl = "http://localhost:5042/api/";
        private readonly IServiceConnection _connection;

        public PostService()
        {
            _connection = new ServiceConnection(baseUrl);
        }

        public async Task<bool> SavePost(Post item)
        {
            bool savedOk = false;
            try
            {
                if (_connection != null)
                {
                    _connection.UseUrl = _connection.BaseUrl + "bid/";
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

        public async Task<List<Bid>> GetAllBids()
        {
            List<Bid>? res = null;
            //Check if the connection is running - if it is, do stuff
            if (_connection != null)
            {
                // Attach the route to the url and call the get method in the API
                _connection.UseUrl = _connection.BaseUrl + "bid/";
                var serviceResponse = await _connection.CallServiceGet();
                // Check if the call is answered and it was successful
                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    // Check the response and deserialize it
                    var content = await serviceResponse.Content.ReadAsStringAsync();

                    res = JsonConvert.DeserializeObject<List<Bid>>(content);
                }

            }
            if (res == null)
            {
                res = new List<Bid>();
            }
            return res;
        }

        public async Task<bool> SaveOffer(Offer item)
        {
            bool savedOk = false;
            try
            {
                if (_connection != null)
                {
                    _connection.UseUrl = _connection.BaseUrl + "offer/";
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

        public async Task<List<Offer>> GetAllOffers()
        {
            List<Offer>? res = null;
            if (_connection != null)
            {
                _connection.UseUrl = _connection.BaseUrl + "offer/";
                var serviceResponse = await _connection.CallServiceGet();
                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    var content = await serviceResponse.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<List<Offer>>(content);
                }

            }
            if (res == null)
            {
                res = new List<Offer>();
            }
            return res;

        }
    }
}





