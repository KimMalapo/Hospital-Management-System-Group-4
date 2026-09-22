namespace UI
{
    partial class LoginForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            this.btnLogin = new Button();
            this.txtUsername = new TextBox();
            txtPassword = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(61, 59);
            label1.Name = "label1";
            label1.Size = new Size(258, 46);
            label1.TabIndex = 0;
            label1.Text = "Hospital System";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 163);
            label2.Name = "label2";
            label2.Size = new Size(103, 28);
            label2.TabIndex = 1;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(12, 216);
            label3.Name = "label3";
            label3.Size = new Size(97, 28);
            label3.TabIndex = 2;
            label3.Text = "Password:";
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new Font("Segoe UI", 12F);
            this.btnLogin.Location = new Point(140, 290);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(124, 50);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 12F);
            this.txtUsername.Location = new Point(129, 160);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(246, 34);
            this.txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(129, 213);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(246, 34);
            txtPassword.TabIndex = 5;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 382);
            Controls.Add(txtPassword);
            Controls.Add(this.txtUsername);
            Controls.Add(this.btnLogin);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private TextBox textBox1;
        private TextBox txtPassword;
    }
}
