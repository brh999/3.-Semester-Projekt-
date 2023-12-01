using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal interface IPostLogic
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<IEnumerable<TransactionLine>> GetRelatedTransactions(Post item);
    }
}
