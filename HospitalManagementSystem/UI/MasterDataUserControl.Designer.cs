namespace UI
{
    partial class MasterDataUserControl
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
            pnlMainHandler = new Panel();
            lblTitle = new Label();
            grpEntry = new GroupBox();
            lblDataType = new Label();
            cmbDataType = new ComboBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblCode = new Label();
            txtCode = new TextBox();
            lblRate = new Label();
            numRate = new NumericUpDown();
            chkActive = new CheckBox();
            btnClear = new Button();
            grpGrid = new GroupBox();
            dgvMasterData = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colDataType = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colRate = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            pnlMainHandler.SuspendLayout();
            grpEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).BeginInit();
            grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMasterData).BeginInit();
            SuspendLayout();
            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpEntry);
            pnlMainHandler.Controls.Add(grpGrid);
            pnlMainHandler.Controls.Add(btnAdd);
            pnlMainHandler.Controls.Add(btnEdit);
            pnlMainHandler.Controls.Add(btnDelete);
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
            lblTitle.Size = new Size(593, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Master Data Management - Reference";
            // 
            // grpEntry
            // 
            grpEntry.BackColor = Color.WhiteSmoke;
            grpEntry.Controls.Add(lblDataType);
            grpEntry.Controls.Add(cmbDataType);
            grpEntry.Controls.Add(lblDescription);
            grpEntry.Controls.Add(txtDescription);
            grpEntry.Controls.Add(lblCode);
            grpEntry.Controls.Add(txtCode);
            grpEntry.Controls.Add(lblRate);
            grpEntry.Controls.Add(numRate);
            grpEntry.Controls.Add(chkActive);
            grpEntry.Controls.Add(btnClear);
            grpEntry.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpEntry.Location = new Point(35, 85);
            grpEntry.Name = "grpEntry";
            grpEntry.Size = new Size(460, 750);
            grpEntry.TabIndex = 1;
            grpEntry.TabStop = false;
            grpEntry.Text = "Reference Data Entry";
            // 
            // lblDataType
            // 
            lblDataType.AutoSize = true;
            lblDataType.Font = new Font("Yu Gothic", 11F);
            lblDataType.Location = new Point(25, 50);
            lblDataType.Name = "lblDataType";
            lblDataType.Size = new Size(84, 20);
            lblDataType.TabIndex = 0;
            lblDataType.Text = "Data Type:";
            // 
            // cmbDataType
            // 
            cmbDataType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDataType.Font = new Font("Yu Gothic", 11F);
            cmbDataType.FormattingEnabled = true;
            cmbDataType.Location = new Point(25, 75);
            cmbDataType.Name = "cmbDataType";
            cmbDataType.Size = new Size(410, 27);
            cmbDataType.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Yu Gothic", 11F);
            lblDescription.Location = new Point(25, 130);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(157, 20);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Name / Description:";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Yu Gothic", 11F);
            txtDescription.Location = new Point(25, 155);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(410, 31);
            txtDescription.TabIndex = 3;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Yu Gothic", 11F);
            lblCode.Location = new Point(25, 210);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(51, 20);
            lblCode.TabIndex = 4;
            lblCode.Text = "Code:";
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Yu Gothic", 11F);
            txtCode.Location = new Point(25, 235);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(410, 31);
            txtCode.TabIndex = 5;
            // 
            // lblRate
            // 
            lblRate.AutoSize = true;
            lblRate.Font = new Font("Yu Gothic", 11F);
            lblRate.Location = new Point(25, 290);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(81, 20);
            lblRate.TabIndex = 6;
            lblRate.Text = "Rate/Cost:";
            // 
            // numRate
            // 
            numRate.DecimalPlaces = 2;
            numRate.Font = new Font("Yu Gothic", 11F);
            numRate.Location = new Point(25, 315);
            numRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numRate.Name = "numRate";
            numRate.Size = new Size(200, 31);
            numRate.TabIndex = 7;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Font = new Font("Yu Gothic", 11F);
            chkActive.Location = new Point(25, 385);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(73, 24);
            chkActive.TabIndex = 8;
            chkActive.Text = "Active";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            btnClear.Location = new Point(25, 450);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(410, 45);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear Form";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // grpGrid
            // 
            grpGrid.BackColor = Color.WhiteSmoke;
            grpGrid.Controls.Add(dgvMasterData);
            grpGrid.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpGrid.Location = new Point(515, 85);
            grpGrid.Name = "grpGrid";
            grpGrid.Size = new Size(935, 750);
            grpGrid.TabIndex = 2;
            grpGrid.TabStop = false;
            grpGrid.Text = "Reference Data List";
            // 
            // dgvMasterData
            // 
            dgvMasterData.AllowUserToAddRows = false;
            dgvMasterData.AllowUserToDeleteRows = false;
            dgvMasterData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMasterData.BackgroundColor = Color.White;
            dgvMasterData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMasterData.Columns.AddRange(new DataGridViewColumn[] { colID, colDataType, colDescription, colCode, colRate, colStatus });
            dgvMasterData.Location = new Point(25, 40);
            dgvMasterData.MultiSelect = false;
            dgvMasterData.Name = "dgvMasterData";
            dgvMasterData.ReadOnly = true;
            dgvMasterData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMasterData.Size = new Size(885, 685);
            dgvMasterData.TabIndex = 0;
            dgvMasterData.SelectionChanged += dgvMasterData_SelectionChanged;
            // 
            // colID
            // 
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colDataType
            // 
            colDataType.HeaderText = "Data Type";
            colDataType.Name = "colDataType";
            colDataType.ReadOnly = true;
            // 
            // colDescription
            // 
            colDescription.HeaderText = "Name/Description";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // colCode
            // 
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            // 
            // colRate
            // 
            colRate.HeaderText = "Rate/Cost";
            colRate.Name = "colRate";
            colRate.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(35, 855);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(250, 60);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "+ Add Master Data";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightSkyBlue;
            btnEdit.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnEdit.Location = new Point(305, 855);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(250, 60);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Edit Selected";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.MistyRose;
            btnDelete.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(575, 855);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(250, 60);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete Selected";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // MasterDataUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "MasterDataUserControl";
            Size = new Size(1500, 979);
            Load += MasterDataUserControl_Load;
            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpEntry.ResumeLayout(false);
            grpEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).EndInit();
            grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMasterData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainHandler;
        private Label lblTitle;
        private GroupBox grpEntry;
        private Label lblDataType;
        private ComboBox cmbDataType;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblCode;
        private TextBox txtCode;
        private Label lblRate;
        private NumericUpDown numRate;
        private CheckBox chkActive;
        private Button btnClear;
        private GroupBox grpGrid;
        private DataGridView dgvMasterData;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colDataType;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colRate;
        private DataGridViewTextBoxColumn colStatus;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
    }
}