using DesktopClient.BusinessLogic;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.UI
{
    public partial class StartupForm : Form
    {
        private ICurrencyLogic _currencyLogic;
        private IPostLogic _postLogic;
        private IAccountLogic _accountLogic;
        public StartupForm()
        {
            InitializeComponent();
            _currencyLogic = new CurrencyLogic();
            _postLogic = new PostLogic();
            _accountLogic = new AccountLogic();
            UpdateCurrencies();
            UpdatePosts();
            UpdateAccounts();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            new CreateCurrencyForm(this).ShowDialog();
        }

        private async void UpdateCurrencies()
        {
            List<Currency> data = (List<Currency>)await _currencyLogic.GetAllCurrencies();
            ListBoxCurrencies.Items.Clear();
            ListBoxCurrencies.DataSource = data;
        }





        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void UpdatePosts()
        {
            List<Offer> data = (List<Offer>)await _postLogic.GetAllPosts();
            listPosts.Items.Clear();
            listPosts.DataSource = data;
        }

        private void listPosts_SelectedIndexChanged(object sender, EventArgs e)
        {

            Offer? offer = (Offer)listPosts.SelectedItem;
            if (offer != null)
            {
                UpdateTransactions(offer);

            }

        }

        private async void UpdateTransactions(Offer offer)
        {
            List<TransactionLine> data = (List<TransactionLine>)await _postLogic.GetRelatedTransactions(offer);
            listTransactions.DataSource = null;
            listTransactions.DataSource = data;
        }
        private async void UpdateAccounts()
        {
            List<Account> data = (List<Account>)await _accountLogic.GetAllAccounts();
            listBoxAccounts.DataSource = data;
        }

        private void listBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account? account = (Account)listBoxAccounts.SelectedItem;
            if (account != null)
            {
                UpdateCurrencyLines(account);
            }
        }

        private async void UpdateCurrencyLines(Account account)
        {
            List<CurrencyLine> data = (List<CurrencyLine>)await _accountLogic.GetRelatedCurrencyLines(account);
            listCurrencyLines.DataSource = data;
        }
    }
}
