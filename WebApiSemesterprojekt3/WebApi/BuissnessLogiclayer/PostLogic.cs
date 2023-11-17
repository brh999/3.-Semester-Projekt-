using Models;
using WebApi.Database;

namespace WebApi.BuissnessLogiclayer
{
    public class PostLogic : IPostLogic
    {
        private readonly IPostDBAccess _dataAccess;
        public PostLogic(IPostDBAccess inDataAccess)
        {
            _dataAccess = inDataAccess;
        }

        public List<Bid> GetAllBids()
        {
            IEnumerable<Bid> bids = new List<Bid>();

            bids = _dataAccess.GetBidPosts();

            return (List<Bid>)bids;
        }

        public List<Offer> GetAllOffers()
        {
            IEnumerable<Offer> offers = new List<Offer>();

            offers = _dataAccess.GetOfferPosts();

            return (List<Offer>)offers;
        }

        public Bid InsertBid(Bid bid)
        {
            throw new NotImplementedException();
        }

        public Offer InsertOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
    }
}
