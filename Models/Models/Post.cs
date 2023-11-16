﻿using System.Transactions;

namespace Models
{
    public abstract class Post
    {
        private double _amount;
        private double _price;
        private bool _isComplete;
        private IEnumerable<TransactionLine> _transactions;

        public Post()
        {
        }
        public Post(double amount, double price, IEnumerable<TransactionLine> transactions)
        {
            _amount = amount;
            _price = price;
            _isComplete = false;
            _transactions = transactions;
        }

        public double Amount { get { return _amount; } init { _amount = value; } }
        public double Price { get { return _price; } init { _price = value; } }
        public bool IsComplete { get { return _isComplete; } set { _isComplete = value; } }
        public IEnumerable<TransactionLine> Transactions { get { return _transactions; } }
    }
}