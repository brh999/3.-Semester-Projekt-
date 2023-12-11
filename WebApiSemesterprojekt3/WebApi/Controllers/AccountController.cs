using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using WebApi.BuissnessLogiclayer;




namespace WebApi.Controllers
{
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountLogic _accountLogic;


        public AccountController(IAccountLogic inControl)
        {
            _accountLogic = inControl;
        }

        // GET: api/account
        [HttpGet, Route("api/[Controller]")]
        public ActionResult<List<AccountDto>> GetAllAccounts()
        {
            ActionResult<List<AccountDto>>? foundReturn = null;

            foundReturn = _accountLogic.GetAllAccounts();
            return foundReturn;
        }


        [HttpGet, Route("api/[Controller]/{aspDotNetId}")]

        public ActionResult<AccountDto> GetAccount(string aspDotNetId)

        {
            AccountDto? accountDto = _accountLogic.GetAccountById(aspDotNetId);
            
            return accountDto;
        }

        // GET: api/<AccountController>
        [HttpGet, Route("api/[Controller]/{id}/wallet")]
        public ActionResult<IEnumerable<CurrencyLine>> GetRelatedCurrencyLines(int id)
        {
            ActionResult<IEnumerable<CurrencyLine>>? foundLines = null;
            List<CurrencyLine> res = null;
            res = _accountLogic.GetRelatedCurrencyLines(id);

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

        // UPDATE: api/<AccountController>
        [HttpPost, Route("api/[Controller]/{id}/wallet")]
        public ActionResult InsertCurrencyLine(string aspDotNetId, CurrencyLine currencyLine)
        {
            ActionResult res = StatusCode(500);
           bool success = _accountLogic.InsertCurrencyLine(aspDotNetId, currencyLine);

            if (success)
            {
                res = Ok();
            }
            return res;
        }




    }

}

