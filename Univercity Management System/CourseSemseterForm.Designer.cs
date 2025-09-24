partial class CourseSemesterForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        pnlHeader = new Panel();
        lblTitleHeader = new Label();
        btnClose = new Button();
        pnlInput = new Panel();
        btnBack = new Button();
        btnClear = new Button();
        btnDelete = new Button();
        btnUpdate = new Button();
        btnAdd = new Button();
        cmbAdvisor = new ComboBox();
        lblAdvisor = new Label();
        cmbProgram = new ComboBox();
        lblProgram = new Label();
        txtTitle = new TextBox();
        lblTitle = new Label();
        txtCourseCode = new TextBox();
        lblCourseCode = new Label();
        txtCourseId = new TextBox();
        lblCourseId = new Label();
        dgvCourses = new DataGridView();
        pnlHeader.SuspendLayout();
        pnlInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(0, 0, 64);
        pnlHeader.Controls.Add(lblTitleHeader);
        pnlHeader.Controls.Add(btnClose);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Margin = new Padding(4, 5, 4, 5);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1600, 77);
        pnlHeader.TabIndex = 0;
        pnlHeader.MouseDown += pnlHeader_MouseDown;
        pnlHeader.MouseMove += pnlHeader_MouseMove;
        pnlHeader.MouseUp += pnlHeader_MouseUp;
        // 
        // lblTitleHeader
        // 
        lblTitleHeader.AutoSize = true;
        lblTitleHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitleHeader.ForeColor = Color.White;
        lblTitleHeader.Location = new Point(16, 14);
        lblTitleHeader.Margin = new Padding(4, 0, 4, 0);
        lblTitleHeader.Name = "lblTitleHeader";
        lblTitleHeader.Size = new Size(409, 37);
        lblTitleHeader.TabIndex = 1;
        lblTitleHeader.Text = "Course Semester Management";
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.BackColor = Color.Red;
        btnClose.FlatStyle = FlatStyle.Flat;
        btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnClose.ForeColor = Color.White;
        btnClose.Location = new Point(1533, 8);
        btnClose.Margin = new Padding(4, 5, 4, 5);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(53, 62);
        btnClose.TabIndex = 0;
        btnClose.Text = "X";
        btnClose.UseVisualStyleBackColor = false;
        btnClose.Click += btnClose_Click;
        // 
        // pnlInput
        // 
        pnlInput.BackColor = SystemColors.Control;
        pnlInput.Controls.Add(btnBack);
        pnlInput.Controls.Add(btnClear);
        pnlInput.Controls.Add(btnDelete);
        pnlInput.Controls.Add(btnUpdate);
        pnlInput.Controls.Add(btnAdd);
        pnlInput.Controls.Add(cmbAdvisor);
        pnlInput.Controls.Add(lblAdvisor);
        pnlInput.Controls.Add(cmbProgram);
        pnlInput.Controls.Add(lblProgram);
        pnlInput.Controls.Add(txtTitle);
        pnlInput.Controls.Add(lblTitle);
        pnlInput.Controls.Add(txtCourseCode);
        pnlInput.Controls.Add(lblCourseCode);
        pnlInput.Controls.Add(txtCourseId);
        pnlInput.Controls.Add(lblCourseId);
        pnlInput.Dock = DockStyle.Left;
        pnlInput.Location = new Point(0, 77);
        pnlInput.Margin = new Padding(4, 5, 4, 5);
        pnlInput.Name = "pnlInput";
        pnlInput.Size = new Size(533, 1000);
        pnlInput.TabIndex = 1;
        // 
        // btnBack
        // 
        btnBack.BackColor = Color.FromArgb(0, 0, 64);
        btnBack.FlatStyle = FlatStyle.Flat;
        btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnBack.ForeColor = Color.White;
        btnBack.Location = new Point(32, 877);
        btnBack.Margin = new Padding(4, 5, 4, 5);
        btnBack.Name = "btnBack";
        btnBack.Size = new Size(475, 77);
        btnBack.TabIndex = 17;
        btnBack.Text = "Back";
        btnBack.UseVisualStyleBackColor = false;
        btnBack.Click += btnBack_Click;
        // 
        // btnClear
        // 
        btnClear.BackColor = Color.Gray;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnClear.ForeColor = Color.White;
        btnClear.Location = new Point(280, 785);
        btnClear.Margin = new Padding(4, 5, 4, 5);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(227, 77);
        btnClear.TabIndex = 16;
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.Red;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(32, 785);
        btnDelete.Margin = new Padding(4, 5, 4, 5);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(227, 77);
        btnDelete.TabIndex = 15;
        btnDelete.Text = "Delete";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = Color.Orange;
        btnUpdate.FlatStyle = FlatStyle.Flat;
        btnUpdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnUpdate.ForeColor = Color.White;
        btnUpdate.Location = new Point(280, 692);
        btnUpdate.Margin = new Padding(4, 5, 4, 5);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(227, 77);
        btnUpdate.TabIndex = 14;
        btnUpdate.Text = "Update";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.Green;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(32, 692);
        btnAdd.Margin = new Padding(4, 5, 4, 5);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(227, 77);
        btnAdd.TabIndex = 13;
        btnAdd.Text = "Add";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += btnAdd_Click;
        // 
        // cmbAdvisor
        // 
        cmbAdvisor.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbAdvisor.Font = new Font("Segoe UI", 12F);
        cmbAdvisor.FormattingEnabled = true;
        cmbAdvisor.Location = new Point(200, 384);
        cmbAdvisor.Margin = new Padding(4, 5, 4, 5);
        cmbAdvisor.Name = "cmbAdvisor";
        cmbAdvisor.Size = new Size(305, 36);
        cmbAdvisor.TabIndex = 12;
        // 
        // lblAdvisor
        // 
        lblAdvisor.AutoSize = true;
        lblAdvisor.Font = new Font("Segoe UI", 12F);
        lblAdvisor.Location = new Point(27, 388);
        lblAdvisor.Margin = new Padding(4, 0, 4, 0);
        lblAdvisor.Name = "lblAdvisor";
        lblAdvisor.Size = new Size(83, 28);
        lblAdvisor.TabIndex = 11;
        lblAdvisor.Text = "Advisor:";
        // 
        // cmbProgram
        // 
        cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbProgram.Font = new Font("Segoe UI", 12F);
        cmbProgram.FormattingEnabled = true;
        cmbProgram.Location = new Point(200, 311);
        cmbProgram.Margin = new Padding(4, 5, 4, 5);
        cmbProgram.Name = "cmbProgram";
        cmbProgram.Size = new Size(305, 36);
        cmbProgram.TabIndex = 10;
        // 
        // lblProgram
        // 
        lblProgram.AutoSize = true;
        lblProgram.Font = new Font("Segoe UI", 12F);
        lblProgram.Location = new Point(27, 316);
        lblProgram.Margin = new Padding(4, 0, 4, 0);
        lblProgram.Name = "lblProgram";
        lblProgram.Size = new Size(92, 28);
        lblProgram.TabIndex = 9;
        lblProgram.Text = "Program:";
        // 
        // txtTitle
        // 
        txtTitle.Font = new Font("Segoe UI", 12F);
        txtTitle.Location = new Point(200, 226);
        txtTitle.Margin = new Padding(4, 5, 4, 5);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(305, 34);
        txtTitle.TabIndex = 6;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 12F);
        lblTitle.Location = new Point(27, 231);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(53, 28);
        lblTitle.TabIndex = 5;
        lblTitle.Text = "Title:";
        // 
        // txtCourseCode
        // 
        txtCourseCode.Font = new Font("Segoe UI", 12F);
        txtCourseCode.Location = new Point(200, 165);
        txtCourseCode.Margin = new Padding(4, 5, 4, 5);
        txtCourseCode.Name = "txtCourseCode";
        txtCourseCode.Size = new Size(305, 34);
        txtCourseCode.TabIndex = 4;
        // 
        // lblCourseCode
        // 
        lblCourseCode.AutoSize = true;
        lblCourseCode.Font = new Font("Segoe UI", 12F);
        lblCourseCode.Location = new Point(27, 169);
        lblCourseCode.Margin = new Padding(4, 0, 4, 0);
        lblCourseCode.Name = "lblCourseCode";
        lblCourseCode.Size = new Size(127, 28);
        lblCourseCode.TabIndex = 3;
        lblCourseCode.Text = "Course Code:";
        // 
        // txtCourseId
        // 
        txtCourseId.Enabled = false;
        txtCourseId.Font = new Font("Segoe UI", 12F);
        txtCourseId.Location = new Point(200, 103);
        txtCourseId.Margin = new Padding(4, 5, 4, 5);
        txtCourseId.Name = "txtCourseId";
        txtCourseId.Size = new Size(305, 34);
        txtCourseId.TabIndex = 2;
        // 
        // lblCourseId
        // 
        lblCourseId.AutoSize = true;
        lblCourseId.Font = new Font("Segoe UI", 12F);
        lblCourseId.Location = new Point(27, 108);
        lblCourseId.Margin = new Padding(4, 0, 4, 0);
        lblCourseId.Name = "lblCourseId";
        lblCourseId.Size = new Size(100, 28);
        lblCourseId.TabIndex = 1;
        lblCourseId.Text = "Course ID:";
        // 
        // dgvCourses
        // 
        dgvCourses.AllowUserToAddRows = false;
        dgvCourses.AllowUserToDeleteRows = false;
        dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCourses.BackgroundColor = SystemColors.ButtonFace;
        dgvCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCourses.Dock = DockStyle.Fill;
        dgvCourses.Location = new Point(533, 77);
        dgvCourses.Margin = new Padding(4, 5, 4, 5);
        dgvCourses.Name = "dgvCourses";
        dgvCourses.ReadOnly = true;
        dgvCourses.RowHeadersWidth = 51;
        dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCourses.Size = new Size(1067, 1000);
        dgvCourses.TabIndex = 2;
        dgvCourses.CellClick += dgvCourses_CellClick;
        dgvCourses.CellContentClick += dgvCourses_CellContentClick;
        // 
        // CourseSemesterForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1600, 1077);
        Controls.Add(dgvCourses);
        Controls.Add(pnlInput);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(4, 5, 4, 5);
        Name = "CourseSemesterForm";
        Text = "Course Semester Management";
        WindowState = FormWindowState.Maximized;
        Load += CourseSemesterForm_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlInput.ResumeLayout(false);
        pnlInput.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlHeader;
    private System.Windows.Forms.Label lblTitleHeader;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel pnlInput;
    private System.Windows.Forms.DataGridView dgvCourses;
    private System.Windows.Forms.Label lblCourseId;
    private System.Windows.Forms.TextBox txtCourseId;
    private System.Windows.Forms.Label lblCourseCode;
    private System.Windows.Forms.TextBox txtCourseCode;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.TextBox txtTitle;
    private System.Windows.Forms.Label lblProgram;
    private System.Windows.Forms.ComboBox cmbProgram;
    private System.Windows.Forms.Label lblAdvisor;
    private System.Windows.Forms.ComboBox cmbAdvisor;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnBack;
}