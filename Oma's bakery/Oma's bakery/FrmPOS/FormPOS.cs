using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Oma_s_bakery.FrmPOS
{
    public partial class FormPOS : Form
    {
        private DataTable productsTable = new DataTable();

        public FormPOS()
        {
            InitializeComponent();
            InitializeProductsTable();
            LoadProducts();
        }

        private void InitializeProductsTable()
        {
            productsTable.Columns.Add("ProductID", typeof(int));
            productsTable.Columns.Add("Name", typeof(string));
            productsTable.Columns.Add("Price", typeof(double));
        }

        private void LoadProducts()
        {
            DataTable dt = MainClass.GetProducts();

            comboBoxProducts.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int productId = Convert.ToInt32(row["ProductID"]);
                string productName = row["Name"].ToString();
                double productPrice = Convert.ToDouble(row["Price"]);

                comboBoxProducts.Items.Add(new ProductItem(productId, productName, productPrice));
            }
        }


        private void comboBoxProducts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ProductItem selectedProduct = comboBoxProducts.SelectedItem as ProductItem;
            if (selectedProduct != null)
            {
                textBoxPrice.Text = selectedProduct.Price.ToString();
            }
        }

        private void buttonAddOrder_Click_1(object sender, EventArgs e)
        {
            int quantity;
            double price;
            if (!int.TryParse(textBoxQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(textBoxPrice.Text, out price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            double amount = quantity * price;

            dataGridView1.Rows.Add(comboBoxProducts.SelectedItem.ToString(),
                                         quantity,
                                         price,
                                         amount);

            UpdateTotalAmount();
        }


        private void UpdateTotalAmount()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                double rowQuantity = Convert.ToDouble(row.Cells["Quantity"].Value);
                double rowPrice = Convert.ToDouble(row.Cells["Price"].Value);
                double rowAmount = rowQuantity * rowPrice;
                row.Cells["Amount"].Value = rowAmount;
                totalAmount += rowAmount;
            }
            textBoxAmount.Text = totalAmount.ToString();
        }


        public class ProductItem
        {
            public int ProductID { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }

            public ProductItem(int productId, string name, double price)
            {
                ProductID = productId;
                Name = name;
                Price = price;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                UpdateTotalAmount();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

      private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
{
    try
    {
                // Get employee information
                //var employeeInfo = GetEmployeeInfo();
                string employeeName = "Sinh"; 
        string employeeID = "001";

        e.Graphics.DrawString("Oma's Bakery HomeMade by Soud", new Font("Times New Roman", 30, FontStyle.Bold), Brushes.Black, new Point(100, 100));
        e.Graphics.DrawString("Ban Suanmone, Vientiane Cap.", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, 150));

        e.Graphics.DrawString("Sale Date : ", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(500, 150));
        e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(600, 150));

        
        e.Graphics.DrawString("Employee Name : ", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, 200));
        e.Graphics.DrawString(employeeName, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(300, 200));

      

        e.Graphics.DrawString("Customer Name : ", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(500, 200));
        e.Graphics.DrawString(textBoxCustomer.Text, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(650, 200));

        e.Graphics.DrawString("RECIEPT", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(100, 250));
        e.Graphics.DrawString("____________", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, 260));

        e.Graphics.DrawString("Description         Quantity       Price             Amount", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, 300));
        e.Graphics.DrawString("_____________________________________________", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, 310));
        int a = 310;

        for (int rows = 0; rows < dataGridView1.RowCount; rows++)
        {
            a = a + 30;

            string cellValue0 = dataGridView1.Rows[rows].Cells[0].Value != null ? dataGridView1.Rows[rows].Cells[0].Value.ToString() : "";
            string cellValue1 = dataGridView1.Rows[rows].Cells[1].Value != null ? dataGridView1.Rows[rows].Cells[1].Value.ToString() : "";
            string cellValue2 = dataGridView1.Rows[rows].Cells[2].Value != null ? dataGridView1.Rows[rows].Cells[2].Value.ToString() : "";
            string cellValue3 = dataGridView1.Rows[rows].Cells[3].Value != null ? dataGridView1.Rows[rows].Cells[3].Value.ToString() : "";

            e.Graphics.DrawString(cellValue0, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, a));
            e.Graphics.DrawString(cellValue1, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(250, a));
            e.Graphics.DrawString(cellValue2, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(350, a));
            e.Graphics.DrawString(cellValue3, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(500, a));
        }

        a = a + 20;
        e.Graphics.DrawString("_____________________________________________", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new Point(100, a));

        a = a + 40;
        e.Graphics.DrawString("Total Amount = ", new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, new Point(100, a));
        e.Graphics.DrawString(textBoxAmount.Text, new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, new Point(300, a));

        a = a + 60;
        e.Graphics.DrawString("Thank You!!!", new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(100, a));

                a = a + 80;
                e.Graphics.DrawString("THE BREAD IN LONG TIME CAN BE SOFT WHY DON'T PEOPLE", new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, new Point(100, a));
            }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred while printing: " + ex.Message, "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        
            private void btnPay_Click(object sender, EventArgs e)
            {
                try
                {
                    using (MySqlConnection connection = MainClass.GetConnection())
                    {
                        connection.Open();

                       
              
                           
                            string query = "INSERT INTO orders (TotalAmount, Date ) VALUES ( @amount, @orderDate)";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            //command.Parameters.AddWithValue("@productId", productId);
                            //command.Parameters.AddWithValue("@quantity", quantity);
                            //command.Parameters.AddWithValue("@price", price);
                            command.Parameters.AddWithValue("@amount", textBoxAmount.Text);
                            command.Parameters.AddWithValue("@orderDate", DateTime.Now);

                            command.ExecuteNonQuery();
               

                        MessageBox.Show("Order details inserted into the database successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 
                    string insertUser = "INSERT INTO customers (name ) VALUES ( @cus_name)";


                    MySqlCommand command2 = new MySqlCommand(insertUser, connection);
                    command.Parameters.AddWithValue("@cus_name", textBoxCustomer.Text);
                    ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while processing the payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        

     
        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }
        private void ClearForm()
        {
            comboBoxProducts.SelectedIndex = -1;
            textBoxPrice.Clear();
            textBoxQuantity.Clear();
            dataGridView1.Rows.Clear();
            textBoxAmount.Clear();
        }
    }

}