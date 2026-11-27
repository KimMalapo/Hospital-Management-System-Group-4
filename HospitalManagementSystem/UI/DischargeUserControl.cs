using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class DischargeUserControl : UserControl
    {
        private readonly int? _initialAdmissionId;
        private AdmissionService? _admissionService;
        private BillingService? _billingService;
        private ChargeService? _chargeService;
        private PaymentService? _paymentService;
        private Admission? _admission;
        private DischargeRecord? _finalRecord;
        private bool _loading;

        public event EventHandler? PatientDischarged;

        public DischargeUserControl() : this(null)
        {
        }

        public DischargeUserControl(int? admissionId)
        {
            _initialAdmissionId = admissionId;
            InitializeComponent();
        }

        private void DischargeUserControl_Load(object? sender, EventArgs e)
        {
            try
            {
                var connectionString = Config.GetConnectionString();
                _admissionService = new AdmissionService(connectionString);
                _billingService = new BillingService(connectionString);
                _chargeService = new ChargeService(connectionString);
                _paymentService = new PaymentService(connectionString);

                LoadAdmissions(_initialAdmissionId);
            }
            catch (Exception ex)
            {
                Logger.Error("DischargeUserControl.Load failed", ex);
                SetMessage("Admissions could not be loaded. Check the database connection and try again.");
            }
        }

        private void LoadAdmissions(int? selectAdmissionId)
        {
            if (_admissionService == null)
                return;

            _loading = true;
            try
            {
                var admissions = _admissionService.Search(string.Empty);
                admissions.Columns.Add("AdmissionDisplay", typeof(string));
                foreach (DataRow row in admissions.Rows)
                {
                    row["AdmissionDisplay"] = $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
                }

                cmbAdmissions.DataSource = admissions;
                cmbAdmissions.DisplayMember = "AdmissionDisplay";
                cmbAdmissions.ValueMember = "AdmissionID";

                if (selectAdmissionId.HasValue)
                    cmbAdmissions.SelectedValue = selectAdmissionId.Value;
                else if (cmbAdmissions.Items.Count > 0)
                    cmbAdmissions.SelectedIndex = 0;
            }
            finally
            {
                _loading = false;
            }

            RefreshAdmissionView();
        }

        private void Admissions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_loading)
            {
                lblMessage.Text = string.Empty;
                RefreshAdmissionView();
            }
        }

        private void RefreshAdmissionView(bool preserveMessage = false)
        {
            errProvider.Clear();
            if (!preserveMessage)
                lblMessage.Text = string.Empty;

            _finalRecord = null;

            if (_admissionService == null || _billingService == null || _chargeService == null || _paymentService == null ||
                cmbAdmissions.SelectedValue is not int admissionId)
            {
                ClearView();
                return;
            }

            try
            {
                _admission = _admissionService.GetById(admissionId);
                if (_admission == null)
                {
                    ClearView();
                    return;
                }

                if (string.Equals(_admission.Status, AdmissionStatus.Discharged, StringComparison.OrdinalIgnoreCase))
                {
                    // AC #3: Load complete final record for discharged admission
                    _finalRecord = _admissionService.GetDischargeRecord(admissionId);
                    BindDischargeRecord(_finalRecord);

                    lblEligibilityBanner.ForeColor = Color.FromArgb(21, 128, 61);
                    lblEligibilityBanner.Text = $"Discharged on {FormatDate(_finalRecord.Admission.DischargeDate)} — Final patient record is ready.";
                    txtDischargeSummary.Text = _finalRecord.Admission.DischargeSummary ?? string.Empty;
                    txtDischargeSummary.ReadOnly = true;
                    btnDischarge.Enabled = false;
                    btnPrintRecord.Enabled = true;
                    return;
                }

                // Active or Cancelled admission: check discharge readiness (AC #1)
                var check = _admissionService.CheckDischarge(admissionId);
                lblEligibilityBanner.ForeColor = check.CanDischarge
                    ? Color.FromArgb(21, 128, 61)
                    : Color.FromArgb(180, 83, 9);
                lblEligibilityBanner.Text = check.Message;

                var bill = _billingService.GetByAdmission(admissionId);
                var charges = _chargeService.GetByAdmission(admissionId, includeVoided: false);
                var payments = bill != null ? _paymentService.GetByBill(bill.BillID) : new DataTable();

                BindActivePreview(_admission, bill, charges, payments);

                txtDischargeSummary.Text = _admission.DischargeSummary ?? string.Empty;
                txtDischargeSummary.ReadOnly = !check.CanDischarge;
                btnDischarge.Enabled = check.CanDischarge;
                btnPrintRecord.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error("DischargeUserControl.RefreshAdmissionView failed", ex);
                SetMessage("Discharge details could not be loaded. Please try again.");
            }
        }

        private void BindActivePreview(Admission admission, Bill? bill, DataTable charges, DataTable payments)
        {
            lblPatientValue.Text = $"{admission.PatientName} (ID: {admission.PatientID})";
            lblRoomValue.Text = admission.RoomNo ?? "-";
            lblDiagnosisValue.Text = admission.DiagnosisCode ?? "-";
            lblAdmitDateValue.Text = FormatDate(admission.AdmitDate);
            lblDischargeDateValue.Text = "Not discharged yet";

            lblBilledValue.Text = FormatMoney(bill?.TotalAmount ?? 0m);
            lblPaidValue.Text = FormatMoney(bill?.AmountPaid ?? 0m);
            lblBalanceValue.Text = FormatMoney(bill?.Balance ?? 0m);

            dgvCharges.DataSource = charges;
            dgvPayments.DataSource = payments;
        }

        private void BindDischargeRecord(DischargeRecord record)
        {
            var a = record.Admission;
            var b = record.Bill;

            lblPatientValue.Text = $"{a.PatientName} (ID: {a.PatientID})";
            lblRoomValue.Text = a.RoomNo ?? "-";
            lblDiagnosisValue.Text = a.DiagnosisCode ?? "-";
            lblAdmitDateValue.Text = FormatDate(a.AdmitDate);
            lblDischargeDateValue.Text = FormatDate(a.DischargeDate);

            lblBilledValue.Text = FormatMoney(b?.TotalAmount ?? 0m);
            lblPaidValue.Text = FormatMoney(b?.AmountPaid ?? 0m);
            lblBalanceValue.Text = FormatMoney(b?.Balance ?? 0m);

            dgvCharges.DataSource = record.Charges;
            dgvPayments.DataSource = record.Payments;
        }

        private void Discharge_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            lblMessage.Text = string.Empty;

            if (_admission == null || _admissionService == null)
                return;

            var summary = txtDischargeSummary.Text.Trim();
            if (summary.Length > 1000)
            {
                errProvider.SetError(txtDischargeSummary, "Discharge summary cannot exceed 1,000 characters.");
                return;
            }

            var confirm = MessageBox.Show(
                this,
                $"Confirm discharge for {_admission.PatientName} ({_admission.AdmissionNo}) from Room {_admission.RoomNo}?\n\nThis will close the admission and release the room as Available.",
                "Confirm Patient Discharge",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                btnDischarge.Enabled = false;
                _admissionService.Discharge(_admission.AdmissionID, string.IsNullOrEmpty(summary) ? null : summary);

                var dischargedAdmissionId = _admission.AdmissionID;
                LoadAdmissions(dischargedAdmissionId);
                RefreshAdmissionView(preserveMessage: true);
                SetMessage($"Admission {_admission.AdmissionNo} has been discharged and Room {_admission.RoomNo} is now Available.", false);

                PatientDischarged?.Invoke(this, EventArgs.Empty);
            }
            catch (ValidationException ex)
            {
                SetMessage(ex.Message);
                RefreshAdmissionView(preserveMessage: true);
            }
            catch (Exception ex)
            {
                Logger.Error("DischargeUserControl.Discharge failed", ex);
                SetMessage("The patient could not be discharged. Please try again.");
            }
        }

        private void PrintRecord_Click(object? sender, EventArgs e)
        {
            if (_admission == null || _admissionService == null)
                return;

            try
            {
                // AC #4: Reload stored final record from the database before printing
                _finalRecord = _admissionService.GetDischargeRecord(_admission.AdmissionID);
                BindDischargeRecord(_finalRecord);

                using var doc = new PrintDocument();
                doc.DocumentName = $"FinalRecord_{_finalRecord.Admission.AdmissionNo}";
                doc.PrintPage += (_, args) => RenderFinalRecordPage(args, _finalRecord);

                using var preview = new PrintPreviewDialog
                {
                    Document = doc,
                    Width = 1000,
                    Height = 760,
                    StartPosition = FormStartPosition.CenterParent
                };
                preview.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Logger.Error("DischargeUserControl.PrintRecord failed", ex);
                SetMessage("Unable to print the final record. Check your printer setup and try again.");
            }
        }

        private static void RenderFinalRecordPage(PrintPageEventArgs e, DischargeRecord record)
        {
            var g = e.Graphics;
            if (g == null)
                return;

            using var titleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
            using var sectionFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var bodyFont = new Font("Segoe UI", 10F);
            using var boldFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float width = e.MarginBounds.Width;

            var a = record.Admission;
            var b = record.Bill;

            g.DrawString("HOSPITAL MANAGEMENT SYSTEM — FINAL DISCHARGE RECORD", titleFont, Brushes.Black, x, y);
            y += 34;
            g.DrawLine(Pens.Gray, x, y, x + width, y);
            y += 14;

            g.DrawString($"Admission No: {a.AdmissionNo}    |    Status: {a.Status}", boldFont, Brushes.Black, x, y);
            y += 24;
            g.DrawString($"Patient: {a.PatientName} (ID: {a.PatientID})", bodyFont, Brushes.Black, x, y);
            y += 22;
            g.DrawString($"Room: {a.RoomNo}    |    Department: {a.DepartmentCode}    |    Diagnosis: {a.DiagnosisCode}", bodyFont, Brushes.Black, x, y);
            y += 22;
            g.DrawString($"Attending Physician: {a.AttendingPhysician}", bodyFont, Brushes.Black, x, y);
            y += 22;
            g.DrawString($"Admit Date: {FormatDate(a.AdmitDate)}    |    Discharge Date: {FormatDate(a.DischargeDate)}", bodyFont, Brushes.Black, x, y);
            y += 30;

            g.DrawString("BILLING & PAYMENT SUMMARY", sectionFont, Brushes.Black, x, y);
            y += 24;
            g.DrawString($"Bill No: {b?.BillNo ?? "-"}    |    Billed Total: {FormatMoney(b?.TotalAmount ?? 0m)}    |    Paid Total: {FormatMoney(b?.AmountPaid ?? 0m)}    |    Balance: {FormatMoney(b?.Balance ?? 0m)}",
                boldFont, Brushes.Black, x, y);
            y += 28;

            g.DrawString("BILLED LINE ITEMS", sectionFont, Brushes.Black, x, y);
            y += 22;
            foreach (DataRow row in record.Charges.Rows)
            {
                var desc = Convert.ToString(row["Description"]) ?? "";
                var qty = Convert.ToInt32(row["Quantity"]);
                var lineTotal = Convert.ToDecimal(row["LineTotal"]);
                g.DrawString($"• {desc} (x{qty}) — {FormatMoney(lineTotal)}", bodyFont, Brushes.Black, x + 10, y);
                y += 20;
            }

            y += 12;
            g.DrawString("PAYMENTS", sectionFont, Brushes.Black, x, y);
            y += 22;
            foreach (DataRow row in record.Payments.Rows)
            {
                var receipt = Convert.ToString(row["ReceiptNo"]) ?? "";
                var method = Convert.ToString(row["Method"]) ?? "";
                var status = Convert.ToString(row["Status"]) ?? "";
                var amount = Convert.ToDecimal(row["Amount"]);
                g.DrawString($"• {receipt} | {method} | {FormatMoney(amount)} ({status})", bodyFont, Brushes.Black, x + 10, y);
                y += 20;
            }

            if (!string.IsNullOrWhiteSpace(a.DischargeSummary))
            {
                y += 14;
                g.DrawString("DISCHARGE SUMMARY", sectionFont, Brushes.Black, x, y);
                y += 22;
                g.DrawString(a.DischargeSummary, bodyFont, Brushes.Black, new RectangleF(x, y, width, 120));
            }
        }

        private void ClearView()
        {
            _admission = null;
            _finalRecord = null;
            lblEligibilityBanner.Text = "No admissions available.";
            lblPatientValue.Text = "-";
            lblRoomValue.Text = "-";
            lblDiagnosisValue.Text = "-";
            lblAdmitDateValue.Text = "-";
            lblDischargeDateValue.Text = "-";
            lblBilledValue.Text = FormatMoney(0m);
            lblPaidValue.Text = FormatMoney(0m);
            lblBalanceValue.Text = FormatMoney(0m);
            dgvCharges.DataSource = null;
            dgvPayments.DataSource = null;
            txtDischargeSummary.Clear();
            txtDischargeSummary.ReadOnly = true;
            btnDischarge.Enabled = false;
            btnPrintRecord.Enabled = false;
        }

        private void SetMessage(string text, bool isError = true)
        {
            lblMessage.ForeColor = isError ? Color.FromArgb(185, 28, 28) : Color.FromArgb(21, 128, 61);
            lblMessage.Text = text;
        }

        private static string FormatMoney(decimal amount) => amount.ToString("₱#,##0.00");

        private static string FormatDate(DateTime? utcDate) =>
            utcDate.HasValue ? TimeConversion.ToLocal(utcDate.Value).ToString("yyyy-MM-dd hh:mm tt") : "-";
    }
}