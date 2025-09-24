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
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void but_programForm_Click(object sender, EventArgs e)
        {
            University__Manegmen university__Manegmen = new University__Manegmen();
            university__Manegmen.ShowDialog();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            create_users create_Users = new create_users();
            create_Users.ShowDialog();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Yaers year = new Yaers();
            year.ShowDialog();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LecturerForm lecturerForm = new LecturerForm();
            lecturerForm.ShowDialog();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            studentForm.ShowDialog();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Student_Course sc = new Student_Course();
            sc.ShowDialog();
            this.Hide();
        }
    }
}
