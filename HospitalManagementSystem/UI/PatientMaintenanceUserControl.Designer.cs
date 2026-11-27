namespace UI
{
    partial class PatientMaintenanceUserControl
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
            grpPatientDetails = new GroupBox();
            lblPatientID = new Label();
            txtPatientID = new TextBox();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblAge = new Label();
            numAge = new NumericUpDown();
            lblGender = new Label();
            cmbGender = new ComboBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblContactNo = new Label();
            txtContactNo = new TextBox();
            lblAdmissionStatus = new Label();
            lblAdmissionStatusVal = new Label();
            btnClear = new Button();
            lblAdmissionHistory = new Label();
            dgvAdmissionHistory = new DataGridView();
            colHistAdmissionID = new DataGridViewTextBoxColumn();
            colHistAdmissionNo = new DataGridViewTextBoxColumn();
            colHistRoomNo = new DataGridViewTextBoxColumn();
            colHistAdmitDate = new DataGridViewTextBoxColumn();
            colHistStatus = new DataGridViewTextBoxColumn();
            grpList = new GroupBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvPatients = new DataGridView();
            colPatientID = new DataGridViewTextBoxColumn();
            colFirstName = new DataGridViewTextBoxColumn();
            colLastName = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colContact = new DataGridViewTextBoxColumn();
            colAdmissionStatus = new DataGridViewTextBoxColumn();
            colAssignedRoom = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRestore = new Button();
            chkShowDeleted = new CheckBox();
            pnlMainHandler.SuspendLayout();
            grpPatientDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAdmissionHistory).BeginInit();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            SuspendLayout();
            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = SystemColors.ActiveBorder;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpPatientDetails);
            pnlMainHandler.Controls.Add(grpList);
            pnlMainHandler.Controls.Add(btnAdd);
            pnlMainHandler.Controls.Add(btnUpdate);
            pnlMainHandler.Controls.Add(btnDelete);
            pnlMainHandler.Controls.Add(btnRestore);
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
            lblTitle.Size = new Size(535, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient Information Maintenance";
            // 
            // grpPatientDetails
            // 
            grpPatientDetails.BackColor = Color.WhiteSmoke;
            grpPatientDetails.Controls.Add(lblPatientID);
            grpPatientDetails.Controls.Add(txtPatientID);
            grpPatientDetails.Controls.Add(lblFirstName);
            grpPatientDetails.Controls.Add(txtFirstName);
            grpPatientDetails.Controls.Add(lblLastName);
            grpPatientDetails.Controls.Add(txtLastName);
            grpPatientDetails.Controls.Add(lblAge);
            grpPatientDetails.Controls.Add(numAge);
            grpPatientDetails.Controls.Add(lblGender);
            grpPatientDetails.Controls.Add(cmbGender);
            grpPatientDetails.Controls.Add(lblAddress);
            grpPatientDetails.Controls.Add(txtAddress);
            grpPatientDetails.Controls.Add(lblContactNo);
            grpPatientDetails.Controls.Add(txtContactNo);
            grpPatientDetails.Controls.Add(lblAdmissionStatus);
            grpPatientDetails.Controls.Add(lblAdmissionStatusVal);
            grpPatientDetails.Controls.Add(btnClear);
            grpPatientDetails.Controls.Add(lblAdmissionHistory);
            grpPatientDetails.Controls.Add(dgvAdmissionHistory);
            grpPatientDetails.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpPatientDetails.Location = new Point(35, 85);
            grpPatientDetails.Name = "grpPatientDetails";
            grpPatientDetails.Size = new Size(480, 750);
            grpPatientDetails.TabIndex = 1;
            grpPatientDetails.TabStop = false;
            grpPatientDetails.Text = "Patient Details";
            // 
            // lblPatientID
            // 
            lblPatientID.AutoSize = true;
            lblPatientID.Font = new Font("Yu Gothic", 11F);
            lblPatientID.Location = new Point(25, 40);
            lblPatientID.Name = "lblPatientID";
            lblPatientID.Size = new Size(81, 20);
            lblPatientID.TabIndex = 0;
            lblPatientID.Text = "Patient ID:";
            // 
            // txtPatientID
            // 
            txtPatientID.Font = new Font("Yu Gothic", 11F);
            txtPatientID.Location = new Point(150, 35);
            txtPatientID.Name = "txtPatientID";
            txtPatientID.Size = new Size(300, 31);
            txtPatientID.TabIndex = 1;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Yu Gothic", 11F);
            lblFirstName.Location = new Point(25, 85);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(88, 20);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            txtFirstName.Font = new Font("Yu Gothic", 11F);
            txtFirstName.Location = new Point(150, 80);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(300, 31);
            txtFirstName.TabIndex = 3;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Yu Gothic", 11F);
            lblLastName.Location = new Point(25, 130);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(87, 20);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            txtLastName.Font = new Font("Yu Gothic", 11F);
            txtLastName.Location = new Point(150, 125);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(300, 31);
            txtLastName.TabIndex = 5;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Font = new Font("Yu Gothic", 11F);
            lblAge.Location = new Point(25, 175);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(39, 20);
            lblAge.TabIndex = 6;
            lblAge.Text = "Age:";
            // 
            // numAge
            // 
            numAge.Font = new Font("Yu Gothic", 11F);
            numAge.Location = new Point(150, 170);
            numAge.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            numAge.Name = "numAge";
            numAge.Size = new Size(120, 31);
            numAge.TabIndex = 7;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Yu Gothic", 11F);
            lblGender.Location = new Point(25, 220);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(63, 20);
            lblGender.TabIndex = 8;
            lblGender.Text = "Gender:";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Yu Gothic", 11F);
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(150, 215);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(300, 28);
            cmbGender.TabIndex = 9;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Yu Gothic", 11F);
            lblAddress.Location = new Point(25, 265);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(70, 20);
            lblAddress.TabIndex = 10;
            lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Yu Gothic", 11F);
            txtAddress.Location = new Point(150, 260);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(300, 74);
            txtAddress.TabIndex = 11;
            // 
            // lblContactNo
            // 
            lblContactNo.AutoSize = true;
            lblContactNo.Font = new Font("Yu Gothic", 11F);
            lblContactNo.Location = new Point(25, 350);
            lblContactNo.Name = "lblContactNo";
            lblContactNo.Size = new Size(89, 20);
            lblContactNo.TabIndex = 12;
            lblContactNo.Text = "Contact No:";
            // 
            // txtContactNo
            // 
            txtContactNo.Font = new Font("Yu Gothic", 11F);
            txtContactNo.Location = new Point(150, 345);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.Size = new Size(300, 31);
            txtContactNo.TabIndex = 13;
            // 
            // lblAdmissionStatus
            // 
            lblAdmissionStatus.AutoSize = true;
            lblAdmissionStatus.Font = new Font("Yu Gothic", 11F);
            lblAdmissionStatus.Location = new Point(25, 392);
            lblAdmissionStatus.Name = "lblAdmissionStatus";
            lblAdmissionStatus.Size = new Size(134, 20);
            lblAdmissionStatus.TabIndex = 14;
            lblAdmissionStatus.Text = "Admission Status:";
            // 
            // lblAdmissionStatusVal
            // 
            lblAdmissionStatusVal.AutoSize = true;
            lblAdmissionStatusVal.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            lblAdmissionStatusVal.ForeColor = Color.DimGray;
            lblAdmissionStatusVal.Location = new Point(165, 392);
            lblAdmissionStatusVal.Name = "lblAdmissionStatusVal";
            lblAdmissionStatusVal.Size = new Size(112, 19);
            lblAdmissionStatusVal.TabIndex = 15;
            lblAdmissionStatusVal.Text = "Not Admitted";
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            btnClear.Location = new Point(150, 424);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(300, 38);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear Form";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblAdmissionHistory
            // 
            lblAdmissionHistory.AutoSize = true;
            lblAdmissionHistory.Font = new Font("Yu Gothic", 11F, FontStyle.Bold);
            lblAdmissionHistory.Location = new Point(25, 474);
            lblAdmissionHistory.Name = "lblAdmissionHistory";
            lblAdmissionHistory.Size = new Size(216, 20);
            lblAdmissionHistory.TabIndex = 17;
            lblAdmissionHistory.Text = "Patient Admission History:";
            // 
            // dgvAdmissionHistory
            // 
            dgvAdmissionHistory.AllowUserToAddRows = false;
            dgvAdmissionHistory.AllowUserToDeleteRows = false;
            dgvAdmissionHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdmissionHistory.BackgroundColor = Color.White;
            dgvAdmissionHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdmissionHistory.Columns.AddRange(new DataGridViewColumn[] { colHistAdmissionID, colHistAdmissionNo, colHistRoomNo, colHistAdmitDate, colHistStatus });
            dgvAdmissionHistory.Location = new Point(25, 500);
            dgvAdmissionHistory.MultiSelect = false;
            dgvAdmissionHistory.Name = "dgvAdmissionHistory";
            dgvAdmissionHistory.ReadOnly = true;
            dgvAdmissionHistory.RowHeadersVisible = false;
            dgvAdmissionHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdmissionHistory.Size = new Size(425, 232);
            dgvAdmissionHistory.TabIndex = 18;
            dgvAdmissionHistory.SelectionChanged += dgvAdmissionHistory_SelectionChanged;
            dgvAdmissionHistory.CellDoubleClick += dgvAdmissionHistory_CellDoubleClick;
            // 
            // colHistAdmissionID
            // 
            colHistAdmissionID.HeaderText = "ID";
            colHistAdmissionID.Name = "colHistAdmissionID";
            colHistAdmissionID.ReadOnly = true;
            colHistAdmissionID.Visible = false;
            // 
            // colHistAdmissionNo
            // 
            colHistAdmissionNo.FillWeight = 32F;
            colHistAdmissionNo.HeaderText = "Admission #";
            colHistAdmissionNo.Name = "colHistAdmissionNo";
            colHistAdmissionNo.ReadOnly = true;
            // 
            // colHistRoomNo
            // 
            colHistRoomNo.FillWeight = 18F;
            colHistRoomNo.HeaderText = "Room";
            colHistRoomNo.Name = "colHistRoomNo";
            colHistRoomNo.ReadOnly = true;
            // 
            // colHistAdmitDate
            // 
            colHistAdmitDate.FillWeight = 26F;
            colHistAdmitDate.HeaderText = "Admitted";
            colHistAdmitDate.Name = "colHistAdmitDate";
            colHistAdmitDate.ReadOnly = true;
            // 
            // colHistStatus
            // 
            colHistStatus.FillWeight = 24F;
            colHistStatus.HeaderText = "Status";
            colHistStatus.Name = "colHistStatus";
            colHistStatus.ReadOnly = true;
            // 
            // grpList
            // 
            grpList.BackColor = Color.WhiteSmoke;
            grpList.Controls.Add(chkShowDeleted);
            grpList.Controls.Add(txtSearch);
            grpList.Controls.Add(lblSearch);
            grpList.Controls.Add(dgvPatients);
            grpList.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpList.Location = new Point(521, 85);
            grpList.Name = "grpList";
            grpList.Size = new Size(966, 750);
            grpList.TabIndex = 2;
            grpList.TabStop = false;
            grpList.Text = "Patient Records";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Yu Gothic", 11F);
            txtSearch.Location = new Point(610, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(280, 31);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Yu Gothic", 11F);
            lblSearch.Location = new Point(485, 40);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(113, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search Patient:";
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPatients.Columns.AddRange(new DataGridViewColumn[] { colPatientID, colFirstName, colLastName, colAge, colGender, colAddress, colContact, colAdmissionStatus, colAssignedRoom });
            dgvPatients.Location = new Point(0, 90);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(960, 645);
            dgvPatients.TabIndex = 2;
            dgvPatients.SelectionChanged += dgvPatients_SelectionChanged;
            // 
            // colPatientID
            // 
            colPatientID.HeaderText = "Patient ID";
            colPatientID.Name = "colPatientID";
            colPatientID.ReadOnly = true;
            // 
            // colFirstName
            // 
            colFirstName.HeaderText = "First Name";
            colFirstName.Name = "colFirstName";
            colFirstName.ReadOnly = true;
            // 
            // colLastName
            // 
            colLastName.HeaderText = "Last Name";
            colLastName.Name = "colLastName";
            colLastName.ReadOnly = true;
            // 
            // colAge
            // 
            colAge.HeaderText = "Age";
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.HeaderText = "Gender";
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            // 
            // colAddress
            // 
            colAddress.HeaderText = "Address";
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            // 
            // colContact
            // 
            colContact.HeaderText = "Contact No";
            colContact.Name = "colContact";
            colContact.ReadOnly = true;
            // 
            // colAdmissionStatus
            // 
            colAdmissionStatus.HeaderText = "Status";
            colAdmissionStatus.Name = "colAdmissionStatus";
            colAdmissionStatus.ReadOnly = true;
            // 
            // colAssignedRoom
            // 
            colAssignedRoom.HeaderText = "Room/Bed";
            colAssignedRoom.Name = "colAssignedRoom";
            colAssignedRoom.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(35, 855);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(250, 60);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "+ Add Patient";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightSkyBlue;
            btnUpdate.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnUpdate.Location = new Point(300, 855);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(250, 60);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update Record";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.MistyRose;
            btnDelete.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(565, 855);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(250, 60);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Deactivate Patient";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRestore
            // 
            btnRestore.BackColor = Color.Honeydew;
            btnRestore.Enabled = false;
            btnRestore.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnRestore.Location = new Point(830, 855);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(250, 60);
            btnRestore.TabIndex = 6;
            btnRestore.Text = "Restore Patient";
            btnRestore.UseVisualStyleBackColor = false;
            btnRestore.Click += btnRestore_Click;
            // 
            // chkShowDeleted
            // 
            chkShowDeleted.AutoSize = true;
            chkShowDeleted.Font = new Font("Yu Gothic", 11F);
            chkShowDeleted.Location = new Point(20, 38);
            chkShowDeleted.Name = "chkShowDeleted";
            chkShowDeleted.Size = new Size(200, 24);
            chkShowDeleted.TabIndex = 2;
            chkShowDeleted.Text = "Show deactivated patients";
            chkShowDeleted.UseVisualStyleBackColor = true;
            chkShowDeleted.CheckedChanged += chkShowDeleted_CheckedChanged;
            // 
            // PatientMaintenanceUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "PatientMaintenanceUserControl";
            Size = new Size(1500, 979);
            Load += PatientMaintenanceUserControl_Load;
            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpPatientDetails.ResumeLayout(false);
            grpPatientDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAdmissionHistory).EndInit();
            grpList.ResumeLayout(false);
            grpList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainHandler;
        private Label lblTitle;
        private GroupBox grpPatientDetails;
        private Label lblPatientID;
        private TextBox txtPatientID;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblAge;
        private NumericUpDown numAge;
        private Label lblGender;
        private ComboBox cmbGender;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblContactNo;
        private TextBox txtContactNo;
        private Label lblAdmissionStatus;
        private Label lblAdmissionStatusVal;
        private Button btnClear;
        private Label lblAdmissionHistory;
        private DataGridView dgvAdmissionHistory;
        private DataGridViewTextBoxColumn colHistAdmissionID;
        private DataGridViewTextBoxColumn colHistAdmissionNo;
        private DataGridViewTextBoxColumn colHistRoomNo;
        private DataGridViewTextBoxColumn colHistAdmitDate;
        private DataGridViewTextBoxColumn colHistStatus;
        private GroupBox grpList;
        private Label lblSearch;
        private TextBox txtSearch;
        private DataGridView dgvPatients;
        private DataGridViewTextBoxColumn colPatientID;
        private DataGridViewTextBoxColumn colFirstName;
        private DataGridViewTextBoxColumn colLastName;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewTextBoxColumn colGender;
        private DataGridViewTextBoxColumn colAddress;
        private DataGridViewTextBoxColumn colContact;
        private DataGridViewTextBoxColumn colAdmissionStatus;
        private DataGridViewTextBoxColumn colAssignedRoom;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRestore;
        private CheckBox chkShowDeleted;
    }
}