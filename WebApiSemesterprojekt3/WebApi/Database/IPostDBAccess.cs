using Models;

namespace WebApi.Database
{
    public interface IPostDBAccess
    {
       
        IEnumerable<Offer> GetOfferPosts();
        IEnumerable<Bid> GetBidPosts();
        IEnumerable<Post> GetAllPosts();

        void InsertOffer(Offer offer);

        void InsertBid(Bid bid);
    }
}
