using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Oma_s_bakery
{
    public partial class FormProfile : Form
    {
        private string connectionString = "server=localhost;user id=root;password=123456;database=omabakerydb";
        private string userID;

        public FormProfile(string userID = null, string fullName = null, string email = null, string username = null)
        {
            InitializeComponent();
            LoadUserData(userID, fullName, email, username);
            this.userID = userID;
        }

        private void LoadUserData(string userID, string fullName, string email, string username)
        {
            textBoxUsername.Text = username;
            textBoxFullName.Text = fullName;
            textBoxEmail.Text = email;
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {
            textBoxFullName.ReadOnly = false;
            textBoxEmail.ReadOnly = false;
            textBoxUsername.ReadOnly = false;
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            string newUsername = textBoxUsername.Text;
            string newFullName = textBoxFullName.Text;
            string newEmail = textBoxEmail.Text;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE users SET FullName = @fullName, Email = @email, Username = @username WHERE ID = @userId";
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@fullName", newFullName);
                    command.Parameters.AddWithValue("@email", newEmail);
                    command.Parameters.AddWithValue("@username", newUsername);
                    command.Parameters.AddWithValue("@userId", userID);
                    command.ExecuteNonQuery();

                    MessageBox.Show("User information updated successfully.");

                    textBoxFullName.ReadOnly = true;
                    textBoxEmail.ReadOnly = true;
                    textBoxUsername.ReadOnly = true;
                    LoadUserData(userID, newFullName, newEmail, newUsername);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("MySQL Error: " + ex.Message);
                }
            }
        }
    }
}
