namespace DesktopClient.UI
{
    partial class StartupForm
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
            btnCreateCurrency = new Button();
            ListBoxCurrencies = new ListBox();
            tabController = new TabControl();
            tab_Currencies = new TabPage();
            label1 = new Label();
            txtCurrencyName = new TextBox();
            tabPosts = new TabPage();
            btnDeletePost = new Button();
            label_transactions = new Label();
            listTransactions = new ListBox();
            label_Posts = new Label();
            listPosts = new ListBox();
            tab_Accounts = new TabPage();
            lblTotalAmount = new Label();
            txtBoxTotal = new TextBox();
            lblCurrencies = new Label();
            listCurrencyLines = new ListBox();
            listBoxAccounts = new ListBox();
            tabController.SuspendLayout();
            tab_Currencies.SuspendLayout();
            tabPosts.SuspendLayout();
            tab_Accounts.SuspendLayout();
            SuspendLayout();
            // 
            // btnCreateCurrency
            // 
            btnCreateCurrency.Location = new Point(6, 32);
            btnCreateCurrency.Name = "btnCreateCurrency";
            btnCreateCurrency.Size = new Size(171, 29);
            btnCreateCurrency.TabIndex = 0;
            btnCreateCurrency.Text = "Create currency";
            btnCreateCurrency.UseVisualStyleBackColor = true;
            btnCreateCurrency.Click += button1_Click;
            // 
            // ListBoxCurrencies
            // 
            ListBoxCurrencies.FormattingEnabled = true;
            ListBoxCurrencies.ItemHeight = 20;
            ListBoxCurrencies.Location = new Point(387, 23);
            ListBoxCurrencies.Margin = new Padding(2);
            ListBoxCurrencies.Name = "ListBoxCurrencies";
            ListBoxCurrencies.Size = new Size(329, 164);
            ListBoxCurrencies.TabIndex = 1;
            ListBoxCurrencies.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // tabController
            // 
            tabController.Controls.Add(tab_Currencies);
            tabController.Controls.Add(tabPosts);
            tabController.Controls.Add(tab_Accounts);
            tabController.Location = new Point(12, 21);
            tabController.Name = "tabController";
            tabController.SelectedIndex = 0;
            tabController.Size = new Size(776, 417);
            tabController.TabIndex = 2;
            // 
            // tab_Currencies
            // 
            tab_Currencies.Controls.Add(label1);
            tab_Currencies.Controls.Add(txtCurrencyName);
            tab_Currencies.Controls.Add(btnCreateCurrency);
            tab_Currencies.Controls.Add(ListBoxCurrencies);
            tab_Currencies.Location = new Point(4, 29);
            tab_Currencies.Name = "tab_Currencies";
            tab_Currencies.Padding = new Padding(3);
            tab_Currencies.Size = new Size(768, 384);
            tab_Currencies.TabIndex = 0;
            tab_Currencies.Text = "Currencies";
            tab_Currencies.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 82);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 5;
            label1.Text = "Currency Name:";
            // 
            // txtCurrencyName
            // 
            txtCurrencyName.Location = new Point(6, 105);
            txtCurrencyName.Name = "txtCurrencyName";
            txtCurrencyName.Size = new Size(203, 27);
            txtCurrencyName.TabIndex = 4;
            // 
            // tabPosts
            // 
            tabPosts.Controls.Add(btnDeletePost);
            tabPosts.Controls.Add(label_transactions);
            tabPosts.Controls.Add(listTransactions);
            tabPosts.Controls.Add(label_Posts);
            tabPosts.Controls.Add(listPosts);
            tabPosts.Location = new Point(4, 29);
            tabPosts.Name = "tabPosts";
            tabPosts.Padding = new Padding(3);
            tabPosts.Size = new Size(768, 384);
            tabPosts.TabIndex = 1;
            tabPosts.Text = "Posts";
            tabPosts.UseVisualStyleBackColor = true;
            // 
            // btnDeletePost
            // 
            btnDeletePost.Enabled = false;
            btnDeletePost.Location = new Point(318, 64);
            btnDeletePost.Name = "btnDeletePost";
            btnDeletePost.Size = new Size(171, 29);
            btnDeletePost.TabIndex = 6;
            btnDeletePost.Text = "Delete Post";
            btnDeletePost.UseVisualStyleBackColor = true;
            btnDeletePost.Click += btnDeletePost_Click;
            // 
            // label_transactions
            // 
            label_transactions.AutoSize = true;
            label_transactions.Location = new Point(519, 17);
            label_transactions.Name = "label_transactions";
            label_transactions.Size = new Size(90, 20);
            label_transactions.TabIndex = 5;
            label_transactions.Text = "Transactions";
            // 
            // listTransactions
            // 
            listTransactions.FormattingEnabled = true;
            listTransactions.ItemHeight = 20;
            listTransactions.Location = new Point(519, 45);
            listTransactions.Margin = new Padding(2);
            listTransactions.Name = "listTransactions";
            listTransactions.Size = new Size(225, 324);
            listTransactions.TabIndex = 4;
            // 
            // label_Posts
            // 
            label_Posts.AutoSize = true;
            label_Posts.Location = new Point(23, 17);
            label_Posts.Name = "label_Posts";
            label_Posts.Size = new Size(42, 20);
            label_Posts.TabIndex = 3;
            label_Posts.Text = "Posts";
            // 
            // listPosts
            // 
            listPosts.FormattingEnabled = true;
            listPosts.ItemHeight = 20;
            listPosts.Location = new Point(23, 45);
            listPosts.Margin = new Padding(2);
            listPosts.Name = "listPosts";
            listPosts.Size = new Size(277, 324);
            listPosts.TabIndex = 2;
            listPosts.SelectedIndexChanged += listPosts_SelectedIndexChanged;
            // 
            // tab_Accounts
            // 
            tab_Accounts.Controls.Add(lblTotalAmount);
            tab_Accounts.Controls.Add(txtBoxTotal);
            tab_Accounts.Controls.Add(lblCurrencies);
            tab_Accounts.Controls.Add(listCurrencyLines);
            tab_Accounts.Controls.Add(listBoxAccounts);
            tab_Accounts.Location = new Point(4, 29);
            tab_Accounts.Name = "tab_Accounts";
            tab_Accounts.Padding = new Padding(3);
            tab_Accounts.Size = new Size(768, 384);
            tab_Accounts.TabIndex = 2;
            tab_Accounts.Text = "Accounts";
            tab_Accounts.UseVisualStyleBackColor = true;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Location = new Point(517, 213);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(99, 20);
            lblTotalAmount.TabIndex = 8;
            lblTotalAmount.Text = "Total Amount";
            // 
            // txtBoxTotal
            // 
            txtBoxTotal.Location = new Point(517, 236);
            txtBoxTotal.Name = "txtBoxTotal";
            txtBoxTotal.Size = new Size(125, 27);
            txtBoxTotal.TabIndex = 7;
            // 
            // lblCurrencies
            // 
            lblCurrencies.AutoSize = true;
            lblCurrencies.Location = new Point(517, 29);
            lblCurrencies.Name = "lblCurrencies";
            lblCurrencies.Size = new Size(77, 20);
            lblCurrencies.TabIndex = 6;
            lblCurrencies.Text = "Currencies";
            // 
            // listCurrencyLines
            // 
            listCurrencyLines.FormattingEnabled = true;
            listCurrencyLines.ItemHeight = 20;
            listCurrencyLines.Location = new Point(517, 51);
            listCurrencyLines.Margin = new Padding(2);
            listCurrencyLines.Name = "listCurrencyLines";
            listCurrencyLines.Size = new Size(220, 144);
            listCurrencyLines.TabIndex = 4;
            // 
            // listBoxAccounts
            // 
            listBoxAccounts.FormattingEnabled = true;
            listBoxAccounts.ItemHeight = 20;
            listBoxAccounts.Location = new Point(30, 29);
            listBoxAccounts.Margin = new Padding(2);
            listBoxAccounts.Name = "listBoxAccounts";
            listBoxAccounts.Size = new Size(388, 324);
            listBoxAccounts.TabIndex = 3;
            listBoxAccounts.SelectedIndexChanged += listBoxAccounts_SelectedIndexChanged;
            // 
            // StartupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabController);
            Name = "StartupForm";
            Text = "StartupForm";
            tabController.ResumeLayout(false);
            tab_Currencies.ResumeLayout(false);
            tab_Currencies.PerformLayout();
            tabPosts.ResumeLayout(false);
            tabPosts.PerformLayout();
            tab_Accounts.ResumeLayout(false);
            tab_Accounts.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCreateCurrency;
        private ListBox ListBoxCurrencies;
        private TabControl tabController;
        private TabPage tab_Currencies;
        private TabPage tabPosts;
        private TextBox txtCurrencyName;
        private Label label1;
        private ListBox listPosts;
        private Label label_transactions;
        private ListBox listTransactions;
        private Label label_Posts;
        private TabPage tab_Accounts;
        private ListBox listCurrencyLines;
        private ListBox listBoxAccounts;
        private Label lblCurrencies;
        private Label lblTotalAmount;
        private TextBox txtBoxTotal;
        private Button btnDeletePost;
    }
}