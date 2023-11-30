using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OfferController : ControllerBase
    {

        private readonly IPostLogic _offerLogic;

        public OfferController(IPostLogic inControl)
        {
            _offerLogic = inControl;
        }

        // GET: api/<OfferController>
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<TransactionLine>> GetRelatedTransactionLines(int id)
        {
            ActionResult<IEnumerable<TransactionLine>>? foundLines = null;
            List<TransactionLine> res = null;
            res = _offerLogic.GetRelatedTransactions(id);

            if (res != null)
            {
                foundLines = Ok(res);
            }
            else
            {
                foundLines = NotFound();
            }
            return foundLines;
        }

        // GET: api/<OfferController>
        [HttpGet]
        public ActionResult<IEnumerable<Offer>> GetAll()
        {
            ActionResult<IEnumerable<Offer>>? foundOffers = null;
            List<Offer> res = null;
            res = _offerLogic.GetAllOffers();

            if (res != null)
            {
                foundOffers = Ok(res);
            }
            else
            {
                foundOffers = NotFound();
            }
            return foundOffers;

        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost("{aspNetUserId}")]
        public IActionResult Post([FromBody] Offer inOffer, string aspNetUserId)
        {
            //TODO: Error handling
            IActionResult result = StatusCode(500);
            Offer? isOfferValid = null;
            try
            {
                isOfferValid = _offerLogic.InsertOffer(inOffer, aspNetUserId);
            }
            catch (Exception)
            {
                return result;
            }

            // Checks if offer is saved into the database
            if (isOfferValid != null)
            {
                result = Ok();
            }

            return result;

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
