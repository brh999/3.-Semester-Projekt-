﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            ActionResult<IEnumerable<Post>>? foundBids = null;
            List<Post> res = null;
            res = _bidLogic.GetAllBids();

            if (res != null)
            {
                foundBids = Ok(res);
            }
            else
            {
                foundBids = NotFound();
            }
            return foundBids;

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
            try
            {
                _bidLogic.InsertBid(bid);
            }
            catch
            {

            }
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
