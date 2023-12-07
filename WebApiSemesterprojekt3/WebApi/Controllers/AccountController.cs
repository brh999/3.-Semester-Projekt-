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
        public ActionResult<List<Account>> GetAllAccounts()
        {
            ActionResult<List<Account>>? foundReturn = null;

            foundReturn = _accountLogic.GetAllAccounts();
            return foundReturn;
        }


        [HttpGet, Route("api/[Controller]/{aspDotNetId}")]

        public ActionResult<AccountDto> GetAccount(string aspDotNetId)

        {
            AccountDto? accountDto = null;
            Account account = _accountLogic.GetAccountById(aspDotNetId);
            if (!string.IsNullOrEmpty(account.Email) && !string.IsNullOrEmpty(account.Username))
            {
                accountDto = new AccountDto(account);
            }

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




    }

}

