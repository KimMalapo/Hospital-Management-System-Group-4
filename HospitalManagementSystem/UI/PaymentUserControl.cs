using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class PaymentUserControl : UserControl
    {
        private readonly int? _initialBillId;
        private AdmissionService? _admissionService;
        private BillingService? _billingService;
        private PaymentService? _paymentService;
        private Bill? _bill;
        private bool _loading;

        private sealed class BillOption
        {
            public int BillId { get; init; }
            public int AdmissionId { get; init; }
            public string Display { get; init; } = string.Empty;
        }

        public PaymentUserControl() : this(null)
        {
        }

        public PaymentUserControl(int? billId)
        {
            _initialBillId = billId;
            InitializeComponent();
            UITheme.Apply(this);
        }

        private void PaymentUserControl_Load(object? sender, EventArgs e)
        {
            // Hide Void from Staff users (AC #8)
            btnVoidPayment.Visible = Permissions.IsAllowed(AppAction.VoidPayment);

            cmbMethod.Items.Clear();
            foreach (var method in PaymentMethod.All)
            {
                cmbMethod.Items.Add(method);
            }
            if (cmbMethod.Items.Count > 0)
                cmbMethod.SelectedIndex = 0;

            try
            {
                var connectionString = Config.GetConnectionString();
                _admissionService = new AdmissionService(connectionString);
                _billingService = new BillingService(connectionString);
                _paymentService = new PaymentService(connectionString);

                LoadBills(_initialBillId);
            }
            catch (Exception ex)
            {
                Logger.Error("PaymentUserControl.Load failed", ex);
                SetMessage("Bills could not be loaded. Check the database connection and try again.");
            }
        }

        private void LoadBills(int? selectBillId)
        {
            if (_admissionService == null || _billingService == null)
                return;

            _loading = true;
            try
            {
                var admissions = _admissionService.Search(string.Empty);
                var options = new List<BillOption>();

                foreach (DataRow row in admissions.Rows)
                {
                    var admissionId = Convert.ToInt32(row["AdmissionID"]);
                    var bill = _billingService.GetByAdmission(admissionId);
                    if (bill == null)
                        continue;

                    options.Add(new BillOption
                    {
                        BillId = bill.BillID,
                        AdmissionId = admissionId,
                        Display = $"{bill.BillNo} | {row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {bill.Status}"
                    });
                }

                cmbBills.DataSource = options;
                cmbBills.DisplayMember = nameof(BillOption.Display);
                cmbBills.ValueMember = nameof(BillOption.BillId);

                if (selectBillId.HasValue && options.Any(o => o.BillId == selectBillId.Value))
                    cmbBills.SelectedValue = selectBillId.Value;
                else if (options.Count > 0)
                    cmbBills.SelectedIndex = 0;
            }
            finally
            {
                _loading = false;
            }

            RefreshPaymentView();
        }

        private void Bills_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_loading)
            {
                lblMessage.Text = string.Empty;
                RefreshPaymentView();
            }
        }

        private void Method_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var method = cmbMethod.SelectedItem?.ToString();
            var requiresRef = !string.IsNullOrEmpty(method) && PaymentMethod.RequiresReference(method);
            lblReferenceCaption.Text = requiresRef ? "Reference Number (Required):" : "Reference Number (Optional):";
        }

        private void Payments_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateHistoryActionButtons();
        }

        private void UpdateHistoryActionButtons()
        {
            var hasSelection = dgvPayments.SelectedRows.Count > 0
                               && dgvPayments.SelectedRows[0].Cells[colPaymentId.Name].Value != null;
            btnPrintReceipt.Enabled = hasSelection;
        }

        private void RefreshPaymentView(bool preserveMessage = false)
        {
            errProvider.Clear();
            if (!preserveMessage)
                lblMessage.Text = string.Empty;

            if (_billingService == null || _paymentService == null || cmbBills.SelectedItem is not BillOption selected)
            {
                ClearView();
                SetMessage("No generated bills are available for payment yet.", false);
                return;
            }

            try
            {
                _bill = _billingService.GetById(selected.BillId);
                if (_bill == null)
                {
                    ClearView();
                    return;
                }

                lblBillNo.Text = _bill.BillNo ?? $"BILL-{_bill.BillID}";
                lblTotal.Text = FormatMoney(_bill.TotalAmount);
                lblPaid.Text = FormatMoney(_bill.AmountPaid);
                lblBalance.Text = FormatMoney(_bill.Balance);
                lblBillStatus.Text = _bill.Status;

                dgvPayments.DataSource = _paymentService.GetByBill(_bill.BillID);
                UpdateHistoryActionButtons();

                var isPaid = string.Equals(_bill.Status, BillStatus.Paid, StringComparison.OrdinalIgnoreCase) || _bill.Balance <= 0m;
                var isVoided = string.Equals(_bill.Status, BillStatus.Voided, StringComparison.OrdinalIgnoreCase);

                if (isPaid)
                {
                    lblBillStatus.Text = "Paid";
                    lblBillStatus.ForeColor = Color.FromArgb(21, 128, 61);
                    SetEntryEnabled(false);
                    if (!preserveMessage)
                        SetMessage("This bill is fully paid. No additional payments can be posted.", false);
                }
                else if (isVoided)
                {
                    lblBillStatus.ForeColor = Color.FromArgb(185, 28, 28);
                    SetEntryEnabled(false);
                    if (!preserveMessage)
                        SetMessage("This bill has been voided.", true);
                }
                else
                {
                    lblBillStatus.ForeColor = Color.FromArgb(30, 41, 59);
                    SetEntryEnabled(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PaymentUserControl.RefreshPaymentView failed", ex);
                SetMessage("Payment details could not be loaded. Please try again.");
            }
        }

        private void PostPayment_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            lblMessage.Text = string.Empty;

            if (_bill == null || _paymentService == null)
                return;

            var method = cmbMethod.SelectedItem?.ToString();
            if (!TryValidatePaymentInput(txtAmount.Text, method, txtReferenceNo.Text, _bill.Balance,
                    out var amount, out var amountError, out var methodError, out var referenceError))
            {
                if (!string.IsNullOrEmpty(amountError))
                    errProvider.SetError(txtAmount, amountError);
                if (!string.IsNullOrEmpty(methodError))
                    errProvider.SetError(cmbMethod, methodError);
                if (!string.IsNullOrEmpty(referenceError))
                    errProvider.SetError(txtReferenceNo, referenceError);
                return;
            }

            try
            {
                btnPostPayment.Enabled = false;
                var saved = _paymentService.Post(_bill.BillID, amount, method, txtReferenceNo.Text.Trim());

                txtAmount.Clear();
                txtReferenceNo.Clear();

                LoadBills(_bill.BillID);
                RefreshPaymentView(preserveMessage: true);
                SetMessage($"Payment of {FormatMoney(saved.Amount)} posted successfully.\nReceipt No: {saved.ReceiptNo}", false);
            }
            catch (ValidationException ex)
            {
                if (string.Equals(ex.Field, "Amount", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(txtAmount, ex.Message);
                else if (string.Equals(ex.Field, "Method", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(cmbMethod, ex.Message);
                else if (string.Equals(ex.Field, "Reference number", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(txtReferenceNo, ex.Message);

                SetMessage(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("PaymentUserControl.PostPayment failed", ex);
                SetMessage("The payment could not be posted. Please try again.");
            }
            finally
            {
                if (_bill != null && _bill.Balance > 0m && !string.Equals(_bill.Status, BillStatus.Paid, StringComparison.OrdinalIgnoreCase))
                    btnPostPayment.Enabled = true;
            }
        }

        private void PrintReceipt_Click(object? sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0)
            {
                SetMessage("Select a payment row in the history table to print its receipt.");
                return;
            }

            var row = dgvPayments.SelectedRows[0];
            if (!int.TryParse(Convert.ToString(row.Cells[colPaymentId.Name].Value), out var paymentId) || paymentId <= 0)
            {
                SetMessage("Unable to determine the selected payment ID.");
                return;
            }

            try
            {
                using var doc = new UI.Printing.ReceiptPrintDocument(paymentId, Config.GetConnectionString());
                UI.Printing.PrintHelper.ShowPreview(this, doc);
            }
            catch (Exception ex)
            {
                Logger.Error("PaymentUserControl.PrintReceipt failed", ex);
                SetMessage("The receipt could not be opened for printing.");
            }
        }

        private void VoidPayment_Click(object? sender, EventArgs e)
        {
            if (_bill == null || _paymentService == null)
                return;

            if (!Permissions.IsAllowed(AppAction.VoidPayment))
            {
                SetMessage("Only an Admin can void posted payments.");
                return;
            }

            if (dgvPayments.SelectedRows.Count == 0)
            {
                SetMessage("Select a payment row in the history table to void.");
                return;
            }

            var row = dgvPayments.SelectedRows[0];
            var status = Convert.ToString(row.Cells[colStatus.Name].Value) ?? string.Empty;
            if (string.Equals(status, "Voided", StringComparison.OrdinalIgnoreCase))
            {
                SetMessage("The selected payment is already voided.");
                return;
            }

            var paymentId = Convert.ToInt32(row.Cells[colPaymentId.Name].Value);
            var receiptNo = Convert.ToString(row.Cells[colReceiptNo.Name].Value) ?? $"#{paymentId}";

            var reason = PromptVoidReason(receiptNo);
            if (reason == null)
                return;

            try
            {
                _paymentService.Void(paymentId, reason);
                LoadBills(_bill.BillID);
                RefreshPaymentView(preserveMessage: true);
                SetMessage($"Payment {receiptNo} was voided and the bill balance has been restored.", false);
            }
            catch (Exception ex)
            {
                Logger.Error("PaymentUserControl.VoidPayment failed", ex);
                SetMessage(ex is ValidationException ? ex.Message : "The payment could not be voided. Please try again.");
            }
        }

        private string? PromptVoidReason(string receiptNo)
        {
            using var dialog = new Form
            {
                Text = $"Void Payment {receiptNo}",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(440, 190),
                BackColor = Color.White,
                Font = new Font("Yu Gothic", 10F)
            };

            var lblPrompt = new Label
            {
                Text = "Enter a reason for voiding this payment (required):",
                Location = new Point(20, 18),
                Size = new Size(400, 22)
            };

            var txtReason = new TextBox
            {
                Location = new Point(20, 46),
                Size = new Size(400, 68),
                Multiline = true,
                MaxLength = 300
            };

            var btnConfirm = new Button
            {
                Text = "Confirm Void",
                BackColor = Color.MistyRose,
                Font = new Font("Yu Gothic Medium", 10F, FontStyle.Bold),
                Location = new Point(216, 132),
                Size = new Size(115, 36)
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Font = new Font("Yu Gothic Medium", 10F, FontStyle.Bold),
                Location = new Point(339, 132),
                Size = new Size(81, 36)
            };

            btnConfirm.Click += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(txtReason.Text) || txtReason.Text.Trim().Length < 3)
                {
                    MessageBox.Show(dialog, "Please enter a reason of at least 3 characters.", "Reason required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] { lblPrompt, txtReason, btnConfirm, btnCancel });
            dialog.AcceptButton = btnConfirm;
            dialog.CancelButton = btnCancel;

            return dialog.ShowDialog(this) == DialogResult.OK ? txtReason.Text.Trim() : null;
        }

        /// <summary>
        /// Validates payment amount (> 0, <= balance, max 2 decimals), payment method, and required reference for non-cash methods.
        /// </summary>
        public static bool TryValidatePaymentInput(
            string? amountInput,
            string? method,
            string? referenceInput,
            decimal balance,
            out decimal amount,
            out string amountError,
            out string methodError,
            out string referenceError)
        {
            amount = 0m;
            amountError = string.Empty;
            methodError = string.Empty;
            referenceError = string.Empty;

            var trimmedAmount = amountInput?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(trimmedAmount) ||
                (!decimal.TryParse(trimmedAmount, NumberStyles.Number, CultureInfo.InvariantCulture, out amount) &&
                 !decimal.TryParse(trimmedAmount, NumberStyles.Number, CultureInfo.CurrentCulture, out amount)) ||
                amount <= 0m)
            {
                amountError = "Enter a payment amount greater than zero.";
            }
            else if (decimal.Round(amount, 2) != amount)
            {
                amountError = "Amount cannot have more than 2 decimal places.";
            }
            else if (amount > balance)
            {
                amountError = $"Payment cannot exceed the remaining balance of ₱{balance:#,##0.00}.";
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                methodError = "Select a payment method.";
            }
            else if (PaymentMethod.RequiresReference(method) && string.IsNullOrWhiteSpace(referenceInput))
            {
                referenceError = $"A reference number is required for {method} payments.";
            }

            return string.IsNullOrEmpty(amountError)
                   && string.IsNullOrEmpty(methodError)
                   && string.IsNullOrEmpty(referenceError);
        }

        private void SetEntryEnabled(bool enabled)
        {
            txtAmount.Enabled = enabled;
            cmbMethod.Enabled = enabled;
            txtReferenceNo.Enabled = enabled;
            btnPostPayment.Enabled = enabled;
        }

        private void ClearView()
        {
            _bill = null;
            lblBillNo.Text = "-";
            lblTotal.Text = FormatMoney(0m);
            lblPaid.Text = FormatMoney(0m);
            lblBalance.Text = FormatMoney(0m);
            lblBillStatus.Text = "-";
            dgvPayments.DataSource = null;
            btnPrintReceipt.Enabled = false;
            SetEntryEnabled(false);
        }

        private void SetMessage(string text, bool isError = true)
        {
            lblMessage.ForeColor = isError ? Color.FromArgb(185, 28, 28) : Color.FromArgb(21, 128, 61);
            lblMessage.Text = text;
        }

        private static string FormatMoney(decimal amount) => amount.ToString("₱#,##0.00");
    }
}