namespace UI
{
    partial class ChargesUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlHeaderCard;
        private System.Windows.Forms.Label lblAdmissionCaption;
        private System.Windows.Forms.ComboBox cmbAdmissions;
        private System.Windows.Forms.Label lblPatientCaption;
        private System.Windows.Forms.Label lblPatientValue;
        private System.Windows.Forms.Label lblRoomCaption;
        private System.Windows.Forms.Label lblRoomValue;
        private System.Windows.Forms.Label lblDiagnosisCaption;
        private System.Windows.Forms.Label lblDiagnosisValue;
        private System.Windows.Forms.Panel pnlReadOnlyBanner;
        private System.Windows.Forms.Label lblReadOnlyMessage;
        private System.Windows.Forms.Panel pnlAddCard;
        private System.Windows.Forms.Label lblServiceCaption;
        private System.Windows.Forms.Label lblQuantityCaption;
        private System.Windows.Forms.ComboBox cmbServices;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddCharge;
        private System.Windows.Forms.Button btnVoidCharge;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvCharges;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargedBy;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Label lblSubtotalCaption;
        private System.Windows.Forms.Label lblSubtotalValue;
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
            System.Windows.Forms.DataGridViewCellStyle headerCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle defaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle unitPriceCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle lineTotalCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            pnlHeaderCard = new System.Windows.Forms.Panel();
            lblAdmissionCaption = new System.Windows.Forms.Label();
            cmbAdmissions = new System.Windows.Forms.ComboBox();
            lblPatientCaption = new System.Windows.Forms.Label();
            lblPatientValue = new System.Windows.Forms.Label();
            lblRoomCaption = new System.Windows.Forms.Label();
            lblRoomValue = new System.Windows.Forms.Label();
            lblDiagnosisCaption = new System.Windows.Forms.Label();
            lblDiagnosisValue = new System.Windows.Forms.Label();
            pnlReadOnlyBanner = new System.Windows.Forms.Panel();
            lblReadOnlyMessage = new System.Windows.Forms.Label();
            pnlAddCard = new System.Windows.Forms.Panel();
            lblServiceCaption = new System.Windows.Forms.Label();
            lblQuantityCaption = new System.Windows.Forms.Label();
            cmbServices = new System.Windows.Forms.ComboBox();
            txtQuantity = new System.Windows.Forms.TextBox();
            btnAddCharge = new System.Windows.Forms.Button();
            btnVoidCharge = new System.Windows.Forms.Button();
            pnlGridCard = new System.Windows.Forms.Panel();
            lblGridTitle = new System.Windows.Forms.Label();
            dgvCharges = new System.Windows.Forms.DataGridView();
            colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblStatusMessage = new System.Windows.Forms.Label();
            lblSubtotalCaption = new System.Windows.Forms.Label();
            lblSubtotalValue = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlHeaderCard.SuspendLayout();
            pnlReadOnlyBanner.SuspendLayout();
            pnlAddCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).BeginInit();
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
            lblTitle.Size = new System.Drawing.Size(218, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient charges";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(37, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(477, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Record services provided to an admitted patient and review the running subtotal.";

            // 
            // pnlHeaderCard
            // 
            pnlHeaderCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlHeaderCard.BackColor = System.Drawing.Color.White;
            pnlHeaderCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlHeaderCard.Controls.Add(lblDiagnosisValue);
            pnlHeaderCard.Controls.Add(lblDiagnosisCaption);
            pnlHeaderCard.Controls.Add(lblRoomValue);
            pnlHeaderCard.Controls.Add(lblRoomCaption);
            pnlHeaderCard.Controls.Add(lblPatientValue);
            pnlHeaderCard.Controls.Add(lblPatientCaption);
            pnlHeaderCard.Controls.Add(cmbAdmissions);
            pnlHeaderCard.Controls.Add(lblAdmissionCaption);
            pnlHeaderCard.Location = new System.Drawing.Point(35, 96);
            pnlHeaderCard.Name = "pnlHeaderCard";
            pnlHeaderCard.Size = new System.Drawing.Size(1430, 118);
            pnlHeaderCard.TabIndex = 2;

            // 
            // lblAdmissionCaption
            // 
            lblAdmissionCaption.AutoSize = true;
            lblAdmissionCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblAdmissionCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblAdmissionCaption.Location = new System.Drawing.Point(20, 14);
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
            cmbAdmissions.Location = new System.Drawing.Point(20, 36);
            cmbAdmissions.Name = "cmbAdmissions";
            cmbAdmissions.Size = new System.Drawing.Size(1388, 27);
            cmbAdmissions.TabIndex = 1;
            cmbAdmissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;

            // 
            // lblPatientCaption
            // 
            lblPatientCaption.AutoSize = true;
            lblPatientCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblPatientCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblPatientCaption.Location = new System.Drawing.Point(20, 78);
            lblPatientCaption.Name = "lblPatientCaption";
            lblPatientCaption.Size = new System.Drawing.Size(59, 19);
            lblPatientCaption.TabIndex = 2;
            lblPatientCaption.Text = "Patient:";

            // 
            // lblPatientValue
            // 
            lblPatientValue.AutoEllipsis = true;
            lblPatientValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblPatientValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblPatientValue.Location = new System.Drawing.Point(85, 78);
            lblPatientValue.Name = "lblPatientValue";
            lblPatientValue.Size = new System.Drawing.Size(420, 21);
            lblPatientValue.TabIndex = 3;
            lblPatientValue.Text = "—";

            // 
            // lblRoomCaption
            // 
            lblRoomCaption.AutoSize = true;
            lblRoomCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblRoomCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblRoomCaption.Location = new System.Drawing.Point(530, 78);
            lblRoomCaption.Name = "lblRoomCaption";
            lblRoomCaption.Size = new System.Drawing.Size(51, 19);
            lblRoomCaption.TabIndex = 4;
            lblRoomCaption.Text = "Room:";

            // 
            // lblRoomValue
            // 
            lblRoomValue.AutoEllipsis = true;
            lblRoomValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblRoomValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblRoomValue.Location = new System.Drawing.Point(587, 78);
            lblRoomValue.Name = "lblRoomValue";
            lblRoomValue.Size = new System.Drawing.Size(220, 21);
            lblRoomValue.TabIndex = 5;
            lblRoomValue.Text = "—";

            // 
            // lblDiagnosisCaption
            // 
            lblDiagnosisCaption.AutoSize = true;
            lblDiagnosisCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblDiagnosisCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDiagnosisCaption.Location = new System.Drawing.Point(835, 78);
            lblDiagnosisCaption.Name = "lblDiagnosisCaption";
            lblDiagnosisCaption.Size = new System.Drawing.Size(76, 19);
            lblDiagnosisCaption.TabIndex = 6;
            lblDiagnosisCaption.Text = "Diagnosis:";

            // 
            // lblDiagnosisValue
            // 
            lblDiagnosisValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblDiagnosisValue.AutoEllipsis = true;
            lblDiagnosisValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDiagnosisValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDiagnosisValue.Location = new System.Drawing.Point(917, 78);
            lblDiagnosisValue.Name = "lblDiagnosisValue";
            lblDiagnosisValue.Size = new System.Drawing.Size(491, 21);
            lblDiagnosisValue.TabIndex = 7;
            lblDiagnosisValue.Text = "—";

            // 
            // pnlReadOnlyBanner
            // 
            pnlReadOnlyBanner.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlReadOnlyBanner.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
            pnlReadOnlyBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlReadOnlyBanner.Controls.Add(lblReadOnlyMessage);
            pnlReadOnlyBanner.Location = new System.Drawing.Point(35, 226);
            pnlReadOnlyBanner.Name = "pnlReadOnlyBanner";
            pnlReadOnlyBanner.Size = new System.Drawing.Size(1430, 42);
            pnlReadOnlyBanner.TabIndex = 3;
            pnlReadOnlyBanner.Visible = false;

            // 
            // lblReadOnlyMessage
            // 
            lblReadOnlyMessage.AutoSize = true;
            lblReadOnlyMessage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblReadOnlyMessage.ForeColor = System.Drawing.Color.FromArgb(146, 64, 14);
            lblReadOnlyMessage.Location = new System.Drawing.Point(18, 11);
            lblReadOnlyMessage.Name = "lblReadOnlyMessage";
            lblReadOnlyMessage.Size = new System.Drawing.Size(465, 19);
            lblReadOnlyMessage.TabIndex = 0;
            lblReadOnlyMessage.Text = "Read-only: This admission is discharged. Charges cannot be modified.";

            // 
            // pnlAddCard
            // 
            pnlAddCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlAddCard.BackColor = System.Drawing.Color.White;
            pnlAddCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlAddCard.Controls.Add(btnVoidCharge);
            pnlAddCard.Controls.Add(btnAddCharge);
            pnlAddCard.Controls.Add(txtQuantity);
            pnlAddCard.Controls.Add(cmbServices);
            pnlAddCard.Controls.Add(lblQuantityCaption);
            pnlAddCard.Controls.Add(lblServiceCaption);
            pnlAddCard.Location = new System.Drawing.Point(35, 280);
            pnlAddCard.Name = "pnlAddCard";
            pnlAddCard.Size = new System.Drawing.Size(1430, 92);
            pnlAddCard.TabIndex = 4;

            // 
            // lblServiceCaption
            // 
            lblServiceCaption.AutoSize = true;
            lblServiceCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblServiceCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblServiceCaption.Location = new System.Drawing.Point(20, 14);
            lblServiceCaption.Name = "lblServiceCaption";
            lblServiceCaption.Size = new System.Drawing.Size(53, 17);
            lblServiceCaption.TabIndex = 0;
            lblServiceCaption.Text = "Service";

            // 
            // lblQuantityCaption
            // 
            lblQuantityCaption.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblQuantityCaption.AutoSize = true;
            lblQuantityCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblQuantityCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblQuantityCaption.Location = new System.Drawing.Point(940, 14);
            lblQuantityCaption.Name = "lblQuantityCaption";
            lblQuantityCaption.Size = new System.Drawing.Size(62, 17);
            lblQuantityCaption.TabIndex = 1;
            lblQuantityCaption.Text = "Quantity";

            // 
            // cmbServices
            // 
            cmbServices.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbServices.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            cmbServices.FormattingEnabled = true;
            cmbServices.Location = new System.Drawing.Point(20, 38);
            cmbServices.Name = "cmbServices";
            cmbServices.Size = new System.Drawing.Size(896, 27);
            cmbServices.TabIndex = 2;

            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtQuantity.Location = new System.Drawing.Point(940, 38);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new System.Drawing.Size(110, 26);
            txtQuantity.TabIndex = 3;
            txtQuantity.Text = "1";

            // 
            // btnAddCharge
            // 
            btnAddCharge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddCharge.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnAddCharge.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddCharge.FlatAppearance.BorderSize = 0;
            btnAddCharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddCharge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnAddCharge.ForeColor = System.Drawing.Color.White;
            btnAddCharge.Location = new System.Drawing.Point(1080, 34);
            btnAddCharge.Name = "btnAddCharge";
            btnAddCharge.Size = new System.Drawing.Size(156, 36);
            btnAddCharge.TabIndex = 4;
            btnAddCharge.Text = "Add charge";
            btnAddCharge.UseVisualStyleBackColor = false;
            btnAddCharge.Click += AddCharge_Click;

            // 
            // btnVoidCharge
            // 
            btnVoidCharge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnVoidCharge.BackColor = System.Drawing.Color.White;
            btnVoidCharge.Cursor = System.Windows.Forms.Cursors.Hand;
            btnVoidCharge.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnVoidCharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnVoidCharge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnVoidCharge.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnVoidCharge.Location = new System.Drawing.Point(1252, 34);
            btnVoidCharge.Name = "btnVoidCharge";
            btnVoidCharge.Size = new System.Drawing.Size(156, 36);
            btnVoidCharge.TabIndex = 5;
            btnVoidCharge.Text = "Void selected";
            btnVoidCharge.UseVisualStyleBackColor = false;
            btnVoidCharge.Click += VoidCharge_Click;

            // 
            // pnlGridCard
            // 
            pnlGridCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlGridCard.BackColor = System.Drawing.Color.White;
            pnlGridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlGridCard.Controls.Add(lblSubtotalValue);
            pnlGridCard.Controls.Add(lblSubtotalCaption);
            pnlGridCard.Controls.Add(lblStatusMessage);
            pnlGridCard.Controls.Add(dgvCharges);
            pnlGridCard.Controls.Add(lblGridTitle);
            pnlGridCard.Location = new System.Drawing.Point(35, 386);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Size = new System.Drawing.Size(1430, 550);
            pnlGridCard.TabIndex = 5;

            // 
            // lblGridTitle
            // 
            lblGridTitle.AutoSize = true;
            lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblGridTitle.Location = new System.Drawing.Point(20, 14);
            lblGridTitle.Name = "lblGridTitle";
            lblGridTitle.Size = new System.Drawing.Size(146, 21);
            lblGridTitle.TabIndex = 0;
            lblGridTitle.Text = "Recorded charges";

            // 
            // dgvCharges
            // 
            dgvCharges.AllowUserToAddRows = false;
            dgvCharges.AllowUserToDeleteRows = false;
            dgvCharges.AllowUserToResizeRows = false;
            dgvCharges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvCharges.AutoGenerateColumns = false;
            dgvCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvCharges.BackgroundColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvCharges.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCharges.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerCellStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            headerCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            headerCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvCharges.ColumnHeadersDefaultCellStyle = headerCellStyle;
            dgvCharges.ColumnHeadersHeight = 40;
            dgvCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCharges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colDescription,
                colQuantity,
                colUnitPrice,
                colLineTotal,
                colChargedBy
            });
            defaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = System.Drawing.Color.White;
            defaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            defaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvCharges.DefaultCellStyle = defaultCellStyle;
            dgvCharges.EnableHeadersVisualStyles = false;
            dgvCharges.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvCharges.Location = new System.Drawing.Point(20, 46);
            dgvCharges.MultiSelect = false;
            dgvCharges.Name = "dgvCharges";
            dgvCharges.ReadOnly = true;
            dgvCharges.RowHeadersVisible = false;
            dgvCharges.RowTemplate.Height = 36;
            dgvCharges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCharges.Size = new System.Drawing.Size(1388, 444);
            dgvCharges.TabIndex = 1;
            dgvCharges.SelectionChanged += ChargesGrid_SelectionChanged;

            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.FillWeight = 38F;
            colDescription.HeaderText = "Service";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;

            // 
            // colQuantity
            // 
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.FillWeight = 10F;
            colQuantity.HeaderText = "Qty";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;

            // 
            // colUnitPrice
            // 
            unitPriceCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            unitPriceCellStyle.Format = "₱#,##0.00";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colUnitPrice.DefaultCellStyle = unitPriceCellStyle;
            colUnitPrice.FillWeight = 16F;
            colUnitPrice.HeaderText = "Unit price";
            colUnitPrice.Name = "colUnitPrice";
            colUnitPrice.ReadOnly = true;

            // 
            // colLineTotal
            // 
            lineTotalCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            lineTotalCellStyle.Format = "₱#,##0.00";
            colLineTotal.DataPropertyName = "LineTotal";
            colLineTotal.DefaultCellStyle = lineTotalCellStyle;
            colLineTotal.FillWeight = 16F;
            colLineTotal.HeaderText = "Line total";
            colLineTotal.Name = "colLineTotal";
            colLineTotal.ReadOnly = true;

            // 
            // colChargedBy
            // 
            colChargedBy.DataPropertyName = "ChargedBy";
            colChargedBy.FillWeight = 20F;
            colChargedBy.HeaderText = "Charged by";
            colChargedBy.Name = "colChargedBy";
            colChargedBy.ReadOnly = true;

            // 
            // lblStatusMessage
            // 
            lblStatusMessage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblStatusMessage.AutoEllipsis = true;
            lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblStatusMessage.Location = new System.Drawing.Point(20, 506);
            lblStatusMessage.Name = "lblStatusMessage";
            lblStatusMessage.Size = new System.Drawing.Size(1080, 24);
            lblStatusMessage.TabIndex = 2;

            // 
            // lblSubtotalCaption
            // 
            lblSubtotalCaption.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            lblSubtotalCaption.AutoSize = true;
            lblSubtotalCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblSubtotalCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSubtotalCaption.Location = new System.Drawing.Point(1160, 505);
            lblSubtotalCaption.Name = "lblSubtotalCaption";
            lblSubtotalCaption.Size = new System.Drawing.Size(78, 21);
            lblSubtotalCaption.TabIndex = 3;
            lblSubtotalCaption.Text = "Subtotal:";

            // 
            // lblSubtotalValue
            // 
            lblSubtotalValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            lblSubtotalValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblSubtotalValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblSubtotalValue.Location = new System.Drawing.Point(1244, 502);
            lblSubtotalValue.Name = "lblSubtotalValue";
            lblSubtotalValue.Size = new System.Drawing.Size(164, 28);
            lblSubtotalValue.TabIndex = 4;
            lblSubtotalValue.Text = "₱0.00";
            lblSubtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // errProvider
            // 
            errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errProvider.ContainerControl = this;

            // 
            // ChargesUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlAddCard);
            Controls.Add(pnlReadOnlyBanner);
            Controls.Add(pnlHeaderCard);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "ChargesUserControl";
            Size = new System.Drawing.Size(1500, 969);
            Load += ChargesUserControl_Load;

            pnlHeaderCard.ResumeLayout(false);
            pnlHeaderCard.PerformLayout();
            pnlReadOnlyBanner.ResumeLayout(false);
            pnlReadOnlyBanner.PerformLayout();
            pnlAddCard.ResumeLayout(false);
            pnlAddCard.PerformLayout();
            pnlGridCard.ResumeLayout(false);
            pnlGridCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}