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
    public partial class ChangeSummaryForm : Form
    {
        public string ChangeSummary { get; private set; }
        public bool IsChanged { get; private set; }
        public ChangeSummaryForm()
        {
            InitializeComponent();
            IsChanged = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ChangeSummary = txtSummary.Text.Trim();
            if (string.IsNullOrWhiteSpace(ChangeSummary))
            {
                MessageBox.Show("Будь ласка, введіть опис змін або натисніть 'Не змінено'.", "Порожній опис", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IsChanged = true;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnNoChange_Click(object sender, EventArgs e)
        {
            IsChanged = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
