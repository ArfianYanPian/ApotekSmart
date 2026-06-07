namespace ApotekSmart.Views.Panels
{
    partial class PanelDashboard
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
            lblWelcome = new Label();
            lblTanggal = new Label();
            pnlStatObat = new Panel();
            lblStatObatNum = new Label();
            lblStatObatTxt = new Label();
            pnlStatTransaksi = new Panel();
            lblStatTrxNum = new Label();
            lblStatTrxTxt = new Label();
            pnlStatResep = new Panel();
            lblStatResepNum = new Label();
            lblStatResepTxt = new Label();
            pnlStatAlert = new Panel();
            lblStatAlertNum = new Label();
            lblStatAlertTxt = new Label();
            pnlStokKritis = new Panel();
            lblTitleStok = new Label();
            dgvStokKritis = new DataGridView();
            pnlObatExp = new Panel();
            lblTitleExp = new Label();
            dgvObatExp = new DataGridView();
            pnlStatObat.SuspendLayout();
            pnlStatTransaksi.SuspendLayout();
            pnlStatResep.SuspendLayout();
            pnlStatAlert.SuspendLayout();
            pnlStokKritis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStokKritis).BeginInit();
            pnlObatExp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvObatExp).BeginInit();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.FromArgb(30, 58, 95);
            lblWelcome.Location = new Point(0, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(267, 45);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Selamat Datang!";
            // 
            // lblTanggal
            // 
            lblTanggal.AutoSize = true;
            lblTanggal.ForeColor = Color.Gray;
            lblTanggal.Location = new Point(5, 43);
            lblTanggal.Name = "lblTanggal";
            lblTanggal.Size = new Size(188, 25);
            lblTanggal.TabIndex = 1;
            lblTanggal.Text = "Senin, 01 Januari 2026";
            // 
            // pnlStatObat
            // 
            pnlStatObat.BackColor = Color.White;
            pnlStatObat.Controls.Add(lblStatObatTxt);
            pnlStatObat.Controls.Add(lblStatObatNum);
            pnlStatObat.Location = new Point(0, 70);
            pnlStatObat.Name = "pnlStatObat";
            pnlStatObat.Size = new Size(240, 100);
            pnlStatObat.TabIndex = 2;
            // 
            // lblStatObatNum
            // 
            lblStatObatNum.AutoSize = true;
            lblStatObatNum.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatObatNum.ForeColor = Color.FromArgb(30, 58, 95);
            lblStatObatNum.Location = new Point(20, 8);
            lblStatObatNum.Name = "lblStatObatNum";
            lblStatObatNum.Size = new Size(56, 65);
            lblStatObatNum.TabIndex = 0;
            lblStatObatNum.Text = "0";
            // 
            // lblStatObatTxt
            // 
            lblStatObatTxt.AutoSize = true;
            lblStatObatTxt.BackColor = Color.Transparent;
            lblStatObatTxt.ForeColor = Color.Gray;
            lblStatObatTxt.Location = new Point(20, 67);
            lblStatObatTxt.Name = "lblStatObatTxt";
            lblStatObatTxt.Size = new Size(136, 25);
            lblStatObatTxt.TabIndex = 1;
            lblStatObatTxt.Text = "Total Obat Aktif";
            lblStatObatTxt.Click += lblStatObatTxt_Click;
            // 
            // pnlStatTransaksi
            // 
            pnlStatTransaksi.BackColor = Color.White;
            pnlStatTransaksi.Controls.Add(lblStatTrxTxt);
            pnlStatTransaksi.Controls.Add(lblStatTrxNum);
            pnlStatTransaksi.Location = new Point(256, 70);
            pnlStatTransaksi.Name = "pnlStatTransaksi";
            pnlStatTransaksi.Size = new Size(240, 100);
            pnlStatTransaksi.TabIndex = 3;
            // 
            // lblStatTrxNum
            // 
            lblStatTrxNum.AutoSize = true;
            lblStatTrxNum.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatTrxNum.ForeColor = Color.FromArgb(39, 174, 96);
            lblStatTrxNum.Location = new Point(20, 8);
            lblStatTrxNum.Name = "lblStatTrxNum";
            lblStatTrxNum.Size = new Size(56, 65);
            lblStatTrxNum.TabIndex = 0;
            lblStatTrxNum.Text = "0";
            // 
            // lblStatTrxTxt
            // 
            lblStatTrxTxt.AutoSize = true;
            lblStatTrxTxt.ForeColor = Color.Gray;
            lblStatTrxTxt.Location = new Point(20, 62);
            lblStatTrxTxt.Name = "lblStatTrxTxt";
            lblStatTrxTxt.Size = new Size(143, 25);
            lblStatTrxTxt.TabIndex = 1;
            lblStatTrxTxt.Text = "Transaksi Hari Ini";
            // 
            // pnlStatResep
            // 
            pnlStatResep.BackColor = Color.White;
            pnlStatResep.Controls.Add(lblStatResepTxt);
            pnlStatResep.Controls.Add(lblStatResepNum);
            pnlStatResep.Location = new Point(512, 70);
            pnlStatResep.Name = "pnlStatResep";
            pnlStatResep.Size = new Size(240, 100);
            pnlStatResep.TabIndex = 4;
            // 
            // lblStatResepNum
            // 
            lblStatResepNum.AutoSize = true;
            lblStatResepNum.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatResepNum.ForeColor = Color.FromArgb(230, 126, 34);
            lblStatResepNum.Location = new Point(20, 8);
            lblStatResepNum.Name = "lblStatResepNum";
            lblStatResepNum.Size = new Size(56, 65);
            lblStatResepNum.TabIndex = 0;
            lblStatResepNum.Text = "0";
            // 
            // lblStatResepTxt
            // 
            lblStatResepTxt.AutoSize = true;
            lblStatResepTxt.ForeColor = Color.Gray;
            lblStatResepTxt.Location = new Point(20, 62);
            lblStatResepTxt.Name = "lblStatResepTxt";
            lblStatResepTxt.Size = new Size(151, 25);
            lblStatResepTxt.TabIndex = 1;
            lblStatResepTxt.Text = "Resep Menunggu";
            // 
            // pnlStatAlert
            // 
            pnlStatAlert.BackColor = Color.White;
            pnlStatAlert.Controls.Add(lblStatAlertTxt);
            pnlStatAlert.Controls.Add(lblStatAlertNum);
            pnlStatAlert.Location = new Point(768, 70);
            pnlStatAlert.Name = "pnlStatAlert";
            pnlStatAlert.Size = new Size(240, 100);
            pnlStatAlert.TabIndex = 5;
            // 
            // lblStatAlertNum
            // 
            lblStatAlertNum.AutoSize = true;
            lblStatAlertNum.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatAlertNum.ForeColor = Color.FromArgb(226, 75, 74);
            lblStatAlertNum.Location = new Point(20, 8);
            lblStatAlertNum.Name = "lblStatAlertNum";
            lblStatAlertNum.Size = new Size(56, 65);
            lblStatAlertNum.TabIndex = 0;
            lblStatAlertNum.Text = "0";
            // 
            // lblStatAlertTxt
            // 
            lblStatAlertTxt.AutoSize = true;
            lblStatAlertTxt.ForeColor = Color.Gray;
            lblStatAlertTxt.Location = new Point(20, 62);
            lblStatAlertTxt.Name = "lblStatAlertTxt";
            lblStatAlertTxt.Size = new Size(89, 25);
            lblStatAlertTxt.TabIndex = 1;
            lblStatAlertTxt.Text = "Alert Stok";
            // 
            // pnlStokKritis
            // 
            pnlStokKritis.Controls.Add(dgvStokKritis);
            pnlStokKritis.Controls.Add(lblTitleStok);
            pnlStokKritis.Location = new Point(0, 190);
            pnlStokKritis.Name = "pnlStokKritis";
            pnlStokKritis.Size = new Size(510, 380);
            pnlStokKritis.TabIndex = 6;
            // 
            // lblTitleStok
            // 
            lblTitleStok.AutoSize = true;
            lblTitleStok.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitleStok.ForeColor = Color.FromArgb(30, 58, 95);
            lblTitleStok.Location = new Point(15, 15);
            lblTitleStok.Name = "lblTitleStok";
            lblTitleStok.Size = new Size(144, 28);
            lblTitleStok.TabIndex = 0;
            lblTitleStok.Text = "⚠️ Stok Kritis";
            // 
            // dgvStokKritis
            // 
            dgvStokKritis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStokKritis.Location = new Point(15, 42);
            dgvStokKritis.Name = "dgvStokKritis";
            dgvStokKritis.RowHeadersWidth = 62;
            dgvStokKritis.Size = new Size(480, 330);
            dgvStokKritis.TabIndex = 1;
            // 
            // pnlObatExp
            // 
            pnlObatExp.BackColor = Color.White;
            pnlObatExp.Controls.Add(dgvObatExp);
            pnlObatExp.Controls.Add(lblTitleExp);
            pnlObatExp.Location = new Point(526, 190);
            pnlObatExp.Name = "pnlObatExp";
            pnlObatExp.Size = new Size(510, 380);
            pnlObatExp.TabIndex = 7;
            // 
            // lblTitleExp
            // 
            lblTitleExp.AutoSize = true;
            lblTitleExp.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitleExp.ForeColor = Color.FromArgb(30, 58, 95);
            lblTitleExp.Location = new Point(15, 15);
            lblTitleExp.Name = "lblTitleExp";
            lblTitleExp.Size = new Size(278, 28);
            lblTitleExp.TabIndex = 0;
            lblTitleExp.Text = "📅 Obat Hampir Kadaluarsa";
            // 
            // dgvObatExp
            // 
            dgvObatExp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvObatExp.Location = new Point(15, 42);
            dgvObatExp.Name = "dgvObatExp";
            dgvObatExp.RowHeadersWidth = 62;
            dgvObatExp.Size = new Size(480, 330);
            dgvObatExp.TabIndex = 1;
            // 
            // PanelDashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlObatExp);
            Controls.Add(pnlStokKritis);
            Controls.Add(pnlStatAlert);
            Controls.Add(pnlStatResep);
            Controls.Add(pnlStatTransaksi);
            Controls.Add(pnlStatObat);
            Controls.Add(lblTanggal);
            Controls.Add(lblWelcome);
            Name = "PanelDashboard";
            Size = new Size(1100, 650);
            pnlStatObat.ResumeLayout(false);
            pnlStatObat.PerformLayout();
            pnlStatTransaksi.ResumeLayout(false);
            pnlStatTransaksi.PerformLayout();
            pnlStatResep.ResumeLayout(false);
            pnlStatResep.PerformLayout();
            pnlStatAlert.ResumeLayout(false);
            pnlStatAlert.PerformLayout();
            pnlStokKritis.ResumeLayout(false);
            pnlStokKritis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStokKritis).EndInit();
            pnlObatExp.ResumeLayout(false);
            pnlObatExp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvObatExp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Label lblTanggal;
        private Panel pnlStatObat;
        private Label lblStatObatNum;
        private Label lblStatObatTxt;
        private Panel pnlStatTransaksi;
        private Label lblStatTrxTxt;
        private Label lblStatTrxNum;
        private Panel pnlStatResep;
        private Label lblStatResepTxt;
        private Label lblStatResepNum;
        private Panel pnlStatAlert;
        private Label lblStatAlertTxt;
        private Label lblStatAlertNum;
        private Panel pnlStokKritis;
        private DataGridView dgvStokKritis;
        private Label lblTitleStok;
        private Panel pnlObatExp;
        private DataGridView dgvObatExp;
        private Label lblTitleExp;
    }
}
