using DesktopClient.Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.BusinessLogic
{
    internal class OfferControl : IOfferControl
    {

        private IPostService _postService;

        public OfferControl() { 
            _postService = new PostService();
        
        }
        public async Task<Offer> CreateOffer(double amount, double price, Currency currency)
        {
            Offer? newOffer = null;

            if (amount > 0 && price > 0)
            {
                newOffer = new Offer(amount, price, currency);
                bool isSaved = await _postService.SaveOffer(newOffer);

                if (!isSaved)
                {
                    throw new Exception("Offer was not save correctly");
                }
                
            }
            else
            {
                throw new ArgumentException();
            }

            return newOffer;
        }
    }
}
