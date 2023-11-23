namespace Models
{
    public class Account
    {
        private double _discount;
        private string _username;
        private string _email;
        private List<CurrencyLine> _wallet;
        private List<Post> _posts;

        public Account()
        {
        }

        public Account(double discount, string username, string email, List<CurrencyLine> wallet, List<Post> posts)
        {
            _discount = discount;
            _username = username;
            _email = email;
            _wallet = wallet;
            _posts = posts;
        }
        public Account(double discount, string username, string email)
        {
            _discount = discount;
            _username = username;
            _email = email;
            _wallet = new List<CurrencyLine>();
            _posts = new List<Post>();
        }

        public void AddCurrencyLine(CurrencyLine line)
        {
            if(line != null)
            {
                _wallet.Add(line);
            }
        }
        public double Discount { get { return _discount; } set {  _discount = value; } }
        public string Username { get { return _username; } set { _username = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public List<CurrencyLine> Wallet { get { return _wallet; } }
        public List<Post> Posts { get { return _posts; } }
    }
}