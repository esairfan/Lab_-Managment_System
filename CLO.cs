using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;

namespace DBproject
{
    public partial class CLO : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        int selectedCloId;
        public CLO()
        {
            InitializeComponent();
            DisplayClo();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void Stubtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student student = new Student();
            student.Show();
        }

        private void Rubricbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAssesment manage = new ManageAssesment();
            manage.Show();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Nametxt.Text == "" )
                {
                    MessageBox.Show("missing data!!");
                }

                string name = Nametxt.Text;
                SqlConnection connection = new SqlConnection(ConnectionString);

                connection.Open();
                string query = "INSERT INTO Clo (Name, DateCreated, DateUpdated) VALUES (@Name, GETDATE(), GETDATE())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Clo added successfully.");
                    Nametxt.Clear();
                    DisplayClo();
                }
                else
                {
                    MessageBox.Show("Failed to add Clo.");
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedCloId = Convert.ToInt32(row.Cells[0].Value);
                Nametxt.Text = row.Cells["Name"].Value.ToString();

            }
        }
        private void CloCellClick(object sender, DataGridViewCellEventArgs e)
        {
          

        }
        private void DisplayClo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Clo";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Clo");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["Clo"];
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        
        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCloId == 0)
                {
                    MessageBox.Show("No Clo selected.");
                    return;
                }

                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Missing data!!");
                    return;
                }

                string name = Nametxt.Text;
                SqlConnection connection = new SqlConnection(ConnectionString);
                
                    connection.Open();
                    string query = "UPDATE Clo SET Name = @Name, DateUpdated = GETDATE() WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Id", selectedCloId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Clo updated successfully.");
                        Nametxt.Clear();
                        DisplayClo();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Clo.");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void GetRecord()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * From Clo", connection);
            connection.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            connection.Close();
            dataGridView1.DataSource = dt;
        }
        private void ClearRecord()
        {
            Nametxt.Clear();
            Nametxt.Focus();
        }
        public int ID;
        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                string deleteQuery = "DELETE FROM Clo WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", ID);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record Deleted successfully.");
                            GetRecord();
                            ClearRecord(); 
                        }
                        else
                        {
                            MessageBox.Show("No record found with the given ID.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No record selected for deletion.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                if (selectedRow != null && !selectedRow.IsNewRow)
                {
                    string idValue = selectedRow.Cells[0].Value?.ToString();
                    if (int.TryParse(idValue, out int id))
                    {
                        ID = id;
                        Nametxt.Text = selectedRow.Cells[1].Value?.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID.");
                    }
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}