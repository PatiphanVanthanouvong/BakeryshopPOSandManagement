using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Oma_s_bakery.FrmCategory;
using Oma_s_bakery.FrmEmployees;
using Oma_s_bakery.FrmPOS;
using Oma_s_bakery.views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Oma_s_bakery
{
    public partial class Form1 : Form
    {
        private string userID;
        private string fullName;
        private string email;
        private string username;
        private string connectionString = "server=localhost;userid=root;password=123456;database=hrdb";
       

        bool isDrawerClick = false;
        public Form1(string userID = null, string fullName = null, string email = null, string username = null)
        {
           InitializeComponent();
        
            this.userID = userID;
            this.fullName = fullName;
            this.email = email;
            this.username = username;
        }
        



      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Close();
           
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to close?", "Exit the Application", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            
                FormProducts form = new FormProducts();
                AddControls(form);

            }

        private void ToggleDrawer()
        {
            isDrawerClick = !isDrawerClick; 

            if (isDrawerClick)
            {
              

                panel1.Size = new Size(70, 970);
            }
            else
            {
      
                panel1.Size = new Size(220, 970);
            }
        }

       

        

        private void btnUsers_Click(object sender, EventArgs e)
        {
            string userID = this.userID;
            string fullName = this.fullName;
            string email = this.email;
          string username = this.username;
            FormProfile userPro = new FormProfile(userID, fullName, email, username);
         AddControls(userPro);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to close?", "Exit the Application", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panelControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FormCategories form = new FormCategories();
                AddControls(form);
         
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddControls(Form F)
        {
            panelControl.Controls.Clear();
            F.Dock = DockStyle.Fill;
            F.TopLevel = false;
            panelControl.Controls.Add(F);
            F.Show();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            FormEmployees form = new FormEmployees();
            AddControls(form);

        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            FormPOS form = new FormPOS();
            AddControls(form);
        }
    }
}
