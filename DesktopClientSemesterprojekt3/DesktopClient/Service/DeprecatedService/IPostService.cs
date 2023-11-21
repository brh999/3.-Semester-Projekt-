using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service.DeprecatedService
{
    public interface IPostService
    {
        Task<bool> SaveBid(Bid item);
        Task<List<Bid>> GetAllBids();
        Task<bool> SaveOffer(Offer item);

        Task<List<Offer>> GetAllOffers();
    }
}
