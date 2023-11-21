using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppWithAuthentication.Controllers
{
    public class PostController : Controller
    {
        // It's important to note, that 'Post' in this controller
        // - does not refer HTML post action, but Post of bid & offers in our system.
        [Authorize]
        public IActionResult Index()
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
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
            ViewData["user"] = loggedInUser;
            return View();
        }

        [Authorize]
        public IActionResult CreateOffer()
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            return View();
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult CreateOffer([FromBody] Offer inPost)
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOffer([FromBody] Bid inPost)
        {
            return Ok();
        }

        [Authorize]
        public IActionResult EditOffer(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditOffer([FromBody] Bid inBid)
        {
            return Ok();
        }

        public IActionResult DeleteOffer(int id)
        {
            return Ok();
        }


        [Authorize]
        public IActionResult CreateBid()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateBid([FromBody] Bid inPost)
        {
            return Ok();
        }

        [Authorize]
        public IActionResult EditBid(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditBid([FromBody] Bid inBid)
        {
            return Ok();
        }

        public IActionResult DeleteBid(int id) {
            return Ok();
        }
    }
}
