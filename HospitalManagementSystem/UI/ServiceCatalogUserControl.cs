using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class ServiceCatalogUserControl : UserControl
    {
        private ServiceCatalogService? _catalogService;
        private int? _selectedServiceId;
        private bool _suppressSelectionChange;

        public ServiceCatalogUserControl()
        {
            InitializeComponent();
        }

        private void ServiceCatalogUserControl_Load(object? sender, EventArgs e)
        {
            var canManage = Permissions.IsAllowed(AppAction.ManageServices);
            pnlFormCard.Enabled = canManage;
            if (!canManage)
            {
                lblFormError.Text = "Read-only: Only an Admin can add, edit, or deactivate catalog services.";
            }

            try
            {
                _catalogService = new ServiceCatalogService(Config.GetConnectionString());
                RefreshGrid();
                ResetForm();
            }
            catch (Exception ex)
            {
                Logger.Error("ServiceCatalogUserControl.Load failed", ex);
                SetStatus("Unable to load the service catalog. Check the database connection.", isError: true);
            }
        }

        private void RefreshGrid(int? reselectServiceId = null)
        {
            if (_catalogService == null) return;

            var term = txtSearch.Text.Trim();
            var activeOnly = chkActiveOnly.Checked;

            DataTable table = string.IsNullOrEmpty(term)
                ? _catalogService.GetAll(activeOnly)
                : _catalogService.Search(term, activeOnly);

            _suppressSelectionChange = true;
            try
            {
                dgvServices.DataSource = table;
                dgvServices.ClearSelection();

                if (reselectServiceId.HasValue)
                {
                    foreach (DataGridViewRow row in dgvServices.Rows)
                    {
                        if (row.DataBoundItem is DataRowView view &&
                            Convert.ToInt32(view["ServiceID"]) == reselectServiceId.Value)
                        {
                            row.Selected = true;
                            dgvServices.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
            finally
            {
                _suppressSelectionChange = false;
            }
        }

        private void ServicesGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_suppressSelectionChange) return;

            ClearErrors();

            if (dgvServices.CurrentRow?.DataBoundItem is not DataRowView row)
            {
                ResetForm();
                return;
            }

            _selectedServiceId = Convert.ToInt32(row["ServiceID"]);
            txtCode.Text = Convert.ToString(row["Code"]) ?? string.Empty;
            txtName.Text = Convert.ToString(row["Name"]) ?? string.Empty;

            var unitPrice = row["UnitPrice"] == DBNull.Value ? 0m : Convert.ToDecimal(row["UnitPrice"]);
            txtUnitPrice.Text = unitPrice.ToString("0.00", CultureInfo.InvariantCulture);

            var isActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]);
            chkIsActive.Checked = isActive;

            btnUpdateService.Enabled = Permissions.IsAllowed(AppAction.ManageServices);
            btnDeactivateService.Enabled = Permissions.IsAllowed(AppAction.ManageServices) && isActive;
        }

        private void AddService_Click(object? sender, EventArgs e)
        {
            ClearErrors();
            lblStatusMessage.Text = string.Empty;

            if (_catalogService == null) return;

            if (!TryBuildServiceItem(serviceId: 0, out var item))
                return;

            try
            {
                btnAddService.Enabled = false;
                var newId = _catalogService.Create(item);
                RefreshGrid(newId);
                ResetForm();
                SetStatus($"Service \"{item.Code}\" added.", isError: false);
            }
            catch (ValidationException ex)
            {
                BindValidationError(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                lblFormError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error("ServiceCatalogUserControl.AddService failed", ex);
                SetStatus("The service could not be added. Please try again.", isError: true);
            }
            finally
            {
                btnAddService.Enabled = Permissions.IsAllowed(AppAction.ManageServices);
            }
        }

        private void UpdateService_Click(object? sender, EventArgs e)
        {
            ClearErrors();
            lblStatusMessage.Text = string.Empty;

            if (_catalogService == null) return;

            if (!_selectedServiceId.HasValue)
            {
                lblFormError.Text = "Select a service from the table first before saving changes.";
                return;
            }

            if (!TryBuildServiceItem(_selectedServiceId.Value, out var item))
                return;

            try
            {
                btnUpdateService.Enabled = false;
                _catalogService.Update(item);
                RefreshGrid(item.ServiceID);
                SetStatus($"Service \"{item.Code}\" updated.", isError: false);
            }
            catch (ValidationException ex)
            {
                BindValidationError(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                lblFormError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error("ServiceCatalogUserControl.UpdateService failed", ex);
                SetStatus("The service could not be updated. Please try again.", isError: true);
            }
            finally
            {
                btnUpdateService.Enabled = Permissions.IsAllowed(AppAction.ManageServices) && _selectedServiceId.HasValue;
            }
        }

        private void DeactivateService_Click(object? sender, EventArgs e)
        {
            ClearErrors();
            lblStatusMessage.Text = string.Empty;

            if (_catalogService == null) return;

            if (!_selectedServiceId.HasValue)
            {
                lblFormError.Text = "Select an active service to deactivate.";
                return;
            }

            var code = txtCode.Text.Trim();
            var confirm = MessageBox.Show(
                FindForm(),
                $"Deactivate service \"{code}\"? It will no longer appear when adding new patient charges.",
                "Deactivate service",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                btnDeactivateService.Enabled = false;
                var targetId = _selectedServiceId.Value;
                _catalogService.Deactivate(targetId);
                RefreshGrid(chkActiveOnly.Checked ? null : targetId);
                if (chkActiveOnly.Checked)
                    ResetForm();
                else
                    chkIsActive.Checked = false;

                SetStatus($"Service \"{code}\" deactivated.", isError: false);
            }
            catch (ValidationException ex)
            {
                lblFormError.Text = ex.Message;
            }
            catch (UnauthorizedAccessException ex)
            {
                lblFormError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error("ServiceCatalogUserControl.DeactivateService failed", ex);
                SetStatus("The service could not be deactivated. Please try again.", isError: true);
            }
        }

        private void ClearForm_Click(object? sender, EventArgs e)
        {
            ClearErrors();
            lblStatusMessage.Text = string.Empty;
            ResetForm();

            _suppressSelectionChange = true;
            try
            {
                dgvServices.ClearSelection();
            }
            finally
            {
                _suppressSelectionChange = false;
            }
        }

        private void Search_TextChanged(object? sender, EventArgs e)
        {
            RefreshGrid(_selectedServiceId);
        }

        private void ActiveOnly_CheckedChanged(object? sender, EventArgs e)
        {
            RefreshGrid(_selectedServiceId);
        }

        private bool TryBuildServiceItem(int serviceId, out ServiceItem item)
        {
            item = new ServiceItem();

            var code = txtCode.Text.Trim().ToUpperInvariant();
            var name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                errProvider.SetError(txtCode, "Service code is required.");
                lblFormError.Text = "Service code is required.";
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                errProvider.SetError(txtName, "Service name is required.");
                lblFormError.Text = "Service name is required.";
                return false;
            }

            if (!TryParseUnitPrice(txtUnitPrice.Text, out var price, out var priceError))
            {
                errProvider.SetError(txtUnitPrice, priceError);
                lblFormError.Text = priceError;
                return false;
            }

            item = new ServiceItem
            {
                ServiceID = serviceId,
                Code = code,
                Name = name,
                UnitPrice = price,
                IsActive = chkIsActive.Checked
            };

            return true;
        }

        private void BindValidationError(ValidationException ex)
        {
            lblFormError.Text = ex.Message;

            if (string.Equals(ex.Field, "Code", StringComparison.OrdinalIgnoreCase))
                errProvider.SetError(txtCode, ex.Message);
            else if (string.Equals(ex.Field, "Name", StringComparison.OrdinalIgnoreCase))
                errProvider.SetError(txtName, ex.Message);
            else if (string.Equals(ex.Field, "UnitPrice", StringComparison.OrdinalIgnoreCase))
                errProvider.SetError(txtUnitPrice, ex.Message);
        }

        private void ResetForm()
        {
            _selectedServiceId = null;
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtUnitPrice.Text = "0.00";
            chkIsActive.Checked = true;
            btnUpdateService.Enabled = false;
            btnDeactivateService.Enabled = false;
        }

        private void ClearErrors()
        {
            errProvider.Clear();
            lblFormError.Text = string.Empty;
        }

        private void SetStatus(string message, bool isError)
        {
            lblStatusMessage.ForeColor = isError
                ? Color.FromArgb(185, 28, 28)
                : Color.FromArgb(21, 128, 61);
            lblStatusMessage.Text = message;
        }

        /// <summary>
        /// Parses and validates a non-negative unit price with at most 2 decimal places.
        /// </summary>
        public static bool TryParseUnitPrice(string? input, out decimal price, out string errorMessage)
        {
            price = 0m;
            var trimmed = input?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(trimmed))
            {
                errorMessage = "Unit price is required.";
                return false;
            }

            if (!decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsed) &&
                !decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed))
            {
                errorMessage = "Enter a valid numeric unit price.";
                return false;
            }

            if (parsed < 0m)
            {
                errorMessage = "Unit price cannot be negative.";
                return false;
            }

            if (decimal.Round(parsed, 2) != parsed)
            {
                errorMessage = "Unit price cannot have more than 2 decimal places.";
                return false;
            }

            price = parsed;
            errorMessage = string.Empty;
            return true;
        }
    }
}