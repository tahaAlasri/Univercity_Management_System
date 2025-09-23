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
    public partial class Yaers : Form
    {
        Year yearObj = new Year();
        public Yaers()
        {
            InitializeComponent();
        }
        private void LoadYears()
        {
            DataTable dt = Year.GetYears();
            dgv_Year_info.DataSource = dt;
        }

        private void But_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Year_label.Text))
            {
                MessageBox.Show("Please enter the year label!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Year year = new Year
            {
                Year_label = Tex_Year_label.Text.Trim()
            };

            if (yearObj.InsertYear(year))
            {
                MessageBox.Show("Year added successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadYears();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to add year ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Yaers_Load(object sender, EventArgs e)
        {
            LoadYears();
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Year_id.Text) || string.IsNullOrWhiteSpace(Tex_Year_label.Text))
            {
                MessageBox.Show("Please select a year to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Year year = new Year
            {
                Year_id = Convert.ToInt32(Tex_Year_id.Text),
                Year_label = Tex_Year_label.Text.Trim()
            };

            if (yearObj.UpdateYear(year))
            {
                MessageBox.Show("Year updated successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadYears();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to update year ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void But_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tex_Year_id.Text))
            {
                MessageBox.Show("Please select a year to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(Tex_Year_id.Text);

            if (MessageBox.Show("Are you sure you want to delete this year?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (yearObj.DeleteYear(id))
                {
                    MessageBox.Show("Year deleted successfully ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadYears();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete year ❌", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Select row from DataGridView
        private void dgv_Year_info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Tex_Year_id.Text = dgv_Year_info.Rows[e.RowIndex].Cells["Year_id"].Value.ToString();
                Tex_Year_label.Text = dgv_Year_info.Rows[e.RowIndex].Cells["Year_label"].Value.ToString();
            }
        }
        private void ClearFields()
        {
            Tex_Year_id.Clear();
            Tex_Year_label.Clear();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Semester sem = new Semester();
            sem.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DashboardForm cu = new DashboardForm();
            cu.Show();
            this.Hide();
        }

        private void dgv_Year_info_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var index = e.RowIndex;
            Tex_Year_id.Text = dgv_Year_info.Rows[index].Cells[0].Value.ToString();
            Tex_Year_label.Text = dgv_Year_info.Rows[index].Cells[1].Value.ToString();

        }
    }
}
