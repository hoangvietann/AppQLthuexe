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
using static System.Net.Mime.MediaTypeNames;

namespace AppQLthuexe
{
    public partial class QLKH : Form
    {
        public QLKH()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }
        private void LoadKhachHang()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Khachhang";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgrv_KH.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTextBox()
        {
            tb_Makh.Clear();
            tb_hoten.Clear();
            tb_cccd.Clear();
            tb_diachi.Clear();
            tb_sdt.Clear();
        }
        private bool IsValidCCCD(string cccd)
        {
            return cccd.Length == 12 && cccd.All(char.IsDigit);
        }

        private bool KiemTraCCCDTrung(string cccd)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM KhachHang WHERE CCCD = @cccd";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cccd", cccd);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool IsSoDienThoaiHopLe(string sdt)
        {
            return !string.IsNullOrWhiteSpace(sdt) && sdt.All(char.IsDigit);
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    string cccd = tb_cccd.Text.Trim();
                    string sdt = tb_sdt.Text.Trim();
                    if (!IsValidCCCD(cccd))
                    {
                        MessageBox.Show("CCCD không hợp lệ! Phải gồm đúng 12 chữ số.");
                        return;
                    }
                    
                    if (KiemTraCCCDTrung(cccd))
                    {
                        MessageBox.Show("CCCD này đã tồn tại. Vui lòng nhập CCCD khác.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(tb_hoten.Text))
                    {
                        MessageBox.Show("Lỗi! Bạn chưa nhập tên khách hàng.");
                        return;
                    }
                    if (!IsSoDienThoaiHopLe(sdt))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng chỉ nhập số.");
                        return;
                    }
                    conn.Open();
                    string sql = "INSERT INTO khachhang (hoten,cccd,diachi,sdt) VALUES (@hoten,@cccd,@diachi,@sdt)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hoten", tb_hoten.Text.Trim());
                        cmd.Parameters.AddWithValue("@cccd", tb_cccd.Text.Trim());
                        cmd.Parameters.AddWithValue("@diachi", tb_diachi.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", tb_sdt.Text.Trim());
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm khách hàng thành công!");
                            LoadKhachHang();
                            LoadTextBox();
                        }
                        else
                        {
                            MessageBox.Show("Không thêm được Khách hàng.");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    string sdt = tb_sdt.Text.Trim();
                    if (!IsSoDienThoaiHopLe(sdt))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng chỉ nhập số.");
                        return;
                    }
                    conn.Open();
                    string sql = "UPDATE khachhang SET hoten = @hoten, cccd = @cccd, diachi = @diachi, sdt = @sdt WHERE makh = @makh";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@makh", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tb_Makh.Text.Trim());
                        cmd.Parameters.AddWithValue("@hoten", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_hoten.Text.Trim();
                        cmd.Parameters.AddWithValue("@cccd", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_cccd.Text.Trim();
                        cmd.Parameters.AddWithValue("@diachi", NpgsqlTypes.NpgsqlDbType.Text).Value = tb_diachi.Text.Trim();
                        cmd.Parameters.AddWithValue("@sdt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_sdt.Text.Trim();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật khách hàng thành công!");
                            LoadKhachHang();
                            LoadTextBox();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng để cập nhật.");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    DialogResult dialog = MessageBox.Show(
                        "Bạn có chắc muốn xóa khách hàng này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dialog == DialogResult.Yes)
                    {
                        string sql = "DELETE FROM khachhang WHERE makh = @makh";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@makh", Convert.ToInt32(tb_Makh.Text.Trim()));

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Xóa khách hàng thành công!");
                                LoadKhachHang();
                                LoadTextBox();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng để xóa.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
            }
        }

        private void dtgrv_KH_DoubleClick(object sender, EventArgs e)
        {
            if (dtgrv_KH.SelectedRows.Count > 0)
            {
                int rowIndex = dtgrv_KH.CurrentCell.RowIndex;
                string MaKH = dtgrv_KH.Rows[rowIndex].Cells["makh"].Value.ToString();
                string HoTen = dtgrv_KH.Rows[rowIndex].Cells["hoten"].Value.ToString();
                string CCCD = dtgrv_KH.Rows[rowIndex].Cells["cccd"].Value.ToString();
                string DiaChi = dtgrv_KH.Rows[rowIndex].Cells["diachi"].Value.ToString();
                string SDT = dtgrv_KH.Rows[rowIndex].Cells["sdt"].Value.ToString();
                tb_Makh.Text = MaKH;
                tb_hoten.Text = HoTen;
                tb_cccd.Text = CCCD;
                tb_diachi.Text = DiaChi;
                tb_sdt.Text = SDT;
            }
        }

        private void btn_Timkiem_Click(object sender, EventArgs e)
        {
            string hoTen = tb_hoten.Text.Trim();
            string cccd = tb_cccd.Text.Trim();
            string diachi = tb_diachi.Text.Trim();
            string sdt = tb_sdt.Text.Trim();
            string sql = "SELECT * FROM KhachHang WHERE 1=1";

            if (!string.IsNullOrEmpty(hoTen))
                sql += " AND Hoten ILIKE @Hoten";
            if (!string.IsNullOrEmpty(cccd))
                sql += " AND CCCD ILIKE @CCCD";
            if (!string.IsNullOrEmpty(diachi))
                sql += " AND Diachi ILIKE @Diachi";
            if (!string.IsNullOrEmpty(sdt))
                sql += " AND Sdt ILIKE @Sdt";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(hoTen))
                        cmd.Parameters.AddWithValue("@Hoten", "%" + hoTen + "%");
                    if (!string.IsNullOrEmpty(cccd))
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                    if (!string.IsNullOrEmpty(diachi))
                        cmd.Parameters.AddWithValue("@Diachi", "%" + diachi + "%");
                    if (!string.IsNullOrEmpty(sdt))
                        cmd.Parameters.AddWithValue("@Sdt", "%" + sdt + "%");
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtgrv_KH.DataSource = dt;
                }
            }
        }

    }
}
