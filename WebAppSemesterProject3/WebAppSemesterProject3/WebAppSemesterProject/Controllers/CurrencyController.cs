using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Policy;

namespace WebAppSemesterProject.Controllers
{
    public class CurrencyController : Controller
    {
        private Uri _url;
        private readonly IConfiguration _configuration;
        public CurrencyController(IConfiguration configuration)
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


        [Route("{controller}/getallcurrencies")]
        public IActionResult GetAlleCurrencies()
        {
            IEnumerable<Currency> currencies = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
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

