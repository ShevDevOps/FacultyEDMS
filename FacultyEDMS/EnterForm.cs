using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public partial class EnterForm : Form
    {
        public EnterForm()
        {
            InitializeComponent();
            SetupPlaceholders();
            CheckDBStatus();
        }
        private void SetupPlaceholders()
        {
            SetPlaceholder(EmailBox, "Email");
            SetPlaceholder(PasswordBox, "Password", true);
        }
        private void CheckDBStatus()
        {
            bool isConnected = DatabaseHelper.CheckConnection();
            DBstat.AutoCheck = false;
            DBstat.Checked = isConnected;
            DBstat.ForeColor = isConnected ? Color.Green : Color.Red;
            DBstat.Text = isConnected ? "Connected" : "No DB";
            DBstat.FlatStyle = FlatStyle.Flat;
            if (!isConnected)
            {
                MessageBox.Show("Could not connect to the database. Please check your settings.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholder, bool isPassword = false)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            if (isPassword) textBox.PasswordChar = '\0';

            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (isPassword) textBox.PasswordChar = '*';
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    if (isPassword) textBox.PasswordChar = '\0';
                }
            };
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            string email = (EmailBox.Text == "Email") ? "" : EmailBox.Text;
            string password = (PasswordBox.Text == "Password") ? "" : PasswordBox.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your email and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow userData = DatabaseHelper.AuthenticateUser(email, password);

            if (userData != null)
            {
                int userId = Convert.ToInt32(userData["id"]);
                string username = userData["username"].ToString();
                int role_id = Convert.ToInt32(userData["role_id"]);
                string role_name = userData["role_name"].ToString();

                MessageBox.Show($"Welcome, {username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UserForm userForm = new UserForm(userId, username, role_id, role_name);
                this.Hide();
                userForm.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Invalid email or password, or your account is blocked.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}