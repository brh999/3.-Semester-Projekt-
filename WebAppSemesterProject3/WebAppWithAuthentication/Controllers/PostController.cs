using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelViews;
using System.Security.Claims;
using WebAppWithAuthentication.ModelViews;
using WebAppWithAuthentication.Service;

namespace WebAppWithAuthentication.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // It's important to note, that 'Post' in this controller
        // - does not refer HTML post action, but Post of bid & offers in our system.

        private readonly IPostServiceAccess _postServiceAccess;

        public PostController(IPostServiceAccess postServiceAccess)
        {
            _postServiceAccess = postServiceAccess;
        }

        /// <summary>
        /// Get homepage view
        /// </summary>
        /// <returns></returns>
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

        public IActionResult CreateOffer()
        {
            ActionResult result = null;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AccountViewModel? account = null;

            AccountService accountService = new();
            Task<Account?> response = accountService.GetAccountById(userId);
            response.Wait();

            account = new AccountViewModel(response.Result);


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


        public IActionResult BuyOffer(double offerAmount, double offerPrice, string offerCurrency, int offerID)
        {
            ViewData["offerAmount"] = offerAmount;
            ViewData["offerPrice"] = offerPrice;
            ViewData["offerCurrency"] = offerCurrency;
            ViewData["offerID"] = offerID;
            return View();
        }


        public IActionResult ConfirmBuyOffer(double offerAmount, double offerPrice, string offerCurrency, int offerID)
        {
            //TODO Temporary solution with new Currency & Post object. Need to refactor to get the actual objects through view to here..
            bool res = false;

            var serviceResult = _postServiceAccess.ConfirmBuyOffer(User.FindFirstValue(ClaimTypes.NameIdentifier), offerAmount, offerPrice, offerCurrency, offerID);

            serviceResult.Wait();

            res = serviceResult.Result;

            if (res)
            {
                TempData["message"] = 1;
            }
            else
            {
                TempData["message"] = 2;
            }
            return RedirectToAction("GetAllPosts");

        }


        public IActionResult EditOffer(int id)
        {
            return View();
        }


        [HttpPut]
        public IActionResult EditOffer([FromBody] Post inBid)
        {
            return Ok();
        }

        public IActionResult DeleteOffer(int id)
        {
            return Ok();
        }



        public IActionResult CreateBid()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateBid([FromBody] Post inPost)
        {
            return Ok();
        }


        public IActionResult EditBid(int id)
        {
            return View();
        }


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

