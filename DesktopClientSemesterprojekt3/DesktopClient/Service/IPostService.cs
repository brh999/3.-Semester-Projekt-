using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    public interface IPostService
    {
        Task<bool> SavePost(Post item);
    }
}
