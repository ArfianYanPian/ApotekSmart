using ApotekSmart.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApotekSmart
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TEST KONEKSI DATABASE
            try
            {
                var db = DatabaseHelper.Instance;
                var dt = db.ExecuteQuery("SELECT * FROM users");
                MessageBox.Show("Koneksi berhasil! Jumlah user: " + dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
