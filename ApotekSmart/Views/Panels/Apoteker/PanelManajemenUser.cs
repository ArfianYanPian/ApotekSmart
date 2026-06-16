using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views.Panels.Apoteker
{
    public partial class PanelManajemenUser : UserControl
    {
        private UserController _userController = new UserController();
        private int _selectedUserId = -1;

        public PanelManajemenUser()
        {
            InitializeComponent();
            AturDGV();
            MuatData();
            AturEvent();
        }

        private void AturDGV()
        {
            dgvUser.ReadOnly = true;
            dgvUser.AllowUserToAddRows = false;
            dgvUser.AllowUserToDeleteRows = false;
            dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUser.BackgroundColor = Color.White;
            dgvUser.BorderStyle = BorderStyle.None;
            dgvUser.RowHeadersVisible = false;
            dgvUser.Font = new Font("Segoe UI", 9f);
            dgvUser.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvUser.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgvUser.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUser.EnableHeadersVisualStyles = false;
            dgvUser.GridColor = Color.FromArgb(235, 238, 242);
        }

        private void MuatData(string keyword = "")
        {
            try
            {
                var users = _userController.ReadAll();
                var dt = new DataTable();
                dt.Columns.Add("id_user", typeof(int));
                dt.Columns.Add("Nama", typeof(string));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("Role", typeof(string));
                dt.Columns.Add("Identitas", typeof(string));
                dt.Columns.Add("Shift", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                foreach (var u in users)
                {
                    if (!string.IsNullOrWhiteSpace(keyword) &&
                        !u.Nama.ToLower().Contains(keyword.ToLower()) &&
                        !u.Username.ToLower().Contains(keyword.ToLower()))
                        continue;

                    string identitas = u is ApotekSmart.Models.Apoteker a ? a.NomorIdentitas : "-";
                    string shift = u is ApotekSmart.Models.Kasir k ? k.NomorShift : "-";

                    dt.Rows.Add(u.IdUser, u.Nama, u.Username,
                        u.Role.ToUpper(), identitas, shift,
                        u.IsActive ? "Aktif" : "Nonaktif");
                }

                dgvUser.DataSource = dt;
                if (dgvUser.Columns.Contains("id_user"))
                    dgvUser.Columns["id_user"].Visible = false;

                _selectedUserId = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data user: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AturEvent()
        {
            btnTambahUser.Click += BtnTambahUser_Click;
            btnEditUser.Click += BtnEditUser_Click;
            btnHapusUser.Click += BtnHapusUser_Click;
            txtCariUser.TextChanged += (s, e) => MuatData(txtCariUser.Text);
            dgvUser.SelectionChanged += (s, e) =>
            {
                if (dgvUser.SelectedRows.Count > 0)
                    _selectedUserId = Convert.ToInt32(
                        dgvUser.SelectedRows[0].Cells["id_user"].Value);
            };
        }

        private void BtnTambahUser_Click(object sender, EventArgs e)
        {
            var form = new FormInputUser();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _userController.Create(form.UserResult);
                    MessageBox.Show("User berhasil ditambahkan!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal tambah user: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Pilih user yang ingin diedit.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                BaseUser user = _userController.ReadById(_selectedUserId);
                var form = new FormInputUser(user);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _userController.Update(form.UserResult);
                    MessageBox.Show("User berhasil diupdate!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal edit user: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapusUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Pilih user yang ingin dihapus.",
                    "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show(
                "Apakah Anda yakin ingin menonaktifkan user ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _userController.Delete(_selectedUserId);
                    MessageBox.Show("User berhasil dinonaktifkan!",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MuatData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal hapus user: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}