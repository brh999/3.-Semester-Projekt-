using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonServiceClientDesktop.Security
{
    internal static class JWT
    {
        public static string? CurrentJWT { get; set; }
        public static TokenState TokenState { get; set; }
    }
}
