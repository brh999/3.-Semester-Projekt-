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
            SuspendLayout();
            // 
            // btnCreateCurrency
            // 
            btnCreateCurrency.Location = new Point(48, 91);
            btnCreateCurrency.Margin = new Padding(4);
            btnCreateCurrency.Name = "btnCreateCurrency";
            btnCreateCurrency.Size = new Size(214, 36);
            btnCreateCurrency.TabIndex = 0;
            btnCreateCurrency.Text = "Create currency";
            btnCreateCurrency.UseVisualStyleBackColor = true;
            btnCreateCurrency.Click += button1_Click;
            // 
            // ListBoxCurrencies
            // 
            ListBoxCurrencies.FormattingEnabled = true;
            ListBoxCurrencies.ItemHeight = 25;
            ListBoxCurrencies.Location = new Point(339, 190);
            ListBoxCurrencies.Name = "ListBoxCurrencies";
            ListBoxCurrencies.Size = new Size(410, 204);
            ListBoxCurrencies.TabIndex = 1;
            ListBoxCurrencies.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // StartupForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(ListBoxCurrencies);
            Controls.Add(btnCreateCurrency);
            Margin = new Padding(4);
            Name = "StartupForm";
            Text = "StartupForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCreateCurrency;
        private ListBox ListBoxCurrencies;
    }
}