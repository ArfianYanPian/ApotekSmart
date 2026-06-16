using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;

namespace ApotekSmart.Views.Panels.Kasir
{
    public partial class PanelLaporanKasir : UserControl
    {
        private LaporanController _laporanController = new LaporanController();

        public PanelLaporanKasir()
        {
            InitializeComponent();
            AturDGV();
            AturEvent();

            // Default tanggal hari ini
            dtpDari.Value = DateTime.Today;
            dtpSampai.Value = DateTime.Today;

            // Langsung tampilkan data hari ini
            MuatData();
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
            btnTampilkan.Click += (s, e) => MuatData();
        }

        private void MuatData()
        {
            if (dtpDari.Value > dtpSampai.Value)
            {
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = _laporanController.GetLaporanResep(
                    dtpDari.Value, dtpSampai.Value);

                dgvLaporan.DataSource = dt;

                // Rename header
                var headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "id_transaksi", "ID" },
                    { "tanggal",      "Tanggal" },
                    { "jenis",        "Jenis" },
                    { "total",        "Total" },
                    { "kasir",        "Kasir" },
                    { "nomor_resep",  "No. Resep" },
                };
                foreach (var h in headers)
                    if (dgvLaporan.Columns.Contains(h.Key))
                        dgvLaporan.Columns[h.Key].HeaderText = h.Value;

                // Warna baris resep vs biasa
                foreach (DataGridViewRow row in dgvLaporan.Rows)
                {
                    string jenis = row.Cells["jenis"]?.Value?.ToString();
                    if (jenis == "Resep")
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(30, 58, 95);
                }

                // Hitung summary
                HitungSummary(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat laporan: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HitungSummary(DataTable dt)
        {
            int totalTrx = dt.Rows.Count;
            decimal totalPendapatan = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["total"] != DBNull.Value)
                    totalPendapatan += Convert.ToDecimal(row["total"]);
            }

            lblTotalTransaksi.Text = $"Total Transaksi: {totalTrx}";
            lblTotalPendapatan.Text = $"Total Pendapatan: Rp {totalPendapatan:N0}";
        }
    }
}