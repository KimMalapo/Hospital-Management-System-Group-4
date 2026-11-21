namespace UI
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelCard = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            lblPasswordHelp = new Label();
            lblFormStatus = new Label();
            btnCreate = new Button();
            btnCancel = new Button();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.Controls.Add(lblTitle);
            panelCard.Controls.Add(lblSubtitle);
            panelCard.Controls.Add(lblEmail);
            panelCard.Controls.Add(txtEmail);
            panelCard.Controls.Add(lblPassword);
            panelCard.Controls.Add(txtPassword);
            panelCard.Controls.Add(lblPasswordHelp);
            panelCard.Controls.Add(lblConfirmPassword);
            panelCard.Controls.Add(txtConfirmPassword);
            panelCard.Controls.Add(lblFormStatus);
            panelCard.Controls.Add(btnCreate);
            panelCard.Controls.Add(btnCancel);
            panelCard.Location = new Point(24, 24);
            panelCard.Name = "panelCard";
            panelCard.Padding = new Padding(28);
            panelCard.Size = new Size(480, 540);
            panelCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(32, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(233, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create your account";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(35, 69);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(250, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Register with your work email and a secure password.";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.Location = new Point(35, 112);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(42, 19);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(35, 137);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(410, 27);
            txtEmail.TabIndex = 3;
            txtEmail.PlaceholderText = "name@hospital.com";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.Location = new Point(35, 181);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(68, 19);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(35, 206);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(410, 27);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordHelp
            // 
            lblPasswordHelp.AutoSize = true;
            lblPasswordHelp.Font = new Font("Segoe UI", 9F);
            lblPasswordHelp.ForeColor = Color.FromArgb(100, 116, 139);
            lblPasswordHelp.Location = new Point(35, 239);
            lblPasswordHelp.Name = "lblPasswordHelp";
            lblPasswordHelp.Size = new Size(339, 15);
            lblPasswordHelp.TabIndex = 6;
            lblPasswordHelp.Text = "Use 8+ characters with uppercase, lowercase, a number and a symbol.";
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmPassword.Location = new Point(35, 276);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(119, 19);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Confirm password";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.Location = new Point(35, 301);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(410, 27);
            txtConfirmPassword.TabIndex = 8;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblFormStatus
            // 
            lblFormStatus.AutoSize = false;
            lblFormStatus.Font = new Font("Segoe UI", 9F);
            lblFormStatus.ForeColor = Color.FromArgb(185, 28, 28);
            lblFormStatus.Location = new Point(35, 340);
            lblFormStatus.Name = "lblFormStatus";
            lblFormStatus.Size = new Size(410, 38);
            lblFormStatus.TabIndex = 9;
            lblFormStatus.Text = string.Empty;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(37, 99, 235);
            btnCreate.Cursor = Cursors.Hand;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(35, 405);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(410, 42);
            btnCreate.TabIndex = 10;
            btnCreate.Text = "Create account";
            btnCreate.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.5F);
            btnCancel.Location = new Point(35, 458);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(410, 36);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // RegisterForm
            // 
            AcceptButton = btnCreate;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 251);
            CancelButton = btnCancel;
            ClientSize = new Size(528, 588);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create account";
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelCard;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private Label lblPasswordHelp;
        private Label lblFormStatus;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnCreate;
        private Button btnCancel;
    }
}
