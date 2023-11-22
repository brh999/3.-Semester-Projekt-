﻿namespace DesktopClient.UI
{
    partial class FrontpageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel1 = new Panel();
            lblOffer = new Label();
            lblBids = new Label();
            lstBoxOffer = new ListBox();
            lstBoxBid = new ListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnBidPost = new Button();
            btnOfferPost = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblOffer);
            panel1.Controls.Add(lblBids);
            panel1.Controls.Add(lstBoxOffer);
            panel1.Controls.Add(lstBoxBid);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(700, 338);
            panel1.TabIndex = 0;
            // 
            // lblOffer
            // 
            lblOffer.AutoSize = true;
            lblOffer.Location = new Point(478, 50);
            lblOffer.Name = "lblOffer";
            lblOffer.Size = new Size(75, 15);
            lblOffer.TabIndex = 5;
            lblOffer.Text = "Active Offers";
            // 
            // lblBids
            // 
            lblBids.AutoSize = true;
            lblBids.Location = new Point(197, 50);
            lblBids.Name = "lblBids";
            lblBids.Size = new Size(65, 15);
            lblBids.TabIndex = 4;
            lblBids.Text = "Active Bids";
            // 
            // lstBoxOffer
            // 
            lstBoxOffer.FormattingEnabled = true;
            lstBoxOffer.ItemHeight = 15;
            lstBoxOffer.Location = new Point(478, 67);
            lstBoxOffer.Margin = new Padding(3, 2, 3, 2);
            lstBoxOffer.Name = "lstBoxOffer";
            lstBoxOffer.Size = new Size(155, 229);
            lstBoxOffer.TabIndex = 3;
            // 
            // lstBoxBid
            // 
            lstBoxBid.FormattingEnabled = true;
            lstBoxBid.ItemHeight = 15;
            lstBoxBid.Location = new Point(197, 67);
            lstBoxBid.Margin = new Padding(3, 2, 3, 2);
            lstBoxBid.Name = "lstBoxBid";
            lstBoxBid.Size = new Size(155, 229);
            lstBoxBid.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18F));
            tableLayoutPanel1.Controls.Add(btnBidPost, 0, 0);
            tableLayoutPanel1.Controls.Add(btnOfferPost, 0, 1);
            tableLayoutPanel1.Location = new Point(10, 82);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Size = new Size(71, 213);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnBidPost
            // 
            btnBidPost.Location = new Point(3, 2);
            btnBidPost.Margin = new Padding(3, 2, 3, 2);
            btnBidPost.Name = "btnBidPost";
            btnBidPost.Size = new Size(65, 22);
            btnBidPost.TabIndex = 0;
            btnBidPost.Text = "Bid";
            btnBidPost.UseVisualStyleBackColor = true;
            btnBidPost.Click += btnBidPost_Click;
            // 
            // btnOfferPost
            // 
            btnOfferPost.Location = new Point(3, 28);
            btnOfferPost.Margin = new Padding(3, 2, 3, 2);
            btnOfferPost.Name = "btnOfferPost";
            btnOfferPost.Size = new Size(65, 22);
            btnOfferPost.TabIndex = 1;
            btnOfferPost.Text = "Offer";
            btnOfferPost.UseVisualStyleBackColor = true;
            btnOfferPost.Click += btnOfferPost_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(192, 37);
            label1.TabIndex = 0;
            label1.Text = "Our Exchange";
            // 
            // FrontpageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrontpageForm";
            Text = "FrontpageForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnBidPost;
        private Button btnOfferPost;
        private ListBox lstBoxBid;
        private Label lblOffer;
        private Label lblBids;
        private ListBox lstBoxOffer;
    }
}