using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Univercity_Management_System
{

    public partial class StudentForm : Form
    {
        private Student student = new Student();
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;

        public StudentForm()
        {
            InitializeComponent();
            LoadStudents();
        }
        private void LoadStudents()
        {
            try
            {
                DataTable dt = student.GetAllStudents();
                dgv_Student_info.DataSource = dt;
                dgv_Student_info.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }
        private void but_back_Click(object sender, EventArgs e)
        {
            DashboardForm frm = new DashboardForm();
            frm.Show();
            this.Hide();
        }

        private void but_print_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing: " + ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Font font = new Font("Arial", 12);
                Brush brush = Brushes.Black;
                float yPos = 100;
                float leftMargin = e.MarginBounds.Left;
                float topMargin = e.MarginBounds.Top;

                e.Graphics.DrawString("Student Information", new Font("Arial", 16, FontStyle.Bold), brush, leftMargin, yPos);
                yPos += 40;

                e.Graphics.DrawString($"Student ID: {Tex_Student_id.Text}", font, brush, leftMargin, yPos);
                yPos += 30;
                e.Graphics.DrawString($"Name: {Tex_name.Text}", font, brush, leftMargin, yPos);
                yPos += 30;
                e.Graphics.DrawString($"DOB: {dtp_Student.Value.ToShortDateString()}", font, brush, leftMargin, yPos);
                yPos += 30;
                e.Graphics.DrawString($"Email: {Tex_email.Text}", font, brush, leftMargin, yPos);
                yPos += 30;
                e.Graphics.DrawString($"Program ID: {tex_programID.Text}", font, brush, leftMargin, yPos);
                yPos += 30;
                e.Graphics.DrawString($"Advisor ID: {tex_advisor_id.Text}", font, brush, leftMargin, yPos);
                yPos += 30;

                if (Pb_yourPic.Image != null)
                {
                    yPos += 40;
                    e.Graphics.DrawImage(Pb_yourPic.Image, leftMargin, yPos, 100, 100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating print document: " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Pb_yourPic.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void But_add_Click(object sender, EventArgs e)
        {
            try
            {
                student.Name = Tex_name.Text.Trim();
                student.DOB = dtp_Student.Value;
                student.Email = Tex_email.Text.Trim();

                // تحويل ProgramID و LectuereID إلى int
                student.ProgramID = string.IsNullOrEmpty(tex_programID.Text) ? (int?)null : int.Parse(tex_programID.Text);
                student.LectuereID = string.IsNullOrEmpty(tex_advisor_id.Text) ? (int?)null : int.Parse(tex_advisor_id.Text);

                student.Image = Pb_yourPic.Image != null ? ImageToByteArray(Pb_yourPic.Image) : null;

                if (student.AddStudent())
                {
                    MessageBox.Show("Student added successfully!");
                    ClearFields();
                    LoadStudents();
                }
                else
                {
                    MessageBox.Show("Failed to add student.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            try
    {
        if (string.IsNullOrEmpty(Tex_Student_id.Text))
        {
            MessageBox.Show("Please select a student to update.");
            return;
        }

        student.StudentID = Tex_Student_id.Text.Trim();
        student.Name = Tex_name.Text.Trim();
        student.DOB = dtp_Student.Value;
        student.Email = Tex_email.Text.Trim();

        student.ProgramID = string.IsNullOrEmpty(tex_programID.Text) ? (int?)null : int.Parse(tex_programID.Text);
        student.LectuereID = string.IsNullOrEmpty(tex_advisor_id.Text) ? (int?)null : int.Parse(tex_advisor_id.Text);

        student.Image = Pb_yourPic.Image != null ? ImageToByteArray(Pb_yourPic.Image) : null;

        if (student.UpdateStudent())
        {
            MessageBox.Show("Student updated successfully!");
            ClearFields();
            LoadStudents();
        }
        else
        {
            MessageBox.Show("Failed to update student.");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
        }

        private void But_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Tex_Student_id.Text))
                {
                    MessageBox.Show("Please select a student to delete.");
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    student.StudentID = Tex_Student_id.Text.Trim();

                    if (student.DeleteStudent())
                    {
                        MessageBox.Show("Student deleted successfully!");
                        ClearFields();
                        LoadStudents();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete student.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            Tex_Student_id.Clear();
            Tex_name.Clear();
            dtp_Student.Value = DateTime.Now;
            Tex_email.Clear();
            tex_programID.Clear();
            tex_advisor_id.Clear();
            Pb_yourPic.Image = null;
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void dgv_Lecturer_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Student_info.Rows[e.RowIndex];

                Tex_Student_id.Text = row.Cells["student_id"].Value.ToString();
                Tex_name.Text = row.Cells["name"].Value.ToString();

                if (row.Cells["dob"].Value != DBNull.Value)
                    dtp_Student.Value = Convert.ToDateTime(row.Cells["dob"].Value);

                Tex_email.Text = row.Cells["email"].Value.ToString();
                tex_programID.Text = row.Cells["program_id"].Value.ToString();
                tex_advisor_id.Text = row.Cells["lectuere_id"].Value.ToString();

                if (row.Cells["image"].Value != DBNull.Value)
                {
                    byte[] imageData = (byte[])row.Cells["image"].Value;
                    Pb_yourPic.Image = ByteArrayToImage(imageData);
                }
                else
                {
                    Pb_yourPic.Image = null;
                    {
                    }
                }
            }
        }

        private void But_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}