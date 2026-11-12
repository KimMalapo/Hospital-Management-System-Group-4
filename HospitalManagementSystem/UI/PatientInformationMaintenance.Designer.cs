namespace UI
{
    partial class PatientInformationMaintenance
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtPatientID = new TextBox();
            txtName = new TextBox();
            txtAge = new TextBox();
            txtAddress = new TextBox();
            cbGender = new ComboBox();
            txtContactNo = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            panelCard = new Panel();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(40, 40, 40);
            label1.Location = new Point(92, 19);
            label1.Name = "label1";
            label1.Size = new Size(259, 32);
            label1.TabIndex = 0;
            label1.Text = "Patient Administration";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(80, 80, 80);
            label2.Location = new Point(64, 69);
            label2.Name = "label2";
            label2.Size = new Size(115, 25);
            label2.TabIndex = 1;
            label2.Text = "Patient ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(80, 80, 80);
            label3.Location = new Point(64, 129);
            label3.Name = "label3";
            label3.Size = new Size(118, 25);
            label3.TabIndex = 2;
            label3.Text = "Full Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(80, 80, 80);
            label4.Location = new Point(64, 189);
            label4.Name = "label4";
            label4.Size = new Size(53, 25);
            label4.TabIndex = 3;
            label4.Text = "Age";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(80, 80, 80);
            label5.Location = new Point(64, 249);
            label5.Name = "label5";
            label5.Size = new Size(89, 25);
            label5.TabIndex = 4;
            label5.Text = "Gender";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(80, 80, 80);
            label6.Location = new Point(64, 320);
            label6.Name = "label6";
            label6.Size = new Size(98, 25);
            label6.TabIndex = 5;
            label6.Text = "Address";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(80, 80, 80);
            label7.Location = new Point(64, 407);
            label7.Name = "label7";
            label7.Size = new Size(136, 25);
            label7.TabIndex = 6;
            label7.Text = "Contact No.";
            // 
            // txtPatientID
            // 
            txtPatientID.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            txtPatientID.Location = new Point(64, 96);
            txtPatientID.Margin = new Padding(3, 2, 3, 2);
            txtPatientID.Name = "txtPatientID";
            txtPatientID.Size = new Size(387, 31);
            txtPatientID.TabIndex = 7;
            // 
            // txtName
            // 
            txtName.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            txtName.Location = new Point(64, 156);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(387, 31);
            txtName.TabIndex = 8;
            // 
            // txtAge
            // 
            txtAge.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            txtAge.Location = new Point(64, 216);
            txtAge.Margin = new Padding(3, 2, 3, 2);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(387, 31);
            txtAge.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            txtAddress.Location = new Point(64, 347);
            txtAddress.Margin = new Padding(3, 2, 3, 2);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(387, 31);
            txtAddress.TabIndex = 10;
            // 
            // cbGender
            // 
            cbGender.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(64, 285);
            cbGender.Margin = new Padding(3, 2, 3, 2);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(387, 33);
            cbGender.TabIndex = 11;
            // 
            // txtContactNo
            // 
            txtContactNo.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            txtContactNo.Location = new Point(64, 434);
            txtContactNo.Margin = new Padding(3, 2, 3, 2);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(387, 31);
            txtContactNo.TabIndex = 12;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(52, 110, 245);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(64, 505);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(125, 32);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(70, 130, 180);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(195, 505);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(125, 32);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "UPDATE";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(217, 83, 79);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(326, 505);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(125, 32);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.FromArgb(192, 255, 255);
            panelCard.Controls.Add(label1);
            panelCard.Controls.Add(btnDelete);
            panelCard.Controls.Add(label2);
            panelCard.Controls.Add(btnUpdate);
            panelCard.Controls.Add(label3);
            panelCard.Controls.Add(btnAdd);
            panelCard.Controls.Add(label4);
            panelCard.Controls.Add(txtContactNo);
            panelCard.Controls.Add(label5);
            panelCard.Controls.Add(cbGender);
            panelCard.Controls.Add(label6);
            panelCard.Controls.Add(txtAddress);
            panelCard.Controls.Add(label7);
            panelCard.Controls.Add(txtAge);
            panelCard.Controls.Add(txtPatientID);
            panelCard.Controls.Add(txtName);
            panelCard.Location = new Point(22, 15);
            panelCard.Margin = new Padding(3, 2, 3, 2);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(507, 563);
            panelCard.TabIndex = 16;
            // 
            // PatientInformationMaintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(541, 589);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "PatientInformationMaintenance";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Patient Information Maintenance";
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtPatientID;
        private TextBox txtName;
        private TextBox txtAge;
        private TextBox txtAddress;
        private ComboBox cbGender;
        private TextBox txtContactNo;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Panel panelCard;
    }
}