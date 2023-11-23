using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppSemesterProject.Models;

namespace WebAppSemesterProject.Servicelayer
{
    public interface ITokenServiceAccess
    {
        Task<string?> GetNewToken(ApiAccount accountToUse);
    }
}
