using System;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Models;
using ApotekSmart.Controllers;

namespace ApotekSmart.Views
{
    public partial class FormMain : Form
    {
        private BaseUser _currentUser;

        // ── Warna tema ────────────────────────────────────────
        private readonly Color CLR_SIDEBAR = Color.FromArgb(30, 58, 95);    // #1E3A5F
        private readonly Color CLR_SIDEBAR_HOV = Color.FromArgb(10, 42, 74);    // #0A2A4A
        private readonly Color CLR_SIDEBAR_ACT = Color.FromArgb(10, 32, 64);    // #0A2040
        private readonly Color CLR_TOPBAR = Color.White;
        private readonly Color CLR_CONTENT = Color.FromArgb(245, 247, 250); // #F5F7FA
        private readonly Color CLR_ACCENT = Color.FromArgb(30, 58, 95);
        private readonly Color CLR_TEXT_LIGHT = Color.FromArgb(122, 174, 212);

        // ── Layout utama ─────────────────────────────────────
        private Panel pnlSidebar = new Panel();
        private Panel pnlTopbar = new Panel();
        private Panel pnlContent = new Panel();

        // ── Sidebar komponen ─────────────────────────────────
        private Label lblSidebarLogo = new Label();
        private Label lblSidebarSub = new Label();
        private Panel pnlMenuArea = new Panel();
        private Label lblUserInfo = new Label();
        private Button btnLogout = new Button();

        // ── Topbar komponen ──────────────────────────────────
        private Label lblPageTitle = new Label();
        private Label lblTopUserInfo = new Label();

        // ── Tombol menu aktif sekarang ───────────────────────
        private Button _activeMenuBtn = null;

        public FormMain(BaseUser user)
        {
            _currentUser = user;
            InitializeForm();
            BuildUI();
            BuildMenu();
            // Tampilkan dashboard sebagai halaman awal
            ShowDashboard();
        }

        // ── Properti Form ────────────────────────────────────
        private void InitializeForm()
        {
            this.Text = "ApotekSmart";
            this.Size = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = CLR_CONTENT;
            this.Font = new Font("Segoe UI", 9f);
        }

        // ────────────────────────────────────────────────────
        //  BANGUN LAYOUT UTAMA
        // ────────────────────────────────────────────────────
        private void BuildUI()
        {
            BuildSidebar();
            BuildTopbar();
            BuildContentArea();

            // Urutan: Content dulu (Fill), lalu Topbar (Top), lalu Sidebar (Left)
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlTopbar);
            this.Controls.Add(pnlSidebar);
        }

        // ────────────────────────────────────────────────────
        //  SIDEBAR
        // ────────────────────────────────────────────────────
        private void BuildSidebar()
        {
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 220;
            pnlSidebar.BackColor = CLR_SIDEBAR;

            // Logo
            lblSidebarLogo.Text = "ApotekSmart";
            lblSidebarLogo.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblSidebarLogo.ForeColor = Color.White;
            lblSidebarLogo.AutoSize = true;
            lblSidebarLogo.Location = new Point(20, 20);

            lblSidebarSub.Text = "Manajemen Apotek";
            lblSidebarSub.Font = new Font("Segoe UI", 8f);
            lblSidebarSub.ForeColor = CLR_TEXT_LIGHT;
            lblSidebarSub.AutoSize = true;
            lblSidebarSub.Location = new Point(20, 44);

            // Garis pemisah logo
            var sep1 = new Panel();
            sep1.Size = new Size(180, 1);
            sep1.Location = new Point(20, 68);
            sep1.BackColor = Color.FromArgb(42, 80, 128);

            // Area menu (scrollable jika banyak)
            pnlMenuArea.Location = new Point(0, 80);
            pnlMenuArea.Size = new Size(220, 580);
            pnlMenuArea.BackColor = Color.Transparent;
            pnlMenuArea.AutoScroll = true;

            // Info user di bawah sidebar
            lblUserInfo.Text = $"👤 {_currentUser.Nama}\n    {_currentUser.Role.ToUpper()}";
            lblUserInfo.Font = new Font("Segoe UI", 8f);
            lblUserInfo.ForeColor = CLR_TEXT_LIGHT;
            lblUserInfo.Size = new Size(180, 36);
            lblUserInfo.Location = new Point(20, 665);

            // Tombol logout
            btnLogout.Text = "Keluar";
            btnLogout.Size = new Size(180, 32);
            btnLogout.Location = new Point(20, 705);
            btnLogout.BackColor = Color.FromArgb(162, 45, 45);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += BtnLogout_Click;

            pnlSidebar.Controls.AddRange(new Control[] {
                lblSidebarLogo, lblSidebarSub, sep1,
                pnlMenuArea, lblUserInfo, btnLogout
            });
        }

        // ────────────────────────────────────────────────────
        //  TOPBAR
        // ────────────────────────────────────────────────────
        private void BuildTopbar()
        {
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Height = 56;
            pnlTopbar.BackColor = CLR_TOPBAR;

            // Garis bawah topbar
            pnlTopbar.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(
                    new System.Drawing.Pen(Color.FromArgb(220, 225, 235)),
                    0, pnlTopbar.Height - 1,
                    pnlTopbar.Width, pnlTopbar.Height - 1);
            };

            lblPageTitle.Text = "Dashboard";
            lblPageTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblPageTitle.ForeColor = CLR_ACCENT;
            lblPageTitle.AutoSize = true;
            lblPageTitle.Location = new Point(20, 14);

            lblTopUserInfo.Text = $"{_currentUser.Nama}  |  {_currentUser.Role.ToUpper()}";
            lblTopUserInfo.Font = new Font("Segoe UI", 9f);
            lblTopUserInfo.ForeColor = Color.Gray;
            lblTopUserInfo.AutoSize = true;
            lblTopUserInfo.Location = new Point(920, 20);

            pnlTopbar.Controls.AddRange(new Control[] {
                lblPageTitle, lblTopUserInfo
            });
        }

        // ────────────────────────────────────────────────────
        //  CONTENT AREA
        // ────────────────────────────────────────────────────
        private void BuildContentArea()
        {
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = CLR_CONTENT;
            pnlContent.Padding = new Padding(20);
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
            lbl.ForeColor = CLR_TEXT_LIGHT;
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
            btn.Tag = text;

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
            // Reset semua tombol menu
            foreach (Control c in pnlMenuArea.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.Transparent;
                    b.Font = new Font("Segoe UI", 9f);
                }
            }
            // Aktifkan tombol yang dipilih
            btn.BackColor = CLR_SIDEBAR_ACT;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _activeMenuBtn = btn;
        }

        // ────────────────────────────────────────────────────
        //  NAVIGASI — tampilkan panel konten
        // ────────────────────────────────────────────────────
        private void LoadPanel(Control panel, string pageTitle)
        {
            lblPageTitle.Text = pageTitle;
            pnlContent.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(panel);
        }

        // ── Apoteker & Kasir ─────────────────────────────────
        private void ShowDashboard()
        {
            LoadPanel(new Panels.PanelDashboard(_currentUser), "Dashboard");
        }

        // ── Apoteker only ────────────────────────────────────
        private void ShowManajemenUser()
        {
            LoadPanel(new Panels.Apoteker.PanelManajemenUser(), "Manajemen User");
        }

        private void ShowManajemenObat()
        {
            LoadPanel(new Panels.Apoteker.PanelManajemenObat(), "Manajemen Obat");
        }

        private void ShowValidasiResep()
        {
            LoadPanel(new Panels.Apoteker.PanelValidasiResep(_currentUser), "Validasi Resep");
        }

        private void ShowKontrolStok()
        {
            LoadPanel(new Panels.Apoteker.PanelKontrolStok(_currentUser), "Kontrol Stok");
        }

        private void ShowLaporanApoteker()
        {
            LoadPanel(new Panels.Apoteker.PanelLaporanApoteker(), "Laporan Detail");
        }

        // ── Kasir only ───────────────────────────────────────
        private void ShowCariObat()
        {
            LoadPanel(new Panels.Kasir.PanelCariObat(), "Cari Obat");
        }

        private void ShowTransaksi()
        {
            LoadPanel(new Panels.Kasir.PanelTransaksi(_currentUser), "Transaksi Penjualan");
        }

        private void ShowLaporanKasir()
        {
            LoadPanel(new Panels.Kasir.PanelLaporanKasir(), "Laporan Harian");
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
                var formLogin = new FormLogin();
                formLogin.Show();
                this.Close();
            }
        }

        private void InitializeComponent() { }
    }
}