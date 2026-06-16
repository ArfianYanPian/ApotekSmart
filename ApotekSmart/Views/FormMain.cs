using System;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Models;
using ApotekSmart.Views.Panels;
using ApotekSmart.Views.Panels.Apoteker;
using ApotekSmart.Views.Panels.Kasir;

namespace ApotekSmart.Views
{
    public partial class FormMain : Form
    {
        private BaseUser _currentUser;
        private Button _activeMenuBtn = null;

        // ── Warna tema ────────────────────────────────────────
        private readonly Color CLR_SIDEBAR_HOV = Color.FromArgb(10, 42, 74);
        private readonly Color CLR_SIDEBAR_ACT = Color.FromArgb(10, 32, 64);

        public FormMain(BaseUser user)
        {
            _currentUser = user;
            InitializeComponent();
            AturTampilan();
            BuildMenu();
            ShowDashboard();
        }

        // ── Atur tampilan awal ───────────────────────────────
        private void AturTampilan()
        {
            // Isi info user di sidebar dan topbar
            lblUserInfo.Text = $"👤 {_currentUser.Nama}\n    {_currentUser.Role.ToUpper()}";
            lblTopUserInfo.Text = $"{_currentUser.Nama}  |  {_currentUser.Role.ToUpper()}";

            // Garis bawah topbar
            pnlTopbar.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(
                    new System.Drawing.Pen(Color.FromArgb(220, 225, 235)),
                    0, pnlTopbar.Height - 1,
                    pnlTopbar.Width, pnlTopbar.Height - 1);
            };
        }

        // ────────────────────────────────────────────────────
        //  BUILD MENU SESUAI ROLE
        // ────────────────────────────────────────────────────
        private void BuildMenu()
        {
            pnlMenuArea.Controls.Clear();
            int y = 10;

            if (_currentUser.Role == "apoteker")
            {
                y = AddMenuHeader("UTAMA", y);
                y = AddMenuButton("🏠  Dashboard", y, ShowDashboard);
                y = AddMenuHeader("MASTER DATA", y);
                y = AddMenuButton("👥  Manajemen User", y, ShowManajemenUser);
                y = AddMenuButton("💊  Manajemen Obat", y, ShowManajemenObat);
                y = AddMenuHeader("OPERASIONAL", y);
                y = AddMenuButton("📋  Validasi Resep", y, ShowValidasiResep);
                y = AddMenuButton("📦  Kontrol Stok", y, ShowKontrolStok);
                y = AddMenuHeader("LAPORAN", y);
                y = AddMenuButton("📊  Laporan Detail", y, ShowLaporanApoteker);
            }
            else if (_currentUser.Role == "kasir")
            {
                y = AddMenuHeader("UTAMA", y);
                y = AddMenuButton("🏠  Dashboard", y, ShowDashboard);
                y = AddMenuHeader("PELAYANAN", y);
                y = AddMenuButton("🔍  Cari Obat", y, ShowCariObat);
                y = AddMenuButton("🛒  Transaksi", y, ShowTransaksi);
                y = AddMenuHeader("LAPORAN", y);
                y = AddMenuButton("📄  Laporan Harian", y, ShowLaporanKasir);
            }
        }

        private int AddMenuHeader(string text, int y)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(122, 174, 212);
            lbl.AutoSize = true;
            lbl.Location = new Point(20, y);
            pnlMenuArea.Controls.Add(lbl);
            return y + 24;
        }

        private int AddMenuButton(string text, int y, Action onClick)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Size = new Size(210, 38);
            btn.Location = new Point(5, y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = CLR_SIDEBAR_HOV;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9f);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.Click += (s, e) =>
            {
                SetActiveMenu(btn);
                onClick?.Invoke();
            };

            pnlMenuArea.Controls.Add(btn);
            return y + 42;
        }

        private void SetActiveMenu(Button btn)
        {
            foreach (Control c in pnlMenuArea.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.Transparent;
                    b.Font = new Font("Segoe UI", 9f);
                }
            }
            btn.BackColor = CLR_SIDEBAR_ACT;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _activeMenuBtn = btn;
        }

        // ────────────────────────────────────────────────────
        //  NAVIGASI
        // ────────────────────────────────────────────────────
        private void LoadPanel(UserControl panel, string pageTitle)
        {
            lblPageTitle.Text = pageTitle;
            pnlContent.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(panel);
        }

        private void ShowDashboard()
        {
            LoadPanel(new PanelDashboard(_currentUser), "Dashboard");
        }

        private void ShowManajemenUser()
        {
            LoadPanel(new PanelManajemenUser(), "Manajemen User");
        }

        private void ShowManajemenObat()
        {
            LoadPanel(new PanelManajemenObat(), "Manajemen Obat");
        }

        private void ShowValidasiResep()
        {
            LoadPanel(new PanelValidasiResep(_currentUser), "Validasi Resep");
        }

        private void ShowKontrolStok()
        {
            LoadPanel(new PanelKontrolStok(_currentUser), "Kontrol Stok");
        }

        private void ShowLaporanApoteker()
        {
            LoadPanel(new PanelLaporanApoteker(), "Laporan Detail");
        }

        private void ShowCariObat()
        {
            LoadPanel(new PanelCariObat(), "Cari Obat");
        }

        private void ShowTransaksi()
        {
            LoadPanel(new PanelTransaksi(_currentUser), "Transaksi Penjualan");
        }

        private void ShowLaporanKasir()
        {
            LoadPanel(new PanelLaporanKasir(), "Laporan Harian");
        }

        // ────────────────────────────────────────────────────
        //  LOGOUT
        // ────────────────────────────────────────────────────
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
                this.Close();
            }
        }
    }
}