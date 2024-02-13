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

namespace Oma_s_bakery.FrmEmployees
{
    public partial class EditEmployees : sampleAdd
    {
        private int employeeId;

        public EditEmployees(int employeeId, string name, string position, string address, string phoneNumber)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            textBoxName.Text = name;
            textBoxPosition.Text = position;
            textBoxAddress.Text = address;
            textBoxPhoneNumber.Text = phoneNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string position = textBoxPosition.Text;
            string address = textBoxAddress.Text;
            string phoneNumber = textBoxPhoneNumber.Text;

            MainClass.UpdateEmployee(employeeId, name, position, address, phoneNumber);
            MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
