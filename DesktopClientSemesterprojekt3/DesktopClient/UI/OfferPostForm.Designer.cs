namespace DesktopClient.UI
{
    partial class OfferPostForm
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
            pnlCreateBuyorder = new Panel();
            label3 = new Label();
            cmbCurrency = new ComboBox();
            btnCreateOffer = new Button();
            txtAmount = new TextBox();
            label2 = new Label();
            txtPrice = new TextBox();
            label1 = new Label();
            pnlCreateBuyorder.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCreateBuyorder
            // 
            pnlCreateBuyorder.Controls.Add(label3);
            pnlCreateBuyorder.Controls.Add(cmbCurrency);
            pnlCreateBuyorder.Controls.Add(btnCreateOffer);
            pnlCreateBuyorder.Controls.Add(txtAmount);
            pnlCreateBuyorder.Controls.Add(label2);
            pnlCreateBuyorder.Controls.Add(txtPrice);
            pnlCreateBuyorder.Controls.Add(label1);
            pnlCreateBuyorder.Location = new Point(281, 72);
            pnlCreateBuyorder.Name = "pnlCreateBuyorder";
            pnlCreateBuyorder.Size = new Size(180, 274);
            pnlCreateBuyorder.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 9);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 5;
            label3.Text = "Currency";
            // 
            // cmbCurrency
            // 
            cmbCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCurrency.FormattingEnabled = true;
            cmbCurrency.Location = new Point(19, 46);
            cmbCurrency.Name = "cmbCurrency";
            cmbCurrency.Size = new Size(60, 28);
            cmbCurrency.TabIndex = 6;
            cmbCurrency.TabStop = false;
            cmbCurrency.SelectedIndexChanged += cmbCurrency_SelectedIndexChanged;
            // 
            // btnCreateOffer
            // 
            btnCreateOffer.Location = new Point(20, 217);
            btnCreateOffer.Name = "btnCreateOffer";
            btnCreateOffer.Size = new Size(135, 29);
            btnCreateOffer.TabIndex = 0;
            btnCreateOffer.TabStop = false;
            btnCreateOffer.Text = "Create Post";
            btnCreateOffer.UseVisualStyleBackColor = true;
            btnCreateOffer.Click += btnCreateBid_Click;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(20, 175);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(84, 27);
            txtAmount.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 77);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 4;
            label2.Text = "Price";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(20, 100);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(84, 27);
            txtPrice.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 149);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 3;
            label1.Text = "Amount";
            // 
            // OfferPostForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlCreateBuyorder);
            Name = "OfferPostForm";
            Text = "OfferPostForm";
            pnlCreateBuyorder.ResumeLayout(false);
            pnlCreateBuyorder.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCreateBuyorder;
        private Label label3;
        private ComboBox cmbCurrency;
        private Button btnCreateOffer;
        private TextBox txtAmount;
        private Label label2;
        private TextBox txtPrice;
        private Label label1;
    }
}