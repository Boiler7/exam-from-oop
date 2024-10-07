using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=test;User ID=sa;Password=1234567Sa";
        public Form2()
        {
            InitializeComponent();

            Product product1 = new Product { Name = "Milk", Price = 2.05m };
            Product product2 = new Product { Name = "Bread", Price = 1.5m };
            Product product3 = new Product { Name = "Ice cream", Price = 5.00m };
            Product product4 = new Product { Name = "Fusilli", Price = 1.75m };
            Product product5 = new Product { Name = "Spaghetti", Price = 1.75m };
            Product product6 = new Product { Name = "Water", Price = 1.00m };
            Product product7 = new Product { Name = "Chicken fillet", Price = 4.50m };
            Product product8 = new Product { Name = "Cheese", Price = 2.50m };
            Product product9 = new Product { Name = "Apple", Price = 0.75m };


            comboBox1.Items.Add(product1);
            comboBox1.Items.Add(product2);
            comboBox1.Items.Add(product3);
            comboBox1.Items.Add(product4);
            comboBox1.Items.Add(product5);
            comboBox1.Items.Add(product6);
            comboBox1.Items.Add(product7);
            comboBox1.Items.Add(product8);
            comboBox1.Items.Add(product9);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Add
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            listBox1.Items.Add("1x - " + selectedItem);
        }

        private void button2_Click(object sender, EventArgs e) // Clear listbox
        {

            listBox1.Items.Clear();

        }

        private void button3_Click(object sender, EventArgs e)  // Remove
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void button4_Click(object sender, EventArgs e) //Upload to database
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("You cannot upload items without a customer name.", "Error");
                    return;
                }
                // Get the customer name
                string customerName = textBox1.Text;

                // SQL query to insert a new row into the "Invoices" table
                string insertInvoiceQuery = "INSERT INTO Invoices (InvoiceName) VALUES (@InvoiceName); SELECT SCOPE_IDENTITY();";

                // SQL query to insert new rows into the "InvoiceItems" table
                string insertInvoiceItemQuery = "INSERT INTO InvoiceItems (InvoiceID, ItemName) VALUES (@InvoiceID, @ItemName)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert the customer's name into the "Invoices" table and retrieve the generated InvoiceID
                    using (SqlCommand command = new SqlCommand(insertInvoiceQuery, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@InvoiceName", customerName);

                        int invoiceID = Convert.ToInt32(command.ExecuteScalar());

                        // Insert the items into the "InvoiceItems" table
                        using (SqlCommand itemCommand = new SqlCommand(insertInvoiceItemQuery, connection))
                        {
                            // Add common parameters outside the loop
                            itemCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);

                            foreach (string product in listBox1.Items)
                            {
                                // Add product-specific parameter inside the loop
                                itemCommand.Parameters.Clear();
                                itemCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                                itemCommand.Parameters.AddWithValue("@ItemName", product);
                                itemCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Items uploaded successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while uploading items:\n" + ex.Message, "Error");
            }
            listBox1.Items.Clear();
        }
    }
}
