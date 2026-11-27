namespace UI
{
    partial class DischargeUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlAdmissionCard;
        private System.Windows.Forms.Label lblAdmissionCaption;
        private System.Windows.Forms.ComboBox cmbAdmissions;
        private System.Windows.Forms.Label lblEligibilityBanner;
        private System.Windows.Forms.Panel pnlRecordCard;
        private System.Windows.Forms.Label lblRecordTitle;
        private System.Windows.Forms.Label lblPatientCaption;
        private System.Windows.Forms.Label lblPatientValue;
        private System.Windows.Forms.Label lblRoomCaption;
        private System.Windows.Forms.Label lblRoomValue;
        private System.Windows.Forms.Label lblDiagnosisCaption;
        private System.Windows.Forms.Label lblDiagnosisValue;
        private System.Windows.Forms.Label lblAdmitDateCaption;
        private System.Windows.Forms.Label lblAdmitDateValue;
        private System.Windows.Forms.Label lblDischargeDateCaption;
        private System.Windows.Forms.Label lblDischargeDateValue;
        private System.Windows.Forms.Label lblBilledCaption;
        private System.Windows.Forms.Label lblBilledValue;
        private System.Windows.Forms.Label lblPaidCaption;
        private System.Windows.Forms.Label lblPaidValue;
        private System.Windows.Forms.Label lblBalanceCaption;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Label lblChargesGridTitle;
        private System.Windows.Forms.DataGridView dgvCharges;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeTotal;
        private System.Windows.Forms.Label lblPaymentsGridTitle;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentStatus;
        private System.Windows.Forms.Panel pnlActionCard;
        private System.Windows.Forms.Label lblActionTitle;
        private System.Windows.Forms.Label lblSummaryCaption;
        private System.Windows.Forms.TextBox txtDischargeSummary;
        private System.Windows.Forms.Button btnDischarge;
        private System.Windows.Forms.Button btnPrintRecord;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ErrorProvider errProvider;

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
            System.Windows.Forms.DataGridViewCellStyle headerStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle defaultStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle moneyStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle moneyStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle headerStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle defaultStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dateStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle moneyStyle3 = new System.Windows.Forms.DataGridViewCellStyle();

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            pnlAdmissionCard = new System.Windows.Forms.Panel();
            lblAdmissionCaption = new System.Windows.Forms.Label();
            cmbAdmissions = new System.Windows.Forms.ComboBox();
            lblEligibilityBanner = new System.Windows.Forms.Label();
            pnlRecordCard = new System.Windows.Forms.Panel();
            lblRecordTitle = new System.Windows.Forms.Label();
            lblPatientCaption = new System.Windows.Forms.Label();
            lblPatientValue = new System.Windows.Forms.Label();
            lblRoomCaption = new System.Windows.Forms.Label();
            lblRoomValue = new System.Windows.Forms.Label();
            lblDiagnosisCaption = new System.Windows.Forms.Label();
            lblDiagnosisValue = new System.Windows.Forms.Label();
            lblAdmitDateCaption = new System.Windows.Forms.Label();
            lblAdmitDateValue = new System.Windows.Forms.Label();
            lblDischargeDateCaption = new System.Windows.Forms.Label();
            lblDischargeDateValue = new System.Windows.Forms.Label();
            lblBilledCaption = new System.Windows.Forms.Label();
            lblBilledValue = new System.Windows.Forms.Label();
            lblPaidCaption = new System.Windows.Forms.Label();
            lblPaidValue = new System.Windows.Forms.Label();
            lblBalanceCaption = new System.Windows.Forms.Label();
            lblBalanceValue = new System.Windows.Forms.Label();
            lblChargesGridTitle = new System.Windows.Forms.Label();
            dgvCharges = new System.Windows.Forms.DataGridView();
            colChargeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargeTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblPaymentsGridTitle = new System.Windows.Forms.Label();
            dgvPayments = new System.Windows.Forms.DataGridView();
            colReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlActionCard = new System.Windows.Forms.Panel();
            lblActionTitle = new System.Windows.Forms.Label();
            lblSummaryCaption = new System.Windows.Forms.Label();
            txtDischargeSummary = new System.Windows.Forms.TextBox();
            btnDischarge = new System.Windows.Forms.Button();
            btnPrintRecord = new System.Windows.Forms.Button();
            lblMessage = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlAdmissionCard.SuspendLayout();
            pnlRecordCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            pnlActionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTitle.Location = new System.Drawing.Point(35, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(256, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient Discharge";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(37, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(447, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Verify full bill settlement, close the admission, and print the final record.";

            // 
            // pnlAdmissionCard
            // 
            pnlAdmissionCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlAdmissionCard.BackColor = System.Drawing.Color.White;
            pnlAdmissionCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlAdmissionCard.Controls.Add(lblEligibilityBanner);
            pnlAdmissionCard.Controls.Add(cmbAdmissions);
            pnlAdmissionCard.Controls.Add(lblAdmissionCaption);
            pnlAdmissionCard.Location = new System.Drawing.Point(35, 96);
            pnlAdmissionCard.Name = "pnlAdmissionCard";
            pnlAdmissionCard.Size = new System.Drawing.Size(1430, 112);
            pnlAdmissionCard.TabIndex = 2;

            // 
            // lblAdmissionCaption
            // 
            lblAdmissionCaption.AutoSize = true;
            lblAdmissionCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblAdmissionCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblAdmissionCaption.Location = new System.Drawing.Point(20, 12);
            lblAdmissionCaption.Name = "lblAdmissionCaption";
            lblAdmissionCaption.Size = new System.Drawing.Size(73, 17);
            lblAdmissionCaption.TabIndex = 0;
            lblAdmissionCaption.Text = "Admission";

            // 
            // cmbAdmissions
            // 
            cmbAdmissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmbAdmissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAdmissions.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            cmbAdmissions.FormattingEnabled = true;
            cmbAdmissions.Location = new System.Drawing.Point(20, 34);
            cmbAdmissions.Name = "cmbAdmissions";
            cmbAdmissions.Size = new System.Drawing.Size(1388, 27);
            cmbAdmissions.TabIndex = 1;
            cmbAdmissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;

            // 
            // lblEligibilityBanner
            // 
            lblEligibilityBanner.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblEligibilityBanner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblEligibilityBanner.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9);
            lblEligibilityBanner.Location = new System.Drawing.Point(20, 70);
            lblEligibilityBanner.Name = "lblEligibilityBanner";
            lblEligibilityBanner.Size = new System.Drawing.Size(1388, 28);
            lblEligibilityBanner.TabIndex = 2;
            lblEligibilityBanner.Text = "Select an admission to check discharge eligibility.";
            lblEligibilityBanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // pnlRecordCard
            // 
            pnlRecordCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlRecordCard.BackColor = System.Drawing.Color.White;
            pnlRecordCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlRecordCard.Controls.Add(dgvPayments);
            pnlRecordCard.Controls.Add(lblPaymentsGridTitle);
            pnlRecordCard.Controls.Add(dgvCharges);
            pnlRecordCard.Controls.Add(lblChargesGridTitle);
            pnlRecordCard.Controls.Add(lblBalanceValue);
            pnlRecordCard.Controls.Add(lblBalanceCaption);
            pnlRecordCard.Controls.Add(lblPaidValue);
            pnlRecordCard.Controls.Add(lblPaidCaption);
            pnlRecordCard.Controls.Add(lblBilledValue);
            pnlRecordCard.Controls.Add(lblBilledCaption);
            pnlRecordCard.Controls.Add(lblDischargeDateValue);
            pnlRecordCard.Controls.Add(lblDischargeDateCaption);
            pnlRecordCard.Controls.Add(lblAdmitDateValue);
            pnlRecordCard.Controls.Add(lblAdmitDateCaption);
            pnlRecordCard.Controls.Add(lblDiagnosisValue);
            pnlRecordCard.Controls.Add(lblDiagnosisCaption);
            pnlRecordCard.Controls.Add(lblRoomValue);
            pnlRecordCard.Controls.Add(lblRoomCaption);
            pnlRecordCard.Controls.Add(lblPatientValue);
            pnlRecordCard.Controls.Add(lblPatientCaption);
            pnlRecordCard.Controls.Add(lblRecordTitle);
            pnlRecordCard.Location = new System.Drawing.Point(35, 224);
            pnlRecordCard.Name = "pnlRecordCard";
            pnlRecordCard.Size = new System.Drawing.Size(960, 712);
            pnlRecordCard.TabIndex = 3;

            // 
            // lblRecordTitle
            // 
            lblRecordTitle.AutoSize = true;
            lblRecordTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblRecordTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblRecordTitle.Location = new System.Drawing.Point(20, 14);
            lblRecordTitle.Name = "lblRecordTitle";
            lblRecordTitle.Size = new System.Drawing.Size(171, 21);
            lblRecordTitle.TabIndex = 0;
            lblRecordTitle.Text = "Admission & Final Record";

            // 
            // lblPatientCaption
            // 
            lblPatientCaption.AutoSize = true;
            lblPatientCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPatientCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPatientCaption.Location = new System.Drawing.Point(20, 48);
            lblPatientCaption.Name = "lblPatientCaption";
            lblPatientCaption.Size = new System.Drawing.Size(47, 15);
            lblPatientCaption.TabIndex = 1;
            lblPatientCaption.Text = "Patient";

            // 
            // lblPatientValue
            // 
            lblPatientValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblPatientValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblPatientValue.Location = new System.Drawing.Point(20, 67);
            lblPatientValue.Name = "lblPatientValue";
            lblPatientValue.Size = new System.Drawing.Size(340, 22);
            lblPatientValue.TabIndex = 2;
            lblPatientValue.Text = "-";

            // 
            // lblRoomCaption
            // 
            lblRoomCaption.AutoSize = true;
            lblRoomCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblRoomCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblRoomCaption.Location = new System.Drawing.Point(380, 48);
            lblRoomCaption.Name = "lblRoomCaption";
            lblRoomCaption.Size = new System.Drawing.Size(41, 15);
            lblRoomCaption.TabIndex = 3;
            lblRoomCaption.Text = "Room";

            // 
            // lblRoomValue
            // 
            lblRoomValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblRoomValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblRoomValue.Location = new System.Drawing.Point(380, 67);
            lblRoomValue.Name = "lblRoomValue";
            lblRoomValue.Size = new System.Drawing.Size(200, 22);
            lblRoomValue.TabIndex = 4;
            lblRoomValue.Text = "-";

            // 
            // lblDiagnosisCaption
            // 
            lblDiagnosisCaption.AutoSize = true;
            lblDiagnosisCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDiagnosisCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDiagnosisCaption.Location = new System.Drawing.Point(600, 48);
            lblDiagnosisCaption.Name = "lblDiagnosisCaption";
            lblDiagnosisCaption.Size = new System.Drawing.Size(63, 15);
            lblDiagnosisCaption.TabIndex = 5;
            lblDiagnosisCaption.Text = "Diagnosis";

            // 
            // lblDiagnosisValue
            // 
            lblDiagnosisValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblDiagnosisValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDiagnosisValue.Location = new System.Drawing.Point(600, 67);
            lblDiagnosisValue.Name = "lblDiagnosisValue";
            lblDiagnosisValue.Size = new System.Drawing.Size(338, 22);
            lblDiagnosisValue.TabIndex = 6;
            lblDiagnosisValue.Text = "-";

            // 
            // lblAdmitDateCaption
            // 
            lblAdmitDateCaption.AutoSize = true;
            lblAdmitDateCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblAdmitDateCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblAdmitDateCaption.Location = new System.Drawing.Point(20, 102);
            lblAdmitDateCaption.Name = "lblAdmitDateCaption";
            lblAdmitDateCaption.Size = new System.Drawing.Size(68, 15);
            lblAdmitDateCaption.TabIndex = 7;
            lblAdmitDateCaption.Text = "Admit date";

            // 
            // lblAdmitDateValue
            // 
            lblAdmitDateValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblAdmitDateValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblAdmitDateValue.Location = new System.Drawing.Point(20, 121);
            lblAdmitDateValue.Name = "lblAdmitDateValue";
            lblAdmitDateValue.Size = new System.Drawing.Size(210, 22);
            lblAdmitDateValue.TabIndex = 8;
            lblAdmitDateValue.Text = "-";

            // 
            // lblDischargeDateCaption
            // 
            lblDischargeDateCaption.AutoSize = true;
            lblDischargeDateCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDischargeDateCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDischargeDateCaption.Location = new System.Drawing.Point(250, 102);
            lblDischargeDateCaption.Name = "lblDischargeDateCaption";
            lblDischargeDateCaption.Size = new System.Drawing.Size(91, 15);
            lblDischargeDateCaption.TabIndex = 9;
            lblDischargeDateCaption.Text = "Discharge date";

            // 
            // lblDischargeDateValue
            // 
            lblDischargeDateValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDischargeDateValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDischargeDateValue.Location = new System.Drawing.Point(250, 121);
            lblDischargeDateValue.Name = "lblDischargeDateValue";
            lblDischargeDateValue.Size = new System.Drawing.Size(210, 22);
            lblDischargeDateValue.TabIndex = 10;
            lblDischargeDateValue.Text = "Not discharged yet";

            // 
            // lblBilledCaption
            // 
            lblBilledCaption.AutoSize = true;
            lblBilledCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblBilledCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBilledCaption.Location = new System.Drawing.Point(480, 102);
            lblBilledCaption.Name = "lblBilledCaption";
            lblBilledCaption.Size = new System.Drawing.Size(70, 15);
            lblBilledCaption.TabIndex = 11;
            lblBilledCaption.Text = "Billed total";

            // 
            // lblBilledValue
            // 
            lblBilledValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblBilledValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBilledValue.Location = new System.Drawing.Point(480, 121);
            lblBilledValue.Name = "lblBilledValue";
            lblBilledValue.Size = new System.Drawing.Size(140, 22);
            lblBilledValue.TabIndex = 12;
            lblBilledValue.Text = "₱0.00";

            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPaidCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPaidCaption.Location = new System.Drawing.Point(640, 102);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new System.Drawing.Size(64, 15);
            lblPaidCaption.TabIndex = 13;
            lblPaidCaption.Text = "Paid total";

            // 
            // lblPaidValue
            // 
            lblPaidValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblPaidValue.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            lblPaidValue.Location = new System.Drawing.Point(640, 121);
            lblPaidValue.Name = "lblPaidValue";
            lblPaidValue.Size = new System.Drawing.Size(140, 22);
            lblPaidValue.TabIndex = 14;
            lblPaidValue.Text = "₱0.00";

            // 
            // lblBalanceCaption
            // 
            lblBalanceCaption.AutoSize = true;
            lblBalanceCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBalanceCaption.Location = new System.Drawing.Point(800, 102);
            lblBalanceCaption.Name = "lblBalanceCaption";
            lblBalanceCaption.Size = new System.Drawing.Size(53, 15);
            lblBalanceCaption.TabIndex = 15;
            lblBalanceCaption.Text = "Balance";

            // 
            // lblBalanceValue
            // 
            lblBalanceValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBalanceValue.Location = new System.Drawing.Point(800, 121);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Size = new System.Drawing.Size(138, 22);
            lblBalanceValue.TabIndex = 16;
            lblBalanceValue.Text = "₱0.00";

            // 
            // lblChargesGridTitle
            // 
            lblChargesGridTitle.AutoSize = true;
            lblChargesGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblChargesGridTitle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblChargesGridTitle.Location = new System.Drawing.Point(20, 160);
            lblChargesGridTitle.Name = "lblChargesGridTitle";
            lblChargesGridTitle.Size = new System.Drawing.Size(113, 19);
            lblChargesGridTitle.TabIndex = 17;
            lblChargesGridTitle.Text = "Billed line items";

            // 
            // dgvCharges
            // 
            dgvCharges.AllowUserToAddRows = false;
            dgvCharges.AllowUserToDeleteRows = false;
            dgvCharges.AllowUserToResizeRows = false;
            dgvCharges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvCharges.AutoGenerateColumns = false;
            dgvCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvCharges.BackgroundColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvCharges.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCharges.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle1.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle1.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            dgvCharges.ColumnHeadersDefaultCellStyle = headerStyle1;
            dgvCharges.ColumnHeadersHeight = 36;
            dgvCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCharges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colChargeDesc,
                colChargeQty,
                colChargePrice,
                colChargeTotal
            });
            defaultStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultStyle1.BackColor = System.Drawing.Color.White;
            defaultStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            defaultStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvCharges.DefaultCellStyle = defaultStyle1;
            dgvCharges.EnableHeadersVisualStyles = false;
            dgvCharges.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvCharges.Location = new System.Drawing.Point(20, 184);
            dgvCharges.MultiSelect = false;
            dgvCharges.Name = "dgvCharges";
            dgvCharges.ReadOnly = true;
            dgvCharges.RowHeadersVisible = false;
            dgvCharges.RowTemplate.Height = 32;
            dgvCharges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCharges.Size = new System.Drawing.Size(918, 230);
            dgvCharges.TabIndex = 18;

            // 
            // colChargeDesc
            // 
            colChargeDesc.DataPropertyName = "Description";
            colChargeDesc.FillWeight = 52F;
            colChargeDesc.HeaderText = "Description";
            colChargeDesc.Name = "colChargeDesc";
            colChargeDesc.ReadOnly = true;

            // 
            // colChargeQty
            // 
            colChargeQty.DataPropertyName = "Quantity";
            colChargeQty.FillWeight = 14F;
            colChargeQty.HeaderText = "Qty";
            colChargeQty.Name = "colChargeQty";
            colChargeQty.ReadOnly = true;

            // 
            // colChargePrice
            // 
            moneyStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            moneyStyle1.Format = "₱#,##0.00";
            colChargePrice.DataPropertyName = "UnitPrice";
            colChargePrice.DefaultCellStyle = moneyStyle1;
            colChargePrice.FillWeight = 17F;
            colChargePrice.HeaderText = "Unit price";
            colChargePrice.Name = "colChargePrice";
            colChargePrice.ReadOnly = true;

            // 
            // colChargeTotal
            // 
            moneyStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            moneyStyle2.Format = "₱#,##0.00";
            colChargeTotal.DataPropertyName = "LineTotal";
            colChargeTotal.DefaultCellStyle = moneyStyle2;
            colChargeTotal.FillWeight = 17F;
            colChargeTotal.HeaderText = "Line total";
            colChargeTotal.Name = "colChargeTotal";
            colChargeTotal.ReadOnly = true;

            // 
            // lblPaymentsGridTitle
            // 
            lblPaymentsGridTitle.AutoSize = true;
            lblPaymentsGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblPaymentsGridTitle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblPaymentsGridTitle.Location = new System.Drawing.Point(20, 428);
            lblPaymentsGridTitle.Name = "lblPaymentsGridTitle";
            lblPaymentsGridTitle.Size = new System.Drawing.Size(134, 19);
            lblPaymentsGridTitle.TabIndex = 19;
            lblPaymentsGridTitle.Text = "Recorded payments";

            // 
            // dgvPayments
            // 
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.AllowUserToResizeRows = false;
            dgvPayments.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.BackgroundColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPayments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle2.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            dgvPayments.ColumnHeadersDefaultCellStyle = headerStyle2;
            dgvPayments.ColumnHeadersHeight = 36;
            dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colReceiptNo,
                colPaymentDate,
                colPaymentMethod,
                colPaymentAmount,
                colPaymentStatus
            });
            defaultStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultStyle2.BackColor = System.Drawing.Color.White;
            defaultStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            defaultStyle2.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvPayments.DefaultCellStyle = defaultStyle2;
            dgvPayments.EnableHeadersVisualStyles = false;
            dgvPayments.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvPayments.Location = new System.Drawing.Point(20, 452);
            dgvPayments.MultiSelect = false;
            dgvPayments.Name = "dgvPayments";
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.RowTemplate.Height = 32;
            dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.Size = new System.Drawing.Size(918, 238);
            dgvPayments.TabIndex = 20;

            // 
            // colReceiptNo
            // 
            colReceiptNo.DataPropertyName = "ReceiptNo";
            colReceiptNo.FillWeight = 24F;
            colReceiptNo.HeaderText = "Receipt #";
            colReceiptNo.Name = "colReceiptNo";
            colReceiptNo.ReadOnly = true;

            // 
            // colPaymentDate
            // 
            dateStyle.Format = "yyyy-MM-dd HH:mm";
            colPaymentDate.DataPropertyName = "PaymentDate";
            colPaymentDate.DefaultCellStyle = dateStyle;
            colPaymentDate.FillWeight = 22F;
            colPaymentDate.HeaderText = "Date";
            colPaymentDate.Name = "colPaymentDate";
            colPaymentDate.ReadOnly = true;

            // 
            // colPaymentMethod
            // 
            colPaymentMethod.DataPropertyName = "Method";
            colPaymentMethod.FillWeight = 18F;
            colPaymentMethod.HeaderText = "Method";
            colPaymentMethod.Name = "colPaymentMethod";
            colPaymentMethod.ReadOnly = true;

            // 
            // colPaymentAmount
            // 
            moneyStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            moneyStyle3.Format = "₱#,##0.00";
            colPaymentAmount.DataPropertyName = "Amount";
            colPaymentAmount.DefaultCellStyle = moneyStyle3;
            colPaymentAmount.FillWeight = 20F;
            colPaymentAmount.HeaderText = "Amount";
            colPaymentAmount.Name = "colPaymentAmount";
            colPaymentAmount.ReadOnly = true;

            // 
            // colPaymentStatus
            // 
            colPaymentStatus.DataPropertyName = "Status";
            colPaymentStatus.FillWeight = 16F;
            colPaymentStatus.HeaderText = "Status";
            colPaymentStatus.Name = "colPaymentStatus";
            colPaymentStatus.ReadOnly = true;

            // 
            // pnlActionCard
            // 
            pnlActionCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pnlActionCard.BackColor = System.Drawing.Color.White;
            pnlActionCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlActionCard.Controls.Add(lblMessage);
            pnlActionCard.Controls.Add(btnPrintRecord);
            pnlActionCard.Controls.Add(btnDischarge);
            pnlActionCard.Controls.Add(txtDischargeSummary);
            pnlActionCard.Controls.Add(lblSummaryCaption);
            pnlActionCard.Controls.Add(lblActionTitle);
            pnlActionCard.Location = new System.Drawing.Point(1013, 224);
            pnlActionCard.Name = "pnlActionCard";
            pnlActionCard.Size = new System.Drawing.Size(452, 712);
            pnlActionCard.TabIndex = 4;

            // 
            // lblActionTitle
            // 
            lblActionTitle.AutoSize = true;
            lblActionTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblActionTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblActionTitle.Location = new System.Drawing.Point(22, 16);
            lblActionTitle.Name = "lblActionTitle";
            lblActionTitle.Size = new System.Drawing.Size(147, 21);
            lblActionTitle.TabIndex = 0;
            lblActionTitle.Text = "Discharge & Print";

            // 
            // lblSummaryCaption
            // 
            lblSummaryCaption.AutoSize = true;
            lblSummaryCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblSummaryCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSummaryCaption.Location = new System.Drawing.Point(22, 56);
            lblSummaryCaption.Name = "lblSummaryCaption";
            lblSummaryCaption.Size = new System.Drawing.Size(186, 17);
            lblSummaryCaption.TabIndex = 1;
            lblSummaryCaption.Text = "Discharge summary / notes";

            // 
            // txtDischargeSummary
            // 
            txtDischargeSummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtDischargeSummary.Location = new System.Drawing.Point(22, 80);
            txtDischargeSummary.MaxLength = 1000;
            txtDischargeSummary.Multiline = true;
            txtDischargeSummary.Name = "txtDischargeSummary";
            txtDischargeSummary.PlaceholderText = "Optional final clinical notes, home instructions, or follow-up details";
            txtDischargeSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDischargeSummary.Size = new System.Drawing.Size(406, 160);
            txtDischargeSummary.TabIndex = 2;

            // 
            // btnDischarge
            // 
            btnDischarge.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnDischarge.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDischarge.Enabled = false;
            btnDischarge.FlatAppearance.BorderSize = 0;
            btnDischarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDischarge.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btnDischarge.ForeColor = System.Drawing.Color.White;
            btnDischarge.Location = new System.Drawing.Point(22, 260);
            btnDischarge.Name = "btnDischarge";
            btnDischarge.Size = new System.Drawing.Size(406, 44);
            btnDischarge.TabIndex = 3;
            btnDischarge.Text = "Discharge patient";
            btnDischarge.UseVisualStyleBackColor = false;
            btnDischarge.Click += Discharge_Click;

            // 
            // btnPrintRecord
            // 
            btnPrintRecord.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            btnPrintRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPrintRecord.Enabled = false;
            btnPrintRecord.FlatAppearance.BorderSize = 0;
            btnPrintRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrintRecord.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btnPrintRecord.ForeColor = System.Drawing.Color.White;
            btnPrintRecord.Location = new System.Drawing.Point(22, 318);
            btnPrintRecord.Name = "btnPrintRecord";
            btnPrintRecord.Size = new System.Drawing.Size(406, 44);
            btnPrintRecord.TabIndex = 4;
            btnPrintRecord.Text = "Print final record";
            btnPrintRecord.UseVisualStyleBackColor = false;
            btnPrintRecord.Click += PrintRecord_Click;

            // 
            // lblMessage
            // 
            lblMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblMessage.Location = new System.Drawing.Point(22, 382);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(406, 304);
            lblMessage.TabIndex = 5;

            // 
            // errProvider
            // 
            errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errProvider.ContainerControl = this;

            // 
            // DischargeUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            Controls.Add(pnlActionCard);
            Controls.Add(pnlRecordCard);
            Controls.Add(pnlAdmissionCard);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "DischargeUserControl";
            Size = new System.Drawing.Size(1500, 969);
            Load += DischargeUserControl_Load;

            pnlAdmissionCard.ResumeLayout(false);
            pnlAdmissionCard.PerformLayout();
            pnlRecordCard.ResumeLayout(false);
            pnlRecordCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            pnlActionCard.ResumeLayout(false);
            pnlActionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}