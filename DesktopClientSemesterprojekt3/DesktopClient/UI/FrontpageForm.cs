using DesktopClient.Service;
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
    public partial class FrontpageForm : Form
    {
        private PostService _postService;
        public FrontpageForm()
        {
            InitializeComponent();
            _postService = new PostService();


        }

        private void btnBidPost_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BidPostForm(this).ShowDialog();

        }

        private async void UpdateBids()
        {
            
            List<Bid> data = await _postService.GetAllBids();
            lstBoxBid.Items.Clear();

            lstBoxBid.Items.Add(data);
        }
    }
}
