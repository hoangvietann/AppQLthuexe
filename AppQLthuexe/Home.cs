using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLthuexe
{
    public partial class Home : Form
    {
        private string userRole;
        public Home(string role)
        {
            InitializeComponent();
            this.Load += new EventHandler(Home_Load);
            userRole = role;
            PhanquyenTK();
        }
        private void PhanquyenTK()
        {
            if (userRole == "Nhân viên")
            {
                btn_BCTK.Enabled = false;
                btn_QLXE.Enabled = false;
                btn_QLTK.Enabled = false;
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ShowFormInPanel(new Thuexe());
        }
        private void ShowFormInPanel(Form form)
        {
            panel_hien.Controls.Clear();  // Xóa các form cũ trong panel
            form.TopLevel = false;  // Đặt form con không phải cửa sổ độc lập
            form.FormBorderStyle = FormBorderStyle.None;  // ẩn viền form
            form.Dock = DockStyle.Fill;  // Fill đầy Panel
            panel_hien.Controls.Add(form);  // thêm form con vào panel
            panel_hien.Tag = form;
            form.BringToFront();  // đưa form lên trên
            form.Show();  // hiển thị form
        }

        private void btn_KH_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new QLKH());
        }
        private void btn_QLXE_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new QLXE());
        }

        private void btn_exitLogin_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?","Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                foreach (Form f in Application.OpenForms.OfType<Form>().ToList())
                {
                    if (!(f is Login)) // Giữ lại LoginForm nếu bạn mở lại nó
                        f.Close();
                }
                // Hiển thị lại Form đăng nhập
                Login login = new Login();
                login.Show();
            }/*this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();*/
        }

        private void btn_Thuexe_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Thuexe());
        }

        private void btn_HD_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new CTHD());
        }

        private void btn_BCTK_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new BaocaoTK());
        }

        private void btn_QLTK_Click(object sender, EventArgs e)
        {
            QLTaikhoan frm = new QLTaikhoan();
            frm.Show();
        }
    }
}
