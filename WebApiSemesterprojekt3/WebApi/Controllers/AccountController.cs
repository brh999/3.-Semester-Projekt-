using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authorization;
using WebApi.BuissnessLogiclayer;
using Models.DTO;




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


        [HttpGet, Route("api/[Controller]/{id}")]

        public ActionResult<AccountDto> GetAccount(string id)

        {
            AccountDto? accountDto = null;
            Account account = _accountLogic.GetAccountById(id);
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

            if(res != null)
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

