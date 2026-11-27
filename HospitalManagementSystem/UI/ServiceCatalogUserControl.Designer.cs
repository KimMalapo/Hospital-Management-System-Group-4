namespace UI
{
    partial class ServiceCatalogUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFormCard;
        private System.Windows.Forms.Label lblFormHeader;
        private System.Windows.Forms.Label lblCodeCaption;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblUnitPriceCaption;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnUpdateService;
        private System.Windows.Forms.Button btnDeactivateService;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Label lblFormError;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Label lblSearchCaption;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkActiveOnly;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActive;
        private System.Windows.Forms.Label lblStatusMessage;
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

            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            pnlFormCard = new System.Windows.Forms.Panel();
            lblFormHeader = new System.Windows.Forms.Label();
            lblCodeCaption = new System.Windows.Forms.Label();
            txtCode = new System.Windows.Forms.TextBox();
            lblNameCaption = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            lblUnitPriceCaption = new System.Windows.Forms.Label();
            txtUnitPrice = new System.Windows.Forms.TextBox();
            chkIsActive = new System.Windows.Forms.CheckBox();
            btnAddService = new System.Windows.Forms.Button();
            btnUpdateService = new System.Windows.Forms.Button();
            btnDeactivateService = new System.Windows.Forms.Button();
            btnClearForm = new System.Windows.Forms.Button();
            lblFormError = new System.Windows.Forms.Label();
            pnlGridCard = new System.Windows.Forms.Panel();
            lblGridTitle = new System.Windows.Forms.Label();
            lblSearchCaption = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            chkActiveOnly = new System.Windows.Forms.CheckBox();
            dgvServices = new System.Windows.Forms.DataGridView();
            colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            lblStatusMessage = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);

            pnlFormCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
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
            lblTitle.Size = new System.Drawing.Size(214, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Service catalog";

            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new System.Drawing.Point(37, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(446, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage billable hospital services, standard prices, and availability.";

            // 
            // pnlFormCard
            // 
            pnlFormCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlFormCard.BackColor = System.Drawing.Color.White;
            pnlFormCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlFormCard.Controls.Add(lblFormError);
            pnlFormCard.Controls.Add(btnClearForm);
            pnlFormCard.Controls.Add(btnDeactivateService);
            pnlFormCard.Controls.Add(btnUpdateService);
            pnlFormCard.Controls.Add(btnAddService);
            pnlFormCard.Controls.Add(chkIsActive);
            pnlFormCard.Controls.Add(txtUnitPrice);
            pnlFormCard.Controls.Add(lblUnitPriceCaption);
            pnlFormCard.Controls.Add(txtName);
            pnlFormCard.Controls.Add(lblNameCaption);
            pnlFormCard.Controls.Add(txtCode);
            pnlFormCard.Controls.Add(lblCodeCaption);
            pnlFormCard.Controls.Add(lblFormHeader);
            pnlFormCard.Location = new System.Drawing.Point(35, 96);
            pnlFormCard.Name = "pnlFormCard";
            pnlFormCard.Size = new System.Drawing.Size(1430, 184);
            pnlFormCard.TabIndex = 2;

            // 
            // lblFormHeader
            // 
            lblFormHeader.AutoSize = true;
            lblFormHeader.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            lblFormHeader.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblFormHeader.Location = new System.Drawing.Point(20, 14);
            lblFormHeader.Name = "lblFormHeader";
            lblFormHeader.Size = new System.Drawing.Size(123, 21);
            lblFormHeader.TabIndex = 0;
            lblFormHeader.Text = "Service details";

            // 
            // lblCodeCaption
            // 
            lblCodeCaption.AutoSize = true;
            lblCodeCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblCodeCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblCodeCaption.Location = new System.Drawing.Point(20, 48);
            lblCodeCaption.Name = "lblCodeCaption";
            lblCodeCaption.Size = new System.Drawing.Size(87, 17);
            lblCodeCaption.TabIndex = 1;
            lblCodeCaption.Text = "Service code";

            // 
            // txtCode
            // 
            txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txtCode.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtCode.Location = new System.Drawing.Point(20, 70);
            txtCode.MaxLength = 30;
            txtCode.Name = "txtCode";
            txtCode.Size = new System.Drawing.Size(240, 26);
            txtCode.TabIndex = 2;

            // 
            // lblNameCaption
            // 
            lblNameCaption.AutoSize = true;
            lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblNameCaption.Location = new System.Drawing.Point(290, 48);
            lblNameCaption.Name = "lblNameCaption";
            lblNameCaption.Size = new System.Drawing.Size(92, 17);
            lblNameCaption.TabIndex = 3;
            lblNameCaption.Text = "Service name";

            // 
            // txtName
            // 
            txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtName.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtName.Location = new System.Drawing.Point(290, 70);
            txtName.MaxLength = 150;
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(740, 26);
            txtName.TabIndex = 4;

            // 
            // lblUnitPriceCaption
            // 
            lblUnitPriceCaption.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblUnitPriceCaption.AutoSize = true;
            lblUnitPriceCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblUnitPriceCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblUnitPriceCaption.Location = new System.Drawing.Point(1060, 48);
            lblUnitPriceCaption.Name = "lblUnitPriceCaption";
            lblUnitPriceCaption.Size = new System.Drawing.Size(92, 17);
            lblUnitPriceCaption.TabIndex = 5;
            lblUnitPriceCaption.Text = "Unit price (₱)";

            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtUnitPrice.Location = new System.Drawing.Point(1060, 70);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new System.Drawing.Size(200, 26);
            txtUnitPrice.TabIndex = 6;
            txtUnitPrice.Text = "0.00";

            // 
            // chkIsActive
            // 
            chkIsActive.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            chkIsActive.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            chkIsActive.Location = new System.Drawing.Point(1290, 73);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new System.Drawing.Size(68, 23);
            chkIsActive.TabIndex = 7;
            chkIsActive.Text = "Active";
            chkIsActive.UseVisualStyleBackColor = true;

            // 
            // btnAddService
            // 
            btnAddService.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnAddService.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddService.FlatAppearance.BorderSize = 0;
            btnAddService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddService.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnAddService.ForeColor = System.Drawing.Color.White;
            btnAddService.Location = new System.Drawing.Point(20, 118);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new System.Drawing.Size(145, 36);
            btnAddService.TabIndex = 8;
            btnAddService.Text = "Add service";
            btnAddService.UseVisualStyleBackColor = false;
            btnAddService.Click += AddService_Click;

            // 
            // btnUpdateService
            // 
            btnUpdateService.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            btnUpdateService.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdateService.Enabled = false;
            btnUpdateService.FlatAppearance.BorderSize = 0;
            btnUpdateService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdateService.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnUpdateService.ForeColor = System.Drawing.Color.White;
            btnUpdateService.Location = new System.Drawing.Point(179, 118);
            btnUpdateService.Name = "btnUpdateService";
            btnUpdateService.Size = new System.Drawing.Size(155, 36);
            btnUpdateService.TabIndex = 9;
            btnUpdateService.Text = "Save changes";
            btnUpdateService.UseVisualStyleBackColor = false;
            btnUpdateService.Click += UpdateService_Click;

            // 
            // btnDeactivateService
            // 
            btnDeactivateService.BackColor = System.Drawing.Color.White;
            btnDeactivateService.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDeactivateService.Enabled = false;
            btnDeactivateService.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnDeactivateService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDeactivateService.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnDeactivateService.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnDeactivateService.Location = new System.Drawing.Point(348, 118);
            btnDeactivateService.Name = "btnDeactivateService";
            btnDeactivateService.Size = new System.Drawing.Size(145, 36);
            btnDeactivateService.TabIndex = 10;
            btnDeactivateService.Text = "Deactivate";
            btnDeactivateService.UseVisualStyleBackColor = false;
            btnDeactivateService.Click += DeactivateService_Click;

            // 
            // btnClearForm
            // 
            btnClearForm.BackColor = System.Drawing.Color.White;
            btnClearForm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClearForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClearForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClearForm.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnClearForm.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClearForm.Location = new System.Drawing.Point(507, 118);
            btnClearForm.Name = "btnClearForm";
            btnClearForm.Size = new System.Drawing.Size(115, 36);
            btnClearForm.TabIndex = 11;
            btnClearForm.Text = "Clear";
            btnClearForm.UseVisualStyleBackColor = false;
            btnClearForm.Click += ClearForm_Click;

            // 
            // lblFormError
            // 
            lblFormError.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblFormError.AutoEllipsis = true;
            lblFormError.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblFormError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblFormError.Location = new System.Drawing.Point(640, 125);
            lblFormError.Name = "lblFormError";
            lblFormError.Size = new System.Drawing.Size(768, 22);
            lblFormError.TabIndex = 12;

            // 
            // pnlGridCard
            // 
            pnlGridCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlGridCard.BackColor = System.Drawing.Color.White;
            pnlGridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlGridCard.Controls.Add(lblStatusMessage);
            pnlGridCard.Controls.Add(dgvServices);
            pnlGridCard.Controls.Add(chkActiveOnly);
            pnlGridCard.Controls.Add(txtSearch);
            pnlGridCard.Controls.Add(lblSearchCaption);
            pnlGridCard.Controls.Add(lblGridTitle);
            pnlGridCard.Location = new System.Drawing.Point(35, 294);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Size = new System.Drawing.Size(1430, 642);
            pnlGridCard.TabIndex = 3;

            // 
            // lblGridTitle
            // 
            lblGridTitle.AutoSize = true;
            lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblGridTitle.Location = new System.Drawing.Point(20, 18);
            lblGridTitle.Name = "lblGridTitle";
            lblGridTitle.Size = new System.Drawing.Size(149, 21);
            lblGridTitle.TabIndex = 0;
            lblGridTitle.Text = "Catalog services";

            // 
            // lblSearchCaption
            // 
            lblSearchCaption.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblSearchCaption.AutoSize = true;
            lblSearchCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblSearchCaption.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSearchCaption.Location = new System.Drawing.Point(868, 19);
            lblSearchCaption.Name = "lblSearchCaption";
            lblSearchCaption.Size = new System.Drawing.Size(53, 17);
            lblSearchCaption.TabIndex = 1;
            lblSearchCaption.Text = "Search:";

            // 
            // txtSearch
            // 
            txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtSearch.Location = new System.Drawing.Point(928, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(320, 25);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += Search_TextChanged;

            // 
            // chkActiveOnly
            // 
            chkActiveOnly.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            chkActiveOnly.AutoSize = true;
            chkActiveOnly.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            chkActiveOnly.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            chkActiveOnly.Location = new System.Drawing.Point(1268, 18);
            chkActiveOnly.Name = "chkActiveOnly";
            chkActiveOnly.Size = new System.Drawing.Size(134, 21);
            chkActiveOnly.TabIndex = 3;
            chkActiveOnly.Text = "Active services only";
            chkActiveOnly.UseVisualStyleBackColor = true;
            chkActiveOnly.CheckedChanged += ActiveOnly_CheckedChanged;

            // 
            // dgvServices
            // 
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AllowUserToDeleteRows = false;
            dgvServices.AllowUserToResizeRows = false;
            dgvServices.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvServices.AutoGenerateColumns = false;
            dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvServices.BackgroundColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvServices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgvServices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvServices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerCellStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            headerCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            headerCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            headerCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvServices.ColumnHeadersDefaultCellStyle = headerCellStyle;
            dgvServices.ColumnHeadersHeight = 40;
            dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colCode,
                colName,
                colUnitPrice,
                colIsActive
            });
            defaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = System.Drawing.Color.White;
            defaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            defaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            defaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            defaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvServices.DefaultCellStyle = defaultCellStyle;
            dgvServices.EnableHeadersVisualStyles = false;
            dgvServices.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvServices.Location = new System.Drawing.Point(20, 54);
            dgvServices.MultiSelect = false;
            dgvServices.Name = "dgvServices";
            dgvServices.ReadOnly = true;
            dgvServices.RowHeadersVisible = false;
            dgvServices.RowTemplate.Height = 36;
            dgvServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvServices.Size = new System.Drawing.Size(1388, 534);
            dgvServices.TabIndex = 4;
            dgvServices.SelectionChanged += ServicesGrid_SelectionChanged;

            // 
            // colCode
            // 
            colCode.DataPropertyName = "Code";
            colCode.FillWeight = 20F;
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;

            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.FillWeight = 48F;
            colName.HeaderText = "Service name";
            colName.Name = "colName";
            colName.ReadOnly = true;

            // 
            // colUnitPrice
            // 
            unitPriceCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            unitPriceCellStyle.Format = "₱#,##0.00";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colUnitPrice.DefaultCellStyle = unitPriceCellStyle;
            colUnitPrice.FillWeight = 18F;
            colUnitPrice.HeaderText = "Unit price";
            colUnitPrice.Name = "colUnitPrice";
            colUnitPrice.ReadOnly = true;

            // 
            // colIsActive
            // 
            colIsActive.DataPropertyName = "IsActive";
            colIsActive.FillWeight = 14F;
            colIsActive.HeaderText = "Active";
            colIsActive.Name = "colIsActive";
            colIsActive.ReadOnly = true;

            // 
            // lblStatusMessage
            // 
            lblStatusMessage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblStatusMessage.AutoEllipsis = true;
            lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            lblStatusMessage.Location = new System.Drawing.Point(20, 600);
            lblStatusMessage.Name = "lblStatusMessage";
            lblStatusMessage.Size = new System.Drawing.Size(1388, 24);
            lblStatusMessage.TabIndex = 5;

            // 
            // errProvider
            // 
            errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errProvider.ContainerControl = this;

            // 
            // ServiceCatalogUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlFormCard);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "ServiceCatalogUserControl";
            Size = new System.Drawing.Size(1500, 969);
            Load += ServiceCatalogUserControl_Load;

            pnlFormCard.ResumeLayout(false);
            pnlFormCard.PerformLayout();
            pnlGridCard.ResumeLayout(false);
            pnlGridCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}