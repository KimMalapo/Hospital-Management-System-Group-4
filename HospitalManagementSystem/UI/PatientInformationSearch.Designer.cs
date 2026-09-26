namespace UI
{
    partial class PatientInformationSearch
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
            txtSearch = new TextBox();
            btnSearch = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblPateintID = new Label();
            lblName = new Label();
            lblAge = new Label();
            lblGender = new Label();
            lblContact = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(44, 28);
            label1.Name = "label1";
            label1.Size = new Size(287, 46);
            label1.TabIndex = 0;
            label1.Text = "PATIENT SEARCH";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(44, 113);
            label2.Name = "label2";
            label2.Size = new Size(83, 28);
            label2.TabIndex = 1;
            label2.Text = "Search :";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(139, 110);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(327, 34);
            txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(207, 173);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 39);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "SEARCH";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(44, 269);
            label3.Name = "label3";
            label3.Size = new Size(105, 28);
            label3.TabIndex = 4;
            label3.Text = "Patient ID :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(44, 310);
            label4.Name = "label4";
            label4.Size = new Size(73, 28);
            label4.TabIndex = 5;
            label4.Text = "Name :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(44, 356);
            label5.Name = "label5";
            label5.Size = new Size(56, 28);
            label5.TabIndex = 6;
            label5.Text = "Age :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(44, 397);
            label6.Name = "label6";
            label6.Size = new Size(85, 28);
            label6.TabIndex = 7;
            label6.Text = "Gender :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(44, 436);
            label7.Name = "label7";
            label7.Size = new Size(89, 28);
            label7.TabIndex = 8;
            label7.Text = "Contact :";
            // 
            // lblPateintID
            // 
            lblPateintID.AutoSize = true;
            lblPateintID.Font = new Font("Segoe UI", 12F);
            lblPateintID.Location = new Point(174, 269);
            lblPateintID.Name = "lblPateintID";
            lblPateintID.Size = new Size(0, 28);
            lblPateintID.TabIndex = 9;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F);
            lblName.Location = new Point(174, 310);
            lblName.Name = "lblName";
            lblName.Size = new Size(0, 28);
            lblName.TabIndex = 10;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Font = new Font("Segoe UI", 12F);
            lblAge.Location = new Point(174, 356);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(0, 28);
            lblAge.TabIndex = 11;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Segoe UI", 12F);
            lblGender.Location = new Point(174, 397);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(0, 28);
            lblGender.TabIndex = 12;
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Font = new Font("Segoe UI", 12F);
            lblContact.Location = new Point(174, 436);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(0, 28);
            lblContact.TabIndex = 13;
            // 
            // PatientInformationSearch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 502);
            Controls.Add(lblContact);
            Controls.Add(lblGender);
            Controls.Add(lblAge);
            Controls.Add(lblName);
            Controls.Add(lblPateintID);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PatientInformationSearch";
            Text = "PatientInformationSearch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblPateintID;
        private Label lblName;
        private Label lblAge;
        private Label lblGender;
        private Label lblContact;
    }
}