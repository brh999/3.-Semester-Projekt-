using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        List<Bid> GetAllBids();

        List<Offer> GetAllOffers();
        List<Post> GetAllPosts();

        Offer InsertOffer (Offer offer);

        Bid InsertBid (Bid bid);
    }
}
