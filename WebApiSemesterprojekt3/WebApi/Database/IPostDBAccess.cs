using Models;

namespace WebApi.Database
{
    public interface IPostDBAccess
    {

        IEnumerable<Post> GetOfferPosts();
        IEnumerable<Post> GetBidPosts();
        IEnumerable<Post> GetAllPosts();
        IEnumerable<TransactionLine> GetTransactionLines(int id);

        void InsertOffer(Post offer, string aspNetUserId);

        void InsertBid(Post bid);

        bool DeleteOffer(int id);

        Account GetAssociatedAccount(int postId);

        bool BuyOffer(Post inPost);

    }
}
