using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.Database;

namespace WebApi.BuissnessLogiclayer
{
    public class PostLogic : IPostLogic
    {
        private readonly IPostDBAccess _dataAccess;
        private readonly IConfiguration _configuration;

        public PostLogic(IPostDBAccess inDataAccess, IConfiguration configuration)
        {
            _dataAccess = inDataAccess;
            _configuration = configuration;
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

        public Post InsertBid(Post bid, string aspNetUserId)
        {
            Post res = bid;
            try
            {
                _dataAccess.InsertBid(bid, aspNetUserId);
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public Post InsertOffer(Post offer, string aspNetUserId)
        {
            Post res = new();
            //Validate Post
            bool isValid = ValidatePost(offer);

            if (isValid)
            {
                bool succes = _dataAccess.InsertOffer(offer, aspNetUserId);

                if (succes)
                {
                    res = offer;
                }
            }
            else
            {
                throw new ArgumentException("Offer does not property constraints");
            }

            return res;
        }

        public List<TransactionLine> GetRelatedTransactions(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be less than or equal to 0");
            }
            IEnumerable<TransactionLine>? foundLines;

            TransactionDBAccess tba = new(_configuration);

            foundLines = tba.GetTransactionLines(id);

            return (List<TransactionLine>)foundLines;
        }


        public bool DeleteOffer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be less than or equal to 0");
            }

            bool res = false;
            res = _dataAccess.DeleteOffer(id);
            return res;
        }


        public bool BuyOffer(Post inPost, string aspNetUserId)
        {
            return _dataAccess.BuyOffer(inPost, aspNetUserId);

        }

        public ActionResult<List<Post>>? GetAllOffersById(string aspNetUser)
        {
            IEnumerable<Post?> foundPosts;

            foundPosts = _dataAccess.GetOfferPostsById(aspNetUser);

            return (List<Post>)foundPosts;


        }

        private bool ValidatePost(Post post)
        {
            bool isValid = true;

            if (post != null)
            {


                isValid = post.Amount >= 0;
                if (isValid)
                {
                    isValid = post.Price >= 0;
                }
                if (isValid)
                {
                    isValid = post.Id >= 0;
                }
                if (isValid)
                {
                    isValid = !post.IsComplete;
                }


                //This should also verify that the more complex properties valid.
                //But for know this is sufficient
            }

            return isValid;
        }


    }
}



