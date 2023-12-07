using Models;

namespace WebApi.Database
{
    public interface IPostDBAccess
    {

        IEnumerable<Post> GetOfferPosts();
        IEnumerable<Post> GetBidPosts();
        IEnumerable<Post> GetAllPosts();
        IEnumerable<TransactionLine> GetTransactionLines(int id);


        bool InsertOffer(Post offer,string aspNetUserId);


        bool InsertBid(Post bid, string aspNetUserId);

        bool DeleteOffer(int id);


        Account GetAssociatedAccount(int postId);

        bool BuyOffer(Post inPost, string aspNetUserId);

        IEnumerable<Post?> GetOfferPostsById(string aspNetuser);


    }
}
