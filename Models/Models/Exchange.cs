namespace Models
{
    public class Exchange
    {
        private double _value;
        private readonly DateTime _date;


        public Exchange()
        {

        }
        public Exchange(double value, DateTime date)
        {
            _value = value;
            _date = date;
        }

        public double Value { get { return _value; } init { _value = value; } }
        public DateTime Date { get { return _date; } init { _date = value; } }

        public override string? ToString()
        {
            return $"Value: {Value}";
        }
    }
}