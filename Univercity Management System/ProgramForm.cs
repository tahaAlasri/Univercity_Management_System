using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class ProgramForm : Form
    {
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public ProgramForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadProgramData();
            SetupDataGridView();
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

        private void LoadProgramData()
        {
            try
            {
                string query = @"
                    SELECT 
                        p.program_id,
                        p.name AS program_name,
                        p.faculty_id,
                        f.name AS faculty_name
                    FROM Program p
                    LEFT JOIN Faculty f ON p.faculty_id = f.faculty_id";

                dataAdapter = new SqlDataAdapter(query, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgv_Program_info.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading program data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView appearance and behavior
            dgv_Program_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Program_info.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Program_info.ReadOnly = true;
            dgv_Program_info.AllowUserToAddRows = false;

            // Set column headers if columns exist
            if (dgv_Program_info.Columns.Count > 0)
            {
                dgv_Program_info.Columns["program_id"].HeaderText = "Program ID";
                dgv_Program_info.Columns["program_name"].HeaderText = "Program Name";
                dgv_Program_info.Columns["faculty_id"].HeaderText = "Faculty ID";
                dgv_Program_info.Columns["faculty_name"].HeaderText = "Faculty Name";
            }
        }

        private bool ValidateInputs()
        {
            // Validate program name
            if (string.IsNullOrWhiteSpace(Tex_name.Text))
            {
                MessageBox.Show("Please enter a program name", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Tex_name.Focus();
                return false;
            }

            // Validate faculty ID (optional field but should be numeric if provided)
            if (!string.IsNullOrWhiteSpace(tex_faculty_id.Text) && !int.TryParse(tex_faculty_id.Text, out _))
            {
                MessageBox.Show("Faculty ID must be a valid number", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tex_faculty_id.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateFacultyExists(int facultyId)
        {
            try
            {
                string checkQuery = "SELECT COUNT(1) FROM Faculty WHERE faculty_id = @FacultyID";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@FacultyID", facultyId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating faculty: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearInputs()
        {
            Tex_Programid.Clear();
            Tex_name.Clear();
            tex_faculty_id.Clear();
        }
        private void But_add_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                // Validate faculty ID if provided
                int? facultyId = null;
                if (!string.IsNullOrWhiteSpace(tex_faculty_id.Text))
                {
                    facultyId = int.Parse(tex_faculty_id.Text);
                    if (!ValidateFacultyExists(facultyId.Value))
                    {
                        MessageBox.Show("The specified faculty ID does not exist", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = @"
                    INSERT INTO Program (name, faculty_id)
                    VALUES (@Name, @FacultyID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Tex_name.Text.Trim());

                    if (facultyId.HasValue)
                        command.Parameters.AddWithValue("@FacultyID", facultyId.Value);
                    else
                        command.Parameters.AddWithValue("@FacultyID", DBNull.Value);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Program added successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProgramData();
                        ClearInputs();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547) // Foreign key violation
                {
                    MessageBox.Show("The specified faculty ID does not exist in the database", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding program: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs() || string.IsNullOrWhiteSpace(Tex_Programid.Text))
            {
                MessageBox.Show("Please select a program to update", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validate faculty ID if provided
                int? facultyId = null;
                if (!string.IsNullOrWhiteSpace(tex_faculty_id.Text))
                {
                    facultyId = int.Parse(tex_faculty_id.Text);
                    if (!ValidateFacultyExists(facultyId.Value))
                    {
                        MessageBox.Show("The specified faculty ID does not exist", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = @"
                    UPDATE Program 
                    SET name = @Name, faculty_id = @FacultyID
                    WHERE program_id = @ProgramID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProgramID", int.Parse(Tex_Programid.Text));
                    command.Parameters.AddWithValue("@Name", Tex_name.Text.Trim());

                    if (facultyId.HasValue)
                        command.Parameters.AddWithValue("@FacultyID", facultyId.Value);
                    else
                        command.Parameters.AddWithValue("@FacultyID", DBNull.Value);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Program updated successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProgramData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Program not found for update", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("The specified faculty ID does not exist", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating program: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void But_delete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Tex_Programid.Text))
            {
                MessageBox.Show("Please select a program to delete", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int programId = int.Parse(Tex_Programid.Text);
            string programName = Tex_name.Text;

            // التحقق أولاً من وجود مراجع مرتبطة بالبرنامج
            if (HasRelatedRecords(programId))
            {
                MessageBox.Show($"Cannot delete program '{programName}' because it has related records.\n\n" +
                              "Please remove or reassign all students, courses, and lecturers from this program first.",
                    "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the program '{programName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Program WHERE program_id = @ProgramID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProgramID", programId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Program deleted successfully", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProgramData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Program not found for deletion", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Cannot delete this program. It is referenced by other records in the database.\n\n" +
                                      "Please remove all references to this program before deleting it.",
                            "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Database error: {sqlEx.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting program: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // دالة للتحقق من وجود سجلات مرتبطة بالبرنامج
        private bool HasRelatedRecords(int programId)
        {
            try
            {
                // قم بتعديل أسماء الجداول حسب هيكل قاعدة البيانات الخاص بك
                string[] checkQueries = {
            "SELECT COUNT(*) FROM Students WHERE program_id = @ProgramID",
            "SELECT COUNT(*) FROM Courses WHERE program_id = @ProgramID",
            "SELECT COUNT(*) FROM Lecturer WHERE program_id = @ProgramID",
            "SELECT COUNT(*) FROM ProgramCourses WHERE program_id = @ProgramID"
        };

                foreach (string query in checkQueries)
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProgramID", programId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            return true; // يوجد سجلات مرتبطة
                        }
                    }
                }
                return false; // لا توجد سجلات مرتبطة
            }
            catch (Exception ex)
            {
                // في حالة حدوث خطأ في التحقق، نعود للطريقة الآمنة
                Console.WriteLine($"Error checking related records: {ex.Message}");
                return false;
            }
        }
        private void But_clear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void but_back_Click(object sender, EventArgs e)
        {
            FacultyForme frm = new FacultyForme();
            frm.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Create print report for programs
            Graphics graphics = e.Graphics;
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font contentFont = new Font("Arial", 10);

            int yPosition = 100;
            int leftMargin = 50;

            // Report title
            graphics.DrawString("Program Management Report", titleFont, Brushes.Black, leftMargin, yPosition);
            yPosition += 40;

            // Report date
            graphics.DrawString($"Report Date: {DateTime.Now:yyyy-MM-dd HH:mm}", contentFont, Brushes.Black, leftMargin, yPosition);
            yPosition += 30;

            // Column headers
            string[] headers = { "Program ID", "Program Name", "Faculty ID", "Faculty Name" };
            float[] columnWidths = { 100, 200, 100, 200 };
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
                    row["program_id"].ToString(),
                    row["program_name"].ToString(),
                    row["faculty_id"] == DBNull.Value ? "N/A" : row["faculty_id"].ToString(),
                    row["faculty_name"] == DBNull.Value ? "N/A" : row["faculty_name"].ToString()
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

        private void dgv_Program_info_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Program_info.Rows[e.RowIndex];

                // Populate form fields with selected program data
                Tex_Programid.Text = row.Cells["program_id"].Value?.ToString() ?? "";
                Tex_name.Text = row.Cells["program_name"].Value?.ToString() ?? "";

                if (row.Cells["faculty_id"].Value != DBNull.Value && row.Cells["faculty_id"].Value != null)
                    tex_faculty_id.Text = row.Cells["faculty_id"].Value.ToString();
                else
                    tex_faculty_id.Clear();
            }
        }

        private void ProgramForm_Load(object sender, EventArgs e)
        {
            LoadProgramData();
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
                MessageBox.Show($"Printing error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}