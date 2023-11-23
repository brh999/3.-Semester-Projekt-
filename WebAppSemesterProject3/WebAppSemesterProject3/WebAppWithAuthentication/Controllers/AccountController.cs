using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppWithAuthentication.DTO;
using Models;

namespace WebAppWithAuthentication.Controllers
{
    public class AccountController : Controller
    {

        private Uri _url;
        IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? url = _configuration.GetConnectionString("DefaultAPI");
            if (url != null)
            {
                _url = new Uri(url);

            }
            else
            {
                throw new Exception("Could not find");
            }
        }
        public async Task<IActionResult> Index()
        {
        
            using (Deserializer<Account> ad = new())
            {
                try
                {
                    //IEnumerable<Account> accounts = await ad.GetList();
                    return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
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
