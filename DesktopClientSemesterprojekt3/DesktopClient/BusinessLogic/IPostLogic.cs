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
        Task<IEnumerable<Offer>> GetAllPosts();
        Task<IEnumerable<TransactionLine>> GetRelatedTransactions(Offer item);
    }
}
