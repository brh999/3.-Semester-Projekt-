namespace Models
{
    public class Post
    {
        private int _id;
        private double _amount;
        private double _price;
        private bool _isComplete;
        private Currency _currency;
        private List<TransactionLine> _transactions;
        private string _type;

        public Post()
        {
        }


        public Post(double amount, double price, Currency currency, int id, string type)
        {
            _id = id;
            _amount = amount;
            _price = price;
            _isComplete = false;
            _transactions = new List<TransactionLine>();
            _type = type;
            _currency = currency;
        }

        public double Amount { get { return _amount; } init { _amount = value; } }
        public double Price { get { return _price; } init { _price = value; } }
        public bool IsComplete { get { return _isComplete; } set { _isComplete = value; } }
        public List<TransactionLine> Transactions { get { return _transactions; } set { _transactions = value; } }
        public Currency Currency { get { return _currency; } init { _currency = value; } }
        public int Id { get { return _id; } set { _id = value; } }
        public string Type { get { return _type; } set { _type = value; } }


    }
}