using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{


    public class AccountDto
    {
        private List<CurrencyLine> _wallet;
        private List<Post> _posts;
        private double _discount;

        public AccountDto() {}


        public AccountDto(Account account) 
        {
            _wallet = account.Wallet;
            _posts = account.Posts;
            _discount = account.Discount;
        }

    }
    
}
