using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Models
{
    public abstract class Post
    {
        private int _amount;
        private double _price;
        private bool _isComplete;
        private Currency _currency;
        private IEnumerable<TransactionLine> _transactions;

        public Post()
        {
        }
        public Post(int amount, double price, Currency currency)
        {
            _amount = amount;
            _price = price;
            _isComplete = false;
            _transactions = new List<TransactionLine>();
            _currency = currency;
        }

        
        public int Amount { get { return _amount; } }
        
        public double Price { get { return _price; } }
        public bool IsComplete { get { return _isComplete; } set { _isComplete = value; } }
        public IEnumerable<TransactionLine> Transactions { get { return _transactions; } }

        public Currency Currency { get { return _currency;} }
    }
}