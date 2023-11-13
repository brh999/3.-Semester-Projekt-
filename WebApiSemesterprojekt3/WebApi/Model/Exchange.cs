namespace WebApi.Model
{
    public class Exchange
    {
        private double _value;
        private DateTime _date;

        public Exchange(double value)
        {
            _date = DateTime.Now;
            _value = value;
        }
    }
}
