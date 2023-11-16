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
            IEnumerable<Post> bids = null;
            IEnumerable<Post> offers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5042/api/");
                // Get bids:
                var responseTask = client.GetAsync("bid");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Bid>>();
                    readTask.Wait();

                    bids = readTask.Result;
                }
                else
                {
                    bids = Enumerable.Empty<Post>();
                    ModelState.AddModelError(string.Empty, "Server error - No bids found");
                }
                // Get offers:
                responseTask = client.GetAsync("offer");
                responseTask.Wait();

                result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Offer>>();
                    readTask.Wait();

                    offers = readTask.Result;
                }
                else
                {
                    offers = Enumerable.Empty<Offer>();
                    ModelState.AddModelError(string.Empty, "Server error - No offers found");
                }

            }
            ViewData["bids"] = bids;
            ViewData["offers"] = offers;
            return View();
        }
    }
}
