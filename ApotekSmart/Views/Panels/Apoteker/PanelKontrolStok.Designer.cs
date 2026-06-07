namespace ApotekSmart.Views.Panels.Apoteker
{
    partial class PanelKontrolStok
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
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            lblPilihObat = new Label();
            cmbObat = new ComboBox();
            lblQtyMasuk = new Label();
            numQtyMasuk = new NumericUpDown();
            lblKeterangan = new Label();
            txtKeterangan = new TextBox();
            btnUpdateStok = new Button();
            lblInfoStok = new Label();
            dgvLogStok = new DataGridView();
            dgvStokKritis = new DataGridView();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQtyMasuk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogStok).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStokKritis).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1060, 620);
            tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lblInfoStok);
            tabPage1.Controls.Add(btnUpdateStok);
            tabPage1.Controls.Add(txtKeterangan);
            tabPage1.Controls.Add(lblKeterangan);
            tabPage1.Controls.Add(numQtyMasuk);
            tabPage1.Controls.Add(lblQtyMasuk);
            tabPage1.Controls.Add(cmbObat);
            tabPage1.Controls.Add(lblPilihObat);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1052, 582);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "📦 Update Stok";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvLogStok);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1052, 582);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "📋 Log Stok";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dgvStokKritis);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1052, 582);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "⚠️ Stok Kritis";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblPilihObat
            // 
            lblPilihObat.AutoSize = true;
            lblPilihObat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPilihObat.Location = new Point(20, 20);
            lblPilihObat.Name = "lblPilihObat";
            lblPilihObat.Size = new Size(96, 25);
            lblPilihObat.TabIndex = 0;
            lblPilihObat.Text = "Pilih Obat";
            // 
            // cmbObat
            // 
            cmbObat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbObat.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbObat.FormattingEnabled = true;
            cmbObat.Location = new Point(20, 44);
            cmbObat.Name = "cmbObat";
            cmbObat.Size = new Size(400, 36);
            cmbObat.TabIndex = 1;
            // 
            // lblQtyMasuk
            // 
            lblQtyMasuk.AutoSize = true;
            lblQtyMasuk.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQtyMasuk.Location = new Point(20, 90);
            lblQtyMasuk.Name = "lblQtyMasuk";
            lblQtyMasuk.Size = new Size(104, 25);
            lblQtyMasuk.TabIndex = 2;
            lblQtyMasuk.Text = "Qty Masuk";
            // 
            // numQtyMasuk
            // 
            numQtyMasuk.Font = new Font("Segoe UI", 10F);
            numQtyMasuk.Location = new Point(20, 115);
            numQtyMasuk.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numQtyMasuk.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQtyMasuk.Name = "numQtyMasuk";
            numQtyMasuk.Size = new Size(200, 34);
            numQtyMasuk.TabIndex = 3;
            numQtyMasuk.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblKeterangan
            // 
            lblKeterangan.AutoSize = true;
            lblKeterangan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKeterangan.Location = new Point(20, 158);
            lblKeterangan.Name = "lblKeterangan";
            lblKeterangan.Size = new Size(111, 25);
            lblKeterangan.TabIndex = 4;
            lblKeterangan.Text = "Keterangan";
            // 
            // txtKeterangan
            // 
            txtKeterangan.BorderStyle = BorderStyle.FixedSingle;
            txtKeterangan.Font = new Font("Segoe UI", 10F);
            txtKeterangan.Location = new Point(20, 180);
            txtKeterangan.Multiline = true;
            txtKeterangan.Name = "txtKeterangan";
            txtKeterangan.Size = new Size(400, 80);
            txtKeterangan.TabIndex = 5;
            // 
            // btnUpdateStok
            // 
            btnUpdateStok.BackColor = Color.FromArgb(30, 58, 95);
            btnUpdateStok.Cursor = Cursors.Hand;
            btnUpdateStok.FlatAppearance.BorderSize = 0;
            btnUpdateStok.FlatStyle = FlatStyle.Flat;
            btnUpdateStok.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdateStok.ForeColor = Color.White;
            btnUpdateStok.Location = new Point(20, 280);
            btnUpdateStok.Name = "btnUpdateStok";
            btnUpdateStok.Size = new Size(200, 42);
            btnUpdateStok.TabIndex = 6;
            btnUpdateStok.Text = "💾 Update Stok";
            btnUpdateStok.UseVisualStyleBackColor = false;
            // 
            // lblInfoStok
            // 
            lblInfoStok.AutoSize = true;
            lblInfoStok.Font = new Font("Segoe UI", 10F);
            lblInfoStok.ForeColor = Color.Gray;
            lblInfoStok.Location = new Point(20, 340);
            lblInfoStok.Name = "lblInfoStok";
            lblInfoStok.Size = new Size(134, 28);
            lblInfoStok.TabIndex = 7;
            lblInfoStok.Text = "Stok saat ini: -";
            // 
            // dgvLogStok
            // 
            dgvLogStok.AllowUserToAddRows = false;
            dgvLogStok.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogStok.Location = new Point(10, 10);
            dgvLogStok.Name = "dgvLogStok";
            dgvLogStok.ReadOnly = true;
            dgvLogStok.RowHeadersVisible = false;
            dgvLogStok.RowHeadersWidth = 62;
            dgvLogStok.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogStok.Size = new Size(1020, 540);
            dgvLogStok.TabIndex = 0;
            // 
            // dgvStokKritis
            // 
            dgvStokKritis.AllowUserToAddRows = false;
            dgvStokKritis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStokKritis.Location = new Point(10, 10);
            dgvStokKritis.Name = "dgvStokKritis";
            dgvStokKritis.ReadOnly = true;
            dgvStokKritis.RowHeadersVisible = false;
            dgvStokKritis.RowHeadersWidth = 62;
            dgvStokKritis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStokKritis.Size = new Size(1020, 540);
            dgvStokKritis.TabIndex = 0;
            // 
            // PanelKontrolStok
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(tabControl);
            Name = "PanelKontrolStok";
            Size = new Size(1100, 650);
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numQtyMasuk).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogStok).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStokKritis).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ComboBox cmbObat;
        private Label lblPilihObat;
        private Label lblQtyMasuk;
        private NumericUpDown numQtyMasuk;
        private TextBox txtKeterangan;
        private Label lblKeterangan;
        private Button btnUpdateStok;
        private Label lblInfoStok;
        private DataGridView dgvLogStok;
        private DataGridView dgvStokKritis;
    }
}
