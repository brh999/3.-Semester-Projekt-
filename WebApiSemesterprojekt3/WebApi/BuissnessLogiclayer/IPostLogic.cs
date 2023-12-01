using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        List<Post> GetAllBids();

        List<Post> GetAllOffers();
        List<Post> GetAllPosts();

        Post InsertOffer (Post offer,string aspNetUserId);

        Post InsertBid (Post bid);
        List<TransactionLine> GetRelatedTransactions(int id);
    }
}
