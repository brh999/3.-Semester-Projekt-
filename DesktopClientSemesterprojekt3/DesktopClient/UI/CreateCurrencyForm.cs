using DesktopClient.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace DesktopClient.UI
{
    public partial class CreateCurrencyForm : Form
    {
        private ICurrencyLogic currencyLogic;
        private Form _parent;
        public CreateCurrencyForm(Form parent)
        {
            InitializeComponent();
            _parent = parent;
            currencyLogic = new CurrencyLogic();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string name = txtCurrencyName.Text;
            bool isValid = true;
            
            isValid = String.IsNullOrWhiteSpace(name);

            isValid = name.Length == name.Trim().Length;

            if (isValid)
            {
                try
                {
                    bool isCreated = currencyLogic.CreateCurrency(name);

                    if (!isCreated) {
                        MessageBox.Show($"currency could not be created");
                    }
                    else
                    {
                        btnCancel_Click(this, e);
                    }
                }
                catch {
                    
                }
                
            }
            else
            {
                MessageBox.Show($"\"{name}\" is not valid");
                txtCurrencyName.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
