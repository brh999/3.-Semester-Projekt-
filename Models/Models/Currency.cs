namespace Models
{
    public class Currency
    {

        private string _type;
        private IEnumerable<Exchange> _exchanges;


        public Currency()
        {
        }
        public Currency(IEnumerable<Exchange> exchanges, string type)
        {
            _type = type;
            _exchanges = exchanges;

        }

        public string Type { get { return _type; } set { _type = value; } }
        //ublic CurrencyEnum Type { get { return _type; } set { _type = value; } }
        public IEnumerable<Exchange> Exchanges { get { return _exchanges; } init { _exchanges = value; } }


    }
}
   