using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppWithAuthentication2;
using Models;

namespace WebAppSemesterProject.Controllers
{
    public class AccountController : Controller
    {
        

        public async Task<IActionResult> Index()
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
