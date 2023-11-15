namespace WebApi.Model
{
    public class Account
    {
        private string _email;
        private string _username;
        private double _discount;
        private List<Post> _posts;
        private readonly Wallet _wallet;
        //TODO add some sort of way to get posts associated with the account.

        public Account() { }
        //public Account(string email, string username, double discount, List<Post> posts, Wallet _wallet)
        //{
        //    _email = email;
        //    _username = username;
        //    _discount = discount;
        //    this._wallet = _wallet ;
        //    Posts = posts;
        //}

        public string Email { get; set; }
        public string Username { get; set; }
        public double Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
    

    }
}
