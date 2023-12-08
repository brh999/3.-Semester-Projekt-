using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using WebAppWithAuthentication.Models;
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
        private readonly IPostServiceAccess _postServiceAccess;
        public PostController(IConfiguration configuration, IPostServiceAccess postServiceAccess)
        {
            _configuration = configuration;
            _postServiceAccess = postServiceAccess;

            //Configure the base API url
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {
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
            List<Post> bidRes = new();
            List<Post> offerRes = new();

            try
            {
                bidRes = (List<Post>)await _postServiceAccess.GetAllBids();
                ViewData["bids"] = bidRes;

                offerRes = (List<Post>)await _postServiceAccess.GetAllOffers();
                ViewData["offers"] = offerRes;

                //ModelState.AddModelError(string.Empty, "No posts found");

                if (TempData["message"] != null)
                {
                    ViewData["message"] = TempData["message"];
                }
                return View();
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

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AccountDto? account = null;

            AccountService accountService = new(_connection);
            Task<AccountDto?> response = accountService.GetAccountById(userId);
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
        public IActionResult CreateOffer(Post inPost)
        {
            System.Security.Claims.ClaimsPrincipal loggedInUser = User;
            ActionResult result = StatusCode(500);
            string aspUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the asp user id from the currently logged in user

            // Api Call
            bool response = _postServiceAccess.CreateOffer(inPost, aspUserId);

            if (response)
            {
                ViewData["type"] = "offer";
                result = View("PostState", inPost);
            }

            return result;
        }

        [Authorize]
        public IActionResult BuyOffer(double offerAmount, double offerPrice, string offerCurrency, int offerID)
        {
            ViewData["offerAmount"] = offerAmount;
            ViewData["offerPrice"] = offerPrice;
            ViewData["offerCurrency"] = offerCurrency;
            ViewData["offerID"] = offerID;
            return View();
        }

        [Authorize]
        public IActionResult ConfirmBuyOffer(double offerAmount, double offerPrice, string offerCurrency, int offerID)
        {
            //TODO Temporary solution with new Currency & Post object. Need to refactor to get the actual objects through view to here..

            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _connection.UseUrl = _connection.BaseUrl + "offer/" + "buyoffer/" + userID;
            Currency inCurrency = new Currency(offerCurrency);
            Post inPost = new Post(offerAmount, offerPrice, inCurrency, offerID, "offer");
            var json = JsonConvert.SerializeObject(inPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var serviceResponse = _connection.CallServicePost(content);
            serviceResponse.Wait();
            if (serviceResponse.Result.IsSuccessStatusCode)
            {
                var apiResponse = serviceResponse.Result.Content.ReadAsAsync<dynamic>().Result;
                bool isComplete = (bool)apiResponse.successful;
                if (isComplete)
                {
                    TempData["message"] = 1;
                }
                else
                {
                    TempData["message"] = 2;
                }

            }
            return RedirectToAction("GetAllPosts");
        }

        [Authorize]
        public IActionResult EditOffer(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditOffer([FromBody] Post inBid)
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
        public IActionResult CreateBid([FromBody] Post inPost)
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
        public IActionResult EditBid([FromBody] Post inBid)
        {
            return Ok();
        }


        public IActionResult DeleteBid(int id)
        {
            return Ok();
        }

    }
}

