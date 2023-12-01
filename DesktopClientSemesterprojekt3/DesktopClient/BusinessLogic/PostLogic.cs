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
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            IEnumerable<Post>? foundPosts;
            foundPosts = await _postService.GetPosts();
            if (foundPosts == null){
                foundPosts = new List<Post>();
            }



            return foundPosts;
        }

        public async Task<IEnumerable<TransactionLine>> GetRelatedTransactions(Post item)
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
