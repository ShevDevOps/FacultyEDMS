using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public partial class LogsForm : Form
    {
        private void LoadLogs()
        {
            DataTable logs = DatabaseHelper.GetAllLogs();
            dataGridView1.DataSource = logs;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Columns.Contains("id"))
                dataGridView1.Columns["id"].Visible = false;
            if (dataGridView1.Columns.Contains("username"))
                dataGridView1.Columns["username"].HeaderText = "Користувач";
            if (dataGridView1.Columns.Contains("action"))
                dataGridView1.Columns["action"].HeaderText = "Дія";
            if (dataGridView1.Columns.Contains("target"))
                dataGridView1.Columns["target"].HeaderText = "Об'єкт";
            if (dataGridView1.Columns.Contains("timestamp"))
                dataGridView1.Columns["timestamp"].HeaderText = "Час";
        }
        public LogsForm()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
