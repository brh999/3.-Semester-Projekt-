﻿using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.BuissnessLogiclayer;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostLogic _postLogic;

        public PostController(IPostLogic postLogic)
        {
            _postLogic = postLogic;
        }


        // GET: api/<PostController>
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            IEnumerable<Post> foundResult = new List<Post>();
            foundResult = _postLogic.GetAllPosts();
            return Ok(foundResult);
        }


        [HttpPost]
        [Route("/buyoffer")]
        public IActionResult BuyOffer([FromBody] Post inPost)
        {
            _postLogic.BuyOffer(inPost);

            return Ok();
        }
    }
}
