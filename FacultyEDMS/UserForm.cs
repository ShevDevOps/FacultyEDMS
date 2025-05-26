using System;
using System.Data;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public partial class UserForm : Form
    {
        private int currentUserId;
        private string currentUsername;
        private int currentUserRoleId;
        private string currentUserRoleName;

        public UserForm(int userId, string username, int roleId, string roleName)
        {
            InitializeComponent();
            this.currentUserId = userId;
            this.currentUsername = username;
            this.currentUserRoleId = roleId;
            this.currentUserRoleName = roleName;

            SetupForm();
            LoadDocuments();
            ConfigureAccess();
        }

        private void SetupForm()
        {
            this.Text = $"Personal Cabinet for <<{currentUsername}>> ({currentUserRoleName})";
            label1.Text = $"Personal Cabinet for <<{currentUsername}>>";
            InfoBox.Visible = false;
            DocumentHistoryView.Visible = false;
            label2.Visible = false;
        }

        // Configure access to elements based on role and document type permissions
        private void ConfigureAccess()
        {
            //Це треба обовязково переробити через запит
            btnCreateNewDocument.Enabled = (currentUserRoleName == "Administrator" || currentUserRoleName == "Teacher" || currentUserRoleName == "Student");

        }

        private void LoadDocuments(string searchTerm = null, string searchBy = null)
        {
            string currentSearchBy = "title";
            if (searchBy != null) currentSearchBy = searchBy;
            DataTable documents = DatabaseHelper.GetDocuments(currentUserId, currentUserRoleId, searchTerm, currentSearchBy, null, null);
            DocumetsView.DataSource = documents;
            DocumetsView.ReadOnly = true;
            if (DocumetsView.Columns.Contains("id")) DocumetsView.Columns["id"].Visible = false;
            if (DocumetsView.Columns.Contains("title")) DocumetsView.Columns["title"].HeaderText = "Title";
            if (DocumetsView.Columns.Contains("author")) DocumetsView.Columns["author"].HeaderText = "Author";
            if (DocumetsView.Columns.Contains("documentType")) DocumetsView.Columns["documentType"].HeaderText = "Type";
            if (DocumetsView.Columns.Contains("createdAt")) DocumetsView.Columns["createdAt"].HeaderText = "Created At";
            if (DocumetsView.Columns.Contains("isSigned")) DocumetsView.Columns["isSigned"].HeaderText = "Signed";
            if (DocumetsView.Columns.Contains("isProtected")) DocumetsView.Columns["isProtected"].HeaderText = "Protected";


            DocumetsView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {   
            if (radioButton1.Checked == true) LoadDocuments(SearchBox.Text, "title");
            else if (radioButton2.Checked == true) LoadDocuments(SearchBox.Text, "author");
            else LoadDocuments(SearchBox.Text, null);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.OpenForms["EnterForm"]?.Show();
            this.Close();
        }

        private void RedactButton_Click(object sender, EventArgs e)
        {
            if (DocumetsView.SelectedRows.Count > 0)
            {
                int docId = Convert.ToInt32(DocumetsView.SelectedRows[0].Cells["id"].Value);
                DocumentMetadataForm metadataForm = new DocumentMetadataForm(docId, currentUserId, currentUserRoleId, currentUserRoleName);
                if (metadataForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDocuments();
                }
            }
            else
            {
                MessageBox.Show("Please select a document to edit its metadata.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LookButton_Click(object sender, EventArgs e)
        {
            if (DocumetsView.SelectedRows.Count > 0)
            {
                int docId = Convert.ToInt32(DocumetsView.SelectedRows[0].Cells["id"].Value);
                DataRow docInfo = DatabaseHelper.GetDocumentInfo(docId);
                if (docInfo != null && !string.IsNullOrEmpty(docInfo["filePath"].ToString()))
                {
                    // Open the actual content form in read-only mode
                    DocumentForm docForm = new DocumentForm(docId, true, currentUserId, currentUserRoleId, currentUserRoleName);
                    docForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No file attached to this document or file not found.", "Cannot View Content", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a document to view its content.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            if (DocumetsView.SelectedRows.Count > 0)
            {
                int docId = Convert.ToInt32(DocumetsView.SelectedRows[0].Cells["id"].Value);
                DataRow docInfo = DatabaseHelper.GetDocumentInfo(docId);
                if (docInfo != null)
                {
                    // Fill InfoBox with data
                    InfoBox.Text = $"Information:\n" +
                                   $"Title: {docInfo["title"]}\n" +
                                   $"Author: {docInfo["author"]}\n" +
                                   $"Type: {docInfo["documentType"]}\n" + // Display document type
                                   $"Created At: {docInfo["createdAt"]}\n" +
                                   $"Last Updated: {docInfo["updatedAt"]}\n" +
                                   $"Signed: {(Convert.ToBoolean(docInfo["isSigned"]) ? "Yes" : "No")}\n" +
                                   $"Protected: {(Convert.ToBoolean(docInfo["isProtected"]) ? "Yes" : "No")}\n" +
                                   $"File Path: {docInfo["filePath"]}";
                    // You might want to use separate labels within InfoBox for better layout.
                    InfoBox.Visible = true;
                    DocumentHistoryView.Visible = false; // Hide history if open
                    label2.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a document.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            InfoBox.Visible = false;
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            if (DocumetsView.SelectedRows.Count > 0)
            {
                int docId = Convert.ToInt32(DocumetsView.SelectedRows[0].Cells["id"].Value);
                DataTable history = DatabaseHelper.GetDocumentHistory(docId);
                DocumentHistoryView.DataSource = history;
                if (DocumentHistoryView.Columns.Contains("versionNumber")) DocumentHistoryView.Columns["versionNumber"].HeaderText = "Version";
                if (DocumentHistoryView.Columns.Contains("changeSummary")) DocumentHistoryView.Columns["changeSummary"].HeaderText = "Change Description";
                if (DocumentHistoryView.Columns.Contains("createdAt")) DocumentHistoryView.Columns["createdAt"].HeaderText = "Date";
                if (DocumentHistoryView.Columns.Contains("editorName")) DocumentHistoryView.Columns["editorName"].HeaderText = "Editor"; // New column

                DocumentHistoryView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DocumentHistoryView.Visible = true;
                label2.Visible = true;
                InfoBox.Visible = false; 
            }
            else
            {
                MessageBox.Show("Please select a document to view history.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCreateNewDocument_Click(object sender, EventArgs e)
        {
            DocumentMetadataForm metadataForm = new DocumentMetadataForm(currentUserId, currentUserRoleId, currentUserRoleName);
            if (metadataForm.ShowDialog() == DialogResult.OK)
            {
                LoadDocuments();
            }
        }
    }
}