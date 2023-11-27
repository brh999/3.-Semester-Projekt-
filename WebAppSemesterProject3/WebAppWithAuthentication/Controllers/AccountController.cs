using Microsoft.AspNetCore.Mvc;
using Models;

using WebAppWithAuthentication.DTO;

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
                _connection = new ServiceConnection(url+"/api");

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
