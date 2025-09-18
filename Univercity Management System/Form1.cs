using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class Form1 : Form
    {

        private string myConn = Properties.Settings.Default.Univercity_CRUD;

        public Form1()
        {
            InitializeComponent();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(myConn))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE username=@username AND password_hash=@password_hash";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password_hash", password);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SaveLoginHistory(username);
                            DashboardForm dashboard = new DashboardForm();
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void SaveLoginHistory(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(myConn))
                {
                    conn.Open();
                    string query = "INSERT INTO LoginHistory (UserName, LoginTime, MachineName) VALUES (@UserName, @LoginTime, @MachineName)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@MachineName", Environment.MachineName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء حفظ السجل: " + ex.Message);
            }
        }

    }
}
