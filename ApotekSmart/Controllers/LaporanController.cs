using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;
using ApotekSmart.Models;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class LaporanController
    {
        // [ASSOCIATION] LaporanController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa LaporanController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [ENCAPSULATION] ValidasiTanggal() private — logika validasi tersembunyi
        // Dipakai ulang oleh 3 method laporan tanpa duplikasi kode
        private void ValidasiTanggal(DateTime dari, DateTime sampai)
        {
            if (dari > sampai)
                throw new ArgumentException(
                    "Tanggal mulai tidak boleh lebih besar dari tanggal akhir.");
        }

        // [POLYMORPHISM] Membuat objek LaporanHarian yang implements ILaporan
        // LaporanController tidak perlu tahu detail implementasi GenerateLaporan()
        // Cukup panggil lewat interface ILaporan
        // [ENCAPSULATION] ValidasiTanggal() dipanggil dulu sebelum generate
        public DataTable GetLaporanHarian(DateTime dari, DateTime sampai)
        {
            ValidasiTanggal(dari, sampai);
            try
            {
                // [POLYMORPHISM] new LaporanHarian() bertipe ILaporan
                // GenerateLaporan() dipanggil sesuai implementasi LaporanHarian
                return new LaporanHarian().GenerateLaporan(dari, sampai);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan harian: " + ex.Message);
            }
        }

        // [POLYMORPHISM] Membuat objek LaporanBulanan yang implements ILaporan
        // Cara pemanggilan sama persis dengan GetLaporanHarian()
        // tapi GenerateLaporan() yang dijalankan berbeda
        public DataTable GetLaporanBulanan(DateTime dari, DateTime sampai)
        {
            ValidasiTanggal(dari, sampai);
            try
            {
                // [POLYMORPHISM] new LaporanBulanan() — GenerateLaporan() berbeda
                // GROUP BY ROLLUP(tahun, bulan) vs ROLLUP(tahun, bulan, hari)
                return new LaporanBulanan().GenerateLaporan(dari, sampai);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan bulanan: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail query UNION ALL transaksi biasa + resep tersembunyi
        // Pemanggil cukup kirim tanggal, dapat DataTable gabungan
        public DataTable GetLaporanResep(DateTime dari, DateTime sampai)
        {
            ValidasiTanggal(dari, sampai);
            try
            {
                string sql = @"SELECT 
                    t.id_transaksi,
                    t.created_at AS tanggal,
                    'Biasa'      AS jenis,
                    t.total,
                    u.nama       AS kasir,
                    NULL         AS nomor_resep
                FROM transaksi t
                JOIN users u ON t.id_kasir = u.id_user
                WHERE t.jenis_transaksi = 'biasa'
                  AND t.created_at BETWEEN @dari AND @sampai

                UNION ALL

                SELECT 
                    t.id_transaksi,
                    t.created_at  AS tanggal,
                    'Resep'       AS jenis,
                    t.total,
                    u.nama        AS kasir,
                    r.nomor_resep
                FROM transaksi t
                JOIN users u ON t.id_kasir = u.id_user
                JOIN resep  r ON t.id_transaksi = r.id_transaksi
                WHERE t.jenis_transaksi = 'resep'
                  AND t.created_at BETWEEN @dari AND @sampai
                ORDER BY tanggal DESC";

                var params_ = new NpgsqlParameter[] {
                    new NpgsqlParameter("@dari",   dari.Date),
                    new NpgsqlParameter("@sampai", sampai.Date.AddDays(1).AddSeconds(-1))
                };
                return _db.ExecuteQuery(sql, params_);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan resep: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail 4 subquery COUNT tersembunyi di dalam method
        // Pemanggil cukup dapat 1 DataRow berisi 4 angka summary
        public DataTable GetSummaryDashboard()
        {
            try
            {
                string sql = @"SELECT
                    (SELECT COUNT(*) FROM obat 
                        WHERE is_active = true)                    AS total_obat,
                    (SELECT COUNT(*) FROM transaksi 
                        WHERE DATE(created_at) = CURRENT_DATE)     AS transaksi_hari_ini,
                    (SELECT COUNT(*) FROM resep 
                        WHERE status_validasi = 'menunggu')        AS resep_menunggu,
                    (SELECT COUNT(*) FROM alert_stok 
                        WHERE is_read = false)                     AS alert_stok";
                return _db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil summary dashboard: " + ex.Message);
            }
        }
    }
}