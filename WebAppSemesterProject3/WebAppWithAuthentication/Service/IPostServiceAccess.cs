using Models;

namespace WebAppWithAuthentication.Service
{
    public interface IPostServiceAccess
    {
        Task<IEnumerable<Post>> GetAllOffers();

        Task<IEnumerable<Post>> GetAllBids();

        bool CreateOffer(Post inPost, string aspNetId);
    }
}
