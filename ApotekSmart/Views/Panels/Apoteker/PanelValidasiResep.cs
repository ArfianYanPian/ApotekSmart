using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels.Apoteker
{
    public partial class PanelValidasiResep : UserControl
    {
        private ResepController _resepController = new ResepController();
        private BaseUser _currentUser;
        private int _selectedResepId = -1;

        public PanelValidasiResep(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            AturDGV();
            AturEvent();
            MuatData();
        }

        private void AturDGV()
        {
            dgvResep.ReadOnly = true;
            dgvResep.AllowUserToAddRows = false;
            dgvResep.AllowUserToDeleteRows = false;
            dgvResep.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResep.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResep.BackgroundColor = Color.White;
            dgvResep.BorderStyle = BorderStyle.None;
            dgvResep.RowHeadersVisible = false;
            dgvResep.Font = new Font("Segoe UI", 9f);
            dgvResep.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvResep.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvResep.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResep.EnableHeadersVisualStyles = false;
            dgvResep.GridColor = Color.FromArgb(235, 238, 242);
        }

        private void MuatData()
        {
            try
            {
                DataTable dt = _resepController.GetResepMenunggu();
                dgvResep.DataSource = dt;

                foreach (string col in new[] { "id_resep", "id_transaksi",
                    "id_apoteker", "validated_at", "catatan_apoteker" })
                    if (dgvResep.Columns.Contains(col))
                        dgvResep.Columns[col].Visible = false;

                if (dgvResep.Columns.Contains("nomor_resep"))
                    dgvResep.Columns["nomor_resep"].HeaderText = "No. Resep";
                if (dgvResep.Columns.Contains("nama_pasien"))
                    dgvResep.Columns["nama_pasien"].HeaderText = "Pasien";
                if (dgvResep.Columns.Contains("nama_dokter"))
                    dgvResep.Columns["nama_dokter"].HeaderText = "Dokter";
                if (dgvResep.Columns.Contains("status_validasi"))
                    dgvResep.Columns["status_validasi"].HeaderText = "Status";
                if (dgvResep.Columns.Contains("created_at"))
                    dgvResep.Columns["created_at"].HeaderText = "Tgl Masuk";

                _selectedResepId = -1;
                ResetDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat resep: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AturEvent()
        {
            dgvResep.SelectionChanged += DgvResep_SelectionChanged;
            btnSetujui.Click += BtnSetujui_Click;
            btnTolak.Click += BtnTolak_Click;
        }

        private void DgvResep_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResep.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvResep.SelectedRows[0];
            _selectedResepId = Convert.ToInt32(row.Cells["id_resep"].Value);

            lblNilaiNomorResep.Text = row.Cells["nomor_resep"].Value?.ToString() ?? "-";
            lblNilaiNamaPasien.Text = row.Cells["nama_pasien"].Value?.ToString() ?? "-";
            lblNilaiNamaDokter.Text = row.Cells["nama_dokter"].Value?.ToString() ?? "-";
            txtCatatan.Text = "";
        }

        private void ResetDetail()
        {
            lblNilaiNomorResep.Text = "-";
            lblNilaiNamaPasien.Text = "-";
            lblNilaiNamaDokter.Text = "-";
            txtCatatan.Text = "";
        }

        private void BtnSetujui_Click(object sender, EventArgs e)
        {
            if (_selectedResepId == -1)
            {
                MessageBox.Show("Pilih resep yang ingin divalidasi.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Apakah Anda yakin ingin menyetujui resep ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _resepController.ValidasiResep(
                        _selectedResepId, "disetujui",
                        _currentUser.IdUser, txtCatatan.Text);

                    MessageBox.Show("Resep berhasil disetujui!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal setujui resep: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTolak_Click(object sender, EventArgs e)
        {
            if (_selectedResepId == -1)
            {
                MessageBox.Show("Pilih resep yang ingin ditolak.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCatatan.Text))
            {
                MessageBox.Show("Catatan wajib diisi jika menolak resep.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Apakah Anda yakin ingin menolak resep ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _resepController.ValidasiResep(
                        _selectedResepId, "ditolak",
                        _currentUser.IdUser, txtCatatan.Text);

                    MessageBox.Show("Resep berhasil ditolak.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal tolak resep: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}