using Oma_s_bakery.models;
using System;
using System.Windows.Forms;

namespace Oma_s_bakery.FrmCategory
{
    public partial class AddCategories : sampleAdd
    {
        public AddCategories()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string categoryName = textBoxCategoryName.Text;
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"INSERT INTO categories (Name) VALUES ('{categoryName.Replace("'", "''")}')";

            MainClass.ExecuteNonQuery(query);
            MessageBox.Show("Category added successfully.");

            textBoxCategoryName.Clear();
        }
    }
}
