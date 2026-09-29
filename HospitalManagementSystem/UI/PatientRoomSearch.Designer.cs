namespace UI
{
    partial class PatientRoomSearch
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
            lblPatient = new Label();
            lblRoom = new Label();
            lblBed = new Label();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(118, 30);
            label1.Name = "label1";
            label1.Size = new Size(287, 46);
            label1.TabIndex = 1;
            label1.Text = "PATIENT SEARCH";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(41, 114);
            label2.Name = "label2";
            label2.Size = new Size(83, 28);
            label2.TabIndex = 2;
            label2.Text = "Search :";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(141, 111);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(327, 34);
            txtSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(199, 175);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 39);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "SEARCH";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(41, 309);
            label3.Name = "label3";
            label3.Size = new Size(73, 28);
            label3.TabIndex = 5;
            label3.Text = "Room :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(41, 266);
            label4.Name = "label4";
            label4.Size = new Size(81, 28);
            label4.TabIndex = 6;
            label4.Text = "Patient :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(41, 356);
            label5.Name = "label5";
            label5.Size = new Size(54, 28);
            label5.TabIndex = 7;
            label5.Text = "Bed :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(41, 401);
            label6.Name = "label6";
            label6.Size = new Size(74, 28);
            label6.TabIndex = 8;
            label6.Text = "Status :";
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.Font = new Font("Segoe UI", 12F);
            lblPatient.Location = new Point(173, 266);
            lblPatient.Name = "lblPatient";
            lblPatient.Size = new Size(0, 28);
            lblPatient.TabIndex = 9;
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Font = new Font("Segoe UI", 12F);
            lblRoom.Location = new Point(173, 309);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(0, 28);
            lblRoom.TabIndex = 10;
            // 
            // lblBed
            // 
            lblBed.AutoSize = true;
            lblBed.Font = new Font("Segoe UI", 12F);
            lblBed.Location = new Point(173, 356);
            lblBed.Name = "lblBed";
            lblBed.Size = new Size(0, 28);
            lblBed.TabIndex = 11;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.Location = new Point(173, 401);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 28);
            lblStatus.TabIndex = 12;
            // 
            // PatientRoomSearch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 460);
            Controls.Add(lblStatus);
            Controls.Add(lblBed);
            Controls.Add(lblRoom);
            Controls.Add(lblPatient);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PatientRoomSearch";
            Text = "Patient Room Search";
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
        private Label lblPatient;
        private Label lblRoom;
        private Label lblBed;
        private Label lblStatus;
    }
}