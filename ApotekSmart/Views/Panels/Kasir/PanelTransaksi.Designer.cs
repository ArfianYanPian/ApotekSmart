namespace ApotekSmart.Views.Panels.Kasir
{
    partial class PanelTransaksi
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
            pnlKiri = new Panel();
            lblPilihObat = new Label();
            cmbObat = new ComboBox();
            lblQty = new Label();
            numQty = new NumericUpDown();
            lblHargaSatuan = new Label();
            lblNilaiHarga = new Label();
            lblInfoStok = new Label();
            btnTambahItem = new Button();
            lblKeranjang = new Label();
            dgvKeranjang = new DataGridView();
            btnHapusItem = new Button();
            panel1 = new Panel();
            lblJenisTransaksi = new Label();
            cmbJenis = new ComboBox();
            pnlInfoResep = new Panel();
            lblNomorResep = new Label();
            txtNomorResep = new TextBox();
            txtNamaPasien = new TextBox();
            txtNamaDokter = new TextBox();
            lblNamaPasien = new Label();
            lblNamaDokter = new Label();
            numJumlahBayar = new NumericUpDown();
            btnProsesBayar = new Button();
            lblTotal = new Label();
            lblNilaiTotal = new Label();
            lblJumlahBayar = new Label();
            lblKembalian = new Label();
            lblNilaiKembalian = new Label();
            pnlKiri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKeranjang).BeginInit();
            panel1.SuspendLayout();
            pnlInfoResep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numJumlahBayar).BeginInit();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.White;
            pnlKiri.Controls.Add(btnHapusItem);
            pnlKiri.Controls.Add(dgvKeranjang);
            pnlKiri.Controls.Add(lblKeranjang);
            pnlKiri.Controls.Add(btnTambahItem);
            pnlKiri.Controls.Add(lblInfoStok);
            pnlKiri.Controls.Add(lblNilaiHarga);
            pnlKiri.Controls.Add(lblHargaSatuan);
            pnlKiri.Controls.Add(numQty);
            pnlKiri.Controls.Add(lblQty);
            pnlKiri.Controls.Add(cmbObat);
            pnlKiri.Controls.Add(lblPilihObat);
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(580, 620);
            pnlKiri.TabIndex = 0;
            // 
            // lblPilihObat
            // 
            lblPilihObat.AutoSize = true;
            lblPilihObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPilihObat.Location = new Point(15, 15);
            lblPilihObat.Name = "lblPilihObat";
            lblPilihObat.Size = new Size(96, 25);
            lblPilihObat.TabIndex = 0;
            lblPilihObat.Text = "Pilih Obat";
            // 
            // cmbObat
            // 
            cmbObat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbObat.Font = new Font("Segoe UI", 10F);
            cmbObat.FormattingEnabled = true;
            cmbObat.Location = new Point(15, 38);
            cmbObat.Name = "cmbObat";
            cmbObat.Size = new Size(380, 36);
            cmbObat.TabIndex = 1;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblQty.Location = new Point(15, 85);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(43, 25);
            lblQty.TabIndex = 2;
            lblQty.Text = "Qty";
            // 
            // numQty
            // 
            numQty.Font = new Font("Segoe UI", 10F);
            numQty.Location = new Point(14, 114);
            numQty.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQty.Name = "numQty";
            numQty.Size = new Size(109, 34);
            numQty.TabIndex = 3;
            numQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblHargaSatuan
            // 
            lblHargaSatuan.AutoSize = true;
            lblHargaSatuan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHargaSatuan.Location = new Point(155, 85);
            lblHargaSatuan.Name = "lblHargaSatuan";
            lblHargaSatuan.Size = new Size(128, 25);
            lblHargaSatuan.TabIndex = 4;
            lblHargaSatuan.Text = "Harga Satuan";
            // 
            // lblNilaiHarga
            // 
            lblNilaiHarga.AutoSize = true;
            lblNilaiHarga.Font = new Font("Segoe UI", 10F);
            lblNilaiHarga.ForeColor = Color.FromArgb(30, 58, 95);
            lblNilaiHarga.Location = new Point(155, 110);
            lblNilaiHarga.Name = "lblNilaiHarga";
            lblNilaiHarga.Size = new Size(52, 28);
            lblNilaiHarga.TabIndex = 5;
            lblNilaiHarga.Text = "Rp 0";
            // 
            // lblInfoStok
            // 
            lblInfoStok.AutoSize = true;
            lblInfoStok.ForeColor = Color.Gray;
            lblInfoStok.Location = new Point(15, 152);
            lblInfoStok.Name = "lblInfoStok";
            lblInfoStok.Size = new Size(63, 25);
            lblInfoStok.TabIndex = 6;
            lblInfoStok.Text = "Stok: -";
            // 
            // btnTambahItem
            // 
            btnTambahItem.BackColor = Color.FromArgb(30, 58, 95);
            btnTambahItem.Cursor = Cursors.Hand;
            btnTambahItem.FlatAppearance.BorderSize = 0;
            btnTambahItem.FlatStyle = FlatStyle.Flat;
            btnTambahItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTambahItem.ForeColor = Color.White;
            btnTambahItem.Location = new Point(15, 178);
            btnTambahItem.Name = "btnTambahItem";
            btnTambahItem.Size = new Size(222, 38);
            btnTambahItem.TabIndex = 7;
            btnTambahItem.Text = "+ Tambah ke Keranjang";
            btnTambahItem.UseVisualStyleBackColor = false;
            // 
            // lblKeranjang
            // 
            lblKeranjang.AutoSize = true;
            lblKeranjang.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKeranjang.ForeColor = Color.FromArgb(30, 58, 95);
            lblKeranjang.Location = new Point(15, 235);
            lblKeranjang.Name = "lblKeranjang";
            lblKeranjang.Size = new Size(142, 28);
            lblKeranjang.TabIndex = 8;
            lblKeranjang.Text = "\U0001f6d2 Keranjang";
            // 
            // dgvKeranjang
            // 
            dgvKeranjang.AllowUserToAddRows = false;
            dgvKeranjang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKeranjang.Location = new Point(15, 268);
            dgvKeranjang.Name = "dgvKeranjang";
            dgvKeranjang.ReadOnly = true;
            dgvKeranjang.RowHeadersVisible = false;
            dgvKeranjang.RowHeadersWidth = 62;
            dgvKeranjang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKeranjang.Size = new Size(550, 260);
            dgvKeranjang.TabIndex = 9;
            // 
            // btnHapusItem
            // 
            btnHapusItem.BackColor = Color.FromArgb(162, 45, 45);
            btnHapusItem.Cursor = Cursors.Hand;
            btnHapusItem.FlatAppearance.BorderSize = 0;
            btnHapusItem.FlatStyle = FlatStyle.Flat;
            btnHapusItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHapusItem.ForeColor = Color.White;
            btnHapusItem.Location = new Point(15, 537);
            btnHapusItem.Name = "btnHapusItem";
            btnHapusItem.Size = new Size(160, 34);
            btnHapusItem.TabIndex = 10;
            btnHapusItem.Text = "🗑️ Hapus Item";
            btnHapusItem.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblNilaiKembalian);
            panel1.Controls.Add(lblKembalian);
            panel1.Controls.Add(lblJumlahBayar);
            panel1.Controls.Add(lblNilaiTotal);
            panel1.Controls.Add(lblTotal);
            panel1.Controls.Add(btnProsesBayar);
            panel1.Controls.Add(numJumlahBayar);
            panel1.Controls.Add(pnlInfoResep);
            panel1.Controls.Add(cmbJenis);
            panel1.Controls.Add(lblJenisTransaksi);
            panel1.Location = new Point(600, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(460, 620);
            panel1.TabIndex = 1;
            // 
            // lblJenisTransaksi
            // 
            lblJenisTransaksi.AutoSize = true;
            lblJenisTransaksi.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJenisTransaksi.Location = new Point(15, 15);
            lblJenisTransaksi.Name = "lblJenisTransaksi";
            lblJenisTransaksi.Size = new Size(138, 25);
            lblJenisTransaksi.TabIndex = 0;
            lblJenisTransaksi.Text = "Jenis Transaksi";
            // 
            // cmbJenis
            // 
            cmbJenis.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJenis.Font = new Font("Segoe UI", 10F);
            cmbJenis.FormattingEnabled = true;
            cmbJenis.Location = new Point(15, 38);
            cmbJenis.Name = "cmbJenis";
            cmbJenis.Size = new Size(420, 36);
            cmbJenis.TabIndex = 1;
            // 
            // pnlInfoResep
            // 
            pnlInfoResep.BackColor = Color.FromArgb(245, 247, 250);
            pnlInfoResep.Controls.Add(lblNamaDokter);
            pnlInfoResep.Controls.Add(lblNamaPasien);
            pnlInfoResep.Controls.Add(txtNamaDokter);
            pnlInfoResep.Controls.Add(txtNamaPasien);
            pnlInfoResep.Controls.Add(txtNomorResep);
            pnlInfoResep.Controls.Add(lblNomorResep);
            pnlInfoResep.Location = new Point(15, 85);
            pnlInfoResep.Name = "pnlInfoResep";
            pnlInfoResep.Size = new Size(430, 178);
            pnlInfoResep.TabIndex = 2;
            pnlInfoResep.Visible = false;
            // 
            // lblNomorResep
            // 
            lblNomorResep.AutoSize = true;
            lblNomorResep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomorResep.Location = new Point(10, 10);
            lblNomorResep.Name = "lblNomorResep";
            lblNomorResep.Size = new Size(127, 25);
            lblNomorResep.TabIndex = 0;
            lblNomorResep.Text = "Nomor Resep";
            // 
            // txtNomorResep
            // 
            txtNomorResep.BorderStyle = BorderStyle.FixedSingle;
            txtNomorResep.Font = new Font("Segoe UI", 10F);
            txtNomorResep.Location = new Point(10, 39);
            txtNomorResep.Name = "txtNomorResep";
            txtNomorResep.Size = new Size(400, 34);
            txtNomorResep.TabIndex = 1;
            // 
            // txtNamaPasien
            // 
            txtNamaPasien.BorderStyle = BorderStyle.FixedSingle;
            txtNamaPasien.Font = new Font("Segoe UI", 10F);
            txtNamaPasien.Location = new Point(10, 108);
            txtNamaPasien.Name = "txtNamaPasien";
            txtNamaPasien.Size = new Size(190, 34);
            txtNamaPasien.TabIndex = 2;
            // 
            // txtNamaDokter
            // 
            txtNamaDokter.BorderStyle = BorderStyle.FixedSingle;
            txtNamaDokter.Font = new Font("Segoe UI", 10F);
            txtNamaDokter.Location = new Point(215, 108);
            txtNamaDokter.Name = "txtNamaDokter";
            txtNamaDokter.Size = new Size(190, 34);
            txtNamaDokter.TabIndex = 3;
            // 
            // lblNamaPasien
            // 
            lblNamaPasien.AutoSize = true;
            lblNamaPasien.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamaPasien.Location = new Point(10, 80);
            lblNamaPasien.Name = "lblNamaPasien";
            lblNamaPasien.Size = new Size(122, 25);
            lblNamaPasien.TabIndex = 4;
            lblNamaPasien.Text = "Nama Pasien";
            // 
            // lblNamaDokter
            // 
            lblNamaDokter.AutoSize = true;
            lblNamaDokter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNamaDokter.Location = new Point(215, 80);
            lblNamaDokter.Name = "lblNamaDokter";
            lblNamaDokter.Size = new Size(125, 25);
            lblNamaDokter.TabIndex = 5;
            lblNamaDokter.Text = "Nama Dokter";
            // 
            // numJumlahBayar
            // 
            numJumlahBayar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numJumlahBayar.Location = new Point(18, 433);
            numJumlahBayar.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numJumlahBayar.Name = "numJumlahBayar";
            numJumlahBayar.Size = new Size(124, 34);
            numJumlahBayar.TabIndex = 3;
            // 
            // btnProsesBayar
            // 
            btnProsesBayar.BackColor = Color.FromArgb(39, 174, 96);
            btnProsesBayar.Cursor = Cursors.Hand;
            btnProsesBayar.FlatAppearance.BorderSize = 0;
            btnProsesBayar.FlatStyle = FlatStyle.Flat;
            btnProsesBayar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProsesBayar.ForeColor = Color.White;
            btnProsesBayar.Location = new Point(15, 555);
            btnProsesBayar.Name = "btnProsesBayar";
            btnProsesBayar.Size = new Size(420, 48);
            btnProsesBayar.TabIndex = 4;
            btnProsesBayar.Text = "💳 PROSES BAYAR";
            btnProsesBayar.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(30, 58, 95);
            lblTotal.Location = new Point(15, 324);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(64, 30);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "Total";
            // 
            // lblNilaiTotal
            // 
            lblNilaiTotal.AutoSize = true;
            lblNilaiTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNilaiTotal.ForeColor = Color.FromArgb(30, 58, 95);
            lblNilaiTotal.Location = new Point(15, 349);
            lblNilaiTotal.Name = "lblNilaiTotal";
            lblNilaiTotal.Size = new Size(88, 45);
            lblNilaiTotal.TabIndex = 6;
            lblNilaiTotal.Text = "Rp 0";
            // 
            // lblJumlahBayar
            // 
            lblJumlahBayar.AutoSize = true;
            lblJumlahBayar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblJumlahBayar.Location = new Point(15, 405);
            lblJumlahBayar.Name = "lblJumlahBayar";
            lblJumlahBayar.Size = new Size(127, 25);
            lblJumlahBayar.TabIndex = 7;
            lblJumlahBayar.Text = "Jumlah Bayar";
            // 
            // lblKembalian
            // 
            lblKembalian.AutoSize = true;
            lblKembalian.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKembalian.Location = new Point(15, 479);
            lblKembalian.Name = "lblKembalian";
            lblKembalian.Size = new Size(102, 25);
            lblKembalian.TabIndex = 8;
            lblKembalian.Text = "Kembalian";
            // 
            // lblNilaiKembalian
            // 
            lblNilaiKembalian.AutoSize = true;
            lblNilaiKembalian.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNilaiKembalian.ForeColor = Color.FromArgb(39, 174, 96);
            lblNilaiKembalian.Location = new Point(18, 504);
            lblNilaiKembalian.Name = "lblNilaiKembalian";
            lblNilaiKembalian.Size = new Size(66, 32);
            lblNilaiKembalian.TabIndex = 9;
            lblNilaiKembalian.Text = "Rp 0";
            // 
            // PanelTransaksi
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panel1);
            Controls.Add(pnlKiri);
            Name = "PanelTransaksi";
            Size = new Size(1100, 650);
            pnlKiri.ResumeLayout(false);
            pnlKiri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKeranjang).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlInfoResep.ResumeLayout(false);
            pnlInfoResep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numJumlahBayar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlKiri;
        private ComboBox cmbObat;
        private Label lblPilihObat;
        private NumericUpDown numQty;
        private Label lblQty;
        private Label lblNilaiHarga;
        private Label lblHargaSatuan;
        private Label lblKeranjang;
        private Button btnTambahItem;
        private Label lblInfoStok;
        private Button btnHapusItem;
        private DataGridView dgvKeranjang;
        private Panel panel1;
        private Panel pnlInfoResep;
        private ComboBox cmbJenis;
        private Label lblJenisTransaksi;
        private Label lblNamaPasien;
        private TextBox txtNamaDokter;
        private TextBox txtNamaPasien;
        private TextBox txtNomorResep;
        private Label lblNomorResep;
        private Button btnProsesBayar;
        private NumericUpDown numJumlahBayar;
        private Label lblNamaDokter;
        private Label lblNilaiTotal;
        private Label lblTotal;
        private Label lblNilaiKembalian;
        private Label lblKembalian;
        private Label lblJumlahBayar;
    }
}
