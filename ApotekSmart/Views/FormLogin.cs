using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApotekSmart.Controllers;
using ApotekSmart.Models;

namespace ApotekSmart.Views
{
    public partial class FormLogin : Form
    {
        private AuthController _authController = new AuthController();

        public FormLogin()
        {
            InitializeComponent();
            AturTampilan();
        }

        // ── Atur tampilan tambahan yang tidak bisa di Designer ──
        private void AturTampilan()
        {
            // Buat pnlCard tampak rounded
            pnlCard.Paint += PnlCard_Paint;

            // Buat pnlDeko1 & pnlDeko2 tampak bulat
            pnlDeko1.Paint += PnlBulat_Paint;
            pnlDeko2.Paint += PnlBulat_Paint;

            // Enter key trigger login
            txtUsername.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e);
            };
            txtPassword.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e);
            };
        }

        // ── Rounded corner untuk pnlCard ────────────────────
        private void PnlCard_Paint(object sender, PaintEventArgs e)
        {
            var p = (Panel)sender;
            using (var path = new GraphicsPath())
            {
                int r = 24;
                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(p.Width - r, 0, r, r, 270, 90);
                path.AddArc(p.Width - r, p.Height - r, r, r, 0, 90);
                path.AddArc(0, p.Height - r, r, r, 90, 90);
                path.CloseFigure();
                p.Region = new Region(path);
            }
        }

        // ── Lingkaran untuk dekorasi ─────────────────────────
        private void PnlBulat_Paint(object sender, PaintEventArgs e)
        {
            var p = (Panel)sender;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, p.Width - 1, p.Height - 1);
                p.Region = new Region(path);
            }
        }

        // ── EVENT: Tombol Login ──────────────────────────────
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                TampilError("Username dan password tidak boleh kosong.");
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Memproses...";

                BaseUser user = _authController.Login(username, password);

                if (user == null)
                {
                    TampilError("Username atau password salah.");
                    return;
                }

                // Buka FormMain, sembunyikan FormLogin
                var formMain = new FormMain(user);
                formMain.Show();
                this.Hide();
                formMain.FormClosed += (s, args) => this.Show();
            }
            catch (Exception ex)
            {
                TampilError("Gagal login: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
            }
        }

        private void TampilError(string pesan)
        {
            lblError.Text = pesan;
            lblError.Visible = true;
        }
    }
}