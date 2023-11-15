namespace DesktopClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCreateBid = new Button();
            txtAmount = new TextBox();
            txtPrice = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cmbCurrency = new ComboBox();
            SuspendLayout();
            // 
            // btnCreateBid
            // 
            btnCreateBid.Location = new Point(537, 374);
            btnCreateBid.Name = "btnCreateBid";
            btnCreateBid.Size = new Size(135, 29);
            btnCreateBid.TabIndex = 0;
            btnCreateBid.Text = "Create buy order";
            btnCreateBid.UseVisualStyleBackColor = true;
            btnCreateBid.Click += btnCreateBid_Click;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(537, 332);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(125, 27);
            txtAmount.TabIndex = 1;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(537, 257);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(536, 306);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 3;
            label1.Text = "amount";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(536, 234);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 4;
            label2.Text = "price";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(537, 166);
            label3.Name = "label3";
            label3.Size = new Size(42, 20);
            label3.TabIndex = 5;
            label3.Text = "price";
            // 
            // cmbCurrency
            // 
            cmbCurrency.FormattingEnabled = true;
            cmbCurrency.Location = new Point(536, 203);
            cmbCurrency.Name = "cmbCurrency";
            cmbCurrency.Size = new Size(151, 28);
            cmbCurrency.TabIndex = 6;
            cmbCurrency.SelectedIndexChanged += cmbCurrency_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbCurrency);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPrice);
            Controls.Add(txtAmount);
            Controls.Add(btnCreateBid);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCreateBid;
        private TextBox txtAmount;
        private TextBox txtPrice;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox cmbCurrency;
    }
}