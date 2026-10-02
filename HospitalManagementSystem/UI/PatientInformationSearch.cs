using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BusinessLogic;

namespace UI
{
    public partial class PatientInformationSearch : Form
    {
        private readonly PatientService? _service;

        public PatientInformationSearch()
        {
            InitializeComponent();
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new PatientService(cs);
        }

        // Search helper - execute a parameterized query and return results
        public DataTable SearchPatients(string sql, params SqlParameter[] parameters)
        {
            try
            {
                return _service != null ? _service.GetPatients(sql) : new DataTable();
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
