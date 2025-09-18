using System;
using System.Data;
using System.Data.SqlClient;

namespace Univercity_Management_System
{
    internal class Semesters
    {
        static string myConn = Properties.Settings.Default.Univercity_CRUD;

        public int Semester_id { get; set; }     // Primary Key (Auto Increment)
        public string Semester_name { get; set; }
        public int Year_id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool Is_active { get; set; }

        private const string selectQuery = "SELECT s.Semester_id, s.Semester_name, s.Year_id, y.Year_label, " +
                                          "s.Start_date, s.End_date, s.Is_active " +
                                          "FROM Semester s INNER JOIN Year y ON s.Year_id = y.Year_id";
        private const string insertQuery = "INSERT INTO Semester(Semester_name, Year_id, Start_date, End_date, Is_active) " +
                                          "VALUES(@Semester_name, @Year_id, @Start_date, @End_date, @Is_active)";
        private const string updateQuery = "UPDATE Semester SET Semester_name=@Semester_name, Year_id=@Year_id, " +
                                          "Start_date=@Start_date, End_date=@End_date, Is_active=@Is_active " +
                                          "WHERE Semester_id=@Semester_id";
        private const string deleteQuery = "DELETE FROM Semester WHERE Semester_id=@Semester_id";

        public static DataTable GetSemesters()
        {
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(selectQuery, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    adapter.Fill(datatable);
                }
            }
            return datatable;
        }

        public bool InsertSemester(Semesters semester)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(insertQuery, con))
                {
                    com.Parameters.AddWithValue("@Semester_name", semester.Semester_name);
                    com.Parameters.AddWithValue("@Year_id", semester.Year_id);
                    com.Parameters.AddWithValue("@Start_date", semester.Start_date);
                    com.Parameters.AddWithValue("@End_date", semester.End_date);
                    com.Parameters.AddWithValue("@Is_active", semester.Is_active);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        public bool UpdateSemester(Semesters semester)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(updateQuery, con))
                {
                    com.Parameters.AddWithValue("@Semester_id", semester.Semester_id);
                    com.Parameters.AddWithValue("@Semester_name", semester.Semester_name);
                    com.Parameters.AddWithValue("@Year_id", semester.Year_id);
                    com.Parameters.AddWithValue("@Start_date", semester.Start_date);
                    com.Parameters.AddWithValue("@End_date", semester.End_date);
                    com.Parameters.AddWithValue("@Is_active", semester.Is_active);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        public bool DeleteSemester(int semesterId)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(deleteQuery, con))
                {
                    com.Parameters.AddWithValue("@Semester_id", semesterId);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }
        public static DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
    }
}