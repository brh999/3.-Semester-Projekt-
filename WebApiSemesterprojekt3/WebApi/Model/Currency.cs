namespace WebApi.Model
{
    public class Currency
    {
        private readonly CurrencyEnum _type;

        public Currency(CurrencyEnum type)
        {
            _type = type;
        }

        public CurrencyEnum Type { get; }
    }
}
