namespace WebApi.Model
{
    abstract public class Post
    {
        private double _amount;

        private double _price;

        private bool _isComplete;

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

    }
}
