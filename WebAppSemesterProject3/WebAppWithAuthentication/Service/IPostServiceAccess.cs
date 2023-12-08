using Models;

namespace WebAppWithAuthentication.Service
{
    public interface IPostServiceAccess
    {
        public Task<IEnumerable<Post>> GetAllOffers();

        public Task<IEnumerable<Post>> GetAllBids();
    }
}
