using Microsoft.AspNetCore.Mvc;
using WebAppWithAuthentication.DTO;
using Models;

namespace WebAppWithAuthentication.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Currency> currencies = new List<Currency>() { new Currency() };
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
