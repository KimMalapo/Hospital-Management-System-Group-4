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
            label1 = new Label();
            label2 = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 20);
            label1.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(100, 20);
            txtEmail.Size = new Size(250, 30);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 60);
            label2.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(100, 60);
            txtPassword.Size = new Size(250, 30);
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(140, 110);
            btnCreate.Size = new Size(120, 36);
            btnCreate.Text = "Create";
            // 
            // RegisterForm
            // 
            ClientSize = new Size(380, 170);
            Controls.Add(label1);
            Controls.Add(txtEmail);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(btnCreate);
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnCreate;
    }
}
