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

        // [COMPOSITION] semua objek ini hidup dan mati bersama PanelTransaksi
        private List<ItemTransaksi> _keranjang = new List<ItemTransaksi>();
        private Pembayaran _pembayaran = new Pembayaran();
        private TransaksiBiasa _transaksiAktif = null;

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
                UpdateTotal();
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

                DataTable dt = _obatController.GetObatById(idObat);
                if (dt.Rows.Count == 0) return;

                // [POLYMORPHISM] MapRowToObat() kembalikan ObatBebas atau ObatResep
                BaseObat obat = ObatController.MapRowToObat(dt.Rows[0]);

                // Cek apakah obat sudah ada di keranjang
                ItemTransaksi existing = _keranjang.Find(i => i.Obat.IdObat == idObat);
                if (existing != null)
                {
                    existing.Qty += qty;
                }
                else
                {
                    // [AGGREGATION] ItemTransaksi menyimpan referensi BaseObat
                    var item = new ItemTransaksi();
                    item.Obat = obat;
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

        // ── Refresh tampilan DGV dari List<ItemTransaksi> ───
        private void RefreshDGV()
        {
            var dt = new DataTable();
            dt.Columns.Add("Nama Obat", typeof(string));
            dt.Columns.Add("Harga", typeof(decimal));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Subtotal", typeof(decimal));

            foreach (ItemTransaksi item in _keranjang)
            {
                dt.Rows.Add(
                    item.Obat.NamaObat,
                    item.HargaSatuan,
                    item.Qty,
                    item.Subtotal
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

        // ── Update total berdasarkan jenis transaksi ─────────
        private void UpdateTotal()
        {
            string jenis = cmbJenis.SelectedItem?.ToString();

            if (jenis == "biasa")
            {
                // [POLYMORPHISM] TransaksiBiasa.HitungTotal() dari BaseTransaksi
                _transaksiAktif = new TransaksiBiasa();
                _transaksiAktif.IdKasir = _currentUser.IdUser;
                _transaksiAktif.Items = _keranjang;
                _transaksiAktif.HitungTotal();

                _pembayaran.Total = _transaksiAktif.Total;
                lblNilaiTotal.Text = $"Rp {_transaksiAktif.Total:N0}";
            }
            else
            {
                // [POLYMORPHISM] TransaksiResep.HitungTotal() dari BaseTransaksi
                var transaksiResep = new TransaksiResep();
                transaksiResep.Items = _keranjang;
                transaksiResep.HitungTotal();

                _pembayaran.Total = transaksiResep.Total;
                lblNilaiTotal.Text = $"Rp {transaksiResep.Total:N0}";
            }

            UpdateKembalian();
        }

        private void UpdateKembalian()
        {
            try
            {
                // [ENCAPSULATION] Kembalian dihitung otomatis oleh objek Pembayaran
                _pembayaran.JumlahBayar = numJumlahBayar.Value;

                lblNilaiKembalian.Text = $"Rp {_pembayaran.Kembalian:N0}";
                lblNilaiKembalian.ForeColor = _pembayaran.Kembalian >= 0
                    ? Color.FromArgb(39, 174, 96)
                    : Color.FromArgb(226, 75, 74);
            }
            catch (InvalidOperationException)
            {
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

            // Validasi pembayaran lewat objek Pembayaran
            try
            {
                _pembayaran.JumlahBayar = numJumlahBayar.Value;
                decimal kembalian = _pembayaran.Kembalian;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message,
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (jenis == "biasa")
                {
                    // [POLYMORPHISM] Buat objek TransaksiBiasa
                    // ProsesTransaksi() → Status = "selesai"
                    _transaksiAktif = new TransaksiBiasa();
                    _transaksiAktif.IdKasir = _currentUser.IdUser;
                    _transaksiAktif.Items = _keranjang;
                    _transaksiAktif.HitungTotal();
                    _transaksiAktif.ProsesTransaksi();

                    // Kirim ke DB lewat controller
                    var items = new List<object>();
                    foreach (ItemTransaksi item in _keranjang)
                        items.Add(new
                        {
                            id_obat = item.Obat.IdObat,
                            qty = item.Qty
                        });

                    string itemsJson = JsonSerializer.Serialize(items);
                    _transaksiController.ProsesBayar(
                        _currentUser.IdUser, jenis,
                        itemsJson, _pembayaran.JumlahBayar);

                    // [POLYMORPHISM] CetakStruk() milik TransaksiBiasa
                    _transaksiAktif.CetakStruk();

                    MessageBox.Show(
                        $"Transaksi berhasil!\n" +
                        $"Total     : Rp {_pembayaran.Total:N0}\n" +
                        $"Bayar     : Rp {_pembayaran.JumlahBayar:N0}\n" +
                        $"Kembalian : Rp {_pembayaran.Kembalian:N0}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // [POLYMORPHISM] Buat objek TransaksiResep
                    // ProsesTransaksi() → Status = "menunggu"
                    var transaksiResep = new TransaksiResep();
                    transaksiResep.IdKasir = _currentUser.IdUser;
                    transaksiResep.NomorResep = txtNomorResep.Text.Trim();
                    transaksiResep.NamaPasien = txtNamaPasien.Text.Trim();
                    transaksiResep.NamaDokter = txtNamaDokter.Text.Trim();
                    transaksiResep.Items = _keranjang;
                    transaksiResep.HitungTotal();
                    transaksiResep.ProsesTransaksi(); // Status → "menunggu"

                    // Kirim ke DB lewat controller dengan total
                    _transaksiController.BuatTransaksiResep(
                        _currentUser.IdUser,
                        transaksiResep.NomorResep,
                        transaksiResep.NamaPasien,
                        transaksiResep.NamaDokter,
                        transaksiResep.Total); // ← kirim total

                    // [POLYMORPHISM] CetakStruk() milik TransaksiResep
                    transaksiResep.CetakStruk();

                    MessageBox.Show(
                        $"Transaksi resep berhasil dibuat!\n" +
                        $"Nomor Resep : {transaksiResep.NomorResep}\n" +
                        $"Pasien      : {transaksiResep.NamaPasien}\n" +
                        $"Total       : Rp {transaksiResep.Total:N0}\n" +
                        $"Status      : Menunggu validasi apoteker.",
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
            _keranjang.Clear();
            _transaksiAktif = null;
            _pembayaran = new Pembayaran();
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