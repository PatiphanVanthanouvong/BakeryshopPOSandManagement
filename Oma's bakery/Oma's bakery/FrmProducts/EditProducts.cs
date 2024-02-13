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
    public partial class EditProducts : sampleAdd
    {
        private int productId;
        private string productName;
        private decimal productPrice;
       

        public EditProducts(int productId, string productName, decimal productPrice )
        {
            InitializeComponent();

            this.productId = productId;
            this.productName = productName;
            this.productPrice = productPrice;
            

            textBoxProductName.Text = productName;
            textBoxProductPrice.Text = productPrice.ToString();
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
            string newName = textBoxProductName.Text;
            decimal newPrice = decimal.Parse(textBoxProductPrice.Text);
            string selectedCategoryName = comboBoxCategories.SelectedItem.ToString();

            int categoryId = MainClass.GetCategoryIdByName(selectedCategoryName);

            MainClass.UpdateProduct(productId, newName, newPrice, categoryId);

            DialogResult = DialogResult.OK; 
            Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }

}
