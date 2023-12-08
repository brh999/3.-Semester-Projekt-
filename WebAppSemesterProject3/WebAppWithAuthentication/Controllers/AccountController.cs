using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using WebAppWithAuthentication.BusinessLogic;

using WebAppWithAuthentication.Service;


namespace WebAppWithAuthentication.Controllers
{
    public class AccountController : Controller
    {

        private ServiceConnection _connection;
        IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {
                _connection = new ServiceConnection(url + "api/");
            }
            else
            {
                throw new Exception("Could not find");
            }
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //gets the currently logged in users AspNetUser.id (string)
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AccountLogic al = new(_connection);

                var accountDto = await al.GetAccountById(userId);

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
