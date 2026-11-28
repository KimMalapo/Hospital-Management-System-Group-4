
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    /// <summary>
    /// Billing screen: generates the bill, applies discounts,
    /// and opens the payment window.
    /// </summary>
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

                if (!admissions.Columns.Contains("AdmissionDisplay"))
                {
                    admissions.Columns.Add(
                        "AdmissionDisplay",
                        typeof(string)
                    );
                }

                foreach (DataRow row in admissions.Rows)
                {
                    row["AdmissionDisplay"] =
                        $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
                }

                cmbAdmissions.DataSource = admissions;
                cmbAdmissions.DisplayMember = "AdmissionDisplay";
                cmbAdmissions.ValueMember = "AdmissionID";

                if (_initialAdmissionId.HasValue)
                {
                    cmbAdmissions.SelectedValue = _initialAdmissionId.Value;
                }
                else if (cmbAdmissions.Items.Count > 0)
                {
                    cmbAdmissions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.Load failed", ex);

                SetMessage(
                    "Admissions could not be loaded. Check the database connection and try again."
                );
            }
            finally
            {
                _loading = false;
            }

            RefreshBill();
        }

        private void Admissions_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (!_loading)
            {
                RefreshBill();
            }
        }

        private void RefreshBill()
        {
            errProvider.Clear();
            SetMessage(string.Empty);

            if (_billingService == null ||
                cmbAdmissions.SelectedValue is not int admissionId)
            {
                ClearBill();
                return;
            }

            try
            {
                _loading = true;

                _bill = _billingService.GetByAdmission(admissionId);

                if (_bill == null)
                {
                    ClearBill();
                    dgvItems.DataSource = null;

                    btnGenerate.Enabled = true;
                    btnGenerate.Text = "Generate bill";

                    return;
                }

                dgvItems.DataSource =
                    _billingService.GetItems(_bill.BillID);

                SetBillTotals(_bill);

                btnGenerate.Enabled = false;
                btnGenerate.Text = "Bill already generated";
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "BillingUserControl.RefreshBill failed",
                    ex
                );

                SetMessage(
                    "Bill details could not be loaded. Please try again."
                );
            }
            finally
            {
                _loading = false;
            }
        }

        private void Generate_Click(object? sender, EventArgs e)
        {
            if (cmbAdmissions.SelectedValue is not int admissionId ||
                _billingService == null)
            {
                return;
            }

            try
            {
                btnGenerate.Enabled = false;

                _bill = _billingService.Generate(admissionId);

                RefreshBill();

                SetMessage(
                    $"Bill {_bill.BillNo} is ready.",
                    isError: false
                );
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "BillingUserControl.Generate failed",
                    ex
                );

                SetMessage(
                    ex is ValidationException
                        ? ex.Message
                        : "The bill could not be generated. Please try again."
                );

                btnGenerate.Enabled = true;
            }
        }

        private void ApplyDiscount_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            SetMessage(string.Empty);

            if (_bill == null || _billingService == null)
            {
                return;
            }

            if (!TryValidateDiscount(
                txtDiscount.Text,
                txtDiscountReason.Text,
                _bill.Subtotal,
                out var amount,
                out var discountError,
                out var reasonError))
            {
                if (!string.IsNullOrEmpty(discountError))
                {
                    errProvider.SetError(
                        txtDiscount,
                        discountError
                    );
                }

                if (!string.IsNullOrEmpty(reasonError))
                {
                    errProvider.SetError(
                        txtDiscountReason,
                        reasonError
                    );
                }

                return;
            }

            try
            {
                btnApplyDiscount.Enabled = false;

                _billingService.ApplyDiscount(
                    _bill.BillID,
                    amount,
                    txtDiscountReason.Text.Trim()
                );

                txtDiscount.Clear();
                txtDiscountReason.Clear();

                RefreshBill();

                SetMessage(
                    "Discount updated.",
                    isError: false
                );
            }
            catch (ValidationException ex)
            {
                if (string.Equals(
                    ex.Field,
                    "Reason",
                    StringComparison.OrdinalIgnoreCase
                ) ||
                string.Equals(
                    ex.Field,
                    "Discount reason",
                    StringComparison.OrdinalIgnoreCase
                ))
                {
                    errProvider.SetError(
                        txtDiscountReason,
                        ex.Message
                    );
                }
                else
                {
                    errProvider.SetError(
                        txtDiscount,
                        ex.Message
                    );
                }

                SetMessage(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "BillingUserControl.ApplyDiscount failed",
                    ex
                );

                SetMessage(
                    "The discount could not be applied. Please try again."
                );
            }
            finally
            {
                UpdateDiscountControlsEnabled();
            }
        }

        // UPDATED: Opens the Payment Form directly.
        private void GoToPayment_Click(object? sender, EventArgs e)
        {
            if (_bill == null)
            {
                SetMessage(
                    "Generate or select a bill before recording a payment."
                );

                return;
            }

            if (_bill.Status == BillStatus.Voided)
            {
                SetMessage(
                    "Payments cannot be recorded for a voided bill."
                );

                return;
            }

            try
            {
                using (var paymentForm = new PaymentsForm(
                    _bill.BillID,
                    _bill.BillNo ?? $"BILL-{_bill.BillID:000000}"
                ))
                {
                    paymentForm.ShowDialog();
                }

                // Reload the bill after the payment window closes.
                RefreshBill();

                SetMessage(
                    "Payment window closed. Bill information has been refreshed.",
                    isError: false
                );
            }
            catch (Exception ex)
            {
                Logger.Error(
                    "BillingUserControl.GoToPayment failed",
                    ex
                );

                SetMessage(
                    ex is ValidationException
                        ? ex.Message
                        : "The Payment Form could not be opened. Please try again."
                );
            }
        }

        /// <summary>
        /// Validates the discount amount and required reason.
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

            var trimmedAmount =
                discountInput?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(trimmedAmount) ||
                (!decimal.TryParse(
                    trimmedAmount,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out amount
                ) &&
                !decimal.TryParse(
                    trimmedAmount,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out amount
                )) ||
                amount < 0m)
            {
                discountError =
                    "Enter a valid discount amount of zero or more.";
            }
            else if (decimal.Round(amount, 2) != amount)
            {
                discountError =
                    "Discount cannot have more than 2 decimal places.";
            }
            else if (amount > subtotal)
            {
                discountError =
                    $"Discount cannot exceed the subtotal of ₱{subtotal:#,##0.00}.";
            }

            var trimmedReason =
                reasonInput?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(trimmedReason))
            {
                reasonError =
                    "A reason is required when applying a discount.";
            }
            else if (trimmedReason.Length > 300)
            {
                reasonError =
                    "Reason cannot be longer than 300 characters.";
            }

            return string.IsNullOrEmpty(discountError) &&
                   string.IsNullOrEmpty(reasonError);
        }

        private void SetBillTotals(Bill bill)
        {
            lblBillStatus.Text = bill.Status;

            lblSubtotal.Text = FormatMoney(bill.Subtotal);
            lblDiscountTotal.Text = FormatMoney(bill.Discount);
            lblTotal.Text = FormatMoney(bill.TotalAmount);
            lblPaid.Text = FormatMoney(bill.AmountPaid);
            lblBalance.Text = FormatMoney(bill.Balance);

            UpdateDiscountControlsEnabled();

            btnGoToPayment.Enabled =
                bill.Status != BillStatus.Voided;
        }

        private void ClearBill()
        {
            _bill = null;

            lblBillStatus.Text = "Not generated";

            lblSubtotal.Text = FormatMoney(0);
            lblDiscountTotal.Text = FormatMoney(0);
            lblTotal.Text = FormatMoney(0);
            lblPaid.Text = FormatMoney(0);
            lblBalance.Text = FormatMoney(0);

            txtDiscount.Clear();
            txtDiscountReason.Clear();

            txtDiscount.Enabled = false;
            txtDiscountReason.Enabled = false;

            btnApplyDiscount.Enabled = false;
            btnGoToPayment.Enabled = false;

            btnGenerate.Enabled = true;
            btnGenerate.Text = "Generate bill";
        }

        private void UpdateDiscountControlsEnabled()
        {
            var canEdit =
                _bill != null &&
                _bill.Status != BillStatus.Paid &&
                _bill.Status != BillStatus.Voided &&
                _bill.Balance > 0;

            txtDiscount.Enabled = canEdit;
            txtDiscountReason.Enabled = canEdit;
            btnApplyDiscount.Enabled = canEdit;
        }

        private void SetMessage(
            string text,
            bool isError = true)
        {
            lblMessage.ForeColor = isError
                ? Color.FromArgb(185, 28, 28)
                : Color.FromArgb(21, 128, 61);

            lblMessage.Text = text;
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("₱#,##0.00");
        }
    }

    public sealed class BillNavigationEventArgs : EventArgs
    {
        public int BillId { get; }

        public int AdmissionId { get; }

        public BillNavigationEventArgs(
            int billId,
            int admissionId)
        {
            BillId = billId;
            AdmissionId = admissionId;
        }
    }
}
