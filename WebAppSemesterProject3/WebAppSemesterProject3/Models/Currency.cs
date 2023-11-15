namespace WebAppSemesterProject3.Models
{
    public class Currency
    {
        public CurrencyEnum Type { get; set; }
        public string Name { get; set; }
        public List<Exchange> Exchanges { get; }

        public Currency(CurrencyEnum type)
        {
            Type = type;
            switch (type)
            {
                case CurrencyEnum.USD:
                    Name = "Dollars";
                    break;
                case CurrencyEnum.BTC:
                    Name = "Bitcoin";
                    break;
                case CurrencyEnum.XRP:
                    Name = "Ripple";
                    break;
            }
        }

        public void AddExchange(Exchange exchange)
        {
            Exchanges.Add(exchange);
        }
    }

    public enum CurrencyEnum
    {
        USD, BTC, XRP
    }
}
