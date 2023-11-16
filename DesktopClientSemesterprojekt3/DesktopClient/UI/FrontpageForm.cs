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
    public partial class FrontpageForm : Form
    {
        public FrontpageForm()
        {
            InitializeComponent();
        }

        private void btnBidPost_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BidPostForm(this).ShowDialog();

        }
    }
}
