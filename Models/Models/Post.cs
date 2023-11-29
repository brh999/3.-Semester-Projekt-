namespace Models
{
    public abstract class Post
    {
        private int _id;
        private double _amount;
        private double _price;
        private bool _isComplete;
        private Currency _currency;
        private List<TransactionLine> _transactions;

        public Post()
        {
        }


        public Post(double amount, double price, Currency currency, int id)
        {
            _id = id;
            _amount = amount;
            _price = price;
            _isComplete = false;
            _transactions = new List<TransactionLine>();
            
            _currency = currency;
        }

        public double Amount { get { return _amount; } init { _amount = value; } }
        public double Price { get { return _price; } init { _price = value; } }
        public bool IsComplete { get { return _isComplete; } set { _isComplete = value; } }
        public List<TransactionLine> Transactions { get { return _transactions; } set { _transactions = value; } }
        public Currency Currency { get { return _currency; } init { _currency = value; } }
        public int Id { get { return _id; } set { _id = value; } }
    }
}