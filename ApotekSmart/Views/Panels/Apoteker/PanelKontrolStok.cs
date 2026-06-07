using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels.Apoteker
{
    public partial class PanelKontrolStok : UserControl
    {
        private StokController _stokController = new StokController();
        private ObatController _obatController = new ObatController();
        private BaseUser _currentUser;

        public PanelKontrolStok(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            AturDGV();
            MuatObat();
            MuatLogStok();
            MuatStokKritis();
            AturEvent();
        }

        private void AturDGV()
        {
            AturStyleDGV(dgvLogStok);
            AturStyleDGV(dgvStokKritis);
        }

        private void AturStyleDGV(DataGridView dgv)
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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = Color.FromArgb(235, 238, 242);
        }

        private void MuatObat()
        {
            try
            {
                DataTable dt = _obatController.GetAllObat();
                cmbObat.DisplayMember = "nama_obat";
                cmbObat.ValueMember = "id_obat";
                cmbObat.DataSource = dt;
                UpdateInfoStok();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat obat: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateInfoStok()
        {
            if (cmbObat.SelectedValue == null) return;
            try
            {
                int idObat = Convert.ToInt32(cmbObat.SelectedValue);
                DataTable dt = _obatController.GetObatById(idObat);
                if (dt.Rows.Count > 0)
                {
                    int stok = Convert.ToInt32(dt.Rows[0]["stok"]);
                    int min = Convert.ToInt32(dt.Rows[0]["stok_minimum"]);
                    lblInfoStok.Text = $"Stok saat ini: {stok} | Minimum: {min}";
                    lblInfoStok.ForeColor = stok <= min
                        ? Color.FromArgb(226, 75, 74)
                        : Color.FromArgb(39, 174, 96);
                }
            }
            catch { }
        }

        private void MuatLogStok()
        {
            try
            {
                DataTable dt = _stokController.GetLogStok();
                dgvLogStok.DataSource = dt;

                var headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "nama_obat",     "Nama Obat" },
                    { "stok_sebelum",  "Stok Sebelum" },
                    { "stok_sesudah",  "Stok Sesudah" },
                    { "keterangan",    "Keterangan" },
                    { "nama_user",     "User" },
                    { "created_at",    "Waktu" },
                };
                foreach (var h in headers)
                    if (dgvLogStok.Columns.Contains(h.Key))
                        dgvLogStok.Columns[h.Key].HeaderText = h.Value;

                foreach (string col in new[] { "id_log", "id_obat", "id_user" })
                    if (dgvLogStok.Columns.Contains(col))
                        dgvLogStok.Columns[col].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat log stok: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MuatStokKritis()
        {
            try
            {
                DataTable dt = _stokController.GetStokKritis();
                dgvStokKritis.DataSource = dt;

                var headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "nama_obat",     "Nama Obat" },
                    { "nama_kategori", "Kategori" },
                    { "stok",          "Stok" },
                    { "stok_minimum",  "Minimum" },
                    { "status_stok",   "Status" },
                };
                foreach (var h in headers)
                    if (dgvStokKritis.Columns.Contains(h.Key))
                        dgvStokKritis.Columns[h.Key].HeaderText = h.Value;

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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat stok kritis: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AturEvent()
        {
            cmbObat.SelectedIndexChanged += (s, e) => UpdateInfoStok();
            btnUpdateStok.Click += BtnUpdateStok_Click;
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl.SelectedIndex == 1) MuatLogStok();
                if (tabControl.SelectedIndex == 2) MuatStokKritis();
            };
        }

        private void BtnUpdateStok_Click(object sender, EventArgs e)
        {
            if (cmbObat.SelectedValue == null)
            {
                MessageBox.Show("Pilih obat terlebih dahulu.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtKeterangan.Text))
            {
                MessageBox.Show("Keterangan tidak boleh kosong.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idObat = Convert.ToInt32(cmbObat.SelectedValue);
                int qty = (int)numQtyMasuk.Value;
                string ket = txtKeterangan.Text.Trim();
                int idUser = _currentUser.IdUser;

                _stokController.UpdateStok(idObat, qty, ket, idUser);

                MessageBox.Show("Stok berhasil diupdate!",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtKeterangan.Clear();
                numQtyMasuk.Value = 1;
                UpdateInfoStok();
                MuatLogStok();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal update stok: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}