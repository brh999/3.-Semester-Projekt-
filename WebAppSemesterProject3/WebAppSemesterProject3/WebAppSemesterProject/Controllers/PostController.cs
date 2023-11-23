using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace WebAppSemesterProject.Controllers
{
    public class PostController : Controller
    {
        // It's important to note, that 'Post' in this controller
        // - does not refer HTML post action, but Post of bid & offers in our system.

        private Uri _url;
        private readonly IConfiguration _configuration;
        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? url = _configuration.GetConnectionString("DefaultAPI");
            if(url != null)
            {
                _url = new Uri(url);

            }
            else
            {
                throw new Exception("Could not find");
            }
        }

        [Authorize]
        
        [Route("{controller}/getallposts")]
        public IActionResult GetAllPosts()
        {
            
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            
            IEnumerable <Post> bids = null;
            IEnumerable<Post> offers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;

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
            AccountDto? account = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = _url;

            //    //This should find put which account that makes the request.
            //    var task = client.GetAsync("Account");

            //    task.Wait();

            //    var result = task.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<AccountDto>();
            //        readTask.Wait();
            //        account = readTask.Result;

            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Server error - No offers found");
            //    }



            //}
            var testData = new Account(100, "Test", "Test@");
            testData.AddCurrencyLine(new CurrencyLine(10, new Currency(new List<Exchange>(), "Dollar")));
            testData.AddCurrencyLine(new CurrencyLine(10, new Currency(new List<Exchange>(), "Euro")));
            account = new AccountDto(testData);
           
            
           
            ViewData.Add("account", account);
            return View();
        }


        
        [HttpPost]
        public IActionResult CreateOffer(Offer inPost)
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

        public IActionResult DeleteBid(int id)
        {
            return Ok();
        }
    }
}
