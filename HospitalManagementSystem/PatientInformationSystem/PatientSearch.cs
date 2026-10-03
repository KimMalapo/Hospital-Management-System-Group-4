using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientInformationSystem
{
    public partial class frmPatientSearch : Form
    {
        public frmPatientSearch()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPatientID.Text == "")
            {
                MessageBox.Show("Please enter a Patient ID.");
                return;
            }

            if (txtPatientID.Text == PatientData.PatientID)
            {
                lblPatientName.Text = "Patient Name: " +
                    PatientData.LastName + " " +
                    PatientData.FirstName + " " +
                    PatientData.MiddleName;

                lblAge.Text = "Age: " + PatientData.Age;
                lblGender.Text = "Gender: " + PatientData.Gender;
                lblContactNumber.Text = "Contact Number: " + PatientData.ContactNumber;
            }
            else
            {
                MessageBox.Show("Patient not found.");
            }

        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            PatientBilling billingForm = new PatientBilling();

            billingForm.FormClosed += (s, args) =>
            {
                this.Show();
            };

            billingForm.Show();

            this.Hide();
        }
    }

}
