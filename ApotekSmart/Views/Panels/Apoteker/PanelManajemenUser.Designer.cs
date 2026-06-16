namespace ApotekSmart.Views.Panels.Apoteker
{
    partial class PanelManajemenUser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlAksi = new Panel();
            btnTambahUser = new Button();
            txtCariUser = new TextBox();
            lblCariUser = new Label();
            dgvUser = new DataGridView();
            pnlTombolAksi = new Panel();
            btnEditUser = new Button();
            btnHapusUser = new Button();
            pnlAksi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUser).BeginInit();
            pnlTombolAksi.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAksi
            // 
            pnlAksi.BackColor = Color.White;
            pnlAksi.Controls.Add(lblCariUser);
            pnlAksi.Controls.Add(txtCariUser);
            pnlAksi.Controls.Add(btnTambahUser);
            pnlAksi.Location = new Point(0, 0);
            pnlAksi.Name = "pnlAksi";
            pnlAksi.Size = new Size(1060, 50);
            pnlAksi.TabIndex = 0;
            // 
            // btnTambahUser
            // 
            btnTambahUser.BackColor = Color.FromArgb(30, 58, 95);
            btnTambahUser.Cursor = Cursors.Hand;
            btnTambahUser.FlatAppearance.BorderSize = 0;
            btnTambahUser.FlatStyle = FlatStyle.Flat;
            btnTambahUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTambahUser.ForeColor = Color.White;
            btnTambahUser.Location = new Point(10, 8);
            btnTambahUser.Name = "btnTambahUser";
            btnTambahUser.Size = new Size(150, 34);
            btnTambahUser.TabIndex = 0;
            btnTambahUser.Text = "+ Tambah User";
            btnTambahUser.UseVisualStyleBackColor = false;
            // 
            // txtCariUser
            // 
            txtCariUser.Font = new Font("Segoe UI", 10F);
            txtCariUser.Location = new Point(800, 8);
            txtCariUser.Name = "txtCariUser";
            txtCariUser.Size = new Size(220, 34);
            txtCariUser.TabIndex = 1;
            // 
            // lblCariUser
            // 
            lblCariUser.AutoSize = true;
            lblCariUser.Location = new Point(770, 14);
            lblCariUser.Name = "lblCariUser";
            lblCariUser.Size = new Size(37, 25);
            lblCariUser.TabIndex = 2;
            lblCariUser.Text = "🔍";
            // 
            // dgvUser
            // 
            dgvUser.AllowUserToAddRows = false;
            dgvUser.BackgroundColor = Color.White;
            dgvUser.BorderStyle = BorderStyle.None;
            dgvUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUser.Location = new Point(0, 60);
            dgvUser.Name = "dgvUser";
            dgvUser.ReadOnly = true;
            dgvUser.RowHeadersVisible = false;
            dgvUser.RowHeadersWidth = 62;
            dgvUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUser.Size = new Size(1060, 480);
            dgvUser.TabIndex = 1;
            // 
            // pnlTombolAksi
            // 
            pnlTombolAksi.BackColor = Color.White;
            pnlTombolAksi.Controls.Add(btnHapusUser);
            pnlTombolAksi.Controls.Add(btnEditUser);
            pnlTombolAksi.Location = new Point(0, 550);
            pnlTombolAksi.Name = "pnlTombolAksi";
            pnlTombolAksi.Size = new Size(1060, 50);
            pnlTombolAksi.TabIndex = 2;
            // 
            // btnEditUser
            // 
            btnEditUser.BackColor = Color.FromArgb(42, 80, 128);
            btnEditUser.Cursor = Cursors.Hand;
            btnEditUser.FlatAppearance.BorderSize = 0;
            btnEditUser.FlatStyle = FlatStyle.Flat;
            btnEditUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditUser.ForeColor = Color.White;
            btnEditUser.Location = new Point(10, 8);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(120, 34);
            btnEditUser.TabIndex = 0;
            btnEditUser.Text = "✏️ Edit";
            btnEditUser.UseVisualStyleBackColor = false;
            // 
            // btnHapusUser
            // 
            btnHapusUser.BackColor = Color.FromArgb(162, 45, 45);
            btnHapusUser.Cursor = Cursors.Hand;
            btnHapusUser.FlatAppearance.BorderSize = 0;
            btnHapusUser.FlatStyle = FlatStyle.Flat;
            btnHapusUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHapusUser.ForeColor = Color.White;
            btnHapusUser.Location = new Point(140, 8);
            btnHapusUser.Name = "btnHapusUser";
            btnHapusUser.Size = new Size(120, 34);
            btnHapusUser.TabIndex = 1;
            btnHapusUser.Text = "🗑️ Hapus";
            btnHapusUser.UseVisualStyleBackColor = false;
            // 
            // PanelManajemenUser
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlTombolAksi);
            Controls.Add(dgvUser);
            Controls.Add(pnlAksi);
            Name = "PanelManajemenUser";
            Size = new Size(1100, 650);
            pnlAksi.ResumeLayout(false);
            pnlAksi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUser).EndInit();
            pnlTombolAksi.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAksi;
        private Label lblCariUser;
        private TextBox txtCariUser;
        private Button btnTambahUser;
        private DataGridView dgvUser;
        private Panel pnlTombolAksi;
        private Button btnEditUser;
        private Button btnHapusUser;
    }
}
