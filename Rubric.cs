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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBproject
{
    public partial class Rubric : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        public Rubric()
        {
            InitializeComponent();
            LoadCloId();
            DisplayRubric();
        }
        private void LoadCloId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT Id FROM Clo";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string id = reader["Id"].ToString();
                            cmbCLOid.Items.Add(id);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Clo Id: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (detailtxt.Text == "")
            {
                MessageBox.Show("Rubric details cannot be null");
                flag = true;

            }
            if (cmbCLOid.Text == "")
            {
                MessageBox.Show("CLOID cannot be null");
                flag = true;

            }
            if (Idtxt.Text == "")
            {
                MessageBox.Show("Rubric ID cannot be null");
                flag = true;

            }

            bool isExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query2 = "SELECT CloId, COUNT(CloId) FROM Rubric GROUP BY CloId HAVING COUNT(CloId) >= 4";
            SqlCommand cmd1 = new SqlCommand(query2, connection);
            connection.Open();
            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(cmbCLOid.Text);
                if (id == Convert.ToInt32(cmbCLOid.Text))
                {
                    isExist = true;
                    MessageBox.Show("More than 4 rubrics can't be added against 1 Clo");
                    break;
                }
            }
            connection.Close();
            bool isExist1 = false;
            string query3 = "SELECT Id FROM Rubric";
            SqlCommand cmd2 = new SqlCommand(query3, connection);
            connection.Open();
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {
                int id = reader1.GetInt32(0);
                if (id == Convert.ToInt32(Idtxt.Text))
                {
                    isExist1 = true;
                    MessageBox.Show("Rubric Id should be Unique;");
                    break;
                }
            }
            connection.Close();
            if (isExist == false && isExist1 == false && flag == false)
            {
                string qeury = "insert into dbo.Rubric(Id, Details,CloId) values('" + this.Idtxt.Text + "','" + detailtxt.Text + "','" + this.cmbCLOid.Text + "')";
                SqlConnection conDataBase = new SqlConnection(ConnectionString);
                SqlCommand cmdDataBase = new SqlCommand(qeury, conDataBase);
                SqlDataReader myreader;
                conDataBase.Open();
                myreader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Rubric has been Saved");
                Idtxt.Text = "";
                detailtxt.Text = "";
                cmbCLOid.Text = "";
                DisplayRubric();
                while (myreader.Read())
                {

                }
                using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("Select * from dbo.Rubric", sqlcon);
                    DataTable dtbl = new DataTable();
                    sqlDA.Fill(dtbl);

                    
                }
               
            }
        


        
    }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                int ID;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ID = Convert.ToInt32(row.Cells["Id"].Value);
                detailtxt.Text = row.Cells["Details"].Value.ToString();
                int Cloid = Convert.ToInt32(row.Cells["CloId"].Value);
            }
        }
        private void DisplayRubric()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Rubric";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Rubric");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["Rubric"];
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

        private void RubricLevelbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            RubricLevel rubricLevel = new RubricLevel();
            rubricLevel.Show();
        }

        private void cmbCLOid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
