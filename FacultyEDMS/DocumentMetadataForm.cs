using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public partial class DocumentMetadataForm : Form
    {
        private int? documentId;
        private string initialFilePath;
        private int loggedInUserId;
        private int loggedInUserRoleId;
        private string loggedInUserRoleName;

        public DocumentMetadataForm(int userId, int userRoleId, string userRoleName)
        {
            InitializeComponent();
            this.documentId = null;
            this.loggedInUserId = userId;
            this.loggedInUserRoleId = userRoleId;
            this.loggedInUserRoleName = userRoleName;
            this.Text = "Create New Document";
            SetupFormForNewDocument();
            SetupEventHandlers();
        }

        public DocumentMetadataForm(int docId, int userId, int userRoleId, string userRoleName)
        {
            InitializeComponent();
            this.documentId = docId;
            this.loggedInUserId = userId;
            this.loggedInUserRoleId = userRoleId;
            this.loggedInUserRoleName = userRoleName;
            this.Text = "Edit Document Metadata";
            LoadDocumentMetadata(docId);
            SetupEventHandlers();
        }

        private void SetupFormForNewDocument()
        {
            PopulateDocumentTypes();
            txtTitle.Text = "New Document Title";
            txtDescription.Text = "Enter document description here.";
            lblFilePath.Text = "No file attached.";

            txtTitle.Enabled = true;
            txtDescription.Enabled = true;
            cmbDocumentType.Enabled = true;
            btnBrowseFile.Enabled = true;
            btnSave.Enabled = true;
            if (btnEditContent != null) btnEditContent.Visible = false;

            DataRow permissions = DatabaseHelper.GetDocumentPermissions(loggedInUserRoleId, (int)cmbDocumentType.SelectedValue);
            if (permissions == null || !Convert.ToBoolean(permissions["can_create"]))
            {
                MessageBox.Show("You do not have permission to create documents.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnSave.Enabled = false;
            }
        }


        private void LoadDocumentMetadata(int docId)
        {
            DataRow docInfo = DatabaseHelper.GetDocumentInfo(docId);
            if (docInfo != null)
            {
                txtTitle.Text = docInfo["title"].ToString();
                txtDescription.Text = docInfo["description"].ToString();
                initialFilePath = docInfo["filePath"].ToString();
                lblFilePath.Text = Path.GetFileName(initialFilePath);

                PopulateDocumentTypes();
                cmbDocumentType.SelectedValue = Convert.ToInt32(docInfo["type_id"]);

                bool canEditMetadata = CheckMetadataEditPermissions(docInfo);

                txtTitle.Enabled = canEditMetadata;
                txtDescription.Enabled = canEditMetadata;
                cmbDocumentType.Enabled = canEditMetadata;
                btnBrowseFile.Enabled = canEditMetadata;
                btnSave.Enabled = canEditMetadata;

                if (btnEditContent != null)
                {
                    bool isRtf = !string.IsNullOrEmpty(initialFilePath) && Path.GetExtension(initialFilePath).ToLower() == ".rtf";
                    btnEditContent.Visible = isRtf;
                    DataRow contentPermissions = DatabaseHelper.GetDocumentPermissions(loggedInUserRoleId, (int)docInfo["type_id"]);
                    bool canEditContent = (contentPermissions != null && (Convert.ToBoolean(contentPermissions["can_edit_all"]) || (Convert.ToBoolean(contentPermissions["can_edit_own"]) && (int)docInfo["authorId"] == loggedInUserId)));

                    bool isSigned = Convert.ToBoolean(docInfo["isSigned"]);

                    btnEditContent.Enabled = isRtf && canEditContent && !isSigned;
                }
            }
            else
            {
                MessageBox.Show("Document not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private bool CheckMetadataEditPermissions(DataRow docInfo)
        {
            DataRow permissions = DatabaseHelper.GetDocumentPermissions(loggedInUserRoleId, (int)docInfo["type_id"]);
            if (permissions == null) return false;

            bool canEditOwn = Convert.ToBoolean(permissions["can_edit_own"]);
            bool canEditAll = Convert.ToBoolean(permissions["can_edit_all"]);

            if (Convert.ToBoolean(docInfo["isSigned"])) return false;

            if (canEditAll) return true;
            if (canEditOwn && (int)docInfo["authorId"] == loggedInUserId) return true;

            return false;
        }


        private void PopulateDocumentTypes()
        {
            DataTable docTypes = DatabaseHelper.GetAllDocumentTypes();
            cmbDocumentType.DataSource = docTypes;
            cmbDocumentType.DisplayMember = "name";
            cmbDocumentType.ValueMember = "id";

            if (cmbDocumentType.Items.Count > 0 && cmbDocumentType.SelectedValue == null)
            {
                cmbDocumentType.SelectedIndex = 0;
            }
        }

        private void SetupEventHandlers()
        {
            btnBrowseFile.Click += BtnBrowseFile_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            if (btnEditContent != null) btnEditContent.Click += BtnEditContent_Click;

            if (!documentId.HasValue)
            {
                cmbDocumentType.SelectedIndexChanged += (s, e) =>
                {
                    if (cmbDocumentType.SelectedValue != null)
                    {
                        DataRow permissions = DatabaseHelper.GetDocumentPermissions(loggedInUserRoleId, (int)cmbDocumentType.SelectedValue);
                        btnSave.Enabled = (permissions != null && Convert.ToBoolean(permissions["can_create"]));
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                };
            }
        }

        private void BtnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Document Files (RTF, PDF)|*.rtf;*.pdf|All files (*.*)|*.*";
                openFileDialog.Title = "Select a Document File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    lblFilePath.Text = Path.GetFileName(openFileDialog.FileName);
                    initialFilePath = openFileDialog.FileName;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            int documentTypeId = (int)cmbDocumentType.SelectedValue;
            string filePath = initialFilePath;

            if (string.IsNullOrWhiteSpace(title) || title == "New Document Title")
            {
                MessageBox.Show("Please enter a valid document title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (documentId.HasValue)
            {
                DataRow currentDocInfo = DatabaseHelper.GetDocumentInfo(documentId.Value);
                if (currentDocInfo != null && Convert.ToBoolean(currentDocInfo["isSigned"]))
                {
                    MessageBox.Show("Cannot edit a signed document.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!CheckMetadataEditPermissions(currentDocInfo))
                {
                    MessageBox.Show("You do not have permission to edit this document's metadata.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newManagedFilePath = null;
                if (!string.IsNullOrEmpty(filePath) && filePath != currentDocInfo["filePath"].ToString())
                {
                    try
                    {
                        string fileName = Path.GetFileName(filePath);
                        string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
                        Directory.CreateDirectory(targetDirectory);
                        newManagedFilePath = Path.Combine(targetDirectory, fileName);
                        File.Copy(filePath, newManagedFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying file: {ex.Message}", "File Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    newManagedFilePath = currentDocInfo["filePath"].ToString();
                }


                bool success = DatabaseHelper.UpdateDocumentMetadata(
                    documentId.Value, title, description, documentTypeId, newManagedFilePath, loggedInUserId);

                if (success)
                {
                    MessageBox.Show("Document metadata updated successfully.");
                    DatabaseHelper.LogAction(loggedInUserId, "Document Metadata Update", $"ID: {documentId.Value}");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update document metadata.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Creating new document
            {
                DataRow permissions = DatabaseHelper.GetDocumentPermissions(loggedInUserRoleId, documentTypeId);
                if (permissions == null || !Convert.ToBoolean(permissions["can_create"]))
                {
                    MessageBox.Show("You do not have permission to create this type of document.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string managedFilePath = null;
                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        string fileName = Path.GetFileName(filePath);
                        string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
                        Directory.CreateDirectory(targetDirectory);
                        managedFilePath = Path.Combine(targetDirectory, fileName);
                        File.Copy(filePath, managedFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying file: {ex.Message}", "File Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                int newDocId = DatabaseHelper.CreateDocument(
                    title, description, loggedInUserId, documentTypeId, managedFilePath);

                if (newDocId > 0)
                {
                    MessageBox.Show("New document created successfully.");
                    DatabaseHelper.LogAction(loggedInUserId, "Document Create", $"ID: {newDocId}");
                    documentId = newDocId;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create new document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditContent_Click(object sender, EventArgs e)
        {
            if (documentId.HasValue && !string.IsNullOrEmpty(initialFilePath) && Path.GetExtension(initialFilePath).ToLower() == ".rtf")
            {
                DataRow docInfo = DatabaseHelper.GetDocumentInfo(documentId.Value);
                if (docInfo != null && Convert.ToBoolean(docInfo["isSigned"]))
                {
                    MessageBox.Show("Cannot edit content of a signed document.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DocumentForm docForm = new DocumentForm(documentId.Value, false, loggedInUserId, loggedInUserRoleId, loggedInUserRoleName);
                docForm.ShowDialog();
                LoadDocumentMetadata(documentId.Value);
            }
            else
            {
                MessageBox.Show("No RTF file attached or document not yet saved.", "Cannot Edit Content", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}