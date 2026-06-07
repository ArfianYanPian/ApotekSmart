namespace ApotekSmart.Views
{
    partial class FormMain
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
            pnlSidebar = new Panel();
            btnLogout = new Button();
            lblUserInfo = new Label();
            pnlMenuArea = new Panel();
            pnlGarisSidebar = new Panel();
            lblSidebarSub = new Label();
            lblSidebarLogo = new Label();
            pnlTopbar = new Panel();
            lblTopUserInfo = new Label();
            lblPageTitle = new Label();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            pnlTopbar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(30, 56, 95);
            pnlSidebar.Controls.Add(btnLogout);
            pnlSidebar.Controls.Add(lblUserInfo);
            pnlSidebar.Controls.Add(pnlMenuArea);
            pnlSidebar.Controls.Add(pnlGarisSidebar);
            pnlSidebar.Controls.Add(lblSidebarSub);
            pnlSidebar.Controls.Add(lblSidebarLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 712);
            pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(162, 45, 45);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(20, 674);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(180, 32);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "KELUAR";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Font = new Font("Segoe UI", 8F);
            lblUserInfo.ForeColor = Color.FromArgb(122, 174, 212);
            lblUserInfo.Location = new Point(20, 645);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(180, 26);
            lblUserInfo.TabIndex = 5;
            lblUserInfo.Text = "👤 Nama User";
            // 
            // pnlMenuArea
            // 
            pnlMenuArea.AutoScroll = true;
            pnlMenuArea.BackColor = Color.Transparent;
            pnlMenuArea.Location = new Point(0, 80);
            pnlMenuArea.Name = "pnlMenuArea";
            pnlMenuArea.Size = new Size(220, 560);
            pnlMenuArea.TabIndex = 4;
            // 
            // pnlGarisSidebar
            // 
            pnlGarisSidebar.BackColor = Color.FromArgb(42, 80, 128);
            pnlGarisSidebar.Location = new Point(20, 57);
            pnlGarisSidebar.Name = "pnlGarisSidebar";
            pnlGarisSidebar.Size = new Size(180, 1);
            pnlGarisSidebar.TabIndex = 3;
            // 
            // lblSidebarSub
            // 
            lblSidebarSub.AutoSize = true;
            lblSidebarSub.BackColor = Color.Transparent;
            lblSidebarSub.ForeColor = Color.FromArgb(122, 174, 212);
            lblSidebarSub.Location = new Point(24, 57);
            lblSidebarSub.Name = "lblSidebarSub";
            lblSidebarSub.Size = new Size(167, 25);
            lblSidebarSub.TabIndex = 2;
            lblSidebarSub.Text = "Manajemen Apotek";
            // 
            // lblSidebarLogo
            // 
            lblSidebarLogo.AutoSize = true;
            lblSidebarLogo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblSidebarLogo.ForeColor = Color.White;
            lblSidebarLogo.Location = new Point(20, 20);
            lblSidebarLogo.Name = "lblSidebarLogo";
            lblSidebarLogo.Size = new Size(178, 36);
            lblSidebarLogo.TabIndex = 1;
            lblSidebarLogo.Text = "ApotekSmart";
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.White;
            pnlTopbar.Controls.Add(lblTopUserInfo);
            pnlTopbar.Controls.Add(lblPageTitle);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(220, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1124, 56);
            pnlTopbar.TabIndex = 1;
            // 
            // lblTopUserInfo
            // 
            lblTopUserInfo.AutoSize = true;
            lblTopUserInfo.ForeColor = Color.Gray;
            lblTopUserInfo.Location = new Point(920, 20);
            lblTopUserInfo.Name = "lblTopUserInfo";
            lblTopUserInfo.Size = new Size(150, 25);
            lblTopUserInfo.TabIndex = 1;
            lblTopUserInfo.Text = "Nama User - Role";
            // 
            // lblPageTitle
            // 
            lblPageTitle.AutoSize = true;
            lblPageTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPageTitle.ForeColor = Color.FromArgb(30, 58, 95);
            lblPageTitle.Location = new Point(20, 14);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(159, 38);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Dashboard";
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 56);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(1124, 656);
            pnlContent.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1344, 712);
            Controls.Add(pnlContent);
            Controls.Add(pnlTopbar);
            Controls.Add(pnlSidebar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ApotekSmart";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlTopbar;
        private Panel pnlContent;
        private Label lblSidebarSub;
        private Label lblSidebarLogo;
        private Panel pnlMenuArea;
        private Panel pnlGarisSidebar;
        private Button btnLogout;
        private Label lblUserInfo;
        private Label lblPageTitle;
        private Label lblTopUserInfo;
    }
}