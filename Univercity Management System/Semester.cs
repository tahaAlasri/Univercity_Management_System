using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class Semester : Form
    {
        Semesters semesterObj = new Semesters();
        public Semester()
        {
            InitializeComponent();
        }
        private void LoadSemesters()
        {
            try
            {
                string query = @"SELECT 
                    s.Semester_id, 
                    s.Semester_name, 
                    y.Year_label, 
                    CAST(s.Start_date AS DATE) as Start_date, 
                    CAST(s.End_date AS DATE) as End_date, 
                    CAST(s.Is_active AS BIT) as Is_active,
                    s.Year_id
                    FROM Semester s 
                    INNER JOIN Year y ON s.Year_id = y.Year_id";

                DataTable dt = Semesters.ExecuteQuery(query);

                // Ensure data types are correct
                dt.Columns["Start_date"].DataType = typeof(DateTime);
                dt.Columns["End_date"].DataType = typeof(DateTime);
                dt.Columns["Is_active"].DataType = typeof(bool);

                dgv_Semester_info.DataSource = dt;

                // Hide Year_id column if it exists
                if (dgv_Semester_info.Columns.Contains("Year_id"))
                {
                    dgv_Semester_info.Columns["Year_id"].Visible = false;
                }

                // Add event handlers
                dgv_Semester_info.DataError += dgv_Semester_info_DataError;
                dgv_Semester_info.CellFormatting += dgv_Semester_info_CellFormatting;

                // Format the grid view
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading semesters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadYears()
        {
            try
            {
                DataTable dt = Year.GetYears();

                if (dt != null && dt.Rows.Count > 0)
                {
                    // مسح العناصر الحالية أولاً
                    cmb_Year.Items.Clear();

                    // تعيين DataSource
                    cmb_Year.DataSource = dt;
                    cmb_Year.DisplayMember = "Year_label";  // اسم العمود الذي يعرض النص
                    cmb_Year.ValueMember = "Year_id";       // اسم العمود للقيمة المخفية

                    Console.WriteLine($"ComboBox loaded with {dt.Rows.Count} items");

                    // اختيار أول عنصر
                    if (cmb_Year.Items.Count > 0)
                    {
                        cmb_Year.SelectedIndex = 0;
                        Console.WriteLine($"Selected item: {cmb_Year.Text}");
                    }
                }
                else
                {
                    MessageBox.Show("No years found. Please add years first.",
                                  "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void But_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Semester_name.Text) ||
                cmb_Year.SelectedValue == null)
            {
                MessageBox.Show("Please fill all required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtp_StartDate.Value >= dtp_EndDate.Value)
            {
                MessageBox.Show("End date must be after start date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Semesters semester = new Semesters
                {
                    Semester_name = Tex_Semester_name.Text.Trim(),
                    Year_id = Convert.ToInt32(cmb_Year.SelectedValue),
                    Start_date = dtp_StartDate.Value,
                    End_date = dtp_EndDate.Value,
                    Is_active = chk_IsActive.Checked
                };

                if (semesterObj.InsertSemester(semester))
                {
                    MessageBox.Show("Semester added successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSemesters();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to add semester ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding semester: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Semester_id.Text) ||
                string.IsNullOrWhiteSpace(Tex_Semester_name.Text) ||
                cmb_Year.SelectedValue == null)
            {
                MessageBox.Show("Please select a semester to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtp_StartDate.Value >= dtp_EndDate.Value)
            {
                MessageBox.Show("End date must be after start date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Semesters semester = new Semesters
                {
                    Semester_id = Convert.ToInt32(Tex_Semester_id.Text),
                    Semester_name = Tex_Semester_name.Text.Trim(),
                    Year_id = Convert.ToInt32(cmb_Year.SelectedValue),
                    Start_date = dtp_StartDate.Value,
                    End_date = dtp_EndDate.Value,
                    Is_active = chk_IsActive.Checked
                };

                if (semesterObj.UpdateSemester(semester))
                {
                    MessageBox.Show("Semester updated successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSemesters();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to update semester ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating semester: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Semester_Load(object sender, EventArgs e)
        {
            LoadSemesters();
            LoadYears();

            // Set default dates
            dtp_StartDate.Value = DateTime.Now;
            dtp_EndDate.Value = DateTime.Now.AddMonths(4);
        }

        private void But_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Semester_id.Text))
            {
                MessageBox.Show("Please select a semester to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(Tex_Semester_id.Text);

                if (MessageBox.Show("Are you sure you want to delete this semester?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (semesterObj.DeleteSemester(id))
                    {
                        MessageBox.Show("Semester deleted successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSemesters();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete semester ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting semester: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void But_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dgv_Semester_info_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent default error dialog
            e.ThrowException = false;

            // Log the error silently
            Console.WriteLine($"DataError at row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
        }

        private void FormatDataGridView()
        {
            try
            {
                // Format date columns
                if (dgv_Semester_info.Columns.Contains("Start_date"))
                {
                    dgv_Semester_info.Columns["Start_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                if (dgv_Semester_info.Columns.Contains("End_date"))
                {
                    dgv_Semester_info.Columns["End_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                // Set column headers
                if (dgv_Semester_info.Columns.Contains("Semester_name"))
                {
                    dgv_Semester_info.Columns["Semester_name"].HeaderText = "Semester Name";
                }
                if (dgv_Semester_info.Columns.Contains("Year_label"))
                {
                    dgv_Semester_info.Columns["Year_label"].HeaderText = "Academic Year";
                }
                if (dgv_Semester_info.Columns.Contains("Start_date"))
                {
                    dgv_Semester_info.Columns["Start_date"].HeaderText = "Start Date";
                }
                if (dgv_Semester_info.Columns.Contains("End_date"))
                {
                    dgv_Semester_info.Columns["End_date"].HeaderText = "End Date";
                }
                if (dgv_Semester_info.Columns.Contains("Is_active"))
                {
                    dgv_Semester_info.Columns["Is_active"].HeaderText = "Active";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
        }

        private void dgv_Semester_info_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Format Is_active column
                if (dgv_Semester_info.Columns[e.ColumnIndex].Name == "Is_active" && e.Value != null)
                {
                    try
                    {
                        if (e.Value is bool)
                        {
                            e.Value = (bool)e.Value ? "Yes" : "No";
                            e.FormattingApplied = true;
                        }
                    }
                    catch
                    {
                        e.Value = "No";
                        e.FormattingApplied = true;
                    }
                }

                // Format date columns
                if ((dgv_Semester_info.Columns[e.ColumnIndex].Name == "Start_date" ||
                     dgv_Semester_info.Columns[e.ColumnIndex].Name == "End_date") &&
                    e.Value != null)
                {
                    try
                    {
                        if (e.Value is DateTime)
                        {
                            e.Value = ((DateTime)e.Value).ToString("yyyy-MM-dd");
                            e.FormattingApplied = true;
                        }
                    }
                    catch
                    {
                        // Ignore date formatting errors
                    }
                }
            }
        }

        private void dgv_Semester_info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv_Semester_info.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgv_Semester_info.Rows[e.RowIndex];

                    // تعبئة الحقول الأساسية
                    Tex_Semester_id.Text = row.Cells["Semester_id"].Value?.ToString() ?? "";
                    Tex_Semester_name.Text = row.Cells["Semester_name"].Value?.ToString() ?? "";

                    // **المهم: تعيين Year_id في ComboBox**
                    if (row.Cells["Year_id"].Value != null)
                    {
                        int yearId = Convert.ToInt32(row.Cells["Year_id"].Value);
                        Console.WriteLine($"Trying to set YearID: {yearId}");

                        SetComboBoxValue(cmb_Year, yearId);
                    }

                    // تعيين التواريخ
                    if (row.Cells["Start_date"].Value != null)
                    {
                        dtp_StartDate.Value = Convert.ToDateTime(row.Cells["Start_date"].Value);
                    }

                    if (row.Cells["End_date"].Value != null)
                    {
                        dtp_EndDate.Value = Convert.ToDateTime(row.Cells["End_date"].Value);
                    }

                    // تعيين حالة التنشيط
                    if (row.Cells["Is_active"].Value != null)
                    {
                        chk_IsActive.Checked = Convert.ToBoolean(row.Cells["Is_active"].Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting row: {ex.Message}", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // دالة مساعدة للتحقق من ComboBox
        private void CheckComboBoxState()
        {
            if (cmb_Year != null)
            {
                Console.WriteLine($"ComboBox Items Count: {cmb_Year.Items.Count}");
                Console.WriteLine($"ComboBox DataSource: {cmb_Year.DataSource}");

                if (cmb_Year.DataSource is DataTable dt)
                {
                    Console.WriteLine($"DataTable Rows: {dt.Rows.Count}");
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"YearID: {row["Year_id"]}, YearLabel: {row["Year_label"]}");
                    }
                }
            }
        }

        // استدعاء الدالة بعد تحميل البيانات
        private void SemesterForm_Load(object sender, EventArgs e)
        {
            LoadSemesters();
            LoadYears();

            // التحقق من حالة ComboBox
            CheckComboBoxState();

            dtp_StartDate.Value = DateTime.Now;
            dtp_EndDate.Value = DateTime.Now.AddMonths(4);
        }
        private void SetComboBoxValue(Guna.UI2.WinForms.Guna2ComboBox comboBox, int value)
        {
            try
            {
                // المحاولة الأولى: استخدام SelectedValue
                comboBox.SelectedValue = value;

                // التحقق إذا تم التعيين بنجاح
                if (comboBox.SelectedValue != null && Convert.ToInt32(comboBox.SelectedValue) == value)
                {
                    Console.WriteLine($"Successfully set ComboBox value to: {value}");
                    return;
                }

                // المحاولة الثانية: البحث يدوياً في العناصر
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i] is DataRowView rowView)
                    {
                        int itemYearId = Convert.ToInt32(rowView["Year_id"]);
                        if (itemYearId == value)
                        {
                            comboBox.SelectedIndex = i;
                            Console.WriteLine($"Manually set ComboBox to index: {i}, value: {value}");
                            return;
                        }
                    }
                }

                // إذا لم يتم العثور على القيمة
                Console.WriteLine($"YearID {value} not found in ComboBox");

                // اختيار أول عنصر إذا كان متاحاً
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                    Console.WriteLine("Set ComboBox to first available item");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting ComboBox value: {ex.Message}");
            }
        }
        private void ClearFields()
        {
            Tex_Semester_id.Clear();
            Tex_Semester_name.Clear();

            if (cmb_Year.Items.Count > 0)
            {
                cmb_Year.SelectedIndex = 0;
            }

            dtp_StartDate.Value = DateTime.Now;
            dtp_EndDate.Value = DateTime.Now.AddMonths(4);
            chk_IsActive.Checked = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Yaers logForm = new Yaers();
            logForm.ShowDialog();
        }
    }
}