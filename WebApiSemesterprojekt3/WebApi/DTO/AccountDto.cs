using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DTO
{


    public class AccountDto
    {
        private List<CurrencyLine> _wallet;
        private List<Post> _posts;
        private double _discount;
        private int _id;
        private string _name;
        private string _email;

        public AccountDto() {}


        public AccountDto(Account account) 
        {
            _wallet = account.Wallet;
            _posts = account.Posts;
            _discount = account.Discount;
            _id = account.Id;
            _name = account.Username;
            _email = account.Email;
        }

        public double Discount { get { return _discount; } set { _discount = value; } }
        
        public List<CurrencyLine> Wallet { get { return _wallet; } set { _wallet = value; } }
        public List<Post> Posts { get { return _posts; } set { _posts = value; } }

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }

    }
    
}
