using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class ChargesUserControl : UserControl
    {
        private readonly int? _initialAdmissionId;
        private AdmissionService? _admissionService;
        private ServiceCatalogService? _catalogService;
        private ChargeService? _chargeService;
        private Admission? _currentAdmission;
        private bool _loading;

        public ChargesUserControl() : this(null)
        {
        }

        public ChargesUserControl(int? admissionId)
        {
            _initialAdmissionId = admissionId;
            InitializeComponent();
        }

        private void ChargesUserControl_Load(object? sender, EventArgs e)
        {
            btnVoidCharge.Visible = Permissions.IsAllowed(AppAction.VoidCharge);

            try
            {
                var connectionString = Config.GetConnectionString();
                _admissionService = new AdmissionService(connectionString);
                _catalogService = new ServiceCatalogService(connectionString);
                _chargeService = new ChargeService(connectionString);

                _loading = true;

                LoadServices();
                LoadAdmissions();

                if (_initialAdmissionId.HasValue)
                    cmbAdmissions.SelectedValue = _initialAdmissionId.Value;
                else if (cmbAdmissions.Items.Count > 0)
                    cmbAdmissions.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error("ChargesUserControl.Load failed", ex);
                SetStatus("Unable to load admissions or services. Check the database connection.");
            }
            finally
            {
                _loading = false;
            }

            RefreshAdmissionAndCharges();
        }

        private void LoadServices()
        {
            if (_catalogService == null) return;

            var services = _catalogService.GetAll(activeOnly: true);
            if (!services.Columns.Contains("ServiceDisplay"))
                services.Columns.Add("ServiceDisplay", typeof(string));

            foreach (DataRow row in services.Rows)
            {
                var code = Convert.ToString(row["Code"]);
                var name = Convert.ToString(row["Name"]);
                var price = row["UnitPrice"] == DBNull.Value ? 0m : Convert.ToDecimal(row["UnitPrice"]);
                row["ServiceDisplay"] = $"{code} — {name} (₱{price:#,##0.00})";
            }

            cmbServices.DataSource = services;
            cmbServices.DisplayMember = "ServiceDisplay";
            cmbServices.ValueMember = "ServiceID";
        }

        private void LoadAdmissions()
        {
            if (_admissionService == null) return;

            var admissions = _admissionService.Search(string.Empty);
            if (!admissions.Columns.Contains("AdmissionDisplay"))
                admissions.Columns.Add("AdmissionDisplay", typeof(string));

            foreach (DataRow row in admissions.Rows)
            {
                row["AdmissionDisplay"] =
                    $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
            }

            cmbAdmissions.DataSource = admissions;
            cmbAdmissions.DisplayMember = "AdmissionDisplay";
            cmbAdmissions.ValueMember = "AdmissionID";
        }

        private void Admissions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_loading)
                RefreshAdmissionAndCharges();
        }

        private void RefreshAdmissionAndCharges()
        {
            errProvider.Clear();
            lblStatusMessage.Text = string.Empty;

            if (_admissionService == null || _chargeService == null || cmbAdmissions.SelectedValue is not int admissionId)
            {
                _currentAdmission = null;
                lblPatientValue.Text = "—";
                lblRoomValue.Text = "—";
                lblDiagnosisValue.Text = "—";
                dgvCharges.DataSource = null;
                lblSubtotalValue.Text = "₱0.00";
                ApplyReadOnlyState(null);
                return;
            }

            try
            {
                _currentAdmission = _admissionService.GetById(admissionId);
                if (_currentAdmission == null)
                {
                    SetStatus("Selected admission could not be found.");
                    ApplyReadOnlyState(null);
                    return;
                }

                lblPatientValue.Text = string.IsNullOrWhiteSpace(_currentAdmission.PatientName)
                    ? $"Patient #{_currentAdmission.PatientID}"
                    : _currentAdmission.PatientName;
                lblRoomValue.Text = string.IsNullOrWhiteSpace(_currentAdmission.RoomNo)
                    ? "—"
                    : _currentAdmission.RoomNo;
                lblDiagnosisValue.Text = string.IsNullOrWhiteSpace(_currentAdmission.DiagnosisCode)
                    ? "—"
                    : _currentAdmission.DiagnosisCode;

                RefreshChargesGridOnly(admissionId);
                ApplyReadOnlyState(_currentAdmission);
            }
            catch (Exception ex)
            {
                Logger.Error("ChargesUserControl.RefreshAdmissionAndCharges failed", ex);
                SetStatus("Failed to load admission charges. Please try again.");
            }
        }

        private void RefreshChargesGridOnly(int admissionId)
        {
            if (_chargeService == null) return;

            dgvCharges.DataSource = _chargeService.GetByAdmission(admissionId, includeVoided: false);

            var subtotal = _chargeService.GetSubtotal(admissionId);
            lblSubtotalValue.Text = $"₱{subtotal:#,##0.00}";

            UpdateVoidButtonState();
        }

        private void ApplyReadOnlyState(Admission? admission)
        {
            var canVoidRole = Permissions.IsAllowed(AppAction.VoidCharge);
            btnVoidCharge.Visible = canVoidRole;

            if (admission == null)
            {
                pnlReadOnlyBanner.Visible = false;
                cmbServices.Enabled = false;
                txtQuantity.Enabled = false;
                btnAddCharge.Enabled = false;
                btnVoidCharge.Enabled = false;
                return;
            }

            var isAdmitted = string.Equals(admission.Status, AdmissionStatus.Admitted, StringComparison.OrdinalIgnoreCase);

            if (isAdmitted)
            {
                pnlReadOnlyBanner.Visible = false;
                cmbServices.Enabled = true;
                txtQuantity.Enabled = true;
                btnAddCharge.Enabled = true;
                UpdateVoidButtonState();
            }
            else
            {
                var statusWord = admission.Status.ToLowerInvariant();
                lblReadOnlyMessage.Text =
                    $"Read-only: This admission is {statusWord}. Charges cannot be added or voided after discharge.";
                pnlReadOnlyBanner.Visible = true;
                cmbServices.Enabled = false;
                txtQuantity.Enabled = false;
                btnAddCharge.Enabled = false;
                btnVoidCharge.Enabled = false;
            }
        }

        private void AddCharge_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            lblStatusMessage.Text = string.Empty;

            if (_chargeService == null || _currentAdmission == null)
                return;

            if (!string.Equals(_currentAdmission.Status, AdmissionStatus.Admitted, StringComparison.OrdinalIgnoreCase))
            {
                SetStatus("Charges cannot be added because this admission is no longer active.");
                return;
            }

            if (cmbServices.SelectedValue is not int serviceId || serviceId <= 0)
            {
                errProvider.SetError(cmbServices, "Select a service to add.");
                return;
            }

            if (!TryParseWholeQuantity(txtQuantity.Text, out var quantity, out var quantityError))
            {
                errProvider.SetError(txtQuantity, quantityError);
                return;
            }

            try
            {
                btnAddCharge.Enabled = false;
                _chargeService.Add(_currentAdmission.AdmissionID, serviceId, quantity);
                txtQuantity.Text = "1";
                RefreshChargesGridOnly(_currentAdmission.AdmissionID);
                SetStatus("Charge added.", isError: false);
            }
            catch (ValidationException ex)
            {
                if (string.Equals(ex.Field, "Quantity", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(txtQuantity, ex.Message);
                else if (string.Equals(ex.Field, "Service", StringComparison.OrdinalIgnoreCase))
                    errProvider.SetError(cmbServices, ex.Message);
                else
                    SetStatus(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("ChargesUserControl.AddCharge failed", ex);
                SetStatus("The charge could not be saved. Please try again.");
            }
            finally
            {
                btnAddCharge.Enabled = string.Equals(
                    _currentAdmission?.Status,
                    AdmissionStatus.Admitted,
                    StringComparison.OrdinalIgnoreCase);
            }
        }

        private void VoidCharge_Click(object? sender, EventArgs e)
        {
            errProvider.Clear();
            lblStatusMessage.Text = string.Empty;

            if (_chargeService == null || _currentAdmission == null)
                return;

            if (!Permissions.IsAllowed(AppAction.VoidCharge))
            {
                SetStatus("Only an Admin can void a charge.");
                return;
            }

            if (dgvCharges.CurrentRow?.DataBoundItem is not DataRowView rowView)
            {
                SetStatus("Select a charge row to void.");
                return;
            }

            var chargeId = Convert.ToInt32(rowView["ChargeID"]);
            var description = Convert.ToString(rowView["Description"]) ?? "selected charge";

            using var prompt = new VoidReasonDialog(description);
            if (prompt.ShowDialog(FindForm()) != DialogResult.OK)
                return;

            try
            {
                btnVoidCharge.Enabled = false;
                _chargeService.Void(chargeId, prompt.Reason);
                RefreshChargesGridOnly(_currentAdmission.AdmissionID);
                SetStatus("Charge voided.", isError: false);
            }
            catch (ValidationException ex)
            {
                SetStatus(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                SetStatus(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("ChargesUserControl.VoidCharge failed", ex);
                SetStatus("The charge could not be voided. Please try again.");
            }
            finally
            {
                UpdateVoidButtonState();
            }
        }

        private void ChargesGrid_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateVoidButtonState();
        }

        private void UpdateVoidButtonState()
        {
            var canVoid = Permissions.IsAllowed(AppAction.VoidCharge)
                          && _currentAdmission != null
                          && string.Equals(_currentAdmission.Status, AdmissionStatus.Admitted, StringComparison.OrdinalIgnoreCase)
                          && dgvCharges.CurrentRow != null;

            btnVoidCharge.Visible = Permissions.IsAllowed(AppAction.VoidCharge);
            btnVoidCharge.Enabled = canVoid;
        }

        /// <summary>
        /// Validates that the quantity input is a non-empty whole integer between 1 and ChargeService.MaxQuantity.
        /// Rejects empty text, 0, negative numbers, and decimal values.
        /// </summary>
        public static bool TryParseWholeQuantity(string? input, out int quantity, out string errorMessage)
        {
            quantity = 0;
            var trimmed = input?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(trimmed))
            {
                errorMessage = "Quantity is required.";
                return false;
            }

            if (!int.TryParse(trimmed, out var parsed) || parsed < 1 || parsed > ChargeService.MaxQuantity)
            {
                errorMessage = $"Quantity must be a whole number from 1 to {ChargeService.MaxQuantity}.";
                return false;
            }

            quantity = parsed;
            errorMessage = string.Empty;
            return true;
        }

        private void SetStatus(string message, bool isError = true)
        {
            lblStatusMessage.ForeColor = isError
                ? Color.FromArgb(185, 28, 28)
                : Color.FromArgb(21, 128, 61);
            lblStatusMessage.Text = message;
        }

        /// <summary>
        /// Modal dialog prompting an Admin for a mandatory void reason before voiding a charge.
        /// </summary>
        private sealed class VoidReasonDialog : Form
        {
            private readonly TextBox _reasonInput = new();
            private readonly ErrorProvider _dialogErrors = new();

            public string Reason => _reasonInput.Text.Trim();

            public VoidReasonDialog(string chargeDescription)
            {
                Text = "Void charge";
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new Size(430, 200);
                BackColor = Color.White;
                Font = new Font("Segoe UI", 9.5F);

                _dialogErrors.ContainerControl = this;
                _dialogErrors.BlinkStyle = ErrorBlinkStyle.NeverBlink;

                var layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(18),
                    ColumnCount = 2,
                    RowCount = 4
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                var promptLabel = new Label
                {
                    Text = $"Enter a reason for voiding \"{chargeDescription}\":",
                    Dock = DockStyle.Fill,
                    ForeColor = Color.FromArgb(30, 41, 59),
                    AutoEllipsis = true
                };
                layout.Controls.Add(promptLabel, 0, 0);
                layout.SetColumnSpan(promptLabel, 2);

                var caption = new Label
                {
                    Text = "Reason (required, at least 3 characters)",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105)
                };
                layout.Controls.Add(caption, 0, 1);
                layout.SetColumnSpan(caption, 2);

                _reasonInput.Dock = DockStyle.Fill;
                _reasonInput.MaxLength = 300;
                _reasonInput.Margin = new Padding(0, 2, 22, 8);
                layout.Controls.Add(_reasonInput, 0, 2);
                layout.SetColumnSpan(_reasonInput, 2);

                var cancelBtn = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 4, 8, 0),
                    FlatStyle = FlatStyle.Flat
                };
                cancelBtn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);

                var confirmBtn = new Button
                {
                    Text = "Confirm void",
                    Dock = DockStyle.Fill,
                    Margin = new Padding(8, 4, 0, 0),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(185, 28, 28),
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false
                };
                confirmBtn.FlatAppearance.BorderSize = 1;
                confirmBtn.FlatAppearance.BorderColor = Color.FromArgb(220, 38, 38);
                confirmBtn.Click += (_, _) =>
                {
                    _dialogErrors.Clear();
                    if (Reason.Length < 3)
                    {
                        _dialogErrors.SetError(_reasonInput, "Reason is required (at least 3 characters).");
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                };

                layout.Controls.Add(cancelBtn, 0, 3);
                layout.Controls.Add(confirmBtn, 1, 3);

                AcceptButton = confirmBtn;
                CancelButton = cancelBtn;

                Controls.Add(layout);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) _dialogErrors.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}