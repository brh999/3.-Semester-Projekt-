using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Offer : Post
    {
        public Offer(double amount, double price, Currency currency) : base(amount, price, currency)
        {

        }
        public Offer() 
        {
        }

        public override string? ToString()
        {
            string res = $" {Amount} for {Price} USD ";
            return res;
        }
    }
}
