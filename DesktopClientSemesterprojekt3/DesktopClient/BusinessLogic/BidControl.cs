using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    public class BidControl : IBidControl
    {
        public Bid? CreateBid(int amount, double price, Currency currency)
        {

            Bid? newBid = null;
            if (amount > 0 && price > 0)
            {
                newBid = new Bid(amount, price, currency);
            }
            else
            {
                throw new ArgumentException();
            }
            return newBid;
        }
    }
}
