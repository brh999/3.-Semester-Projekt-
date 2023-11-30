using Models;

namespace WebApi.Database
{
    public interface IPostDBAccess
    {
       
        IEnumerable<Offer> GetOfferPosts();
        IEnumerable<Bid> GetBidPosts();
        IEnumerable<Post> GetAllPosts();
        IEnumerable<TransactionLine> GetTransactionLines(int id);

        void InsertOffer(Offer offer,string aspNetUserId);

        void InsertBid(Bid bid);
    }
}
