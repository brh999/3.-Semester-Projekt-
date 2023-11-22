using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authorization;
using WebApi.BuissnessLogiclayer;




namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountLogic _accountCtrl;


        public AccountController(IAccountLogic inControl)
        {
            _accountCtrl = inControl;
        }

        // GET: api/account
        [HttpGet, Route("/")]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            ActionResult<List<Account>>? foundReturn = null;

            foundReturn = _accountCtrl.GetAllAccounts();
            return foundReturn;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet, Route("/{id}")]
        public ActionResult<Account> GetAccount([FromBody] int id)
        {
            return null;
        }
    }

}

