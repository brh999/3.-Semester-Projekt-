using DesktopClient.BusinessLogic;
using Models;

namespace DesktopClient
{
    public partial class BidPostForm : Form
    {
        private BidControl _bidControl;
        private Form _parent;
        public BidPostForm(Form parent)
        {
            InitializeComponent();
            this._parent = parent;
           
            _bidControl = new BidControl();
            
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
            CurrencyEnum chosenEnum = (CurrencyEnum)cmbCurrency.SelectedItem;
            
            IEnumerable<Exchange> exchanges = new List<Exchange>();
            Currency currency = new Currency(chosenEnum, exchanges);
            Bid? createBid = null;
            Task createBidPost =  Task.Run(() =>
            {
                createBid = _bidControl.CreateBid(amount, price, currency);
            });
            btnCreateBid.Enabled = false;
            await createBidPost;

            if(createBid == null) {
                MessageBox.Show("Input is not valid, only positive integers");
                ClearBidInput();
            }
        
        }

        private void ClearBidInput()
        {
            cmbCurrency.SelectedIndex = 0;
            txtAmount.Text = string.Empty;
            txtPrice.Text = string.Empty;
            btnCreateBid.Enabled = true;
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