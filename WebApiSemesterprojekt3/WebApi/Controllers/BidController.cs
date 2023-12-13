using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidController : ControllerBase
    {
        private readonly IPostLogic _bidLogic;

        public BidController(IPostLogic inControl)
        {
            _bidLogic = inControl;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {

            IEnumerable<Post> res = new List<Post>();
            res = _bidLogic.GetAllBids();

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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost, Route("insert")]
        public void InsertBid(Post bid)
        {
            throw new NotImplementedException();
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
