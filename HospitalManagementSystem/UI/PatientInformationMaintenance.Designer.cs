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
            this.btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(73, 35);
            label1.Name = "label1";
            label1.Size = new Size(321, 46);
            label1.TabIndex = 0;
            label1.Text = "Patient Information";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(11, 108);
            label2.Name = "label2";
            label2.Size = new Size(105, 28);
            label2.TabIndex = 1;
            label2.Text = "Patient ID :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(12, 152);
            label3.Name = "label3";
            label3.Size = new Size(73, 28);
            label3.TabIndex = 2;
            label3.Text = "Name :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(11, 198);
            label4.Name = "label4";
            label4.Size = new Size(56, 28);
            label4.TabIndex = 3;
            label4.Text = "Age :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(11, 242);
            label5.Name = "label5";
            label5.Size = new Size(85, 28);
            label5.TabIndex = 4;
            label5.Text = "Gender :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(11, 287);
            label6.Name = "label6";
            label6.Size = new Size(91, 28);
            label6.TabIndex = 5;
            label6.Text = "Address :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(11, 331);
            label7.Name = "label7";
            label7.Size = new Size(125, 28);
            label7.TabIndex = 6;
            label7.Text = "Contact No. :";
            // 
            // txtPatientID
            // 
            txtPatientID.Font = new Font("Segoe UI", 12F);
            txtPatientID.Location = new Point(200, 105);
            txtPatientID.Name = "txtPatientID";
            txtPatientID.Size = new Size(251, 34);
            txtPatientID.TabIndex = 7;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 12F);
            txtName.Location = new Point(200, 149);
            txtName.Name = "txtName";
            txtName.Size = new Size(251, 34);
            txtName.TabIndex = 8;
            // 
            // txtAge
            // 
            txtAge.Font = new Font("Segoe UI", 12F);
            txtAge.Location = new Point(200, 195);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(251, 34);
            txtAge.TabIndex = 9;
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 12F);
            txtAddress.Location = new Point(200, 284);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(251, 34);
            txtAddress.TabIndex = 10;
            // 
            // cbGender
            // 
            cbGender.Font = new Font("Segoe UI", 12F);
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(200, 239);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(251, 36);
            cbGender.TabIndex = 11;
            // 
            // txtContactNo
            // 
            txtContactNo.Font = new Font("Segoe UI", 12F);
            txtContactNo.Location = new Point(200, 328);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(251, 34);
            txtContactNo.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAdd.Location = new Point(57, 412);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(103, 39);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate.Location = new Point(189, 412);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(103, 39);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "UPDATE";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(319, 412);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(103, 39);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // PatientInformationMaintenance
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 497);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(this.btnAdd);
            Controls.Add(txtContactNo);
            Controls.Add(cbGender);
            Controls.Add(txtAddress);
            Controls.Add(txtAge);
            Controls.Add(txtName);
            Controls.Add(txtPatientID);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PatientInformationMaintenance";
            Text = "PatientInformationMaintenance";
            ResumeLayout(false);
            PerformLayout();
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
        private Button button1;
        private Button btnUpdate;
        private Button btnDelete;
        private MaskedTextBox maskedTextBox1;
    }
}