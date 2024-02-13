using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Oma_s_bakery
{
    public partial class FormLogin : Form
    {
        bool isObsecured = true;
        private string connectionString = "server=localhost;user id=root;password=123456;database=omabakerydb";

        public FormLogin()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = isObsecured;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to close?", "Exit the Application", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUsername.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Please input Username and Password", "Error");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM users WHERE Username = @username AND Password = @password";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    command.Parameters.AddWithValue("@password", textBoxPassword.Text);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                          
                            string userID = reader["ID"].ToString();
                            string fullName = reader["FullName"].ToString();
                            string email = reader["Email"].ToString();
                            string username = reader["username"].ToString();
                            MessageBox.Show("Login Successful!");
                            this.Hide();
                            Form1 form1 = new Form1(userID, fullName, email, username);
                            form1.ShowDialog();



                        }
                        else
                        {
                            MessageBox.Show("Incorrect Login Information! Try again.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void showPassword_Click(object sender, EventArgs e)
        {

            isObsecured = !isObsecured; 

            if (isObsecured)
            {
                textBoxPassword.UseSystemPasswordChar = true; 
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = false; 
            }
        }
    }
}
