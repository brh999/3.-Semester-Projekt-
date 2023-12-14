using Microsoft.AspNetCore.Mvc;
using Models;
using WebAppWithAuthentication.Service;

namespace WebAppSemesterProject.Controllers
{
    public class CurrencyController : Controller


    {


        private ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        //[Route,("{controller}/getallcurrencies")]
        public async Task<IActionResult> Index()
        {
            ActionResult res = View();
            List<Currency> currencies = null;

            currencies = (List<Currency>)await _currencyService.GetAllCurrencies();

            if (currencies != null)
            {
                res = View(currencies);
            }
            return res;

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
