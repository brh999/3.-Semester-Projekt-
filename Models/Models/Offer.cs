using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Offer : Post
    {
        public Offer(int amount, double price, Currency currency) : base(amount, price, currency)
        {

        }
        public Offer() 
        {
        }
    }
}
