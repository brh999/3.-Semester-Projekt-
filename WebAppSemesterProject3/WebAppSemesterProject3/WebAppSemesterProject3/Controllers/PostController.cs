using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppSemesterProject3.Controllers
{
    public class PostController : Controller
    {
        // It's important to note, that 'Post' in this controller
        // - does not refer HTML post action, but Post of bid & offers in our system.
        public IActionResult Index()
        {
            Currency btc = new Currency(CurrencyEnum.BTC, new List<Exchange>());
            List<Post> posts = new List<Post>() { new Bid(1, 6.99, btc), 
            new Offer(2, 8, btc)};
            return View(posts);
        }
    }
}
