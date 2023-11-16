namespace Models
{
    public class Currency
    {
        private CurrencyEnum _type;
        private string _name;
        private IEnumerable<Exchange> _exchanges;


        public Currency()
        {
        }
        public Currency(CurrencyEnum type, IEnumerable<Exchange> exchanges)
        {
            _type = type;
            _exchanges = exchanges;
            switch (_type)
            {
                case CurrencyEnum.BTC:
                    _name = "Bitcoin";
                    break;
                case CurrencyEnum.XRP:
                    _name = "XRP";
                    break;
                case CurrencyEnum.ETH:
                    _name = "Etherium";
                    break;
                case CurrencyEnum.DOGE:
                    _name = "Doge Coin";
                    break;
                case CurrencyEnum.AUR:
                    _name = "Auroracoin";
                    break;
                default:
                    _name = "Dollar";
                    break;
            }
        }

        public string Name { get { return _name; } }
        public CurrencyEnum Type { get { return _type; } }
        public IEnumerable<Exchange> Exchanges { get { return _exchanges; } }




    }
    public enum CurrencyEnum
    {
        USD, BTC, XRP, ETH, DOGE, AUR
    }
}