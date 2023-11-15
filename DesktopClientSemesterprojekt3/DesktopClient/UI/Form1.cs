using DesktopClient.BusinessLogic;
using Models;

namespace DesktopClient
{
    public partial class Form1 : Form
    {
        private BidControl _bidControl;
        public Form1()
        {
            InitializeComponent();
            _bidControl = new BidControl();
            cmbCurrency.DataSource = Enum.GetValues(typeof(CurrencyEnum));
        }

        private void btnCreateBid_Click(object sender, EventArgs e)
        {
            int amount;
            double price;

            if (!int.TryParse(txtAmount.Text, out amount))
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

            _bidControl.CreateBid(amount, price,currency);

        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyEnum selectedValue = (CurrencyEnum)cmbCurrency.SelectedItem;

        }
    }
}