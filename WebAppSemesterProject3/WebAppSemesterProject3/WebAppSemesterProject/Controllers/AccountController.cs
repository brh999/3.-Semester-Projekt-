using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppSemesterProject;
using Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAppSemesterProject.Controllers
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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userID = User.Identity.Name;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = _url;
                var responseTask = client.GetAsync("bid");
                responseTask.Wait();
                //IEnumerable<Account> accounts = await 
                    return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
        }

        public IActionResult Login()
        {
            return View();
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
