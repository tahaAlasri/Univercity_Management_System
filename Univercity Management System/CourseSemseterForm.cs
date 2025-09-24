using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Univercity_Management_System;
using static Guna.UI2.WinForms.Suite.Descriptions;

public partial class CourseSemesterForm : Form
{
    private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\lenovo\\Desktop\\Univercity_Management_System\\Univercity Management System\\Univercity_CRUD.mdf';Integrated Security=True";
    private bool isDragging = false;
    private Point dragCursorPoint;
    private Point dragFormPoint;

    public CourseSemesterForm()
    {
        InitializeComponent();
    }

    private void CourseSemesterForm_Load(object sender, EventArgs e)
    {
        LoadCoursesData();
        LoadPrograms();
        LoadAdvisors();
    }

    private void LoadCoursesData()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Course_Semester";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCourses.DataSource = dt;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading courses data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadPrograms()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT program_id, name FROM Program";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbProgram.DataSource = dt;
                cmbProgram.DisplayMember = "name";
                cmbProgram.ValueMember = "program_id";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading programs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadAdvisors()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT lecturer_id, name FROM Lecturer";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbAdvisor.DataSource = dt;
                cmbAdvisor.DisplayMember = "name";
                cmbAdvisor.ValueMember = "lecturer_id";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading advisors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCourseCode.Text) || string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            MessageBox.Show("Course Code and Title are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Course_Semester (Code, Title,  Program_id, Advisor_id) VALUES (@Code, @Title,@ProgramId, @AdvisorId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Code", txtCourseCode.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@ProgramId", cmbProgram.SelectedValue);
                cmd.Parameters.AddWithValue("@AdvisorId", cmbAdvisor.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Course added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCoursesData();
                ClearFields();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error adding course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCourseId.Text))
        {
            MessageBox.Show("Please select a course from the list to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Course_Semester SET Code = @Code, Title = @Title,Program_id = @ProgramId, Advisor_id = @AdvisorId WHERE Course_id = @CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", int.Parse(txtCourseId.Text));
                cmd.Parameters.AddWithValue("@Code", txtCourseCode.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@ProgramId", cmbProgram.SelectedValue);
                cmd.Parameters.AddWithValue("@AdvisorId", cmbAdvisor.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Course updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCoursesData();
                ClearFields();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCourseId.Text))
        {
            MessageBox.Show("Please select a course from the list to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Course_Semester WHERE Course_id = @CourseId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseId", int.Parse(txtCourseId.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCoursesData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        DashboardForm dashboardForm = new DashboardForm();
        dashboardForm.ShowDialog();
        this.Close();
    }

    private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            DataGridViewRow row = dgvCourses.Rows[e.RowIndex];
            txtCourseId.Text = row.Cells["Course_id"].Value.ToString();
            txtCourseCode.Text = row.Cells["Code"].Value.ToString();
            txtTitle.Text = row.Cells["Title"].Value.ToString();

            object programValue = row.Cells["Program_id"].Value;
            object advisorValue = row.Cells["Advisor_id"].Value;

            if (programValue != DBNull.Value)
            {
                cmbProgram.SelectedValue = programValue;
            }
            else
            {
                cmbProgram.SelectedIndex = -1;
            }

            if (advisorValue != DBNull.Value)
            {
                cmbAdvisor.SelectedValue = advisorValue;
            }
            else
            {
                cmbAdvisor.SelectedIndex = -1;
            }
        }
    }

    private void ClearFields()
    {
        txtCourseId.Clear();
        txtCourseCode.Clear();
        txtTitle.Clear();
        cmbProgram.SelectedIndex = -1;
        cmbAdvisor.SelectedIndex = -1;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
    {
        isDragging = true;
        dragCursorPoint = Cursor.Position;
        dragFormPoint = this.Location;
    }

    private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            this.Location = Point.Add(dragFormPoint, new Size(diff));
        }
    }

    private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
    {
        isDragging = false;
    }

    private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void plInput_Paint(object sender, PaintEventArgs e)
    {

    }
}