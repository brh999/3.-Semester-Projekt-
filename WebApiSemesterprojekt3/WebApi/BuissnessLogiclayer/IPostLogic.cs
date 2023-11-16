using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        List<Bid> GetAllBids();

        List<Offer> GetAllOffers();
    }
}
