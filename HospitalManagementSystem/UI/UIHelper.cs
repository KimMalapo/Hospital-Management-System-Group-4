using Microsoft.Data.SqlClient;

namespace UI
{
    /// <summary>Small helpers shared by the screens (switch view, check fields, message boxes).</summary>
    public static class UIHelper
    {
        public static void ShowView<T>(Panel host) where T : UserControl, new()
        {
            ArgumentNullException.ThrowIfNull(host);

            if (host.Controls.Count == 1 && host.Controls[0] is T)
                return;

            ShowView(host, new T());
        }

        public static void ShowView(Panel host, UserControl view)
        {
            ArgumentNullException.ThrowIfNull(host);
            ArgumentNullException.ThrowIfNull(view);

            if (host.Controls.Count == 1 && host.Controls[0].GetType() == view.GetType())
            {
                view.Dispose();
                return;
            }

            host.SuspendLayout();
            try
            {
                while (host.Controls.Count > 0)
                {
                    var currentView = host.Controls[0];
                    host.Controls.Remove(currentView);
                    currentView.Dispose();
                }

                view.Dock = DockStyle.Fill;
                host.Controls.Add(view);
            }
            catch
            {
                if (!view.IsDisposed)
                    view.Dispose();
                throw;
            }
            finally
            {
                host.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Checks a dictionary of field names and values. Returns true if all are filled, 
        /// or false with a detailed list of missing fields.
        /// </summary>
        public static bool ValidateFields(Dictionary<string, string> fields, out string errorMessage)
        {
            var missingFields = fields
                .Where(field => string.IsNullOrWhiteSpace(field.Value))
                .Select(field => $"• {field.Key}")
                .ToList();

            if (missingFields.Count > 0)
            {
                errorMessage = "The following required fields are empty:\n\n" + string.Join("\n", missingFields);
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// Centralized database exception handler for unreachable servers or query failures.
        /// </summary>
        public static void HandleDatabaseError(Exception ex, string actionName)
        {
            BusinessLogic.Logger.Error($"UIHelper: Error {actionName}", ex);

            // Common SQL Server error codes for server down, network failure, or timeout
            if (ex is SqlException sqlEx && (sqlEx.Number is 53 or 20 or -2 || sqlEx.Class >= 20))
            {
                ShowError("Database is unreachable. Please verify that your SQL Server is running and your connection string is correct.", "Database Unreachable");
                return;
            }

            ShowError($"An error occurred while {actionName}: {ex.Message}", "Database Error");
        }

        /// <summary>Shows a service error: rule and permission messages as a warning, anything else as a database error.</summary>
        public static void ShowServiceError(IWin32Window owner, Exception ex, string actionName)
        {
            if (ex is BusinessLogic.ValidationException or UnauthorizedAccessException or InvalidOperationException)
                ShowWarning(owner, ex.Message, "Cannot " + actionName);
            else
                HandleDatabaseError(ex, actionName);
        }

        /// <summary>Asks the user for a line of text (like a void reason). Returns an empty string if they cancel.</summary>
        public static string AskText(string prompt, string title) =>
            Microsoft.VisualBasic.Interaction.InputBox(prompt, title, "").Trim();

        /// <summary>Age in whole years on today's date.</summary>
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static void ShowInfo(string message, string title) =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void ShowInfo(IWin32Window owner, string message, string title) =>
            MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void ShowWarning(string message, string title) =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void ShowWarning(IWin32Window owner, string message, string title) =>
            MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void ShowError(string message, string title) =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
