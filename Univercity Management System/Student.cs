using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    internal class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public int? ProgramID { get; set; }
        public int? LecturerID { get; set; }
        public byte[] Image { get; set; }

        private string connectionString = Properties.Settings.Default.Univercity_CRUD;

        public bool AddStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Student (name, dob, email, program_id, lecturer_id, image) 
                                     VALUES (@Name, @DOB, @Email, @ProgramID, @LecturerID, @Image)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = DOB ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@ProgramID", SqlDbType.Int).Value = ProgramID ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@LecturerID", SqlDbType.Int).Value = LecturerID ?? (object)DBNull.Value;

                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = Image ?? (object)DBNull.Value;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding student: " + ex.Message);
                    return false;
                }
            }
        }

        public bool UpdateStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Student SET name=@Name, dob=@DOB, email=@Email, 
                                    program_id=@ProgramID, lecturer_id=@LecturerID, image=@Image 
                                    WHERE student_id=@StudentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = DOB ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@ProgramID", SqlDbType.Int).Value = ProgramID ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@LecturerID", SqlDbType.Int).Value = LecturerID ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = Image ?? (object)DBNull.Value;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating student: " + ex.Message);
                    return false;
                }
            }
        }

 public bool DeleteStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Student WHERE student_id = @StudentID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message);
                    return false;
                }
            }
        }

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
        // التحقق من وجود Lecturer
        public bool LecturerExists(int lecturerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Lecturer WHERE lecturer_id = @LecturerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@LecturerID", SqlDbType.Int).Value = lecturerId;
                    return (int)cmd.ExecuteScalar() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        // التحقق من وجود Program
        public bool ProgramExists(int programId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Program WHERE program_id = @ProgramID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@ProgramID", SqlDbType.Int).Value = programId;
                    return (int)cmd.ExecuteScalar() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
