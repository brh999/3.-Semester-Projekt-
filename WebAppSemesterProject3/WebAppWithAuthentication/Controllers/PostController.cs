using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

using Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;
using System.Text;
using WebAppWithAuthentication.BusinessLogic;
using WebAppWithAuthentication.Models;
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

            //Configure the base API url
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {

                _connection = new ServiceConnection(url + "Api/");
                _connection = new ServiceConnection(url + "Api/");

            }
            else
            {
                throw new Exception("Could not find");
            }
        }


        /// <summary>
        /// Get homepage view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            IEnumerable<Post> bids = null;
            IEnumerable<Post> offers = null;
            try
            {
                // Get bids:
                _connection.UseUrl = _connection.BaseUrl + "bid";
                var response = _connection.CallServiceGet();
                response.Wait();
                var result = response.Result;
                if (result != null) {
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Bid>>();
                        readTask.Wait();

                        bids = readTask.Result;
                    }
                    else
                    {
                        bids = Enumerable.Empty<Post>();
                        ModelState.AddModelError(string.Empty, "No bids found");
                    }
                    // Get offers:
                    _connection.UseUrl = _connection.BaseUrl + "offer";
                    response = _connection.CallServiceGet();
                    response.Wait();

                    result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Offer>>();
                        readTask.Wait();

                        offers = readTask.Result;
                    }
                    else
                    {
                        offers = Enumerable.Empty<Offer>();
                        ModelState.AddModelError(string.Empty, "No offers found");
                    }
                    ViewData["bids"] = bids;
                    ViewData["offers"] = offers;
                    ViewData["user"] = loggedInUser;
                    return View();
                }
                return StatusCode(500);
            }
            catch
            {
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Create and return offer View
        /// </summary>
        /// <returns></returns>
        [Authorize]
        
        public IActionResult CreateOffer()
        {

            ActionResult result = null;

            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AccountDto? account = null;

            //This should find  which account that made the request and not simple account 'c811de3f-ab3c-4445-8d70-612e68d61c93'.
            AccountLogic accountLogic = new(_connection);
            Task<AccountDto?> response = accountLogic.GetAccountById(userId);
            response.Wait();

            account = response.Result;

          
            if (account != null)
            {
                ViewData.Add("account", account);
                result = View();

            }
            
            


            if (result == null)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                result = View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
            return result;
        }


        /// <summary>
        /// Create offer from parameter
        /// </summary>
        /// <param name="inPost"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult CreateOffer(Offer inPost)
        {
            //Quick fix, at this point offer dont need to have an exchange or any transaction.
            //But the API do not comprehend that these values could be null
            //therefore we create 'Empty' instances of objects.
            // TODO refractor this in a later sprint.
            if  (inPost.Currency.Exchanges == null)
            {
                inPost.Currency.Exchanges = new Exchange();
            }
            if  (inPost.Transactions == null)
            {
                inPost.Transactions = new List<TransactionLine>();
            }

            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            ActionResult result = StatusCode(500);

            //Validate input
            //TODO: create error handling if the input is not valid.
            //Either at the browser level or Control
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
                //Create the use url to this call.
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _connection.UseUrl = _connection.BaseUrl + "offer/" + userId;


                //Serialize the offer object
                var json = JsonConvert.SerializeObject(inPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var serviceResponse = _connection.CallServicePost(content);
                serviceResponse.Wait();

                //Check response from API
                if (serviceResponse == null || !serviceResponse.Result.IsSuccessStatusCode)
                {
                    //502 Bad Gateway
                    result = StatusCode(502);
                }


                ViewData["type"] = "offer";
                result = View("PostState",inPost);

            }
            else
            {
                result = StatusCode(404);
            }

            return result;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetAllOffersAsync()
        {
            List<Offer> foundOffers = new();

            _connection.UseUrl = _connection.BaseUrl + "offer/";

            try
            {
                var response = await _connection.CallServiceGet();
                response?.EnsureSuccessStatusCode();

                if (response is not null)
                {
                    var readResponse = await response.Content.ReadAsAsync<List<Offer>>();
                }
                else
                {
                    return StatusCode(500); //internal server error
                }
            }
            catch
            {
                return StatusCode(500);
            }


            return Ok(foundOffers);
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

