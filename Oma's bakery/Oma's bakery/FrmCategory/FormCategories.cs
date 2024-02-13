using MySql.Data.MySqlClient;
using Oma_s_bakery.FrmProducts;
using Oma_s_bakery.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oma_s_bakery.FrmCategory
{
    public partial class FormCategories : Form
    {
        public FormCategories()
        {
            InitializeComponent();
            LoadCategories();
        }
        private void LoadCategories()
        {
            dataGridView1.Rows.Clear();

            DataTable dataTable = MainClass.GetCategories();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowIndex].Cells["category_id"].Value = row["category_id"];
                    dataGridView1.Rows[rowIndex].Cells["Name"].Value = row["Name"];
                }
            }
            else
            {
                MessageBox.Show("No categories found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCategories addform = new AddCategories();
            addform.ShowDialog();
        
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string keyword = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a search keyword.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"SELECT * FROM categories WHERE Name LIKE '%{keyword}%'";
            DataTable dataTable = MainClass.ExecuteSelectQuery(query);
            dataGridView1.DataSource = dataTable;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        
            int categoryId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["category_id"].Value);
            string categoryName = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();

           
            EditCatogory editForm = new EditCatogory(categoryId, categoryName);
            editForm.ShowDialog();

          
            LoadCategories();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
             if (dataGridView1.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a category to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        int categoryId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["category_id"].Value);

        DialogResult result = MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            string query = $"DELETE FROM categories WHERE category_id = {categoryId}";

            MainClass.ExecuteNonQuery(query);

            LoadCategories();
        }
        }
    }
}
