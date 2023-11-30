using DesktopClient.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    public class PostLogic : IPostLogic
    {

        private IPostService _postService;

        public PostLogic()
        {
            _postService = new PostService();
        }
        public async Task<IEnumerable<Offer>> GetAllPosts()
        {
            IEnumerable<Offer>? foundPosts;
            foundPosts = await _postService.GetPosts();
            if (foundPosts == null){
                foundPosts = new List<Offer>();
            }



            return foundPosts;
        }

        public async Task<IEnumerable<TransactionLine>> GetRelatedTransactions(Offer item)
        {
            IEnumerable<TransactionLine> foundTransactions;
            foundTransactions = await _postService.GetRelatedTransactions(item);
            if (foundTransactions == null)
            {
                foundTransactions = new List<TransactionLine>();
            }
            return foundTransactions;
        }
    }
}
