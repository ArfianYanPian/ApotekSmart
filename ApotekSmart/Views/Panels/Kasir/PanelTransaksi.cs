using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels.Kasir
{
    public partial class PanelTransaksi : UserControl
    {
        private ObatController _obatController = new ObatController();
        private TransaksiController _transaksiController = new TransaksiController();
        private BaseUser _currentUser;

        // Keranjang belanja
        private DataTable _keranjang = new DataTable();

        public PanelTransaksi(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            InitKeranjang();
            AturDGV();
            MuatObat();
            AturEvent();

            numJumlahBayar.Minimum = 0;
            numJumlahBayar.Maximum = 999999999;
            numJumlahBayar.Value = 0;
        }

        // ── Inisialisasi tabel keranjang ─────────────────────
        private void InitKeranjang()
        {
            _keranjang.Columns.Add("id_obat", typeof(int));
            _keranjang.Columns.Add("Nama Obat", typeof(string));
            _keranjang.Columns.Add("Harga", typeof(decimal));
            _keranjang.Columns.Add("Qty", typeof(int));
            _keranjang.Columns.Add("Subtotal", typeof(decimal));
        }

        private void AturDGV()
        {
            dgvKeranjang.ReadOnly = true;
            dgvKeranjang.AllowUserToAddRows = false;
            dgvKeranjang.AllowUserToDeleteRows = false;
            dgvKeranjang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKeranjang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKeranjang.BackgroundColor = Color.White;
            dgvKeranjang.BorderStyle = BorderStyle.None;
            dgvKeranjang.RowHeadersVisible = false;
            dgvKeranjang.Font = new Font("Segoe UI", 9f);
            dgvKeranjang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvKeranjang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvKeranjang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKeranjang.EnableHeadersVisualStyles = false;
            dgvKeranjang.GridColor = Color.FromArgb(235, 238, 242);
            dgvKeranjang.DataSource = _keranjang;

            // Sembunyikan kolom id
            if (dgvKeranjang.Columns.Contains("id_obat"))
                dgvKeranjang.Columns["id_obat"].Visible = false;
        }

        private void MuatObat()
        {
            try
            {
                DataTable dt = _obatController.GetAllObat();
                cmbObat.DisplayMember = "nama_obat";
                cmbObat.ValueMember = "id_obat";
                cmbObat.DataSource = dt;
                UpdateInfoObat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat obat: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateInfoObat()
        {
            if (cmbObat.SelectedValue == null) return;
            try
            {
                int idObat = Convert.ToInt32(cmbObat.SelectedValue);
                DataTable dt = _obatController.GetObatById(idObat);
                if (dt.Rows.Count > 0)
                {
                    decimal harga = Convert.ToDecimal(dt.Rows[0]["harga_jual"]);
                    int stok = Convert.ToInt32(dt.Rows[0]["stok"]);

                    lblNilaiHarga.Text = $"Rp {harga:N0}";
                    lblInfoStok.Text = $"Stok: {stok}";
                    lblInfoStok.ForeColor = stok <= 0
                        ? Color.FromArgb(226, 75, 74)
                        : Color.Gray;
                    numQty.Maximum = stok > 0 ? stok : 1;
                }
            }
            catch { }
        }

        private void AturEvent()
        {
            cmbObat.SelectedIndexChanged += (s, e) => UpdateInfoObat();

            cmbJenis.Items.Add("biasa");
            cmbJenis.Items.Add("resep");
            cmbJenis.SelectedIndex = 0;
            cmbJenis.SelectedIndexChanged += (s, e) =>
            {
                pnlInfoResep.Visible = cmbJenis.SelectedItem?.ToString() == "resep";
            };

            btnTambahItem.Click += BtnTambahItem_Click;
            btnHapusItem.Click += BtnHapusItem_Click;
            btnProsesBayar.Click += BtnProsesBayar_Click;

            numJumlahBayar.ValueChanged += (s, e) => UpdateKembalian();
        }

        // ── Tambah item ke keranjang ─────────────────────────
        private void BtnTambahItem_Click(object sender, EventArgs e)
        {
            if (cmbObat.SelectedValue == null) return;

            int idObat = Convert.ToInt32(cmbObat.SelectedValue);
            string nama = cmbObat.Text;
            int qty = (int)numQty.Value;
            decimal harga = 0;

            // Ambil harga dari DataSource
            DataTable dt = (DataTable)cmbObat.DataSource;
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["id_obat"]) == idObat)
                {
                    harga = Convert.ToDecimal(row["harga_jual"]);
                    break;
                }
            }

            // Cek apakah obat sudah ada di keranjang
            foreach (DataRow row in _keranjang.Rows)
            {
                if (Convert.ToInt32(row["id_obat"]) == idObat)
                {
                    row["Qty"] = Convert.ToInt32(row["Qty"]) + qty;
                    row["Subtotal"] = Convert.ToDecimal(row["Harga"]) *
                                      Convert.ToInt32(row["Qty"]);
                    UpdateTotal();
                    return;
                }
            }

            // Tambah baris baru
            _keranjang.Rows.Add(idObat, nama, harga, qty, harga * qty);
            UpdateTotal();
        }

        // ── Hapus item dari keranjang ────────────────────────
        private void BtnHapusItem_Click(object sender, EventArgs e)
        {
            if (dgvKeranjang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih item yang ingin dihapus.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idx = dgvKeranjang.SelectedRows[0].Index;
            _keranjang.Rows.RemoveAt(idx);
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in _keranjang.Rows)
                total += Convert.ToDecimal(row["Subtotal"]);

            lblNilaiTotal.Text = $"Rp {total:N0}";

            // Jangan ubah value numJumlahBayar sama sekali
            // Biarkan kasir input manual
            UpdateKembalian();
        }

        private void UpdateKembalian()
        {
            decimal total = 0;
            foreach (DataRow row in _keranjang.Rows)
                total += Convert.ToDecimal(row["Subtotal"]);

            decimal bayar = numJumlahBayar.Value;
            decimal kembalian = bayar - total;

            lblNilaiKembalian.Text = $"Rp {kembalian:N0}";
            lblNilaiKembalian.ForeColor = kembalian >= 0
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(226, 75, 74);
        }

        // ── Proses bayar ─────────────────────────────────────
        private void BtnProsesBayar_Click(object sender, EventArgs e)
        {
            if (_keranjang.Rows.Count == 0)
            {
                MessageBox.Show("Keranjang masih kosong.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jenis = cmbJenis.SelectedItem?.ToString();

            // Validasi resep
            if (jenis == "resep")
            {
                if (string.IsNullOrWhiteSpace(txtNomorResep.Text))
                { MessageBox.Show("Nomor resep tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrWhiteSpace(txtNamaPasien.Text))
                { MessageBox.Show("Nama pasien tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrWhiteSpace(txtNamaDokter.Text))
                { MessageBox.Show("Nama dokter tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            }

            decimal total = 0;
            foreach (DataRow row in _keranjang.Rows)
                total += Convert.ToDecimal(row["Subtotal"]);

            if (numJumlahBayar.Value < total)
            {
                MessageBox.Show("Jumlah bayar kurang dari total.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (jenis == "resep")
                {
                    // Buat transaksi resep dulu
                    _transaksiController.BuatTransaksiResep(
                        _currentUser.IdUser,
                        txtNomorResep.Text.Trim(),
                        txtNamaPasien.Text.Trim(),
                        txtNamaDokter.Text.Trim());

                    MessageBox.Show("Transaksi resep berhasil dibuat!\nMenunggu validasi apoteker.",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Buat JSON items untuk SP
                    var items = new List<object>();
                    foreach (DataRow row in _keranjang.Rows)
                        items.Add(new { id_obat = row["id_obat"], qty = row["Qty"] });

                    string itemsJson = System.Text.Json.JsonSerializer.Serialize(items);

                    _transaksiController.ProsesBayar(
                        _currentUser.IdUser, jenis,
                        itemsJson, numJumlahBayar.Value);

                    MessageBox.Show(
                        $"Transaksi berhasil!\nTotal: Rp {total:N0}\nKembalian: Rp {numJumlahBayar.Value - total:N0}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Reset form
                _keranjang.Rows.Clear();
                numJumlahBayar.Minimum = 0;
                numJumlahBayar.Value = 0;
                lblNilaiTotal.Text = "Rp 0";
                lblNilaiKembalian.Text = "Rp 0";
                txtNomorResep.Clear();
                txtNamaPasien.Clear();
                txtNamaDokter.Clear();
                cmbJenis.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal proses transaksi: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}