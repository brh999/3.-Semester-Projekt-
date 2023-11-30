namespace Models
{
    public class TransactionLine
    {
        private DateTime _date;
        private double _amount;
        private Bid? _buyer;
        private Post? _seller;

        public TransactionLine()
        {
        }

        public TransactionLine(DateTime date, double amount, Bid buyer, Post seller)
        {
            this._date = date;
            this._amount = amount;
            this._buyer = buyer;
            this._seller = seller;
        }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public double Amount { get { return _amount; } set { _amount = value; } }
        public Bid Buyer { get { return _buyer; } set { _buyer = value; } }
        public Post Seller { get { return _seller; } set { _seller = value; } }

        public override string? ToString()
        {
            string res = $"{_amount} {_buyer.Id} at {_date}";
            return res;
        }
    }
}