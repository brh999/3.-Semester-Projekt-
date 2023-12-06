using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;
using WebApi.Database;

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

            try
            {
                res = _offerLogic.GetRelatedTransactions(id);
                if (res != null)
                {
                    foundLines = Ok(res);
                }
                else
                {
                    foundLines = NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                foundLines = StatusCode(id, ex.Message);
            }catch(DatabaseException ex)
            {
                foundLines = StatusCode(500, ex.Message);
            }
            

            
            return foundLines;
        }

        // GET: api/<OfferController>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {

            IEnumerable<Post> res = new List<Post>();
            res = _offerLogic.GetAllOffers();

            if (res != null && res.Count() > 0)
            {
                return Ok(res);
            }
            else if (res != null && res.Count() <= 0)
            {
                return StatusCode(204); // Success with no content
            }

            return StatusCode(500);

        }


        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OfferController>
        [HttpPost("{aspNetUserId}")]
        public IActionResult Post([FromBody] Post inOffer, string aspNetUserId)
        {
            
            IActionResult result = StatusCode(500);
           
            try
            {
                Post returnedOffer = _offerLogic.InsertOffer(inOffer, aspNetUserId);

                if (returnedOffer.Equals(inOffer)){
                    result = Ok(returnedOffer);
                }
            }
            catch (DatabaseException)
            {

            }catch (ArgumentException ex)
            {
                result = BadRequest(ex.Message);
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
        public IActionResult Delete(int id)
        {
            IActionResult result = StatusCode(500);
            try
            {
                if (_offerLogic.DeleteOffer(id))
                {
                    result = Ok();
                }
            }
            catch (DatabaseException)
            {

            }catch(ArgumentException ex)
            {
                result = BadRequest(ex.Message);
            }
            return result;
        }

        [HttpPost]
        [Route("buyoffer/{aspNetUserId}")]
        public IActionResult BuyOffer([FromBody] Post inPost, string aspNetUserId)
        {
            _offerLogic.BuyOffer(inPost, aspNetUserId);

            return Ok();
        }

    }
}
