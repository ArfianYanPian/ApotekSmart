using System;
using System.Drawing;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views
{
    public partial class FormLogin : Form
    {
        private AuthController _authController = new AuthController();

        // ── Komponen ──────────────────────────────────────────
        private Panel pnlKiri = new Panel();
        private Panel pnlKanan = new Panel();
        private Panel pnlCard = new Panel();
        private Panel pnlDeko1 = new Panel();
        private Panel pnlDeko2 = new Panel();

        private Label lblAppName = new Label();
        private Label lblTagline = new Label();
        private Label lblStat1Num = new Label();
        private Label lblStat1Txt = new Label();
        private Label lblStat2Num = new Label();
        private Label lblStat2Txt = new Label();
        private Label lblStat3Num = new Label();
        private Label lblStat3Txt = new Label();

        private Label lblJudul = new Label();
        private Label lblSub = new Label();
        private Label lblUsername = new Label();
        private Label lblPassword = new Label();
        private TextBox txtUsername = new TextBox();
        private TextBox txtPassword = new TextBox();
        private Button btnLogin = new Button();
        private Label lblError = new Label();
        private Label lblFooter = new Label();

        public FormLogin()
        {
            InitializeForm();
            BuildUI();
        }

        // ── Properti Form ────────────────────────────────────
        private void InitializeForm()
        {
            this.Text = "ApotekSmart — Login";
            this.Size = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 58, 95);   // #1E3A5F
            this.Font = new Font("Segoe UI", 9f);
        }

        // ── Bangun seluruh UI secara kode ────────────────────
        private void BuildUI()
        {
            BuildPanelKiri();
            BuildPanelKanan();

            // Urutan penting: Kiri dulu (Dock Left), lalu Kanan (Dock Fill)
            this.Controls.Add(pnlKanan);
            this.Controls.Add(pnlKiri);
        }

        // ────────────────────────────────────────────────────
        //  PANEL KIRI — Branding
        // ────────────────────────────────────────────────────
        private void BuildPanelKiri()
        {
            pnlKiri.Dock = DockStyle.Left;
            pnlKiri.Width = 760;
            pnlKiri.BackColor = Color.Transparent;

            // Lingkaran dekorasi 1 (kiri atas)
            pnlDeko1.Size = new Size(300, 300);
            pnlDeko1.Location = new Point(-80, -80);
            pnlDeko1.BackColor = Color.FromArgb(10, 42, 74);    // #0A2A4A
            MakeCircle(pnlDeko1);

            // Lingkaran dekorasi 2 (kiri bawah)
            pnlDeko2.Size = new Size(180, 180);
            pnlDeko2.Location = new Point(180, 520);
            pnlDeko2.BackColor = Color.FromArgb(10, 42, 74);
            MakeCircle(pnlDeko2);

            // Label nama aplikasi
            lblAppName.Text = "ApotekSmart";
            lblAppName.Font = new Font("Segoe UI", 28f, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(190, 260);

            // Tagline
            lblTagline.Text = "Sistem Manajemen Apotek Terintegrasi\nyang Cerdas dan Efisien";
            lblTagline.Font = new Font("Segoe UI", 10f);
            lblTagline.ForeColor = Color.FromArgb(122, 174, 212);  // #7AAED4
            lblTagline.Size = new Size(300, 50);
            lblTagline.AutoSize = false;
            lblTagline.Location = new Point(190, 320);

            // Statistik kecil
            int statY = 400;
            int stat1X = 160, stat2X = 300, stat3X = 440;
            BuildStatLabel(lblStat1Num, "2", stat1X, statY);
            BuildStatLabel(lblStat1Txt, "Role Pengguna", stat1X - 10, statY + 34, small: true);
            BuildStatLabel(lblStat2Num, "15+", stat2X, statY);
            BuildStatLabel(lblStat2Txt, "Fitur Sistem", stat2X - 5, statY + 34, small: true);
            BuildStatLabel(lblStat3Num, "OOP", stat3X, statY);
            BuildStatLabel(lblStat3Txt, "Berbasis PBO", stat3X - 5, statY + 34, small: true);

            pnlKiri.Controls.AddRange(new Control[] {
                pnlDeko1, pnlDeko2,
                lblAppName, lblTagline,
                lblStat1Num, lblStat1Txt,
                lblStat2Num, lblStat2Txt,
                lblStat3Num, lblStat3Txt
            });
        }

        private void BuildStatLabel(Label lbl, string text, int x, int y,
                                    bool small = false)
        {
            lbl.Text = text;
            lbl.Font = small
                ? new Font("Segoe UI", 9f)
                : new Font("Segoe UI", 18f, FontStyle.Bold);
            lbl.ForeColor = small
                ? Color.FromArgb(122, 174, 212)
                : Color.White;
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
        }

        // ────────────────────────────────────────────────────
        //  PANEL KANAN — Form Login
        // ────────────────────────────────────────────────────
        private void BuildPanelKanan()
        {
            pnlKanan.Dock = DockStyle.Fill;
            pnlKanan.BackColor = Color.FromArgb(10, 32, 64);   // #0A2040

            // Card putih
            pnlCard.Size = new Size(360, 430);
            pnlCard.Location = new Point(48, 160);
            pnlCard.BackColor = Color.White;
            MakeRounded(pnlCard, 12);

            // Judul card
            lblJudul.Text = "Selamat Datang";
            lblJudul.Font = new Font("Segoe UI", 18f, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(30, 58, 95);
            lblJudul.AutoSize = true;
            lblJudul.Location = new Point(30, 30);

            // Sub judul
            lblSub.Text = "Masuk dengan akun Anda";
            lblSub.Font = new Font("Segoe UI", 9f);
            lblSub.ForeColor = Color.Gray;
            lblSub.AutoSize = true;
            lblSub.Location = new Point(30, 70);

            // Label & TextBox Username
            lblUsername.Text = "Username";
            lblUsername.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(85, 85, 85);
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(30, 115);

            txtUsername.Size = new Size(300, 34);
            txtUsername.Location = new Point(30, 136);
            txtUsername.Font = new Font("Segoe UI", 11f);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.BackColor = Color.FromArgb(247, 249, 252);

            // Label & TextBox Password
            lblPassword.Text = "Password";
            lblPassword.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(85, 85, 85);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 190);

            txtPassword.Size = new Size(300, 34);
            txtPassword.Location = new Point(30, 211);
            txtPassword.Font = new Font("Segoe UI", 11f);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.BackColor = Color.FromArgb(247, 249, 252);
            txtPassword.PasswordChar = '●';

            // Tombol Login
            btnLogin.Text = "LOGIN";
            btnLogin.Size = new Size(300, 42);
            btnLogin.Location = new Point(30, 275);
            btnLogin.BackColor = Color.FromArgb(30, 58, 95);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;

            // Enter key juga trigger login
            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e); };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e); };

            // Label error
            lblError.Text = "";
            lblError.Font = new Font("Segoe UI", 9f);
            lblError.ForeColor = Color.FromArgb(226, 75, 74);
            lblError.AutoSize = true;
            lblError.Location = new Point(30, 328);
            lblError.Visible = false;

            // Footer
            lblFooter.Text = "© 2025 ApotekSmart";
            lblFooter.Font = new Font("Segoe UI", 8f);
            lblFooter.ForeColor = Color.FromArgb(180, 180, 180);
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(110, 395);

            pnlCard.Controls.AddRange(new Control[] {
                lblJudul, lblSub,
                lblUsername, txtUsername,
                lblPassword, txtPassword,
                btnLogin, lblError, lblFooter
            });

            pnlKanan.Controls.Add(pnlCard);
        }

        // ────────────────────────────────────────────────────
        //  EVENT: Tombol Login
        // ────────────────────────────────────────────────────
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Username dan password tidak boleh kosong.");
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Memproses...";

                BaseUser user = _authController.Login(username, password);

                if (user == null)
                {
                    ShowError("Username atau password salah.");
                    return;
                }

                // Buka FormMain sesuai role, tutup FormLogin
                var formMain = new FormMain(user);
                formMain.Show();
                this.Hide();
                formMain.FormClosed += (s, args) => this.Close();
            }
            catch (Exception ex)
            {
                ShowError("Gagal login: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
            }
        }

        private void ShowError(string pesan)
        {
            lblError.Text = pesan;
            lblError.Visible = true;
        }

        // ────────────────────────────────────────────────────
        //  Helper: buat panel tampak bulat
        // ────────────────────────────────────────────────────
        private void MakeCircle(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                var p = (Panel)s;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, p.Width - 1, p.Height - 1);
                    p.Region = new Region(path);
                }
            };
        }

        private void MakeRounded(Panel panel, int radius)
        {
            panel.Paint += (s, e) =>
            {
                var p = (Panel)s;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int d = radius * 2;
                    path.AddArc(0, 0, d, d, 180, 90);
                    path.AddArc(p.Width - d, 0, d, d, 270, 90);
                    path.AddArc(p.Width - d, p.Height - d, d, d, 0, 90);
                    path.AddArc(0, p.Height - d, d, d, 90, 90);
                    path.CloseFigure();
                    p.Region = new Region(path);
                }
            };
        }
    }
}