using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppSemesterProject.Controllers
{
    public class CurrencyController : Controller
    {
        [Route("{controller}/getallcurrencies")]
        public IActionResult GetAlleCurrencies()
        {
            IEnumerable<Currency> currencies = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5042/api/");
                var responseTask = client.GetAsync("Currency");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Currency>>();
                    readTask.Wait();

                    currencies = readTask.Result;
                } else
                {
                    currencies = Enumerable.Empty<Currency>();
                    ModelState.AddModelError(string.Empty, "No currencies to be found");
                }
            }
            ViewData["currencies"] = currencies;
            return View();

        }
    }
}

