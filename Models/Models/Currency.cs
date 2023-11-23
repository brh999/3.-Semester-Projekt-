namespace Models
{
    public class Currency
    {

        private string _type;
        private Exchange _exchange;


        public Currency()
        {
        }
        public Currency(Exchange exchange, string type)
        {
            _type = type;
            _exchange = exchange;

        }

        public string Type { get { return _type; } set { _type = value; } }
        //ublic CurrencyEnum Type { get { return _type; } set { _type = value; } }
        public Exchange Exchanges { get { return _exchange; } init { _exchange = value; } }


    }
}
   