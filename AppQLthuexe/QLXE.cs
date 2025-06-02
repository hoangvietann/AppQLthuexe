using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLthuexe
{
    public partial class QLXE : Form
    {
        public QLXE()
        {
            InitializeComponent();
        }

        private void QLXE_Load(object sender, EventArgs e)
        {
            LoadDTGV();
        }

        private void LoadDTGV()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM XeOto";
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgv_ShowXe.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Loadtextbox()
        {
            tb_MaX.Clear();
            tb_TenXe.Clear();
            tb_theloai.Clear();
            tb_Dongia.Clear();
            cb_Trangthai.Text = "";
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    string dongiaStr = tb_Dongia.Text.Replace(",", "").Replace("$", "").Trim();
                    if (string.IsNullOrWhiteSpace(dongiaStr))
                    {
                        MessageBox.Show("Vui lòng nhập đơn giá.");
                        return;
                    }

                    // Thử chuyển đổi chuỗi sang số nguyên
                    if (!int.TryParse(dongiaStr, out int dongia))
                    {
                        MessageBox.Show("Đơn giá không hợp lệ. Vui lòng nhập số.");
                        return;
                    }
                    conn.Open();
                    string sql = "INSERT INTO XeOto (tenx, theloai, bienso, dongiax, trangthai) VALUES (@tenx, @theloai, @bienso, @dongiax, @trangthai)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenx", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_TenXe.Text.Trim();
                        cmd.Parameters.AddWithValue("@theloai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_theloai.Text.Trim();
                        cmd.Parameters.AddWithValue("@bienso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_Biensoxe.Text.Trim();
                        cmd.Parameters.AddWithValue("@dongiax", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Dongia.Text.Replace(",", "").Replace("$", "").Trim());
                        cmd.Parameters.AddWithValue("@trangthai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "Trống";
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm xe thành công!");
                            LoadDTGV();
                            Loadtextbox();
                        }
                        else
                        {
                            MessageBox.Show("Không thêm được xe.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm xe: " + ex.Message);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(NpgConfig.connString))
                {
                    string dongiaStr = tb_Dongia.Text.Replace(",", "").Replace("$", "").Trim();
                    if (string.IsNullOrWhiteSpace(dongiaStr))
                    {
                        MessageBox.Show("Vui lòng nhập đơn giá.");
                        return;
                    }

                    // Thử chuyển đổi chuỗi sang số nguyên
                    if (!int.TryParse(dongiaStr, out int dongia))
                    {
                        MessageBox.Show("Đơn giá không hợp lệ. Vui lòng nhập số.");
                        return;
                    }
                    conn.Open();
                    string sql = "UPDATE XeOto SET tenx = @TenX, theloai = @Theloai, bienso = @Bienso, dongiax = @Dongiax, trangthai = @Trangthai WHERE max = @MaX";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaX", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tb_MaX.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tenx", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_TenXe.Text.Trim();
                        cmd.Parameters.AddWithValue("@Theloai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tb_theloai.Text.Trim();
                        cmd.Parameters.AddWithValue("@Bienso", NpgsqlTypes.NpgsqlDbType.Text).Value = tb_Biensoxe.Text.Trim();
                        cmd.Parameters.AddWithValue("@Dongiax", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(tb_Dongia.Text.Replace(",", "").Replace("$", "").Trim());
                        cmd.Parameters.AddWithValue("@Trangthai", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb_Trangthai.Text.Trim();
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật xe thành công!");
                            LoadDTGV();
                            Loadtextbox();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy xe để cập nhật.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật xe: " + ex.Message);
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
                        "Bạn có chắc muốn xóa xe này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dialog == DialogResult.Yes)
                    {
                        string sql = "DELETE FROM XeOto WHERE max = @MaX";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaX", int.Parse(tb_MaX.Text.Trim()));

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Xóa xe thành công!");
                                LoadDTGV();
                                Loadtextbox();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy xe để xóa.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa xe: " + ex.Message);
            }
        }

        private void dtgv_ShowXe_DoubleClick(object sender, EventArgs e)
        {
            if (dtgv_ShowXe.SelectedRows.Count > 0)
            {
                int rowIndex = dtgv_ShowXe.CurrentCell.RowIndex;
                string MaX = dtgv_ShowXe.Rows[rowIndex].Cells["max"].Value.ToString();
                string TenX = dtgv_ShowXe.Rows[rowIndex].Cells["tenx"].Value.ToString();
                string Theloai = dtgv_ShowXe.Rows[rowIndex].Cells["theloai"].Value.ToString();
                string Bienso = dtgv_ShowXe.Rows[rowIndex].Cells["bienso"].Value.ToString();
                int dongiaValue = Convert.ToInt32(dtgv_ShowXe.Rows[rowIndex].Cells["dongiax"].Value.ToString());
                string Dongia = dongiaValue.ToString("N0");
                string Trangthai = dtgv_ShowXe.Rows[rowIndex].Cells["trangthai"].Value.ToString();
                tb_MaX.Text = MaX;
                tb_TenXe.Text = TenX;
                tb_theloai.Text = Theloai;
                tb_Biensoxe.Text = Bienso;
                tb_Dongia.Text = Dongia;
                cb_Trangthai.Text = Trangthai;
            }
        }
    }
    
}
