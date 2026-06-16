using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels
{
    public partial class PanelDashboard : UserControl
    {
        private BaseUser _currentUser;
        private LaporanController _laporanController = new LaporanController();
        private StokController _stokController = new StokController();

        public PanelDashboard(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            AturTampilan();
            MuatData();
        }

        // ── Atur tampilan sesuai role ────────────────────────
        private void AturTampilan()
        {
            lblWelcome.Text = $"Selamat Datang, {_currentUser.Nama}!";
            lblTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy",
                new System.Globalization.CultureInfo("id-ID"));

            // Kasir: sembunyikan kartu yang tidak perlu
            if (_currentUser.Role == "kasir")
            {
                pnlStatObat.Visible = false;
                pnlStatAlert.Visible = false;
                pnlStokKritis.Visible = false;
                pnlObatExp.Visible = false;
                pnlStatTransaksi.Location = new Point(0, 70);
                pnlStatResep.Location = new Point(256, 70);
            }

            AturDGV(dgvStokKritis);
            AturDGV(dgvObatExp);
        }

        private void AturDGV(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.Font = new Font("Segoe UI", 9f);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 58, 95);
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Color.FromArgb(235, 238, 242);
        }

        // ── Muat semua data ──────────────────────────────────
        private void MuatData()
        {
            try
            {
                MuatSummary();
                if (_currentUser.Role == "apoteker")
                {
                    MuatStokKritis();
                    MuatObatKadaluarsa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat dashboard: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MuatSummary()
        {
            DataTable dt = _laporanController.GetSummaryDashboard();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblStatObatNum.Text = row["total_obat"].ToString();
                lblStatTrxNum.Text = row["transaksi_hari_ini"].ToString();
                lblStatResepNum.Text = row["resep_menunggu"].ToString();
                lblStatAlertNum.Text = row["alert_stok"].ToString();

                if (Convert.ToInt32(row["alert_stok"]) > 0)
                    lblStatAlertNum.ForeColor = Color.FromArgb(226, 75, 74);
                if (Convert.ToInt32(row["resep_menunggu"]) > 0)
                    lblStatResepNum.ForeColor = Color.FromArgb(230, 126, 34);
            }
        }

        private void MuatStokKritis()
        {
            DataTable dt = _stokController.GetStokKritis();
            dgvStokKritis.DataSource = dt;

            if (dgvStokKritis.Columns.Contains("nama_obat"))
                dgvStokKritis.Columns["nama_obat"].HeaderText = "Nama Obat";
            if (dgvStokKritis.Columns.Contains("nama_kategori"))
                dgvStokKritis.Columns["nama_kategori"].HeaderText = "Kategori";
            if (dgvStokKritis.Columns.Contains("stok"))
                dgvStokKritis.Columns["stok"].HeaderText = "Stok";
            if (dgvStokKritis.Columns.Contains("stok_minimum"))
                dgvStokKritis.Columns["stok_minimum"].HeaderText = "Min";
            if (dgvStokKritis.Columns.Contains("status_stok"))
                dgvStokKritis.Columns["status_stok"].HeaderText = "Status";
            if (dgvStokKritis.Columns.Contains("id_obat"))
                dgvStokKritis.Columns["id_obat"].Visible = false;

            foreach (DataGridViewRow row in dgvStokKritis.Rows)
            {
                string status = row.Cells["status_stok"]?.Value?.ToString();
                if (status == "HABIS")
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(226, 75, 74);
                else if (status == "KRITIS")
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
            }
        }

        private void MuatObatKadaluarsa()
        {
            DataTable dt = _stokController.GetObatKadaluarsa();
            dgvObatExp.DataSource = dt;

            if (dgvObatExp.Columns.Contains("nama_obat"))
                dgvObatExp.Columns["nama_obat"].HeaderText = "Nama Obat";
            if (dgvObatExp.Columns.Contains("nama_kategori"))
                dgvObatExp.Columns["nama_kategori"].HeaderText = "Kategori";
            if (dgvObatExp.Columns.Contains("tanggal_exp"))
                dgvObatExp.Columns["tanggal_exp"].HeaderText = "Tgl Exp";
            if (dgvObatExp.Columns.Contains("status"))
                dgvObatExp.Columns["status"].HeaderText = "Status";

            foreach (string col in new[] { "id_obat", "jenis",
                "hari_sejak_exp", "hari_sampai_exp", "stok" })
                if (dgvObatExp.Columns.Contains(col))
                    dgvObatExp.Columns[col].Visible = false;

            foreach (DataGridViewRow row in dgvObatExp.Rows)
            {
                string status = row.Cells["status"]?.Value?.ToString();
                if (status == "KADALUARSA")
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(226, 75, 74);
                else if (status == "HAMPIR_EXP")
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
            }
        }
    }
}