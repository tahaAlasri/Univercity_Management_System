using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class LoginHistoryForm : Form
    {
        private string myConn = Properties.Settings.Default.Univercity_CRUD;

        public LoginHistoryForm()
        {
            InitializeComponent();
        }

        private void LoginHistoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(myConn))
                {
                    conn.Open();
                    string query = "SELECT LogId, UserName, LoginTime, MachineName FROM LoginHistory ORDER BY LoginTime DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvLoginHistory.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message);
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            create_users cu = new create_users();
            cu.Show();
            this.Hide();
        }
    }
}
