using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univercity_Management_System
{
    internal class Lecturer
    {
        public string LecturerID { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string DepartmentID { get; set; }
        public string UserID { get; set; }

        // Database connection string
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;

        public Lecturer() { }

        public Lecturer(string lecturerID, string name, string rank, string departmentID, string userID)
        {
            LecturerID = lecturerID;
            Name = name;
            Rank = rank;
            DepartmentID = departmentID;
            UserID = userID;
        }

        // Add a new lecturer to the database
        public bool AddLecturer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Lecturer (LecturerID, Name, Rank, DepartmentID, UserID) 
                                     VALUES (@LecturerID, @Name, @Rank, @DepartmentID, @UserID)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LecturerID", LecturerID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Rank", Rank);
                    cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding lecturer: " + ex.Message);
                    return false;
                }
            }
        }

        // Update an existing lecturer
        public bool UpdateLecturer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Lecturer SET Name = @Name, Rank = @Rank, 
                                     DepartmentID = @DepartmentID, UserID = @UserID 
                                     WHERE LecturerID = @LecturerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LecturerID", LecturerID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Rank", Rank);
                    cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating lecturer: " + ex.Message);
                    return false;
                }
            }
        }

        // Delete a lecturer
        public bool DeleteLecturer()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Lecturer WHERE LecturerID = @LecturerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LecturerID", LecturerID);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting lecturer: " + ex.Message);
                    return false;
                }
            }
        }

        // Get all lecturers
        public System.Data.DataTable GetAllLecturers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Lecturer";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading lecturers: " + ex.Message);
                }
            }

            return dt;
        }

        // Search for a lecturer by ID
        public Lecturer GetLecturerByID(string lecturerID)
        {
            Lecturer lecturer = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Lecturer WHERE LecturerID = @LecturerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LecturerID", lecturerID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lecturer = new Lecturer(
                            reader["LecturerID"].ToString(),
                            reader["Name"].ToString(),
                            reader["Rank"].ToString(),
                            reader["DepartmentID"].ToString(),
                            reader["UserID"].ToString()
                        );
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error finding lecturer: " + ex.Message);
                }
            }

            return lecturer;
        }
    }
}