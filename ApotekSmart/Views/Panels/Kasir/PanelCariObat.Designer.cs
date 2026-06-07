namespace ApotekSmart.Views.Panels.Kasir
{
    partial class PanelCariObat
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
            lblInfo = new Label();
            dgvObat = new DataGridView();
            pnlFilter = new Panel();
            lblCari = new Label();
            lblKategori = new Label();
            txtCari = new TextBox();
            cmbKategori = new ComboBox();
            btnCari = new Button();
            btnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvObat).BeginInit();
            pnlFilter.SuspendLayout();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(0, 600);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(199, 25);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Total: 0 obat ditemukan";
            // 
            // dgvObat
            // 
            dgvObat.AllowUserToAddRows = false;
            dgvObat.BackgroundColor = Color.White;
            dgvObat.BorderStyle = BorderStyle.None;
            dgvObat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvObat.Location = new Point(0, 70);
            dgvObat.Name = "dgvObat";
            dgvObat.ReadOnly = true;
            dgvObat.RowHeadersVisible = false;
            dgvObat.RowHeadersWidth = 62;
            dgvObat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvObat.Size = new Size(1060, 520);
            dgvObat.TabIndex = 1;
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = Color.White;
            pnlFilter.Controls.Add(btnReset);
            pnlFilter.Controls.Add(btnCari);
            pnlFilter.Controls.Add(cmbKategori);
            pnlFilter.Controls.Add(txtCari);
            pnlFilter.Controls.Add(lblKategori);
            pnlFilter.Controls.Add(lblCari);
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1060, 60);
            pnlFilter.TabIndex = 2;
            // 
            // lblCari
            // 
            lblCari.AutoSize = true;
            lblCari.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCari.ForeColor = Color.FromArgb(30, 58, 95);
            lblCari.Location = new Point(15, 18);
            lblCari.Name = "lblCari";
            lblCari.Size = new Size(135, 28);
            lblCari.TabIndex = 0;
            lblCari.Text = "🔍 Cari Obat";
            // 
            // lblKategori
            // 
            lblKategori.AutoSize = true;
            lblKategori.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKategori.Location = new Point(400, 18);
            lblKategori.Name = "lblKategori";
            lblKategori.Size = new Size(85, 25);
            lblKategori.TabIndex = 1;
            lblKategori.Text = "Kategori";
            // 
            // txtCari
            // 
            txtCari.Location = new Point(150, 13);
            txtCari.Name = "txtCari";
            txtCari.Size = new Size(250, 31);
            txtCari.TabIndex = 2;
            // 
            // cmbKategori
            // 
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.Font = new Font("Segoe UI", 10F);
            cmbKategori.FormattingEnabled = true;
            cmbKategori.Location = new Point(486, 13);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(180, 36);
            cmbKategori.TabIndex = 3;
            // 
            // btnCari
            // 
            btnCari.BackColor = Color.FromArgb(30, 58, 95);
            btnCari.Cursor = Cursors.Hand;
            btnCari.FlatAppearance.BorderSize = 0;
            btnCari.FlatStyle = FlatStyle.Flat;
            btnCari.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCari.ForeColor = Color.White;
            btnCari.Location = new Point(682, 13);
            btnCari.Name = "btnCari";
            btnCari.Size = new Size(100, 34);
            btnCari.TabIndex = 4;
            btnCari.Text = "Cari";
            btnCari.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = SystemColors.ScrollBar;
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.FromArgb(50, 50, 50);
            btnReset.Location = new Point(796, 13);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 34);
            btnReset.TabIndex = 5;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // PanelCariObat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlFilter);
            Controls.Add(dgvObat);
            Controls.Add(lblInfo);
            Name = "PanelCariObat";
            Size = new Size(1100, 650);
            ((System.ComponentModel.ISupportInitialize)dgvObat).EndInit();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInfo;
        private DataGridView dgvObat;
        private Panel pnlFilter;
        private Label lblCari;
        private Button btnCari;
        private ComboBox cmbKategori;
        private TextBox txtCari;
        private Label lblKategori;
        private Button btnReset;
    }
}
