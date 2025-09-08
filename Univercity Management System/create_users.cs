using System;
using System.Data;
using System.Windows.Forms;

namespace Univercity_Management_System
{
    public partial class create_users : Form
    {
        public create_users()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgv_user_info.DataSource = Users.GetUsers();
            dgv_user_info.ClearSelection();
        }

        private void But_add_Click(object sender, EventArgs e)
        {
            try
            {
                Users user = new Users
                {
                    UserName = Tex_username.Text,
                    Password_hash = Tex_pass.Text,
                    Role = Tex_role.Text
                };

                if (user.InsertUser(user))
                {
                    MessageBox.Show("✅ تم إضافة المستخدم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("❌ فشل في إضافة المستخدم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message);
            }
        }

        private void But_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Tex_userid.Text, out int userId))
                {
                    MessageBox.Show("⚠️ حدد المستخدم لتعديله");
                    return;
                }

                Users user = new Users
                {
                    UserId = userId,
                    UserName = Tex_username.Text,
                    Password_hash = Tex_pass.Text,
                    Role = Tex_role.Text
                };

                if (user.UpdateUser(user))
                {
                    MessageBox.Show("✅ تم تعديل بيانات المستخدم", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("❌ فشل في تعديل بيانات المستخدم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message);
            }
        }

        private void But_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Tex_userid.Text, out int userId))
                {
                    MessageBox.Show("⚠️ حدد المستخدم للحذف");
                    return;
                }

                if (new Users().DeleteUser(userId))
                {
                    MessageBox.Show("✅ تم حذف المستخدم", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("❌ فشل في حذف المستخدم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ: " + ex.Message);
            }
        }

        private void But_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            Tex_userid.Clear();
            Tex_username.Clear();
            Tex_pass.Clear();
            Tex_role.Clear();
            dgv_user_info.ClearSelection();
        }

        private void dgv_user_info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_user_info.Rows[e.RowIndex];
                Tex_userid.Text = row.Cells["UserId"].Value.ToString();
                Tex_username.Text = row.Cells["UserName"].Value.ToString();
                Tex_pass.Text = row.Cells["Password_hash"].Value.ToString();
                Tex_role.Text = row.Cells["Role"].Value.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
