using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

using Models.DTO;
using Newtonsoft.Json;
using System.Text;

using WebAppWithAuthentication.Security;
using WebAppWithAuthentication.Service;

namespace WebAppWithAuthentication.Controllers
{
    public class PostController : Controller
    {
        // It's important to note, that 'Post' in this controller
        // - does not refer HTML post action, but Post of bid & offers in our system.

        private Uri _url;
        private readonly IConfiguration _configuration;
        private ServiceConnection _connection;
        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {

                _connection = new ServiceConnection(url+"/Api/");

            }
            else
            {
                throw new Exception("Could not find");
            }
        }

        [Authorize]

        public async Task<IActionResult> Index()

        {
            string? tokenValue = await GetToken();
            if (tokenValue != null)
            {
                System.Security.Claims.ClaimsPrincipal loggedInUser = User;
                IEnumerable<Post> bids = null;
                IEnumerable<Post> offers = null;
                using (var client = new HttpClient())
                {
                    string bearerTokenValue = "Bearer" + " " + tokenValue;
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);
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
            else
            {
                return StatusCode(500);
            }
        }




        [Authorize]
        public IActionResult CreateOffer()
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            AccountDto? account = null;
            ////This should find put which account that makes the request.
            //_connection.UseUrl = _connection.BaseUrl + "account/1";
            //var task = _connection.CallServiceGet();
            //try
            //{
            //    task.Wait();
            //}
            //catch
            //{

            //}


            //var result = task.Result;

            //if (result.IsSuccessStatusCode)
            //{
            //    var readTask = result.Content.ReadAsAsync<AccountDto>();
            //    readTask.Wait();
            //    account = readTask.Result;
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Server error - No offers found");
            //}




            // test account
            var testData = new Account(100, "Test", "Test@");
            testData.AddCurrencyLine(new CurrencyLine(10, new Currency(new Exchange(), "USD")));
            testData.AddCurrencyLine(new CurrencyLine(10, new Currency(new Exchange(), "EUR")));
            account = new AccountDto(testData);



            ViewData.Add("account", account);
            return View();
        }



        [Authorize]
        [HttpPost]
        public IActionResult CreateOffer(Offer inPost)
        {
            
            if(inPost.Currency.Exchanges == null)
            {
                inPost.Currency.Exchanges = new Exchange();
            }
            if(inPost.Transactions == null)
            {
                inPost.Transactions = new List<TransactionLine>();
            }

            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            ActionResult result = StatusCode(500);

            //Validate input
            bool goOn = true;
            if (!(inPost.Amount > 0) || !(inPost.Price > 0))
            {
                goOn = false;
            }

            if (String.IsNullOrEmpty(inPost.Currency.Type))
            {
                goOn = false;
            }

            if (goOn)
            {
                _connection.UseUrl = _connection.BaseUrl + "offer";
                
                var json = JsonConvert.SerializeObject(inPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var serviceResponse = _connection.CallServicePost(content);
                serviceResponse.Wait();

                if (serviceResponse == null || !serviceResponse.Result.IsSuccessStatusCode)
                {
                    result = StatusCode(502);
                }

                result = RedirectToAction("Index", "Home");
            }
            else
            {
                result = StatusCode(404);
            }

            return result;
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

        private async Task<string?> GetToken()
        {
            TokenManager tokenHelp = new TokenManager();
            string? foundToken = await tokenHelp.GetToken();
            return foundToken;
        }


    }
}

