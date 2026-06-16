using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;

namespace ApotekSmart.Views.Panels.Apoteker
{
    public partial class PanelManajemenObat : UserControl
    {
        private ObatController _obatController = new ObatController();
        private int _selectedObatId = -1;

        public PanelManajemenObat()
        {
            InitializeComponent();
            AturDGV();
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

        private void MuatData(string keyword = "")
        {
            try
            {
                DataTable dt = string.IsNullOrWhiteSpace(keyword)
                    ? _obatController.GetAllObat()
                    : _obatController.SearchObat(keyword);

                dgvObat.DataSource = dt;

                // Rename header
                var headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "id_obat",        "ID" },
                    { "nama_obat",      "Nama Obat" },
                    { "jenis",          "Jenis" },
                    { "golongan",       "Golongan" },
                    { "nama_kategori",  "Kategori" },
                    { "satuan",         "Satuan" },
                    { "harga_beli",     "Harga Beli" },
                    { "harga_jual",     "Harga Jual" },
                    { "stok",           "Stok" },
                    { "stok_minimum",   "Min" },
                    { "tanggal_exp",    "Tgl Exp" },
                    { "status_stok",    "Status Stok" },
                    { "status_kelayakan", "Kelayakan" },
                    { "is_active",      "Aktif" },
                };

                foreach (var h in headers)
                    if (dgvObat.Columns.Contains(h.Key))
                        dgvObat.Columns[h.Key].HeaderText = h.Value;

                // Sembunyikan kolom tidak perlu
                foreach (string col in new[] { "id_kategori", "deskripsi", "created_at" })
                    if (dgvObat.Columns.Contains(col))
                        dgvObat.Columns[col].Visible = false;

                // Warna baris berdasarkan status
                foreach (DataGridViewRow row in dgvObat.Rows)
                {
                    string status = row.Cells["status_kelayakan"]?.Value?.ToString();
                    if (status == "KADALUARSA")
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(226, 75, 74);
                    else if (status == "HAMPIR_EXP")
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                }

                _selectedObatId = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data obat: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AturEvent()
        {
            btnTambahObat.Click += BtnTambahObat_Click;
            btnEditObat.Click += BtnEditObat_Click;
            btnHapusObat.Click += BtnHapusObat_Click;

            txtCariObat.TextChanged += (s, e) => MuatData(txtCariObat.Text);

            dgvObat.SelectionChanged += (s, e) =>
            {
                if (dgvObat.SelectedRows.Count > 0)
                    _selectedObatId = Convert.ToInt32(
                        dgvObat.SelectedRows[0].Cells["id_obat"].Value);
            };
        }

        private void BtnTambahObat_Click(object sender, EventArgs e)
        {
            var form = new FormInputObat();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _obatController.TambahObat(form.ObatResult);
                    MessageBox.Show("Obat berhasil ditambahkan!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal tambah obat: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditObat_Click(object sender, EventArgs e)
        {
            if (_selectedObatId == -1)
            {
                MessageBox.Show("Pilih obat yang ingin diedit.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DataTable dt = _obatController.GetObatById(_selectedObatId);
                if (dt.Rows.Count == 0) return;

                var obat = ObatController.MapRowToObat(dt.Rows[0]);
                var form = new FormInputObat(obat);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _obatController.EditObat(form.ObatResult);
                    MessageBox.Show("Obat berhasil diupdate!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal edit obat: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapusObat_Click(object sender, EventArgs e)
        {
            if (_selectedObatId == -1)
            {
                MessageBox.Show("Pilih obat yang ingin dihapus.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                "Apakah Anda yakin ingin menonaktifkan obat ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _obatController.HapusObat(_selectedObatId);
                    MessageBox.Show("Obat berhasil dinonaktifkan!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal hapus obat: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}