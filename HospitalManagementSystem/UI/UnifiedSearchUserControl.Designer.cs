namespace UI
{
    partial class UnifiedSearchUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMainHandler = new Panel();
            lblTitle = new Label();
            grpSearchBox = new GroupBox();
            lblSearchPrompt = new Label();
            txtSearchInput = new TextBox();
            btnSearch = new Button();
            grpPatientInfo = new GroupBox();
            lblPatientID = new Label();
            lblValPatientID = new Label();
            lblFullName = new Label();
            lblValFullName = new Label();
            lblAge = new Label();
            lblValAge = new Label();
            lblGender = new Label();
            lblValGender = new Label();
            lblContact = new Label();
            lblValContact = new Label();
            lblAdmissionStatus = new Label();
            lblValAdmissionStatus = new Label();
            grpRoomAssignment = new GroupBox();
            lblRoom = new Label();
            lblValRoom = new Label();
            lblBed = new Label();
            lblValBed = new Label();
            lblRoomStatus = new Label();
            lblValRoomStatus = new Label();
            pnlMainHandler.SuspendLayout();
            grpSearchBox.SuspendLayout();
            grpPatientInfo.SuspendLayout();
            grpRoomAssignment.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpSearchBox);
            pnlMainHandler.Controls.Add(grpPatientInfo);
            pnlMainHandler.Controls.Add(grpRoomAssignment);
            pnlMainHandler.Location = new Point(1, 2);
            pnlMainHandler.Name = "pnlMainHandler";
            pnlMainHandler.Size = new Size(1490, 970);
            pnlMainHandler.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Yu Gothic", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(35, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(424, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient and Room Search";
            // 
            // grpSearchBox
            // 
            grpSearchBox.BackColor = Color.WhiteSmoke;
            grpSearchBox.Controls.Add(lblSearchPrompt);
            grpSearchBox.Controls.Add(txtSearchInput);
            grpSearchBox.Controls.Add(btnSearch);
            grpSearchBox.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpSearchBox.Location = new Point(35, 85);
            grpSearchBox.Name = "grpSearchBox";
            grpSearchBox.Size = new Size(1420, 110);
            grpSearchBox.TabIndex = 1;
            grpSearchBox.TabStop = false;
            grpSearchBox.Text = "Search Criteria";
            // 
            // lblSearchPrompt
            // 
            lblSearchPrompt.AutoSize = true;
            lblSearchPrompt.Font = new Font("Yu Gothic", 11F);
            lblSearchPrompt.Location = new Point(30, 48);
            lblSearchPrompt.Name = "lblSearchPrompt";
            lblSearchPrompt.Size = new Size(162, 20);
            lblSearchPrompt.TabIndex = 0;
            lblSearchPrompt.Text = "Enter Patient ID / Name:";
            // 
            // txtSearchInput
            // 
            txtSearchInput.Font = new Font("Yu Gothic", 12F);
            txtSearchInput.Location = new Point(210, 43);
            txtSearchInput.Name = "txtSearchInput";
            txtSearchInput.Size = new Size(500, 33);
            txtSearchInput.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightSkyBlue;
            btnSearch.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnSearch.Location = new Point(730, 38);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(180, 42);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍 SEARCH";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // grpPatientInfo
            // 
            grpPatientInfo.BackColor = Color.WhiteSmoke;
            grpPatientInfo.Controls.Add(lblPatientID);
            grpPatientInfo.Controls.Add(lblValPatientID);
            grpPatientInfo.Controls.Add(lblFullName);
            grpPatientInfo.Controls.Add(lblValFullName);
            grpPatientInfo.Controls.Add(lblAge);
            grpPatientInfo.Controls.Add(lblValAge);
            grpPatientInfo.Controls.Add(lblGender);
            grpPatientInfo.Controls.Add(lblValGender);
            grpPatientInfo.Controls.Add(lblContact);
            grpPatientInfo.Controls.Add(lblValContact);
            grpPatientInfo.Controls.Add(lblAdmissionStatus);
            grpPatientInfo.Controls.Add(lblValAdmissionStatus);
            grpPatientInfo.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpPatientInfo.Location = new Point(35, 215);
            grpPatientInfo.Name = "grpPatientInfo";
            grpPatientInfo.Size = new Size(690, 640);
            grpPatientInfo.TabIndex = 2;
            grpPatientInfo.TabStop = false;
            grpPatientInfo.Text = "Patient Information";
            // 
            // lblPatientID
            // 
            lblPatientID.AutoSize = true;
            lblPatientID.Font = new Font("Yu Gothic", 11F);
            lblPatientID.Location = new Point(35, 60);
            lblPatientID.Name = "lblPatientID";
            lblPatientID.Size = new Size(84, 20);
            lblPatientID.TabIndex = 0;
            lblPatientID.Text = "Patient ID:";
            // 
            // lblValPatientID
            // 
            lblValPatientID.AutoSize = true;
            lblValPatientID.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValPatientID.ForeColor = Color.DimGray;
            lblValPatientID.Location = new Point(210, 60);
            lblValPatientID.Name = "lblValPatientID";
            lblValPatientID.Size = new Size(31, 20);
            lblValPatientID.TabIndex = 1;
            lblValPatientID.Text = "---";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Yu Gothic", 11F);
            lblFullName.Location = new Point(35, 130);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(83, 20);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Full Name:";
            // 
            // lblValFullName
            // 
            lblValFullName.AutoSize = true;
            lblValFullName.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValFullName.ForeColor = Color.DimGray;
            lblValFullName.Location = new Point(210, 130);
            lblValFullName.Name = "lblValFullName";
            lblValFullName.Size = new Size(31, 20);
            lblValFullName.TabIndex = 3;
            lblValFullName.Text = "---";
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Font = new Font("Yu Gothic", 11F);
            lblAge.Location = new Point(35, 200);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(41, 20);
            lblAge.TabIndex = 4;
            lblAge.Text = "Age:";
            // 
            // lblValAge
            // 
            lblValAge.AutoSize = true;
            lblValAge.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValAge.ForeColor = Color.DimGray;
            lblValAge.Location = new Point(210, 200);
            lblValAge.Name = "lblValAge";
            lblValAge.Size = new Size(31, 20);
            lblValAge.TabIndex = 5;
            lblValAge.Text = "---";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Yu Gothic", 11F);
            lblGender.Location = new Point(35, 270);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(62, 20);
            lblGender.TabIndex = 6;
            lblGender.Text = "Gender:";
            // 
            // lblValGender
            // 
            lblValGender.AutoSize = true;
            lblValGender.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValGender.ForeColor = Color.DimGray;
            lblValGender.Location = new Point(210, 270);
            lblValGender.Name = "lblValGender";
            lblValGender.Size = new Size(31, 20);
            lblValGender.TabIndex = 7;
            lblValGender.Text = "---";
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Font = new Font("Yu Gothic", 11F);
            lblContact.Location = new Point(35, 340);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(92, 20);
            lblContact.TabIndex = 8;
            lblContact.Text = "Contact No:";
            // 
            // lblValContact
            // 
            lblValContact.AutoSize = true;
            lblValContact.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValContact.ForeColor = Color.DimGray;
            lblValContact.Location = new Point(210, 340);
            lblValContact.Name = "lblValContact";
            lblValContact.Size = new Size(31, 20);
            lblValContact.TabIndex = 9;
            lblValContact.Text = "---";
            // 
            // lblAdmissionStatus
            // 
            lblAdmissionStatus.AutoSize = true;
            lblAdmissionStatus.Font = new Font("Yu Gothic", 11F);
            lblAdmissionStatus.Location = new Point(35, 410);
            lblAdmissionStatus.Name = "lblAdmissionStatus";
            lblAdmissionStatus.Size = new Size(133, 20);
            lblAdmissionStatus.TabIndex = 10;
            lblAdmissionStatus.Text = "Admission Status:";
            // 
            // lblValAdmissionStatus
            // 
            lblValAdmissionStatus.AutoSize = true;
            lblValAdmissionStatus.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            lblValAdmissionStatus.ForeColor = Color.DimGray;
            lblValAdmissionStatus.Location = new Point(210, 410);
            lblValAdmissionStatus.Name = "lblValAdmissionStatus";
            lblValAdmissionStatus.Size = new Size(110, 19);
            lblValAdmissionStatus.TabIndex = 11;
            lblAdmissionStatus.Text = "Not Admitted";
            // 
            // grpRoomAssignment
            // 
            grpRoomAssignment.BackColor = Color.WhiteSmoke;
            grpRoomAssignment.Controls.Add(lblRoom);
            grpRoomAssignment.Controls.Add(lblValRoom);
            grpRoomAssignment.Controls.Add(lblBed);
            grpRoomAssignment.Controls.Add(lblValBed);
            grpRoomAssignment.Controls.Add(lblRoomStatus);
            grpRoomAssignment.Controls.Add(lblValRoomStatus);
            grpRoomAssignment.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpRoomAssignment.Location = new Point(745, 215);
            grpRoomAssignment.Name = "grpRoomAssignment";
            grpRoomAssignment.Size = new Size(710, 640);
            grpRoomAssignment.TabIndex = 3;
            grpRoomAssignment.TabStop = false;
            grpRoomAssignment.Text = "Room & Bed Assignment (Side Panel)";
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Font = new Font("Yu Gothic", 11F);
            lblRoom.Location = new Point(35, 60);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(119, 20);
            lblRoom.TabIndex = 0;
            lblRoom.Text = "Assigned Room:";
            // 
            // lblValRoom
            // 
            lblValRoom.AutoSize = true;
            lblValRoom.Font = new Font("Yu Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValRoom.ForeColor = Color.DarkRed;
            lblValRoom.Location = new Point(210, 60);
            lblValRoom.Name = "lblValRoom";
            lblValRoom.Size = new Size(224, 20);
            lblValRoom.TabIndex = 1;
            lblValRoom.Text = "No active room assignment";
            // 
            // lblBed
            // 
            lblBed.AutoSize = true;
            lblBed.Font = new Font("Yu Gothic", 11F);
            lblBed.Location = new Point(35, 130);
            lblBed.Name = "lblBed";
            lblBed.Size = new Size(103, 20);
            lblBed.TabIndex = 2;
            lblBed.Text = "Assigned Bed:";
            // 
            // lblValBed
            // 
            lblValBed.AutoSize = true;
            lblValBed.Font = new Font("Yu Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValBed.ForeColor = Color.DimGray;
            lblValBed.Location = new Point(210, 130);
            lblValBed.Name = "lblValBed";
            lblValBed.Size = new Size(31, 20);
            lblValBed.TabIndex = 3;
            lblValBed.Text = "---";
            // 
            // lblRoomStatus
            // 
            lblRoomStatus.AutoSize = true;
            lblRoomStatus.Font = new Font("Yu Gothic", 11F);
            lblRoomStatus.Location = new Point(35, 200);
            lblRoomStatus.Name = "lblRoomStatus";
            lblRoomStatus.Size = new Size(105, 20);
            lblRoomStatus.TabIndex = 4;
            lblRoomStatus.Text = "Room Status:";
            // 
            // lblValRoomStatus
            // 
            lblValRoomStatus.AutoSize = true;
            lblValRoomStatus.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            lblValRoomStatus.ForeColor = Color.DimGray;
            lblValRoomStatus.Location = new Point(210, 200);
            lblValRoomStatus.Name = "lblValRoomStatus";
            lblValRoomStatus.Size = new Size(36, 19);
            lblValRoomStatus.TabIndex = 5;
            lblValRoomStatus.Text = "---";
            // 
            // UnifiedSearchUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "UnifiedSearchUserControl";
            Size = new Size(1500, 979);
            Load += UnifiedSearchUserControl_Load;
            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpSearchBox.ResumeLayout(false);
            grpSearchBox.PerformLayout();
            grpPatientInfo.ResumeLayout(false);
            grpPatientInfo.PerformLayout();
            grpRoomAssignment.ResumeLayout(false);
            grpRoomAssignment.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainHandler;
        private Label lblTitle;
        private GroupBox grpSearchBox;
        private Label lblSearchPrompt;
        private TextBox txtSearchInput;
        private Button btnSearch;
        private GroupBox grpPatientInfo;
        private Label lblPatientID;
        private Label lblValPatientID;
        private Label lblFullName;
        private Label lblValFullName;
        private Label lblAge;
        private Label lblValAge;
        private Label lblGender;
        private Label lblValGender;
        private Label lblContact;
        private Label lblValContact;
        private Label lblAdmissionStatus;
        private Label lblValAdmissionStatus;
        private GroupBox grpRoomAssignment;
        private Label lblRoom;
        private Label lblValRoom;
        private Label lblBed;
        private Label lblValBed;
        private Label lblRoomStatus;
        private Label lblValRoomStatus;
    }
}