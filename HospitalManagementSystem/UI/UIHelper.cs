using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace UI
{
    public static class UIHelper
    {
        /// <summary>
        /// Checks a dictionary of field names and values. Returns true if all are filled, 
        /// or false with a detailed list of missing fields.
        /// </summary>
        public static bool ValidateFields(Dictionary<string, string> fields, out string errorMessage)
        {
            var missingFields = new List<string>();

            foreach (var field in fields)
            {
                if (string.IsNullOrWhiteSpace(field.Value))
                {
                    missingFields.Add($"• {field.Key}");
                }
            }

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

            if (ex is SqlException sqlEx)
            {
                // Common SQL Server error codes for server down, network failure, or timeout
                if (sqlEx.Number == 53 || sqlEx.Number == 20 || sqlEx.Number == -2 || sqlEx.Class >= 20)
                {
                    MessageBox.Show("Database is unreachable. Please verify that your SQL Server is running and your connection string is correct.", "Database Unreachable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show($"An error occurred while {actionName}: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}