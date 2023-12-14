using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using WebAppWithAuthentication.DTO;


using WebAppWithAuthentication.Service;


namespace WebAppWithAuthentication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //gets the currently logged in users AspNetUser.id (string)
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var accountDto = await _accountService.GetAccountById(userId);

                if (accountDto == null)
                {
                    return NotFound();
                }
                return View(accountDto);
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Account account)
        {
            return View(account);
        }

    }
}
