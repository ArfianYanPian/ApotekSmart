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

        // FIX: Ganti DataTable _keranjang ke List<ItemTransaksi>
        // Ini penerapan COMPOSITION — ItemTransaksi hidup dan mati bersama PanelTransaksi
        private List<ItemTransaksi> _keranjang = new List<ItemTransaksi>();

        // FIX: Tambah objek Pembayaran untuk kelola pembayaran
        private Pembayaran _pembayaran = new Pembayaran();

        public PanelTransaksi(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            InitDGV();
            MuatObat();
            AturEvent();

            numJumlahBayar.Minimum = 0;
            numJumlahBayar.Maximum = 999999999;
            numJumlahBayar.Value = 0;
        }

        // ── Atur DataGridView ────────────────────────────────
        private void InitDGV()
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

            try
            {
                int idObat = Convert.ToInt32(cmbObat.SelectedValue);
                int qty = (int)numQty.Value;

                // Ambil data obat dari DB
                DataTable dt = _obatController.GetObatById(idObat);
                if (dt.Rows.Count == 0) return;

                // FIX: Map DataRow ke BaseObat (ObatBebas atau ObatResep)
                // Ini penerapan POLYMORPHISM — MapRowToObat() kembalikan tipe yang tepat
                BaseObat obat = ObatController.MapRowToObat(dt.Rows[0]);

                // Cek apakah obat sudah ada di keranjang
                ItemTransaksi existing = _keranjang.Find(i => i.Obat.IdObat == idObat);
                if (existing != null)
                {
                    // Update qty item yang sudah ada
                    existing.Qty += qty;
                }
                else
                {
                    // FIX: Buat ItemTransaksi baru — bukan DataRow
                    // Ini penerapan CLASS MODEL yang benar
                    var item = new ItemTransaksi();
                    item.Obat = obat;         // AGGREGATION — obat tetap hidup
                    item.Qty = qty;
                    item.HargaSatuan = obat.HargaJual;
                    _keranjang.Add(item);
                }

                RefreshDGV();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal tambah item: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Refresh tampilan DataGridView dari List<ItemTransaksi> ──
        private void RefreshDGV()
        {
            // FIX: Build DataTable dari List<ItemTransaksi>
            // untuk ditampilkan di DataGridView
            var dt = new DataTable();
            dt.Columns.Add("Nama Obat", typeof(string));
            dt.Columns.Add("Harga", typeof(decimal));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Subtotal", typeof(decimal));

            foreach (ItemTransaksi item in _keranjang)
            {
                dt.Rows.Add(
                    item.Obat.NamaObat,   // dari property BaseObat
                    item.HargaSatuan,
                    item.Qty,
                    item.Subtotal         // read-only, dihitung otomatis
                );
            }

            dgvKeranjang.DataSource = dt;
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
            _keranjang.RemoveAt(idx);
            RefreshDGV();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            // FIX: Pakai HitungTotal() dari BaseTransaksi pattern
            // Total dihitung dari Subtotal tiap ItemTransaksi
            decimal total = 0;
            foreach (ItemTransaksi item in _keranjang)
                total += item.Subtotal; // Subtotal read-only di ItemTransaksi

            // FIX: Simpan total ke objek Pembayaran
            _pembayaran.Total = total;
            lblNilaiTotal.Text = $"Rp {total:N0}";
            UpdateKembalian();
        }

        private void UpdateKembalian()
        {
            try
            {
                // FIX: Pakai objek Pembayaran untuk hitung kembalian
                // Kembalian adalah read-only property di Pembayaran
                _pembayaran.JumlahBayar = numJumlahBayar.Value;

                lblNilaiKembalian.Text = $"Rp {_pembayaran.Kembalian:N0}";
                lblNilaiKembalian.ForeColor = _pembayaran.Kembalian >= 0
                    ? Color.FromArgb(39, 174, 96)
                    : Color.FromArgb(226, 75, 74);
            }
            catch (InvalidOperationException)
            {
                // Jumlah bayar kurang dari total
                lblNilaiKembalian.Text = "Kurang!";
                lblNilaiKembalian.ForeColor = Color.FromArgb(226, 75, 74);
            }
        }

        // ── Proses bayar ─────────────────────────────────────
        private void BtnProsesBayar_Click(object sender, EventArgs e)
        {
            if (_keranjang.Count == 0)
            {
                MessageBox.Show("Keranjang masih kosong.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jenis = cmbJenis.SelectedItem?.ToString();

            if (jenis == "resep")
            {
                if (string.IsNullOrWhiteSpace(txtNomorResep.Text))
                { MessageBox.Show("Nomor resep tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrWhiteSpace(txtNamaPasien.Text))
                { MessageBox.Show("Nama pasien tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrWhiteSpace(txtNamaDokter.Text))
                { MessageBox.Show("Nama dokter tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            }

            // FIX: Validasi pembayaran lewat objek Pembayaran
            try
            {
                _pembayaran.JumlahBayar = numJumlahBayar.Value;
                decimal kembalian = _pembayaran.Kembalian; // throw jika kurang
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message,
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (jenis == "resep")
                {
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
                    // FIX: Build JSON dari List<ItemTransaksi>
                    var items = new List<object>();
                    foreach (ItemTransaksi item in _keranjang)
                        items.Add(new
                        {
                            id_obat = item.Obat.IdObat,  // dari property BaseObat
                            qty = item.Qty
                        });

                    string itemsJson = JsonSerializer.Serialize(items);

                    _transaksiController.ProsesBayar(
                        _currentUser.IdUser, jenis,
                        itemsJson, _pembayaran.JumlahBayar);

                    MessageBox.Show(
                        $"Transaksi berhasil!\n" +
                        $"Total     : Rp {_pembayaran.Total:N0}\n" +
                        $"Bayar     : Rp {_pembayaran.JumlahBayar:N0}\n" +
                        $"Kembalian : Rp {_pembayaran.Kembalian:N0}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal proses transaksi: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Reset form setelah transaksi ─────────────────────
        private void ResetForm()
        {
            // FIX: Reset List<ItemTransaksi> dan objek Pembayaran
            _keranjang.Clear();
            _pembayaran = new Pembayaran(); // reset ke default
            dgvKeranjang.DataSource = null;
            numJumlahBayar.Value = 0;
            lblNilaiTotal.Text = "Rp 0";
            lblNilaiKembalian.Text = "Rp 0";
            txtNomorResep.Clear();
            txtNamaPasien.Clear();
            txtNamaDokter.Clear();
            cmbJenis.SelectedIndex = 0;
        }
    }
}