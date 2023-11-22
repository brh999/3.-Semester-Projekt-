using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppSemesterProject;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAppSemesterProject.Controllers
{
    public class AccountController : Controller
    {

        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userID = User.Identity.Name;
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
