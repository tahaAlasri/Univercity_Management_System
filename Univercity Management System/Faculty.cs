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
    public partial class Faculty_Department : Form
    {
        // In-memory list to store departments for this example

        public Faculty_Department()
        {
            
            InitializeComponent();

           
            InitializeGrid();
        }

        private void InitializeGrid()
        {


            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.Width = 150;

            var colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Name";
            colName.DataPropertyName = "Name";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            RefreshGrid();
        }

        private void RefreshGrid()
        {
            // Bind a copy so the grid updates properly

        }

        private void ClearInputs()
        {
            txt_FacDept_ID.Text = string.Empty;
            txt_FacDept_Name.Text = string.Empty;

        }

        private void But_add_Click(object? sender, EventArgs e)
        {
            var id = txt_FacDept_ID.Text.Trim();
            var name = txt_FacDept_Name.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please enter Department ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter Department Name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            ClearInputs();
        }

        private void But_update_Click(object? sender, EventArgs e)
        {
            
        }

        private void But_delete_Click(object? sender, EventArgs e)
        {
           
        }

        private void But_clear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BackButton_Click(object? sender, EventArgs e)
        {
            // Close this form to go back to previous
            this.Close();
        }

        private void Dgv_FacDept_info_CellClick(object? sender, DataGridViewCellEventArgs e)
        {



        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_FacDept_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txt_FacDept_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_Year_info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
