namespace Models
{
    public class TransactionLine
    {
        private readonly DateTime _date;
        private int _amount;
        private Offer? _buyer;
        private Offer? _seller;

        public TransactionLine()
        {
        }

        public TransactionLine(DateTime date, int amount, Offer buyer, Offer seller)
        {
            this._date = date;
            this._amount = amount;
            this._buyer = buyer;
            this._seller = seller;
        }
        public DateTime Date { get { return _date; } }
        public int Amount { get { return _amount; } }
        public Offer Buyer { get { return _buyer; } }
        public Offer seller { get { return _seller; } }
    }
}