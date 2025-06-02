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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppQLthuexe
{
    public partial class Thuexe : Form
    {
        public Thuexe()
        {
            InitializeComponent();
        }
        private void Thuexe_Load(object sender, EventArgs e)
        {
            Loadtgv();
            tb_TimKH.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tb_TimKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void dtgv_showOto_DoubleClick(object sender, EventArgs e)
        {
            if (dtgv_showOto.SelectedRows.Count > 0)
            {

                int rowIndex = dtgv_showOto.CurrentCell.RowIndex;
                string MaX = dtgv_showOto.Rows[rowIndex].Cells["max"].Value.ToString();
                string TenX = dtgv_showOto.Rows[rowIndex].Cells["tenx"].Value.ToString();
                string Theloai = dtgv_showOto.Rows[rowIndex].Cells["theloai"].Value.ToString();
                string Bienso = dtgv_showOto.Rows[rowIndex].Cells["bienso"].Value.ToString();
                int dongiaxx = Convert.ToInt32(dtgv_showOto.Rows[rowIndex].Cells["dongiax"].Value.ToString());
                string DongiaX = dongiaxx.ToString("N0");
                string Trangthai = dtgv_showOto.Rows[rowIndex].Cells["trangthai"].Value.ToString();
                lb_MaX.Text = MaX;
                lb_TenX.Text = "Tên xe: " + TenX;
                lb_Bienso.Text = "Biển số: " + Bienso;
                lb_Theloai.Text = "Thể loại: " + Theloai;
                lb_Dongiax.Text = DongiaX;
                lb_Trangthai.Text = "Trạng thái " + Trangthai;
                Checktrangthaixe();
                
            }
        }

        private void btn_car5_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '5 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void btn_car7_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '7 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void btn_car16_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '16 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void btn_car29_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '29 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void btn_car35_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '35 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void btn_car45_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM XeOto WHERE Theloai = '45 chỗ'";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }

        private void Loadtgv()
        {
            string query = "SELECT * FROM XeOto";
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgv_showOto.DataSource = dt;
            }
        }
        private void tb_TimKH_TextChanged(object sender, EventArgs e)
        {
            string keyword = tb_TimKH.Text.Trim();
            if (keyword.Length >= 2)
            {
                AutoCompleteStringCollection auto = LoadAutoCompleteTenKhachHang(keyword);
                tb_TimKH.AutoCompleteCustomSource = auto;
            }
        }

        private AutoCompleteStringCollection LoadAutoCompleteTenKhachHang(string keyword)
        {
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            string[] words = keyword.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
                return auto;

            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();

                // Tạo câu truy vấn động với ILIKE cho từng từ
                string query = "SELECT Hoten, CCCD FROM KhachHang WHERE ";
                List<string> conditions = new List<string>();
                for (int i = 0; i < words.Length; i++)
                {
                    conditions.Add($"Hoten ILIKE @kw{i}");
                }
                query += string.Join(" AND ", conditions);

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                for (int i = 0; i < words.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"@kw{i}", $"%{words[i]}%");
                }

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string hoten = reader["Hoten"].ToString();
                    string cccd = reader["CCCD"].ToString();
                    auto.Add($"{hoten} - {cccd}");
                }
            }
            return auto;
        }
        private int GetMaKhachHangTuTextBox()
        {
            string selectedText = tb_TimKH.Text;
            if (string.IsNullOrWhiteSpace(selectedText) || !selectedText.Contains("-"))
                return -1;

            string cccd = selectedText.Split('-')[1].Trim();

            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string query = "SELECT MaKH FROM KhachHang WHERE CCCD = @cccd";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cccd", cccd);

                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return -1;
            }
        }
        private void Checktrangthaixe()
        {
            int MaX = int.Parse(lb_MaX.Text);
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = "SELECT trangthai FROM XeOto WHERE max = @MaX";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = MaX;
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                    string trangThai = result.ToString();

                        if (trangThai == "Đang thuê")
                        {
                            btn_Thuexe.Enabled = false;
                            btn_Traxe.Enabled = true;
                        }
                        else if(trangThai == "Bảo trì")
                        {
                            btn_Thuexe.Enabled = false;
                            btn_Traxe.Enabled = false;
                        }else
                        {
                            btn_Thuexe.Enabled = true;
                            btn_Traxe.Enabled = false;
                        }
                    }
                    else
                    {
                        btn_Thuexe.Enabled = false;
                    }
                }
                
            }
        }

        /*private void Checktrangthaixe2()
        {
            if (lb_MaX == null && lb_Dongiax == null)
            {
                btn_Thuexe.Enabled = false;
            }
            else
            {
                btn_Thuexe.Enabled = true;
            }
        }*/
        private void Clearlabel()
        {
            lb_MaX.Text = "";
            lb_TenX.Text = "Tên xe:";
            lb_Bienso.Text = "Biển số:";
            lb_Theloai.Text = "Thể loại:";
            lb_Dongiax.Text = "";
            lb_Trangthai.Text = "Trạng thái:";
            tb_Thanhtien.Text = "";
            tb_showtg.Text = "";
            cb_thuetaixe.Checked = false;
            tb_TimKH.Clear();
        }

        private void btn_Datxe_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiểm tra trạng thái xe
                        string checkSql = "SELECT trangthai FROM XeOto WHERE max = @MaX";
                        using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(lb_MaX.Text.Trim());
                            object result = checkCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("Không tìm thấy xe.");
                            }

                            string trangthaiHienTai = result.ToString();
                            if (trangthaiHienTai == "Bảo trì")
                            {
                                MessageBox.Show("Xe đang bảo trì, không thể cho thuê.");
                                return;
                            }
                        }
                        //  Cập nhật trạng thái xe
                        string sql1 = "UPDATE XeOto SET trangthai = @Trangthai WHERE max = @MaX";
                        using (var cmd = new NpgsqlCommand(sql1, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(lb_MaX.Text.Trim());
                            cmd.Parameters.AddWithValue("@Trangthai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "Đang thuê";
                            int resultUpdate = cmd.ExecuteNonQuery();
                            if (resultUpdate <= 0)
                            {
                                throw new Exception("Không tìm thấy xe để cập nhật trạng thái.");
                            }
                        }

                        //  Lấy hoặc tạo hợp đồng
                        int mahd;
                        if (cb_Thuethemxe.Checked)
                        {
                            if (string.IsNullOrEmpty(tb_showMahd.Text))
                                throw new Exception("Bạn cần nhập mã hợp đồng để thêm dữ liệu vào hợp đồng cũ.");
                                mahd = int.Parse(tb_showMahd.Text.Trim());
                        }
                        else // Nếu tạo hợp đồng mới
                        {
                            int maKH = GetMaKhachHangTuTextBox();
                            string sql2 = "INSERT INTO HopDong (makh) VALUES (@MaKH) RETURNING mahd";
                            using (var cmd = new NpgsqlCommand(sql2, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", NpgsqlTypes.NpgsqlDbType.Integer).Value = maKH;
                                object result = cmd.ExecuteScalar();
                                if (result == null)
                                    throw new Exception("Không tạo được hợp đồng.");
                                mahd = Convert.ToInt32(result);
                                tb_showMahd.Text = mahd.ToString(); // Cập nhật lại label
                            }
                        }

                        //Thêm chi tiết hợp đồng
                        string sql3 = "INSERT INTO ChiTietHD (MaHD, MaX, Thuetaixe, NgayThue, NgayTra, Dongia, Sogiothue, Thanhtien) " +
                                      "VALUES (@MaHD, @MaX, @Thuetaixe, @Ngaythue, @Ngaytra, @Dongia, @Sogiothue, @Thanhtien)";
                        string thuetaixe = cb_thuetaixe.Checked ? "Có" : "Không";
                        using (var cmd = new NpgsqlCommand(sql3, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaHD", NpgsqlTypes.NpgsqlDbType.Integer).Value = mahd;
                            cmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(lb_MaX.Text);
                            cmd.Parameters.AddWithValue("@Thuetaixe", NpgsqlTypes.NpgsqlDbType.Varchar).Value = thuetaixe;
                            cmd.Parameters.AddWithValue("@Ngaythue", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_batdau.Value;
                            cmd.Parameters.AddWithValue("@Ngaytra", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = dtp_ketthuc.Value;
                            cmd.Parameters.AddWithValue("@Dongia", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(lb_Dongiax.Text.Replace(",", "").Replace("$", ""));
                            cmd.Parameters.AddWithValue("@Sogiothue", NpgsqlTypes.NpgsqlDbType.Numeric).Value = decimal.Parse(tb_showtg.Text);
                            cmd.Parameters.AddWithValue("@Thanhtien", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Thanhtien.Text.Replace(",", "").Replace("$", ""));
                            int resultInsert = cmd.ExecuteNonQuery();
                            if (resultInsert <= 0)
                            {
                                throw new Exception("Không tạo được chi tiết hợp đồng.");
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("Thuê xe thành công và cập nhật trạng thái xe thành công!");
                        Loadtgv();
                        Clearlabel();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi trong quá trình thuê xe: " + ex.Message);
                    }
                }
            }
        }

        private void Tinhsogio() {
            DateTime batdau = dtp_batdau.Value;
            DateTime ketthuc = dtp_ketthuc.Value;
            TimeSpan khoangthoigian = ketthuc - batdau;
            Double sogio = khoangthoigian.TotalHours;
            tb_showtg.Text = sogio.ToString("0.00");
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

        private void btn_Tinhgio_Click(object sender, EventArgs e)
        {
            Tinhsogio();
            decimal soGioThue;

            if (decimal.TryParse(tb_showtg.Text, out soGioThue) && soGioThue > 0 )
            {
                string textDonGia = lb_Dongiax.Text.Replace(",", "").Replace("$", "").Trim();
                if (decimal.TryParse(textDonGia, out decimal donGiaXe))
                {
                    bool coTaiXe = cb_thuetaixe.Checked;
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
                MessageBox.Show("Vui lòng nhập số giờ thuê hợp lệ!");
            }
        }

        private void btn_Traxe_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = "UPDATE XeOto SET trangthai = @Trangthai WHERE max = @MaX";
                try
                {
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(lb_MaX.Text.Trim());
                        cmd.Parameters.AddWithValue("@Trangthai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "Trống";
                        int resultUpdate = cmd.ExecuteNonQuery();
                        if (resultUpdate <= 0)
                        {
                            throw new Exception("Không tìm thấy xe để cập nhật trạng thái.");
                        }
                        Loadtgv();
                        Clearlabel();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trong quá trình trả xe: " + ex.Message);
                }
            }
        }

    }
}
