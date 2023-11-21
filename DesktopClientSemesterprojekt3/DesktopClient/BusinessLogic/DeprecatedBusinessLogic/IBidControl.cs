using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DesktopClient.BusinessLogic.DeprecatedBusinessLogic
{
    public interface IBidControl
    {

        Task<Bid?> CreateBid(double amount, double price, Currency currency);

    }
}
