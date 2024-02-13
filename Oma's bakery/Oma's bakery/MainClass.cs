using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oma_s_bakery
{
    public class MainClass
    {
        public static string connectionString = "server=localhost;userid=root;password=123456;database=omabakerydb";
        public static  MySqlConnection connection = new MySqlConnection(connectionString);
       



        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand(qry, connection);
                cmd.CommandType = CommandType.Text;

            }
            catch { }
        }
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error executing command: " + ex.Message);
                }
            }
        }



        public static  DataTable ExecuteSelectQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                }
            }

            return dataTable;
        }
        public static object ExecuteScalar(string query)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
        }

        // GetCategories QUERY
        public static DataTable GetCategories()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM categories";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }
        public static DataTable GetProducts()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM products";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

        public static string GetCategoryName(int categoryId)
        {
            string categoryName = string.Empty;
            string query = $"SELECT Name FROM categories WHERE category_id = {categoryId}";

            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoryName = reader["Name"].ToString();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                }
            }

            return categoryName;
        }
        public static int GetCategoryIdByName(string categoryName)
        {
            string query = $"SELECT category_id FROM categories WHERE Name = '{categoryName}'";
            object result = ExecuteScalar(query);
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                throw new ArgumentException($"Category '{categoryName}' not found.");
            }
        }

        public static void InsertProduct(string productName, decimal productPrice, int categoryId)
        {
            string query = $"INSERT INTO products (Name, Price, category_id) VALUES ('{productName}', '{productPrice}', '{categoryId}')";
            ExecuteNonQuery(query);
        }

        public static void UpdateProduct(int productId, string newName, decimal newPrice, int categoryId)
        {
            string query = $"UPDATE products SET Name = @newName, Price = @newPrice, category_id = @categoryId WHERE productid = @productId";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("@newName", newName),
            new MySqlParameter("@newPrice", newPrice),
            new MySqlParameter("@categoryId", categoryId),
            new MySqlParameter("@productId", productId)
        };

            ExecuteNonQuery(query, parameters.ToArray());
        }
        public static DataTable GetEmployees()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM employees";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public static void AddEmployee(string name, string position, string address, string phoneNumber)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO employees (Name, Position, Address, PhoneNumber) VALUES (@Name, @Position, @Address, @PhoneNumber)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Position", position);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateEmployee(int id, string name, string position, string address, string phoneNumber)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE employees SET Name = @Name, Position = @Position, Address = @Address, PhoneNumber = @PhoneNumber WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Position", position);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteEmployee(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM employees WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
        public static DataTable SearchEmployees(string keyword)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM employees WHERE Name LIKE @keyword OR Position LIKE @keyword OR Address LIKE @keyword OR PhoneNumber LIKE @keyword";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return dataTable;
        }

    }
}



