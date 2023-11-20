using DesktopClient.BusinessLogic;
using Microsoft.VisualBasic.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DesktopClient.UI
{
    public partial class OfferPostForm : Form
    {
        private OfferControl _offerControl;
        private Form _parent;
        public OfferPostForm(Form parent)
        {
            InitializeComponent();
            _offerControl = new OfferControl();
            _parent = parent;
            cmbCurrency.DataSource = Enum.GetValues(typeof(CurrencyEnum));
        }

        private async void btnCreateBid_Click(object sender, EventArgs e)
        {
            double amount;
            double price;

            if (!double.TryParse(txtAmount.Text, out amount))
            {
                // TODO: inform user that something went wrong.
            }

            if (!double.TryParse(txtPrice.Text, out price))
            {
                // TODO: inform user that something went wrong.
            }
            CurrencyEnum currencyEnum = (CurrencyEnum)cmbCurrency.SelectedIndex;

            IEnumerable<Exchange> exchanges = new List<Exchange>();
            Currency currency = new Currency(currencyEnum, exchanges);
            Offer? createOffer = null;

            //Try to create and persist Offer and handle posible errors
            try
            {
                createOffer = await _offerControl.CreateOffer(amount, price, currency);
            }catch (ArgumentException ex) { 
                ClearBidInput();
                MessageBox.Show("Amount and price must be a positive integer");
                
            }catch(Exception ex) {
                MessageBox.Show("Offer was not saved correctly");
            }
            
            
            btnCreateOffer.Enabled = false;
            

            if (createOffer == null)
            {
                
                ClearBidInput();
            }

        }

        private void ClearBidInput()
        {
            cmbCurrency.SelectedIndex = 0;
            txtAmount.Text = string.Empty;
            txtPrice.Text = string.Empty;
            btnCreateOffer.Enabled = true;
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyEnum selectedValue = (CurrencyEnum)cmbCurrency.SelectedItem;
        }

        /// <summary>
        /// We override the OnCloseEvent, to make sure that the parent form is displayed.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _parent.Show();
        }
    }
}
