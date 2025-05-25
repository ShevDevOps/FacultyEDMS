using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public static class DatabaseHelper
{
    private static string baseConnectionString = ConfigurationManager.AppSettings["BaseConnectionString"];
    private static string dbName = ConfigurationManager.AppSettings["DatabaseName"];

    private static string connectionString = $"{baseConnectionString}Database={dbName};";

    private static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool CheckConnection()
    {
        MySqlConnection connection = null;
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Database connection error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        finally
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

    // Authenticate user
    public static DataRow AuthenticateUser(string email, string password)
    {
        string hashedPassword = HashPassword(password);
        string query = @"
            SELECT
                u.id,
                u.username,
                u.role_id,
                r.name AS role_name
            FROM
                users u
            JOIN
                roles r ON u.role_id = r.id
            WHERE
                u.email = @email
                AND u.password = @password
                AND u.isBlocked = 0;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", hashedPassword);

                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            LogAction(Convert.ToInt32(dataTable.Rows[0]["id"]), "User Login", "Success");
                            return dataTable.Rows[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection error during authentication: " + ex.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        LogAction(-1, "User Login", $"Failed attempt for email: {email}");
        return null;
    }

    // Get documents considering role and filters
    public static DataTable GetDocuments(int userId, int userRoleId, string searchTerm, string searchBy, DateTime? dateFrom, DateTime? dateTo)
    {
        string query = @"
            SELECT
                d.id,
                d.title,
                u.username AS author,
                dt.name AS documentType,
                d.createdAt,
                d.isSigned,
                d.isProtected,
                d.filePath
            FROM
                documents d
            JOIN
                users u ON d.authorId = u.id
            JOIN
                documenttypes dt ON d.type_id = dt.id
            JOIN
                role_document_permissions rdp ON rdp.document_type_id = d.type_id
            WHERE 1=1 ";

        // !!! ADD FILTERING LOGIC BASED ON ROLE AND DOCUMENT TYPE !!!
        query += " AND rdp.role_id = @userRoleId ";

        query += " AND (d.authorId = @userId OR rdp.can_view_all = TRUE) ";


        if (!string.IsNullOrEmpty(searchTerm))
        {
            if (searchBy == "title") query += " AND d.title LIKE @searchTerm";
            else if (searchBy == "author") query += " AND u.username LIKE @searchTerm";
            // if (searchBy == "type") query += " AND dt.name LIKE @searchTerm"; // Optional
        }
        if (dateFrom.HasValue) query += " AND d.createdAt >= @dateFrom";
        if (dateTo.HasValue) query += " AND d.createdAt <= @dateTo";

        // Order results
        query += " ORDER BY d.createdAt DESC";

        DataTable dataTable = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@userRoleId", userRoleId);

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                }
                if (dateFrom.HasValue)
                {
                    command.Parameters.AddWithValue("@dateFrom", dateFrom.Value);
                }
                if (dateTo.HasValue)
                {
                    command.Parameters.AddWithValue("@dateTo", dateTo.Value);
                }

                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading documents: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return dataTable;
    }

    // Get document history
    public static DataTable GetDocumentHistory(int documentId)
    {
        string query = @"
            SELECT
                dv.versionNumber,
                dv.changeSummary,
                dv.createdAt,
                u.username AS editorName
            FROM
                documentversions dv
            JOIN
                users u ON dv.editorId = u.id
            WHERE
                dv.document_id = @documentId
            ORDER BY
                dv.versionNumber DESC;";

        DataTable dataTable = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@documentId", documentId);
                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading document history: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return dataTable;
    }

    // Get document information
    public static DataRow GetDocumentInfo(int documentId)
    {
        string query = @"
        SELECT
            d.id,
            d.title,
            d.description,
            u.username AS author,
            dt.name AS documentType,
            d.type_id, -- ADDED this line
            d.createdAt,
            d.updatedAt,
            d.isSigned,
            d.isProtected,
            d.filePath,
            d.authorId
        FROM
            documents d
        JOIN
            users u ON d.authorId = u.id
        JOIN
            documenttypes dt ON d.type_id = dt.id
        WHERE
            d.id = @documentId;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@documentId", documentId);
                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting document information: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return null;
    }

    // Logging actions
    public static void LogAction(int userId, string action, string target)
    {
        string query = "INSERT INTO logs (userId, action, target, timestamp) VALUES (@userId, @action, @target, NOW());";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId > 0 ? (object)userId : DBNull.Value);
                command.Parameters.AddWithValue("@action", action);
                command.Parameters.AddWithValue("@target", target);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Logging error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    // Save document
    public static int SaveDocument(string title, int authorId, int documentTypeId, string filePath, bool isNew = true, int? documentId = null)
    {
        string query;
        if (isNew)
        {
            query = "INSERT INTO documents (title, authorId, type_id, createdAt, filePath) VALUES (@title, @authorId, @documentTypeId, NOW(), @filePath); SELECT LAST_INSERT_ID();";
        }
        else
        {
            query = "UPDATE documents SET title = @title, filePath = @filePath, updatedAt = NOW() WHERE id = @documentId;";
        }

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@authorId", authorId);
                command.Parameters.AddWithValue("@filePath", filePath);
                command.Parameters.AddWithValue("@documentTypeId", documentTypeId);
                if (!isNew) command.Parameters.AddWithValue("@documentId", documentId.Value);

                try
                {
                    connection.Open();
                    if (isNew)
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                        return documentId.Value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving document: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }
    }

    public static DataTable GetAllDocumentTypes()
    {
        string query = "SELECT id, name FROM documenttypes ORDER BY name;";
        DataTable dataTable = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading document types: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return dataTable;
    }

    public static bool SignDocument(int documentId)
    {
        string query = "UPDATE documents SET isSigned = TRUE, updatedAt = NOW() WHERE id = @documentId;";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@documentId", documentId);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error signing document: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }

    public static int CreateDocument(string title, string description, int authorId, int documentTypeId, string filePath)
    {
        string query = @"
                INSERT INTO documents (title, description, authorId, type_id, filePath, createdAt, updatedAt)
                VALUES (@title, @description, @authorId, @type_id, @filePath, NOW(), NOW());
                SELECT LAST_INSERT_ID();";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@authorId", authorId);
                command.Parameters.AddWithValue("@type_id", documentTypeId);
                command.Parameters.AddWithValue("@filePath", filePath ?? (object)DBNull.Value);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating document: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return -1;
    }

    public static bool UpdateDocumentMetadata(int documentId, string title, string description, int documentTypeId, string filePath, int editorId)
    {
        string query = @"
                UPDATE documents
                SET
                    title = @title,
                    description = @description,
                    type_id = @type_id,
                    filePath = @filePath,
                    updatedAt = NOW()
                WHERE
                    id = @documentId;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@documentId", documentId);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@type_id", documentTypeId);
                command.Parameters.AddWithValue("@filePath", filePath ?? (object)DBNull.Value);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating document metadata: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }

    public static bool CanUserCreateAnyDocument(int roleId)
    {
        string query = "SELECT COUNT(*) FROM role_document_permissions WHERE role_id = @roleId AND can_create = TRUE;";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@roleId", roleId);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking create permissions: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return false;
    }

    // Helper to get document permissions for a specific role and document type
    public static DataRow GetDocumentPermissions(int roleId, int documentTypeId)
    {
        string query = @"
            SELECT
                can_create,
                can_edit_own,
                can_edit_all,
                can_sign,
                can_view_all
            FROM
                role_document_permissions
            WHERE
                role_id = @roleId AND document_type_id = @documentTypeId;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@roleId", roleId);
                command.Parameters.AddWithValue("@documentTypeId", documentTypeId);

                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting document permissions: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        return null;
    }
}