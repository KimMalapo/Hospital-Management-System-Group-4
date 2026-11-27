namespace UI
{
    partial class BillingUserControl
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
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel pnlItemsCard;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.Panel pnlSummaryCard;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblBillStatus;
        private System.Windows.Forms.Label lblSubtotalCaption;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscountCaption;
        private System.Windows.Forms.Label lblDiscountTotal;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPaidCaption;
        private System.Windows.Forms.Label lblPaid;
        private System.Windows.Forms.Label lblBalanceCaption;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblDiscountHeading;
        private System.Windows.Forms.Label lblDiscountAmountCaption;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDiscountReasonCaption;
        private System.Windows.Forms.TextBox txtDiscountReason;
        private System.Windows.Forms.Button btnApplyDiscount;
        private System.Windows.Forms.Button btnGoToPayment;
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
            System.Windows.Forms.DataGridViewCellStyle headerCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle defaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle unitPriceCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle amountCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            pnlAdmissionCard = new System.Windows.Forms.Panel();
            lblAdmissionCaption = new System.Windows.Forms.Label();
            cmbAdmissions = new System.Windows.Forms.ComboBox();
            btnGenerate = new System.Windows.Forms.Button();
            pnlItemsCard = new System.Windows.Forms.Panel();
            lblItemsTitle = new System.Windows.Forms.Label();
            dgvItems = new System.Windows.Forms.DataGridView();
            colItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlSummaryCard = new System.Windows.Forms.Panel();
            lblSummaryTitle = new System.Windows.Forms.Label();
            lblStatusCaption = new System.Windows.Forms.Label();
            lblBillStatus = new System.Windows.Forms.Label();
            lblSubtotalCaption = new System.Windows.Forms.Label();
            lblSubtotal = new System.Windows.Forms.Label();
            lblDiscountCaption = new System.Windows.Forms.Label();
            lblDiscountTotal = new System.Windows.Forms.Label();
            lblTotalCaption = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();
            lblPaidCaption = new System.Windows.Forms.Label();
            lblPaid = new System.Windows.Forms.Label();
            lblBalanceCaption = new System.Windows.Forms.Label();
            lblBalance = new System.Windows.Forms.Label();
            lblDiscountHeading = new System.Windows.Forms.Label();
            lblDiscountAmountCaption = new System.Windows.Forms.Label();
            txtDiscount = new System.Windows.Forms.TextBox();
            lblDiscountReasonCaption = new System.Windows.Forms.Label();
            txtDiscountReason = new System.Windows.Forms.TextBox();
            btnApplyDiscount = new System.Windows.Forms.Button();
            btnGoToPayment = new System.Windows.Forms.Button();
            lblMessage = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlAdmissionCard.SuspendLayout();
            pnlItemsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlSummaryCard.SuspendLayout();
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
            lblTitle.Size = new System.Drawing.Size(96, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Billing";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(37, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(356, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Generate and review the itemized bill for an admission.";

            // 
            // pnlAdmissionCard
            // 
            pnlAdmissionCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlAdmissionCard.BackColor = System.Drawing.Color.White;
            pnlAdmissionCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlAdmissionCard.Controls.Add(btnGenerate);
            pnlAdmissionCard.Controls.Add(cmbAdmissions);
            pnlAdmissionCard.Controls.Add(lblAdmissionCaption);
            pnlAdmissionCard.Location = new System.Drawing.Point(35, 96);
            pnlAdmissionCard.Name = "pnlAdmissionCard";
            pnlAdmissionCard.Size = new System.Drawing.Size(1430, 92);
            pnlAdmissionCard.TabIndex = 2;

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
            cmbAdmissions.Location = new System.Drawing.Point(20, 38);
            cmbAdmissions.Name = "cmbAdmissions";
            cmbAdmissions.Size = new System.Drawing.Size(1180, 27);
            cmbAdmissions.TabIndex = 1;
            cmbAdmissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;

            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnGenerate.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnGenerate.ForeColor = System.Drawing.Color.White;
            btnGenerate.Location = new System.Drawing.Point(1218, 34);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(190, 36);
            btnGenerate.TabIndex = 2;
            btnGenerate.Text = "Generate bill";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += Generate_Click;

            // 
            // pnlItemsCard
            // 
            pnlItemsCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlItemsCard.BackColor = System.Drawing.Color.White;
            pnlItemsCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlItemsCard.Controls.Add(dgvItems);
            pnlItemsCard.Controls.Add(lblItemsTitle);
            pnlItemsCard.Location = new System.Drawing.Point(35, 204);
            pnlItemsCard.Name = "pnlItemsCard";
            pnlItemsCard.Size = new System.Drawing.Size(960, 732);
            pnlItemsCard.TabIndex = 3;

            // 
            // lblItemsTitle
            // 
            lblItemsTitle.AutoSize = true;
            lblItemsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblItemsTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblItemsTitle.Location = new System.Drawing.Point(20, 16);
            lblItemsTitle.Name = "lblItemsTitle";
            lblItemsTitle.Size = new System.Drawing.Size(83, 21);
            lblItemsTitle.TabIndex = 0;
            lblItemsTitle.Text = "Bill items";

            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvItems.AutoGenerateColumns = false;
            dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.BackgroundColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerCellStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            headerCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            headerCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvItems.ColumnHeadersDefaultCellStyle = headerCellStyle;
            dgvItems.ColumnHeadersHeight = 40;
            dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colItemType,
                colDescription,
                colQuantity,
                colUnitPrice,
                colAmount
            });
            defaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = System.Drawing.Color.White;
            defaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            defaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvItems.DefaultCellStyle = defaultCellStyle;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvItems.Location = new System.Drawing.Point(20, 48);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowTemplate.Height = 36;
            dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Size = new System.Drawing.Size(918, 662);
            dgvItems.TabIndex = 1;

            // 
            // colItemType
            // 
            colItemType.DataPropertyName = "ItemType";
            colItemType.FillWeight = 18F;
            colItemType.HeaderText = "Type";
            colItemType.Name = "colItemType";
            colItemType.ReadOnly = true;

            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.FillWeight = 42F;
            colDescription.HeaderText = "Description";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;

            // 
            // colQuantity
            // 
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.FillWeight = 12F;
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
            colUnitPrice.FillWeight = 14F;
            colUnitPrice.HeaderText = "Unit price";
            colUnitPrice.Name = "colUnitPrice";
            colUnitPrice.ReadOnly = true;

            // 
            // colAmount
            // 
            amountCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            amountCellStyle.Format = "₱#,##0.00";
            colAmount.DataPropertyName = "Amount";
            colAmount.DefaultCellStyle = amountCellStyle;
            colAmount.FillWeight = 16F;
            colAmount.HeaderText = "Line total";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;

            // 
            // pnlSummaryCard
            // 
            pnlSummaryCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pnlSummaryCard.BackColor = System.Drawing.Color.White;
            pnlSummaryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlSummaryCard.Controls.Add(lblMessage);
            pnlSummaryCard.Controls.Add(btnGoToPayment);
            pnlSummaryCard.Controls.Add(btnApplyDiscount);
            pnlSummaryCard.Controls.Add(txtDiscountReason);
            pnlSummaryCard.Controls.Add(lblDiscountReasonCaption);
            pnlSummaryCard.Controls.Add(txtDiscount);
            pnlSummaryCard.Controls.Add(lblDiscountAmountCaption);
            pnlSummaryCard.Controls.Add(lblDiscountHeading);
            pnlSummaryCard.Controls.Add(lblBalance);
            pnlSummaryCard.Controls.Add(lblBalanceCaption);
            pnlSummaryCard.Controls.Add(lblPaid);
            pnlSummaryCard.Controls.Add(lblPaidCaption);
            pnlSummaryCard.Controls.Add(lblTotal);
            pnlSummaryCard.Controls.Add(lblTotalCaption);
            pnlSummaryCard.Controls.Add(lblDiscountTotal);
            pnlSummaryCard.Controls.Add(lblDiscountCaption);
            pnlSummaryCard.Controls.Add(lblSubtotal);
            pnlSummaryCard.Controls.Add(lblSubtotalCaption);
            pnlSummaryCard.Controls.Add(lblBillStatus);
            pnlSummaryCard.Controls.Add(lblStatusCaption);
            pnlSummaryCard.Controls.Add(lblSummaryTitle);
            pnlSummaryCard.Location = new System.Drawing.Point(1013, 204);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Size = new System.Drawing.Size(452, 732);
            pnlSummaryCard.TabIndex = 4;

            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblSummaryTitle.Location = new System.Drawing.Point(22, 16);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new System.Drawing.Size(113, 21);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "Bill summary";

            // 
            // lblStatusCaption
            // 
            lblStatusCaption.AutoSize = true;
            lblStatusCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblStatusCaption.Location = new System.Drawing.Point(22, 58);
            lblStatusCaption.Name = "lblStatusCaption";
            lblStatusCaption.Size = new System.Drawing.Size(49, 19);
            lblStatusCaption.TabIndex = 1;
            lblStatusCaption.Text = "Status";

            // 
            // lblBillStatus
            // 
            lblBillStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblBillStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBillStatus.Location = new System.Drawing.Point(220, 56);
            lblBillStatus.Name = "lblBillStatus";
            lblBillStatus.Size = new System.Drawing.Size(208, 23);
            lblBillStatus.TabIndex = 2;
            lblBillStatus.Text = "Not generated";
            lblBillStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblSubtotalCaption
            // 
            lblSubtotalCaption.AutoSize = true;
            lblSubtotalCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblSubtotalCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSubtotalCaption.Location = new System.Drawing.Point(22, 98);
            lblSubtotalCaption.Name = "lblSubtotalCaption";
            lblSubtotalCaption.Size = new System.Drawing.Size(63, 19);
            lblSubtotalCaption.TabIndex = 3;
            lblSubtotalCaption.Text = "Subtotal";

            // 
            // lblSubtotal
            // 
            lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSubtotal.Location = new System.Drawing.Point(220, 96);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new System.Drawing.Size(208, 23);
            lblSubtotal.TabIndex = 4;
            lblSubtotal.Text = "₱0.00";
            lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblDiscountCaption
            // 
            lblDiscountCaption.AutoSize = true;
            lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblDiscountCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDiscountCaption.Location = new System.Drawing.Point(22, 138);
            lblDiscountCaption.Name = "lblDiscountCaption";
            lblDiscountCaption.Size = new System.Drawing.Size(65, 19);
            lblDiscountCaption.TabIndex = 5;
            lblDiscountCaption.Text = "Discount";

            // 
            // lblDiscountTotal
            // 
            lblDiscountTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDiscountTotal.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDiscountTotal.Location = new System.Drawing.Point(220, 136);
            lblDiscountTotal.Name = "lblDiscountTotal";
            lblDiscountTotal.Size = new System.Drawing.Size(208, 23);
            lblDiscountTotal.TabIndex = 6;
            lblDiscountTotal.Text = "₱0.00";
            lblDiscountTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTotalCaption.Location = new System.Drawing.Point(22, 180);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new System.Drawing.Size(45, 20);
            lblTotalCaption.TabIndex = 7;
            lblTotalCaption.Text = "Total";

            // 
            // lblTotal
            // 
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTotal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTotal.Location = new System.Drawing.Point(220, 178);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(208, 25);
            lblTotal.TabIndex = 8;
            lblTotal.Text = "₱0.00";
            lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblPaidCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblPaidCaption.Location = new System.Drawing.Point(22, 222);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new System.Drawing.Size(93, 19);
            lblPaidCaption.TabIndex = 9;
            lblPaidCaption.Text = "Amount paid";

            // 
            // lblPaid
            // 
            lblPaid.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblPaid.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblPaid.Location = new System.Drawing.Point(220, 220);
            lblPaid.Name = "lblPaid";
            lblPaid.Size = new System.Drawing.Size(208, 23);
            lblPaid.TabIndex = 10;
            lblPaid.Text = "₱0.00";
            lblPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblBalanceCaption
            // 
            lblBalanceCaption.AutoSize = true;
            lblBalanceCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBalanceCaption.Location = new System.Drawing.Point(22, 264);
            lblBalanceCaption.Name = "lblBalanceCaption";
            lblBalanceCaption.Size = new System.Drawing.Size(70, 21);
            lblBalanceCaption.TabIndex = 11;
            lblBalanceCaption.Text = "Balance";

            // 
            // lblBalance
            // 
            lblBalance.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            lblBalance.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBalance.Location = new System.Drawing.Point(220, 261);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new System.Drawing.Size(208, 27);
            lblBalance.TabIndex = 12;
            lblBalance.Text = "₱0.00";
            lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblDiscountHeading
            // 
            lblDiscountHeading.AutoSize = true;
            lblDiscountHeading.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblDiscountHeading.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDiscountHeading.Location = new System.Drawing.Point(22, 322);
            lblDiscountHeading.Name = "lblDiscountHeading";
            lblDiscountHeading.Size = new System.Drawing.Size(123, 20);
            lblDiscountHeading.TabIndex = 13;
            lblDiscountHeading.Text = "Apply discount";

            // 
            // lblDiscountAmountCaption
            // 
            lblDiscountAmountCaption.AutoSize = true;
            lblDiscountAmountCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblDiscountAmountCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDiscountAmountCaption.Location = new System.Drawing.Point(22, 354);
            lblDiscountAmountCaption.Name = "lblDiscountAmountCaption";
            lblDiscountAmountCaption.Size = new System.Drawing.Size(78, 17);
            lblDiscountAmountCaption.TabIndex = 14;
            lblDiscountAmountCaption.Text = "Amount (₱)";

            // 
            // txtDiscount
            // 
            txtDiscount.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtDiscount.Location = new System.Drawing.Point(22, 376);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.PlaceholderText = "0.00";
            txtDiscount.Size = new System.Drawing.Size(386, 26);
            txtDiscount.TabIndex = 15;

            // 
            // lblDiscountReasonCaption
            // 
            lblDiscountReasonCaption.AutoSize = true;
            lblDiscountReasonCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblDiscountReasonCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDiscountReasonCaption.Location = new System.Drawing.Point(22, 416);
            lblDiscountReasonCaption.Name = "lblDiscountReasonCaption";
            lblDiscountReasonCaption.Size = new System.Drawing.Size(119, 17);
            lblDiscountReasonCaption.TabIndex = 16;
            lblDiscountReasonCaption.Text = "Reason (required)";

            // 
            // txtDiscountReason
            // 
            txtDiscountReason.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtDiscountReason.Location = new System.Drawing.Point(22, 438);
            txtDiscountReason.MaxLength = 300;
            txtDiscountReason.Name = "txtDiscountReason";
            txtDiscountReason.PlaceholderText = "e.g. Senior Citizen / PhilHealth / Promo";
            txtDiscountReason.Size = new System.Drawing.Size(386, 26);
            txtDiscountReason.TabIndex = 17;

            // 
            // btnApplyDiscount
            // 
            btnApplyDiscount.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnApplyDiscount.Cursor = System.Windows.Forms.Cursors.Hand;
            btnApplyDiscount.FlatAppearance.BorderSize = 0;
            btnApplyDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnApplyDiscount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnApplyDiscount.ForeColor = System.Drawing.Color.White;
            btnApplyDiscount.Location = new System.Drawing.Point(22, 482);
            btnApplyDiscount.Name = "btnApplyDiscount";
            btnApplyDiscount.Size = new System.Drawing.Size(406, 38);
            btnApplyDiscount.TabIndex = 18;
            btnApplyDiscount.Text = "Apply discount";
            btnApplyDiscount.UseVisualStyleBackColor = false;
            btnApplyDiscount.Click += ApplyDiscount_Click;

            // 
            // btnGoToPayment
            // 
            btnGoToPayment.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            btnGoToPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGoToPayment.Enabled = false;
            btnGoToPayment.FlatAppearance.BorderSize = 0;
            btnGoToPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGoToPayment.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btnGoToPayment.ForeColor = System.Drawing.Color.White;
            btnGoToPayment.Location = new System.Drawing.Point(22, 536);
            btnGoToPayment.Name = "btnGoToPayment";
            btnGoToPayment.Size = new System.Drawing.Size(406, 42);
            btnGoToPayment.TabIndex = 19;
            btnGoToPayment.Text = "Go to payment";
            btnGoToPayment.UseVisualStyleBackColor = false;
            btnGoToPayment.Click += GoToPayment_Click;

            // 
            // lblMessage
            // 
            lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblMessage.Location = new System.Drawing.Point(22, 594);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(406, 110);
            lblMessage.TabIndex = 20;

            // 
            // errProvider
            // 
            errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errProvider.ContainerControl = this;

            // 
            // BillingUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            Controls.Add(pnlSummaryCard);
            Controls.Add(pnlItemsCard);
            Controls.Add(pnlAdmissionCard);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "BillingUserControl";
            Size = new System.Drawing.Size(1500, 969);
            Load += BillingUserControl_Load;

            pnlAdmissionCard.ResumeLayout(false);
            pnlAdmissionCard.PerformLayout();
            pnlItemsCard.ResumeLayout(false);
            pnlItemsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlSummaryCard.ResumeLayout(false);
            pnlSummaryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}