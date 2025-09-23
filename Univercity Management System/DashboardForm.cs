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

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 dashboard = new Form1();
            dashboard.Show();
            this.Hide();
        }

        private void butUsers_Click(object sender, EventArgs e)
        {
            create_users frm = new create_users();
            frm.Show();
            this.Hide();
        }

        private void butYears_Click(object sender, EventArgs e)
        {
            Yaers yaer = new Yaers();
            yaer.Show();
            this.Hide();
        }

        private void butLecturer_Click(object sender, EventArgs e)
        {
            LecturerForm lec = new LecturerForm();
            lec.Show();
            this.Hide();
        }

        private void butStudent_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            student.Show();
            this.Hide();
        }
    }
}
