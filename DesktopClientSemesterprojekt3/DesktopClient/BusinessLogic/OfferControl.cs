using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal class OfferControl : IOfferControl
    {
        public Offer? CreateOffer(double amount, double price, Currency currency)
        {
            Offer? newOffer = null;

            if (amount > 0 && price > 0)
            {
                newOffer = new Offer(amount, price, currency);
            }
            else
            {
                throw new ArgumentException();
            }

            return newOffer;
        }
    }
}
