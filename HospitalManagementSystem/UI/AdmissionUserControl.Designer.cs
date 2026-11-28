namespace UI
{
    partial class AdmissionUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMainHandler;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.GroupBox grpPatient;
        private System.Windows.Forms.Label lblPatientSearchCaption;
        private System.Windows.Forms.TextBox _patientSearch;
        private System.Windows.Forms.DataGridView _patientsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.Label _selectedPatient;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblRoomCaption;
        private System.Windows.Forms.ComboBox _rooms;
        private System.Windows.Forms.Label lblDepartmentCaption;
        private System.Windows.Forms.ComboBox _departments;
        private System.Windows.Forms.Label lblDiagnosisCaption;
        private System.Windows.Forms.ComboBox _diagnoses;
        private System.Windows.Forms.Label lblPhysicianCaption;
        private System.Windows.Forms.TextBox _physician;
        private System.Windows.Forms.Label lblReasonCaption;
        private System.Windows.Forms.TextBox _reason;
        private System.Windows.Forms.Label lblAdmitDateCaption;
        private System.Windows.Forms.DateTimePicker _admitDate;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdmit;
        private System.Windows.Forms.ErrorProvider _errors;
        private System.Windows.Forms.Timer _patientSearchTimer;

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
            components = new System.ComponentModel.Container();
            pnlMainHandler = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            grpPatient = new System.Windows.Forms.GroupBox();
            lblPatientSearchCaption = new System.Windows.Forms.Label();
            _patientSearch = new System.Windows.Forms.TextBox();
            _patientsGrid = new System.Windows.Forms.DataGridView();
            colPatientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _selectedPatient = new System.Windows.Forms.Label();
            grpDetails = new System.Windows.Forms.GroupBox();
            lblRoomCaption = new System.Windows.Forms.Label();
            _rooms = new System.Windows.Forms.ComboBox();
            lblDepartmentCaption = new System.Windows.Forms.Label();
            _departments = new System.Windows.Forms.ComboBox();
            lblDiagnosisCaption = new System.Windows.Forms.Label();
            _diagnoses = new System.Windows.Forms.ComboBox();
            lblPhysicianCaption = new System.Windows.Forms.Label();
            _physician = new System.Windows.Forms.TextBox();
            lblReasonCaption = new System.Windows.Forms.Label();
            _reason = new System.Windows.Forms.TextBox();
            lblAdmitDateCaption = new System.Windows.Forms.Label();
            _admitDate = new System.Windows.Forms.DateTimePicker();
            _status = new System.Windows.Forms.Label();
            btnClear = new System.Windows.Forms.Button();
            btnAdmit = new System.Windows.Forms.Button();
            _errors = new System.Windows.Forms.ErrorProvider(components);
            _patientSearchTimer = new System.Windows.Forms.Timer(components);

            pnlMainHandler.SuspendLayout();
            grpPatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_patientsGrid).BeginInit();
            grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_errors).BeginInit();
            SuspendLayout();

            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(lblSubtitle);
            pnlMainHandler.Controls.Add(grpPatient);
            pnlMainHandler.Controls.Add(grpDetails);
            pnlMainHandler.Location = new System.Drawing.Point(1, 2);
            pnlMainHandler.Name = "pnlMainHandler";
            pnlMainHandler.Size = new System.Drawing.Size(1490, 970);
            pnlMainHandler.TabIndex = 0;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Yu Gothic", 24F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(35, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(270, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Admit a Patient";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Yu Gothic", 10.5F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(38, 66);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(530, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Choose a registered patient, assign a room, and record the admission details.";

            // 
            // grpPatient
            // 
            grpPatient.BackColor = System.Drawing.Color.WhiteSmoke;
            grpPatient.Controls.Add(lblPatientSearchCaption);
            grpPatient.Controls.Add(_patientSearch);
            grpPatient.Controls.Add(_patientsGrid);
            grpPatient.Controls.Add(_selectedPatient);
            grpPatient.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpPatient.Location = new System.Drawing.Point(35, 100);
            grpPatient.Name = "grpPatient";
            grpPatient.Size = new System.Drawing.Size(640, 825);
            grpPatient.TabIndex = 2;
            grpPatient.TabStop = false;
            grpPatient.Text = "01. Select Patient";

            // 
            // lblPatientSearchCaption
            // 
            lblPatientSearchCaption.AutoSize = true;
            lblPatientSearchCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblPatientSearchCaption.Location = new System.Drawing.Point(25, 42);
            lblPatientSearchCaption.Name = "lblPatientSearchCaption";
            lblPatientSearchCaption.Size = new System.Drawing.Size(120, 20);
            lblPatientSearchCaption.TabIndex = 0;
            lblPatientSearchCaption.Text = "Patient Search:";

            // 
            // _patientSearch
            // 
            _patientSearch.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _patientSearch.Location = new System.Drawing.Point(25, 70);
            _patientSearch.Name = "_patientSearch";
            _patientSearch.PlaceholderText = "Enter patient ID or name";
            _patientSearch.Size = new System.Drawing.Size(590, 31);
            _patientSearch.TabIndex = 1;
            _patientSearch.TextChanged += PatientSearch_TextChanged;

            // 
            // _patientsGrid
            // 
            _patientsGrid.AllowUserToAddRows = false;
            _patientsGrid.AllowUserToDeleteRows = false;
            _patientsGrid.AllowUserToResizeRows = false;
            _patientsGrid.AutoGenerateColumns = false;
            _patientsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _patientsGrid.BackgroundColor = System.Drawing.Color.White;
            _patientsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _patientsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colPatientId,
                colPatientName,
                colAge
            });
            _patientsGrid.Location = new System.Drawing.Point(25, 118);
            _patientsGrid.MultiSelect = false;
            _patientsGrid.Name = "_patientsGrid";
            _patientsGrid.ReadOnly = true;
            _patientsGrid.RowHeadersVisible = false;
            _patientsGrid.RowTemplate.Height = 36;
            _patientsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _patientsGrid.Size = new System.Drawing.Size(590, 625);
            _patientsGrid.TabIndex = 2;
            _patientsGrid.SelectionChanged += PatientsGrid_SelectionChanged;

            // 
            // colPatientId
            // 
            colPatientId.DataPropertyName = "PatientID";
            colPatientId.FillWeight = 22F;
            colPatientId.HeaderText = "Patient ID";
            colPatientId.Name = "PatientID";
            colPatientId.ReadOnly = true;

            // 
            // colPatientName
            // 
            colPatientName.DataPropertyName = "PatientName";
            colPatientName.FillWeight = 58F;
            colPatientName.HeaderText = "Patient";
            colPatientName.Name = "PatientName";
            colPatientName.ReadOnly = true;

            // 
            // colAge
            // 
            colAge.DataPropertyName = "Age";
            colAge.FillWeight = 20F;
            colAge.HeaderText = "Age";
            colAge.Name = "Age";
            colAge.ReadOnly = true;

            // 
            // _selectedPatient
            // 
            _selectedPatient.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            _selectedPatient.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            _selectedPatient.ForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            _selectedPatient.Location = new System.Drawing.Point(25, 760);
            _selectedPatient.Name = "_selectedPatient";
            _selectedPatient.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            _selectedPatient.Size = new System.Drawing.Size(590, 44);
            _selectedPatient.TabIndex = 3;
            _selectedPatient.Text = "No patient selected";
            _selectedPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // grpDetails
            // 
            grpDetails.BackColor = System.Drawing.Color.WhiteSmoke;
            grpDetails.Controls.Add(lblRoomCaption);
            grpDetails.Controls.Add(_rooms);
            grpDetails.Controls.Add(lblDepartmentCaption);
            grpDetails.Controls.Add(_departments);
            grpDetails.Controls.Add(lblDiagnosisCaption);
            grpDetails.Controls.Add(_diagnoses);
            grpDetails.Controls.Add(lblPhysicianCaption);
            grpDetails.Controls.Add(_physician);
            grpDetails.Controls.Add(lblReasonCaption);
            grpDetails.Controls.Add(_reason);
            grpDetails.Controls.Add(lblAdmitDateCaption);
            grpDetails.Controls.Add(_admitDate);
            grpDetails.Controls.Add(_status);
            grpDetails.Controls.Add(btnClear);
            grpDetails.Controls.Add(btnAdmit);
            grpDetails.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpDetails.Location = new System.Drawing.Point(695, 100);
            grpDetails.Name = "grpDetails";
            grpDetails.Size = new System.Drawing.Size(755, 825);
            grpDetails.TabIndex = 3;
            grpDetails.TabStop = false;
            grpDetails.Text = "02. Admission Details (* Required)";

            // 
            // lblRoomCaption
            // 
            lblRoomCaption.AutoSize = true;
            lblRoomCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblRoomCaption.Location = new System.Drawing.Point(25, 48);
            lblRoomCaption.Name = "lblRoomCaption";
            lblRoomCaption.Size = new System.Drawing.Size(134, 20);
            lblRoomCaption.TabIndex = 0;
            lblRoomCaption.Text = "Available Room *:";

            // 
            // _rooms
            // 
            _rooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _rooms.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _rooms.FormattingEnabled = true;
            _rooms.Location = new System.Drawing.Point(210, 44);
            _rooms.Name = "_rooms";
            _rooms.Size = new System.Drawing.Size(515, 27);
            _rooms.TabIndex = 1;

            // 
            // lblDepartmentCaption
            // 
            lblDepartmentCaption.AutoSize = true;
            lblDepartmentCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblDepartmentCaption.Location = new System.Drawing.Point(25, 104);
            lblDepartmentCaption.Name = "lblDepartmentCaption";
            lblDepartmentCaption.Size = new System.Drawing.Size(108, 20);
            lblDepartmentCaption.TabIndex = 2;
            lblDepartmentCaption.Text = "Department *:";

            // 
            // _departments
            // 
            _departments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _departments.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _departments.FormattingEnabled = true;
            _departments.Location = new System.Drawing.Point(210, 100);
            _departments.Name = "_departments";
            _departments.Size = new System.Drawing.Size(515, 27);
            _departments.TabIndex = 3;

            // 
            // lblDiagnosisCaption
            // 
            lblDiagnosisCaption.AutoSize = true;
            lblDiagnosisCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblDiagnosisCaption.Location = new System.Drawing.Point(25, 160);
            lblDiagnosisCaption.Name = "lblDiagnosisCaption";
            lblDiagnosisCaption.Size = new System.Drawing.Size(93, 20);
            lblDiagnosisCaption.TabIndex = 4;
            lblDiagnosisCaption.Text = "Diagnosis *:";

            // 
            // _diagnoses
            // 
            _diagnoses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _diagnoses.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _diagnoses.FormattingEnabled = true;
            _diagnoses.Location = new System.Drawing.Point(210, 156);
            _diagnoses.Name = "_diagnoses";
            _diagnoses.Size = new System.Drawing.Size(515, 27);
            _diagnoses.TabIndex = 5;

            // 
            // lblPhysicianCaption
            // 
            lblPhysicianCaption.AutoSize = true;
            lblPhysicianCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblPhysicianCaption.Location = new System.Drawing.Point(25, 218);
            lblPhysicianCaption.Name = "lblPhysicianCaption";
            lblPhysicianCaption.Size = new System.Drawing.Size(92, 20);
            lblPhysicianCaption.TabIndex = 6;
            lblPhysicianCaption.Text = "Physician *:";

            // 
            // _physician
            // 
            _physician.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _physician.Location = new System.Drawing.Point(210, 214);
            _physician.MaxLength = 200;
            _physician.Name = "_physician";
            _physician.PlaceholderText = "Enter physician name";
            _physician.Size = new System.Drawing.Size(515, 31);
            _physician.TabIndex = 7;

            // 
            // lblReasonCaption
            // 
            lblReasonCaption.AutoSize = true;
            lblReasonCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblReasonCaption.Location = new System.Drawing.Point(25, 278);
            lblReasonCaption.Name = "lblReasonCaption";
            lblReasonCaption.Size = new System.Drawing.Size(68, 20);
            lblReasonCaption.TabIndex = 8;
            lblReasonCaption.Text = "Reason:";

            // 
            // _reason
            // 
            _reason.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _reason.Location = new System.Drawing.Point(210, 274);
            _reason.MaxLength = 500;
            _reason.Multiline = true;
            _reason.Name = "_reason";
            _reason.PlaceholderText = "Add context for this admission (optional)";
            _reason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            _reason.Size = new System.Drawing.Size(515, 130);
            _reason.TabIndex = 9;

            // 
            // lblAdmitDateCaption
            // 
            lblAdmitDateCaption.AutoSize = true;
            lblAdmitDateCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblAdmitDateCaption.Location = new System.Drawing.Point(25, 434);
            lblAdmitDateCaption.Name = "lblAdmitDateCaption";
            lblAdmitDateCaption.Size = new System.Drawing.Size(134, 20);
            lblAdmitDateCaption.TabIndex = 10;
            lblAdmitDateCaption.Text = "Admission Date *:";

            // 
            // _admitDate
            // 
            _admitDate.CustomFormat = "MMM dd, yyyy  |  hh:mm tt";
            _admitDate.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _admitDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            _admitDate.Location = new System.Drawing.Point(210, 430);
            _admitDate.Name = "_admitDate";
            _admitDate.Size = new System.Drawing.Size(515, 31);
            _admitDate.TabIndex = 11;

            // 
            // _status
            // 
            _status.Font = new System.Drawing.Font("Yu Gothic", 10.5F);
            _status.ForeColor = System.Drawing.Color.Firebrick;
            _status.Location = new System.Drawing.Point(25, 490);
            _status.Name = "_status";
            _status.Size = new System.Drawing.Size(700, 120);
            _status.TabIndex = 12;

            // 
            // btnClear
            // 
            btnClear.BackColor = System.Drawing.Color.White;
            btnClear.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            btnClear.Location = new System.Drawing.Point(345, 745);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(160, 50);
            btnClear.TabIndex = 13;
            btnClear.Text = "Clear Form";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += ClearButton_Click;

            // 
            // btnAdmit
            // 
            btnAdmit.BackColor = System.Drawing.Color.LightGreen;
            btnAdmit.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            btnAdmit.Location = new System.Drawing.Point(520, 745);
            btnAdmit.Name = "btnAdmit";
            btnAdmit.Size = new System.Drawing.Size(205, 50);
            btnAdmit.TabIndex = 14;
            btnAdmit.Text = "Create Admission";
            btnAdmit.UseVisualStyleBackColor = false;
            btnAdmit.Click += AdmitButton_Click;

            // 
            // _errors
            // 
            _errors.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            _errors.ContainerControl = this;

            // 
            // _patientSearchTimer
            // 
            _patientSearchTimer.Interval = 300;
            _patientSearchTimer.Tick += PatientSearchTimer_Tick;

            // 
            // AdmissionUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "AdmissionUserControl";
            Size = new System.Drawing.Size(1500, 979);
            Load += AdmissionUserControl_Load;

            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpPatient.ResumeLayout(false);
            grpPatient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_patientsGrid).EndInit();
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_errors).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}