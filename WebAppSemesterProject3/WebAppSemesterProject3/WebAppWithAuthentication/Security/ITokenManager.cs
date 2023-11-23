using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonServiceClientDesktop.Security
{
    public interface ITokenManager
    {
        Task<string?> GetToken(TokenState currentState);
    }
}
