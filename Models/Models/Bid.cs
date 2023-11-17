using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Models
{
    public class Bid : Post
    {
        public Bid() 
        {
            
        }
        public Bid(double amount, double price, Currency currency) : base(amount, price, currency)
        {
        }

        public override string? ToString()
        {
            string res = $" {Currency.Type} {Amount} for {Price} USD ";
            return res;
        }
    }

}
