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
using System.Windows.Forms.VisualStyles;

namespace DBproject
{
    public partial class StudentAttendence : Form
    {
        private string ConnectionString = @"Data Source=DESKTOP-KOU2P2U\SQLEXPRESS01;Initial Catalog=ProjectB;Integrated Security=True;";
        public StudentAttendence()
        {
            InitializeComponent();
            LoadAttendanceStatus();

        }
        
        private void LoadRegistrationNumbers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT RegistrationNumber FROM Student";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string registrationNumber = reader["RegistrationNumber"].ToString();
                            cmbRegNo.Items.Add(registrationNumber);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Registration Numbers: " + ex.Message);
            }
        }
        private void LoadAttendanceStatus()
        {
            string query = "SELECT [Name] FROM [dbo].[Lookup] WHERE [Category] = 'ATTENDANCE_STATUS';";

            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string attendanceStatus = reader["Name"].ToString();
                        cmbAttendanceStatus.Items.Add(attendanceStatus);
                    }
                    reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance status: " + ex.Message);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                string registrationNumber = cmbRegNo.SelectedItem.ToString();
                DateTime attendanceDate = dtpAttendanceDate.Value;
                int status = cmbAttendanceStatus.SelectedIndex + 1; // Assuming status index starts from 1

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Retrieve StudentId from Student table
                    int studentId;
                    string studentIdQuery = "SELECT Id FROM Student WHERE RegistrationNumber = @RegistrationNumber";
                    using (SqlCommand studentIdCommand = new SqlCommand(studentIdQuery, connection))
                    {
                        studentIdCommand.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                        object result = studentIdCommand.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            studentId = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("No student found with the selected registration number.");
                            return;
                        }
                    }

                    // Check if attendance for this student on this date already exists
                    string checkAttendanceQuery = "SELECT COUNT(*) FROM ClassAttendance AS CA " +
                                                  "INNER JOIN StudentAttendance AS SA ON CA.Id = SA.AttendanceId " +
                                                  "WHERE SA.StudentId = @StudentId AND CA.AttendanceDate = @AttendanceDate";
                    using (SqlCommand checkAttendanceCommand = new SqlCommand(checkAttendanceQuery, connection))
                    {
                        checkAttendanceCommand.Parameters.AddWithValue("@StudentId", studentId);
                        checkAttendanceCommand.Parameters.AddWithValue("@AttendanceDate", attendanceDate);

                        int existingAttendanceCount = Convert.ToInt32(checkAttendanceCommand.ExecuteScalar());
                        if (existingAttendanceCount > 0)
                        {
                            MessageBox.Show("Attendance for this student on this date already exists.");
                            return;
                        }
                    }
                    bool isActiveStudent;
                    string checkStudentStatusQuery = "SELECT Status FROM Student WHERE RegistrationNumber = @RegistrationNumber";
                    using (SqlCommand checkStudentStatusCommand = new SqlCommand(checkStudentStatusQuery, connection))
                    {
                        checkStudentStatusCommand.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                        object statusResult = checkStudentStatusCommand.ExecuteScalar();
                        isActiveStudent = (statusResult != null && statusResult != DBNull.Value && Convert.ToInt32(statusResult) == 5);
                    }
                    if (isActiveStudent)
                    {
                        // Insert into ClassAttendance table
                        string insertAttendanceQuery = "INSERT INTO ClassAttendance (AttendanceDate) VALUES (@AttendanceDate); SELECT SCOPE_IDENTITY();";
                        int attendanceId;
                        using (SqlCommand insertAttendanceCommand = new SqlCommand(insertAttendanceQuery, connection))
                        {
                            insertAttendanceCommand.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                            attendanceId = Convert.ToInt32(insertAttendanceCommand.ExecuteScalar());
                        }

                        // Insert into StudentAttendance table
                        string insertQuery = "INSERT INTO StudentAttendance (AttendanceId, StudentId, AttendanceStatus) " +
                                             "VALUES (@AttendanceId, @StudentId, @Status)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AttendanceId", attendanceId);
                            command.Parameters.AddWithValue("@StudentId", studentId);
                            command.Parameters.AddWithValue("@Status", status);

                            int rowsAffected = command.ExecuteNonQuery();
                            // Atendance Shehdules 
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Attendance saved successfully.");
                                // Clear controls or perform any other action upon successful save
                            }
                            else
                            {
                                MessageBox.Show("Error: Attendance not saved.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot mark attendance for inactive student.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving attendance: " + ex.Message);
            }
        }

        private void StudentAttendence_Load(object sender, EventArgs e)
        {
            LoadRegistrationNumbers();
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void MngAssesmentbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAssesment manageAssesment = new ManageAssesment();
            manageAssesment.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
