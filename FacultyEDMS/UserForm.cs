using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
                DataRow docInfo = DatabaseHelper.GetDocumentInfo(docId);
                if (docInfo != null && !string.IsNullOrEmpty(docInfo["filePath"].ToString()) && File.Exists(docInfo["filePath"].ToString()))
                {
                    string filePath = docInfo["filePath"].ToString();

                    // Відкрити файл у стандартній програмі
                    var process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = filePath;
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                    process.WaitForExit(); // Чекаємо, поки користувач закриє файл

                    // Після закриття файлу — діалог для коментаря
                    using (var summaryForm = new ChangeSummaryForm())
                    {
                        if (summaryForm.ShowDialog() == DialogResult.OK && summaryForm.IsChanged)
                        {
                            AddDocumentVersion(docId, summaryForm.ChangeSummary, filePath);
                        }
                    }
                    LoadDocuments();
                }
                else
                {
                    MessageBox.Show("No file attached to this document or file not found.", "Cannot Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a document to edit.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddDocumentVersion(int docId, string changeSummary, string filePath)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["BaseConnectionString"] +
                                      "Database=" + System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] + ";";

            int newVersionNumber = 1;
            string versionQuery = "SELECT IFNULL(MAX(versionNumber), 0) FROM documentversions WHERE document_id = @document_id";
            using (MySqlConnection versionConn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand versionCmd = new MySqlCommand(versionQuery, versionConn))
                {
                    versionCmd.Parameters.AddWithValue("@document_id", docId);
                    versionConn.Open();
                    object result = versionCmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int prevVersion))
                    {
                        newVersionNumber = prevVersion + 1;
                    }
                }
            }

            string insertQuery = "INSERT INTO documentversions (document_id, versionNumber, changeSummary, filePath, createdAt, editorId) " +
                                 "VALUES (@document_id, @versionNumber, @changeSummary, @filePath, NOW(), @editorId)";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@document_id", docId);
                    command.Parameters.AddWithValue("@versionNumber", newVersionNumber);
                    command.Parameters.AddWithValue("@changeSummary", changeSummary);
                    command.Parameters.AddWithValue("@filePath", filePath);
                    command.Parameters.AddWithValue("@editorId", currentUserId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
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

                int newDocId = -1;
                string filePath = "";
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["BaseConnectionString"] +
                                          "Database=" + System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] + ";";
                string query = "SELECT id, filePath FROM documents WHERE authorId = @userId ORDER BY createdAt DESC LIMIT 1";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", currentUserId);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newDocId = Convert.ToInt32(reader["id"]);
                                filePath = reader["filePath"]?.ToString() ?? "";
                            }
                        }
                    }
                }

                if (newDocId != -1)
                {
                    AddDocumentVersion(newDocId, "Документ створено", filePath);
                }
            }
        }

        private void BrowseFileButton_Click(object sender, EventArgs e)
        {
            if (DocumetsView.SelectedRows.Count > 0)
            {
                int docId = Convert.ToInt32(DocumetsView.SelectedRows[0].Cells["id"].Value);

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Document Files (RTF, PDF)|*.rtf;*.pdf|All files (*.*)|*.*";
                    openFileDialog.Title = "Select a Document File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string selectedFilePath = openFileDialog.FileName;
                            string fileName = Path.GetFileName(selectedFilePath);
                            string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
                            Directory.CreateDirectory(targetDirectory);
                            string managedFilePath = Path.Combine(targetDirectory, fileName);

                            File.Copy(selectedFilePath, managedFilePath, true);

                            // Оновлення напряму тут
                            string connectionString = System.Configuration.ConfigurationManager.AppSettings["BaseConnectionString"] +
                                                    "Database=" + System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] + ";";
                            string query = "UPDATE documents SET filePath = @filePath, updatedAt = NOW() WHERE id = @docId";
                            bool success = false;
                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@filePath", managedFilePath);
                                    //command.Parameters.AddWithValue("@userId", currentUserId);
                                    command.Parameters.AddWithValue("@docId", docId);
                                    connection.Open();
                                    success = command.ExecuteNonQuery() > 0;
                                }
                            }
                            query = "INSERT INTO documentversions (document_id, versionNumber, changeSummary, filePath, createdAt, editorId) VALUES (@document_id, @versionNumber, @changeSummary, @filePath, NOW(), @editorId)";                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@filePath", managedFilePath);
                                    command.Parameters.AddWithValue("@document_id", docId);
                                    command.Parameters.AddWithValue("@editorId", currentUserId);
                                    command.Parameters.AddWithValue("@changeSummary", $"File {fileName} attached to Document");
                                    int newVersionNumber = 1;
                                    string versionQuery = "SELECT IFNULL(MAX(versionNumber), 0) FROM documentversions WHERE document_id = @document_id";
                                    using (MySqlConnection versionConn = new MySqlConnection(connectionString))
                                    {
                                        using (MySqlCommand versionCmd = new MySqlCommand(versionQuery, versionConn))
                                        {
                                            versionCmd.Parameters.AddWithValue("@document_id", docId);
                                            versionConn.Open();
                                            object result = versionCmd.ExecuteScalar();
                                            if (result != null && int.TryParse(result.ToString(), out int prevVersion))
                                            {
                                                newVersionNumber = prevVersion + 1;
                                            }
                                        }
                                    }
                                    command.Parameters.AddWithValue("@versionNumber", newVersionNumber);
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                            }
                            if (success)
                            {
                                MessageBox.Show("File attached successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDocuments();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update file path in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error attaching file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a document.", "No Document Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}