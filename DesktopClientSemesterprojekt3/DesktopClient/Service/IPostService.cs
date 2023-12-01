using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    internal interface IPostService
    {
        Task<IEnumerable<Post>?>? GetPosts();
        Task<IEnumerable<TransactionLine>?>? GetRelatedTransactions(Post item);
    }
}
