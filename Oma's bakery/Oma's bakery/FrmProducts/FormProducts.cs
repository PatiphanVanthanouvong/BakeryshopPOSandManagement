using Oma_s_bakery.FrmCategory;
using Oma_s_bakery.FrmProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oma_s_bakery.views
{
    public partial class FormProducts : Form
    {
        public FormProducts()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            DataTable dataTable = MainClass.GetProducts();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                
                dataGridView1.Rows.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowIndex].Cells["product_id"].Value = row["productid"];
                    dataGridView1.Rows[rowIndex].Cells["Name"].Value = row["Name"];
                    dataGridView1.Rows[rowIndex].Cells["Price"].Value = row["Price"];

                    int categoryId = Convert.ToInt32(row["category_id"]);
                    string categoryName = MainClass.GetCategoryName(categoryId);
                    dataGridView1.Rows[rowIndex].Cells["category"].Value = categoryName;
                }
            }
            else
            {
                MessageBox.Show("No products found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProducts addForm = new AddProducts();
            addForm.ShowDialog();

          
            LoadProducts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                string query = $"SELECT * FROM products WHERE Name LIKE '%{keyword}%'";
                DataTable dataTable = MainClass.ExecuteSelectQuery(query);
                dataGridView1.DataSource = dataTable;
                
            }
            else
            {
                LoadProducts();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["product_id"].Value);
            string productName = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            decimal productPrice = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Price"].Value);
            

            EditProducts editForm = new EditProducts(productId, productName, productPrice);
            editForm.ShowDialog();
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["product_id"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = $"DELETE FROM products WHERE productid = {productId}";
                MainClass.ExecuteNonQuery(query);
                LoadProducts();
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AddProducts addform = new AddProducts();
            addform.ShowDialog();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
