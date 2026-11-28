namespace UI
{
    partial class BillingUserControl
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
            components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components);
            pnlMainHandler = new Panel();
            lblTitle = new Label();
            grpAdmission = new GroupBox();
            lblAdmission = new Label();
            _admissions = new ComboBox();
            _generate = new Button();
            _goToPayment = new Button();
            _printStatement = new Button();
            grpBillItems = new GroupBox();
            _items = new DataGridView();
            grpSummary = new GroupBox();
            lblStatusKey = new Label();
            _billStatus = new Label();
            lblSubtotalKey = new Label();
            _subtotal = new Label();
            lblDiscountKey = new Label();
            _discountTotal = new Label();
            lblTotalKey = new Label();
            _total = new Label();
            lblPaidKey = new Label();
            _paid = new Label();
            lblBalanceKey = new Label();
            _balance = new Label();
            lblDiscountHeading = new Label();
            lblDiscountAmount = new Label();
            _discount = new TextBox();
            lblDiscountReason = new Label();
            _discountReason = new TextBox();
            _applyDiscount = new Button();
            _voidBill = new Button();
            _message = new Label();
            ((System.ComponentModel.ISupportInitialize)_errors).BeginInit();
            pnlMainHandler.SuspendLayout();
            grpAdmission.SuspendLayout();
            grpBillItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_items).BeginInit();
            grpSummary.SuspendLayout();
            SuspendLayout();
            // 
            // _errors
            // 
            _errors.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _errors.ContainerControl = this;
            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpAdmission);
            pnlMainHandler.Controls.Add(grpBillItems);
            pnlMainHandler.Controls.Add(grpSummary);
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
            lblTitle.Size = new Size(340, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Billing Management";
            // 
            // grpAdmission
            // 
            grpAdmission.BackColor = Color.WhiteSmoke;
            grpAdmission.Controls.Add(lblAdmission);
            grpAdmission.Controls.Add(_admissions);
            grpAdmission.Controls.Add(_generate);
            grpAdmission.Controls.Add(_goToPayment);
            grpAdmission.Controls.Add(_printStatement);
            grpAdmission.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpAdmission.Location = new Point(35, 85);
            grpAdmission.Name = "grpAdmission";
            grpAdmission.Size = new Size(1415, 105);
            grpAdmission.TabIndex = 1;
            grpAdmission.TabStop = false;
            grpAdmission.Text = "Admission Selection";
            // 
            // lblAdmission
            // 
            lblAdmission.AutoSize = true;
            lblAdmission.Font = new Font("Yu Gothic", 11F);
            lblAdmission.Location = new Point(25, 45);
            lblAdmission.Name = "lblAdmission";
            lblAdmission.Size = new Size(88, 20);
            lblAdmission.TabIndex = 0;
            lblAdmission.Text = "Admission:";
            // 
            // _admissions
            // 
            _admissions.DropDownStyle = ComboBoxStyle.DropDownList;
            _admissions.Font = new Font("Yu Gothic", 11F);
            _admissions.FormattingEnabled = true;
            _admissions.Location = new Point(130, 40);
            _admissions.Name = "_admissions";
            _admissions.Size = new Size(655, 27);
            _admissions.TabIndex = 1;
            // 
            // _generate
            // 
            _generate.BackColor = Color.LightSkyBlue;
            _generate.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _generate.Location = new Point(800, 34);
            _generate.Name = "_generate";
            _generate.Size = new Size(200, 42);
            _generate.TabIndex = 2;
            _generate.Text = "Generate Bill";
            _generate.UseVisualStyleBackColor = false;
            // 
            // _goToPayment
            // 
            _goToPayment.BackColor = Color.LightGreen;
            _goToPayment.Enabled = false;
            _goToPayment.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _goToPayment.Location = new Point(1010, 34);
            _goToPayment.Name = "_goToPayment";
            _goToPayment.Size = new Size(185, 42);
            _goToPayment.TabIndex = 3;
            _goToPayment.Text = "Go to Payment";
            _goToPayment.UseVisualStyleBackColor = false;
            // 
            // _printStatement
            // 
            _printStatement.BackColor = Color.White;
            _printStatement.Enabled = false;
            _printStatement.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _printStatement.Location = new Point(1205, 34);
            _printStatement.Name = "_printStatement";
            _printStatement.Size = new Size(185, 42);
            _printStatement.TabIndex = 4;
            _printStatement.Text = "Print Statement";
            _printStatement.UseVisualStyleBackColor = false;
            // 
            // grpBillItems
            // 
            grpBillItems.BackColor = Color.WhiteSmoke;
            grpBillItems.Controls.Add(_items);
            grpBillItems.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpBillItems.Location = new Point(35, 210);
            grpBillItems.Name = "grpBillItems";
            grpBillItems.Size = new Size(920, 715);
            grpBillItems.TabIndex = 2;
            grpBillItems.TabStop = false;
            grpBillItems.Text = "Bill Items";
            // 
            // _items
            // 
            _items.AllowUserToAddRows = false;
            _items.AllowUserToDeleteRows = false;
            _items.AllowUserToResizeRows = false;
            _items.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _items.BackgroundColor = Color.White;
            _items.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _items.Location = new Point(25, 45);
            _items.MultiSelect = false;
            _items.Name = "_items";
            _items.ReadOnly = true;
            _items.RowHeadersVisible = false;
            _items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _items.Size = new Size(870, 645);
            _items.TabIndex = 0;
            // 
            // grpSummary
            // 
            grpSummary.BackColor = Color.WhiteSmoke;
            grpSummary.Controls.Add(lblStatusKey);
            grpSummary.Controls.Add(_billStatus);
            grpSummary.Controls.Add(lblSubtotalKey);
            grpSummary.Controls.Add(_subtotal);
            grpSummary.Controls.Add(lblDiscountKey);
            grpSummary.Controls.Add(_discountTotal);
            grpSummary.Controls.Add(lblTotalKey);
            grpSummary.Controls.Add(_total);
            grpSummary.Controls.Add(lblPaidKey);
            grpSummary.Controls.Add(_paid);
            grpSummary.Controls.Add(lblBalanceKey);
            grpSummary.Controls.Add(_balance);
            grpSummary.Controls.Add(lblDiscountHeading);
            grpSummary.Controls.Add(lblDiscountAmount);
            grpSummary.Controls.Add(_discount);
            grpSummary.Controls.Add(lblDiscountReason);
            grpSummary.Controls.Add(_discountReason);
            grpSummary.Controls.Add(_applyDiscount);
            grpSummary.Controls.Add(_voidBill);
            grpSummary.Controls.Add(_message);
            grpSummary.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpSummary.Location = new Point(975, 210);
            grpSummary.Name = "grpSummary";
            grpSummary.Size = new Size(475, 715);
            grpSummary.TabIndex = 3;
            grpSummary.TabStop = false;
            grpSummary.Text = "Bill Summary";
            // 
            // lblStatusKey
            // 
            lblStatusKey.AutoSize = true;
            lblStatusKey.Font = new Font("Yu Gothic", 11F);
            lblStatusKey.Location = new Point(25, 50);
            lblStatusKey.Name = "lblStatusKey";
            lblStatusKey.Size = new Size(57, 20);
            lblStatusKey.TabIndex = 0;
            lblStatusKey.Text = "Status:";
            // 
            // _billStatus
            // 
            _billStatus.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _billStatus.Location = new Point(210, 50);
            _billStatus.Name = "_billStatus";
            _billStatus.Size = new Size(240, 24);
            _billStatus.TabIndex = 1;
            _billStatus.Text = "Not generated";
            _billStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtotalKey
            // 
            lblSubtotalKey.AutoSize = true;
            lblSubtotalKey.Font = new Font("Yu Gothic", 11F);
            lblSubtotalKey.Location = new Point(25, 95);
            lblSubtotalKey.Name = "lblSubtotalKey";
            lblSubtotalKey.Size = new Size(72, 20);
            lblSubtotalKey.TabIndex = 2;
            lblSubtotalKey.Text = "Subtotal:";
            // 
            // _subtotal
            // 
            _subtotal.Font = new Font("Yu Gothic", 11F);
            _subtotal.Location = new Point(210, 95);
            _subtotal.Name = "_subtotal";
            _subtotal.Size = new Size(240, 24);
            _subtotal.TabIndex = 3;
            _subtotal.Text = "₱0.00";
            _subtotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDiscountKey
            // 
            lblDiscountKey.AutoSize = true;
            lblDiscountKey.Font = new Font("Yu Gothic", 11F);
            lblDiscountKey.Location = new Point(25, 140);
            lblDiscountKey.Name = "lblDiscountKey";
            lblDiscountKey.Size = new Size(76, 20);
            lblDiscountKey.TabIndex = 4;
            lblDiscountKey.Text = "Discount:";
            // 
            // _discountTotal
            // 
            _discountTotal.Font = new Font("Yu Gothic", 11F);
            _discountTotal.Location = new Point(210, 140);
            _discountTotal.Name = "_discountTotal";
            _discountTotal.Size = new Size(240, 24);
            _discountTotal.TabIndex = 5;
            _discountTotal.Text = "₱0.00";
            _discountTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalKey
            // 
            lblTotalKey.AutoSize = true;
            lblTotalKey.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            lblTotalKey.Location = new Point(25, 188);
            lblTotalKey.Name = "lblTotalKey";
            lblTotalKey.Size = new Size(54, 21);
            lblTotalKey.TabIndex = 6;
            lblTotalKey.Text = "Total:";
            // 
            // _total
            // 
            _total.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            _total.Location = new Point(210, 188);
            _total.Name = "_total";
            _total.Size = new Size(240, 26);
            _total.TabIndex = 7;
            _total.Text = "₱0.00";
            _total.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPaidKey
            // 
            lblPaidKey.AutoSize = true;
            lblPaidKey.Font = new Font("Yu Gothic", 11F);
            lblPaidKey.Location = new Point(25, 238);
            lblPaidKey.Name = "lblPaidKey";
            lblPaidKey.Size = new Size(105, 20);
            lblPaidKey.TabIndex = 8;
            lblPaidKey.Text = "Amount Paid:";
            // 
            // _paid
            // 
            _paid.Font = new Font("Yu Gothic", 11F);
            _paid.Location = new Point(210, 238);
            _paid.Name = "_paid";
            _paid.Size = new Size(240, 24);
            _paid.TabIndex = 9;
            _paid.Text = "₱0.00";
            _paid.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblBalanceKey
            // 
            lblBalanceKey.AutoSize = true;
            lblBalanceKey.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            lblBalanceKey.Location = new Point(25, 285);
            lblBalanceKey.Name = "lblBalanceKey";
            lblBalanceKey.Size = new Size(78, 21);
            lblBalanceKey.TabIndex = 10;
            lblBalanceKey.Text = "Balance:";
            // 
            // _balance
            // 
            _balance.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            _balance.Location = new Point(210, 285);
            _balance.Name = "_balance";
            _balance.Size = new Size(240, 26);
            _balance.TabIndex = 11;
            _balance.Text = "₱0.00";
            _balance.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDiscountHeading
            // 
            lblDiscountHeading.AutoSize = true;
            lblDiscountHeading.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            lblDiscountHeading.Location = new Point(25, 355);
            lblDiscountHeading.Name = "lblDiscountHeading";
            lblDiscountHeading.Size = new Size(134, 21);
            lblDiscountHeading.TabIndex = 12;
            lblDiscountHeading.Text = "Apply Discount";
            // 
            // lblDiscountAmount
            // 
            lblDiscountAmount.AutoSize = true;
            lblDiscountAmount.Font = new Font("Yu Gothic", 11F);
            lblDiscountAmount.Location = new Point(25, 395);
            lblDiscountAmount.Name = "lblDiscountAmount";
            lblDiscountAmount.Size = new Size(135, 20);
            lblDiscountAmount.TabIndex = 13;
            lblDiscountAmount.Text = "Discount Amount:";
            // 
            // _discount
            // 
            _discount.Font = new Font("Yu Gothic", 11F);
            _discount.Location = new Point(25, 420);
            _discount.Name = "_discount";
            _discount.PlaceholderText = "0.00";
            _discount.Size = new Size(425, 31);
            _discount.TabIndex = 14;
            // 
            // lblDiscountReason
            // 
            lblDiscountReason.AutoSize = true;
            lblDiscountReason.Font = new Font("Yu Gothic", 11F);
            lblDiscountReason.Location = new Point(25, 470);
            lblDiscountReason.Name = "lblDiscountReason";
            lblDiscountReason.Size = new Size(141, 20);
            lblDiscountReason.TabIndex = 15;
            lblDiscountReason.Text = "Reason (Required):";
            // 
            // _discountReason
            // 
            _discountReason.Font = new Font("Yu Gothic", 11F);
            _discountReason.Location = new Point(25, 495);
            _discountReason.MaxLength = 300;
            _discountReason.Name = "_discountReason";
            _discountReason.PlaceholderText = "Enter reason";
            _discountReason.Size = new Size(425, 31);
            _discountReason.TabIndex = 16;
            // 
            // _applyDiscount
            // 
            _applyDiscount.BackColor = Color.LightGreen;
            _applyDiscount.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _applyDiscount.Location = new Point(25, 550);
            _applyDiscount.Name = "_applyDiscount";
            _applyDiscount.Size = new Size(425, 48);
            _applyDiscount.TabIndex = 17;
            _applyDiscount.Text = "Apply Discount";
            _applyDiscount.UseVisualStyleBackColor = false;
            // 
            // _voidBill
            // 
            _voidBill.BackColor = Color.MistyRose;
            _voidBill.Enabled = false;
            _voidBill.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            _voidBill.Location = new Point(25, 610);
            _voidBill.Name = "_voidBill";
            _voidBill.Size = new Size(425, 48);
            _voidBill.TabIndex = 19;
            _voidBill.Text = "Void Bill";
            _voidBill.UseVisualStyleBackColor = false;
            // 
            // _message
            // 
            _message.Font = new Font("Yu Gothic", 10F);
            _message.ForeColor = Color.FromArgb(185, 28, 28);
            _message.Location = new Point(25, 668);
            _message.Name = "_message";
            _message.Size = new Size(425, 40);
            _message.TabIndex = 18;
            // 
            // BillingUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "BillingUserControl";
            Size = new Size(1500, 979);
            ((System.ComponentModel.ISupportInitialize)_errors).EndInit();
            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpAdmission.ResumeLayout(false);
            grpAdmission.PerformLayout();
            grpBillItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_items).EndInit();
            grpSummary.ResumeLayout(false);
            grpSummary.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ErrorProvider _errors;
        private Panel pnlMainHandler;
        private Label lblTitle;
        private GroupBox grpAdmission;
        private Label lblAdmission;
        private ComboBox _admissions;
        private Button _generate;
        private Button _goToPayment;
        private Button _printStatement;
        private GroupBox grpBillItems;
        private DataGridView _items;
        private GroupBox grpSummary;
        private Label lblStatusKey;
        private Label _billStatus;
        private Label lblSubtotalKey;
        private Label _subtotal;
        private Label lblDiscountKey;
        private Label _discountTotal;
        private Label lblTotalKey;
        private Label _total;
        private Label lblPaidKey;
        private Label _paid;
        private Label lblBalanceKey;
        private Label _balance;
        private Label lblDiscountHeading;
        private Label lblDiscountAmount;
        private TextBox _discount;
        private Label lblDiscountReason;
        private TextBox _discountReason;
        private Button _applyDiscount;
        private Button _voidBill;
        private Label _message;
    }
}