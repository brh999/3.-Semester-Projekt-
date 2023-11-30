using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        List<Bid> GetAllBids();

        List<Offer> GetAllOffers();
        List<Post> GetAllPosts();

        Offer InsertOffer (Offer offer,string aspNetUserId);

        Bid InsertBid (Bid bid);
        List<TransactionLine> GetRelatedTransactions(int id);
    }
}
