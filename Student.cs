using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DBproject
{
    public partial class Student : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        private ErrorProvider errorProvider;
        public Student()
        {
            InitializeComponent();
            DisplayStudents();
            LoadStudentStatus();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            errorProvider = new ErrorProvider();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; 
            RegNo.TextChanged += RegNo_TextChanged;
            Contact.TextChanged += Contact_TextChanged;
            Email.TextChanged += Email_TextChanged;
            FirstName.TextChanged += FirstName_TextChanged;
            LastName.TextChanged += LastName_TextChanged;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LoadStudentStatus()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT [LookupId], [Name] FROM [dbo].[Lookup] WHERE [Category] = 'STUDENT_STATUS'";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int lookupId = Convert.ToInt32(reader["LookupId"]);
                        string studentStatus = reader["Name"].ToString();

                        // If the status corresponds to "Active" or "Inactive", set the ComboBox text accordingly
                        if (lookupId == 5 || lookupId == 6)
                        {
                            cmbStudentStatus.Text = studentStatus;
                        }

                        // Add all statuses to the ComboBox regardless
                        cmbStudentStatus.Items.Add(studentStatus);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading student statuses: " + ex.Message);
            }
        }
        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void clobtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CLO clo = new CLO();
            clo.Show();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstName.Text == "" || LastName.Text == "" || Contact.Text == "" || Email.Text == "" || RegNo.Text == "" || cmbStudentStatus.Text == "")
                {
                    MessageBox.Show("missing data!!");
                }
                else
                {
                    
                    string firstName = FirstName.Text;
                    string lastName = LastName.Text;
                    string contact = Contact.Text;
                    string registrationNumber = RegNo.Text;
                    string email = Email.Text;
                    int Status = (cmbStudentStatus.Text == "Active") ? 5 : 6;
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    connection.Open();
                    string Query = "insert into Student (FirstName , LastName , Contact , RegistrationNumber , Email , Status) " +
                       "Values ('{0}' ,'{1}' , '{2}' , '{3}' ,'{4}', '{5}' )";
                    Query = string.Format(Query, firstName, lastName, contact, registrationNumber, email, Status);
                    SqlCommand command = new SqlCommand(Query, connection);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student added successfully.");
                        FirstName.Text = "";
                        LastName.Text = "";
                        Contact.Text = "";
                        Email.Text = "";
                        RegNo.Text = "";
                        cmbStudentStatus.Text = "";
                        DisplayStudents();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add student.");
                    }

                    

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int studentID;
        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstName.Text == "" || LastName.Text == "" || Contact.Text == "" || Email.Text == "" || RegNo.Text == "" || cmbStudentStatus.Text == "")
                {
                    MessageBox.Show("missing data!!");
                }

                else
                {
                    string firstName = FirstName.Text;
                    string lastName = LastName.Text;
                    string contact = Contact.Text;
                    string registrationNumber = RegNo.Text;
                    string email = Email.Text;
                    int Status = (cmbStudentStatus.Text == "Active") ? 5 : 6;
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    connection.Open();

                    string query = "UPDATE Student SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, " +
                            "RegistrationNumber = @RegistrationNumber, Email = @Email, Status = @Status WHERE Id = @StudentId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@StudentId", studentID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student Updated!!!");
                        FirstName.Text = "";
                        LastName.Text = "";
                        Contact.Text = "";
                        Email.Text = "";
                        RegNo.Text = "";
                        cmbStudentStatus.Text = "";
                        DisplayStudents();
                    }
                    else
                    {
                        MessageBox.Show("Student Update Failed");
                    }
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
                studentID = Convert.ToInt32(row.Cells["Id"].Value);
                FirstName.Text = row.Cells["FirstName"].Value.ToString();
                LastName.Text = row.Cells["LastName"].Value.ToString();
                RegNo.Text = row.Cells["RegistrationNumber"].Value.ToString();
                Email.Text = row.Cells["Email"].Value.ToString();
                Contact.Text = row.Cells["Contact"].Value.ToString();
                int statusValue;
                if (int.TryParse(row.Cells["Status"].Value.ToString(), out statusValue))
                {
                    // Set the ComboBox text based on the status value
                    if (statusValue == 5)
                    {
                        cmbStudentStatus.Text = "Active";
                    }
                    else if (statusValue == 6)
                    {
                        cmbStudentStatus.Text = "Inactive";
                    }
                    else
                    {
                        // If the status is neither 5 nor 6, set ComboBox text to empty
                        cmbStudentStatus.Text = "";
                    }
                }
                else
                {
                    // If parsing fails, set ComboBox text to empty
                    cmbStudentStatus.Text = "";
                }

            }
        }

        private void DisplayStudents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Student";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Student");

                    if (dataSet.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables["Student"];
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

        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentID == 0)
                {
                    MessageBox.Show("No student selected.");
                    return;
                }

                SqlConnection connection = new SqlConnection(ConnectionString);

                connection.Open();
                string query = "DELETE FROM Student WHERE id = @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentID);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student deleted successfully.");
                    DisplayStudents();
                    FirstName.Text = "";
                    LastName.Text = "";
                    Contact.Text = "";
                    Email.Text = "";
                    RegNo.Text = "";
                    cmbStudentStatus.Text = "";
                }
                else
                {
                    MessageBox.Show("Failed to delete student.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentAttendence studentAttendence = new StudentAttendence();
            studentAttendence.Show();
        }
        private void RegNo_TextChanged(object sender, EventArgs e)
        {
            // Validate registration number format
            string regNumber = RegNo.Text;
            if (!IsValidRegistrationNumber(regNumber))
            {
                errorProvider.SetError(RegNo, "Invalid registration number format. Example: 2023-CS-104");
            }
            else
            {
                errorProvider.SetError(RegNo, ""); // Clear error if format is valid
            }
        }
        private bool IsValidRegistrationNumber(string rollNumber)
        {
            // Regular expression for the format "YYYY-XX-NN" where YYYY is the year, XX is the department code, and NN is the roll number
            string pattern = @"^\d{4}-[A-Za-z]{2}-\d{1,3}$";
            return Regex.IsMatch(rollNumber, pattern);
        }
        private void Contact_TextChanged(object sender, EventArgs e)
        {
            // Validate contact number format
            string contactNumber = Contact.Text;
            if (!IsValidContactNumber(contactNumber))
            {
                errorProvider.SetError(Contact, "Invalid contact number format. Example: +92 321 7469854 or 92321746985 or 03247589065");
            }
            else
            {
                errorProvider.SetError(Contact, ""); // Clear error if format is valid
            }
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            // Validate email format
            string email = Email.Text;
            if (!IsValidEmail(email))
            {
                errorProvider.SetError(Email, "Invalid email address format. Example: example@example.com");
            }
            else
            {
                errorProvider.SetError(Email, ""); // Clear error if format is valid
            }
        }

        private bool IsValidContactNumber(string contactNumber)
        {
            string pattern = @"^(?:(\+92\s?)?|0)?\d{3}\s?\d{3}\s?\d{4}$";
            return Regex.IsMatch(contactNumber, pattern);
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for a basic email validation
            string pattern = @"^\S+@\S+\.\S+$";
            return Regex.IsMatch(email, pattern);
        }
        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            // Validate name format
            string name = FirstName.Text;
            if (!IsCapitalized(name))
            {
                errorProvider.SetError(FirstName, "Name should start with a capital letter and contain only alphabetic characters.");
            }
            else
            {
                errorProvider.SetError(FirstName, ""); // Clear error if format is valid
            }
        }

        private bool IsCapitalized(string name)
        {
            string pattern = @"^[A-Z][a-z]*$";
            return Regex.IsMatch(name, pattern);
        }
        private void LastName_TextChanged(object sender, EventArgs e)
        {
           
            string name = LastName.Text;
            if (!IsCapitalized(name))
            {
                errorProvider.SetError(LastName, "Name should start with a capital letter and contain only alphabetic characters.");
            }
            else
            {
                errorProvider.SetError(LastName, ""); // Clear error if format is valid
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FirstName_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void RegNo_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
