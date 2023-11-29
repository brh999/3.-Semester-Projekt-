namespace Models
{
    public class TransactionLine
    {
        private readonly DateTime _date;
        private double _amount;
        private Bid? _buyer;
        private Offer? _seller;

        public TransactionLine()
        {
        }

        public TransactionLine(DateTime date, double amount, Bid buyer, Offer seller)
        {
            this._date = date;
            this._amount = amount;
            this._buyer = buyer;
            this._seller = seller;
        }
        public DateTime Date { get { return _date; } set { } }
        public double Amount { get { return _amount; } set { } }
        public Bid Buyer { get { return _buyer; } set { } }
        public Offer seller { get { return _seller; } set { } }
    }
}