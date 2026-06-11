using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;
using ApotekSmart.Interfaces;

namespace ApotekSmart.Models
{
    // [INTERFACE] LaporanBulanan mengimplementasikan interface ILaporan
    // Wajib mengimplementasikan GenerateLaporan() dan CetakLaporan()
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class LaporanBulanan : ILaporan
    {
        // [ASSOCIATION] LaporanBulanan menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa LaporanBulanan
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [INTERFACE] Implementasi method GenerateLaporan() dari ILaporan
        // [ENCAPSULATION] Detail query SQL tersembunyi di dalam method
        // Pemanggil cukup kirim tanggal, dapat DataTable — tidak perlu tahu SQL-nya
        public DataTable GenerateLaporan(DateTime tanggalMulai, DateTime tanggalAkhir)
        {
            if (tanggalMulai > tanggalAkhir)
                throw new ArgumentException(
                    "Tanggal mulai tidak boleh lebih besar dari tanggal akhir.");

            string sql = @"SELECT
                EXTRACT(YEAR  FROM t.created_at) AS tahun,
                EXTRACT(MONTH FROM t.created_at) AS bulan,
                COUNT(t.id_transaksi)            AS jumlah_transaksi,
                SUM(t.total)                     AS total_penjualan,
                COUNT(CASE WHEN t.jenis_transaksi = 'resep'
                           THEN 1 END)           AS transaksi_resep,
                COUNT(CASE WHEN t.jenis_transaksi = 'biasa'
                           THEN 1 END)           AS transaksi_biasa
            FROM transaksi t
            WHERE t.status = 'selesai'
              AND t.created_at BETWEEN @dari AND @sampai
            GROUP BY ROLLUP(
                EXTRACT(YEAR  FROM t.created_at),
                EXTRACT(MONTH FROM t.created_at)
            )
            ORDER BY tahun, bulan";

            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@dari",   tanggalMulai.Date),
                new NpgsqlParameter("@sampai", tanggalAkhir.Date.AddDays(1).AddSeconds(-1))
            };
            return _db.ExecuteQuery(sql, params_);
        }

        // [INTERFACE] Implementasi method CetakLaporan() dari ILaporan
        // [POLYMORPHISM] LaporanBulanan dan LaporanHarian punya CetakLaporan() sendiri
        // meski keduanya sama-sama mengimplementasikan ILaporan
        public void CetakLaporan()
        {
            // TODO: Implementasi cetak/export laporan
            throw new NotImplementedException("CetakLaporan belum diimplementasi.");
        }
    }
}