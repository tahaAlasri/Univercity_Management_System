namespace Univercity_Management_System
{
    partial class University__Manegmen
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            txtUniversityID = new TextBox();
            label1 = new Label();
            button2 = new Button();
            groupBox1 = new GroupBox();
            txtUniversityName = new TextBox();
            ID1 = new Label();
            button5 = new Button();
            label4 = new Label();
            butMin = new Guna.UI2.WinForms.Guna2Button();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            dgv_Student_info = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Student_info).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.Location = new Point(758, 72);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(118, 33);
            button1.TabIndex = 0;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Maroon;
            button3.Location = new Point(573, 183);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(118, 33);
            button3.TabIndex = 2;
            button3.Text = "Delete";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Maroon;
            button4.Location = new Point(740, 183);
            button4.Margin = new Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new Size(118, 33);
            button4.TabIndex = 3;
            button4.Text = "Clear";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // txtUniversityID
            // 
            txtUniversityID.Location = new Point(215, 98);
            txtUniversityID.Margin = new Padding(4, 3, 4, 3);
            txtUniversityID.Name = "txtUniversityID";
            txtUniversityID.Size = new Size(252, 30);
            txtUniversityID.TabIndex = 4;
            txtUniversityID.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 229);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 23);
            label1.TabIndex = 7;
            label1.Text = "University Name";
            // 
            // button2
            // 
            button2.BackColor = Color.Green;
            button2.Location = new Point(573, 72);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(118, 33);
            button2.TabIndex = 10;
            button2.Text = "Add";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.None;
            groupBox1.BackColor = Color.Navy;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(txtUniversityName);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(ID1);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(txtUniversityID);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.ButtonFace;
            groupBox1.Location = new Point(22, 87);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(982, 379);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = " Mangment Infoemation";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // txtUniversityName
            // 
            txtUniversityName.Location = new Point(215, 221);
            txtUniversityName.Margin = new Padding(4, 3, 4, 3);
            txtUniversityName.Name = "txtUniversityName";
            txtUniversityName.Size = new Size(252, 30);
            txtUniversityName.TabIndex = 16;
            // 
            // ID1
            // 
            ID1.AutoSize = true;
            ID1.Location = new Point(51, 106);
            ID1.Margin = new Padding(4, 0, 4, 0);
            ID1.Name = "ID1";
            ID1.Size = new Size(113, 23);
            ID1.TabIndex = 14;
            ID1.Text = "University ID";
            // 
            // button5
            // 
            button5.BackColor = Color.Red;
            button5.ForeColor = Color.Navy;
            button5.Location = new Point(614, 270);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new Size(196, 33);
            button5.TabIndex = 13;
            button5.Text = "Back";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(418, 29);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(192, 23);
            label4.TabIndex = 12;
            label4.Text = "University Manegment";
            label4.Click += label4_Click;
            // 
            // butMin
            // 
            butMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            butMin.BackColor = Color.FromArgb(241, 237, 228);
            butMin.BorderColor = Color.FromArgb(241, 237, 228);
            butMin.BorderRadius = 5;
            butMin.CustomizableEdges = customizableEdges5;
            butMin.DisabledState.BorderColor = Color.DarkGray;
            butMin.DisabledState.CustomBorderColor = Color.DarkGray;
            butMin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            butMin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            butMin.FillColor = Color.FromArgb(241, 237, 228);
            butMin.Font = new Font("Microsoft Sans Serif", 14.1999989F, FontStyle.Bold);
            butMin.ForeColor = Color.FromArgb(0, 0, 64);
            butMin.Location = new Point(963, 10);
            butMin.Name = "butMin";
            butMin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            butMin.Size = new Size(42, 42);
            butMin.TabIndex = 19;
            butMin.Text = "-";
            butMin.Click += butMin_Click;
            // 
            // guna2Button1
            // 
            guna2Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button1.BackColor = Color.FromArgb(241, 237, 228);
            guna2Button1.BorderColor = Color.FromArgb(241, 237, 228);
            guna2Button1.BorderRadius = 5;
            guna2Button1.CustomizableEdges = customizableEdges7;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(241, 237, 228);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(241, 237, 228);
            guna2Button1.Font = new Font("Microsoft Sans Serif", 14.1999989F, FontStyle.Bold);
            guna2Button1.ForeColor = Color.FromArgb(0, 0, 64);
            guna2Button1.Location = new Point(1013, 10);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Button1.Size = new Size(38, 42);
            guna2Button1.TabIndex = 18;
            guna2Button1.Text = "X";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // dgv_Student_info
            // 
            dgv_Student_info.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv_Student_info.BackgroundColor = Color.FromArgb(241, 237, 228);
            dgv_Student_info.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Student_info.Location = new Point(2, 472);
            dgv_Student_info.Name = "dgv_Student_info";
            dgv_Student_info.RowHeadersWidth = 51;
            dgv_Student_info.Size = new Size(1173, 188);
            dgv_Student_info.TabIndex = 21;
            dgv_Student_info.UseWaitCursor = true;
            dgv_Student_info.CellContentClick += dgv_Student_info_CellContentClick;
            // 
            // University__Manegmen
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 237, 228);
            ClientSize = new Size(1095, 661);
            Controls.Add(dgv_Student_info);
            Controls.Add(butMin);
            Controls.Add(guna2Button1);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "University__Manegmen";
            Text = "University__Manegmen";
            TransparencyKey = Color.Black;
            WindowState = FormWindowState.Maximized;
            Load += University__Manegmen_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Student_info).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button3;
        private Button button4;
        private TextBox txtUniversityID;
        private Label label1;
        private Button button2;
        private GroupBox groupBox1;
        private Label label4;
        private Button button5;
        private Label ID1;
        private TextBox txtUniversityName;
        private Guna.UI2.WinForms.Guna2Button butMin;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private DataGridView dgv_Student_info;
    }
}