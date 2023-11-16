using Microsoft.AspNetCore.Mvc;
using WebAppSemesterProject3.DTO;
using Models;

namespace WebAppSemesterProject3.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Currency> currencies = new List<Currency>() { new Currency(CurrencyEnum.BTC, new List<Exchange>()) };
            using (Deserializer<Currency> ad = new())
            {
                try
                {
                   // IEnumerable<Currency> Currencies = await ad.GetList();
                    return View(currencies);
                }
                catch (Exception ex)
                {
                    return View(currencies);
                }
            }
        }
    }
}
