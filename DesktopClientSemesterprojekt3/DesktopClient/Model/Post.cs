using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Model
{
    abstract class Post
    {
        double amount { get; set; }
        double price { get; set; }
        bool isComplete { get; set; }
    }
}
