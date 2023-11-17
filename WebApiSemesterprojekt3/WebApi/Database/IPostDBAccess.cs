using Models;

namespace WebApi.Database
{
    public interface IPostDBAccess
    {
       
        IEnumerable<Offer> GetOfferPosts();
        IEnumerable<Bid> GetBidPosts();

        Offer InsertOffer(Offer offer);

        void InsertBid(Bid bid);
    }
}
