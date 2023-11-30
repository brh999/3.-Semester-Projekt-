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
        private List<Offer> _posts;
        private double _discount;

        public AccountDto() {}


        public AccountDto(Account account) 
        {
            _wallet = account.Wallet;
            //_posts = account.Posts;
            _discount = account.Discount;
        }

        public double Discount { get { return _discount; } set { _discount = value; } }
        
        public List<CurrencyLine> Wallet { get { return _wallet; } set { _wallet = value; } }
        public List<Offer> Posts { get { return _posts; } set { _posts = value; } }


    }
    
}
