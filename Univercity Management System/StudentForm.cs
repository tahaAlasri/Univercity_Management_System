using Guna.UI2.WinForms;
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
        private void LoadLecturers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT lecturer_id, name FROM Lecturer";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cb_lecturer_id.Items.Clear();
                    while (reader.Read())
                    {
                        // عرض الـ ID والاسم معاً ولكن تخزين الـ ID فقط كـ Tag
                        string displayText = $"{reader["lecturer_id"]} - {reader["name"]}";
                        cb_lecturer_id.Items.Add(displayText);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lecturers: " + ex.Message);
            }
        }
        public StudentForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadLecturers();
        }
        private void LoadStudents()
        {
            try
            {
                Back.Visible = false;
                guna2Panel1.Visible = false;
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
                e.Graphics.DrawString($"Advisor ID: {cb_lecturer_id.Text}", font, brush, leftMargin, yPos);
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
                // التحقق من الحقول المطلوبة
                if (string.IsNullOrEmpty(Tex_name.Text.Trim()))
                {
                    MessageBox.Show("Please enter student name.");
                    return;
                }

                student.Name = Tex_name.Text.Trim();
                student.DOB = dtp_Student.Value;
                student.Email = Tex_email.Text.Trim();

                // التحقق من ProgramID
                if (string.IsNullOrEmpty(tex_programID.Text))
                {
                    MessageBox.Show("Please enter Program ID.");
                    return;
                }

                int programId = int.Parse(tex_programID.Text);
                student.ProgramID = programId;

                // التحقق من LecturerID واستخراج الـ ID من النص
                if (string.IsNullOrEmpty(cb_lecturer_id.Text))
                {
                    MessageBox.Show("Please select an Advisor.");
                    return;
                }

                string lecturerText = cb_lecturer_id.Text;
                string lecturerIdStr = lecturerText.Split('-')[0].Trim(); // أخذ الجزء قبل -
                int lecturerId = int.Parse(lecturerIdStr);

                student.LecturerID = lecturerId;
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
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Program ID and Advisor ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message);
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

                student.StudentID = int.Parse(Tex_Student_id.Text.Trim());
                student.Name = Tex_name.Text.Trim();
                student.DOB = dtp_Student.Value;
                student.Email = Tex_email.Text.Trim();

                // التحقق من ProgramID
                if (string.IsNullOrEmpty(tex_programID.Text))
                {
                    MessageBox.Show("Please enter Program ID.");
                    return;
                }
                student.ProgramID = int.Parse(tex_programID.Text);

                // التحقق من LecturerID واستخراج الـ ID
                if (string.IsNullOrEmpty(cb_lecturer_id.Text))
                {
                    MessageBox.Show("Please select an Advisor.");
                    return;
                }

                string lecturerText = cb_lecturer_id.Text;
                string lecturerIdStr = lecturerText.Split('-')[0].Trim();
                student.LecturerID = int.Parse(lecturerIdStr);

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
                MessageBox.Show("Error updating student: " + ex.Message);
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
                    student.StudentID = int.Parse(Tex_Student_id.Text.Trim());

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
            Pb_yourPic.Image = null;
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;

            try
            {
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    Image originalImage = Image.FromStream(ms);

                    // الحفاظ على نسبة الطول والعرض
                    double ratioX = (double)Pb_yourPic.Width / originalImage.Width;
                    double ratioY = (double)Pb_yourPic.Height / originalImage.Height;
                    double ratio = Math.Min(ratioX, ratioY);

                    int newWidth = (int)(originalImage.Width * ratio);
                    int newHeight = (int)(originalImage.Height * ratio);

                    Bitmap resizedImage = new Bitmap(newWidth, newHeight);

                    using (Graphics graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }

                    originalImage.Dispose();
                    return resizedImage;
                }
            }
            catch
            {
                return null;
            }
        }
        private void dgv_Lecturer_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Student_info.Rows[e.RowIndex];

                Tex_Student_id.Text = row.Cells["student_id"].Value?.ToString() ?? "";
                Tex_name.Text = row.Cells["name"].Value?.ToString() ?? "";

                if (row.Cells["dob"].Value != null && row.Cells["dob"].Value != DBNull.Value)
                    dtp_Student.Value = Convert.ToDateTime(row.Cells["dob"].Value);

                Tex_email.Text = row.Cells["email"].Value?.ToString() ?? "";
                tex_programID.Text = row.Cells["program_id"].Value?.ToString() ?? "";

                // البحث عن النص المناسب في الـ ComboBox بناءً على الـ ID
                string lecturerId = row.Cells["lectuere_id"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(lecturerId))
                {
                    foreach (string item in cb_lecturer_id.Items)
                    {
                        if (item.StartsWith(lecturerId + " - "))
                        {
                            cb_lecturer_id.Text = item;
                            break;
                        }
                    }
                }

                if (row.Cells["image"].Value != null && row.Cells["image"].Value != DBNull.Value)
                {
                    byte[] imageData = (byte[])row.Cells["image"].Value;
                    Pb_yourPic.Image = ByteArrayToImage(imageData);
                }
                else
                {
                    Pb_yourPic.Image = null;
                }
            }
        }

        private void But_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void dgv_Student_info_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var index = e.RowIndex;
                DataGridViewRow row = dgv_Student_info.Rows[index];

                Tex_Student_id.Text = row.Cells[0].Value?.ToString() ?? "";
                Tex_name.Text = row.Cells[1].Value?.ToString() ?? "";

                if (row.Cells[2].Value != null && row.Cells[2].Value != DBNull.Value)
                    dtp_Student.Value = Convert.ToDateTime(row.Cells[2].Value);

                Tex_email.Text = row.Cells[3].Value?.ToString() ?? "";
                tex_programID.Text = row.Cells[4].Value?.ToString() ?? "";

                // للـ ComboBox
                string lecturerId = row.Cells[5].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(lecturerId))
                {
                    foreach (string item in cb_lecturer_id.Items)
                    {
                        if (item.StartsWith(lecturerId + " - "))
                        {
                            cb_lecturer_id.Text = item;
                            break;
                        }
                    }
                }

                // تحميل الصورة بحجم الـ PictureBox
                if (row.Cells[6].Value != null && row.Cells[6].Value != DBNull.Value)
                {
                    byte[] imageData = (byte[])row.Cells[6].Value;
                    Pb_yourPic.Image = ByteArrayToImage(imageData);
                }
                else
                {
                    Pb_yourPic.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student data: " + ex.Message);
            }
        }
        private void but_card_Click(object sender, EventArgs e)
        {

            pb_yourCardPic.Image = Pb_yourPic.Image;
            Label_name.Text = Tex_name.Text;
            Label_id.Text = Tex_Student_id.Text;
            Label_email.Text = Tex_email.Text;

            guna2GroupBox1.Visible = false;
            dgv_Student_info.Visible = false;
            guna2Panel1.Visible = true;
            Label_id.Visible = true;
            Label_name.Visible = true;
            Label_email.Visible = true;
            Back.Visible = true;
            but_card.Visible = false;

        }

        private void pb_yourCardPic_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            guna2GroupBox1.Visible = true;
            dgv_Student_info.Visible = true;
            guna2Panel1.Visible = false;
            Back.Visible = false;
            but_card.Visible = true;

        }
    }
}