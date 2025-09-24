using System;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class FacultyForme : Form
    {
        private string connectionString = Properties.Settings.Default.Univercity_CRUD;
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public FacultyForme()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadFacultyData();
            SetupDataGridView();
        }

        private void InitializeDatabase()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                dataAdapter = new SqlDataAdapter();
                dataTable = new DataTable();
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFacultyData()
        {
            try
            {
                string query = @"
                    SELECT 
                        f.faculty_id,
                        f.name AS faculty_name,
                        f.dean,
                        f.university_id,
                        u.name AS university_name
                    FROM Faculty f
                    LEFT JOIN University u ON f.university_id = u.university_id";

                dataAdapter = new SqlDataAdapter(query, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgv_Faculty_info.DataSource = dataTable;
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading faculty data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgv_Faculty_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Faculty_info.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Faculty_info.ReadOnly = true;
            dgv_Faculty_info.AllowUserToAddRows = false;

            // Set column headers
            if (dgv_Faculty_info.Columns.Count >= 5)
            {
                dgv_Faculty_info.Columns[0].HeaderText = "Faculty ID";
                dgv_Faculty_info.Columns[1].HeaderText = "Faculty Name";
                dgv_Faculty_info.Columns[2].HeaderText = "Dean";
                dgv_Faculty_info.Columns[3].HeaderText = "University ID";
                dgv_Faculty_info.Columns[4].HeaderText = "University Name";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txt_FacDept_Name.Text))
            {
                MessageBox.Show("Please enter faculty name", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_FacDept_Name.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tex_Dean.Text))
            {
                MessageBox.Show("Please enter dean name", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tex_Dean.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tex_un_id.Text) || !int.TryParse(tex_un_id.Text, out _))
            {
                MessageBox.Show("Please enter a valid university ID", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tex_un_id.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateUniversityExists(int universityId)
        {
            try
            {
                string checkQuery = "SELECT COUNT(1) FROM University WHERE university_id = @UniversityID";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UniversityID", universityId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating university: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearInputs()
        {
            txt_FacDept_ID.Clear();
            txt_FacDept_Name.Clear();
            tex_Dean.Clear();
            tex_un_id.Clear();
            dgv_Faculty_info.ClearSelection();
        }
        private void dgv_Year_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Faculty_info.Rows[e.RowIndex];

                // Populate form fields with selected faculty data
                txt_FacDept_ID.Text = row.Cells["faculty_id"].Value?.ToString() ?? "";
                txt_FacDept_Name.Text = row.Cells["faculty_name"].Value?.ToString() ?? "";
                tex_Dean.Text = row.Cells["dean"].Value?.ToString() ?? "";

                if (row.Cells["university_id"].Value != DBNull.Value && row.Cells["university_id"].Value != null)
                    tex_un_id.Text = row.Cells["university_id"].Value.ToString();
                else
                    tex_un_id.Clear();
            }
        }

        private void But_add_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                int universityId = int.Parse(tex_un_id.Text);
                if (!ValidateUniversityExists(universityId))
                {
                    MessageBox.Show("The specified university ID does not exist", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = @"
                    INSERT INTO Faculty (name, dean, university_id)
                    VALUES (@Name, @Dean, @UniversityID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", txt_FacDept_Name.Text.Trim());
                    command.Parameters.AddWithValue("@Dean", tex_Dean.Text.Trim());
                    command.Parameters.AddWithValue("@UniversityID", universityId);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Faculty added successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFacultyData();
                        ClearInputs();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("The specified university ID does not exist in the database", "Error",
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
                MessageBox.Show($"Error adding faculty: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void but_back_Click(object sender, EventArgs e)
        {
            DashboardForm frm = new DashboardForm();
            frm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Faculty_Department_Load(object sender, EventArgs e)
        {
            LoadFacultyData();
        }

        private void dgv_Faculty_info_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var index = e.RowIndex;
                DataGridViewRow row = dgv_Faculty_info.Rows[index];

                txt_FacDept_ID.Text = row.Cells[0].Value?.ToString() ?? "";
                txt_FacDept_Name.Text = row.Cells[1].Value?.ToString() ?? "";
                tex_Dean.Text = row.Cells[2].Value?.ToString() ?? "";
                tex_un_id.Text = row.Cells[3].Value?.ToString() ?? "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student data: " + ex.Message);
            }
        }

        private void But_update_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs() || string.IsNullOrWhiteSpace(txt_FacDept_ID.Text))
            {
                MessageBox.Show("Please select a faculty to update", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int universityId = int.Parse(tex_un_id.Text);
                if (!ValidateUniversityExists(universityId))
                {
                    MessageBox.Show("The specified university ID does not exist", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = @"
        UPDATE Faculty 
        SET name = @Name, dean = @Dean, university_id = @UniversityID
        WHERE faculty_id = @FacultyID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FacultyID", int.Parse(txt_FacDept_ID.Text));
                    command.Parameters.AddWithValue("@Name", txt_FacDept_Name.Text.Trim());
                    command.Parameters.AddWithValue("@Dean", tex_Dean.Text.Trim());
                    command.Parameters.AddWithValue("@UniversityID", universityId);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Faculty updated successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFacultyData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Faculty not found for update", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("The specified university ID does not exist", "Error",
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
                MessageBox.Show($"Error updating faculty: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void But_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_FacDept_ID.Text))
            {
                MessageBox.Show("Please select a faculty to delete", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string facultyName = txt_FacDept_Name.Text;
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the faculty '{facultyName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Faculty WHERE faculty_id = @FacultyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FacultyID", int.Parse(txt_FacDept_ID.Text));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Faculty deleted successfully", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadFacultyData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Faculty not found for deletion", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Cannot delete this faculty. It may be referenced by other records in the database.\n\n" +
                                      "Please remove all references to this faculty before deleting it.",
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
                    MessageBox.Show($"Error deleting faculty: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void But_clear_Click_1(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void but_programForm_Click(object sender, EventArgs e)
        {
            ProgramForm programForm = new ProgramForm();
            programForm.ShowDialog();
            this.Close();
        }
    }
}
