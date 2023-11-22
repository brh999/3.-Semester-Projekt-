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
    }

}

