using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult<List<Post>> getAllPosts()
        {
            ActionResult<List<Post>>? foundReturn;

            foundReturn = _postLogic.GetAllPosts();
            return foundReturn;
        }
    }
}
