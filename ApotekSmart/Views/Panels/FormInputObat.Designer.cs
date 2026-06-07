namespace ApotekSmart.Views.Panels
{
    partial class FormInputObat
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
            lblNamaObat = new Label();
            txtNamaObat = new TextBox();
            lblKategori = new Label();
            cmbKategori = new ComboBox();
            lblJenis = new Label();
            cmbJenis = new ComboBox();
            lblGolongan = new Label();
            cmbGolongan = new ComboBox();
            lblSatuan = new Label();
            txtSatuan = new TextBox();
            lblStok = new Label();
            numStok = new NumericUpDown();
            lblHargaBeli = new Label();
            numHargaBeli = new NumericUpDown();
            lblHargaJual = new Label();
            numHargaJual = new NumericUpDown();
            lblStokMin = new Label();
            numStokMin = new NumericUpDown();
            lblTanggalExp = new Label();
            dtpTanggalExp = new DateTimePicker();
            btnSimpan = new Button();
            btnBatal = new Button();
            ((System.ComponentModel.ISupportInitialize)numStok).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHargaBeli).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHargaJual).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStokMin).BeginInit();
            SuspendLayout();
            // 
            // lblNamaObat
            // 
            lblNamaObat.AutoSize = true;
            lblNamaObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamaObat.ForeColor = Color.FromArgb(85, 85, 85);
            lblNamaObat.Location = new Point(20, 20);
            lblNamaObat.Name = "lblNamaObat";
            lblNamaObat.Size = new Size(109, 25);
            lblNamaObat.TabIndex = 0;
            lblNamaObat.Text = "Nama Obat";
            // 
            // txtNamaObat
            // 
            txtNamaObat.BorderStyle = BorderStyle.FixedSingle;
            txtNamaObat.Font = new Font("Segoe UI", 10F);
            txtNamaObat.Location = new Point(20, 43);
            txtNamaObat.Name = "txtNamaObat";
            txtNamaObat.Size = new Size(400, 34);
            txtNamaObat.TabIndex = 1;
            // 
            // lblKategori
            // 
            lblKategori.AutoSize = true;
            lblKategori.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKategori.ForeColor = Color.FromArgb(85, 85, 85);
            lblKategori.Location = new Point(20, 88);
            lblKategori.Name = "lblKategori";
            lblKategori.Size = new Size(85, 25);
            lblKategori.TabIndex = 2;
            lblKategori.Text = "Kategori";
            // 
            // cmbKategori
            // 
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.Font = new Font("Segoe UI", 10F);
            cmbKategori.FormattingEnabled = true;
            cmbKategori.Location = new Point(20, 114);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(190, 36);
            cmbKategori.TabIndex = 3;
            cmbKategori.SelectedIndexChanged += cmbKategori_SelectedIndexChanged;
            // 
            // lblJenis
            // 
            lblJenis.AutoSize = true;
            lblJenis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJenis.ForeColor = Color.FromArgb(85, 85, 85);
            lblJenis.Location = new Point(230, 88);
            lblJenis.Name = "lblJenis";
            lblJenis.Size = new Size(54, 25);
            lblJenis.TabIndex = 4;
            lblJenis.Text = "Jenis";
            // 
            // cmbJenis
            // 
            cmbJenis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJenis.Font = new Font("Segoe UI", 10F);
            cmbJenis.FormattingEnabled = true;
            cmbJenis.Location = new Point(230, 114);
            cmbJenis.Name = "cmbJenis";
            cmbJenis.Size = new Size(190, 36);
            cmbJenis.TabIndex = 5;
            // 
            // lblGolongan
            // 
            lblGolongan.AutoSize = true;
            lblGolongan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGolongan.ForeColor = Color.FromArgb(85, 85, 85);
            lblGolongan.Location = new Point(20, 156);
            lblGolongan.Name = "lblGolongan";
            lblGolongan.Size = new Size(95, 25);
            lblGolongan.TabIndex = 6;
            lblGolongan.Text = "Golongan";
            lblGolongan.Click += label1_Click;
            // 
            // cmbGolongan
            // 
            cmbGolongan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGolongan.Font = new Font("Segoe UI", 10F);
            cmbGolongan.FormattingEnabled = true;
            cmbGolongan.Location = new Point(20, 181);
            cmbGolongan.Name = "cmbGolongan";
            cmbGolongan.Size = new Size(400, 36);
            cmbGolongan.TabIndex = 7;
            // 
            // lblSatuan
            // 
            lblSatuan.AutoSize = true;
            lblSatuan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSatuan.ForeColor = Color.FromArgb(85, 85, 85);
            lblSatuan.Location = new Point(20, 224);
            lblSatuan.Name = "lblSatuan";
            lblSatuan.Size = new Size(71, 25);
            lblSatuan.TabIndex = 8;
            lblSatuan.Text = "Satuan";
            // 
            // txtSatuan
            // 
            txtSatuan.BorderStyle = BorderStyle.FixedSingle;
            txtSatuan.Font = new Font("Segoe UI", 10F);
            txtSatuan.Location = new Point(20, 249);
            txtSatuan.Name = "txtSatuan";
            txtSatuan.Size = new Size(190, 34);
            txtSatuan.TabIndex = 9;
            // 
            // lblStok
            // 
            lblStok.AutoSize = true;
            lblStok.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStok.ForeColor = Color.FromArgb(85, 85, 85);
            lblStok.Location = new Point(230, 224);
            lblStok.Name = "lblStok";
            lblStok.Size = new Size(97, 25);
            lblStok.TabIndex = 10;
            lblStok.Text = "Stok Awal";
            // 
            // numStok
            // 
            numStok.Font = new Font("Segoe UI", 10F);
            numStok.Location = new Point(230, 249);
            numStok.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numStok.Name = "numStok";
            numStok.Size = new Size(190, 34);
            numStok.TabIndex = 11;
            // 
            // lblHargaBeli
            // 
            lblHargaBeli.AutoSize = true;
            lblHargaBeli.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHargaBeli.ForeColor = Color.FromArgb(85, 85, 85);
            lblHargaBeli.Location = new Point(20, 292);
            lblHargaBeli.Name = "lblHargaBeli";
            lblHargaBeli.Size = new Size(101, 25);
            lblHargaBeli.TabIndex = 12;
            lblHargaBeli.Text = "Harga Beli";
            // 
            // numHargaBeli
            // 
            numHargaBeli.Font = new Font("Segoe UI", 10F);
            numHargaBeli.Location = new Point(20, 318);
            numHargaBeli.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numHargaBeli.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numHargaBeli.Name = "numHargaBeli";
            numHargaBeli.Size = new Size(190, 34);
            numHargaBeli.TabIndex = 13;
            numHargaBeli.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // lblHargaJual
            // 
            lblHargaJual.AutoSize = true;
            lblHargaJual.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHargaJual.ForeColor = Color.FromArgb(85, 85, 85);
            lblHargaJual.Location = new Point(230, 292);
            lblHargaJual.Name = "lblHargaJual";
            lblHargaJual.Size = new Size(103, 25);
            lblHargaJual.TabIndex = 14;
            lblHargaJual.Text = "Harga Jual";
            // 
            // numHargaJual
            // 
            numHargaJual.Font = new Font("Segoe UI", 10F);
            numHargaJual.Location = new Point(230, 320);
            numHargaJual.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numHargaJual.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numHargaJual.Name = "numHargaJual";
            numHargaJual.Size = new Size(190, 34);
            numHargaJual.TabIndex = 15;
            numHargaJual.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // lblStokMin
            // 
            lblStokMin.AutoSize = true;
            lblStokMin.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStokMin.ForeColor = Color.FromArgb(85, 85, 85);
            lblStokMin.Location = new Point(20, 360);
            lblStokMin.Name = "lblStokMin";
            lblStokMin.Size = new Size(136, 25);
            lblStokMin.TabIndex = 16;
            lblStokMin.Text = "Stok Minimum";
            lblStokMin.Click += label1_Click_1;
            // 
            // numStokMin
            // 
            numStokMin.Location = new Point(20, 385);
            numStokMin.Name = "numStokMin";
            numStokMin.Size = new Size(190, 31);
            numStokMin.TabIndex = 17;
            // 
            // lblTanggalExp
            // 
            lblTanggalExp.AutoSize = true;
            lblTanggalExp.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTanggalExp.ForeColor = Color.FromArgb(85, 85, 85);
            lblTanggalExp.Location = new Point(230, 360);
            lblTanggalExp.Name = "lblTanggalExp";
            lblTanggalExp.Size = new Size(148, 25);
            lblTanggalExp.TabIndex = 18;
            lblTanggalExp.Text = "Tanggal Expired";
            // 
            // dtpTanggalExp
            // 
            dtpTanggalExp.Font = new Font("Segoe UI", 10F);
            dtpTanggalExp.Format = DateTimePickerFormat.Short;
            dtpTanggalExp.Location = new Point(230, 384);
            dtpTanggalExp.Name = "dtpTanggalExp";
            dtpTanggalExp.Size = new Size(190, 34);
            dtpTanggalExp.TabIndex = 19;
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.FromArgb(30, 58, 95);
            btnSimpan.Cursor = Cursors.Hand;
            btnSimpan.FlatAppearance.BorderSize = 0;
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSimpan.ForeColor = Color.White;
            btnSimpan.Location = new Point(20, 515);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(190, 40);
            btnSimpan.TabIndex = 20;
            btnSimpan.Text = "Simpan";
            btnSimpan.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            btnBatal.BackColor = SystemColors.ScrollBar;
            btnBatal.FlatAppearance.BorderSize = 0;
            btnBatal.FlatStyle = FlatStyle.Flat;
            btnBatal.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBatal.ForeColor = Color.FromArgb(50, 50, 50);
            btnBatal.Location = new Point(230, 515);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(190, 40);
            btnBatal.TabIndex = 21;
            btnBatal.Text = "Batal";
            btnBatal.UseVisualStyleBackColor = false;
            // 
            // FormInputObat
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(438, 564);
            Controls.Add(btnBatal);
            Controls.Add(btnSimpan);
            Controls.Add(dtpTanggalExp);
            Controls.Add(lblTanggalExp);
            Controls.Add(numStokMin);
            Controls.Add(lblStokMin);
            Controls.Add(numHargaJual);
            Controls.Add(lblHargaJual);
            Controls.Add(numHargaBeli);
            Controls.Add(lblHargaBeli);
            Controls.Add(numStok);
            Controls.Add(lblStok);
            Controls.Add(txtSatuan);
            Controls.Add(lblSatuan);
            Controls.Add(cmbGolongan);
            Controls.Add(lblGolongan);
            Controls.Add(cmbJenis);
            Controls.Add(lblJenis);
            Controls.Add(cmbKategori);
            Controls.Add(lblKategori);
            Controls.Add(txtNamaObat);
            Controls.Add(lblNamaObat);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInputObat";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tambah Obat";
            Load += FormInputObat_Load;
            ((System.ComponentModel.ISupportInitialize)numStok).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHargaBeli).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHargaJual).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStokMin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNamaObat;
        private TextBox txtNamaObat;
        private Label lblKategori;
        private ComboBox cmbKategori;
        private Label lblJenis;
        private ComboBox cmbJenis;
        private Label lblGolongan;
        private ComboBox cmbGolongan;
        private Label lblSatuan;
        private TextBox txtSatuan;
        private Label lblStok;
        private NumericUpDown numStok;
        private Label lblHargaBeli;
        private NumericUpDown numHargaBeli;
        private Label lblHargaJual;
        private NumericUpDown numHargaJual;
        private Label lblStokMin;
        private NumericUpDown numStokMin;
        private Label lblTanggalExp;
        private DateTimePicker dtpTanggalExp;
        private Button btnSimpan;
        private Button btnBatal;
    }
}