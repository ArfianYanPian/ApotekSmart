namespace ApotekSmart.Views.Panels.Kasir
{
    partial class PanelLaporanKasir
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
            btnTampilkan = new Button();
            pnlSummary = new Panel();
            lblTotalTransaksi = new Label();
            lblTotalPendapatan = new Label();
            dgvLaporan = new DataGridView();
            pnlFilter.SuspendLayout();
            pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).BeginInit();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = Color.White;
            pnlFilter.Controls.Add(btnTampilkan);
            pnlFilter.Controls.Add(dtpSampai);
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
            lblSampai.Location = new Point(230, 20);
            lblSampai.Name = "lblSampai";
            lblSampai.Size = new Size(74, 25);
            lblSampai.TabIndex = 1;
            lblSampai.Text = "Sampai";
            // 
            // dtpDari
            // 
            dtpDari.Format = DateTimePickerFormat.Short;
            dtpDari.Location = new Point(64, 15);
            dtpDari.Name = "dtpDari";
            dtpDari.Size = new Size(160, 31);
            dtpDari.TabIndex = 2;
            // 
            // dtpSampai
            // 
            dtpSampai.Format = DateTimePickerFormat.Short;
            dtpSampai.Location = new Point(307, 15);
            dtpSampai.Name = "dtpSampai";
            dtpSampai.Size = new Size(160, 31);
            dtpSampai.TabIndex = 3;
            // 
            // btnTampilkan
            // 
            btnTampilkan.BackColor = Color.FromArgb(30, 58, 95);
            btnTampilkan.Cursor = Cursors.Hand;
            btnTampilkan.FlatAppearance.BorderSize = 0;
            btnTampilkan.FlatStyle = FlatStyle.Flat;
            btnTampilkan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTampilkan.ForeColor = Color.White;
            btnTampilkan.Location = new Point(470, 13);
            btnTampilkan.Name = "btnTampilkan";
            btnTampilkan.Size = new Size(160, 34);
            btnTampilkan.TabIndex = 4;
            btnTampilkan.Text = "📄 Tampilkan";
            btnTampilkan.UseVisualStyleBackColor = false;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(lblTotalPendapatan);
            pnlSummary.Controls.Add(lblTotalTransaksi);
            pnlSummary.Location = new Point(0, 70);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(1060, 70);
            pnlSummary.TabIndex = 1;
            // 
            // lblTotalTransaksi
            // 
            lblTotalTransaksi.AutoSize = true;
            lblTotalTransaksi.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalTransaksi.ForeColor = Color.FromArgb(30, 58, 95);
            lblTotalTransaksi.Location = new Point(20, 22);
            lblTotalTransaksi.Name = "lblTotalTransaksi";
            lblTotalTransaksi.Size = new Size(181, 28);
            lblTotalTransaksi.TabIndex = 0;
            lblTotalTransaksi.Text = " Total Transaksi: 0";
            // 
            // lblTotalPendapatan
            // 
            lblTotalPendapatan.AutoSize = true;
            lblTotalPendapatan.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalPendapatan.ForeColor = Color.FromArgb(39, 174, 96);
            lblTotalPendapatan.Location = new Point(300, 22);
            lblTotalPendapatan.Name = "lblTotalPendapatan";
            lblTotalPendapatan.Size = new Size(230, 28);
            lblTotalPendapatan.TabIndex = 1;
            lblTotalPendapatan.Text = "Total Pendapatan: Rp 0";
            // 
            // dgvLaporan
            // 
            dgvLaporan.AllowUserToAddRows = false;
            dgvLaporan.BackgroundColor = Color.White;
            dgvLaporan.BorderStyle = BorderStyle.None;
            dgvLaporan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLaporan.Location = new Point(0, 150);
            dgvLaporan.Name = "dgvLaporan";
            dgvLaporan.ReadOnly = true;
            dgvLaporan.RowHeadersVisible = false;
            dgvLaporan.RowHeadersWidth = 62;
            dgvLaporan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLaporan.Size = new Size(1060, 470);
            dgvLaporan.TabIndex = 2;
            // 
            // PanelLaporanKasir
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(dgvLaporan);
            Controls.Add(pnlSummary);
            Controls.Add(pnlFilter);
            Name = "PanelLaporanKasir";
            Size = new Size(1100, 650);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFilter;
        private DateTimePicker dtpDari;
        private Label lblSampai;
        private Label lblDari;
        private Button btnTampilkan;
        private DateTimePicker dtpSampai;
        private Panel pnlSummary;
        private Label lblTotalTransaksi;
        private Label lblTotalPendapatan;
        private DataGridView dgvLaporan;
    }
}
