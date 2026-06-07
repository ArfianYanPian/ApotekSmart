namespace ApotekSmart.Views.Panels.Apoteker
{
    partial class PanelValidasiResep
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlKiri = new Panel();
            lblDaftarResep = new Label();
            dgvResep = new DataGridView();
            pnlKanan = new Panel();
            lblDetailResep = new Label();
            lblNomorResep = new Label();
            lblNilaiNomorResep = new Label();
            lblNamaPasien = new Label();
            lblNilaiNamaPasien = new Label();
            lblNamaDokter = new Label();
            lblNilaiNamaDokter = new Label();
            lblCatatan = new Label();
            txtCatatan = new TextBox();
            btnSetujui = new Button();
            btnTolak = new Button();
            pnlKiri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResep).BeginInit();
            pnlKanan.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.White;
            pnlKiri.Controls.Add(dgvResep);
            pnlKiri.Controls.Add(lblDaftarResep);
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(400, 620);
            pnlKiri.TabIndex = 0;
            // 
            // lblDaftarResep
            // 
            lblDaftarResep.AutoSize = true;
            lblDaftarResep.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDaftarResep.ForeColor = Color.FromArgb(30, 58, 95);
            lblDaftarResep.Location = new Point(15, 15);
            lblDaftarResep.Name = "lblDaftarResep";
            lblDaftarResep.Size = new Size(277, 28);
            lblDaftarResep.TabIndex = 0;
            lblDaftarResep.Text = "📋 Daftar Resep Menunggu";
            // 
            // dgvResep
            // 
            dgvResep.AllowUserToAddRows = false;
            dgvResep.BackgroundColor = Color.White;
            dgvResep.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResep.Location = new Point(15, 45);
            dgvResep.Name = "dgvResep";
            dgvResep.ReadOnly = true;
            dgvResep.RowHeadersVisible = false;
            dgvResep.RowHeadersWidth = 62;
            dgvResep.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResep.Size = new Size(370, 560);
            dgvResep.TabIndex = 1;
            // 
            // pnlKanan
            // 
            pnlKanan.BackColor = Color.White;
            pnlKanan.Controls.Add(btnTolak);
            pnlKanan.Controls.Add(btnSetujui);
            pnlKanan.Controls.Add(txtCatatan);
            pnlKanan.Controls.Add(lblCatatan);
            pnlKanan.Controls.Add(lblNilaiNamaDokter);
            pnlKanan.Controls.Add(lblNamaDokter);
            pnlKanan.Controls.Add(lblNilaiNamaPasien);
            pnlKanan.Controls.Add(lblNamaPasien);
            pnlKanan.Controls.Add(lblNilaiNomorResep);
            pnlKanan.Controls.Add(lblNomorResep);
            pnlKanan.Controls.Add(lblDetailResep);
            pnlKanan.Location = new Point(420, 0);
            pnlKanan.Name = "pnlKanan";
            pnlKanan.Size = new Size(640, 620);
            pnlKanan.TabIndex = 1;
            pnlKanan.Paint += pnlKanan_Paint;
            // 
            // lblDetailResep
            // 
            lblDetailResep.AutoSize = true;
            lblDetailResep.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetailResep.ForeColor = Color.FromArgb(30, 58, 95);
            lblDetailResep.Location = new Point(15, 15);
            lblDetailResep.Name = "lblDetailResep";
            lblDetailResep.Size = new Size(165, 28);
            lblDetailResep.TabIndex = 0;
            lblDetailResep.Text = "📄 Detail Resep";
            // 
            // lblNomorResep
            // 
            lblNomorResep.AutoSize = true;
            lblNomorResep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomorResep.Location = new Point(15, 55);
            lblNomorResep.Name = "lblNomorResep";
            lblNomorResep.Size = new Size(127, 25);
            lblNomorResep.TabIndex = 1;
            lblNomorResep.Text = "Nomor Resep";
            // 
            // lblNilaiNomorResep
            // 
            lblNilaiNomorResep.AutoSize = true;
            lblNilaiNomorResep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNilaiNomorResep.Location = new Point(200, 55);
            lblNilaiNomorResep.Name = "lblNilaiNomorResep";
            lblNilaiNomorResep.Size = new Size(19, 25);
            lblNilaiNomorResep.TabIndex = 2;
            lblNilaiNomorResep.Text = "-";
            // 
            // lblNamaPasien
            // 
            lblNamaPasien.AutoSize = true;
            lblNamaPasien.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamaPasien.Location = new Point(15, 85);
            lblNamaPasien.Name = "lblNamaPasien";
            lblNamaPasien.Size = new Size(132, 25);
            lblNamaPasien.TabIndex = 3;
            lblNamaPasien.Text = "Nama Pasien :";
            // 
            // lblNilaiNamaPasien
            // 
            lblNilaiNamaPasien.AutoSize = true;
            lblNilaiNamaPasien.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNilaiNamaPasien.Location = new Point(200, 85);
            lblNilaiNamaPasien.Name = "lblNilaiNamaPasien";
            lblNilaiNamaPasien.Size = new Size(19, 25);
            lblNilaiNamaPasien.TabIndex = 4;
            lblNilaiNamaPasien.Text = "-";
            // 
            // lblNamaDokter
            // 
            lblNamaDokter.AutoSize = true;
            lblNamaDokter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamaDokter.Location = new Point(15, 115);
            lblNamaDokter.Name = "lblNamaDokter";
            lblNamaDokter.Size = new Size(135, 25);
            lblNamaDokter.TabIndex = 5;
            lblNamaDokter.Text = "Nama Dokter :";
            // 
            // lblNilaiNamaDokter
            // 
            lblNilaiNamaDokter.AutoSize = true;
            lblNilaiNamaDokter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNilaiNamaDokter.Location = new Point(200, 115);
            lblNilaiNamaDokter.Name = "lblNilaiNamaDokter";
            lblNilaiNamaDokter.Size = new Size(19, 25);
            lblNilaiNamaDokter.TabIndex = 6;
            lblNilaiNamaDokter.Text = "-";
            // 
            // lblCatatan
            // 
            lblCatatan.AutoSize = true;
            lblCatatan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCatatan.Location = new Point(15, 155);
            lblCatatan.Name = "lblCatatan";
            lblCatatan.Size = new Size(172, 25);
            lblCatatan.TabIndex = 7;
            lblCatatan.Text = "Catatan Apoteker :";
            // 
            // txtCatatan
            // 
            txtCatatan.BorderStyle = BorderStyle.FixedSingle;
            txtCatatan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCatatan.Location = new Point(15, 178);
            txtCatatan.Multiline = true;
            txtCatatan.Name = "txtCatatan";
            txtCatatan.Size = new Size(600, 80);
            txtCatatan.TabIndex = 8;
            // 
            // btnSetujui
            // 
            btnSetujui.BackColor = Color.FromArgb(39, 174, 96);
            btnSetujui.Cursor = Cursors.Hand;
            btnSetujui.FlatAppearance.BorderSize = 0;
            btnSetujui.FlatStyle = FlatStyle.Flat;
            btnSetujui.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSetujui.ForeColor = Color.White;
            btnSetujui.Location = new Point(15, 280);
            btnSetujui.Name = "btnSetujui";
            btnSetujui.Size = new Size(190, 42);
            btnSetujui.TabIndex = 9;
            btnSetujui.Text = "✅ Setujui Resep";
            btnSetujui.UseVisualStyleBackColor = false;
            // 
            // btnTolak
            // 
            btnTolak.BackColor = Color.FromArgb(162, 45, 45);
            btnTolak.FlatAppearance.BorderSize = 0;
            btnTolak.FlatStyle = FlatStyle.Flat;
            btnTolak.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTolak.ForeColor = Color.White;
            btnTolak.Location = new Point(220, 280);
            btnTolak.Name = "btnTolak";
            btnTolak.Size = new Size(190, 42);
            btnTolak.TabIndex = 10;
            btnTolak.Text = "❌ Tolak Resep";
            btnTolak.UseVisualStyleBackColor = false;
            // 
            // PanelValidasiResep
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlKanan);
            Controls.Add(pnlKiri);
            Name = "PanelValidasiResep";
            Size = new Size(1100, 650);
            pnlKiri.ResumeLayout(false);
            pnlKiri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResep).EndInit();
            pnlKanan.ResumeLayout(false);
            pnlKanan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlKiri;
        private DataGridView dgvResep;
        private Label lblDaftarResep;
        private Panel pnlKanan;
        private Label lblNamaPasien;
        private Label lblNilaiNomorResep;
        private Label lblNomorResep;
        private Label lblDetailResep;
        private TextBox txtCatatan;
        private Label lblCatatan;
        private Label lblNilaiNamaDokter;
        private Label lblNamaDokter;
        private Label lblNilaiNamaPasien;
        private Button btnTolak;
        private Button btnSetujui;
    }
}
