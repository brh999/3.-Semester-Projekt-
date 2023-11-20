using DesktopClient.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Service;

namespace DesktopClient.BusinessLogic
{
    public class BidControl : IBidControl
        
    {
        private IPostService _postService;

        public BidControl() { 
            _postService = new PostService();
        }

        /// <summary>
        /// This method creates and persist a new bid.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>

        public async Task<Bid?> CreateBid(double amount, double price, Currency currency)
        {

            Bid? newBid = null;
            // Validate the method arguments
            if (amount > 0 && price > 0)
            {
                //Create the bid
                newBid = new Bid(amount, price, currency);


                // Persist the new bid
                bool ok = await _postService.SaveBid(newBid);

                // Check that the bid was saved correctly
                if (!ok) {
                    throw new Exception("Bid was not saved correctly");
                }


            }
            else
            {
                throw new ArgumentException();
            }

            return newBid;
        }
    }
}
