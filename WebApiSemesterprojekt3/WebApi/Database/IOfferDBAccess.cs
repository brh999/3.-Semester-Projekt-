using Models;

namespace WebApi.Database
{
    public interface IOfferDBAccess
    {
        List<Offer>GetAllOffers();
    }
}
