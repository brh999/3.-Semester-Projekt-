using Microsoft.AspNetCore.Mvc;
using WebAppSemesterProject3.DTO;
using WebAppSemesterProject3.Models;

namespace WebAppSemesterProject3.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (Deserializer<Currency> ad = new())
            {
                try
                {
                    IEnumerable<Currency> Currencies = await ad.GetList();
                    return View(Currencies);
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
        }
    }
}
