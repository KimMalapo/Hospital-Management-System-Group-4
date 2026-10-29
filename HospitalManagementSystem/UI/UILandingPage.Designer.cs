namespace UI
{
    partial class UILandingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UILandingPage));
            btnLogout = new Button();
            panel1 = new Panel();
            pictureBox5 = new PictureBox();
            pictureBox1 = new PictureBox();
            lblSignedInAs = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pnlUCHandler = new Panel();
            label4 = new Label();
            btnHome = new Button();
            lblPatients = new Button();
            btnRoom = new Button();
            btnMasterData = new Button();
            label5 = new Label();
            label6 = new Label();
            btnPatientMaintenance = new Button();
            btnSearch = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Red;
            btnLogout.Font = new Font("Yu Gothic Medium", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogout.Location = new Point(33, 993);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(352, 60);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(pictureBox5);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lblSignedInAs);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-1, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1922, 112);
            panel1.TabIndex = 2;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(1546, 23);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(70, 70);
            pictureBox5.TabIndex = 5;
            pictureBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(13, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // lblSignedInAs
            // 
            lblSignedInAs.AutoSize = true;
            lblSignedInAs.Font = new Font("Yu Gothic Medium", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignedInAs.Location = new Point(1762, 48);
            lblSignedInAs.Name = "lblSignedInAs";
            lblSignedInAs.Size = new Size(114, 19);
            lblSignedInAs.TabIndex = 3;
            lblSignedInAs.Text = "{admin/staff}";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic Medium", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(1622, 48);
            label3.Name = "label3";
            label3.Size = new Size(115, 19);
            label3.TabIndex = 2;
            label3.Text = "Signed in as: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(119, 67);
            label2.Name = "label2";
            label2.Size = new Size(227, 29);
            label2.TabIndex = 1;
            label2.Text = "Management System";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(119, 11);
            label1.Name = "label1";
            label1.Size = new Size(217, 49);
            label1.TabIndex = 0;
            label1.Text = "HOSPITAL";
            // 
            // pnlUCHandler
            // 
            pnlUCHandler.BackColor = SystemColors.ControlLight;
            pnlUCHandler.Location = new Point(421, 110);
            pnlUCHandler.Name = "pnlUCHandler";
            pnlUCHandler.Size = new Size(1500, 979);
            pnlUCHandler.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic Medium", 15F, FontStyle.Bold);
            label4.ForeColor = Color.DimGray;
            label4.Location = new Point(33, 145);
            label4.Name = "label4";
            label4.Size = new Size(126, 26);
            label4.TabIndex = 4;
            label4.Text = "Main Menu";
            // 
            // btnHome
            // 
            btnHome.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHome.Location = new Point(33, 213);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(352, 55);
            btnHome.TabIndex = 5;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // lblPatients
            // 
            lblPatients.Font = new Font("Yu Gothic Medium", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPatients.Location = new Point(33, 437);
            lblPatients.Name = "lblPatients";
            lblPatients.Size = new Size(352, 55);
            lblPatients.TabIndex = 6;
            lblPatients.Text = "Patients";
            lblPatients.UseVisualStyleBackColor = true;
            // 
            // btnRoom
            // 
            btnRoom.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRoom.Location = new Point(33, 651);
            btnRoom.Name = "btnRoom";
            btnRoom.Size = new Size(352, 55);
            btnRoom.TabIndex = 7;
            btnRoom.Text = "Room";
            btnRoom.UseVisualStyleBackColor = true;
            // 
            // btnMasterData
            // 
            btnMasterData.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMasterData.Location = new Point(33, 741);
            btnMasterData.Name = "btnMasterData";
            btnMasterData.Size = new Size(352, 55);
            btnMasterData.TabIndex = 8;
            btnMasterData.Text = "Master Data";
            btnMasterData.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(87, 842);
            label5.Name = "label5";
            label5.Size = new Size(248, 31);
            label5.TabIndex = 9;
            label5.Text = "__________________";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic Medium", 15F, FontStyle.Bold);
            label6.ForeColor = Color.DimGray;
            label6.Location = new Point(33, 927);
            label6.Name = "label6";
            label6.Size = new Size(108, 26);
            label6.TabIndex = 10;
            label6.Text = "Account :";
            // 
            // btnPatientMaintenance
            // 
            btnPatientMaintenance.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPatientMaintenance.Location = new Point(33, 543);
            btnPatientMaintenance.Name = "btnPatientMaintenance";
            btnPatientMaintenance.Size = new Size(352, 55);
            btnPatientMaintenance.TabIndex = 11;
            btnPatientMaintenance.Text = "Patient Maintenance";
            btnPatientMaintenance.UseVisualStyleBackColor = true;
            btnPatientMaintenance.Click += btnPatientMaintenance_Click;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(33, 324);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(352, 55);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // UILandingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1920, 1080);
            Controls.Add(btnSearch);
            Controls.Add(btnPatientMaintenance);
            Controls.Add(btnLogout);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnMasterData);
            Controls.Add(btnRoom);
            Controls.Add(lblPatients);
            Controls.Add(btnHome);
            Controls.Add(label4);
            Controls.Add(pnlUCHandler);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UILandingPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += UILandingPage_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogout;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblSignedInAs;
        private PictureBox pictureBox1;
        private Panel pnlUCHandler;
        private Label label4;
        private Button btnHome;
        private Button lblPatients;
        private Button btnRoom;
        private Button btnMasterData;
        private Label label5;
        private Label label6;
        private Button button1;
        private PictureBox pictureBox5;
        private Button button3;
        private Button btnRooms;
        private Button btnPatientMaintenance;
        private Button btnSearch;
    }
}