using Models;
using System.Security.Cryptography;
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
        public List<Post> GetAllPosts()
        {
            IEnumerable<Post>? res;
            res = _dataAccess.GetAllPosts();

            return (List<Post>)res;
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
            Bid res = bid;
            try
            {
                _dataAccess.InsertBid(bid);
            } catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public Offer InsertOffer(Offer offer,string aspNetUserId)
        {
            Offer res = offer;
            try
            {
                _dataAccess.InsertOffer(offer, aspNetUserId);
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public List<TransactionLine> GetRelatedTransactions(int id)
        {
            
            IEnumerable<TransactionLine>? foundLines;

            foundLines = _dataAccess.GetTransactionLines(id);

            return (List<TransactionLine>)foundLines;
        }
    }
}
