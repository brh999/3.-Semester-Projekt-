using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DesktopClient.BusinessLogic
{
    public interface IBidControl
    {
        Bid? CreateBid(int amount, double price, Currency currency);
    }
}
