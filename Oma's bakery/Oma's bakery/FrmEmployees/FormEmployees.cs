using System;
using System.Data;
using System.Windows.Forms;

namespace Oma_s_bakery.FrmEmployees
{
    public partial class FormEmployees : Form
    {
        public FormEmployees()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            dataGridView1.Rows.Clear();

            DataTable dataTable = MainClass.GetEmployees();

            foreach (DataRow row in dataTable.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["Id"].Value = row["Id"];
                dataGridView1.Rows[rowIndex].Cells["Name"].Value = row["Name"];
                dataGridView1.Rows[rowIndex].Cells["Position"].Value = row["Position"];
                dataGridView1.Rows[rowIndex].Cells["Address"].Value = row["Address"];
                dataGridView1.Rows[rowIndex].Cells["PhoneNumber"].Value = row["PhoneNumber"];
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEmployees addForm = new AddEmployees();
            addForm.ShowDialog();
            LoadEmployees();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedEmployeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            string name = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            string position = dataGridView1.SelectedRows[0].Cells["Position"].Value.ToString();
            string address = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
            string phoneNumber = dataGridView1.SelectedRows[0].Cells["PhoneNumber"].Value.ToString();
            EditEmployees editForm = new EditEmployees(selectedEmployeeId, name, position, address, phoneNumber);
            
            editForm.ShowDialog();
            LoadEmployees();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int selectedEmployeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                MainClass.DeleteEmployee(selectedEmployeeId);
                LoadEmployees();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }
        //
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                DataTable dataTable = MainClass.SearchEmployees(keyword);
                dataGridView1.DataSource = null; 
                dataGridView1.Rows.Clear(); // Clear any existing rows
                foreach (DataRow row in dataTable.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();

                    dataGridView1.Rows[rowIndex].Cells["Id"].Value = row["Id"];
                    dataGridView1.Rows[rowIndex].Cells["Name"].Value = row["Name"];
                    dataGridView1.Rows[rowIndex].Cells["Position"].Value = row["Position"];
                    dataGridView1.Rows[rowIndex].Cells["Address"].Value = row["Address"];
                    dataGridView1.Rows[rowIndex].Cells["PhoneNumber"].Value = row["PhoneNumber"];
                }
            }
            else
            {
                LoadEmployees();
            }
        }

    }
}
