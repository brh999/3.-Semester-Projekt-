using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    public class PostService : IPostService
    {
        readonly IServiceConnection _postService;
        readonly string? _serviceUseUrl;
        readonly string? _serviceBaseUrl;
        private readonly NameValueCollection _appConfig;

        public PostService()
        {
            _appConfig = ConfigurationManager.AppSettings;
            _serviceBaseUrl = _appConfig.Get("BaseUrl");
            if (!string.IsNullOrEmpty(_serviceBaseUrl))
            {
                _serviceUseUrl = _serviceBaseUrl + "api/";
            }
            _postService = new ServiceConnection(_serviceUseUrl);
        }
        public async Task<IEnumerable<Offer>?>? GetPosts()
        {
            List<Offer>? res = null;
            _postService.UseUrl = _postService.BaseUrl + "offer/" ;

            var serviceResponse = await _postService.CallServiceGet();

            if(serviceResponse != null && serviceResponse.IsSuccessStatusCode)
            {
                var responsePosts = await serviceResponse.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<Offer>>(responsePosts);
            }
            return res;
        }

        public async Task<IEnumerable<TransactionLine>?>? GetRelatedTransactions(Offer item)
        {
            List<TransactionLine>? res = null;
            string id = item.Id.ToString();
            _postService.UseUrl = _postService.BaseUrl + $"offer/{id}";

            var serviceResponse = await _postService.CallServiceGet();

            if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
            {
                var responseTransactions = await serviceResponse.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<TransactionLine>>(responseTransactions);
            }
            return res;
        }
    }
}
