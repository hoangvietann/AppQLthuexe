namespace AppQLthuexe
{
    partial class Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_BCTK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Thuexe = new System.Windows.Forms.Button();
            this.btn_exitLogin = new System.Windows.Forms.Button();
            this.btn_QLTK = new System.Windows.Forms.Button();
            this.btn_HD = new System.Windows.Forms.Button();
            this.btn_KH = new System.Windows.Forms.Button();
            this.btn_QLXE = new System.Windows.Forms.Button();
            this.panel_hien = new System.Windows.Forms.Panel();
            this.npgsqlDataAdapter1 = new Npgsql.NpgsqlDataAdapter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.btn_BCTK);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_Thuexe);
            this.panel1.Controls.Add(this.btn_exitLogin);
            this.panel1.Controls.Add(this.btn_QLTK);
            this.panel1.Controls.Add(this.btn_HD);
            this.panel1.Controls.Add(this.btn_KH);
            this.panel1.Controls.Add(this.btn_QLXE);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 658);
            this.panel1.TabIndex = 0;
            // 
            // btn_BCTK
            // 
            this.btn_BCTK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_BCTK.Location = new System.Drawing.Point(3, 373);
            this.btn_BCTK.Name = "btn_BCTK";
            this.btn_BCTK.Size = new System.Drawing.Size(221, 55);
            this.btn_BCTK.TabIndex = 6;
            this.btn_BCTK.Text = "Báo cáo thống kê";
            this.btn_BCTK.UseVisualStyleBackColor = false;
            this.btn_BCTK.Click += new System.EventHandler(this.btn_BCTK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AppQLthuexe.Properties.Resources.favicon_logo_2_150x150;
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Thuexe
            // 
            this.btn_Thuexe.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Thuexe.Location = new System.Drawing.Point(3, 129);
            this.btn_Thuexe.Name = "btn_Thuexe";
            this.btn_Thuexe.Size = new System.Drawing.Size(221, 55);
            this.btn_Thuexe.TabIndex = 0;
            this.btn_Thuexe.Text = "Thuê xe";
            this.btn_Thuexe.UseVisualStyleBackColor = false;
            this.btn_Thuexe.Click += new System.EventHandler(this.btn_Thuexe_Click);
            // 
            // btn_exitLogin
            // 
            this.btn_exitLogin.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btn_exitLogin.Location = new System.Drawing.Point(3, 589);
            this.btn_exitLogin.Name = "btn_exitLogin";
            this.btn_exitLogin.Size = new System.Drawing.Size(221, 66);
            this.btn_exitLogin.TabIndex = 4;
            this.btn_exitLogin.Text = "Đăng Xuất";
            this.btn_exitLogin.UseVisualStyleBackColor = false;
            this.btn_exitLogin.Click += new System.EventHandler(this.btn_exitLogin_Click);
            // 
            // btn_QLTK
            // 
            this.btn_QLTK.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btn_QLTK.Location = new System.Drawing.Point(3, 527);
            this.btn_QLTK.Name = "btn_QLTK";
            this.btn_QLTK.Size = new System.Drawing.Size(221, 56);
            this.btn_QLTK.TabIndex = 3;
            this.btn_QLTK.Text = "QL Hệ thống";
            this.btn_QLTK.UseVisualStyleBackColor = false;
            this.btn_QLTK.Click += new System.EventHandler(this.btn_QLTK_Click);
            // 
            // btn_HD
            // 
            this.btn_HD.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_HD.Location = new System.Drawing.Point(3, 251);
            this.btn_HD.Name = "btn_HD";
            this.btn_HD.Size = new System.Drawing.Size(221, 55);
            this.btn_HD.TabIndex = 2;
            this.btn_HD.Text = "QL Hợp đồng";
            this.btn_HD.UseVisualStyleBackColor = false;
            this.btn_HD.Click += new System.EventHandler(this.btn_HD_Click);
            // 
            // btn_KH
            // 
            this.btn_KH.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_KH.Location = new System.Drawing.Point(3, 190);
            this.btn_KH.Name = "btn_KH";
            this.btn_KH.Size = new System.Drawing.Size(221, 55);
            this.btn_KH.TabIndex = 0;
            this.btn_KH.Text = "QL Khách hàng";
            this.btn_KH.UseVisualStyleBackColor = false;
            this.btn_KH.Click += new System.EventHandler(this.btn_KH_Click);
            // 
            // btn_QLXE
            // 
            this.btn_QLXE.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_QLXE.Location = new System.Drawing.Point(3, 312);
            this.btn_QLXE.Name = "btn_QLXE";
            this.btn_QLXE.Size = new System.Drawing.Size(221, 55);
            this.btn_QLXE.TabIndex = 0;
            this.btn_QLXE.Text = "QL Xe";
            this.btn_QLXE.UseVisualStyleBackColor = false;
            this.btn_QLXE.Click += new System.EventHandler(this.btn_QLXE_Click);
            // 
            // panel_hien
            // 
            this.panel_hien.Location = new System.Drawing.Point(234, 2);
            this.panel_hien.Name = "panel_hien";
            this.panel_hien.Size = new System.Drawing.Size(1327, 683);
            this.panel_hien.TabIndex = 4;
            // 
            // npgsqlDataAdapter1
            // 
            this.npgsqlDataAdapter1.DeleteCommand = null;
            this.npgsqlDataAdapter1.InsertCommand = null;
            this.npgsqlDataAdapter1.SelectCommand = null;
            this.npgsqlDataAdapter1.UpdateCommand = null;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 668);
            this.Controls.Add(this.panel_hien);
            this.Controls.Add(this.panel1);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrangChu";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_HD;
        private System.Windows.Forms.Button btn_KH;
        private System.Windows.Forms.Button btn_QLXE;
        private System.Windows.Forms.Button btn_QLTK;
        private System.Windows.Forms.Panel panel_hien;
        private System.Windows.Forms.Button btn_exitLogin;
        private Npgsql.NpgsqlDataAdapter npgsqlDataAdapter1;
        private System.Windows.Forms.Button btn_Thuexe;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_BCTK;
    }
}

