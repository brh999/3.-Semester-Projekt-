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
        private CurrencyLogic _currencyLogic;
        public StartupForm()
        {
            InitializeComponent();
            _currencyLogic = new CurrencyLogic();
            UpdateCurrencies();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CreateCurrencyForm(this).ShowDialog();
        }

        private async void UpdateCurrencies ()
        {
            List<Currency> data = (List<Currency>)await _currencyLogic.GetAllCurrencies();
            ListBoxCurrencies.Items.Clear();
            ListBoxCurrencies.DataSource = data;
        }

       



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
