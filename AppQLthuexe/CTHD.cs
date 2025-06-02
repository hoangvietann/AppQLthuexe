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
    public partial class CTHD : Form
    {
        public CTHD()
        {
            InitializeComponent();
        }

        private void CTHD_Load(object sender, EventArgs e)
        {
            LoaddtgvHD();
            LoaddtgvCTHD();
        }

        private void LoaddtgvHD()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string query = "SELECT hd.MaHD, hd.MaKH, kh.Hoten AS TenKhachHang, hd.NgayLap, hd.Tongtien FROM HopDong hd JOIN KhachHang kh ON hd.MaKH = kh.MaKH;";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgv_showhd.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoaddtgvCTHD()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ChiTietHD";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgv_showcthd.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgv_showhd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_showhd.SelectedRows.Count > 0)
            {
                int rowIndex = dtgv_showhd.CurrentCell.RowIndex;
                string MaHD = dtgv_showhd.Rows[rowIndex].Cells["mahd"].Value.ToString();
                string MaKH = dtgv_showhd.Rows[rowIndex].Cells["makh"].Value.ToString();
                string Ngaylap = dtgv_showhd.Rows[rowIndex].Cells["ngaylap"].Value.ToString();
                int tongtien = Convert.ToInt32(dtgv_showhd.Rows[rowIndex].Cells["tongtien"].Value.ToString());
                string Tongtien = tongtien.ToString("N0");
                tb_MaHD.Text = MaHD;
                tb_MaKH.Text = MaKH;
                dtp_Ngaylap.Text = Ngaylap;
                tb_Tongtien.Text = Tongtien;
                if (e.RowIndex >= 0)
                {
                    try
                    {
                        DataGridViewRow row = dtgv_showhd.Rows[e.RowIndex];
                        string maHD = row.Cells["mahd"].Value?.ToString();
                        if (!string.IsNullOrEmpty(maHD))
                        {
                            LoadChiTietHopDong(maHD);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi chọn hợp đồng: " + ex.Message);
                    }
                }
            }
        }

        private void dtgv_showcthd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_showcthd.SelectedRows.Count > 0)
            {
                int rowIndex = dtgv_showcthd.CurrentCell.RowIndex;
                string MaCTHD = dtgv_showcthd.Rows[rowIndex].Cells["macthd"].Value.ToString();
                string MaHD = dtgv_showcthd.Rows[rowIndex].Cells["mahd"].Value.ToString();
                string MaX = dtgv_showcthd.Rows[rowIndex].Cells["max"].Value.ToString();
                string Thuetaixe = dtgv_showcthd.Rows[rowIndex].Cells["thuetaixe"].Value.ToString();
                string Ngaythue = dtgv_showcthd.Rows[rowIndex].Cells["ngaythue"].Value.ToString();
                string Ngaytra = dtgv_showcthd.Rows[rowIndex].Cells["ngaytra"].Value.ToString();
                int dongiax = Convert.ToInt32(dtgv_showcthd.Rows[rowIndex].Cells["dongia"].Value.ToString());
                string Dongiax = dongiax.ToString("N0");
                string Sogiothue = dtgv_showcthd.Rows[rowIndex].Cells["sogiothue"].Value.ToString();
                int thanhtien = Convert.ToInt32(dtgv_showcthd.Rows[rowIndex].Cells["thanhtien"].Value.ToString());
                string Thanhtien = thanhtien.ToString("N0");
                tb_Macthd.Text = MaCTHD;
                tb_MaHDCT.Text = MaHD;
                tb_MaX.Text = MaX;
                tb_Dongiax.Text = Dongiax;
                cb_Thuetaixe.Checked = (Thuetaixe == "Có");
                dtp_NgayThue.Text = Ngaythue;
                dtp_Ngaytra.Text = Ngaytra;
                tb_Sogiothue.Text = Sogiothue;
                tb_Thanhtien.Text = Thanhtien;
                
            }
        }

        private void LoadChiTietHopDong(String mahd)
        {
            try
            {
                string sql = " SELECT * FROM ChiTietHD  WHERE MaHD = @mahd";

                using (var conn = new NpgsqlConnection(NpgConfig.connString))
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mahd", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(mahd));
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dtgv_showcthd.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hợp đồng: " + ex.Message);
            }

        }

        /*private void btn_ThemHD_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = "INSERT INTO HopDong (MaKH, Ngaylap, Tongtien) VALUES (@makh, @Ngaylap, @tongtien)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@makh", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tb_MaKH.Text);
                        cmd.Parameters.Add("@Ngaylap", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_Ngaylap.Value;
                        cmd.Parameters.Add("@tongtien", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Tongtien.Text.Replace(",", "").Replace("$", "").Trim());
                        cmd.ExecuteNonQuery();
                        LoaddtgvHD();
                    }
                    MessageBox.Show("Thêm hợp đồng thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hợp đồng: " + ex.Message);
            }
        }*/

        private void btn_SuaHD_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string sql = "UPDATE HopDong SET MaKH = @makh, Ngaylap = @Ngaylap, Tongtien = @tongtien WHERE MaHD = @mahd";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@makh", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tb_MaKH.Text);
                        cmd.Parameters.Add("@Ngaylap", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_Ngaylap.Value;
                        cmd.Parameters.Add("@tongtien", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Tongtien.Text.Replace(",", "").Replace("$", "").Trim());
                        cmd.Parameters.Add("@mahd", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tb_MaHD.Text);
                        int rows = cmd.ExecuteNonQuery();
                        LoaddtgvHD();
                        MessageBox.Show(rows > 0 ? "Cập nhật thành công" : "Không tìm thấy hợp đồng");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hợp đồng: " + ex.Message);
            }
        }

        private void btn_XoaHD_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa hợp đồng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm != DialogResult.Yes)
                        return;

                    conn.Open();

                    int mahd = int.Parse(tb_MaHD.Text);

                    // Bắt đầu transaction để đảm bảo tính toàn vẹn
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // Xóa ChiTietHD trước
                            string sqlCT = "DELETE FROM ChiTietHD WHERE MaHD = @mahd";
                            using (var cmdCT = new NpgsqlCommand(sqlCT, conn))
                            {
                                cmdCT.Parameters.AddWithValue("@mahd", mahd);
                                cmdCT.ExecuteNonQuery();
                            }

                            // Sau đó xóa HopDong
                            string sqlHD = "DELETE FROM HopDong WHERE MaHD = @mahd";
                            using (var cmdHD = new NpgsqlCommand(sqlHD, conn))
                            {
                                cmdHD.Parameters.AddWithValue("@mahd", mahd);
                                int rows = cmdHD.ExecuteNonQuery();

                                trans.Commit(); // xác nhận xóa cả 2

                                LoaddtgvHD();
                                MessageBox.Show(rows > 0 ? "Xóa hợp đồng và chi tiết thành công." : "Không tìm thấy hợp đồng.");
                            }
                        }
                        catch (Exception exInner)
                        {
                            trans.Rollback();
                            MessageBox.Show("Lỗi khi xóa: " + exInner.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        /*private void btn_ThemCT_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"
                INSERT INTO ChiTietHD (MaHD, MaX, Thuetaixe, NgayThue, NgayTra, Dongia, Sogiothue, Thanhtien)
                VALUES (@mahd, @max, @thuetaixe, @ngaythue, @ngaytra, @dongia, @sogiothue, @thanhtien)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@mahd", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_MaHDCT.Text));
                        cmd.Parameters.AddWithValue("@max", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_MaX.Text));
                        cmd.Parameters.AddWithValue("@thuetaixe", cb_Thuetaixe.Checked ? "Có" : "Không");
                        cmd.Parameters.AddWithValue("@ngaythue", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_NgayThue.Value;
                        cmd.Parameters.AddWithValue("@ngaytra", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_Ngaytra.Value;
                        cmd.Parameters.AddWithValue("@dongia", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Dongiax.Text.Replace(",", "").Replace("$", "").Trim());
                        cmd.Parameters.AddWithValue("@sogiothue", NpgsqlTypes.NpgsqlDbType.Numeric).Value = decimal.Parse(tb_Sogiothue.Text);
                        cmd.Parameters.AddWithValue("@thanhtien", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Thanhtien.Text.Replace(",", "").Replace("$", "").Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm chi tiết hợp đồng thành công!");
                    }
                }
                LoadChiTietHopDong(tb_MaHDCT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }*/

        private void btn_SuaCT_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string sql = @"
                UPDATE ChiTietHD 
                SET MaHD = @mahd, MaX = @max, Thuetaixe = @thuetaixe, 
                    NgayThue = @ngaythue, NgayTra = @ngaytra, 
                    Dongia = @dongia, Sogiothue = @sogiothue, Thanhtien = @thanhtien
                WHERE MaCTHD = @macthd";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@macthd", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_Macthd.Text));
                        cmd.Parameters.AddWithValue("@mahd", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_MaHDCT.Text));
                        cmd.Parameters.AddWithValue("@max", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_MaX.Text));
                        cmd.Parameters.AddWithValue("@thuetaixe", cb_Thuetaixe.Checked ? "Có" : "Không");
                        cmd.Parameters.AddWithValue("@ngaythue", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_NgayThue.Value;
                        cmd.Parameters.AddWithValue("@ngaytra", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_Ngaytra.Value;
                        cmd.Parameters.AddWithValue("@dongia", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(tb_Dongiax.Text.Replace(",", "").Replace("$", "").Trim()));
                        cmd.Parameters.AddWithValue("@sogiothue", NpgsqlTypes.NpgsqlDbType.Numeric, decimal.Parse(tb_Sogiothue.Text));
                        cmd.Parameters.AddWithValue("@thanhtien", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(tb_Thanhtien.Text.Replace(",", "").Replace("$", "").Trim()));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!");
                    }
                }
                LoadChiTietHopDong(tb_MaHDCT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btn_XoaCT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(NpgConfig.connString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM ChiTietHD WHERE MaCTHD = @macthd";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@macthd", NpgsqlTypes.NpgsqlDbType.Integer, int.Parse(tb_Macthd.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa thành công!");
                        }
                    }
                    LoadChiTietHopDong(tb_MaHDCT.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void Tinhsogio()
        {
            DateTime batdau = dtp_NgayThue.Value;
            DateTime ketthuc = dtp_Ngaytra.Value;
            TimeSpan khoangthoigian = ketthuc - batdau;
            Double sogio = khoangthoigian.TotalHours;
            tb_Sogiothue.Text = sogio.ToString("0.00");
        }
        public decimal TinhThanhTien(decimal soGioThue, bool coTaiXe, decimal donGiaXe)
        {
            decimal thanhTien = soGioThue * donGiaXe;


            if (coTaiXe)
            {
                thanhTien *= 1.05m;
            }

            return thanhTien;
        }
        private void btn_Tinhtien_Click(object sender, EventArgs e)
        {
            Tinhsogio();
            decimal soGioThue;

            if (decimal.TryParse(tb_Sogiothue.Text, out soGioThue) && soGioThue > 0)
            {
                string textDonGia = tb_Dongiax.Text.Replace(",", "").Replace("$", "").Trim();
                if (decimal.TryParse(textDonGia, out decimal donGiaXe))
                {
                    bool coTaiXe = cb_Thuetaixe.Checked;
                    decimal thanhTien = TinhThanhTien(soGioThue, coTaiXe, donGiaXe);
                    tb_Thanhtien.Text = thanhTien.ToString("N0");
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn xe để thuê!");
                }

            }
            else
            {
                MessageBox.Show("Số giờ thuê chưa hợp lệ!");
            }
        }

        private void btn_Timkiem_Click(object sender, EventArgs e)
        {
            string keyword = tb_MaKH.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập tên hoặc CCCD của khách hàng.");
                return;
            }

            int makh = -1;
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();

                // Bước 1: Tìm MaKH từ KhachHang theo Hoten hoặc CCCD
                string queryKH = "SELECT MaKH FROM KhachHang WHERE Hoten ILIKE @kw OR CCCD ILIKE @kw";
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryKH, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        makh = Convert.ToInt32(result);
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng.");
                        return;
                    }
                }

                // Bước 2: Tìm hợp đồng từ HopDong theo MaKH và ngày lập (tùy chọn)
                string queryHD;
                NpgsqlCommand cmd2;

                if (dtp_Ngaylap.Checked)  // Người dùng có chọn ngày
                {
                    queryHD = "SELECT * FROM HopDong WHERE MaKH = @makh AND DATE(NgayLap) = @ngay";
                    cmd2 = new NpgsqlCommand(queryHD, conn);
                    cmd2.Parameters.AddWithValue("@makh", makh);
                    cmd2.Parameters.AddWithValue("@ngay", dtp_Ngaylap.Value.Date);
                }
                else  // Người dùng không chọn ngày => không lọc theo ngày
                {
                    queryHD = "SELECT * FROM HopDong WHERE MaKH = @makh";
                    cmd2 = new NpgsqlCommand(queryHD, conn);
                    cmd2.Parameters.AddWithValue("@makh", makh);
                }

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd2))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtgv_showhd.DataSource = dt;
                }
            }
        }
    }
}
