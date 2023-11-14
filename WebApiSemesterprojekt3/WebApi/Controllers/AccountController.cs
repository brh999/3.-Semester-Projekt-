using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebApi.BuissnessLogiclayer;
using WebApi.Database;
using WebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountControl _accountCtrl;


        public AccountController(IAccountControl inControl)
        {
            _accountCtrl = inControl;
        }

        // GET: api/<AccountController>
        [HttpGet, Route("account")]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            ActionResult<List<Account>>? foundReturn = null;

            foundReturn = _accountCtrl.GetAllAccounts();
            return foundReturn;
            }
    }

}
    
