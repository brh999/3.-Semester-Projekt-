using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyLogic _currencyLogic;

        public CurrencyController(ICurrencyLogic inControl)
        {
            _currencyLogic = inControl;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Currency>> GetAll()
        {
            ActionResult<IEnumerable<Currency>> foundCurrencies = null;
            List<Currency> res = null;
            res = _currencyLogic.GetCurrencyList();

            if (res != null)
            {
                foundCurrencies = Ok(res);
            }
            else
            {
                foundCurrencies = NotFound();
            }
            return foundCurrencies;
        }

    }
}
