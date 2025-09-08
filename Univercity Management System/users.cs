using System;
using System.Data;
using System.Data.SqlClient;

namespace Univercity_Management_System
{
    internal class Users
    {
        static string myConn = Properties.Settings.Default.Univercity_CRUD;

        public int UserId { get; set; }  // INT لأن user_id تلقائي
        public string UserName { get; set; }
        public string Password_hash { get; set; }
        public string Role { get; set; }

        private const string selectQuery = "SELECT user_id AS UserId, username AS UserName, password_hash AS Password_hash, role AS Role FROM Users";
        private const string insertQuery = "INSERT INTO Users(username, password_hash, role) VALUES(@UserName, @Password_hash, @Role)";
        private const string updateQuery = "UPDATE Users SET username=@UserName, password_hash=@Password_hash, role=@Role WHERE user_id=@UserId";
        private const string deleteQuery = "DELETE FROM Users WHERE user_id=@UserId";

        // قراءة كل المستخدمين
        public static DataTable GetUsers()
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

        // إضافة مستخدم
        public bool InsertUser(Users user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(insertQuery, con))
                {
                    com.Parameters.AddWithValue("@UserName", user.UserName);
                    com.Parameters.AddWithValue("@Password_hash", user.Password_hash);
                    com.Parameters.AddWithValue("@Role", user.Role);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        // تعديل مستخدم
        public bool UpdateUser(Users user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(updateQuery, con))
                {
                    com.Parameters.AddWithValue("@UserId", user.UserId);
                    com.Parameters.AddWithValue("@UserName", user.UserName);
                    com.Parameters.AddWithValue("@Password_hash", user.Password_hash);
                    com.Parameters.AddWithValue("@Role", user.Role);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }

        // حذف مستخدم
        public bool DeleteUser(int userId)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(deleteQuery, con))
                {
                    com.Parameters.AddWithValue("@UserId", userId);
                    rows = com.ExecuteNonQuery();
                }
            }
            return rows > 0;
        }
    }
}
