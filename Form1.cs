using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace Exam
{
    public partial class Form1 : Form
    {
        public SqlConnection cnn;
        private string selectedInvoiceName;
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=test;User ID=sa;Password=1234567Sa";
        public Form1()
        {
            InitializeComponent();

            try
            {
                // SQL query to retrieve the list of invoices
                string query = "SELECT InvoiceID, InvoiceName FROM Invoices";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable invoiceTable = new DataTable();
                    adapter.Fill(invoiceTable);

                    dataGridView1.DataSource = invoiceTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading invoices:\n" + ex.Message, "Error");
            }
        }

        private void GeneratePDF(int invoiceID)
        {
            try
            {
                // SQL query to retrieve the items and customer name of the selected invoice
                string query = "SELECT ItemName, InvoiceName FROM InvoiceItems JOIN Invoices ON InvoiceItems.InvoiceID = Invoices.InvoiceID WHERE InvoiceItems.InvoiceID = @InvoiceID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", invoiceID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable itemsTable = new DataTable();
                        adapter.Fill(itemsTable);

                        // Retrieve the customer name
                        string customerName = itemsTable.Rows[0]["InvoiceName"].ToString();

                        // Create a new PDF document
                        iTextSharp.text.Document document = new iTextSharp.text.Document();
                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Invoice.pdf", FileMode.Create));
                        document.Open();

                        iTextSharp.text.Font invoiceFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD);
                        Paragraph header = new Paragraph("Invoice:\n", invoiceFont);
                        header.Alignment = Element.ALIGN_CENTER;
                        document.Add(header);

                        // Add the customer name to the PDF
                        Paragraph customerParagraph = new Paragraph($"Customer: {customerName}\n\n");
                        document.Add(customerParagraph);

                        // Create a table to display the items
                        PdfPTable table = new PdfPTable(1);
                        table.WidthPercentage = 100;

                        foreach (DataRow row in itemsTable.Rows)
                        {
                            string itemName = row["ItemName"].ToString();
                            PdfPCell cell = new PdfPCell(new Phrase(itemName));
                            table.AddCell(cell);
                        }

                        document.Add(table);

                        document.Close();

                        MessageBox.Show("PDF generated successfully!", "Success");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating the PDF:\n Try to close pdf file if it is open.\n" + ex.Message, "Error");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)  //Convert to pdf
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["InvoiceID"].Value);
                GeneratePDF(selectedInvoiceID);
            }
            else
            {
                MessageBox.Show("Please select an invoice from the list.", "Information");
            }

        }

        private int GetCurrentInvoiceID()
        {
            int invoiceID = 0;

            // SQL query to retrieve the latest inserted invoice ID
            string query = "SELECT MAX(InvoiceID) FROM Invoices";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        invoiceID = Convert.ToInt32(result);
                    }
                }
            }
            return invoiceID;
        }

        private void button3_Click_1(object sender, EventArgs e) //Invoice selection
        {
            try
            {
                // Check if a row is selected in the dataGridView1
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected invoice's ID from the dataGridView1
                    int selectedInvoiceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["InvoiceID"].Value);

                    // SQL query to retrieve the items of the selected invoice
                    string query = "SELECT ItemName FROM InvoiceItems WHERE InvoiceID = @InvoiceID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter to the query to prevent SQL injection
                            command.Parameters.AddWithValue("@InvoiceID", selectedInvoiceID);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable itemsTable = new DataTable();
                            adapter.Fill(itemsTable);

                            dataGridView2.DataSource = itemsTable;
                        }
                    }

                    MessageBox.Show("Invoice items loaded successfully!", "Success");
                }
                else
                {
                    MessageBox.Show("Please select an invoice from the list.", "Information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading invoice items:\n" + ex.Message, "Error");
            }
        }
        private void button4_Click_2(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL query to retrieve the list of invoices
                string query = "SELECT InvoiceID, InvoiceName FROM Invoices";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable invoiceTable = new DataTable();
                    adapter.Fill(invoiceTable);

                    dataGridView1.DataSource = invoiceTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing invoices:\n" + ex.Message, "Error");
            }

            try
            {

                // SQL query to retrieve the list of invoices
                string query = "SELECT InvoiceID, ItemName FROM InvoiceItems";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable invoiceTable = new DataTable();
                    adapter.Fill(invoiceTable);

                    dataGridView2.DataSource = invoiceTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing invoices:\n" + ex.Message, "Error");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}