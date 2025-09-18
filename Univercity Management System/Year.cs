using System;
using System.Data;
using System.Data.SqlClient;

namespace Univercity_Management_System
{
    internal class Year
    {
        static string myConn = Properties.Settings.Default.Univercity_CRUD;

        public int Year_id { get; set; }     // Primary Key (Auto Increment)
        public string Year_label { get; set; }

        private const string selectQuery = "SELECT Year_id, Year_label FROM Year";
        private const string insertQuery = "INSERT INTO Year(Year_label) VALUES(@Year_label)";
        private const string updateQuery = "UPDATE Year SET Year_label=@Year_label WHERE Year_id=@Year_id";
        private const string deleteQuery = "DELETE FROM Year WHERE Year_id=@Year_id";

        // قراءة كل السنوات
        public static DataTable GetYears()
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

        // إضافة سنة
        public bool InsertYear(Year year)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(insertQuery, con))
                {
                    com.Parameters.AddWithValue("@Year_label", year.Year_label);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        // تعديل سنة
        public bool UpdateYear(Year year)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(updateQuery, con))
                {
                    com.Parameters.AddWithValue("@Year_id", year.Year_id);
                    com.Parameters.AddWithValue("@Year_label", year.Year_label);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        // حذف سنة
        public bool DeleteYear(int yearId)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(deleteQuery, con))
                {
                    com.Parameters.AddWithValue("@Year_id", yearId);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }
    }
}
