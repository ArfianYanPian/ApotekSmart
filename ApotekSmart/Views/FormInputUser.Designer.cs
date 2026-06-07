namespace ApotekSmart.Views
{
    partial class FormInputUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblNama = new Label();
            txtNama = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            lblIdentitas = new Label();
            txtIdentitas = new TextBox();
            btnSimpan = new Button();
            btnBatal = new Button();
            SuspendLayout();
            // 
            // lblNama
            // 
            lblNama.AutoSize = true;
            lblNama.BackColor = SystemColors.Control;
            lblNama.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNama.ForeColor = Color.FromArgb(85, 85, 85);
            lblNama.Location = new Point(30, 20);
            lblNama.Name = "lblNama";
            lblNama.Size = new Size(139, 25);
            lblNama.TabIndex = 0;
            lblNama.Text = "Nama Lengkap";
            // 
            // txtNama
            // 
            txtNama.BorderStyle = BorderStyle.FixedSingle;
            txtNama.Font = new Font("Segoe UI", 10F);
            txtNama.Location = new Point(30, 42);
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(340, 34);
            txtNama.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(85, 85, 85);
            lblUsername.Location = new Point(30, 90);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(97, 25);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(30, 112);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(340, 34);
            txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = Color.FromArgb(85, 85, 85);
            lblPassword.Location = new Point(30, 160);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(92, 25);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(30, 182);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(340, 34);
            txtPassword.TabIndex = 5;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRole.ForeColor = Color.FromArgb(85, 85, 85);
            lblRole.Location = new Point(30, 230);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(50, 25);
            lblRole.TabIndex = 6;
            lblRole.Text = "Role";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(30, 252);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(340, 36);
            cmbRole.TabIndex = 7;
            // 
            // lblIdentitas
            // 
            lblIdentitas.AutoSize = true;
            lblIdentitas.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIdentitas.ForeColor = Color.FromArgb(85, 85, 85);
            lblIdentitas.Location = new Point(30, 300);
            lblIdentitas.Name = "lblIdentitas";
            lblIdentitas.Size = new Size(173, 25);
            lblIdentitas.TabIndex = 8;
            lblIdentitas.Text = "Nomor SIPA / Shift";
            // 
            // txtIdentitas
            // 
            txtIdentitas.BorderStyle = BorderStyle.FixedSingle;
            txtIdentitas.Font = new Font("Segoe UI", 10F);
            txtIdentitas.Location = new Point(30, 322);
            txtIdentitas.Name = "txtIdentitas";
            txtIdentitas.Size = new Size(340, 34);
            txtIdentitas.TabIndex = 9;
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.FromArgb(30, 58, 95);
            btnSimpan.Cursor = Cursors.Hand;
            btnSimpan.FlatAppearance.BorderSize = 0;
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSimpan.ForeColor = Color.White;
            btnSimpan.Location = new Point(30, 393);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(160, 40);
            btnSimpan.TabIndex = 10;
            btnSimpan.Text = "Simpan";
            btnSimpan.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            btnBatal.BackColor = SystemColors.ScrollBar;
            btnBatal.Cursor = Cursors.Hand;
            btnBatal.FlatAppearance.BorderSize = 0;
            btnBatal.FlatStyle = FlatStyle.Flat;
            btnBatal.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBatal.ForeColor = Color.FromArgb(50, 50, 50);
            btnBatal.Location = new Point(210, 393);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(160, 40);
            btnBatal.TabIndex = 11;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = false;
            // 
            // FormInputUser
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(398, 444);
            Controls.Add(btnBatal);
            Controls.Add(btnSimpan);
            Controls.Add(txtIdentitas);
            Controls.Add(lblIdentitas);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(txtNama);
            Controls.Add(lblNama);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInputUser";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tambah User";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNama;
        private TextBox txtNama;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblIdentitas;
        private TextBox txtIdentitas;
        private Button btnSimpan;
        private Button btnBatal;
    }
}