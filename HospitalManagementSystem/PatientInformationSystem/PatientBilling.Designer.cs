namespace PatientInformationSystem
{
    partial class PatientBilling
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPatientID = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblServiceItem = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblConsultation = new System.Windows.Forms.Label();
            this.lblMedicine = new System.Windows.Forms.Label();
            this.lblLaboratory = new System.Windows.Forms.Label();
            this.lblRoomAdmission = new System.Windows.Forms.Label();
            this.lblOther = new System.Windows.Forms.Label();
            this.txtPatientID = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.txtConsultation = new System.Windows.Forms.TextBox();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.txtLaboratory = new System.Windows.Forms.TextBox();
            this.txtMedicine = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPaymentChange = new System.Windows.Forms.Label();
            this.lblPayment = new System.Windows.Forms.Label();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnSaveBill = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPatientName);
            this.groupBox1.Controls.Add(this.txtPatientID);
            this.groupBox1.Controls.Add(this.lblPatientName);
            this.groupBox1.Controls.Add(this.lblPatientID);
            this.groupBox1.Location = new System.Drawing.Point(22, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient Information";
            // 
            // lblPatientID
            // 
            this.lblPatientID.AutoSize = true;
            this.lblPatientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientID.Location = new System.Drawing.Point(42, 40);
            this.lblPatientID.Name = "lblPatientID";
            this.lblPatientID.Size = new System.Drawing.Size(78, 16);
            this.lblPatientID.TabIndex = 2;
            this.lblPatientID.Text = "Patient ID:";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(16, 75);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(104, 16);
            this.lblPatientName.TabIndex = 4;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSaveBill);
            this.groupBox2.Controls.Add(this.btnCalculate);
            this.groupBox2.Controls.Add(this.lblChange);
            this.groupBox2.Controls.Add(this.txtPayment);
            this.groupBox2.Controls.Add(this.lblPayment);
            this.groupBox2.Controls.Add(this.lblPaymentChange);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.lblTotalAmount);
            this.groupBox2.Controls.Add(this.txtMedicine);
            this.groupBox2.Controls.Add(this.txtLaboratory);
            this.groupBox2.Controls.Add(this.txtRoom);
            this.groupBox2.Controls.Add(this.txtOther);
            this.groupBox2.Controls.Add(this.txtConsultation);
            this.groupBox2.Controls.Add(this.lblOther);
            this.groupBox2.Controls.Add(this.lblRoomAdmission);
            this.groupBox2.Controls.Add(this.lblLaboratory);
            this.groupBox2.Controls.Add(this.lblMedicine);
            this.groupBox2.Controls.Add(this.lblConsultation);
            this.groupBox2.Controls.Add(this.lblAmount);
            this.groupBox2.Controls.Add(this.lblServiceItem);
            this.groupBox2.Location = new System.Drawing.Point(22, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 570);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Billing Details";
            // 
            // lblServiceItem
            // 
            this.lblServiceItem.AutoSize = true;
            this.lblServiceItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceItem.Location = new System.Drawing.Point(16, 47);
            this.lblServiceItem.Name = "lblServiceItem";
            this.lblServiceItem.Size = new System.Drawing.Size(102, 16);
            this.lblServiceItem.TabIndex = 5;
            this.lblServiceItem.Text = "Service / Item";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(245, 47);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(58, 16);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Amount";
            // 
            // lblConsultation
            // 
            this.lblConsultation.AutoSize = true;
            this.lblConsultation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsultation.Location = new System.Drawing.Point(16, 118);
            this.lblConsultation.Name = "lblConsultation";
            this.lblConsultation.Size = new System.Drawing.Size(92, 16);
            this.lblConsultation.TabIndex = 8;
            this.lblConsultation.Text = "Consultation";
            // 
            // lblMedicine
            // 
            this.lblMedicine.AutoSize = true;
            this.lblMedicine.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicine.Location = new System.Drawing.Point(16, 171);
            this.lblMedicine.Name = "lblMedicine";
            this.lblMedicine.Size = new System.Drawing.Size(70, 16);
            this.lblMedicine.TabIndex = 9;
            this.lblMedicine.Text = "Medicine";
            // 
            // lblLaboratory
            // 
            this.lblLaboratory.AutoSize = true;
            this.lblLaboratory.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLaboratory.Location = new System.Drawing.Point(16, 226);
            this.lblLaboratory.Name = "lblLaboratory";
            this.lblLaboratory.Size = new System.Drawing.Size(82, 16);
            this.lblLaboratory.TabIndex = 10;
            this.lblLaboratory.Text = "Laboratory";
            // 
            // lblRoomAdmission
            // 
            this.lblRoomAdmission.AutoSize = true;
            this.lblRoomAdmission.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomAdmission.Location = new System.Drawing.Point(16, 287);
            this.lblRoomAdmission.Name = "lblRoomAdmission";
            this.lblRoomAdmission.Size = new System.Drawing.Size(125, 16);
            this.lblRoomAdmission.TabIndex = 11;
            this.lblRoomAdmission.Text = "Room/Admission";
            // 
            // lblOther
            // 
            this.lblOther.AutoSize = true;
            this.lblOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOther.Location = new System.Drawing.Point(16, 346);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(44, 16);
            this.lblOther.TabIndex = 12;
            this.lblOther.Text = "Other";
            // 
            // txtPatientID
            // 
            this.txtPatientID.Location = new System.Drawing.Point(156, 33);
            this.txtPatientID.Name = "txtPatientID";
            this.txtPatientID.Size = new System.Drawing.Size(134, 22);
            this.txtPatientID.TabIndex = 5;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(156, 69);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(233, 22);
            this.txtPatientName.TabIndex = 6;
            // 
            // txtConsultation
            // 
            this.txtConsultation.Location = new System.Drawing.Point(248, 112);
            this.txtConsultation.Name = "txtConsultation";
            this.txtConsultation.Size = new System.Drawing.Size(90, 22);
            this.txtConsultation.TabIndex = 13;
            // 
            // txtOther
            // 
            this.txtOther.Location = new System.Drawing.Point(248, 340);
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(90, 22);
            this.txtOther.TabIndex = 14;
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(248, 281);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(90, 22);
            this.txtRoom.TabIndex = 15;
            // 
            // txtLaboratory
            // 
            this.txtLaboratory.Location = new System.Drawing.Point(248, 220);
            this.txtLaboratory.Name = "txtLaboratory";
            this.txtLaboratory.Size = new System.Drawing.Size(90, 22);
            this.txtLaboratory.TabIndex = 16;
            // 
            // txtMedicine
            // 
            this.txtMedicine.Location = new System.Drawing.Point(248, 168);
            this.txtMedicine.Name = "txtMedicine";
            this.txtMedicine.Size = new System.Drawing.Size(90, 22);
            this.txtMedicine.TabIndex = 17;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(174, 400);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(43, 16);
            this.lblTotalAmount.TabIndex = 18;
            this.lblTotalAmount.Text = "Total";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(246, 400);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 16);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "label1";
            // 
            // lblPaymentChange
            // 
            this.lblPaymentChange.AutoSize = true;
            this.lblPaymentChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentChange.Location = new System.Drawing.Point(157, 472);
            this.lblPaymentChange.Name = "lblPaymentChange";
            this.lblPaymentChange.Size = new System.Drawing.Size(60, 16);
            this.lblPaymentChange.TabIndex = 20;
            this.lblPaymentChange.Text = "Change";
            this.lblPaymentChange.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblPayment
            // 
            this.lblPayment.AutoSize = true;
            this.lblPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayment.Location = new System.Drawing.Point(150, 432);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(67, 16);
            this.lblPayment.TabIndex = 21;
            this.lblPayment.Text = "Payment";
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(248, 429);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(90, 22);
            this.txtPayment.TabIndex = 22;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(246, 472);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(44, 16);
            this.lblChange.TabIndex = 23;
            this.lblChange.Text = "label2";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(360, 465);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(91, 23);
            this.btnCalculate.TabIndex = 24;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.Location = new System.Drawing.Point(360, 526);
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Size = new System.Drawing.Size(91, 23);
            this.btnSaveBill.TabIndex = 25;
            this.btnSaveBill.Text = "Save Bill";
            this.btnSaveBill.UseVisualStyleBackColor = true;
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // PatientBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 786);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PatientBilling";
            this.Text = "PatientBilling";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblConsultation;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblServiceItem;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.TextBox txtPatientID;
        private System.Windows.Forms.TextBox txtMedicine;
        private System.Windows.Forms.TextBox txtLaboratory;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.TextBox txtConsultation;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.Label lblRoomAdmission;
        private System.Windows.Forms.Label lblLaboratory;
        private System.Windows.Forms.Label lblMedicine;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Label lblPaymentChange;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Button btnSaveBill;
    }
}