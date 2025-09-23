using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class LecturerForm : Form
    {
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;
        private Lecturer lecturer = new Lecturer();
        public LecturerForm()
        {
            InitializeComponent();
        }

        private void Lecturer_Load(object sender, EventArgs e)
        {
            LoadLecturerData();
        }

        private void LoadLecturerData()
        {
            dgv_Lecturer_info.DataSource = lecturer.GetAllLecturers();
            dgv_Lecturer_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void But_add_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                lecturer.Name = Tex_name.Text;
                lecturer.Rank = Tex_rank.Text;
                lecturer.DepartmentID = tex_Dep_id.Text;

                if (lecturer.AddLecturer())
                {
                    MessageBox.Show("Lecturer added successfully!");
                    ClearForm();
                    LoadLecturerData();
                }
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                lecturer.LecturerID = Tex_Lecturerid.Text;
                lecturer.Name = Tex_name.Text;
                lecturer.Rank = Tex_rank.Text;
                lecturer.DepartmentID = tex_Dep_id.Text;

                if (lecturer.UpdateLecturer())
                {
                    MessageBox.Show("Lecturer updated successfully!");
                    ClearForm();
                    LoadLecturerData();
                }
            }
        }

        private void ClearForm()
        {
            Tex_Lecturerid.Text = "";
            Tex_name.Text = "";
            Tex_rank.Text = "";
            tex_Dep_id.Text = "";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(Tex_name.Text))
            {
                MessageBox.Show("Please enter a Name.");
                return false;
            }

            if (string.IsNullOrEmpty(Tex_rank.Text))
            {
                MessageBox.Show("Please enter a Rank.");
                return false;
            }

            if (string.IsNullOrEmpty(tex_Dep_id.Text))
            {
                MessageBox.Show("Please enter a Department ID.");
                return false;
            }

            return true;
        }
        private void But_delete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Tex_Lecturerid.Text))
            {
                MessageBox.Show("Please enter a Lecturer ID to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this lecturer?", "Confirm Delete",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lecturer.LecturerID = Tex_Lecturerid.Text;

                if (lecturer.DeleteLecturer())
                {
                    MessageBox.Show("Lecturer deleted successfully!");
                    ClearForm();
                    LoadLecturerData();
                }
            }
        }

        private void But_clear_Click_1(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void but_back_Click_1(object sender, EventArgs e)
        {
            DashboardForm frm = new DashboardForm();
            frm.Show();
            this.Hide();
        }

        private void but_print_Click_1(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            // Create a bitmap of the data grid view
            Bitmap bm = new Bitmap(dgv_Lecturer_info.Width, dgv_Lecturer_info.Height);
            dgv_Lecturer_info.DrawToBitmap(bm, new Rectangle(0, 0, dgv_Lecturer_info.Width, dgv_Lecturer_info.Height));

            // Draw the bitmap on the page
            e.Graphics.DrawImage(bm, 50, 100);

            // Add header text
            e.Graphics.DrawString("Lecturer Report", new Font("Arial", 20, FontStyle.Bold),
                Brushes.Black, new Point(300, 50));
        }

        private void dgv_Lecturer_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Lecturer_info.Rows[e.RowIndex];

                Tex_Lecturerid.Text = row.Cells["LecturerID"].Value.ToString();
                Tex_name.Text = row.Cells["Name"].Value.ToString();
                Tex_rank.Text = row.Cells["Rank"].Value.ToString();
                tex_Dep_id.Text = row.Cells["DepartmentID"].Value.ToString();
            }
        }
    }
}
