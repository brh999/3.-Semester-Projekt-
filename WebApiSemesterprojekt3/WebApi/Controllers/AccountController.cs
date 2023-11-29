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
        private readonly IAccountLogic _accountCtrl;


        public AccountController(IAccountLogic inControl)
        {
            _accountCtrl = inControl;
        }

        // GET: api/account
        [HttpGet, Route("api/[Controller]")]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            ActionResult<List<Account>>? foundReturn = null;

            foundReturn = _accountCtrl.GetAllAccounts();
            return foundReturn;
        }

        
        [HttpGet, Route("api/[Controller]/{id}")]

        public ActionResult<AccountDto> GetAccount(string id)  

        {
            AccountDto? accountDto = null;
            Account account = _accountCtrl.GetAccountById(id);
            if(!string.IsNullOrEmpty(account.Email) && !string.IsNullOrEmpty(account.Username))
            {
                accountDto = new AccountDto(account);
            }
            
            return accountDto;
        }

        


    }

}

