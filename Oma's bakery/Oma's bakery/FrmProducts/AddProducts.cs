using Oma_s_bakery.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oma_s_bakery.FrmProducts
{
    public partial class AddProducts : sampleAdd
    {
        public AddProducts()
        {
            InitializeComponent();
            LoadCategoriesIntoComboBox();
        }
        private void LoadCategoriesIntoComboBox()
        {
            comboBoxCategories.Items.Clear();

            DataTable dataTable = MainClass.GetCategories();

            foreach (DataRow row in dataTable.Rows)
            {
                string categoryName = row["Name"].ToString();
                comboBoxCategories.Items.Add(categoryName);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = textBoxProductName.Text;
                decimal productPrice = decimal.Parse(textBoxProductPrice.Text);

                string selectedCategoryName = comboBoxCategories.SelectedItem.ToString();

                int categoryId = MainClass.GetCategoryIdByName(selectedCategoryName);

                MainClass.InsertProduct(productName, productPrice, categoryId);

                MessageBox.Show("Product saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
