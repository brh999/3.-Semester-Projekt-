using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bid : Post
    {
        public Bid() 
        { 
        }
        public Bid(int amount, double price, IEnumerable<TransactionLine> transactions) : base(amount, price, transactions)
        {
        }
    }
}
