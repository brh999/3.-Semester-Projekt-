namespace Models
{
    public class CurrencyLine
    {

        private double _amount;
        private Currency _currency;

        public CurrencyLine()
        {
        }

        public CurrencyLine(double amount, Currency currency)
        {
            this._amount = amount;
            this._currency = currency;
        }

        public double Amount { get { return _amount; } set { _amount = value; } }
        public Currency Currency { get { return _currency; } set { _currency = value; }  }

        public double GetPrice()
        {
            double res;
            res = _amount * _currency.Exchange.Value;
            return res;
        }

        public override string? ToString()
        {
            string res = $"{_currency.Type} : {_amount}";
            return res;
        }
    }
}