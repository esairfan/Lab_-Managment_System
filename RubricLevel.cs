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

namespace DBproject
{
    public partial class RubricLevel : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        public RubricLevel()
        {
            InitializeComponent();
            DisplayRubric();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            LoadRubricLevels();
        }
        private void LoadRubricLevels()
        {
            cmbRubricLevel.Items.Add("Excellent");
            cmbRubricLevel.Items.Add("Good");
            cmbRubricLevel.Items.Add("Satisfactory");
            cmbRubricLevel.Items.Add("Unsatisfied");
        }
        private void LoadRubricIds()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Id FROM Rubric";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbRubricId.Items.Add(reader["Id"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Rubric IDs: " + ex.Message);
                }
            }
        }
        int rubricId;
        int rubricLevelId;
        private void Addbtn_Click(object sender, EventArgs e)
        {
            
            
                int rubricCount = GetRubricCount(rubricId);

                if (rubricCount >= 4)
                {
                    MessageBox.Show("Rubric ID already has 4 or more entries in RubricLevel table. Cannot add more.");
                    return;
                }
                try
                {
                if (cmbRubricId.Text == "" || txtDetRubric.Text == "" || cmbRubricLevel.Text == "")
                {
                    MessageBox.Show("missing data!!");
                }
                else
                {
                    SqlConnection conn = new SqlConnection(ConnectionString);

                    conn.Open();
                    string query = "INSERT INTO RubricLevel (RubricId, Details, MeasurementLevel) VALUES (@RubricId, @Details, @MeasurementLevel)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RubricId", cmbRubricId.SelectedItem);
                    cmd.Parameters.AddWithValue("@Details", txtDetRubric.Text);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", GetRubricLevelNo(cmbRubricLevel.SelectedItem.ToString()));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Rubric level added successfully.");
                        DisplayRubric();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add rubric level.");
                    }

                }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Rubric Level: " + ex.Message);
                }
            
        }

        private void RubricLevel_Load(object sender, EventArgs e)
        {
            LoadRubricIds();
        }
        private int GetRubricLevelNo(string rubricLevel)
        {
            switch (rubricLevel)
            {
                case "Excellent":
                    return 1;
                case "Good":
                    return 2;
                case "Satisfactory":
                    return 3;
                case "Unsatisfied":
                    return 4;
                default:
                    return 0;
            }
        }
        private void DisplayRubric()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM RubricLevel";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "RubricLevel");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["RubricLevel"];
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                rubricId = Convert.ToInt32(row.Cells["RubricId"].Value);
                rubricLevelId = Convert.ToInt32(row.Cells["Id"].Value);
                txtDetRubric.Text = row.Cells["Details"].Value.ToString();

                cmbRubricId.SelectedItem = rubricId.ToString();
                cmbRubricLevel.SelectedItem = GetRubricLevelName(Convert.ToInt32(row.Cells["MeasurementLevel"].Value));
            }
        }
        private int GetRubricCount(int rubricId)
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM RubricLevel WHERE RubricId = @RubricId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RubricId", rubricId);
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting rubric count: " + ex.Message);
            }
            return count;
        }
        private int GetUpdatedRubricCount(int rubricId, int rubricLevelId)
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM RubricLevel WHERE RubricId = @RubricId AND Id != @RubricLevelId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RubricId", rubricId);
                    command.Parameters.AddWithValue("@RubricLevelId", rubricLevelId);
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting updated rubric count: " + ex.Message);
            }
            return count;
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);

                conn.Open();
                string query = "DELETE FROM RubricLevel WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", rubricId); // Assuming selectedRubricLevelId is a variable that holds the ID of the rubric level to delete.
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Rubric level deleted successfully.");
                    DisplayRubric(); // Refresh the grid view after deletion
                }
                else
                {
                    MessageBox.Show("Failed to delete rubric level.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Rubric Level: " + ex.Message);
            }
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbRubricId.Text == "" || txtDetRubric.Text == "" || cmbRubricLevel.Text == "")
                {
                    MessageBox.Show("Missing data!!");
                }
                else

                {
                    int currentCount = GetRubricCount(rubricId);
                    int updatedCount = GetUpdatedRubricCount(rubricId, rubricLevelId);

                    if (updatedCount > currentCount && updatedCount >= 4)
                    {
                        MessageBox.Show("Updating this rubric level will exceed the limit of 4 entries for the rubric ID. Update cannot be performed.");
                        return;
                    }
                    SqlConnection conn = new SqlConnection(ConnectionString);

                    conn.Open();
                    string query = "UPDATE RubricLevel SET Details = @Details, MeasurementLevel = @MeasurementLevel WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", rubricId);
                    cmd.Parameters.AddWithValue("@Details", txtDetRubric.Text);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", GetRubricLevelNo(cmbRubricLevel.SelectedItem.ToString()));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Rubric level updated successfully.");
                        DisplayRubric(); // Refresh the grid view after updating
                    }
                    else
                    {
                        MessageBox.Show("Failed to update rubric level.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Rubric Level: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssesmentComponent assesmentComponent = new AssesmentComponent();
            assesmentComponent.Show();
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentResult studentResult = new StudentResult();
            studentResult.Show();
        }
        private string GetRubricLevelName(int level)
        {
            switch (level)
            {
                case 1:
                    return "Excellent";
                case 2:
                    return "Good";
                case 3:
                    return "Satisfactory";
                case 4:
                    return "Unsatisfied";
                default:
                    return "";
            }
        }

        private void cmbRubricId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRubricLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
