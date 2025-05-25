using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public static class DatabaseSetup
    {
        private static string baseConnectionString = ConfigurationManager.AppSettings["BaseConnectionString"];
        private static string dbName = ConfigurationManager.AppSettings["DatabaseName"];

        private static string connectionString = $"{baseConnectionString}Database={dbName};";

        public static void EnsureDatabaseExists()
        {
            try
            {
                // 1. Check if DB exists
                if (!DatabaseExists())
                {
                    CreateDatabase();
                    CreateTables();
                }
                // 2. If DB exists, check if tables exist
                else if (!TablesExist())
                {
                    CreateTables();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Critical database connection or setup error: {ex.Message}\nCheck your MySQL server settings and connection string.", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General error during DB setup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // Check if the database exists
        private static bool DatabaseExists()
        {
            using (MySqlConnection connection = new MySqlConnection(baseConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand($"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{dbName}'", connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return (result != null && result.ToString() == dbName);
                }
            }
        }

        // Create the database
        private static void CreateDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(baseConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show($"Database '{dbName}' successfully created.", "DB Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static bool TablesExist()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand($"SHOW TABLES LIKE 'users';", connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return (result != null && result.ToString() == "users");
                }
            }
        }

        private static void CreateTables()
        {
            string script = LoadSqlScript("FacultyEDMS.create_tables.sql");

            if (string.IsNullOrEmpty(script))
            {
                MessageBox.Show("Failed to load SQL script for table creation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlScript mySqlScript = new MySqlScript(connection, script);
                mySqlScript.Delimiter = ";";
                mySqlScript.Execute();
                MessageBox.Show("Database tables successfully created and populated with base data.", "DB Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Load SQL script from embedded resources
        private static string LoadSqlScript(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}