using System.ComponentModel.DataAnnotations;

namespace WebAppSemesterProject3.Models
{
    public class Account
    {



        public Account(string name, string mail, double discount, int id)
        {
            this.Username = name;
            this.Email = mail;
            this.discount = discount;
            this.AccountId = id;
        }

        public Account() { }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public double discount { get; set; }
        public int AccountId { get; set; }
    }
}
