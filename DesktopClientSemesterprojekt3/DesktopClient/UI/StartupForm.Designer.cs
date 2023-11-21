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
            SuspendLayout();
            // 
            // btnCreateCurrency
            // 
            btnCreateCurrency.Location = new Point(38, 73);
            btnCreateCurrency.Name = "btnCreateCurrency";
            btnCreateCurrency.Size = new Size(171, 29);
            btnCreateCurrency.TabIndex = 0;
            btnCreateCurrency.Text = "Create currency";
            btnCreateCurrency.UseVisualStyleBackColor = true;
            btnCreateCurrency.Click += button1_Click;
            // 
            // StartupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCreateCurrency);
            Name = "StartupForm";
            Text = "StartupForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCreateCurrency;
    }
}