namespace ApotekSmart.Views.Panels.Apoteker
{
    partial class PanelManajemenObat
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
            btnTambahObat = new Button();
            lblCariObat = new Label();
            txtCariObat = new TextBox();
            dgvObat = new DataGridView();
            pnlTombolAksi = new Panel();
            btnEditObat = new Button();
            btnHapusObat = new Button();
            pnlAksi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvObat).BeginInit();
            pnlTombolAksi.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAksi
            // 
            pnlAksi.BackColor = Color.White;
            pnlAksi.Controls.Add(txtCariObat);
            pnlAksi.Controls.Add(lblCariObat);
            pnlAksi.Controls.Add(btnTambahObat);
            pnlAksi.Location = new Point(0, 0);
            pnlAksi.Name = "pnlAksi";
            pnlAksi.Size = new Size(1060, 50);
            pnlAksi.TabIndex = 0;
            // 
            // btnTambahObat
            // 
            btnTambahObat.BackColor = Color.FromArgb(30, 58, 95);
            btnTambahObat.Cursor = Cursors.Hand;
            btnTambahObat.FlatAppearance.BorderSize = 0;
            btnTambahObat.FlatStyle = FlatStyle.Flat;
            btnTambahObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTambahObat.ForeColor = Color.White;
            btnTambahObat.Location = new Point(10, 8);
            btnTambahObat.Name = "btnTambahObat";
            btnTambahObat.Size = new Size(140, 34);
            btnTambahObat.TabIndex = 0;
            btnTambahObat.Text = "+ Tambah Obat";
            btnTambahObat.UseVisualStyleBackColor = false;
            // 
            // lblCariObat
            // 
            lblCariObat.AutoSize = true;
            lblCariObat.Location = new Point(770, 14);
            lblCariObat.Name = "lblCariObat";
            lblCariObat.Size = new Size(37, 25);
            lblCariObat.TabIndex = 1;
            lblCariObat.Text = "🔍";
            // 
            // txtCariObat
            // 
            txtCariObat.Font = new Font("Segoe UI", 10F);
            txtCariObat.Location = new Point(800, 8);
            txtCariObat.Name = "txtCariObat";
            txtCariObat.Size = new Size(220, 34);
            txtCariObat.TabIndex = 2;
            // 
            // dgvObat
            // 
            dgvObat.AllowUserToAddRows = false;
            dgvObat.BackgroundColor = Color.White;
            dgvObat.BorderStyle = BorderStyle.None;
            dgvObat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvObat.Location = new Point(0, 60);
            dgvObat.Name = "dgvObat";
            dgvObat.ReadOnly = true;
            dgvObat.RowHeadersVisible = false;
            dgvObat.RowHeadersWidth = 62;
            dgvObat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvObat.Size = new Size(1060, 480);
            dgvObat.TabIndex = 1;
            // 
            // pnlTombolAksi
            // 
            pnlTombolAksi.BackColor = Color.White;
            pnlTombolAksi.Controls.Add(btnHapusObat);
            pnlTombolAksi.Controls.Add(btnEditObat);
            pnlTombolAksi.Location = new Point(0, 550);
            pnlTombolAksi.Name = "pnlTombolAksi";
            pnlTombolAksi.Size = new Size(1060, 50);
            pnlTombolAksi.TabIndex = 2;
            // 
            // btnEditObat
            // 
            btnEditObat.BackColor = Color.FromArgb(42, 80, 128);
            btnEditObat.Cursor = Cursors.Hand;
            btnEditObat.FlatAppearance.BorderSize = 0;
            btnEditObat.FlatStyle = FlatStyle.Flat;
            btnEditObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditObat.ForeColor = Color.White;
            btnEditObat.Location = new Point(10, 8);
            btnEditObat.Name = "btnEditObat";
            btnEditObat.Size = new Size(120, 34);
            btnEditObat.TabIndex = 0;
            btnEditObat.Text = "✏️ Edit";
            btnEditObat.UseVisualStyleBackColor = false;
            // 
            // btnHapusObat
            // 
            btnHapusObat.BackColor = Color.FromArgb(162, 45, 45);
            btnHapusObat.Cursor = Cursors.Hand;
            btnHapusObat.FlatAppearance.BorderSize = 0;
            btnHapusObat.FlatStyle = FlatStyle.Flat;
            btnHapusObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHapusObat.ForeColor = Color.White;
            btnHapusObat.Location = new Point(140, 8);
            btnHapusObat.Name = "btnHapusObat";
            btnHapusObat.Size = new Size(120, 34);
            btnHapusObat.TabIndex = 1;
            btnHapusObat.Text = "🗑️ Hapus";
            btnHapusObat.UseVisualStyleBackColor = false;
            // 
            // PanelManajemenObat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlTombolAksi);
            Controls.Add(dgvObat);
            Controls.Add(pnlAksi);
            Name = "PanelManajemenObat";
            Size = new Size(1100, 650);
            pnlAksi.ResumeLayout(false);
            pnlAksi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvObat).EndInit();
            pnlTombolAksi.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAksi;
        private Button btnTambahObat;
        private TextBox txtCariObat;
        private Label lblCariObat;
        private DataGridView dgvObat;
        private Panel pnlTombolAksi;
        private Button btnEditObat;
        private Button btnHapusObat;
    }
}
