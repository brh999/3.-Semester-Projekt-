using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.BuissnessLogiclayer
{
    public interface IPostLogic
    {
        IEnumerable<Post> GetAllBids();

        IEnumerable<Post> GetAllOffers();
        IEnumerable<Post> GetAllPosts();

        Post InsertOffer(Post offer, string aspNetUserId);

        Post InsertBid(Post bid, string aspNetUserId);
        List<TransactionLine> GetRelatedTransactions(int id);

        bool DeleteOffer(int id);

        bool BuyOffer(Post inPost, string aspNetUserId, int delay = 0);

        ActionResult<List<Post>>? GetAllOffersById(string aspNetUser);


    }
}
