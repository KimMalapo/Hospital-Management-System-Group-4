using System.Data;
using Microsoft.Data.SqlClient;

namespace UI
{
    /// <summary>Master Data screen: add services, room types, departments, diagnosis codes and drug categories.</summary>
    public partial class MasterDataUserControl : UserControl
    {
        private const string DefaultServiceCategory = "Other";

        // Master-data types stored in a generic lookup table, mapped to the table name used in their stored procedures.
        private static readonly Dictionary<string, string> LookupTables = new()
        {
            ["Room Type"] = "tblRoomTypes",
            ["Diagnosis Code"] = "tblDiagnosisCodes",
            ["Department"] = "tblDepartmentTypes",
            ["Drug Category"] = "tblDrugCategories"
        };

        private bool _loading;
        private readonly ComboBox _serviceCategory = new();
        private readonly Label _serviceCategoryLabel = new();

        public MasterDataUserControl()
        {
            InitializeComponent();
            UITheme.Apply(this);
        }

        private void MasterDataUserControl_Load(object sender, EventArgs e)
        {
            cmbDataType.Items.Clear();
            cmbDataType.Items.AddRange(new object[] { "Service", "Room Type", "Diagnosis Code", "Department", "Drug Category" });
            cmbDataType.SelectedIndexChanged += cmbDataType_SelectedIndexChanged;
            dgvMasterData.SelectionChanged += dgvMasterData_SelectionChanged;
            numRate.Maximum = 10000000;
            _serviceCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            _serviceCategory.Items.AddRange(new object[] { "Consultation", "Laboratory", "Imaging", "Procedure", "Medication", "Supply", DefaultServiceCategory });
            _serviceCategory.Location = new Point(235, 315);
            _serviceCategory.Size = new Size(200, 27);
            _serviceCategory.Font = txtCode.Font;
            _serviceCategory.Visible = false;
            _serviceCategoryLabel.AutoSize = true;
            _serviceCategoryLabel.Location = new Point(235, 290);
            _serviceCategoryLabel.Font = lblRate.Font;
            _serviceCategoryLabel.Text = "Category:";
            _serviceCategoryLabel.Visible = false;
            grpEntry.Controls.Add(_serviceCategory);
            grpEntry.Controls.Add(_serviceCategoryLabel);
            UITheme.Apply(grpEntry);
            LoadMasterData();
        }

        private string SelectedType => cmbDataType.SelectedItem?.ToString() ?? string.Empty;

        private bool IsServiceSelected => SelectedType == "Service";

        private string SelectedServiceCategory => _serviceCategory.SelectedItem?.ToString() ?? DefaultServiceCategory;

        // Stored-procedure table name for the selected non-service type.
        private string SelectedLookupTable =>
            LookupTables.TryGetValue(SelectedType, out var table)
                ? table
                : throw new InvalidOperationException("Select a supported master-data type.");

        private void cmbDataType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var isService = IsServiceSelected;
            lblRate.Text = isService ? "Rate/Cost:" : "Rate/Cost (N/A):";
            numRate.Enabled = isService;
            _serviceCategory.Visible = isService;
            _serviceCategoryLabel.Visible = isService;
            _serviceCategory.SelectedIndex = isService ? _serviceCategory.Items.IndexOf(DefaultServiceCategory) : -1;
            LoadMasterData();
        }

        private void LoadMasterData()
        {
            if (string.IsNullOrEmpty(SelectedType)) return;

            try
            {
                _loading = true;
                dgvMasterData.Rows.Clear();
                DataTable rows = SelectedType switch
                {
                    "Service" => DbHelper.Query(
                        "SELECT ServiceID AS Id, Name, Code, UnitPrice AS Rate, CASE WHEN IsActive = 1 THEN N'Active' ELSE N'Inactive' END AS Status FROM dbo.tblServices ORDER BY Category, Name"),
                    "Room Type" => DbHelper.Query(
                        "SELECT RoomTypeID AS Id, Name, Code, CAST(NULL AS DECIMAL(10,2)) AS Rate, Status FROM dbo.tblRoomTypes WHERE IsDeleted = 0 ORDER BY Code"),
                    "Diagnosis Code" => DbHelper.Query(
                        "SELECT DiagnosisCodeID AS Id, Description AS Name, Code, CAST(NULL AS DECIMAL(10,2)) AS Rate, Status FROM dbo.tblDiagnosisCodes WHERE IsDeleted = 0 ORDER BY Code"),
                    "Department" => DbHelper.Query(
                        "SELECT DepartmentTypeID AS Id, Name, Code, CAST(NULL AS DECIMAL(10,2)) AS Rate, Status FROM dbo.tblDepartmentTypes WHERE IsDeleted = 0 ORDER BY Code"),
                    "Drug Category" => DbHelper.Query(
                        "SELECT DrugCategoryID AS Id, Name, Code, CAST(NULL AS DECIMAL(10,2)) AS Rate, Status FROM dbo.tblDrugCategories WHERE IsDeleted = 0 ORDER BY Code"),
                    _ => new DataTable()
                };

                foreach (DataRow row in rows.Rows)
                {
                    object rate = row["Rate"] == DBNull.Value ? string.Empty : Convert.ToDecimal(row["Rate"]).ToString("0.00");
                    dgvMasterData.Rows.Add(row["Id"], SelectedType, row["Name"], row["Code"], rate, row["Status"]);
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Unable to load master data: {ex.Message}", "Master Data Error");
            }
            finally
            {
                _loading = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrEmpty(SelectedType))
            {
                UIHelper.ShowWarning("Select a data type and enter both a name and code.", "Validation Error");
                return;
            }

            try
            {
                string code = txtCode.Text.Trim();
                string name = txtDescription.Text.Trim();
                string status = chkActive.Checked ? "Active" : "Inactive";
                if (IsServiceSelected)
                {
                    DbHelper.Execute("EXEC dbo.usp_tblServices_Create @Code, @Name, @Category, @DrugCategoryCode, @UnitPrice",
                        new SqlParameter("@Code", code), new SqlParameter("@Name", name),
                        new SqlParameter("@Category", SelectedServiceCategory),
                        new SqlParameter("@DrugCategoryCode", DBNull.Value),
                        new SqlParameter("@UnitPrice", numRate.Value));
                }
                else if (LookupTables.TryGetValue(SelectedType, out var table))
                {
                    DbHelper.Execute($"EXEC dbo.usp_{table}_Create @Code, @Name, @Status, @Notes",
                        new SqlParameter("@Code", code), new SqlParameter("@Name", name),
                        new SqlParameter("@Status", status), new SqlParameter("@Notes", DBNull.Value));
                }
                LoadMasterData();
                ClearForm();
                UIHelper.ShowInfo("Master data added.", "Success");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Unable to add master data: {ex.Message}", "Master Data Error");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMasterData.SelectedRows.Count == 0)
            {
                UIHelper.ShowWarning("Select a record to edit.", "Selection Required");
                return;
            }

            try
            {
                DataGridViewRow row = dgvMasterData.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["colID"].Value);
                string name = txtDescription.Text.Trim();
                string status = chkActive.Checked ? "Active" : "Inactive";
                if (IsServiceSelected)
                {
                    DbHelper.Execute("EXEC dbo.usp_tblServices_Update @ServiceID, @Code, @Name, @Category, @DrugCategoryCode, @UnitPrice, @IsActive",
                        new SqlParameter("@ServiceID", id), new SqlParameter("@Code", txtCode.Text.Trim()),
                        new SqlParameter("@Name", name), new SqlParameter("@Category", SelectedServiceCategory),
                        new SqlParameter("@DrugCategoryCode", DBNull.Value), new SqlParameter("@UnitPrice", numRate.Value),
                        new SqlParameter("@IsActive", chkActive.Checked));
                }
                else
                {
                    DbHelper.Execute($"EXEC dbo.usp_{SelectedLookupTable}_Update @Id, @Name, @Status, @Notes",
                        new SqlParameter("@Id", id), new SqlParameter("@Name", name),
                        new SqlParameter("@Status", status), new SqlParameter("@Notes", DBNull.Value));
                }
                LoadMasterData();
                UIHelper.ShowInfo("Master data updated.", "Success");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Unable to update master data: {ex.Message}", "Master Data Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMasterData.SelectedRows.Count == 0)
            {
                UIHelper.ShowWarning("Select a record to deactivate.", "Selection Required");
                return;
            }

            DataGridViewRow row = dgvMasterData.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["colID"].Value);
            try
            {
                if (IsServiceSelected)
                {
                    DbHelper.Execute("EXEC dbo.usp_tblServices_Deactivate @ServiceID", new SqlParameter("@ServiceID", id));
                }
                else
                {
                    DbHelper.Execute($"EXEC dbo.usp_{SelectedLookupTable}_SoftDelete @Id", new SqlParameter("@Id", id));
                }
                LoadMasterData();
                ClearForm();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Unable to deactivate master data: {ex.Message}", "Master Data Error");
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtDescription.Clear();
            txtCode.Clear();
            numRate.Value = 0;
            if (IsServiceSelected && _serviceCategory.Items.Count > 0) _serviceCategory.SelectedIndex = _serviceCategory.Items.IndexOf(DefaultServiceCategory);
            chkActive.Checked = true;
            dgvMasterData.ClearSelection();
        }

        private void dgvMasterData_SelectionChanged(object? sender, EventArgs e)
        {
            if (_loading || dgvMasterData.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvMasterData.SelectedRows[0];
            txtDescription.Text = row.Cells["colDescription"].Value?.ToString();
            txtCode.Text = row.Cells["colCode"].Value?.ToString();
            if (decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate)) numRate.Value = rate;
            chkActive.Checked = row.Cells["colStatus"].Value?.ToString() == "Active";

            if (IsServiceSelected)
            {
                DataTable service = DbHelper.Query(
                    "SELECT Category FROM dbo.tblServices WHERE ServiceID = @ServiceID",
                    new SqlParameter("@ServiceID", Convert.ToInt32(row.Cells["colID"].Value)));
                if (service.Rows.Count > 0)
                    _serviceCategory.SelectedItem = service.Rows[0]["Category"].ToString();
            }
        }
    }
}
