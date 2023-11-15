﻿using WebApi.Model;

namespace WebApi.Database
{
    public interface IBidDBAccess
    {
        public IEnumerable<Bid> GetAllBids();
    }
}