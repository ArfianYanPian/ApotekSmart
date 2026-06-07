namespace ApotekSmart.Views
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlKiri = new Panel();
            lblTagline = new Label();
            lblAppName = new Label();
            pnlDeko2 = new Panel();
            pnlDeko1 = new Panel();
            pnlKanan = new Panel();
            pnlCard = new Panel();
            lblFooter = new Label();
            lblError = new Label();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            lblPassword = new Label();
            lblUsername = new Label();
            lblSub = new Label();
            lblJudul = new Label();
            pnlKiri.SuspendLayout();
            pnlKanan.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.Transparent;
            pnlKiri.Controls.Add(lblTagline);
            pnlKiri.Controls.Add(lblAppName);
            pnlKiri.Controls.Add(pnlDeko2);
            pnlKiri.Controls.Add(pnlDeko1);
            pnlKiri.Dock = DockStyle.Left;
            pnlKiri.Font = new Font("Segoe UI", 10F);
            pnlKiri.ForeColor = Color.FromArgb(122, 174, 212);
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(760, 712);
            pnlKiri.TabIndex = 0;
            // 
            // lblTagline
            // 
            lblTagline.AutoSize = true;
            lblTagline.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTagline.ForeColor = Color.White;
            lblTagline.Location = new Point(146, 334);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(500, 76);
            lblTagline.TabIndex = 4;
            lblTagline.Text = "Sistem Manajemen Apotek Terintegrasi\nyang Cerdas dan Efisien";
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(190, 260);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(381, 74);
            lblAppName.TabIndex = 3;
            lblAppName.Text = "ApotekSmart";
            // 
            // pnlDeko2
            // 
            pnlDeko2.BackColor = Color.FromArgb(10, 42, 74);
            pnlDeko2.Location = new Point(449, 532);
            pnlDeko2.Name = "pnlDeko2";
            pnlDeko2.Size = new Size(180, 180);
            pnlDeko2.TabIndex = 1;
            // 
            // pnlDeko1
            // 
            pnlDeko1.BackColor = Color.FromArgb(10, 42, 74);
            pnlDeko1.Location = new Point(-80, -80);
            pnlDeko1.Name = "pnlDeko1";
            pnlDeko1.Size = new Size(300, 300);
            pnlDeko1.TabIndex = 0;
            // 
            // pnlKanan
            // 
            pnlKanan.BackColor = Color.FromArgb(10, 32, 64);
            pnlKanan.Controls.Add(pnlCard);
            pnlKanan.Dock = DockStyle.Fill;
            pnlKanan.Location = new Point(0, 0);
            pnlKanan.Name = "pnlKanan";
            pnlKanan.Size = new Size(1344, 712);
            pnlKanan.TabIndex = 1;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(lblFooter);
            pnlCard.Controls.Add(lblError);
            pnlCard.Controls.Add(btnLogin);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(lblUsername);
            pnlCard.Controls.Add(lblSub);
            pnlCard.Controls.Add(lblJudul);
            pnlCard.Location = new Point(843, 95);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(424, 518);
            pnlCard.TabIndex = 0;
            // 
            // lblFooter
            // 
            lblFooter.AutoSize = true;
            lblFooter.Font = new Font("Segoe UI", 8F);
            lblFooter.ForeColor = Color.FromArgb(180, 180, 180);
            lblFooter.Location = new Point(128, 490);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(159, 21);
            lblFooter.TabIndex = 8;
            lblFooter.Text = "© 2026 ApotekSmart";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.FromArgb(226, 75, 74);
            lblError.Location = new Point(56, 396);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 25);
            lblError.TabIndex = 7;
            lblError.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(30, 58, 95);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(56, 338);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 42);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "LOGIN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(247, 249, 252);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(56, 278);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(298, 37);
            txtPassword.TabIndex = 5;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(247, 249, 252);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(56, 198);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(298, 37);
            txtUsername.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = Color.FromArgb(85, 85, 85);
            lblPassword.Location = new Point(56, 250);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(92, 25);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(85, 85, 85);
            lblUsername.Location = new Point(56, 167);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(97, 25);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 10F);
            lblSub.ForeColor = Color.Gray;
            lblSub.Location = new Point(92, 93);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(238, 28);
            lblSub.TabIndex = 1;
            lblSub.Text = "Masuk dengan akun Anda";
            // 
            // lblJudul
            // 
            lblJudul.AutoSize = true;
            lblJudul.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJudul.ForeColor = Color.FromArgb(30, 58, 95);
            lblJudul.Location = new Point(67, 45);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(287, 48);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "Selamat Datang";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 58, 95);
            ClientSize = new Size(1344, 712);
            Controls.Add(pnlKiri);
            Controls.Add(pnlKanan);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ApotekSmart - Login";
            pnlKiri.ResumeLayout(false);
            pnlKiri.PerformLayout();
            pnlKanan.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlKiri;
        private Panel pnlKanan;
        private Label lblAppName;
        private Panel pnlDeko2;
        private Panel pnlDeko1;
        private Label lblTagline;
        private Panel pnlCard;
        private Label lblSub;
        private Label lblJudul;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Label lblPassword;
        private Label lblUsername;
        private Label lblFooter;
        private Label lblError;
        private Button btnLogin;
    }
}