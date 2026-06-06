using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;
using ApotekSmart.Models;

namespace ApotekSmart.Controllers
{
    public class LaporanController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        private void ValidasiTanggal(DateTime dari, DateTime sampai)
        {
            if (dari > sampai)
                throw new ArgumentException(
                    "Tanggal mulai tidak boleh lebih besar dari tanggal akhir.");
        }

        public DataTable GetLaporanHarian(DateTime dari, DateTime sampai)
        {
            ValidasiTanggal(dari, sampai);
            try
            {
                return new LaporanHarian().GenerateLaporan(dari, sampai);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan harian: " + ex.Message);
            }
        }

        public DataTable GetLaporanBulanan(DateTime dari, DateTime sampai)
        {
            ValidasiTanggal(dari, sampai);
            try
            {
                return new LaporanBulanan().GenerateLaporan(dari, sampai);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan bulanan: " + ex.Message);
            }
        }

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
                    new NpgsqlParameter("@dari",   dari),
                    new NpgsqlParameter("@sampai", sampai)
                };
                return _db.ExecuteQuery(sql, params_);
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal generate laporan resep: " + ex.Message);
            }
        }

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