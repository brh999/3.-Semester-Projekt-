namespace WebApi.Model
{
    public class CurrencyLine<Currency>
    {
        private double _amount;
        private readonly Currency _currency;

        public CurrencyLine(double amount, Currency currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public double Amount { get; set; }
        public Currency GetCurrency
        {
            get { return _currency; }
        }
    }
}