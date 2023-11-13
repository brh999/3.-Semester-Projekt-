namespace WebApi.Model
{
    public class Exchange
    {
        private double _value;
        private DateTime _date;
        private List<Post> _posts;

        public Exchange()
        {
            _date = DateTime.Now;

        }

    }
}
