using WebApi.Model;

namespace WebApi.BuissnessLogiclayer
{
    public interface IBidLogic
    {
        public IEnumerable<Bid> GetAllBids();

        public Bid GetAccountSpecificBids(int AccountFk);


    }
}
