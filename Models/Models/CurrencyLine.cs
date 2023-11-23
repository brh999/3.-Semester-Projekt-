﻿namespace Models
{
    public class CurrencyLine
    {

        private double _amount;
        private Currency _currency;

        public CurrencyLine()
        {
        }

        public CurrencyLine(double amount, Currency currency)
        {
            this._amount = amount;
            this._currency = currency;
        }

        public double Amount { get { return _amount; } set { _amount = value; } }
        public Currency Currency { get { return _currency; } }
    }
}