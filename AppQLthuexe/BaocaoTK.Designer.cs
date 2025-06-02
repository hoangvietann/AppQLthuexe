namespace AppQLthuexe
{
    partial class BaocaoTK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartDoanhthu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tab_DTthoigian = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Xemngay = new System.Windows.Forms.Button();
            this.btn_Xemnam = new System.Windows.Forms.Button();
            this.btn_Xemthang = new System.Windows.Forms.Button();
            this.cb_Thang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Nam = new System.Windows.Forms.ComboBox();
            this.tab_DTtheoxe = new System.Windows.Forms.TabPage();
            this.dtgv_showdtloaixe = new System.Windows.Forms.DataGridView();
            this.btn_DTthang = new System.Windows.Forms.Button();
            this.btn_TheoNam = new System.Windows.Forms.Button();
            this.cb_ThangXe = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_NamXe = new System.Windows.Forms.ComboBox();
            this.chartDoanhthuxe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tab_Doanhthukh = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgv_Showdtkh = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthu)).BeginInit();
            this.tab_DTthoigian.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tab_DTtheoxe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_showdtloaixe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthuxe)).BeginInit();
            this.tab_Doanhthukh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Showdtkh)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDoanhthu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhthu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhthu.Legends.Add(legend1);
            this.chartDoanhthu.Location = new System.Drawing.Point(8, 198);
            this.chartDoanhthu.Name = "chartDoanhthu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDoanhthu.Series.Add(series1);
            this.chartDoanhthu.Size = new System.Drawing.Size(1271, 373);
            this.chartDoanhthu.TabIndex = 0;
            this.chartDoanhthu.Text = "chart1";
            // 
            // tab_DTthoigian
            // 
            this.tab_DTthoigian.AccessibleName = "";
            this.tab_DTthoigian.Controls.Add(this.tabPage1);
            this.tab_DTthoigian.Controls.Add(this.tab_DTtheoxe);
            this.tab_DTthoigian.Controls.Add(this.tab_Doanhthukh);
            this.tab_DTthoigian.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_DTthoigian.Location = new System.Drawing.Point(0, 6);
            this.tab_DTthoigian.Name = "tab_DTthoigian";
            this.tab_DTthoigian.SelectedIndex = 0;
            this.tab_DTthoigian.Size = new System.Drawing.Size(1322, 630);
            this.tab_DTthoigian.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_Xemngay);
            this.tabPage1.Controls.Add(this.btn_Xemnam);
            this.tabPage1.Controls.Add(this.btn_Xemthang);
            this.tabPage1.Controls.Add(this.cb_Thang);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cb_Nam);
            this.tabPage1.Controls.Add(this.chartDoanhthu);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1314, 595);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Doanh thu theo thời gian";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_Xemngay
            // 
            this.btn_Xemngay.Location = new System.Drawing.Point(972, 133);
            this.btn_Xemngay.Name = "btn_Xemngay";
            this.btn_Xemngay.Size = new System.Drawing.Size(181, 36);
            this.btn_Xemngay.TabIndex = 8;
            this.btn_Xemngay.Text = "Xem theo ngày";
            this.btn_Xemngay.UseVisualStyleBackColor = true;
            this.btn_Xemngay.Click += new System.EventHandler(this.btn_Xemngay_Click);
            // 
            // btn_Xemnam
            // 
            this.btn_Xemnam.Location = new System.Drawing.Point(972, 16);
            this.btn_Xemnam.Name = "btn_Xemnam";
            this.btn_Xemnam.Size = new System.Drawing.Size(181, 36);
            this.btn_Xemnam.TabIndex = 7;
            this.btn_Xemnam.Text = "Xem theo năm";
            this.btn_Xemnam.UseVisualStyleBackColor = true;
            this.btn_Xemnam.Click += new System.EventHandler(this.btn_Xemnam_Click);
            // 
            // btn_Xemthang
            // 
            this.btn_Xemthang.Location = new System.Drawing.Point(972, 74);
            this.btn_Xemthang.Name = "btn_Xemthang";
            this.btn_Xemthang.Size = new System.Drawing.Size(181, 36);
            this.btn_Xemthang.TabIndex = 6;
            this.btn_Xemthang.Text = "Xem theo tháng";
            this.btn_Xemthang.UseVisualStyleBackColor = true;
            this.btn_Xemthang.Click += new System.EventHandler(this.btn_Xemthang_Click);
            // 
            // cb_Thang
            // 
            this.cb_Thang.FormattingEnabled = true;
            this.cb_Thang.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cb_Thang.Location = new System.Drawing.Point(724, 22);
            this.cb_Thang.Name = "cb_Thang";
            this.cb_Thang.Size = new System.Drawing.Size(121, 30);
            this.cb_Thang.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tháng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(420, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Năm:";
            // 
            // cb_Nam
            // 
            this.cb_Nam.FormattingEnabled = true;
            this.cb_Nam.Items.AddRange(new object[] {
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035",
            "2036",
            "2037",
            "2038",
            "2039",
            "2040"});
            this.cb_Nam.Location = new System.Drawing.Point(489, 22);
            this.cb_Nam.Name = "cb_Nam";
            this.cb_Nam.Size = new System.Drawing.Size(121, 30);
            this.cb_Nam.TabIndex = 2;
            // 
            // tab_DTtheoxe
            // 
            this.tab_DTtheoxe.Controls.Add(this.dtgv_showdtloaixe);
            this.tab_DTtheoxe.Controls.Add(this.btn_DTthang);
            this.tab_DTtheoxe.Controls.Add(this.btn_TheoNam);
            this.tab_DTtheoxe.Controls.Add(this.cb_ThangXe);
            this.tab_DTtheoxe.Controls.Add(this.label4);
            this.tab_DTtheoxe.Controls.Add(this.label5);
            this.tab_DTtheoxe.Controls.Add(this.cb_NamXe);
            this.tab_DTtheoxe.Controls.Add(this.chartDoanhthuxe);
            this.tab_DTtheoxe.Location = new System.Drawing.Point(4, 31);
            this.tab_DTtheoxe.Name = "tab_DTtheoxe";
            this.tab_DTtheoxe.Padding = new System.Windows.Forms.Padding(3);
            this.tab_DTtheoxe.Size = new System.Drawing.Size(1314, 595);
            this.tab_DTtheoxe.TabIndex = 1;
            this.tab_DTtheoxe.Text = "Doanh thu theo loại xe";
            this.tab_DTtheoxe.UseVisualStyleBackColor = true;
            // 
            // dtgv_showdtloaixe
            // 
            this.dtgv_showdtloaixe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgv_showdtloaixe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_showdtloaixe.Location = new System.Drawing.Point(18, 22);
            this.dtgv_showdtloaixe.Name = "dtgv_showdtloaixe";
            this.dtgv_showdtloaixe.RowHeadersWidth = 51;
            this.dtgv_showdtloaixe.RowTemplate.Height = 24;
            this.dtgv_showdtloaixe.Size = new System.Drawing.Size(768, 217);
            this.dtgv_showdtloaixe.TabIndex = 13;
            // 
            // btn_DTthang
            // 
            this.btn_DTthang.Location = new System.Drawing.Point(1050, 101);
            this.btn_DTthang.Name = "btn_DTthang";
            this.btn_DTthang.Size = new System.Drawing.Size(206, 38);
            this.btn_DTthang.TabIndex = 11;
            this.btn_DTthang.Text = "Doanh thu theo tháng";
            this.btn_DTthang.UseVisualStyleBackColor = true;
            this.btn_DTthang.Click += new System.EventHandler(this.btn_DTthang_Click);
            // 
            // btn_TheoNam
            // 
            this.btn_TheoNam.Location = new System.Drawing.Point(1050, 27);
            this.btn_TheoNam.Name = "btn_TheoNam";
            this.btn_TheoNam.Size = new System.Drawing.Size(206, 38);
            this.btn_TheoNam.TabIndex = 10;
            this.btn_TheoNam.Text = "Doanh thu theo năm";
            this.btn_TheoNam.UseVisualStyleBackColor = true;
            this.btn_TheoNam.Click += new System.EventHandler(this.btn_TheoNam_Click);
            // 
            // cb_ThangXe
            // 
            this.cb_ThangXe.FormattingEnabled = true;
            this.cb_ThangXe.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cb_ThangXe.Location = new System.Drawing.Point(880, 106);
            this.cb_ThangXe.Name = "cb_ThangXe";
            this.cb_ThangXe.Size = new System.Drawing.Size(121, 30);
            this.cb_ThangXe.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(802, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tháng:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(813, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 22);
            this.label5.TabIndex = 7;
            this.label5.Text = "Năm:";
            // 
            // cb_NamXe
            // 
            this.cb_NamXe.FormattingEnabled = true;
            this.cb_NamXe.Items.AddRange(new object[] {
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035",
            "2036",
            "2037",
            "2038",
            "2039",
            "2040"});
            this.cb_NamXe.Location = new System.Drawing.Point(880, 27);
            this.cb_NamXe.Name = "cb_NamXe";
            this.cb_NamXe.Size = new System.Drawing.Size(121, 30);
            this.cb_NamXe.TabIndex = 6;
            // 
            // chartDoanhthuxe
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDoanhthuxe.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartDoanhthuxe.Legends.Add(legend2);
            this.chartDoanhthuxe.Location = new System.Drawing.Point(3, 229);
            this.chartDoanhthuxe.Name = "chartDoanhthuxe";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartDoanhthuxe.Series.Add(series2);
            this.chartDoanhthuxe.Size = new System.Drawing.Size(1269, 340);
            this.chartDoanhthuxe.TabIndex = 2;
            this.chartDoanhthuxe.Text = "chart1";
            // 
            // tab_Doanhthukh
            // 
            this.tab_Doanhthukh.Controls.Add(this.label3);
            this.tab_Doanhthukh.Controls.Add(this.dtgv_Showdtkh);
            this.tab_Doanhthukh.Location = new System.Drawing.Point(4, 31);
            this.tab_Doanhthukh.Name = "tab_Doanhthukh";
            this.tab_Doanhthukh.Size = new System.Drawing.Size(1314, 595);
            this.tab_Doanhthukh.TabIndex = 2;
            this.tab_Doanhthukh.Text = "Doanh thu theo khách hàng";
            this.tab_Doanhthukh.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(613, 33);
            this.label3.TabIndex = 1;
            this.label3.Text = "TÓP KHÁCH HÀNG THUÊ XE NHIỀU TIỀN NHẤT";
            // 
            // dtgv_Showdtkh
            // 
            this.dtgv_Showdtkh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgv_Showdtkh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Showdtkh.Location = new System.Drawing.Point(138, 119);
            this.dtgv_Showdtkh.Name = "dtgv_Showdtkh";
            this.dtgv_Showdtkh.RowHeadersWidth = 51;
            this.dtgv_Showdtkh.RowTemplate.Height = 24;
            this.dtgv_Showdtkh.Size = new System.Drawing.Size(958, 453);
            this.dtgv_Showdtkh.TabIndex = 0;
            // 
            // BaocaoTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 636);
            this.Controls.Add(this.tab_DTthoigian);
            this.Name = "BaocaoTK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaocaoTK";
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthu)).EndInit();
            this.tab_DTthoigian.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tab_DTtheoxe.ResumeLayout(false);
            this.tab_DTtheoxe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_showdtloaixe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthuxe)).EndInit();
            this.tab_Doanhthukh.ResumeLayout(false);
            this.tab_Doanhthukh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Showdtkh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhthu;
        private System.Windows.Forms.TabControl tab_DTthoigian;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tab_DTtheoxe;
        private System.Windows.Forms.ComboBox cb_Thang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Nam;
        private System.Windows.Forms.Button btn_Xemngay;
        private System.Windows.Forms.Button btn_Xemnam;
        private System.Windows.Forms.Button btn_Xemthang;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhthuxe;
        private System.Windows.Forms.TabPage tab_Doanhthukh;
        private System.Windows.Forms.DataGridView dtgv_Showdtkh;
        private System.Windows.Forms.ComboBox cb_ThangXe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_NamXe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_DTthang;
        private System.Windows.Forms.Button btn_TheoNam;
        private System.Windows.Forms.DataGridView dtgv_showdtloaixe;
    }
}