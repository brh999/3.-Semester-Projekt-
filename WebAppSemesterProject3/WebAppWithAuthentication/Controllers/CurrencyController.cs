using Microsoft.AspNetCore.Mvc;
using Models;
using WebAppWithAuthentication.Service;

namespace WebAppSemesterProject.Controllers
{
    public class CurrencyController : Controller


    {
        private Uri _url;
        IConfiguration _configuration;


        public CurrencyController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? url = _configuration.GetConnectionString("BaseUrl");
            if (url != null)
            {
                _url = new Uri(url);
            }
            else
            {
                throw new Exception("Could not find");
            }
        }

        //[Route,("{controller}/getallcurrencies")]
        public IActionResult Index()
        {
            List<Currency> currencies = null;

            ServiceConnection sc = new(_url.ToString());

            sc.UseUrl = sc.BaseUrl + "api/currency/";

            var responseTask = sc.CallServiceGet();
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Currency>>();
                readTask.Wait();

                currencies = readTask.Result;
            }
            else
            {
                // Handle the error if needed
                currencies = new List<Currency>();
            }

            //ViewData["currencies"] = currencies;
            return View(currencies);
        }

        // GET: CurrencyController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CurrencyController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrencyController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrencyController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
