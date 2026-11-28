
using System;
using System.Data;
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

        public BillingUserControl() : this(null) { }

        public BillingUserControl(int? admissionId)
        {
            _initialAdmissionId = admissionId;
            InitializeComponent();
            UITheme.Apply(this);

            _admissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;
            _generate.Click += Generate_Click;
            _goToPayment.Click += GoToPayment_Click;
            _printStatement.Click += PrintStatement_Click;
            _applyDiscount.Click += ApplyDiscount_Click;
            _voidBill.Click += VoidBill_Click;
            _voidBill.Visible = Permissions.IsAllowed(AppAction.VoidBill); // Admin only
            Load += BillingUserControl_Load;

            _items.AutoGenerateColumns = true;
            _items.ReadOnly = true;
            _items.AllowUserToAddRows = false;
            _items.AllowUserToDeleteRows = false;
            _items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _items.MultiSelect = false;
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
                    admissions.Columns.Add("AdmissionDisplay", typeof(string));

                foreach (DataRow row in admissions.Rows)
                {
                    row["AdmissionDisplay"] =
                        $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
                }

                _admissions.DataSource = admissions;
                _admissions.DisplayMember = "AdmissionDisplay";
                _admissions.ValueMember = "AdmissionID";

                if (_initialAdmissionId.HasValue)
                    _admissions.SelectedValue = _initialAdmissionId.Value;
                else if (_admissions.Items.Count > 0)
                    _admissions.SelectedIndex = 0;
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
            _errors.Clear();
            SetMessage(string.Empty);

            if (_billingService == null || !TryGetSelectedAdmissionId(out var admissionId))
            {
                ClearBill();
                _items.DataSource = null;
                _generate.Enabled = false;
                return;
            }

            try
            {
                _loading = true;
                _bill = _billingService.GetByAdmission(admissionId);

                if (_bill == null)
                {
                    ClearBill();
                    _items.DataSource = null;
                    _generate.Enabled = IsSelectedAdmissionActive();
                    return;
                }

                _items.DataSource = _billingService.GetItems(_bill.BillID);
                SetBillTotals(_bill);
                _generate.Enabled = false;
                _generate.Text = "Bill already generated";
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
            if (_billingService == null || !TryGetSelectedAdmissionId(out var admissionId))
            {
                SetMessage("Select an admission first.");
                return;
            }

            if (!IsSelectedAdmissionActive())
            {
                SetMessage("A bill can only be generated for an active admission.");
                return;
            }

            try
            {
                _generate.Enabled = false;
                _bill = _billingService.Generate(admissionId);
                RefreshBill();
                SetMessage($"Bill {_bill.BillNo} is ready.", false);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.Generate failed", ex);
                SetMessage(ex is ValidationException ? ex.Message : "The bill could not be generated. Please try again.");
                _generate.Enabled = true;
            }
        }

        private void ApplyDiscount_Click(object? sender, EventArgs e)
        {
            _errors.Clear();
            SetMessage(string.Empty);

            if (_bill == null || _billingService == null)
                return;

            if (!TryValidateDiscount(
                _discount.Text,
                _discountReason.Text,
                _bill.Subtotal,
                out var amount,
                out var discountError,
                out var reasonError))
            {
                if (!string.IsNullOrEmpty(discountError))
                    _errors.SetError(_discount, discountError);

                if (!string.IsNullOrEmpty(reasonError))
                    _errors.SetError(_discountReason, reasonError);

                return;
            }

            try
            {
                _applyDiscount.Enabled = false;

                _billingService.ApplyDiscount(
                    _bill.BillID,
                    amount,
                    _discountReason.Text.Trim());

                _discount.Clear();
                _discountReason.Clear();

                RefreshBill();
                SetMessage("Discount updated.", false);
            }
            catch (ValidationException ex)
            {
                if (string.Equals(ex.Field, "Reason", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(ex.Field, "Discount reason", StringComparison.OrdinalIgnoreCase))
                    _errors.SetError(_discountReason, ex.Message);
                else
                    _errors.SetError(_discount, ex.Message);

                SetMessage(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.ApplyDiscount failed", ex);
                SetMessage("The discount could not be applied. Please try again.");
            }
            finally
            {
                UpdateDiscountControlsEnabled();
            }
        }

        // Voids the current bill (Admin only). The bill must have no payments; the admission can then be billed again.
        private void VoidBill_Click(object? sender, EventArgs e)
        {
            _errors.Clear();
            SetMessage(string.Empty);

            if (_bill == null || _billingService == null)
            {
                SetMessage("Select a bill to void.");
                return;
            }

            if (!Permissions.IsAllowed(AppAction.VoidBill))
            {
                SetMessage("Only an Admin can void a bill.");
                return;
            }

            if (_bill.AmountPaid > 0)
            {
                SetMessage("This bill has payments. Void the payments first (Payment screen), then void the bill.");
                return;
            }

            var billNo = _bill.BillNo;
            var reason = UIHelper.AskText($"Why is bill {billNo} being voided?", "Void bill");
            if (string.IsNullOrWhiteSpace(reason))
            {
                SetMessage("A reason is required to void a bill. Nothing was changed.");
                return;
            }

            try
            {
                _voidBill.Enabled = false;
                _billingService.Void(_bill.BillID, reason);

                RefreshBill();
                SetMessage($"Bill {billNo} was voided. You can generate a new bill for this admission.", false);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.VoidBill failed", ex);
                SetMessage(ex is ValidationException or UnauthorizedAccessException
                    ? ex.Message
                    : "The bill could not be voided. Please try again.");
                _voidBill.Enabled = _bill != null && _bill.Status != BillStatus.Voided;
            }
        }

        private void GoToPayment_Click(object? sender, EventArgs e)
        {
            if (_bill == null)
            {
                SetMessage("Generate or select a bill before recording a payment.");
                return;
            }

            if (_bill.Status == BillStatus.Voided)
            {
                SetMessage("Payments cannot be recorded for a voided bill.");
                return;
            }

            GoToPaymentRequested?.Invoke(
                this,
                new BillNavigationEventArgs(_bill.BillID, _bill.AdmissionID));
        }

        private void PrintStatement_Click(object? sender, EventArgs e)
        {
            if (_bill == null)
            {
                SetMessage("Generate or select a bill before printing its statement.");
                return;
            }

            try
            {
                using var document = new UI.Printing.StatementPrintDocument(
                    _bill.BillID,
                    Config.GetConnectionString());

                UI.Printing.PrintHelper.ShowPreview(this, document);
            }
            catch (Exception ex)
            {
                Logger.Error("BillingUserControl.PrintStatement failed", ex);
                SetMessage("The bill statement could not be opened for printing.");
            }
        }

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

            var value = discountInput?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(value) ||
                (!decimal.TryParse(value, NumberStyles.Number,
                    CultureInfo.InvariantCulture, out amount) &&
                 !decimal.TryParse(value, NumberStyles.Number,
                    CultureInfo.CurrentCulture, out amount)) ||
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

            var reason = reasonInput?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(reason))
                reasonError = "A reason is required when applying a discount.";
            else if (reason.Length > 300)
                reasonError = "Reason cannot be longer than 300 characters.";

            return string.IsNullOrEmpty(discountError) &&
                   string.IsNullOrEmpty(reasonError);
        }

        private bool TryGetSelectedAdmissionId(out int admissionId)
        {
            admissionId = 0;

            if (_admissions.SelectedValue == null ||
                _admissions.SelectedValue is DataRowView)
                return false;

            return int.TryParse(
                Convert.ToString(_admissions.SelectedValue),
                out admissionId) && admissionId > 0;
        }

        private bool IsSelectedAdmissionActive()
        {
            if (_admissions.SelectedItem is not DataRowView row ||
                !row.Row.Table.Columns.Contains("Status"))
                return false;

            return string.Equals(
                Convert.ToString(row["Status"]),
                AdmissionStatus.Admitted,
                StringComparison.OrdinalIgnoreCase);
        }

        private void SetBillTotals(Bill bill)
        {
            _billStatus.Text = bill.Status;
            _subtotal.Text = FormatMoney(bill.Subtotal);
            _discountTotal.Text = FormatMoney(bill.Discount);
            _total.Text = FormatMoney(bill.TotalAmount);
            _paid.Text = FormatMoney(bill.AmountPaid);
            _balance.Text = FormatMoney(bill.Balance);

            UpdateDiscountControlsEnabled();

            _goToPayment.Enabled =
                bill.Status != BillStatus.Voided && bill.Balance > 0;

            _printStatement.Enabled = true;
            _voidBill.Enabled = bill.Status != BillStatus.Voided;
        }

        private void ClearBill()
        {
            _bill = null;

            _billStatus.Text = "Not generated";
            _subtotal.Text = FormatMoney(0);
            _discountTotal.Text = FormatMoney(0);
            _total.Text = FormatMoney(0);
            _paid.Text = FormatMoney(0);
            _balance.Text = FormatMoney(0);

            _discount.Clear();
            _discountReason.Clear();

            _discount.Enabled = false;
            _discountReason.Enabled = false;

            _applyDiscount.Enabled = false;
            _goToPayment.Enabled = false;
            _printStatement.Enabled = false;
            _voidBill.Enabled = false;

            _generate.Text = "Generate Bill";
        }

        private void UpdateDiscountControlsEnabled()
        {
            var canEdit =
                _bill != null &&
                _bill.Status != BillStatus.Paid &&
                _bill.Status != BillStatus.Voided &&
                _bill.Balance > 0;

            _discount.Enabled = canEdit;
            _discountReason.Enabled = canEdit;
            _applyDiscount.Enabled = canEdit;
        }

        private void SetMessage(string text, bool isError = true)
        {
            _message.ForeColor = isError
                ? System.Drawing.Color.FromArgb(185, 28, 28)
                : System.Drawing.Color.FromArgb(21, 128, 61);

            _message.Text = text;
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

        public BillNavigationEventArgs(int billId, int admissionId)
        {
            BillId = billId;
            AdmissionId = admissionId;
        }
    }
}
