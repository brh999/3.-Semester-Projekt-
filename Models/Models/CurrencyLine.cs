namespace Models
{
    public class CurrencyLine
    {

        private int _amount;
        private Currency _currency;

        public CurrencyLine()
        {
        }

        public CurrencyLine(int amount, Currency currency)
        {
            this._amount = amount;
            this._currency = currency;
        }

        public int Amount { get { return _amount; } set { _amount = value; } }
        public Currency Currency { get { return _currency; } }
    }
}