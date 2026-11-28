namespace UI
{
    partial class PaymentUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMainHandler;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpBillSelection;
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
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.Button btnPrintReceipt;
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
        private System.Windows.Forms.GroupBox grpPostPayment;
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
            System.Windows.Forms.DataGridViewCellStyle dateCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle amountCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            pnlMainHandler = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            grpBillSelection = new System.Windows.Forms.GroupBox();
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
            grpHistory = new System.Windows.Forms.GroupBox();
            btnPrintReceipt = new System.Windows.Forms.Button();
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
            grpPostPayment = new System.Windows.Forms.GroupBox();
            lblAmountCaption = new System.Windows.Forms.Label();
            txtAmount = new System.Windows.Forms.TextBox();
            lblMethodCaption = new System.Windows.Forms.Label();
            cmbMethod = new System.Windows.Forms.ComboBox();
            lblReferenceCaption = new System.Windows.Forms.Label();
            txtReferenceNo = new System.Windows.Forms.TextBox();
            btnPostPayment = new System.Windows.Forms.Button();
            lblMessage = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlMainHandler.SuspendLayout();
            grpBillSelection.SuspendLayout();
            grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            grpPostPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            SuspendLayout();

            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpBillSelection);
            pnlMainHandler.Controls.Add(grpHistory);
            pnlMainHandler.Controls.Add(grpPostPayment);
            pnlMainHandler.Location = new System.Drawing.Point(1, 2);
            pnlMainHandler.Name = "pnlMainHandler";
            pnlMainHandler.Size = new System.Drawing.Size(1490, 970);
            pnlMainHandler.TabIndex = 0;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Yu Gothic", 24F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(35, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(375, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Payment Management";

            // 
            // grpBillSelection
            // 
            grpBillSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            grpBillSelection.Controls.Add(lblBillCaption);
            grpBillSelection.Controls.Add(cmbBills);
            grpBillSelection.Controls.Add(lblBillNoCaption);
            grpBillSelection.Controls.Add(lblBillNo);
            grpBillSelection.Controls.Add(lblTotalCaption);
            grpBillSelection.Controls.Add(lblTotal);
            grpBillSelection.Controls.Add(lblPaidCaption);
            grpBillSelection.Controls.Add(lblPaid);
            grpBillSelection.Controls.Add(lblBalanceCaption);
            grpBillSelection.Controls.Add(lblBalance);
            grpBillSelection.Controls.Add(lblStatusCaption);
            grpBillSelection.Controls.Add(lblBillStatus);
            grpBillSelection.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpBillSelection.Location = new System.Drawing.Point(35, 85);
            grpBillSelection.Name = "grpBillSelection";
            grpBillSelection.Size = new System.Drawing.Size(1415, 165);
            grpBillSelection.TabIndex = 1;
            grpBillSelection.TabStop = false;
            grpBillSelection.Text = "Admission & Bill Summary";

            // 
            // lblBillCaption
            // 
            lblBillCaption.AutoSize = true;
            lblBillCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblBillCaption.Location = new System.Drawing.Point(25, 42);
            lblBillCaption.Name = "lblBillCaption";
            lblBillCaption.Size = new System.Drawing.Size(118, 20);
            lblBillCaption.TabIndex = 0;
            lblBillCaption.Text = "Admission / Bill:";

            // 
            // cmbBills
            // 
            cmbBills.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBills.Font = new System.Drawing.Font("Yu Gothic", 11F);
            cmbBills.FormattingEnabled = true;
            cmbBills.Location = new System.Drawing.Point(165, 38);
            cmbBills.Name = "cmbBills";
            cmbBills.Size = new System.Drawing.Size(1225, 27);
            cmbBills.TabIndex = 1;
            cmbBills.SelectedIndexChanged += Bills_SelectedIndexChanged;

            // 
            // lblBillNoCaption
            // 
            lblBillNoCaption.AutoSize = true;
            lblBillNoCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblBillNoCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBillNoCaption.Location = new System.Drawing.Point(25, 88);
            lblBillNoCaption.Name = "lblBillNoCaption";
            lblBillNoCaption.Size = new System.Drawing.Size(87, 18);
            lblBillNoCaption.TabIndex = 2;
            lblBillNoCaption.Text = "Bill Number:";

            // 
            // lblBillNo
            // 
            lblBillNo.AutoSize = true;
            lblBillNo.Font = new System.Drawing.Font("Yu Gothic Medium", 13F, System.Drawing.FontStyle.Bold);
            lblBillNo.Location = new System.Drawing.Point(25, 112);
            lblBillNo.Name = "lblBillNo";
            lblBillNo.Size = new System.Drawing.Size(22, 23);
            lblBillNo.TabIndex = 3;
            lblBillNo.Text = "-";

            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTotalCaption.Location = new System.Drawing.Point(310, 88);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new System.Drawing.Size(68, 18);
            lblTotalCaption.TabIndex = 4;
            lblTotalCaption.Text = "Bill Total:";

            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new System.Drawing.Font("Yu Gothic Medium", 13F, System.Drawing.FontStyle.Bold);
            lblTotal.Location = new System.Drawing.Point(310, 112);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(61, 23);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "₱0.00";

            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblPaidCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPaidCaption.Location = new System.Drawing.Point(590, 88);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.Size = new System.Drawing.Size(94, 18);
            lblPaidCaption.TabIndex = 6;
            lblPaidCaption.Text = "Amount Paid:";

            // 
            // lblPaid
            // 
            lblPaid.AutoSize = true;
            lblPaid.Font = new System.Drawing.Font("Yu Gothic Medium", 13F, System.Drawing.FontStyle.Bold);
            lblPaid.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            lblPaid.Location = new System.Drawing.Point(590, 112);
            lblPaid.Name = "lblPaid";
            lblPaid.Size = new System.Drawing.Size(61, 23);
            lblPaid.TabIndex = 7;
            lblPaid.Text = "₱0.00";

            // 
            // lblBalanceCaption
            // 
            lblBalanceCaption.AutoSize = true;
            lblBalanceCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBalanceCaption.Location = new System.Drawing.Point(870, 88);
            lblBalanceCaption.Name = "lblBalanceCaption";
            lblBalanceCaption.Size = new System.Drawing.Size(63, 18);
            lblBalanceCaption.TabIndex = 8;
            lblBalanceCaption.Text = "Balance:";

            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new System.Drawing.Font("Yu Gothic Medium", 13F, System.Drawing.FontStyle.Bold);
            lblBalance.Location = new System.Drawing.Point(870, 112);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new System.Drawing.Size(61, 23);
            lblBalance.TabIndex = 9;
            lblBalance.Text = "₱0.00";

            // 
            // lblStatusCaption
            // 
            lblStatusCaption.AutoSize = true;
            lblStatusCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblStatusCaption.Location = new System.Drawing.Point(1150, 88);
            lblStatusCaption.Name = "lblStatusCaption";
            lblStatusCaption.Size = new System.Drawing.Size(53, 18);
            lblStatusCaption.TabIndex = 10;
            lblStatusCaption.Text = "Status:";

            // 
            // lblBillStatus
            // 
            lblBillStatus.AutoSize = true;
            lblBillStatus.Font = new System.Drawing.Font("Yu Gothic Medium", 13F, System.Drawing.FontStyle.Bold);
            lblBillStatus.Location = new System.Drawing.Point(1150, 112);
            lblBillStatus.Name = "lblBillStatus";
            lblBillStatus.Size = new System.Drawing.Size(22, 23);
            lblBillStatus.TabIndex = 11;
            lblBillStatus.Text = "-";

            // 
            // grpHistory
            // 
            grpHistory.BackColor = System.Drawing.Color.WhiteSmoke;
            grpHistory.Controls.Add(btnPrintReceipt);
            grpHistory.Controls.Add(btnVoidPayment);
            grpHistory.Controls.Add(dgvPayments);
            grpHistory.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpHistory.Location = new System.Drawing.Point(35, 270);
            grpHistory.Name = "grpHistory";
            grpHistory.Size = new System.Drawing.Size(920, 655);
            grpHistory.TabIndex = 2;
            grpHistory.TabStop = false;
            grpHistory.Text = "Payment History";

            // 
            // btnPrintReceipt
            // 
            btnPrintReceipt.BackColor = System.Drawing.Color.White;
            btnPrintReceipt.Enabled = false;
            btnPrintReceipt.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            btnPrintReceipt.Location = new System.Drawing.Point(525, 32);
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new System.Drawing.Size(180, 40);
            btnPrintReceipt.TabIndex = 0;
            btnPrintReceipt.Text = "Print Receipt";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += PrintReceipt_Click;

            // 
            // btnVoidPayment
            // 
            btnVoidPayment.BackColor = System.Drawing.Color.MistyRose;
            btnVoidPayment.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            btnVoidPayment.Location = new System.Drawing.Point(715, 32);
            btnVoidPayment.Name = "btnVoidPayment";
            btnVoidPayment.Size = new System.Drawing.Size(180, 40);
            btnVoidPayment.TabIndex = 1;
            btnVoidPayment.Text = "Void Selected";
            btnVoidPayment.UseVisualStyleBackColor = false;
            btnVoidPayment.Click += VoidPayment_Click;

            // 
            // dgvPayments
            // 
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.AllowUserToResizeRows = false;
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.BackgroundColor = System.Drawing.Color.White;
            dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            dgvPayments.Location = new System.Drawing.Point(25, 85);
            dgvPayments.MultiSelect = false;
            dgvPayments.Name = "dgvPayments";
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.RowTemplate.Height = 36;
            dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.Size = new System.Drawing.Size(870, 545);
            dgvPayments.TabIndex = 2;
            dgvPayments.SelectionChanged += Payments_SelectionChanged;

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
            colReceivedBy.HeaderText = "Received By";
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
            // grpPostPayment
            // 
            grpPostPayment.BackColor = System.Drawing.Color.WhiteSmoke;
            grpPostPayment.Controls.Add(lblAmountCaption);
            grpPostPayment.Controls.Add(txtAmount);
            grpPostPayment.Controls.Add(lblMethodCaption);
            grpPostPayment.Controls.Add(cmbMethod);
            grpPostPayment.Controls.Add(lblReferenceCaption);
            grpPostPayment.Controls.Add(txtReferenceNo);
            grpPostPayment.Controls.Add(btnPostPayment);
            grpPostPayment.Controls.Add(lblMessage);
            grpPostPayment.Font = new System.Drawing.Font("Yu Gothic Medium", 12F, System.Drawing.FontStyle.Bold);
            grpPostPayment.Location = new System.Drawing.Point(975, 270);
            grpPostPayment.Name = "grpPostPayment";
            grpPostPayment.Size = new System.Drawing.Size(475, 655);
            grpPostPayment.TabIndex = 3;
            grpPostPayment.TabStop = false;
            grpPostPayment.Text = "Post Payment";

            // 
            // lblAmountCaption
            // 
            lblAmountCaption.AutoSize = true;
            lblAmountCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblAmountCaption.Location = new System.Drawing.Point(25, 50);
            lblAmountCaption.Name = "lblAmountCaption";
            lblAmountCaption.Size = new System.Drawing.Size(91, 20);
            lblAmountCaption.TabIndex = 0;
            lblAmountCaption.Text = "Amount (₱):";

            // 
            // txtAmount
            // 
            txtAmount.Font = new System.Drawing.Font("Yu Gothic", 11F);
            txtAmount.Location = new System.Drawing.Point(25, 78);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "0.00";
            txtAmount.Size = new System.Drawing.Size(425, 31);
            txtAmount.TabIndex = 1;

            // 
            // lblMethodCaption
            // 
            lblMethodCaption.AutoSize = true;
            lblMethodCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblMethodCaption.Location = new System.Drawing.Point(25, 132);
            lblMethodCaption.Name = "lblMethodCaption";
            lblMethodCaption.Size = new System.Drawing.Size(131, 20);
            lblMethodCaption.TabIndex = 2;
            lblMethodCaption.Text = "Payment Method:";

            // 
            // cmbMethod
            // 
            cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMethod.Font = new System.Drawing.Font("Yu Gothic", 11F);
            cmbMethod.FormattingEnabled = true;
            cmbMethod.Location = new System.Drawing.Point(25, 160);
            cmbMethod.Name = "cmbMethod";
            cmbMethod.Size = new System.Drawing.Size(425, 27);
            cmbMethod.TabIndex = 3;
            cmbMethod.SelectedIndexChanged += Method_SelectedIndexChanged;

            // 
            // lblReferenceCaption
            // 
            lblReferenceCaption.AutoSize = true;
            lblReferenceCaption.Font = new System.Drawing.Font("Yu Gothic", 11F);
            lblReferenceCaption.Location = new System.Drawing.Point(25, 212);
            lblReferenceCaption.Name = "lblReferenceCaption";
            lblReferenceCaption.Size = new System.Drawing.Size(141, 20);
            lblReferenceCaption.TabIndex = 4;
            lblReferenceCaption.Text = "Reference Number:";

            // 
            // txtReferenceNo
            // 
            txtReferenceNo.Font = new System.Drawing.Font("Yu Gothic", 11F);
            txtReferenceNo.Location = new System.Drawing.Point(25, 240);
            txtReferenceNo.MaxLength = 100;
            txtReferenceNo.Name = "txtReferenceNo";
            txtReferenceNo.PlaceholderText = "Required for non-cash payments";
            txtReferenceNo.Size = new System.Drawing.Size(425, 31);
            txtReferenceNo.TabIndex = 5;

            // 
            // btnPostPayment
            // 
            btnPostPayment.BackColor = System.Drawing.Color.LightGreen;
            btnPostPayment.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            btnPostPayment.Location = new System.Drawing.Point(25, 300);
            btnPostPayment.Name = "btnPostPayment";
            btnPostPayment.Size = new System.Drawing.Size(425, 50);
            btnPostPayment.TabIndex = 6;
            btnPostPayment.Text = "Post Payment";
            btnPostPayment.UseVisualStyleBackColor = false;
            btnPostPayment.Click += PostPayment_Click;

            // 
            // lblMessage
            // 
            lblMessage.Font = new System.Drawing.Font("Yu Gothic", 10.5F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblMessage.Location = new System.Drawing.Point(25, 370);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(425, 260);
            lblMessage.TabIndex = 7;

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
            Controls.Add(pnlMainHandler);
            Name = "PaymentUserControl";
            Size = new System.Drawing.Size(1500, 979);
            Load += PaymentUserControl_Load;

            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpBillSelection.ResumeLayout(false);
            grpBillSelection.PerformLayout();
            grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            grpPostPayment.ResumeLayout(false);
            grpPostPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}