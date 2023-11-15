namespace WebApi.Model
{
    public class Wallet
    {
 
        public List<CurrencyLine<Currency>> _valutaLines;

        public Wallet()
        {
            _valutaLines = new List<CurrencyLine<Currency>>();

        }
    }
}
