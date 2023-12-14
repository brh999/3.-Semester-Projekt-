using Models;

namespace WebAppWithAuthentication.Service
{
    public interface IPostServiceAccess
    {
        Task<IEnumerable<Post>> GetAllOffers();

        Task<IEnumerable<Post>> GetAllBids();

        public bool CreateOffer(Post inPost, string aspNetId);

        public Task<bool> ConfirmBuyOffer(string AspUserId, double offerAmount, double offerPrice, string offerCurrency, int offerID);
    }
}
