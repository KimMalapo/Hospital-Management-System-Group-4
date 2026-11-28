namespace UI
{
    partial class ActiveAdmissionsUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMainHandler;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblSearchCaption;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.ComboBox _status;
        private System.Windows.Forms.GroupBox grpAdmissions;
        private System.Windows.Forms.DataGridView _admissionsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdmissionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdmissionNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoomNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiagnosisName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdmitDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDaysStayed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRunningTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colAddCharges;
        private System.Windows.Forms.DataGridViewButtonColumn colViewBill;
        private System.Windows.Forms.DataGridViewButtonColumn colDischarge;
        private System.Windows.Forms.Panel _emptyStatePanel;
        private System.Windows.Forms.Label _emptyState;
        private System.Windows.Forms.Timer _searchTimer;

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
            System.Windows.Forms.DataGridViewCellStyle dateCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle moneyCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle actionBtnStyle = new System.Windows.Forms.DataGridViewCellStyle();

            pnlMainHandler = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            grpFilter = new System.Windows.Forms.GroupBox();
            lblSearchCaption = new System.Windows.Forms.Label();
            _search = new System.Windows.Forms.TextBox();
            lblStatusCaption = new System.Windows.Forms.Label();
            _status = new System.Windows.Forms.ComboBox();
            grpAdmissions = new System.Windows.Forms.GroupBox();
            _admissionsGrid = new System.Windows.Forms.DataGridView();
            colAdmissionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAdmissionNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRoomNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDiagnosisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAdmitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDaysStayed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRunningTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAddCharges = new System.Windows.Forms.DataGridViewButtonColumn();
            colViewBill = new System.Windows.Forms.DataGridViewButtonColumn();
            colDischarge = new System.Windows.Forms.DataGridViewButtonColumn();
            _emptyStatePanel = new System.Windows.Forms.Panel();
            _emptyState = new System.Windows.Forms.Label();
            _searchTimer = new System.Windows.Forms.Timer(components);

            pnlMainHandler.SuspendLayout();
            grpFilter.SuspendLayout();
            grpAdmissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_admissionsGrid).BeginInit();
            _emptyStatePanel.SuspendLayout();
            SuspendLayout();

            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(lblSubtitle);
            pnlMainHandler.Controls.Add(grpFilter);
            pnlMainHandler.Controls.Add(grpAdmissions);
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
            lblTitle.Size = new System.Drawing.Size(315, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Active Admissions";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Yu Gothic", 10.5F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(38, 66);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(560, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Review current and previous stays, then continue with the next care or billing step.";

            // 
            // grpFilter
            // 
            grpFilter.BackColor = System.Drawing.Color.WhiteSmoke;
            grpFilter.Controls.Add(lblSearchCaption);
            grpFilter.Controls.Add(_search);
            grpFilter.Controls.Add(lblStatusCaption);
            grpFilter.Controls.Add(_status);
            grpFilter.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpFilter.Location = new System.Drawing.Point(35, 100);
            grpFilter.Name = "grpFilter";
            grpFilter.Size = new System.Drawing.Size(1415, 105);
            grpFilter.TabIndex = 2;
            grpFilter.TabStop = false;
            grpFilter.Text = "Filter Admissions";

            // 
            // lblSearchCaption
            // 
            lblSearchCaption.AutoSize = true;
            lblSearchCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblSearchCaption.Location = new System.Drawing.Point(25, 46);
            lblSearchCaption.Name = "lblSearchCaption";
            lblSearchCaption.Size = new System.Drawing.Size(63, 20);
            lblSearchCaption.TabIndex = 0;
            lblSearchCaption.Text = "Search:";

            // 
            // _search
            // 
            _search.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _search.Location = new System.Drawing.Point(100, 42);
            _search.Name = "_search";
            _search.PlaceholderText = "Patient name, room, or admission number";
            _search.Size = new System.Drawing.Size(960, 31);
            _search.TabIndex = 1;
            _search.TextChanged += Search_TextChanged;

            // 
            // lblStatusCaption
            // 
            lblStatusCaption.AutoSize = true;
            lblStatusCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblStatusCaption.Location = new System.Drawing.Point(1090, 46);
            lblStatusCaption.Name = "lblStatusCaption";
            lblStatusCaption.Size = new System.Drawing.Size(57, 20);
            lblStatusCaption.TabIndex = 2;
            lblStatusCaption.Text = "Status:";

            // 
            // _status
            // 
            _status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _status.Font = new System.Drawing.Font("Yu Gothic", 11F);
            _status.FormattingEnabled = true;
            _status.Items.AddRange(new object[]
            {
                "Admitted",
                "Discharged",
                "Cancelled",
                "All statuses"
            });
            _status.Location = new System.Drawing.Point(1160, 42);
            _status.Name = "_status";
            _status.Size = new System.Drawing.Size(230, 27);
            _status.TabIndex = 3;
            _status.SelectedIndexChanged += Status_SelectedIndexChanged;

            // 
            // grpAdmissions
            // 
            grpAdmissions.BackColor = System.Drawing.Color.WhiteSmoke;
            grpAdmissions.Controls.Add(_emptyStatePanel);
            grpAdmissions.Controls.Add(_admissionsGrid);
            grpAdmissions.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpAdmissions.Location = new System.Drawing.Point(35, 225);
            grpAdmissions.Name = "grpAdmissions";
            grpAdmissions.Size = new System.Drawing.Size(1415, 700);
            grpAdmissions.TabIndex = 3;
            grpAdmissions.TabStop = false;
            grpAdmissions.Text = "Admissions List";

            // 
            // _admissionsGrid
            // 
            _admissionsGrid.AllowUserToAddRows = false;
            _admissionsGrid.AllowUserToDeleteRows = false;
            _admissionsGrid.AllowUserToResizeRows = false;
            _admissionsGrid.AutoGenerateColumns = false;
            _admissionsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _admissionsGrid.BackgroundColor = System.Drawing.Color.White;
            _admissionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _admissionsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colAdmissionId,
                colAdmissionNo,
                colPatientName,
                colRoomNo,
                colDiagnosisName,
                colAdmitDate,
                colDaysStayed,
                colRunningTotal,
                colStatus,
                colAddCharges,
                colViewBill,
                colDischarge
            });
            _admissionsGrid.Location = new System.Drawing.Point(25, 42);
            _admissionsGrid.MultiSelect = false;
            _admissionsGrid.Name = "_admissionsGrid";
            _admissionsGrid.ReadOnly = true;
            _admissionsGrid.RowHeadersVisible = false;
            _admissionsGrid.RowTemplate.Height = 38;
            _admissionsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _admissionsGrid.Size = new System.Drawing.Size(1365, 633);
            _admissionsGrid.TabIndex = 0;
            _admissionsGrid.CellContentClick += AdmissionsGrid_CellContentClick;

            // 
            // colAdmissionId
            // 
            colAdmissionId.DataPropertyName = "AdmissionID";
            colAdmissionId.HeaderText = "ID";
            colAdmissionId.Name = "AdmissionID";
            colAdmissionId.ReadOnly = true;
            colAdmissionId.Visible = false;

            // 
            // colAdmissionNo
            // 
            colAdmissionNo.DataPropertyName = "AdmissionNo";
            colAdmissionNo.FillWeight = 105F;
            colAdmissionNo.HeaderText = "Admission No.";
            colAdmissionNo.Name = "colAdmissionNo";
            colAdmissionNo.ReadOnly = true;

            // 
            // colPatientName
            // 
            colPatientName.DataPropertyName = "PatientName";
            colPatientName.FillWeight = 145F;
            colPatientName.HeaderText = "Patient";
            colPatientName.Name = "colPatientName";
            colPatientName.ReadOnly = true;

            // 
            // colRoomNo
            // 
            colRoomNo.DataPropertyName = "RoomNo";
            colRoomNo.FillWeight = 55F;
            colRoomNo.HeaderText = "Room";
            colRoomNo.Name = "colRoomNo";
            colRoomNo.ReadOnly = true;

            // 
            // colDiagnosisName
            // 
            colDiagnosisName.DataPropertyName = "DiagnosisName";
            colDiagnosisName.FillWeight = 105F;
            colDiagnosisName.HeaderText = "Diagnosis";
            colDiagnosisName.Name = "colDiagnosisName";
            colDiagnosisName.ReadOnly = true;

            // 
            // colAdmitDate
            // 
            dateCellStyle.Format = "MMM dd, yyyy";
            colAdmitDate.DataPropertyName = "AdmitDate";
            colAdmitDate.DefaultCellStyle = dateCellStyle;
            colAdmitDate.FillWeight = 85F;
            colAdmitDate.HeaderText = "Admitted";
            colAdmitDate.Name = "colAdmitDate";
            colAdmitDate.ReadOnly = true;

            // 
            // colDaysStayed
            // 
            colDaysStayed.DataPropertyName = "DaysStayed";
            colDaysStayed.FillWeight = 40F;
            colDaysStayed.HeaderText = "Days";
            colDaysStayed.Name = "colDaysStayed";
            colDaysStayed.ReadOnly = true;

            // 
            // colRunningTotal
            // 
            moneyCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            moneyCellStyle.Format = "₱#,##0.00";
            colRunningTotal.DataPropertyName = "RunningTotal";
            colRunningTotal.DefaultCellStyle = moneyCellStyle;
            colRunningTotal.FillWeight = 80F;
            colRunningTotal.HeaderText = "Running Total";
            colRunningTotal.Name = "colRunningTotal";
            colRunningTotal.ReadOnly = true;

            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "Status";
            colStatus.FillWeight = 70F;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;

            // 
            // actionBtnStyle
            // 
            actionBtnStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            actionBtnStyle.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            actionBtnStyle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            actionBtnStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            actionBtnStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);

            // 
            // colAddCharges
            // 
            colAddCharges.DefaultCellStyle = actionBtnStyle;
            colAddCharges.FillWeight = 80F;
            colAddCharges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colAddCharges.HeaderText = "";
            colAddCharges.Name = "AddCharges";
            colAddCharges.ReadOnly = true;
            colAddCharges.Text = "Add charges";
            colAddCharges.UseColumnTextForButtonValue = true;

            // 
            // colViewBill
            // 
            colViewBill.DefaultCellStyle = actionBtnStyle;
            colViewBill.FillWeight = 65F;
            colViewBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colViewBill.HeaderText = "";
            colViewBill.Name = "ViewBill";
            colViewBill.ReadOnly = true;
            colViewBill.Text = "View bill";
            colViewBill.UseColumnTextForButtonValue = true;

            // 
            // colDischarge
            // 
            colDischarge.DefaultCellStyle = actionBtnStyle;
            colDischarge.FillWeight = 70F;
            colDischarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colDischarge.HeaderText = "";
            colDischarge.Name = "Discharge";
            colDischarge.ReadOnly = true;
            colDischarge.Text = "Discharge";
            colDischarge.UseColumnTextForButtonValue = true;

            // 
            // _emptyStatePanel
            // 
            _emptyStatePanel.BackColor = System.Drawing.Color.White;
            _emptyStatePanel.Controls.Add(_emptyState);
            _emptyStatePanel.Location = new System.Drawing.Point(25, 42);
            _emptyStatePanel.Name = "_emptyStatePanel";
            _emptyStatePanel.Size = new System.Drawing.Size(1365, 633);
            _emptyStatePanel.TabIndex = 1;
            _emptyStatePanel.Visible = true;

            // 
            // _emptyState
            // 
            _emptyState.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            _emptyState.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            _emptyState.Location = new System.Drawing.Point(20, 280);
            _emptyState.Name = "_emptyState";
            _emptyState.Size = new System.Drawing.Size(1325, 60);
            _emptyState.TabIndex = 0;
            _emptyState.Text = "No active admissions yet.";
            _emptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // _searchTimer
            // 
            _searchTimer.Interval = 300;
            _searchTimer.Tick += SearchTimer_Tick;

            // 
            // ActiveAdmissionsUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "ActiveAdmissionsUserControl";
            Size = new System.Drawing.Size(1500, 979);
            Load += ActiveAdmissionsUserControl_Load;

            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpFilter.ResumeLayout(false);
            grpFilter.PerformLayout();
            grpAdmissions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_admissionsGrid).EndInit();
            _emptyStatePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}