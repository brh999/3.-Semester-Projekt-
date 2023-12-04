using Models;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Post> GetAllPosts()
        {
            IEnumerable<Post> res = new List<Post>();
            res = _dataAccess.GetAllPosts();

            return res;
        }
        public IEnumerable<Post> GetAllBids()
        {
            IEnumerable<Post> bids = new List<Post>();

            bids = _dataAccess.GetBidPosts();

            return bids;
        }

        public IEnumerable<Post> GetAllOffers()
        {
            IEnumerable<Post> offers = new List<Post>();

            offers = _dataAccess.GetOfferPosts();

            return offers;
        }

        public Post InsertBid(Post bid)
        {
            Post res = bid;
            try
            {
                _dataAccess.InsertBid(bid);
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public Post InsertOffer(Post offer, string aspNetUserId)
        {
            Post res = null;
            try
            {
                bool succes = _dataAccess.InsertOffer(offer, aspNetUserId);

                if(succes)
                {
                    res = offer;
                }
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


        public bool DeleteOffer(int id)
        {
            bool res = false;
            res = _dataAccess.DeleteOffer(id);
            return res;
        }

        public ActionResult<List<Post>>? GetAllOffersById(string aspNetUser)
        {
            IEnumerable<Post?> foundPosts;

            foundPosts = _dataAccess.GetOfferPostsById(aspNetUser);

            return (List<Post>)foundPosts;

        }
    }
}


