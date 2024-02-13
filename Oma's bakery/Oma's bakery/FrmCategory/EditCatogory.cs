using MySql.Data.MySqlClient;
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

namespace Oma_s_bakery.FrmCategory
{
    public partial class EditCatogory : sampleAdd
    {
        private int categoryId;
        public EditCatogory(int categoryId, string categoryName)
        {
            InitializeComponent();
            this.categoryId = categoryId;
            textBoxCategoryName.Text = categoryName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newCategoryName = textBoxCategoryName.Text;

            string query = "UPDATE categories SET Name = @newCategoryName WHERE category_Id = @categoryId";

            MainClass.ExecuteNonQuery(query,
     new MySqlParameter("@newCategoryName", newCategoryName),
     new MySqlParameter("@categoryId", categoryId));

            MessageBox.Show("Category updated successfully.");

            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxCategoryName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
