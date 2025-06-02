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
using System.Windows.Forms.DataVisualization.Charting;

namespace AppQLthuexe
{
    public partial class BaocaoTK : Form
    {
        public BaocaoTK()
        {
            InitializeComponent();
            LoadBieuDoDoanhThu();
            LoadDoanhthuloaixe();
            LoadThongKeTatCaKhachHang();
            /*LoadBieuDoDoanhThuTheoLoaiXe();*/
        }
        private void LoadBieuDoDoanhThu()
        {
            chartDoanhthu.Series.Clear();
            chartDoanhthu.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("DoanhThuTheoThang");
            chartDoanhthu.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true; // Hiện giá trị trên cột
            chartDoanhthu.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhthu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthu.Series.Add(series);

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        EXTRACT(MONTH FROM hd.ngaylap) AS thang,
                        SUM(hd.tongtien) AS doanhthu
                    FROM HopDong hd
                    GROUP BY thang
                    ORDER BY thang;
                ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string thang = "Tháng " + reader.GetInt32(0).ToString();
                        double doanhThu = Convert.ToDouble(reader.GetDecimal(1));

                        series.Points.AddXY(thang, doanhThu);
                    }
                }
            }
        }

        private void btn_Xemnam_Click(object sender, EventArgs e)
        {
            chartDoanhthu.Series.Clear();
            chartDoanhthu.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("DoanhThuTheoNam");
            chartDoanhthu.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.Int32;
            series.IsValueShownAsLabel = true;
            chartDoanhthu.ChartAreas[0].AxisX.Title = "Năm";
            chartDoanhthu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthu.Series.Add(series);

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
        SELECT 
            EXTRACT(YEAR FROM ngaylap) AS nam,
            SUM(tongtien) AS doanhthu
        FROM hopdong
        GROUP BY nam
        ORDER BY nam;
    ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int nam = (int)reader.GetDouble(0); // EXTRACT trả về double
                        double doanhThu = Convert.ToDouble(reader.GetDecimal(1));
                        int idx = series.Points.AddXY(nam, doanhThu);
                        series.Points[idx].Label = doanhThu.ToString("#,##0");
                    }
                }
            }
        }

        private void btn_Xemthang_Click(object sender, EventArgs e)
        {
            chartDoanhthu.Series.Clear();
            chartDoanhthu.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("DoanhThuTheoThang");
            chartDoanhthu.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true; // Hiện giá trị trên cột
            chartDoanhthu.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhthu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthu.Series.Add(series);

            // Lấy năm từ ComboBox
            int nam = int.Parse(cb_Nam.SelectedItem.ToString());

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
        SELECT 
            EXTRACT(MONTH FROM hd.ngaylap) AS thang,
            SUM(hd.tongtien) AS doanhthu
        FROM HopDong hd
        WHERE EXTRACT(YEAR FROM hd.ngaylap) = @nam
        GROUP BY thang
        ORDER BY thang;
    ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nam", nam);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string thang = "Tháng " + reader.GetInt32(0).ToString();
                            double doanhThu = Convert.ToDouble(reader.GetDecimal(1));
                            int idx = series.Points.AddXY(thang, doanhThu);
                            series.Points[idx].Label = doanhThu.ToString("#,##0");
                        }
                    }
                }
            }
        }

        private void btn_Xemngay_Click(object sender, EventArgs e)
        {
            chartDoanhthu.Series.Clear();
            chartDoanhthu.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("DoanhThuTheoNgay");
            chartDoanhthu.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true;
            chartDoanhthu.ChartAreas[0].AxisX.Title = "Ngày";
            chartDoanhthu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthu.Series.Add(series);

            // Kiểm tra dữ liệu nhập
            if (!int.TryParse(cb_Thang.SelectedItem?.ToString(), out int thang) ||
                !int.TryParse(cb_Nam.SelectedItem?.ToString(), out int nam))
            {
                MessageBox.Show("Vui lòng chọn tháng và năm hợp lệ.");
                return;
            }

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
        SELECT 
            DATE(ngaylap) AS ngay,
            SUM(tongtien) AS doanhthu
        FROM hopdong
        WHERE EXTRACT(MONTH FROM ngaylap) = @thang
          AND EXTRACT(YEAR FROM ngaylap) = @nam
        GROUP BY ngay
        ORDER BY ngay;
    ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@thang", thang);
                    cmd.Parameters.AddWithValue("@nam", nam);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ngay = Convert.ToDateTime(reader.GetDateTime(0)).ToString("dd/MM");
                            double doanhThu = Convert.ToDouble(reader.GetDecimal(1));
                            int idx = series.Points.AddXY(ngay, doanhThu);
                            series.Points[idx].Label = doanhThu.ToString("#,##0");
                        }
                    }
                }
            }
        }

        private void LoadDoanhthuloaixe()
        {
            chartDoanhthuxe.Series.Clear();
            chartDoanhthuxe.ChartAreas.Clear();

            // Tạo vùng biểu đồ
            ChartArea chartArea = new ChartArea("DoanhThuTheoLoaiXe");
            chartDoanhthuxe.ChartAreas.Add(chartArea); // Đúng tên chart

            // Tạo series dữ liệu
            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true; // Hiện giá trị trên cột

            // Tiêu đề trục
            chartDoanhthuxe.ChartAreas[0].AxisX.Title = "Loại xe (số chỗ ngồi)";
            chartDoanhthuxe.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthuxe.Series.Add(series);

            // Kết nối CSDL và lấy dữ liệu
            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();

                string sql = @"
            SELECT x.theloai, SUM(ct.thanhtien) AS doanhthu
            FROM chitiethd ct
            JOIN xeoto x ON ct.max = x.max
            GROUP BY x.theloai
            ORDER BY doanhthu DESC;
        ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string theloai = reader.GetString(0); // ví dụ: "5 chỗ", "16 chỗ",...
                        decimal doanhthu = reader.GetDecimal(1);

                        // Thêm dữ liệu và hiển thị label có dấu phân cách
                        int pointIndex = series.Points.AddXY(theloai, doanhthu);
                        series.Points[pointIndex].Label = string.Format("{0:N0} VNĐ", doanhthu);
                    }
                }
            }
        }

        private void LoadThongKeTatCaKhachHang()
        {
            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY COALESCE(SUM(ct.Thanhtien), 0) DESC) AS STT,
                    kh.MaKH,
                    kh.Hoten,
                    COUNT(ct.MaCTHD) AS SoLanThue,
                    COALESCE(SUM(ct.Sogiothue), 0) AS TongGioThue,
                    COALESCE(SUM(ct.Thanhtien), 0) AS TongTien
                FROM KhachHang kh
                LEFT JOIN HopDong hd ON kh.MaKH = hd.MaKH
                LEFT JOIN ChiTietHD ct ON ct.MaHD = hd.MaHD
                GROUP BY kh.MaKH, kh.Hoten
                ORDER BY TongTien DESC;
                ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtgv_Showdtkh.DataSource = dt;
                }
            }
        }

        private void btn_TheoNam_Click(object sender, EventArgs e)
        {
            if (cb_NamXe.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm");
                return;
            }

            int nam = int.Parse(cb_NamXe.SelectedItem.ToString());
            LoadDoanhThuTheoLoaiXeTheoNam(nam);
        }

        private void LoadDoanhThuTheoLoaiXeTheoNam(int nam)
        {
            chartDoanhthuxe.Series.Clear();
            chartDoanhthuxe.ChartAreas.Clear();

            ChartArea area = new ChartArea("DoanhThuLoaiXe");
            chartDoanhthuxe.ChartAreas.Add(area);

            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            chartDoanhthuxe.Series.Add(series);

            // Reset DataGridView
            dtgv_showdtloaixe.Rows.Clear();
            dtgv_showdtloaixe.Columns.Clear();

            // Thiết lập cột cho DataGridView (nếu chưa thiết lập trong Designer)
            dtgv_showdtloaixe.Columns.Add("LoaiXe", "Loại xe");
            dtgv_showdtloaixe.Columns.Add("SoLanThue", "Số lần thuê");
            dtgv_showdtloaixe.Columns.Add("TongGioThue", "Tổng giờ thuê");
            dtgv_showdtloaixe.Columns.Add("DoanhThu", "Doanh thu (VNĐ)");

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();

                string sql = @"
            SELECT 
                x.theloai, 
                COUNT(cthd.macthd) AS SoLanThue,
                COALESCE(SUM(cthd.sogiothue), 0) AS TongGioThue,
                COALESCE(SUM(cthd.thanhtien), 0) AS DoanhThu
            FROM chitiethd cthd
            JOIN xeoto x ON cthd.max = x.max
            JOIN hopdong hd ON cthd.mahd = hd.mahd
            WHERE EXTRACT(YEAR FROM hd.ngaylap) = @nam
            GROUP BY x.theloai
            ORDER BY DoanhThu DESC;
        ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nam", nam);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string theloai = reader.GetString(0);
                            int soLanThue = reader.GetInt32(1);
                            double tongGioThue = reader.GetDouble(2);
                            long doanhThu = Convert.ToInt64(reader.GetDecimal(3));

                            // Thêm dữ liệu vào DataGridView
                            dtgv_showdtloaixe.Rows.Add(
                                theloai,
                                soLanThue,
                                tongGioThue.ToString("F2"),
                                doanhThu.ToString("N0")
                            );

                            // Thêm điểm vào biểu đồ
                            int idx = series.Points.AddXY(theloai, doanhThu);
                            series.Points[idx].Label = doanhThu.ToString("N0");
                        }
                    }
                }
            }
        }

        private void btn_DTthang_Click(object sender, EventArgs e)
        {
            if (cb_NamXe.SelectedItem == null || cb_ThangXe.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn cả năm và tháng.");
                return;
            }

            int nam = int.Parse(cb_NamXe.SelectedItem.ToString());
            int thang = int.Parse(cb_ThangXe.SelectedItem.ToString());

            LoadThongKeLoaiXeTheoThangNam(nam, thang);
        }
        private void LoadThongKeLoaiXeTheoThangNam(int nam, int thang)
        {
            chartDoanhthuxe.Series.Clear();
            chartDoanhthuxe.ChartAreas.Clear();
            chartDoanhthuxe.ChartAreas.Add(new ChartArea("DoanhThuLoaiXe"));

            dtgv_showdtloaixe.Columns.Clear();
            dtgv_showdtloaixe.Rows.Clear();

            dtgv_showdtloaixe.Columns.Add("LoaiXe", "Loại xe");
            dtgv_showdtloaixe.Columns.Add("SoLanThue", "Số lần thuê");
            dtgv_showdtloaixe.Columns.Add("TongGioThue", "Tổng giờ thuê");
            dtgv_showdtloaixe.Columns.Add("DoanhThu", "Doanh thu (VNĐ)");

            using (var conn = new NpgsqlConnection(NpgConfig.connString))
            {
                conn.Open();
                string sql = @"
            SELECT 
                x.theloai,
                COUNT(cthd.macthd) AS solanthue,
                COALESCE(SUM(cthd.sogiothue), 0) AS tonggio,
                COALESCE(SUM(cthd.thanhtien), 0) AS doanhthu
            FROM chitiethd cthd
            JOIN xeoto x ON x.max = cthd.max
            JOIN hopdong hd ON hd.mahd = cthd.mahd
            WHERE EXTRACT(YEAR FROM hd.ngaylap) = @nam AND EXTRACT(MONTH FROM hd.ngaylap) = @thang
            GROUP BY x.theloai
            ORDER BY doanhthu DESC;
        ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nam", nam);
                    cmd.Parameters.AddWithValue("thang", thang);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Series series = new Series("Doanh thu theo loại xe");
                        series.ChartType = SeriesChartType.Column;
                        series.IsValueShownAsLabel = true;

                        while (reader.Read())
                        {
                            string theloai = reader.GetString(0);
                            int solan = reader.GetInt32(1);
                            double tonggio = reader.GetDouble(2);
                            long doanhthu = Convert.ToInt64(reader.GetDecimal(3));

                            dtgv_showdtloaixe.Rows.Add(theloai, solan, tonggio.ToString("F2"), doanhthu.ToString("N0"));

                            int idx = series.Points.AddXY(theloai, doanhthu);
                            series.Points[idx].Label = doanhthu.ToString("N0");
                        }

                        chartDoanhthuxe.Series.Add(series);
                    }
                }
            }
        }


        /*private void ThongKeThueXe()
{
using (var conn = new NpgsqlConnection(connString))
{
conn.Open();

string sql = @"SELECT COUNT(*) AS SoLanThue, SUM(Sogiothue) AS TongGioThue FROM ChiTietHD;";

using (var cmd = new NpgsqlCommand(sql, conn))
using (var reader = cmd.ExecuteReader())
{
  if (reader.Read())
  {
      int soLanThue = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
      decimal tongGio = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);

      lblSoLanThue.Text = $"Số lần thuê: {soLanThue:N0} lần";
      lblTongGioThue.Text = $"Tổng thời gian thuê: {tongGio:N2} giờ";
  }
}
}
}*/

        /*private void LoadBieuDoDoanhThuTheoLoaiXe()
        {
            chartDoanhthuxe.Series.Clear();
            chartDoanhthuxe.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("DoanhThuTheoLoaiXe");
            chartDoanhthuxe.ChartAreas.Add(chartArea);

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true; // Hiện giá trị trên cột
            chartDoanhthuxe.ChartAreas[0].AxisX.Title = "Loại xe";
            chartDoanhthuxe.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhthuxe.Series.Add(series);

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = @" SELECT x.theloai, SUM(cthd.thanhtien) AS doanhthu FROM chitiethd cthd JOIN xeoto x ON x.max = cthd.max GROUP BY x.theloai ORDER BY doanhthu DESC; ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string loaiXe = reader.GetString(0); // theloai
                        double doanhThu = Convert.ToDouble(reader.GetDecimal(1));

                        series.Points.AddXY(loaiXe, doanhThu);
                    }
                }
            }
        }*/
    }
}
