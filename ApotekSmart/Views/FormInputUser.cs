using System;
using System.Windows.Forms;
using ApotekSmart.Models;

namespace ApotekSmart.Views
{
    public partial class FormInputUser : Form
    {
        public BaseUser UserResult { get; private set; }
        private BaseUser _editUser = null;

        // ── Constructor Tambah ───────────────────────────────
        public FormInputUser()
        {
            InitializeComponent();
            AturForm();
        }

        // ── Constructor Edit ─────────────────────────────────
        public FormInputUser(BaseUser user)
        {
            _editUser = user;
            InitializeComponent();
            AturForm();
            IsiDataEdit();
        }

        private void AturForm()
        {
            // Isi pilihan role
            cmbRole.Items.Clear();
            cmbRole.Items.Add("apoteker");
            cmbRole.Items.Add("kasir");
            cmbRole.SelectedIndex = 0;

            // Update label identitas saat role berubah
            cmbRole.SelectedIndexChanged += (s, e) => UpdateLabelIdentitas();
            UpdateLabelIdentitas();

            btnSimpan.Click += BtnSimpan_Click;
            btnBatal.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Text = _editUser == null ? "Tambah User" : "Edit User";
        }

        private void UpdateLabelIdentitas()
        {
            if (cmbRole.SelectedItem?.ToString() == "apoteker")
            {
                lblIdentitas.Text = "Nomor SIPA (contoh: SIPA-2024-001)";
                txtIdentitas.PlaceholderText = "SIPA-2024-001";
            }
            else
            {
                lblIdentitas.Text = "Nomor Shift (contoh: SHIFT-01)";
                txtIdentitas.PlaceholderText = "SHIFT-01";
            }
        }

        private void IsiDataEdit()
        {
            txtNama.Text = _editUser.Nama;
            txtUsername.Text = _editUser.Username;
            txtPassword.Text = _editUser.GetPassword();
            cmbRole.SelectedItem = _editUser.Role;

            if (_editUser is ApotekSmart.Models.Apoteker a)
                txtIdentitas.Text = a.NomorIdentitas ?? "";
            else if (_editUser is ApotekSmart.Models.Kasir k)
                txtIdentitas.Text = k.NomorShift ?? "";
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            { MessageBox.Show("Nama tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            { MessageBox.Show("Username tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            { MessageBox.Show("Password tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string role = cmbRole.SelectedItem?.ToString();

            try
            {
                if (role == "apoteker")
                {
                    if (string.IsNullOrWhiteSpace(txtIdentitas.Text))
                    { MessageBox.Show("Nomor SIPA tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    var apoteker = new ApotekSmart.Models.Apoteker();
                    if (_editUser != null) apoteker.IdUser = _editUser.IdUser;
                    apoteker.Nama = txtNama.Text.Trim();
                    apoteker.Username = txtUsername.Text.Trim();
                    apoteker.Role = "apoteker";
                    apoteker.IsActive = true;
                    apoteker.NomorIdentitas = txtIdentitas.Text.Trim();
                    apoteker.SetPassword(txtPassword.Text);
                    UserResult = apoteker;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtIdentitas.Text))
                    { MessageBox.Show("Nomor Shift tidak boleh kosong.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    var kasir = new ApotekSmart.Models.Kasir();
                    if (_editUser != null) kasir.IdUser = _editUser.IdUser;
                    kasir.Nama = txtNama.Text.Trim();
                    kasir.Username = txtUsername.Text.Trim();
                    kasir.Role = "kasir";
                    kasir.IsActive = true;
                    kasir.NomorShift = txtIdentitas.Text.Trim();
                    kasir.SetPassword(txtPassword.Text);
                    UserResult = kasir;
                }

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