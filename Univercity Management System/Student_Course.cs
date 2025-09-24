using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class Student_Course : Form
    {
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public Student_Course()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadStudentCourseData();
            SetupDataGridView();
            LoadComboBoxData();
        }
        private void InitializeDatabase()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                dataAdapter = new SqlDataAdapter();
                dataTable = new DataTable();

                // Open database connection
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load students list
                string studentsQuery = "SELECT student_id, name FROM Student";
                SqlCommand studentsCmd = new SqlCommand(studentsQuery, connection);
                SqlDataReader studentsReader = studentsCmd.ExecuteReader();

                DataTable studentsTable = new DataTable();
                studentsTable.Load(studentsReader);
                studentsReader.Close();

                // Load courses list
                string coursesQuery = "SELECT course_id, code, title FROM Course_Semester";
                SqlCommand coursesCmd = new SqlCommand(coursesQuery, connection);
                SqlDataReader coursesReader = coursesCmd.ExecuteReader();

                DataTable coursesTable = new DataTable();
                coursesTable.Load(coursesReader);
                coursesReader.Close();

                // Load semesters list
                string semestersQuery = "SELECT Semester_ID, Semester_Name FROM Semester";
                SqlCommand semestersCmd = new SqlCommand(semestersQuery, connection);
                SqlDataReader semestersReader = semestersCmd.ExecuteReader();

                DataTable semestersTable = new DataTable();
                semestersTable.Load(semestersReader);
                semestersReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentCourseData()
        {
            try
            {
                string query = @"
                    SELECT 
                        sc.student_id,
                        s.name AS student_name,
                        sc.course_id,
                        cs.code AS course_code,
                        cs.title AS course_title,
                        sc.semester_id,
                        sem.Semester_Name,
                        sc.grade
                    FROM Student_Course sc
                    INNER JOIN Student s ON sc.student_id = s.student_id
                    INNER JOIN Course_Semester cs ON sc.course_id = cs.course_id
                    INNER JOIN Semester sem ON sc.semester_id = sem.Semester_ID";

                dataAdapter = new SqlDataAdapter(query, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgv_CourseStudent_info.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgv_CourseStudent_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_CourseStudent_info.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_CourseStudent_info.ReadOnly = true;
            dgv_CourseStudent_info.AllowUserToAddRows = false;

            // Set column headers
            if (dgv_CourseStudent_info.Columns.Count > 0)
            {
                dgv_CourseStudent_info.Columns["student_id"].HeaderText = "Student ID";
                dgv_CourseStudent_info.Columns["student_name"].HeaderText = "Student Name";
                dgv_CourseStudent_info.Columns["course_id"].HeaderText = "Course ID";
                dgv_CourseStudent_info.Columns["course_code"].HeaderText = "Course Code";
                dgv_CourseStudent_info.Columns["course_title"].HeaderText = "Course Title";
                dgv_CourseStudent_info.Columns["semester_id"].HeaderText = "Semester ID";
                dgv_CourseStudent_info.Columns["Semester_Name"].HeaderText = "Semester Name";
                dgv_CourseStudent_info.Columns["grade"].HeaderText = "Grade";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Tex_courseid.Text) || !int.TryParse(Tex_courseid.Text, out _))
            {
                MessageBox.Show("Please enter a valid course ID", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Tex_courseid.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Tex_studentid.Text) || !int.TryParse(Tex_studentid.Text, out _))
            {
                MessageBox.Show("Please enter a valid student ID", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Tex_studentid.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Tex_Semestrid.Text) || !int.TryParse(Tex_Semestrid.Text, out _))
            {
                MessageBox.Show("Please enter a valid semester ID", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Tex_Semestrid.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tex_grade.Text))
            {
                MessageBox.Show("Please enter a grade", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tex_grade.Focus();
                return false;
            }

            // Validate grade format (can be number or letter grade)
            if (!IsValidGrade(tex_grade.Text))
            {
                MessageBox.Show("Grade must be a number between 0-100 or letter grade (A, B, C, D, F)", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tex_grade.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidGrade(string grade)
        {
            // Check if grade is numeric (0-100)
            if (float.TryParse(grade, out float numericGrade))
            {
                return numericGrade >= 0 && numericGrade <= 100;
            }

            // Check if grade is letter grade
            string[] validLetterGrades = { "A", "B", "C", "D", "F", "A+", "A-", "B+", "B-", "C+", "C-", "D+", "D-" };
            return Array.Exists(validLetterGrades, g => g.Equals(grade, StringComparison.OrdinalIgnoreCase));
        }

        private void ClearInputs()
        {
            Tex_courseid.Clear();
            Tex_studentid.Clear();
            Tex_Semestrid.Clear();
            tex_grade.Clear();
        }

        private void Lecturer_Load(object sender, EventArgs e)
        {
            LoadStudentCourseData();
        }



        private void But_add_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string query = @"
                    IF NOT EXISTS (SELECT 1 FROM Student_Course 
                                  WHERE course_id = @CourseID AND student_id = @StudentID AND semester_id = @SemesterID)
                    BEGIN
                        INSERT INTO Student_Course (course_id, student_id, semester_id, grade)
                        VALUES (@CourseID, @StudentID, @SemesterID, @Grade)
                    END";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", int.Parse(Tex_courseid.Text));
                    command.Parameters.AddWithValue("@StudentID", int.Parse(Tex_studentid.Text));
                    command.Parameters.AddWithValue("@SemesterID", int.Parse(Tex_Semestrid.Text));
                    command.Parameters.AddWithValue("@Grade", tex_grade.Text);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Student course record added successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudentCourseData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("This student is already registered for this course in the same semester", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string query = @"
                    UPDATE Student_Course 
                    SET grade = @Grade
                    WHERE course_id = @CourseID AND student_id = @StudentID AND semester_id = @SemesterID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", int.Parse(Tex_courseid.Text));
                    command.Parameters.AddWithValue("@StudentID", int.Parse(Tex_studentid.Text));
                    command.Parameters.AddWithValue("@SemesterID", int.Parse(Tex_Semestrid.Text));
                    command.Parameters.AddWithValue("@Grade", tex_grade.Text);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Student course record updated successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudentCourseData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Record not found for update", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void But_delete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_courseid.Text) ||
                string.IsNullOrWhiteSpace(Tex_studentid.Text) ||
                string.IsNullOrWhiteSpace(Tex_Semestrid.Text))
            {
                MessageBox.Show("Please select a record to delete", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = @"
                        DELETE FROM Student_Course 
                        WHERE course_id = @CourseID AND student_id = @StudentID AND semester_id = @SemesterID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseID", int.Parse(Tex_courseid.Text));
                        command.Parameters.AddWithValue("@StudentID", int.Parse(Tex_studentid.Text));
                        command.Parameters.AddWithValue("@SemesterID", int.Parse(Tex_Semestrid.Text));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudentCourseData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Record not found for deletion", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void But_clear_Click_1(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void but_back_Click_1(object sender, EventArgs e)
        {
            DashboardForm frm = new DashboardForm();
            frm.Show();
            this.Hide();
        }

        private void but_print_Click_1(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Printing error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Create print report
            Graphics graphics = e.Graphics;
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font contentFont = new Font("Arial", 10);

            int yPosition = 100;
            int leftMargin = 50;

            // Report title
            graphics.DrawString("Student Course Report", titleFont, Brushes.Black, leftMargin, yPosition);
            yPosition += 40;

            // Report date
            graphics.DrawString($"Report Date: {DateTime.Now:yyyy-MM-dd HH:mm}", contentFont, Brushes.Black, leftMargin, yPosition);
            yPosition += 30;

            // Column headers
            string[] headers = { "Student ID", "Student Name", "Course Code", "Course Title", "Semester", "Grade" };
            float[] columnWidths = { 80, 120, 80, 150, 100, 50 };
            float xPosition = leftMargin;

            for (int i = 0; i < headers.Length; i++)
            {
                graphics.DrawString(headers[i], headerFont, Brushes.Black, xPosition, yPosition);
                xPosition += columnWidths[i];
            }

            yPosition += 30;

            // Report data
            foreach (DataRow row in dataTable.Rows)
            {
                xPosition = leftMargin;
                string[] rowData = {
                    row["student_id"].ToString(),
                    row["student_name"].ToString(),
                    row["course_code"].ToString(),
                    row["course_title"].ToString(),
                    row["Semester_Name"].ToString(),
                    row["grade"].ToString()
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    graphics.DrawString(rowData[i], contentFont, Brushes.Black, xPosition, yPosition);
                    xPosition += columnWidths[i];
                }

                yPosition += 25;

                // Check for page end
                if (yPosition >= e.MarginBounds.Height)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
        }

        private void dgv_Lecturer_info_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_CourseStudent_info.Rows[e.RowIndex];

                Tex_courseid.Text = row.Cells["course_id"].Value?.ToString() ?? "";
                Tex_studentid.Text = row.Cells["student_id"].Value?.ToString() ?? "";
                Tex_Semestrid.Text = row.Cells["semester_id"].Value?.ToString() ?? "";
                tex_grade.Text = row.Cells["grade"].Value?.ToString() ?? "";
            }
        }

    }
}
