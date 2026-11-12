namespace UI
{
    partial class LoginForm
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
            lblTitle = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnToggle = new Button();
            linkRegister = new LinkLabel();
            panelCard = new Panel();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblTitle.Location = new Point(135, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(188, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hospital System";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10.5F);
            lblUsername.ForeColor = Color.FromArgb(80, 80, 80);
            lblUsername.Location = new Point(50, 95);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 19);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10.5F);
            lblPassword.ForeColor = Color.FromArgb(80, 80, 80);
            lblPassword.Location = new Point(50, 165);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11.5F);
            txtUsername.Location = new Point(50, 119);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(292, 28);
            txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11.5F);
            txtPassword.Location = new Point(50, 189);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(253, 28);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(52, 110, 245);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 246);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(292, 38);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnToggle
            // 
            btnToggle.BackColor = Color.FromArgb(240, 240, 240);
            btnToggle.Cursor = Cursors.Hand;
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.FlatStyle = FlatStyle.Flat;
            btnToggle.Font = new Font("Segoe UI", 10F);
            btnToggle.Location = new Point(306, 189);
            btnToggle.Margin = new Padding(0);
            btnToggle.Name = "btnToggle";
            btnToggle.Size = new Size(36, 28);
            btnToggle.TabIndex = 5;
            btnToggle.Text = "👁";
            btnToggle.UseVisualStyleBackColor = false;
            btnToggle.Click += btnToggle_Click;
            // 
            // linkRegister
            // 
            linkRegister.ActiveLinkColor = Color.FromArgb(30, 80, 200);
            linkRegister.AutoSize = true;
            linkRegister.Font = new Font("Segoe UI", 9.5F);
            linkRegister.LinkColor = Color.FromArgb(52, 110, 245);
            linkRegister.Location = new Point(148, 302);
            linkRegister.Name = "linkRegister";
            linkRegister.Size = new Size(95, 17);
            linkRegister.TabIndex = 7;
            linkRegister.TabStop = true;
            linkRegister.Text = "Create account";
            linkRegister.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.Controls.Add(lblTitle);
            panelCard.Controls.Add(linkRegister);
            panelCard.Controls.Add(lblUsername);
            panelCard.Controls.Add(btnLogin);
            panelCard.Controls.Add(lblPassword);
            panelCard.Controls.Add(btnToggle);
            panelCard.Controls.Add(txtUsername);
            panelCard.Controls.Add(txtPassword);
            panelCard.Location = new Point(35, 30);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(392, 355);
            panelCard.TabIndex = 8;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(462, 415);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hospital System - Login";
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnToggle;
        private LinkLabel linkRegister;
        private Panel panelCard;
    }
}