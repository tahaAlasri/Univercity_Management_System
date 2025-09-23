using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    internal class Student
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public int? ProgramID { get; set; }      // تعديل: بدل string خليتها int?
        public int? LectuereID { get; set; }     // تعديل: بدل string خليتها int?
        public byte[] Image { get; set; }

        private string connectionString = Properties.Settings.Default.Univercity_CRUD;

        public Student() { }

        public Student(string name, DateTime? dob, string email,
                     int? programID, int? lectuereID, byte[] image)
        {
            Name = name;
            DOB = dob;
            Email = email;
            ProgramID = programID;
            LectuereID = lectuereID;
            Image = image;
        }

        // إضافة طالب جديد
        public bool AddStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Student (name, dob, email, program_id, lectuere_id, image) 
                                     VALUES (@Name, @DOB, @Email, @ProgramID, @LectuereID, @Image)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@DOB", DOB.HasValue ? (object)DOB.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
                    cmd.Parameters.AddWithValue("@ProgramID", ProgramID.HasValue ? (object)ProgramID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LectuereID", LectuereID.HasValue ? (object)LectuereID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Image", Image ?? (object)DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding student: " + ex.Message);
                    return false;
                }
            }
        }

        // تحديث بيانات طالب
        public bool UpdateStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Student 
                                     SET name = @Name, dob = @DOB, email = @Email, 
                                         program_id = @ProgramID, lectuere_id = @LectuereID, image = @Image
                                     WHERE student_id = @StudentID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@DOB", DOB.HasValue ? (object)DOB.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
                    cmd.Parameters.AddWithValue("@ProgramID", ProgramID.HasValue ? (object)ProgramID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@LectuereID", LectuereID.HasValue ? (object)LectuereID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Image", Image ?? (object)DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating student: " + ex.Message);
                    return false;
                }
            }
        }

        // حذف طالب
        public bool DeleteStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Student WHERE student_id = @StudentID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message);
                    return false;
                }
            }
        }

        // جلب كل الطلاب
        public DataTable GetAllStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Student";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching students: " + ex.Message);
                }
                return dt;
            }
        }
    }
}
