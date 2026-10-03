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
    public partial class PatientBilling : Form
    {
        public PatientBilling()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal consultation = 0;
            decimal medicine = 0;
            decimal laboratory = 0;
            decimal room = 0;
            decimal other = 0;
            decimal payment = 0;

            decimal.TryParse(txtConsultation.Text, out consultation);
            decimal.TryParse(txtMedicine.Text, out medicine);
            decimal.TryParse(txtLaboratory.Text, out laboratory);
            decimal.TryParse(txtRoom.Text, out room);
            decimal.TryParse(txtOther.Text, out other);
            decimal.TryParse(txtPayment.Text, out payment);

            decimal total = consultation + medicine + laboratory + room + other;

            decimal change = payment - total;

            lblTotal.Text = "₱ " + total.ToString("0.00");
            lblChange.Text = "₱ " + change.ToString("0.00");
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (txtPatientID.Text == "" ||
      txtPatientName.Text == "")
            {
                MessageBox.Show("Please enter the patient information.",
                                "Patient Billing");
                return;
            }

            if (lblTotal.Text == "₱ 0.00")
            {
                MessageBox.Show("Please calculate the bill first.",
                                "Patient Billing");
                return;
            }

            MessageBox.Show("Bill saved successfully!",
                            "Patient Billing");

            txtPatientID.Clear();
            txtPatientName.Clear();
            txtConsultation.Clear();
            txtMedicine.Clear();
            txtLaboratory.Clear();
            txtRoom.Clear();
            txtOther.Clear();
            txtPayment.Clear();

            lblTotal.Text = "₱ 0.00";
            lblChange.Text = "₱ 0.00";
        }
    }
    
    
}
