namespace UI
{
    partial class RoomManagement
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listRoomNo = new ListView();
            this.listType = new ListView();
            this.listRate = new ListView();
            listStatus = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            this.btnDelete = new Button();
            btnSearch = new Button();
            SuspendLayout();
            // 
            // listRoomNo
            // 
            this.listRoomNo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.listRoomNo.Location = new Point(12, 109);
            this.listRoomNo.Name = "listRoomNo";
            this.listRoomNo.Size = new Size(151, 308);
            this.listRoomNo.TabIndex = 0;
            this.listRoomNo.UseCompatibleStateImageBehavior = false;
            this.listRoomNo.View = View.List;
            // 
            // listType
            // 
            this.listType.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.listType.Location = new Point(191, 109);
            this.listType.Name = "listType";
            this.listType.Size = new Size(151, 308);
            this.listType.TabIndex = 1;
            this.listType.UseCompatibleStateImageBehavior = false;
            this.listType.View = View.List;
            // 
            // listRate
            // 
            this.listRate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.listRate.Location = new Point(377, 109);
            this.listRate.Name = "listRate";
            this.listRate.Size = new Size(151, 308);
            this.listRate.TabIndex = 2;
            this.listRate.UseCompatibleStateImageBehavior = false;
            this.listRate.View = View.List;
            // 
            // listStatus
            // 
            listStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            listStatus.Location = new Point(565, 109);
            listStatus.Name = "listStatus";
            listStatus.Size = new Size(151, 308);
            listStatus.TabIndex = 3;
            listStatus.UseCompatibleStateImageBehavior = false;
            listStatus.View = View.List;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(12, 97);
            label1.Name = "label1";
            label1.Size = new Size(106, 28);
            label1.TabIndex = 4;
            label1.Text = "Room No.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(191, 97);
            label2.Name = "label2";
            label2.Size = new Size(57, 28);
            label2.TabIndex = 5;
            label2.Text = "Type";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(377, 97);
            label3.Name = "label3";
            label3.Size = new Size(55, 28);
            label3.TabIndex = 6;
            label3.Text = "Rate";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(565, 97);
            label4.Name = "label4";
            label4.Size = new Size(71, 28);
            label4.TabIndex = 7;
            label4.Text = "Status";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(335, 46);
            label5.TabIndex = 8;
            label5.Text = "Room Management";
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAdd.Location = new Point(85, 475);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(97, 37);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEdit.Location = new Point(245, 475);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(97, 37);
            btnEdit.TabIndex = 10;
            btnEdit.Text = "EDIT";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnDelete.Location = new Point(377, 475);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(97, 37);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSearch.Location = new Point(539, 475);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(97, 37);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "SEARCH";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // RoomManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 556);
            Controls.Add(btnSearch);
            Controls.Add(this.btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listStatus);
            Controls.Add(this.listRate);
            Controls.Add(this.listType);
            Controls.Add(this.listRoomNo);
            Name = "RoomManagement";
            Text = "RoomManagement";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ListView listView2;
        private ListView listView3;
        private ListView listStatus;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnAdd;
        private Button btnEdit;
        private Button button3;
        private Button btnSearch;
    }
}