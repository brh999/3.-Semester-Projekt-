using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Offer : Post
    {
        public Offer(int amount, double price, IEnumerable<TransactionLine> transactions) : base(amount, price, transactions)
        {
        }
        public Offer() 
        {
        }
    }
}
