using System;
using System.Data;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels
{
    public partial class FormInputObat : Form
    {
        public BaseObat ObatResult { get; private set; }
        private BaseObat _editObat = null;
        private ObatController _obatController = new ObatController();

        // ── Constructor Tambah ───────────────────────────────
        public FormInputObat()
        {
            InitializeComponent();
            AturForm();
        }

        // ── Constructor Edit ─────────────────────────────────
        public FormInputObat(BaseObat obat)
        {
            _editObat = obat;
            InitializeComponent();
            AturForm();
            IsiDataEdit();
        }

        private void AturForm()
        {
            this.Text = _editObat == null ? "Tambah Obat" : "Edit Obat";

            // Isi ComboBox Jenis
            cmbJenis.Items.Clear();
            cmbJenis.Items.Add("bebas");
            cmbJenis.Items.Add("resep");
            cmbJenis.SelectedIndex = 0;

            // Isi ComboBox Golongan
            cmbGolongan.Items.Clear();
            cmbGolongan.Items.Add("-");
            cmbGolongan.Items.Add("Keras");
            cmbGolongan.Items.Add("Psikotropika");
            cmbGolongan.Items.Add("Narkotika");
            cmbGolongan.SelectedIndex = 0;

            // Golongan hanya aktif kalau jenis = resep
            cmbJenis.SelectedIndexChanged += (s, e) =>
            {
                bool isResep = cmbJenis.SelectedItem?.ToString() == "resep";
                cmbGolongan.Enabled = isResep;
                lblGolongan.ForeColor = isResep
                    ? System.Drawing.Color.FromArgb(85, 85, 85)
                    : System.Drawing.Color.LightGray;
            };
            cmbGolongan.Enabled = false;

            // Tanggal exp minimal hari ini
            dtpTanggalExp.MinDate = DateTime.Today;
            dtpTanggalExp.Value = DateTime.Today.AddYears(1);

            // Muat kategori dari DB
            MuatKategori();

            btnSimpan.Click += BtnSimpan_Click;
            btnBatal.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void MuatKategori()
        {
            try
            {
                DataTable dt = _obatController.GetAllKategori();
                cmbKategori.DisplayMember = "nama_kategori";
                cmbKategori.ValueMember = "id_kategori";
                cmbKategori.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat kategori: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IsiDataEdit()
        {
            txtNamaObat.Text = _editObat.NamaObat;
            txtSatuan.Text = _editObat.Satuan;
            numHargaBeli.Value = _editObat.HargaBeli;
            numHargaJual.Value = _editObat.HargaJual;
            numStok.Value = _editObat.Stok;
            numStokMin.Value = _editObat.StokMinimum;
            dtpTanggalExp.Value = _editObat.TanggalExp < DateTime.Today
                ? DateTime.Today : _editObat.TanggalExp;
            cmbJenis.SelectedItem = _editObat.Jenis;

            if (_editObat is ObatResep or_)
            {
                cmbGolongan.Enabled = true;
                cmbGolongan.SelectedItem = or_.GolonganObat ?? "-";
            }

            // Set kategori
            if (cmbKategori.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["id_kategori"]) == _editObat.IdKategori)
                    {
                        cmbKategori.SelectedValue = _editObat.IdKategori;
                        break;
                    }
                }
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi
            if (string.IsNullOrWhiteSpace(txtNamaObat.Text))
            { MessageBox.Show("Nama obat tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrWhiteSpace(txtSatuan.Text))
            { MessageBox.Show("Satuan tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (numHargaJual.Value < numHargaBeli.Value)
            { MessageBox.Show("Harga jual tidak boleh lebih kecil dari harga beli.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string jenis = cmbJenis.SelectedItem?.ToString();
            if (jenis == "resep" && cmbGolongan.SelectedItem?.ToString() == "-")
            { MessageBox.Show("Golongan obat resep harus dipilih.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                BaseObat obat;

                if (jenis == "resep")
                {
                    var obatResep = new ObatResep();
                    obatResep.GolonganObat = cmbGolongan.SelectedItem.ToString();
                    obat = obatResep;
                }
                else
                {
                    obat = new ObatBebas();
                }

                if (_editObat != null) obat.IdObat = _editObat.IdObat;

                obat.IdKategori = Convert.ToInt32(cmbKategori.SelectedValue);
                obat.NamaObat = txtNamaObat.Text.Trim();
                obat.Jenis = jenis;
                obat.Satuan = txtSatuan.Text.Trim();
                obat.HargaBeli = numHargaBeli.Value;
                obat.HargaJual = numHargaJual.Value;
                obat.Stok = (int)numStok.Value;
                obat.StokMinimum = (int)numStokMin.Value;
                obat.TanggalExp = dtpTanggalExp.Value.Date;
                obat.IsActive = true;

                obat.Validate();
                ObatResult = obat;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input tidak valid: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}