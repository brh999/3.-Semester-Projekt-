using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        IEnumerable<Bid> GetAllBids();
    }
}
