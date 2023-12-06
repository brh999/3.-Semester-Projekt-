using DesktopClient.BusinessLogic;
using Models;

namespace DesktopClient.UI
{
    public partial class Client : Form
    {
        private ICurrencyLogic _currencyLogic;
        private IPostLogic _postLogic;
        private IAccountLogic _accountLogic;
        public Client()
        {
            InitializeComponent();
            _currencyLogic = new CurrencyLogic();
            _postLogic = new PostLogic();
            _accountLogic = new AccountLogic();
            UpdateCurrencies();
            UpdatePosts();
            UpdateAccounts();
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            string name = txtCurrencyName.Text;
            double value = Double.Parse(txtCurrencyValue.Text);
            bool isValid = false;
            isValid = !String.IsNullOrWhiteSpace(name);
            if (isValid)
            {
                bool isSuccess = await _currencyLogic.CreateCurrency(name, value);
                if (!isSuccess)
                {
                    MessageBox.Show("Could not create currency");
                }
                else
                {
                    MessageBox.Show("Currency Created");
                    UpdateCurrencies();
                }
            }

        }

        private async void UpdateCurrencies()
        {
            List<Currency> data = (List<Currency>)await _currencyLogic.GetAllCurrencies();
            ListBoxCurrencies.DataSource = data;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void UpdatePosts()
        {
            List<Post> data = (List<Post>)await _postLogic.GetAllPosts();
            listPosts.DataSource = data;
        }

        private void listPosts_SelectedIndexChanged(object sender, EventArgs e)
        {

            Post? post = (Post)listPosts.SelectedItem;
            if (post != null)
            {
                UpdateTransactions(post);
                btnDeletePost.Enabled = true;

            }

        }

        private async void UpdateTransactions(Post post)
        {
            List<TransactionLine> data = (List<TransactionLine>)await _postLogic.GetRelatedTransactions(post);
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
            txtBoxTotal.Text = UpdatePrice(data).ToString();
        }
        private double UpdatePrice(List<CurrencyLine> data)
        {
            double res = 0;
            foreach (CurrencyLine currencyLine in data)
            {
                res += currencyLine.GetPrice();
            }
            return res;
        }

        private void btnDeletePost_Click(object sender, EventArgs e)
        {
            Post item = (Post)listPosts.SelectedItem;
            if (item != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _postLogic.DeletePost(item);
                    UpdatePosts();
                }
                else
                {
                    //Stop, Hammertime!

                }
            }
        }

        private void txtCurrencyValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false; // Allow Backspace and delete
                return;
            }

            // Check if the key pressed is a digit or a dot
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Block non-digit and dot characters
            }

            // Check if '.' is already present in the textbox
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true; // Block additional dots
            }
        }

        private void txtCurrencyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false; // Allow Backspace and delete
                return;
            }

            if (!char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Block non-letter characters
            }
        }
    }
}
