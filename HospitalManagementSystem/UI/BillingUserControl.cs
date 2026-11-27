using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    

    public partial class BillingUserControl : UserControl
    {
        private readonly int? _initialAdmissionId;
        private AdmissionService? _admissionService;
        private BillingService? _billingService;
        private Bill? _bill;
        private bool _loading;

        public event EventHandler<BillNavigationEventArgs>? GoToPaymentRequested;

        public BillingUserControl() : this(null)
        {
        }

        public BillingUserControl(int? admissionId)
        {
            _initialAdmissionId = admissionId;
            InitializeComponent();
        }

        private void BillingUserControl_Load(object? sender, EventArgs e)
        {
            try
            {
                var connectionString = Config.GetConnectionString();
                _admissionService = new AdmissionService(connectionString);
                _billingService = new BillingService(connectionString);
                _loading = true;

                var admissions = _admissionService.Search(string.Empty);
                admissions.Columns.Add("AdmissionDisplay", typeof(string));
                foreach (DataRow row in admissions.Rows)
                {
                    row["AdmissionDisplay"] = $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
                }

                cmbAdmissions.DataSource = admissions;
                cmbAdmissions.DisplayMember = "AdmissionDisplay";
                cmbAdmissions.ValueMember = "AdmissionID";

                if (_initialAdmissionId.HasValue)
                    cmbAdmissions.SelectedValue = _initialAdmissionId.Value;
                else if (cmbAdmissions.Items.Count > 0)
                    cmbAdmissions.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.Load failed", ex);
                SetMessage("Admissions could not be loaded. Check the database connection and try again.");
            }
            finally
            {
                _loading = false;
            }

            RefreshBill();
        }

        private void Admissions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_loading)
                RefreshBill();
        }

        private void RefreshBill()
        {
            errProvider.Clear();

            if (_billingService == null || cmbAdmissions.SelectedValue is not int admissionId)
            {
                ClearBill();
                return;
            }

            try
            {
                _loading = true;
                lblMessage.Text = string.Empty;
                _bill = _billingService.GetByAdmission(admissionId);

                if (_bill == null)
                {
                    dgvItems.DataSource = null;
                    SetBillTotals(null);
                    btnGenerate.Enabled = IsSelectedAdmissionActive();
                    btnGoToPayment.Enabled = false;
                    SetMessage("No bill exists yet. Generate one to calculate the room stay and posted charges.", false);
                    return;
                }

                dgvItems.DataSource = _billingService.GetItems(_bill.BillID);
                SetBillTotals(_bill);

                // Regenerate is disabled once a bill exists (and when payments exist)
                btnGenerate.Enabled = false;
                btnGenerate.Text = "Bill already generated";

                var canModify = _bill.Status != BillStatus.Paid && _bill.Status != BillStatus.Voided && _bill.Balance > 0;
                txtDiscount.Enabled = canModify;
                txtDiscountReason.Enabled = canModify;
                btnApplyDiscount.Enabled = canModify;
                btnGoToPayment.Enabled = _bill.Status != BillStatus.Voided;
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.RefreshBill failed", ex);
                SetMessage("Bill details could not be loaded. Please try again.");
            }
            finally
            {
                _loading = false;
            }
        }

        private void Generate_Click(object? sender, EventArgs e)
        {
            if (cmbAdmissions.SelectedValue is not int admissionId || _billingService == null)
                return;

            try
            {
                btnGenerate.Enabled = false;
                _bill = _billingService.Generate(admissionId);
                RefreshBill();
                SetMessage($"Bill {_bill.BillNo} is ready.", false);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.Generate failed", ex);
                SetMessage(ex is ValidationException ? ex.Message : "The bill could not be generated. Please try again.");
                btnGenerate.Enabled = false;
            }
        }

        private void ApplyDiscount_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            lblMessage.Text = string.Empty;

            if (_bill == null || _billingService == null)
                return;

            if (!TryValidateDiscount(txtDiscount.Text, txtDiscountReason.Text, _bill.Subtotal, out var amount, out var discountError, out var reasonError))
            {
                if (!string.IsNullOrEmpty(discountError))
                    errProvider.SetError(txtDiscount, discountError);
                if (!string.IsNullOrEmpty(reasonError))
                    errProvider.SetError(txtDiscountReason, reasonError);
                return;
            }

            try
            {
                btnApplyDiscount.Enabled = false;
                _billingService.ApplyDiscount(_bill.BillID, amount, txtDiscountReason.Text.Trim());
                txtDiscount.Clear();
                txtDiscountReason.Clear();
                RefreshBill();
                SetMessage("Discount updated.", false);
            }
            catch (ValidationException ex)
            {
                if (string.Equals(ex.Field, "Reason", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(txtDiscountReason, ex.Message);
                else
                    errProvider.SetError(txtDiscount, ex.Message);
                SetMessage(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.ApplyDiscount failed", ex);
                SetMessage("The discount could not be applied. Please try again.");
            }
            finally
            {
                btnApplyDiscount.Enabled = _bill != null
                                           && _bill.Status != BillStatus.Paid
                                           && _bill.Status != BillStatus.Voided
                                           && _bill.Balance > 0;
            }
        }

        private void GoToPayment_Click(object? sender, EventArgs e)
        {
            if (_bill == null)
                return;

            GoToPaymentRequested?.Invoke(this, new BillNavigationEventArgs(_bill.BillID, _bill.AdmissionID));
        }

        /// <summary>
        /// Validates discount amount (0 to subtotal, max 2 decimals) and mandatory discount reason.
        /// </summary>
        public static bool TryValidateDiscount(
            string? discountInput,
            string? reasonInput,
            decimal subtotal,
            out decimal amount,
            out string discountError,
            out string reasonError)
        {
            amount = 0m;
            discountError = string.Empty;
            reasonError = string.Empty;

            var trimmedAmount = discountInput?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(trimmedAmount) ||
                (!decimal.TryParse(trimmedAmount, NumberStyles.Number, CultureInfo.InvariantCulture, out amount) &&
                 !decimal.TryParse(trimmedAmount, NumberStyles.Number, CultureInfo.CurrentCulture, out amount)) ||
                amount < 0m)
            {
                discountError = "Enter a valid discount amount of zero or more.";
            }
            else if (decimal.Round(amount, 2) != amount)
            {
                discountError = "Discount cannot have more than 2 decimal places.";
            }
            else if (amount > subtotal)
            {
                discountError = $"Discount cannot exceed the subtotal of ₱{subtotal:#,##0.00}.";
            }

            if (string.IsNullOrWhiteSpace(reasonInput))
            {
                reasonError = "Enter a reason for the discount.";
            }

            return string.IsNullOrEmpty(discountError) && string.IsNullOrEmpty(reasonError);
        }

        private bool IsSelectedAdmissionActive() =>
            cmbAdmissions.SelectedItem is DataRowView row &&
            string.Equals(Convert.ToString(row["Status"]), AdmissionStatus.Admitted, StringComparison.OrdinalIgnoreCase);

        private void SetBillTotals(Bill? bill)
        {
            lblBillStatus.Text = bill?.Status ?? "Not generated";
            lblSubtotal.Text = FormatMoney(bill?.Subtotal ?? 0m);
            lblDiscountTotal.Text = FormatMoney(bill?.Discount ?? 0m);
            lblTotal.Text = FormatMoney(bill?.TotalAmount ?? 0m);
            lblPaid.Text = FormatMoney(bill?.AmountPaid ?? 0m);
            lblBalance.Text = FormatMoney(bill?.Balance ?? 0m);

            if (bill == null)
            {
                txtDiscount.Clear();
                txtDiscountReason.Clear();
                txtDiscount.Enabled = false;
                txtDiscountReason.Enabled = false;
                btnApplyDiscount.Enabled = false;
                btnGoToPayment.Enabled = false;
                btnGenerate.Text = "Generate bill";
            }
            else
            {
                var canModify = bill.Status != BillStatus.Paid && bill.Status != BillStatus.Voided && bill.Balance > 0;
                txtDiscount.Enabled = canModify;
                txtDiscountReason.Enabled = canModify;
                btnApplyDiscount.Enabled = canModify;
                btnGoToPayment.Enabled = bill.Status != BillStatus.Voided;
            }
        }

        private void ClearBill()
        {
            _bill = null;
            dgvItems.DataSource = null;
            SetBillTotals(null);
            btnGenerate.Enabled = false;
            lblMessage.Text = string.Empty;
        }

        private void SetMessage(string text, bool isError = true)
        {
            lblMessage.ForeColor = isError ? Color.FromArgb(185, 28, 28) : Color.FromArgb(21, 128, 61);
            lblMessage.Text = text;
        }

        private static string FormatMoney(decimal amount) => amount.ToString("₱#,##0.00");
    }

    public sealed class BillNavigationEventArgs : EventArgs
    {
        public int BillId { get; }
        public int AdmissionId { get; }

        public BillNavigationEventArgs(int billId, int admissionId)
        {
            BillId = billId;
            AdmissionId = admissionId;
        }
    }
}