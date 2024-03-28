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
using System.Xml.Linq;
using System.Collections;

namespace DBproject
{
    public partial class ManageAssesment : Form
    {
        int Id;
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        public ManageAssesment()
        {
            InitializeComponent();
            DisplayAssesment();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Titletxt.Text == "" || Markstxt.Text == "" || Weightagetxt.Text == "")
                {
                    MessageBox.Show("missing data!!");
                }
                else
                {

                    string title = Titletxt.Text;
                    int marks = int.Parse(Markstxt.Text);
                    int weightage = int.Parse(Weightagetxt.Text);
                    
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    connection.Open();
                    string Query = "insert into Assessment (Title , DateCreated , TotalMarks , TotalWeightage) " +
                       "Values (@title,GetDate(),@marks,@weightage)";
                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@title",title);
                    command.Parameters.AddWithValue("@marks", marks);
                    command.Parameters.AddWithValue("@weightage", weightage);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Assessment added successfully.");
                        Titletxt.Text = "";
                        Markstxt.Text = "";
                        Weightagetxt.Text = "";
                        DisplayAssesment();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add assessment.");
                    }



                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void Compbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssesmentComponent assesmentComponent = new AssesmentComponent();
            assesmentComponent.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Id = Convert.ToInt32(row.Cells["Id"].Value);
                Titletxt.Text = row.Cells["Title"].Value.ToString();
                Markstxt.Text = row.Cells["TotalMarks"].Value.ToString();
                Weightagetxt.Text = row.Cells["TotalWeightage"].Value.ToString();
            }
        }
        private void DisplayAssesment()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Assessment";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Assessment");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["Assessment"];
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

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id == 0)
                {
                    MessageBox.Show("No Assessment selected.");
                    return;
                }

                if (Titletxt.Text == "" || Markstxt.Text == "" || Weightagetxt.Text == "")
                {
                    MessageBox.Show("Missing data!!");
                    return;
                }

                string title = Titletxt.Text;
                int marks = int.Parse(Markstxt.Text);
                int weightage = int.Parse(Weightagetxt.Text);
                SqlConnection connection = new SqlConnection(ConnectionString);

                connection.Open();
                string query = "UPDATE Assessment SET Title = @title, TotalMarks = @marks, TotalWeightage = @weightage  WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@marks", marks);
                command.Parameters.AddWithValue("@weightage", weightage);
                command.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Assessment updated successfully.");
                    Titletxt.Text = "";
                    Markstxt.Text = "";
                    Weightagetxt.Text = "";
                    DisplayAssesment();
                }
                else
                {
                    MessageBox.Show("Failed to update Assessment.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id == 0)
                {
                    MessageBox.Show("No Assessment selected.");
                    return;
                }
                if (Titletxt.Text == "" || Markstxt.Text == "" || Weightagetxt.Text == "")
                {
                    MessageBox.Show("Missing data!!");
                    return;
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Assessment WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Id", Id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Assessment deleted successfully.");
                        Titletxt.Text = "";
                        Markstxt.Text = "";
                        Weightagetxt.Text = "";
                        DisplayAssesment();
                    }
                    else
                    {
                        MessageBox.Show("Assessment to delete Clo.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
