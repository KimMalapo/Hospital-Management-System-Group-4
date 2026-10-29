namespace UI
{
    partial class RoomManagementUserControl
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
            grpRoomDetails = new GroupBox();
            lblRoomNo = new Label();
            txtRoomNo = new TextBox();
            lblRoomType = new Label();
            cmbRoomType = new ComboBox();
            lblBedNo = new Label();
            txtBedNo = new TextBox();
            lblRate = new Label();
            numRate = new NumericUpDown();
            lblAvailability = new Label();
            cmbAvailability = new ComboBox();
            btnClear = new Button();
            grpList = new GroupBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvRooms = new DataGridView();
            colRoomNo = new DataGridViewTextBoxColumn();
            colRoomType = new DataGridViewTextBoxColumn();
            colBedNo = new DataGridViewTextBoxColumn();
            colRate = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            pnlMainHandler.SuspendLayout();
            grpRoomDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).BeginInit();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
            SuspendLayout();
            // 
            // pnlMainHandler
            // 
            pnlMainHandler.BackColor = SystemColors.ControlLight;
            pnlMainHandler.Controls.Add(lblTitle);
            pnlMainHandler.Controls.Add(grpRoomDetails);
            pnlMainHandler.Controls.Add(grpList);
            pnlMainHandler.Controls.Add(btnAdd);
            pnlMainHandler.Controls.Add(btnUpdate);
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
            lblTitle.Size = new Size(330, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hospital Room Management";
            // 
            // grpRoomDetails
            // 
            grpRoomDetails.BackColor = Color.WhiteSmoke;
            grpRoomDetails.Controls.Add(lblRoomNo);
            grpRoomDetails.Controls.Add(txtRoomNo);
            grpRoomDetails.Controls.Add(lblRoomType);
            grpRoomDetails.Controls.Add(cmbRoomType);
            grpRoomDetails.Controls.Add(lblBedNo);
            grpRoomDetails.Controls.Add(txtBedNo);
            grpRoomDetails.Controls.Add(lblRate);
            grpRoomDetails.Controls.Add(numRate);
            grpRoomDetails.Controls.Add(lblAvailability);
            grpRoomDetails.Controls.Add(cmbAvailability);
            grpRoomDetails.Controls.Add(btnClear);
            grpRoomDetails.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpRoomDetails.Location = new Point(35, 85);
            grpRoomDetails.Name = "grpRoomDetails";
            grpRoomDetails.Size = new Size(480, 750);
            grpRoomDetails.TabIndex = 1;
            grpRoomDetails.TabStop = false;
            grpRoomDetails.Text = "Room Details";
            // 
            // lblRoomNo
            // 
            lblRoomNo.AutoSize = true;
            lblRoomNo.Font = new Font("Yu Gothic", 11F);
            lblRoomNo.Location = new Point(25, 55);
            lblRoomNo.Name = "lblRoomNo";
            lblRoomNo.Size = new Size(81, 20);
            lblRoomNo.TabIndex = 0;
            lblRoomNo.Text = "Room No:";
            // 
            // txtRoomNo
            // 
            txtRoomNo.Font = new Font("Yu Gothic", 11F);
            txtRoomNo.Location = new Point(160, 50);
            txtRoomNo.Name = "txtRoomNo";
            txtRoomNo.Size = new Size(290, 31);
            txtRoomNo.TabIndex = 1;
            // 
            // lblRoomType
            // 
            lblRoomType.AutoSize = true;
            lblRoomType.Font = new Font("Yu Gothic", 11F);
            lblRoomType.Location = new Point(25, 120);
            lblRoomType.Name = "lblRoomType";
            lblRoomType.Size = new Size(92, 20);
            lblRoomType.TabIndex = 2;
            lblRoomType.Text = "Room Type:";
            // 
            // cmbRoomType
            // 
            cmbRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoomType.Font = new Font("Yu Gothic", 11F);
            cmbRoomType.FormattingEnabled = true;
            cmbRoomType.Location = new Point(160, 115);
            cmbRoomType.Name = "cmbRoomType";
            cmbRoomType.Size = new Size(290, 27);
            cmbRoomType.TabIndex = 3;
            // 
            // lblBedNo
            // 
            lblBedNo.AutoSize = true;
            lblBedNo.Font = new Font("Yu Gothic", 11F);
            lblBedNo.Location = new Point(25, 185);
            lblBedNo.Name = "lblBedNo";
            lblBedNo.Size = new Size(64, 20);
            lblBedNo.TabIndex = 4;
            lblBedNo.Text = "Bed No:";
            // 
            // txtBedNo
            // 
            txtBedNo.Font = new Font("Yu Gothic", 11F);
            txtBedNo.Location = new Point(160, 180);
            txtBedNo.Name = "txtBedNo";
            txtBedNo.Size = new Size(290, 31);
            txtBedNo.TabIndex = 5;
            // 
            // lblRate
            // 
            lblRate.AutoSize = true;
            lblRate.Font = new Font("Yu Gothic", 11F);
            lblRate.Location = new Point(25, 250);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(89, 20);
            lblRate.TabIndex = 6;
            lblRate.Text = "Room Rate:";
            // 
            // numRate
            // 
            numRate.DecimalPlaces = 2;
            numRate.Font = new Font("Yu Gothic", 11F);
            numRate.Location = new Point(160, 245);
            numRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numRate.Name = "numRate";
            numRate.Size = new Size(290, 31);
            numRate.TabIndex = 7;
            // 
            // lblAvailability
            // 
            lblAvailability.AutoSize = true;
            lblAvailability.Font = new Font("Yu Gothic", 11F);
            lblAvailability.Location = new Point(25, 315);
            lblAvailability.Name = "lblAvailability";
            lblAvailability.Size = new Size(89, 20);
            lblAvailability.TabIndex = 8;
            lblAvailability.Text = "Availability:";
            // 
            // cmbAvailability
            // 
            cmbAvailability.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAvailability.Font = new Font("Yu Gothic", 11F);
            cmbAvailability.FormattingEnabled = true;
            cmbAvailability.Location = new Point(160, 310);
            cmbAvailability.Name = "cmbAvailability";
            cmbAvailability.Size = new Size(290, 27);
            cmbAvailability.TabIndex = 9;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Yu Gothic Medium", 11F, FontStyle.Bold);
            btnClear.Location = new Point(160, 380);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(290, 45);
            btnClear.TabIndex = 10;
            btnClear.Text = "Clear Form";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // grpList
            // 
            grpList.BackColor = Color.WhiteSmoke;
            grpList.Controls.Add(txtSearch);
            grpList.Controls.Add(lblSearch);
            grpList.Controls.Add(dgvRooms);
            grpList.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            grpList.Location = new Point(535, 85);
            grpList.Name = "grpList";
            grpList.Size = new Size(915, 750);
            grpList.TabIndex = 2;
            grpList.TabStop = false;
            grpList.Text = "Room Records";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Yu Gothic", 11F);
            txtSearch.Location = new Point(610, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(280, 31);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Yu Gothic", 11F);
            lblSearch.Location = new Point(485, 40);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(111, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search Room:";
            // 
            // dgvRooms
            // 
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AllowUserToDeleteRows = false;
            dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRooms.BackgroundColor = Color.White;
            dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRooms.Columns.AddRange(new DataGridViewColumn[] { colRoomNo, colRoomType, colBedNo, colRate, colStatus });
            dgvRooms.Location = new Point(25, 80);
            dgvRooms.MultiSelect = false;
            dgvRooms.Name = "dgvRooms";
            dgvRooms.ReadOnly = true;
            dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRooms.Size = new Size(865, 645);
            dgvRooms.TabIndex = 2;
            dgvRooms.SelectionChanged += dgvRooms_SelectionChanged;
            // 
            // colRoomNo
            // 
            colRoomNo.HeaderText = "Room No.";
            colRoomNo.Name = "colRoomNo";
            colRoomNo.ReadOnly = true;
            // 
            // colRoomType
            // 
            colRoomType.HeaderText = "Room Type";
            colRoomType.Name = "colRoomType";
            colRoomType.ReadOnly = true;
            // 
            // colBedNo
            // 
            colBedNo.HeaderText = "Bed No.";
            colBedNo.Name = "colBedNo";
            colBedNo.ReadOnly = true;
            // 
            // colRate
            // 
            colRate.HeaderText = "Room Rate (Daily)";
            colRate.Name = "colRate";
            colRate.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Availability";
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
            btnAdd.Text = "+ Add Room";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightSkyBlue;
            btnUpdate.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnUpdate.Location = new Point(300, 855);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(250, 60);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update Room";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.MistyRose;
            btnDelete.Font = new Font("Yu Gothic Medium", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(565, 855);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(250, 60);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete Room";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // RoomManagementUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMainHandler);
            Name = "RoomManagementUserControl";
            Size = new Size(1500, 979);
            Load += RoomManagementUserControl_Load;
            pnlMainHandler.ResumeLayout(false);
            pnlMainHandler.PerformLayout();
            grpRoomDetails.ResumeLayout(false);
            grpRoomDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRate).EndInit();
            grpList.ResumeLayout(false);
            grpList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainHandler;
        private Label lblTitle;
        private GroupBox grpRoomDetails;
        private Label lblRoomNo;
        private TextBox txtRoomNo;
        private Label lblRoomType;
        private ComboBox cmbRoomType;
        private Label lblBedNo;
        private TextBox txtBedNo;
        private Label lblRate;
        private NumericUpDown numRate;
        private Label lblAvailability;
        private ComboBox cmbAvailability;
        private Button btnClear;
        private GroupBox grpList;
        private TextBox txtSearch;
        private Label lblSearch;
        private DataGridView dgvRooms;
        private DataGridViewTextBoxColumn colRoomNo;
        private DataGridViewTextBoxColumn colRoomType;
        private DataGridViewTextBoxColumn colBedNo;
        private DataGridViewTextBoxColumn colRate;
        private DataGridViewTextBoxColumn colStatus;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}