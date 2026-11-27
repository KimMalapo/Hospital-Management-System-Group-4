namespace UI
{
    partial class PaymentUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlBillCard;
        private System.Windows.Forms.Label lblBillCaption;
        private System.Windows.Forms.ComboBox cmbBills;
        private System.Windows.Forms.Label lblBillNoCaption;
        private System.Windows.Forms.Label lblBillNo;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPaidCaption;
        private System.Windows.Forms.Label lblPaid;
        private System.Windows.Forms.Label lblBalanceCaption;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblBillStatus;
        private System.Windows.Forms.Panel pnlHistoryCard;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.Button btnVoidPayment;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferenceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceivedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Panel pnlPostCard;
        private System.Windows.Forms.Label lblPostTitle;
        private System.Windows.Forms.Label lblAmountCaption;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblMethodCaption;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblReferenceCaption;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Button btnPostPayment;
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
            System.Windows.Forms.DataGridViewCellStyle dateCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle amountCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            pnlBillCard = new System.Windows.Forms.Panel();
            lblBillCaption = new System.Windows.Forms.Label();
            cmbBills = new System.Windows.Forms.ComboBox();
            lblBillNoCaption = new System.Windows.Forms.Label();
            lblBillNo = new System.Windows.Forms.Label();
            lblTotalCaption = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();
            lblPaidCaption = new System.Windows.Forms.Label();
            lblPaid = new System.Windows.Forms.Label();
            lblBalanceCaption = new System.Windows.Forms.Label();
            lblBalance = new System.Windows.Forms.Label();
            lblStatusCaption = new System.Windows.Forms.Label();
            lblBillStatus = new System.Windows.Forms.Label();
            pnlHistoryCard = new System.Windows.Forms.Panel();
            lblHistoryTitle = new System.Windows.Forms.Label();
            btnVoidPayment = new System.Windows.Forms.Button();
            dgvPayments = new System.Windows.Forms.DataGridView();
            colPaymentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colReceivedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlPostCard = new System.Windows.Forms.Panel();
            lblPostTitle = new System.Windows.Forms.Label();
            lblAmountCaption = new System.Windows.Forms.Label();
            txtAmount = new System.Windows.Forms.TextBox();
            lblMethodCaption = new System.Windows.Forms.Label();
            cmbMethod = new System.Windows.Forms.ComboBox();
            lblReferenceCaption = new System.Windows.Forms.Label();
            txtReferenceNo = new System.Windows.Forms.TextBox();
            btnPostPayment = new System.Windows.Forms.Button();
            lblMessage = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlBillCard.SuspendLayout();
            pnlHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            pnlPostCard.SuspendLayout();
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
            lblTitle.Size = new System.Drawing.Size(134, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Payments";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(37, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(422, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Record patient payments, issue receipts, and review payment history.";

            // 
            // pnlBillCard
            // 
            pnlBillCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlBillCard.BackColor = System.Drawing.Color.White;
            pnlBillCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlBillCard.Controls.Add(lblBillStatus);
            pnlBillCard.Controls.Add(lblStatusCaption);
            pnlBillCard.Controls.Add(lblBalance);
            pnlBillCard.Controls.Add(lblBalanceCaption);
            pnlBillCard.Controls.Add(lblPaid);
            pnlBillCard.Controls.Add(lblPaidCaption);
            pnlBillCard.Controls.Add(lblTotal);
            pnlBillCard.Controls.Add(lblTotalCaption);
            pnlBillCard.Controls.Add(lblBillNo);
            pnlBillCard.Controls.Add(lblBillNoCaption);
            pnlBillCard.Controls.Add(cmbBills);
            pnlBillCard.Controls.Add(lblBillCaption);
            pnlBillCard.Location = new System.Drawing.Point(35, 96);
            pnlBillCard.Name = "pnlBillCard";
            pnlBillCard.Size = new System.Drawing.Size(1430, 146);
            pnlBillCard.TabIndex = 2;

            // 
            // lblBillCaption
            // 
            lblBillCaption.AutoSize = true;
            lblBillCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblBillCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblBillCaption.Location = new System.Drawing.Point(20, 14);
            lblBillCaption.Name = "lblBillCaption";
            lblBillCaption.Size = new System.Drawing.Size(102, 17);
            lblBillCaption.TabIndex = 0;
            lblBillCaption.Text = "Admission / Bill";

            // 
            // cmbBills
            // 
            cmbBills.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmbBills.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBills.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            cmbBills.FormattingEnabled = true;
            cmbBills.Location = new System.Drawing.Point(20, 38);
            cmbBills.Name = "cmbBills";
            cmbBills.Size = new System.Drawing.Size(1388, 27);
            cmbBills.TabIndex = 1;
            cmbBills.SelectedIndexChanged += Bills_SelectedIndexChanged;

            // 
            // lblBillNoCaption
            // 
            lblBillNoCaption.AutoSize = true;
            lblBillNoCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblBillNoCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBillNoCaption.Location = new System.Drawing.Point(20, 82);
            lblBillNoCaption.Name = "lblBillNoCaption";
            lblBillNoCaption.Size = new System.Drawing.Size(78, 15);
            lblBillNoCaption.TabIndex = 2;
            lblBillNoCaption.Text = "Bill number";

            // 
            // lblBillNo
            // 
            lblBillNo.AutoSize = true;
            lblBillNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblBillNo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBillNo.Location = new System.Drawing.Point(20, 103);
            lblBillNo.Name = "lblBillNo";
            lblBillNo.Size = new System.Drawing.Size(19, 21);
            lblBillNo.TabIndex = 3;
            lblBillNo.Text = "-";

            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTotalCaption.Location = new System.Drawing.Point(310, 82);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new System.Drawing.Size(69, 15);
            lblTotalCaption.TabIndex = 4;
            lblTotalCaption.Text = "Bill total";

            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblTotal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTotal.Location = new System.Drawing.Point(310, 103);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(52, 21);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "₱0.00";

            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPaidCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPaidCaption.Location = new System.Drawing.Point(590, 82);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new System.Drawing.Size(83, 15);
            lblPaidCaption.TabIndex = 6;
            lblPaidCaption.Text = "Amount paid";

            // 
            // lblPaid
            // 
            lblPaid.AutoSize = true;
            lblPaid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblPaid.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            lblPaid.Location = new System.Drawing.Point(590, 103);
            lblPaid.Name = "lblPaid";
            lblPaid.Size = new System.Drawing.Size(52, 21);
            lblPaid.TabIndex = 7;
            lblPaid.Text = "₱0.00";

            // 
            // lblBalanceCaption
            // 
            lblBalanceCaption.AutoSize = true;
            lblBalanceCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBalanceCaption.Location = new System.Drawing.Point(870, 82);
            lblBalanceCaption.Name = "lblBalanceCaption";
            lblBalanceCaption.Size = new System.Drawing.Size(53, 15);
            lblBalanceCaption.TabIndex = 8;
            lblBalanceCaption.Text = "Balance";

            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            lblBalance.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBalance.Location = new System.Drawing.Point(870, 102);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new System.Drawing.Size(55, 23);
            lblBalance.TabIndex = 9;
            lblBalance.Text = "₱0.00";

            // 
            // lblStatusCaption
            // 
            lblStatusCaption.AutoSize = true;
            lblStatusCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblStatusCaption.Location = new System.Drawing.Point(1150, 82);
            lblStatusCaption.Name = "lblStatusCaption";
            lblStatusCaption.Size = new System.Drawing.Size(45, 15);
            lblStatusCaption.TabIndex = 10;
            lblStatusCaption.Text = "Status";

            // 
            // lblBillStatus
            // 
            lblBillStatus.AutoSize = true;
            lblBillStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblBillStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblBillStatus.Location = new System.Drawing.Point(1150, 103);
            lblBillStatus.Name = "lblBillStatus";
            lblBillStatus.Size = new System.Drawing.Size(19, 21);
            lblBillStatus.TabIndex = 11;
            lblBillStatus.Text = "-";

            // 
            // pnlHistoryCard
            // 
            pnlHistoryCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlHistoryCard.BackColor = System.Drawing.Color.White;
            pnlHistoryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlHistoryCard.Controls.Add(dgvPayments);
            pnlHistoryCard.Controls.Add(btnVoidPayment);
            pnlHistoryCard.Controls.Add(lblHistoryTitle);
            pnlHistoryCard.Location = new System.Drawing.Point(35, 258);
            pnlHistoryCard.Name = "pnlHistoryCard";
            pnlHistoryCard.Size = new System.Drawing.Size(960, 678);
            pnlHistoryCard.TabIndex = 3;

            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.AutoSize = true;
            lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblHistoryTitle.Location = new System.Drawing.Point(20, 18);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new System.Drawing.Size(134, 21);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "Payment history";

            // 
            // btnVoidPayment
            // 
            btnVoidPayment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnVoidPayment.BackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnVoidPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnVoidPayment.FlatAppearance.BorderSize = 0;
            btnVoidPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnVoidPayment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnVoidPayment.ForeColor = System.Drawing.Color.White;
            btnVoidPayment.Location = new System.Drawing.Point(778, 14);
            btnVoidPayment.Name = "btnVoidPayment";
            btnVoidPayment.Size = new System.Drawing.Size(160, 32);
            btnVoidPayment.TabIndex = 1;
            btnVoidPayment.Text = "Void selected";
            btnVoidPayment.UseVisualStyleBackColor = false;
            btnVoidPayment.Click += VoidPayment_Click;

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
            headerCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerCellStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            headerCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            headerCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvPayments.ColumnHeadersDefaultCellStyle = headerCellStyle;
            dgvPayments.ColumnHeadersHeight = 40;
            dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colPaymentId,
                colReceiptNo,
                colPaymentDate,
                colAmount,
                colMethod,
                colReferenceNo,
                colReceivedBy,
                colStatus
            });
            defaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = System.Drawing.Color.White;
            defaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            defaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvPayments.DefaultCellStyle = defaultCellStyle;
            dgvPayments.EnableHeadersVisualStyles = false;
            dgvPayments.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvPayments.Location = new System.Drawing.Point(20, 56);
            dgvPayments.MultiSelect = false;
            dgvPayments.Name = "dgvPayments";
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.RowTemplate.Height = 36;
            dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.Size = new System.Drawing.Size(918, 600);
            dgvPayments.TabIndex = 2;

            // 
            // colPaymentId
            // 
            colPaymentId.DataPropertyName = "PaymentID";
            colPaymentId.HeaderText = "ID";
            colPaymentId.Name = "colPaymentId";
            colPaymentId.ReadOnly = true;
            colPaymentId.Visible = false;

            // 
            // colReceiptNo
            // 
            colReceiptNo.DataPropertyName = "ReceiptNo";
            colReceiptNo.FillWeight = 18F;
            colReceiptNo.HeaderText = "Receipt #";
            colReceiptNo.Name = "colReceiptNo";
            colReceiptNo.ReadOnly = true;

            // 
            // colPaymentDate
            // 
            dateCellStyle.Format = "yyyy-MM-dd HH:mm";
            colPaymentDate.DataPropertyName = "PaymentDate";
            colPaymentDate.DefaultCellStyle = dateCellStyle;
            colPaymentDate.FillWeight = 18F;
            colPaymentDate.HeaderText = "Date";
            colPaymentDate.Name = "colPaymentDate";
            colPaymentDate.ReadOnly = true;

            // 
            // colAmount
            // 
            amountCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            amountCellStyle.Format = "₱#,##0.00";
            colAmount.DataPropertyName = "Amount";
            colAmount.DefaultCellStyle = amountCellStyle;
            colAmount.FillWeight = 15F;
            colAmount.HeaderText = "Amount";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;

            // 
            // colMethod
            // 
            colMethod.DataPropertyName = "Method";
            colMethod.FillWeight = 14F;
            colMethod.HeaderText = "Method";
            colMethod.Name = "colMethod";
            colMethod.ReadOnly = true;

            // 
            // colReferenceNo
            // 
            colReferenceNo.DataPropertyName = "ReferenceNo";
            colReferenceNo.FillWeight = 15F;
            colReferenceNo.HeaderText = "Reference";
            colReferenceNo.Name = "colReferenceNo";
            colReferenceNo.ReadOnly = true;

            // 
            // colReceivedBy
            // 
            colReceivedBy.DataPropertyName = "ReceivedBy";
            colReceivedBy.FillWeight = 16F;
            colReceivedBy.HeaderText = "Received by";
            colReceivedBy.Name = "colReceivedBy";
            colReceivedBy.ReadOnly = true;

            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "Status";
            colStatus.FillWeight = 12F;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;

            // 
            // pnlPostCard
            // 
            pnlPostCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pnlPostCard.BackColor = System.Drawing.Color.White;
            pnlPostCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlPostCard.Controls.Add(lblMessage);
            pnlPostCard.Controls.Add(btnPostPayment);
            pnlPostCard.Controls.Add(txtReferenceNo);
            pnlPostCard.Controls.Add(lblReferenceCaption);
            pnlPostCard.Controls.Add(cmbMethod);
            pnlPostCard.Controls.Add(lblMethodCaption);
            pnlPostCard.Controls.Add(txtAmount);
            pnlPostCard.Controls.Add(lblAmountCaption);
            pnlPostCard.Controls.Add(lblPostTitle);
            pnlPostCard.Location = new System.Drawing.Point(1013, 258);
            pnlPostCard.Name = "pnlPostCard";
            pnlPostCard.Size = new System.Drawing.Size(452, 678);
            pnlPostCard.TabIndex = 4;

            // 
            // lblPostTitle
            // 
            lblPostTitle.AutoSize = true;
            lblPostTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblPostTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblPostTitle.Location = new System.Drawing.Point(22, 18);
            lblPostTitle.Name = "lblPostTitle";
            lblPostTitle.Size = new System.Drawing.Size(113, 21);
            lblPostTitle.TabIndex = 0;
            lblPostTitle.Text = "Post payment";

            // 
            // lblAmountCaption
            // 
            lblAmountCaption.AutoSize = true;
            lblAmountCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblAmountCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblAmountCaption.Location = new System.Drawing.Point(22, 60);
            lblAmountCaption.Name = "lblAmountCaption";
            lblAmountCaption.Size = new System.Drawing.Size(78, 17);
            lblAmountCaption.TabIndex = 1;
            lblAmountCaption.Text = "Amount (₱)";

            // 
            // txtAmount
            // 
            txtAmount.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtAmount.Location = new System.Drawing.Point(22, 82);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "0.00";
            txtAmount.Size = new System.Drawing.Size(386, 27);
            txtAmount.TabIndex = 2;

            // 
            // lblMethodCaption
            // 
            lblMethodCaption.AutoSize = true;
            lblMethodCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblMethodCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblMethodCaption.Location = new System.Drawing.Point(22, 128);
            lblMethodCaption.Name = "lblMethodCaption";
            lblMethodCaption.Size = new System.Drawing.Size(114, 17);
            lblMethodCaption.TabIndex = 3;
            lblMethodCaption.Text = "Payment method";

            // 
            // cmbMethod
            // 
            cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMethod.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            cmbMethod.FormattingEnabled = true;
            cmbMethod.Location = new System.Drawing.Point(22, 150);
            cmbMethod.Name = "cmbMethod";
            cmbMethod.Size = new System.Drawing.Size(386, 27);
            cmbMethod.TabIndex = 4;
            cmbMethod.SelectedIndexChanged += Method_SelectedIndexChanged;

            // 
            // lblReferenceCaption
            // 
            lblReferenceCaption.AutoSize = true;
            lblReferenceCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblReferenceCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblReferenceCaption.Location = new System.Drawing.Point(22, 196);
            lblReferenceCaption.Name = "lblReferenceCaption";
            lblReferenceCaption.Size = new System.Drawing.Size(121, 17);
            lblReferenceCaption.TabIndex = 5;
            lblReferenceCaption.Text = "Reference number";

            // 
            // txtReferenceNo
            // 
            txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtReferenceNo.Location = new System.Drawing.Point(22, 218);
            txtReferenceNo.MaxLength = 100;
            txtReferenceNo.Name = "txtReferenceNo";
            txtReferenceNo.PlaceholderText = "Required for non-cash payments";
            txtReferenceNo.Size = new System.Drawing.Size(386, 26);
            txtReferenceNo.TabIndex = 6;

            // 
            // btnPostPayment
            // 
            btnPostPayment.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnPostPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPostPayment.FlatAppearance.BorderSize = 0;
            btnPostPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPostPayment.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btnPostPayment.ForeColor = System.Drawing.Color.White;
            btnPostPayment.Location = new System.Drawing.Point(22, 268);
            btnPostPayment.Name = "btnPostPayment";
            btnPostPayment.Size = new System.Drawing.Size(406, 42);
            btnPostPayment.TabIndex = 7;
            btnPostPayment.Text = "Post payment";
            btnPostPayment.UseVisualStyleBackColor = false;
            btnPostPayment.Click += PostPayment_Click;

            // 
            // lblMessage
            // 
            lblMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblMessage.Location = new System.Drawing.Point(22, 330);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(406, 320);
            lblMessage.TabIndex = 8;

            // 
            // errProvider
            // 
            errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errProvider.ContainerControl = this;

            // 
            // PaymentUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            Controls.Add(pnlPostCard);
            Controls.Add(pnlHistoryCard);
            Controls.Add(pnlBillCard);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "PaymentUserControl";
            Size = new System.Drawing.Size(1500, 969);
            Load += PaymentUserControl_Load;

            pnlBillCard.ResumeLayout(false);
            pnlBillCard.PerformLayout();
            pnlHistoryCard.ResumeLayout(false);
            pnlHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            pnlPostCard.ResumeLayout(false);
            pnlPostCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}