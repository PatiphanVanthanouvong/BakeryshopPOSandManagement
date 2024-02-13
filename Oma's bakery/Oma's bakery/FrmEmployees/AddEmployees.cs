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
    public partial class AddEmployees : sampleAdd
    {
        public AddEmployees()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string position = textBoxPosition.Text;
            string address = textBoxAddress.Text;
            string phoneNumber = textBoxPhoneNumber.Text;

            MainClass.AddEmployee(name, position, address, phoneNumber);
            MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
