using Models;

namespace DesktopClient.Service.DeprecatedService
{
    [Obsolete]
    public interface IPostService
    {
        Task<bool> SaveBid(Bid item);
        Task<List<Bid>> GetAllBids(string tokenToUse);
        Task<bool> SaveOffer(Offer item);

        Task<List<Offer>> GetAllOffers();
    }
}
