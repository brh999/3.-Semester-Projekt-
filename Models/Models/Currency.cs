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

        public Currency(string type)
        {
            _type=type;
            _exchange = new Exchange();
        }

        public string Type { get { return _type; } set { _type = value; } }
        
        public Exchange Exchanges { get { return _exchange; } set { _exchange = value; } }

        public override string? ToString()
        {
            return $"Currency Type: {Type}      {Exchanges}";
        }
    }
}
   