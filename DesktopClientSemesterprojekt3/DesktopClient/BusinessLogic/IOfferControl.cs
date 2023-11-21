using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal interface IOfferControl
    {
        Task<Offer> CreateOffer( double amount, double price, Currency currency);
    }
}
