using DesktopClient.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    public class BidControl : IBidControl
        
    {
        private IPostService _postService;
        public BidControl()
        {
            _postService = new PostService();
        }
        public async Task<Bid?> CreateBid(double amount, double price, Currency currency)
        {

            Bid? newBid = null;

            if (amount > 0 && price > 0)
            {
                newBid = new Bid(amount, price, currency);
                bool ok = await _postService.SavePost(newBid);

            }
            else
            {
                throw new ArgumentException();
            }

            return newBid;
        }
    }
}
