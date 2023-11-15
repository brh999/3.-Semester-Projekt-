namespace Models
{
    public class Account
    {
        private double _discount;
        private string _username;
        private string _email;
        private IEnumerable<CurrencyLine> _wallet;
        private IEnumerable<Post> _posts;

        public Account()
        {
        }

        public Account(double discount, string username, string email, IEnumerable<CurrencyLine> wallet, IEnumerable<Post> posts)
        {
            _discount = discount;
            _username = username;
            _email = email;
            _wallet = wallet;
            _posts = posts;
        }
        public double Discount { get { return _discount; } set {  _discount = value; } }
        public string Username { get { return _username; } set { _username = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public IEnumerable<CurrencyLine> Wallet { get { return _wallet; } }
        public IEnumerable<Post> Posts { get { return _posts; } }
    }
}