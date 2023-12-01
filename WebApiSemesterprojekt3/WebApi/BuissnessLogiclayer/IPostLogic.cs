using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        IEnumerable<Post> GetAllBids();

        IEnumerable<Post> GetAllOffers();
        IEnumerable<Post> GetAllPosts();

        Post InsertOffer(Post offer, string aspNetUserId);

        Post InsertBid(Post bid);
        List<TransactionLine> GetRelatedTransactions(int id);
        bool DeleteOffer(int id);
    }
}
