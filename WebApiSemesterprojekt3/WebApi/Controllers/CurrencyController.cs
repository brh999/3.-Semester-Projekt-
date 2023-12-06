using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        public ActionResult<Currency> GetCurrencyById(int id)
        {
            ActionResult<Currency> currency = null;
            Currency res = null;

            res = _currencyLogic.GetCurrencyById(id);

            if (res != null)
            {
                currency = Ok(res);
            }
            else
            {
                currency = NotFound();
            }

            return currency;

        }
        // Post api/<CurrencyController>
        [HttpPost]
        public IActionResult Post([FromBody] Currency currency)
        {
            IActionResult result = StatusCode(500);
            try
            {
                bool success = _currencyLogic.InsertCurrency(currency);
                if (success)
                {
                    result = Ok();
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }


        }

    }
}
