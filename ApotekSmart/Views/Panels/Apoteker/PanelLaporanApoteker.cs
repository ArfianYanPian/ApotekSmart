using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;

namespace ApotekSmart.Views.Panels.Apoteker
{
    public partial class PanelLaporanApoteker : UserControl
    {
        private LaporanController _laporanController = new LaporanController();

        public PanelLaporanApoteker()
        {
            InitializeComponent();
            AturDGV();
            AturEvent();

            // Default tanggal
            dtpDari.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpSampai.Value = DateTime.Now;
        }

        private void AturDGV()
        {
            dgvLaporan.ReadOnly = true;
            dgvLaporan.AllowUserToAddRows = false;
            dgvLaporan.AllowUserToDeleteRows = false;
            dgvLaporan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLaporan.BackgroundColor = Color.White;
            dgvLaporan.BorderStyle = BorderStyle.None;
            dgvLaporan.RowHeadersVisible = false;
            dgvLaporan.Font = new Font("Segoe UI", 9f);
            dgvLaporan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvLaporan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvLaporan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLaporan.EnableHeadersVisualStyles = false;
            dgvLaporan.GridColor = Color.FromArgb(235, 238, 242);
        }

        private void AturEvent()
        {
            btnHarian.Click += BtnHarian_Click;
            btnBulanan.Click += BtnBulanan_Click;
            btnResep.Click += BtnResep_Click;
        }

        private void BtnHarian_Click(object sender, EventArgs e)
        {
            if (!ValidasiTanggal()) return;
            try
            {
                lblJudulLaporan.Text = $"Laporan Harian  ({dtpDari.Value:dd/MM/yyyy} - {dtpSampai.Value:dd/MM/yyyy})";
                DataTable dt = _laporanController.GetLaporanHarian(dtpDari.Value, dtpSampai.Value);
                dgvLaporan.DataSource = dt;
                RenameHeaderHarian();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal ambil laporan harian: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBulanan_Click(object sender, EventArgs e)
        {
            if (!ValidasiTanggal()) return;
            try
            {
                lblJudulLaporan.Text = $"Laporan Bulanan  ({dtpDari.Value:MM/yyyy} - {dtpSampai.Value:MM/yyyy})";
                DataTable dt = _laporanController.GetLaporanBulanan(dtpDari.Value, dtpSampai.Value);
                dgvLaporan.DataSource = dt;
                RenameHeaderBulanan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal ambil laporan bulanan: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResep_Click(object sender, EventArgs e)
        {
            if (!ValidasiTanggal()) return;
            try
            {
                lblJudulLaporan.Text = $"Laporan Resep  ({dtpDari.Value:dd/MM/yyyy} - {dtpSampai.Value:dd/MM/yyyy})";
                DataTable dt = _laporanController.GetLaporanResep(dtpDari.Value, dtpSampai.Value);
                dgvLaporan.DataSource = dt;
                RenameHeaderResep();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal ambil laporan resep: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidasiTanggal()
        {
            if (dtpDari.Value > dtpSampai.Value)
            {
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void RenameHeaderHarian()
        {
            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "tahun",             "Tahun" },
                { "bulan",             "Bulan" },
                { "hari",              "Hari" },
                { "jumlah_transaksi",  "Jml Transaksi" },
                { "total_penjualan",   "Total Penjualan" },
            };
            foreach (var h in headers)
                if (dgvLaporan.Columns.Contains(h.Key))
                    dgvLaporan.Columns[h.Key].HeaderText = h.Value;
        }

        private void RenameHeaderBulanan()
        {
            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "tahun",             "Tahun" },
                { "bulan",             "Bulan" },
                { "jumlah_transaksi",  "Jml Transaksi" },
                { "total_penjualan",   "Total Penjualan" },
                { "transaksi_resep",   "Transaksi Resep" },
                { "transaksi_biasa",   "Transaksi Biasa" },
            };
            foreach (var h in headers)
                if (dgvLaporan.Columns.Contains(h.Key))
                    dgvLaporan.Columns[h.Key].HeaderText = h.Value;
        }

        private void RenameHeaderResep()
        {
            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "id_transaksi",  "ID" },
                { "tanggal",       "Tanggal" },
                { "jenis",         "Jenis" },
                { "total",         "Total" },
                { "kasir",         "Kasir" },
                { "nomor_resep",   "No. Resep" },
            };
            foreach (var h in headers)
                if (dgvLaporan.Columns.Contains(h.Key))
                    dgvLaporan.Columns[h.Key].HeaderText = h.Value;
        }
    }
}