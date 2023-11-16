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

        public IEnumerable<Bid> GetAllBids()
        {
            throw new NotImplementedException();
        }


    }
}
