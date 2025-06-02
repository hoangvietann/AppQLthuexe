using Npgsql;
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
    public partial class QLTaikhoan : Form
    {
        public QLTaikhoan()
        {
            InitializeComponent();
            Loaddgv_showTK();
        }

        private void Loaddgv_showTK()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Taikhoan";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgv_showTK.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cleartextbox()
        {
            txt_TK.Clear();
            txt_MK.Clear();
            cb_LoaiTK.SelectedIndex = 0;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = "INSERT INTO TaiKhoan (TenTK, Matkhau, LoaiTK) VALUES (@tentk, @matkhau, @loaitk)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tentk", txt_TK.Text);
                    cmd.Parameters.AddWithValue("@matkhau", txt_MK.Text);
                    cmd.Parameters.AddWithValue("@loaitk", cb_LoaiTK.Text);

                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm tài khoản thành công.");
                        Loaddgv_showTK();
                        Cleartextbox();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = "UPDATE TaiKhoan SET Matkhau = @matkhau, LoaiTK = @loaitk WHERE TenTK = @tentk";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tentk", txt_TK.Text);
                    cmd.Parameters.AddWithValue("@matkhau", txt_MK.Text);
                    cmd.Parameters.AddWithValue("@loaitk", cb_LoaiTK.Text);

                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        Loaddgv_showTK();
                        Cleartextbox();
                        if (result > 0)
                            MessageBox.Show("Cập nhật tài khoản thành công.");
                        else
                            MessageBox.Show("Không tìm thấy tài khoản để cập nhật.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string sql = "DELETE FROM TaiKhoan WHERE TenTK = @tentk";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tentk", txt_TK.Text);

                        try
                        {
                            int result = cmd.ExecuteNonQuery();
                            Loaddgv_showTK();
                            Cleartextbox();
                            if (result > 0)
                                MessageBox.Show("Xóa tài khoản thành công.");
                            else
                                MessageBox.Show("Không tìm thấy tài khoản để xóa.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void dtgv_showTK_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_showTK.SelectedRows.Count > 0)
            {
                int rowIndex = dtgv_showTK.CurrentCell.RowIndex;
                string TenTK = dtgv_showTK.Rows[rowIndex].Cells["tentk"].Value.ToString();
                string MK = dtgv_showTK.Rows[rowIndex].Cells["matkhau"].Value.ToString();
                string LoaiTK = dtgv_showTK.Rows[rowIndex].Cells["loaitk"].Value.ToString();
                txt_TK.Text = TenTK;
                txt_MK.Text = MK;
                cb_LoaiTK.Text = LoaiTK;
            }
        }


    }
}
