using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLthuexe
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btn_Login;
            tb_MK.UseSystemPasswordChar = true;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = tb_TTK.Text.Trim();
            string password = tb_MK.Text.Trim();

            string query = "SELECT * FROM TaiKhoan WHERE TenTK = @username AND MatKhau = @password";

            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {

                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string role = reader["LoaiTK"].ToString();
                        Home home = new Home(role);
                        this.Hide();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!");
                    }
                }
            }
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void cb_HienMK_CheckedChanged(object sender, EventArgs e)
        {
  
            tb_MK.UseSystemPasswordChar = !cb_HienMK.Checked;
            
        }
    }
}
