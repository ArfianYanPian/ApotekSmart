using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;

namespace ApotekSmart.Views.Panels.Kasir
{
    public partial class PanelCariObat : UserControl
    {
        private ObatController _obatController = new ObatController();

        public PanelCariObat()
        {
            InitializeComponent();
            AturDGV();
            MuatKategori();
            MuatData();
            AturEvent();
        }

        private void AturDGV()
        {
            dgvObat.ReadOnly = true;
            dgvObat.AllowUserToAddRows = false;
            dgvObat.AllowUserToDeleteRows = false;
            dgvObat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvObat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvObat.BackgroundColor = Color.White;
            dgvObat.BorderStyle = BorderStyle.None;
            dgvObat.RowHeadersVisible = false;
            dgvObat.Font = new Font("Segoe UI", 9f);
            dgvObat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvObat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvObat.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvObat.EnableHeadersVisualStyles = false;
            dgvObat.GridColor = Color.FromArgb(235, 238, 242);
        }

        private void MuatKategori()
        {
            try
            {
                DataTable dt = _obatController.GetAllKategori();
                cmbKategori.Items.Clear();
                cmbKategori.Items.Add("Semua Kategori");
                foreach (DataRow row in dt.Rows)
                    cmbKategori.Items.Add(row["nama_kategori"].ToString());
                cmbKategori.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat kategori: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MuatData()
        {
            try
            {
                string nama = txtCari.Text.Trim();
                string kategori = cmbKategori.SelectedIndex == 0
                    ? "" : cmbKategori.SelectedItem.ToString();

                DataTable dt = _obatController.SearchObat(nama, kategori);
                dgvObat.DataSource = dt;

                // Rename header
                var headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "nama_obat",      "Nama Obat" },
                    { "jenis",          "Jenis" },
                    { "golongan",       "Golongan" },
                    { "nama_kategori",  "Kategori" },
                    { "satuan",         "Satuan" },
                    { "harga_jual",     "Harga Jual" },
                    { "stok",           "Stok" },
                    { "tanggal_exp",    "Tgl Exp" },
                    { "status_stok",    "Status Stok" },
                    { "status_kelayakan", "Kelayakan" },
                };
                foreach (var h in headers)
                    if (dgvObat.Columns.Contains(h.Key))
                        dgvObat.Columns[h.Key].HeaderText = h.Value;

                // Sembunyikan kolom tidak perlu
                foreach (string col in new[] { "id_obat", "id_kategori",
                    "harga_beli", "stok_minimum", "deskripsi",
                    "created_at", "is_active" })
                    if (dgvObat.Columns.Contains(col))
                        dgvObat.Columns[col].Visible = false;

                // Warna baris
                foreach (DataGridViewRow row in dgvObat.Rows)
                {
                    string status = row.Cells["status_kelayakan"]?.Value?.ToString();
                    if (status == "KADALUARSA")
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(226, 75, 74);
                    else if (status == "HAMPIR_EXP")
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);

                    string stok = row.Cells["status_stok"]?.Value?.ToString();
                    if (stok == "HABIS")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 245);
                }

                lblInfo.Text = $"Total: {dt.Rows.Count} obat ditemukan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cari obat: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AturEvent()
        {
            btnCari.Click += (s, e) => MuatData();
            btnReset.Click += (s, e) =>
            {
                txtCari.Clear();
                cmbKategori.SelectedIndex = 0;
                MuatData();
            };
            // Cari realtime saat ketik
            txtCari.TextChanged += (s, e) => MuatData();
        }
    }
}