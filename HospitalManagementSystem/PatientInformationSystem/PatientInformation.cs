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
    public partial class frmPatientInformation : Form
    {
        public frmPatientInformation()
        {
            InitializeComponent();
        }

        private void frmPatientInformation_Load(object sender, EventArgs e)
        {
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            if (txtPatientID.Text == "" ||
              txtFirstName.Text == "" ||
              txtMiddleName.Text == "" ||
              txtLastName.Text == "" ||
              txtAge.Text == "" ||
              cmbGender.SelectedIndex == 0 ||
              txtContactNumber.Text == "")
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            PatientData.PatientID = txtPatientID.Text;
            PatientData.FirstName = txtFirstName.Text;
            PatientData.MiddleName = txtMiddleName.Text;
            PatientData.LastName = txtLastName.Text;
            PatientData.Age = txtAge.Text;
            PatientData.Gender = cmbGender.Text;
            PatientData.ContactNumber = txtContactNumber.Text;

            MessageBox.Show("Patient successfully added!");

            frmPatientSearch form2 = new frmPatientSearch();
            form2.Show();
        }
    }
}
