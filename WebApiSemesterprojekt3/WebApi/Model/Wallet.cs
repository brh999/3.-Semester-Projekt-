namespace WebApi.Model
{
    public class Wallet
    {
        public List<CurrencyLine> _valutaLines;

        public Wallet()
        {
            _valutaLines = new List<CurrencyLine>();
        }
    }
}
