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
            listStatus = new ListView();
            label4 = new Label();
            label5 = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnSearch = new Button();
            listRoomNo = new ListView();
            listType = new ListView();
            listRate = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnDelete = new Button();
            SuspendLayout();
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
            label5.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(324, 46);
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
            // listRoomNo
            // 
            listRoomNo.Location = new Point(12, 109);
            listRoomNo.Name = "listRoomNo";
            listRoomNo.Size = new Size(151, 308);
            listRoomNo.TabIndex = 13;
            listRoomNo.UseCompatibleStateImageBehavior = false;
            // 
            // listType
            // 
            listType.Location = new Point(191, 109);
            listType.Name = "listType";
            listType.Size = new Size(151, 308);
            listType.TabIndex = 14;
            listType.UseCompatibleStateImageBehavior = false;
            // 
            // listRate
            // 
            listRate.Location = new Point(377, 109);
            listRate.Name = "listRate";
            listRate.Size = new Size(151, 308);
            listRate.TabIndex = 15;
            listRate.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(12, 97);
            label1.Name = "label1";
            label1.Size = new Size(106, 28);
            label1.TabIndex = 16;
            label1.Text = "Room No.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(191, 97);
            label2.Name = "label2";
            label2.Size = new Size(57, 28);
            label2.TabIndex = 17;
            label2.Text = "Type";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(377, 97);
            label3.Name = "label3";
            label3.Size = new Size(55, 28);
            label3.TabIndex = 18;
            label3.Text = "Rate";
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDelete.Location = new Point(392, 475);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(97, 37);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // RoomManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 556);
            Controls.Add(btnDelete);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listRate);
            Controls.Add(listType);
            Controls.Add(listRoomNo);
            Controls.Add(btnSearch);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(listStatus);
            Name = "RoomManagement";
            Text = "RoomManagement";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listStatus;
        private Label label4;
        private Label label5;
        private Button btnAdd;
        private Button btnEdit;

        private Button btnSearch;
        private ListView listRoomNo;
        private ListView listType;
        private ListView listRate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnDelete;
    }
}