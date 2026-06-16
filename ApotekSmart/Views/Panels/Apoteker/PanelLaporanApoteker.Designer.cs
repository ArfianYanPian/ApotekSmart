namespace ApotekSmart.Views.Panels.Apoteker
{
    partial class PanelLaporanApoteker
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
            pnlFilter = new Panel();
            lblDari = new Label();
            lblSampai = new Label();
            dtpDari = new DateTimePicker();
            dtpSampai = new DateTimePicker();
            btnHarian = new Button();
            btnBulanan = new Button();
            btnResep = new Button();
            lblJudulLaporan = new Label();
            dgvLaporan = new DataGridView();
            pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).BeginInit();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = Color.White;
            pnlFilter.Controls.Add(btnResep);
            pnlFilter.Controls.Add(btnBulanan);
            pnlFilter.Controls.Add(dtpDari);
            pnlFilter.Controls.Add(lblSampai);
            pnlFilter.Controls.Add(lblDari);
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1060, 60);
            pnlFilter.TabIndex = 0;
            // 
            // lblDari
            // 
            lblDari.AutoSize = true;
            lblDari.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDari.Location = new Point(15, 20);
            lblDari.Name = "lblDari";
            lblDari.Size = new Size(47, 25);
            lblDari.TabIndex = 0;
            lblDari.Text = "Dari";
            // 
            // lblSampai
            // 
            lblSampai.AutoSize = true;
            lblSampai.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSampai.Location = new Point(229, 20);
            lblSampai.Name = "lblSampai";
            lblSampai.Size = new Size(74, 25);
            lblSampai.TabIndex = 1;
            lblSampai.Text = "Sampai";
            // 
            // dtpDari
            // 
            dtpDari.Format = DateTimePickerFormat.Short;
            dtpDari.Location = new Point(62, 16);
            dtpDari.Name = "dtpDari";
            dtpDari.Size = new Size(160, 31);
            dtpDari.TabIndex = 2;
            // 
            // dtpSampai
            // 
            dtpSampai.Format = DateTimePickerFormat.Short;
            dtpSampai.Location = new Point(302, 17);
            dtpSampai.Name = "dtpSampai";
            dtpSampai.Size = new Size(160, 31);
            dtpSampai.TabIndex = 1;
            // 
            // btnHarian
            // 
            btnHarian.BackColor = Color.FromArgb(30, 58, 95);
            btnHarian.Cursor = Cursors.Hand;
            btnHarian.FlatAppearance.BorderSize = 0;
            btnHarian.FlatStyle = FlatStyle.Flat;
            btnHarian.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHarian.ForeColor = Color.White;
            btnHarian.Location = new Point(470, 13);
            btnHarian.Name = "btnHarian";
            btnHarian.Size = new Size(183, 34);
            btnHarian.TabIndex = 2;
            btnHarian.Text = "📅 Laporan Harian";
            btnHarian.UseVisualStyleBackColor = false;
            // 
            // btnBulanan
            // 
            btnBulanan.BackColor = Color.FromArgb(42, 80, 128);
            btnBulanan.Cursor = Cursors.Hand;
            btnBulanan.FlatAppearance.BorderSize = 0;
            btnBulanan.FlatStyle = FlatStyle.Flat;
            btnBulanan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBulanan.ForeColor = Color.White;
            btnBulanan.Location = new Point(661, 13);
            btnBulanan.Name = "btnBulanan";
            btnBulanan.Size = new Size(199, 34);
            btnBulanan.TabIndex = 3;
            btnBulanan.Text = "📆 Laporan Bulanan";
            btnBulanan.UseVisualStyleBackColor = false;
            // 
            // btnResep
            // 
            btnResep.BackColor = Color.FromArgb(39, 174, 96);
            btnResep.Cursor = Cursors.Hand;
            btnResep.FlatAppearance.BorderSize = 0;
            btnResep.FlatStyle = FlatStyle.Flat;
            btnResep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnResep.ForeColor = Color.White;
            btnResep.Location = new Point(867, 13);
            btnResep.Name = "btnResep";
            btnResep.Size = new Size(187, 34);
            btnResep.TabIndex = 4;
            btnResep.Text = "📋 Laporan Resep";
            btnResep.UseVisualStyleBackColor = false;
            // 
            // lblJudulLaporan
            // 
            lblJudulLaporan.AutoSize = true;
            lblJudulLaporan.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJudulLaporan.ForeColor = Color.FromArgb(30, 58, 95);
            lblJudulLaporan.Location = new Point(0, 75);
            lblJudulLaporan.Name = "lblJudulLaporan";
            lblJudulLaporan.Size = new Size(271, 30);
            lblJudulLaporan.TabIndex = 3;
            lblJudulLaporan.Text = "Pilih jenis laporan di atas";
            // 
            // dgvLaporan
            // 
            dgvLaporan.AllowUserToAddRows = false;
            dgvLaporan.BackgroundColor = Color.White;
            dgvLaporan.BorderStyle = BorderStyle.None;
            dgvLaporan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLaporan.Location = new Point(0, 111);
            dgvLaporan.Name = "dgvLaporan";
            dgvLaporan.ReadOnly = true;
            dgvLaporan.RowHeadersVisible = false;
            dgvLaporan.RowHeadersWidth = 62;
            dgvLaporan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLaporan.Size = new Size(1060, 520);
            dgvLaporan.TabIndex = 4;
            // 
            // PanelLaporanApoteker
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(dgvLaporan);
            Controls.Add(lblJudulLaporan);
            Controls.Add(btnHarian);
            Controls.Add(dtpSampai);
            Controls.Add(pnlFilter);
            Name = "PanelLaporanApoteker";
            Size = new Size(1100, 650);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlFilter;
        private DateTimePicker dtpDari;
        private Label lblSampai;
        private Label lblDari;
        private DateTimePicker dtpSampai;
        private Button btnHarian;
        private Button btnBulanan;
        private Button btnResep;
        private Label lblJudulLaporan;
        private DataGridView dgvLaporan;
    }
}
