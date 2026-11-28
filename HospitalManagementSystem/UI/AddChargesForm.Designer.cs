namespace UI
{
    partial class AddChargesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grpServices;
        private System.Windows.Forms.Label lblSearchCaption;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.DataGridView _servicesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceUnitPrice;
        private System.Windows.Forms.Label lblQuantityCaption;
        private System.Windows.Forms.NumericUpDown _quantity;
        private System.Windows.Forms.Button btnAddCharge;
        private System.Windows.Forms.GroupBox grpCharges;
        private System.Windows.Forms.DataGridView _chargesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargedAt;
        private System.Windows.Forms.Label _total;
        private System.Windows.Forms.Button btnVoidCharge;
        private System.Windows.Forms.Button btnClose;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle servicePriceStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle chargePriceStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle lineTotalStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle chargedAtStyle = new System.Windows.Forms.DataGridViewCellStyle();

            pnlMain = new System.Windows.Forms.Panel();
            grpServices = new System.Windows.Forms.GroupBox();
            lblSearchCaption = new System.Windows.Forms.Label();
            _search = new System.Windows.Forms.TextBox();
            _servicesGrid = new System.Windows.Forms.DataGridView();
            colServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colServiceUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblQuantityCaption = new System.Windows.Forms.Label();
            _quantity = new System.Windows.Forms.NumericUpDown();
            btnAddCharge = new System.Windows.Forms.Button();
            grpCharges = new System.Windows.Forms.GroupBox();
            _chargesGrid = new System.Windows.Forms.DataGridView();
            colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargeUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colChargedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            _total = new System.Windows.Forms.Label();
            btnVoidCharge = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();

            pnlMain.SuspendLayout();
            grpServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_servicesGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_quantity).BeginInit();
            grpCharges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_chargesGrid).BeginInit();
            SuspendLayout();

            // 
            // pnlMain
            // 
            pnlMain.BackColor = System.Drawing.SystemColors.ControlLight;
            pnlMain.Controls.Add(grpServices);
            pnlMain.Controls.Add(grpCharges);
            pnlMain.Controls.Add(btnVoidCharge);
            pnlMain.Controls.Add(btnClose);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(964, 661);
            pnlMain.TabIndex = 0;

            // 
            // grpServices
            // 
            grpServices.BackColor = System.Drawing.Color.WhiteSmoke;
            grpServices.Controls.Add(lblSearchCaption);
            grpServices.Controls.Add(_search);
            grpServices.Controls.Add(_servicesGrid);
            grpServices.Controls.Add(lblQuantityCaption);
            grpServices.Controls.Add(_quantity);
            grpServices.Controls.Add(btnAddCharge);
            grpServices.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            grpServices.Location = new System.Drawing.Point(16, 12);
            grpServices.Name = "grpServices";
            grpServices.Size = new System.Drawing.Size(932, 290);
            grpServices.TabIndex = 0;
            grpServices.TabStop = false;
            grpServices.Text = "Available Services";

            // 
            // lblSearchCaption
            // 
            lblSearchCaption.AutoSize = true;
            lblSearchCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblSearchCaption.Location = new System.Drawing.Point(16, 34);
            lblSearchCaption.Name = "lblSearchCaption";
            lblSearchCaption.Size = new System.Drawing.Size(58, 18);
            lblSearchCaption.TabIndex = 0;
            lblSearchCaption.Text = "Search:";

            // 
            // _search
            // 
            _search.Font = new System.Drawing.Font("Yu Gothic", 10F);
            _search.Location = new System.Drawing.Point(82, 30);
            _search.Name = "_search";
            _search.PlaceholderText = "Search services by name or code";
            _search.Size = new System.Drawing.Size(834, 28);
            _search.TabIndex = 1;
            _search.TextChanged += Search_TextChanged;

            // 
            // _servicesGrid
            // 
            _servicesGrid.AllowUserToAddRows = false;
            _servicesGrid.AllowUserToDeleteRows = false;
            _servicesGrid.AllowUserToResizeRows = false;
            _servicesGrid.AutoGenerateColumns = false;
            _servicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _servicesGrid.BackgroundColor = System.Drawing.Color.White;
            _servicesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _servicesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colServiceName,
                colCategory,
                colServiceUnitPrice
            });
            _servicesGrid.Location = new System.Drawing.Point(16, 68);
            _servicesGrid.MultiSelect = false;
            _servicesGrid.Name = "_servicesGrid";
            _servicesGrid.ReadOnly = true;
            _servicesGrid.RowHeadersVisible = false;
            _servicesGrid.RowTemplate.Height = 32;
            _servicesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _servicesGrid.Size = new System.Drawing.Size(900, 166);
            _servicesGrid.TabIndex = 2;

            // 
            // colServiceName
            // 
            colServiceName.DataPropertyName = "Name";
            colServiceName.FillWeight = 50F;
            colServiceName.HeaderText = "Service";
            colServiceName.Name = "colServiceName";
            colServiceName.ReadOnly = true;

            // 
            // colCategory
            // 
            colCategory.DataPropertyName = "Category";
            colCategory.FillWeight = 25F;
            colCategory.HeaderText = "Category";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;

            // 
            // colServiceUnitPrice
            // 
            servicePriceStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            servicePriceStyle.Format = "₱#,##0.00";
            colServiceUnitPrice.DataPropertyName = "UnitPrice";
            colServiceUnitPrice.DefaultCellStyle = servicePriceStyle;
            colServiceUnitPrice.FillWeight = 25F;
            colServiceUnitPrice.HeaderText = "Unit price";
            colServiceUnitPrice.Name = "colServiceUnitPrice";
            colServiceUnitPrice.ReadOnly = true;

            // 
            // lblQuantityCaption
            // 
            lblQuantityCaption.AutoSize = true;
            lblQuantityCaption.Font = new System.Drawing.Font("Yu Gothic", 10F);
            lblQuantityCaption.Location = new System.Drawing.Point(16, 250);
            lblQuantityCaption.Name = "lblQuantityCaption";
            lblQuantityCaption.Size = new System.Drawing.Size(67, 18);
            lblQuantityCaption.TabIndex = 3;
            lblQuantityCaption.Text = "Quantity:";

            // 
            // _quantity
            // 
            _quantity.Font = new System.Drawing.Font("Yu Gothic", 10F);
            _quantity.Location = new System.Drawing.Point(92, 246);
            _quantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            _quantity.Name = "_quantity";
            _quantity.Size = new System.Drawing.Size(90, 28);
            _quantity.TabIndex = 4;
            _quantity.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // 
            // btnAddCharge
            // 
            btnAddCharge.BackColor = System.Drawing.Color.LightGreen;
            btnAddCharge.Font = new System.Drawing.Font("Yu Gothic Medium", 10F, System.Drawing.FontStyle.Bold);
            btnAddCharge.Location = new System.Drawing.Point(200, 244);
            btnAddCharge.Name = "btnAddCharge";
            btnAddCharge.Size = new System.Drawing.Size(150, 34);
            btnAddCharge.TabIndex = 5;
            btnAddCharge.Text = "Add Charge";
            btnAddCharge.UseVisualStyleBackColor = false;
            btnAddCharge.Click += Add_Click;

            // 
            // grpCharges
            // 
            grpCharges.BackColor = System.Drawing.Color.WhiteSmoke;
            grpCharges.Controls.Add(_chargesGrid);
            grpCharges.Controls.Add(_total);
            grpCharges.Font = new System.Drawing.Font("Yu Gothic Medium", 11F, System.Drawing.FontStyle.Bold);
            grpCharges.Location = new System.Drawing.Point(16, 312);
            grpCharges.Name = "grpCharges";
            grpCharges.Size = new System.Drawing.Size(932, 288);
            grpCharges.TabIndex = 1;
            grpCharges.TabStop = false;
            grpCharges.Text = "Admission Charges";

            // 
            // _chargesGrid
            // 
            _chargesGrid.AllowUserToAddRows = false;
            _chargesGrid.AllowUserToDeleteRows = false;
            _chargesGrid.AllowUserToResizeRows = false;
            _chargesGrid.AutoGenerateColumns = false;
            _chargesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _chargesGrid.BackgroundColor = System.Drawing.Color.White;
            _chargesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _chargesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colDescription,
                colQty,
                colChargeUnitPrice,
                colLineTotal,
                colChargedAt
            });
            _chargesGrid.Location = new System.Drawing.Point(16, 32);
            _chargesGrid.MultiSelect = false;
            _chargesGrid.Name = "_chargesGrid";
            _chargesGrid.ReadOnly = true;
            _chargesGrid.RowHeadersVisible = false;
            _chargesGrid.RowTemplate.Height = 32;
            _chargesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _chargesGrid.Size = new System.Drawing.Size(900, 206);
            _chargesGrid.TabIndex = 0;

            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.FillWeight = 38F;
            colDescription.HeaderText = "Charge";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;

            // 
            // colQty
            // 
            colQty.DataPropertyName = "Quantity";
            colQty.FillWeight = 8F;
            colQty.HeaderText = "Qty";
            colQty.Name = "colQty";
            colQty.ReadOnly = true;

            // 
            // colChargeUnitPrice
            // 
            chargePriceStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            chargePriceStyle.Format = "₱#,##0.00";
            colChargeUnitPrice.DataPropertyName = "UnitPrice";
            colChargeUnitPrice.DefaultCellStyle = chargePriceStyle;
            colChargeUnitPrice.FillWeight = 16F;
            colChargeUnitPrice.HeaderText = "Unit price";
            colChargeUnitPrice.Name = "colChargeUnitPrice";
            colChargeUnitPrice.ReadOnly = true;

            // 
            // colLineTotal
            // 
            lineTotalStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            lineTotalStyle.Format = "₱#,##0.00";
            colLineTotal.DataPropertyName = "LineTotal";
            colLineTotal.DefaultCellStyle = lineTotalStyle;
            colLineTotal.FillWeight = 16F;
            colLineTotal.HeaderText = "Total";
            colLineTotal.Name = "colLineTotal";
            colLineTotal.ReadOnly = true;

            // 
            // colChargedAt
            // 
            chargedAtStyle.Format = "g";
            colChargedAt.DataPropertyName = "ChargedAt";
            colChargedAt.DefaultCellStyle = chargedAtStyle;
            colChargedAt.FillWeight = 22F;
            colChargedAt.HeaderText = "Added";
            colChargedAt.Name = "colChargedAt";
            colChargedAt.ReadOnly = true;

            // 
            // _total
            // 
            _total.Font = new System.Drawing.Font("Yu Gothic Medium", 11.5F, System.Drawing.FontStyle.Bold);
            _total.Location = new System.Drawing.Point(16, 246);
            _total.Name = "_total";
            _total.Size = new System.Drawing.Size(900, 30);
            _total.TabIndex = 1;
            _total.Text = "Charges total: ₱0.00";
            _total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // btnVoidCharge
            // 
            btnVoidCharge.BackColor = System.Drawing.Color.MistyRose;
            btnVoidCharge.Font = new System.Drawing.Font("Yu Gothic Medium", 10F, System.Drawing.FontStyle.Bold);
            btnVoidCharge.Location = new System.Drawing.Point(622, 610);
            btnVoidCharge.Name = "btnVoidCharge";
            btnVoidCharge.Size = new System.Drawing.Size(190, 38);
            btnVoidCharge.TabIndex = 2;
            btnVoidCharge.Text = "Void Selected Charge";
            btnVoidCharge.UseVisualStyleBackColor = false;
            btnVoidCharge.Click += Void_Click;

            // 
            // btnClose
            // 
            btnClose.BackColor = System.Drawing.Color.White;
            btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnClose.Font = new System.Drawing.Font("Yu Gothic Medium", 10F, System.Drawing.FontStyle.Bold);
            btnClose.Location = new System.Drawing.Point(824, 610);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(124, 38);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;

            // 
            // AddChargesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(964, 661);
            Controls.Add(pnlMain);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddChargesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add Charges";
            Load += AddChargesForm_Load;

            pnlMain.ResumeLayout(false);
            grpServices.ResumeLayout(false);
            grpServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_servicesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_quantity).EndInit();
            grpCharges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_chargesGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}