using System.ComponentModel.DataAnnotations;

namespace WebAppSemesterProject3.Models
{
    public class Account
    {



        public Account(string name, string mail, double discount, int id, Wallet wallet)
        {
            _username = name;
            _email = mail;
            _discount = discount;
            _accountId = id;
            _wallet = wallet;
        }

        public Account() { }

        public string? _username { get; set; }

        public string? _email { get; set; }

        public double _discount { get; set; }
        public int _accountId { get; set; }

        public Wallet _wallet { get; set; }
    }
}
