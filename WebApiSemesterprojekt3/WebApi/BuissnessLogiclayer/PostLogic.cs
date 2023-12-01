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
        public List<Post> GetAllBids()
        {
            IEnumerable<Post> bids = new List<Post>();

            bids = _dataAccess.GetBidPosts();

            return (List<Post>)bids;
        }

        public List<Post> GetAllOffers()
        {
            IEnumerable<Post> offers = new List<Post>();

            offers = _dataAccess.GetOfferPosts();

            return (List<Post>)offers;
        }

        public Post InsertBid(Post bid)
        {
            Post res = bid;
            try
            {
                _dataAccess.InsertBid(bid);
            } catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public Post InsertOffer(Post offer,string aspNetUserId)
        {
            Post res = offer;
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
