namespace Univercity_Management_System
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            butUsers = new Button();
            button9 = new Button();
            butCourseSemester = new Button();
            butYears = new Button();
            butDepartment = new Button();
            butStudentCourse = new Button();
            butUniversity = new Button();
            butFaculty = new Button();
            butLecturer = new Button();
            butStudent = new Button();
            butMin = new Guna.UI2.WinForms.Guna2Button();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.Anchor = AnchorStyles.Top;
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Dubai", 18F, FontStyle.Bold);
            guna2HtmlLabel1.Location = new Point(541, 12);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(540, 53);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Welcome to the Al-Snowaryon University!";
            // 
            // guna2Panel2
            // 
            guna2Panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Panel2.BackColor = Color.FromArgb(0, 0, 64);
            guna2Panel2.Controls.Add(butUsers);
            guna2Panel2.Controls.Add(button9);
            guna2Panel2.Controls.Add(butCourseSemester);
            guna2Panel2.Controls.Add(butYears);
            guna2Panel2.Controls.Add(butDepartment);
            guna2Panel2.Controls.Add(butStudentCourse);
            guna2Panel2.Controls.Add(butUniversity);
            guna2Panel2.Controls.Add(butFaculty);
            guna2Panel2.Controls.Add(butLecturer);
            guna2Panel2.Controls.Add(butStudent);
            guna2Panel2.CustomizableEdges = customizableEdges1;
            guna2Panel2.Font = new Font("Dubai", 16F, FontStyle.Bold);
            guna2Panel2.Location = new Point(-1, 0);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel2.Size = new Size(282, 940);
            guna2Panel2.TabIndex = 2;
            // 
            // butUsers
            // 
            butUsers.BackColor = Color.FromArgb(241, 237, 228);
            butUsers.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butUsers.Location = new Point(23, 28);
            butUsers.Name = "butUsers";
            butUsers.Size = new Size(239, 49);
            butUsers.TabIndex = 9;
            butUsers.Text = "Create Usres";
            butUsers.UseVisualStyleBackColor = false;
            butUsers.Click += butUsers_Click;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button9.BackColor = Color.FromArgb(241, 237, 228);
            button9.Font = new Font("Dubai", 16F, FontStyle.Bold);
            button9.ForeColor = Color.Maroon;
            button9.Location = new Point(23, 849);
            button9.Name = "button9";
            button9.Size = new Size(238, 47);
            button9.TabIndex = 8;
            button9.Text = "LogOut";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // butCourseSemester
            // 
            butCourseSemester.BackColor = Color.FromArgb(241, 237, 228);
            butCourseSemester.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butCourseSemester.Location = new Point(23, 760);
            butCourseSemester.Name = "butCourseSemester";
            butCourseSemester.Size = new Size(238, 47);
            butCourseSemester.TabIndex = 7;
            butCourseSemester.Text = "Course Semester";
            butCourseSemester.UseVisualStyleBackColor = false;
            // 
            // butYears
            // 
            butYears.BackColor = Color.FromArgb(241, 237, 228);
            butYears.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butYears.Location = new Point(23, 203);
            butYears.Name = "butYears";
            butYears.Size = new Size(239, 49);
            butYears.TabIndex = 6;
            butYears.Text = "Years  Manegment";
            butYears.UseVisualStyleBackColor = false;
            butYears.Click += butYears_Click;
            // 
            // butDepartment
            // 
            butDepartment.BackColor = Color.FromArgb(241, 237, 228);
            butDepartment.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butDepartment.Location = new Point(21, 670);
            butDepartment.Name = "butDepartment";
            butDepartment.Size = new Size(238, 47);
            butDepartment.TabIndex = 5;
            butDepartment.Text = "Department Manegment";
            butDepartment.UseVisualStyleBackColor = false;
            // 
            // butStudentCourse
            // 
            butStudentCourse.BackColor = Color.FromArgb(241, 237, 228);
            butStudentCourse.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butStudentCourse.Location = new Point(23, 393);
            butStudentCourse.Name = "butStudentCourse";
            butStudentCourse.Size = new Size(239, 49);
            butStudentCourse.TabIndex = 4;
            butStudentCourse.Text = "Studet Course";
            butStudentCourse.UseVisualStyleBackColor = false;
            // 
            // butUniversity
            // 
            butUniversity.BackColor = Color.FromArgb(241, 237, 228);
            butUniversity.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butUniversity.Location = new Point(23, 111);
            butUniversity.Name = "butUniversity";
            butUniversity.Size = new Size(239, 49);
            butUniversity.TabIndex = 3;
            butUniversity.Text = "University Manegment";
            butUniversity.UseVisualStyleBackColor = false;
            // 
            // butFaculty
            // 
            butFaculty.BackColor = Color.FromArgb(241, 237, 228);
            butFaculty.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butFaculty.Location = new Point(22, 576);
            butFaculty.Name = "butFaculty";
            butFaculty.Size = new Size(238, 47);
            butFaculty.TabIndex = 2;
            butFaculty.Text = "Faculty Manegment";
            butFaculty.UseVisualStyleBackColor = false;
            // 
            // butLecturer
            // 
            butLecturer.BackColor = Color.FromArgb(241, 237, 228);
            butLecturer.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butLecturer.Location = new Point(22, 483);
            butLecturer.Name = "butLecturer";
            butLecturer.Size = new Size(239, 49);
            butLecturer.TabIndex = 1;
            butLecturer.Text = "Lecturer Manegment";
            butLecturer.UseVisualStyleBackColor = false;
            // 
            // butStudent
            // 
            butStudent.BackColor = Color.FromArgb(241, 237, 228);
            butStudent.Font = new Font("Dubai", 12F, FontStyle.Bold);
            butStudent.Location = new Point(22, 296);
            butStudent.Name = "butStudent";
            butStudent.Size = new Size(239, 50);
            butStudent.TabIndex = 0;
            butStudent.Text = "Student  Manegment";
            butStudent.UseVisualStyleBackColor = false;
            // 
            // butMin
            // 
            butMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            butMin.BackColor = Color.FromArgb(241, 237, 228);
            butMin.BorderColor = Color.FromArgb(241, 237, 228);
            butMin.BorderRadius = 5;
            butMin.CustomizableEdges = customizableEdges3;
            butMin.DisabledState.BorderColor = Color.DarkGray;
            butMin.DisabledState.CustomBorderColor = Color.DarkGray;
            butMin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            butMin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            butMin.FillColor = Color.FromArgb(241, 237, 228);
            butMin.Font = new Font("Dubai", 14.1999989F, FontStyle.Bold);
            butMin.ForeColor = Color.FromArgb(0, 0, 64);
            butMin.Location = new Point(1377, 12);
            butMin.Name = "butMin";
            butMin.ShadowDecoration.CustomizableEdges = customizableEdges4;
            butMin.Size = new Size(42, 42);
            butMin.TabIndex = 7;
            butMin.Text = "-";
            butMin.Click += butMin_Click;
            // 
            // guna2Button1
            // 
            guna2Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button1.BackColor = Color.FromArgb(241, 237, 228);
            guna2Button1.BorderColor = Color.FromArgb(241, 237, 228);
            guna2Button1.BorderRadius = 5;
            guna2Button1.CustomizableEdges = customizableEdges5;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(241, 237, 228);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(241, 237, 228);
            guna2Button1.Font = new Font("Dubai", 14.1999989F, FontStyle.Bold);
            guna2Button1.ForeColor = Color.FromArgb(0, 0, 64);
            guna2Button1.Location = new Point(1437, 12);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button1.Size = new Size(38, 42);
            guna2Button1.TabIndex = 6;
            guna2Button1.Text = "X";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 1;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 237, 228);
            ClientSize = new Size(1506, 941);
            Controls.Add(butMin);
            Controls.Add(guna2Button1);
            Controls.Add(guna2Panel2);
            Controls.Add(guna2HtmlLabel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "DashboardForm";
            Text = "DashboardForm";
            WindowState = FormWindowState.Maximized;
            guna2Panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Button butUniversity;
        private Button butFaculty;
        private Button butLecturer;
        private Button butStudent;
        private Guna.UI2.WinForms.Guna2Button butMin;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Button butStudentCourse;
        private Button butDepartment;
        private Button butYears;
        private Button butCourseSemester;
        private Button button9;
        private Button butUsers;
    }
}