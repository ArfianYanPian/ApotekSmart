using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;
using ApotekSmart.Interfaces;

namespace ApotekSmart.Models
{
    public class LaporanHarian : ILaporan
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public DataTable GenerateLaporan(DateTime tanggalMulai, DateTime tanggalAkhir)
        {
            string sql = @"SELECT 
                EXTRACT(YEAR FROM t.created_at)  AS tahun,
                EXTRACT(MONTH FROM t.created_at) AS bulan,
                EXTRACT(DAY FROM t.created_at)   AS hari,
                COUNT(t.id_transaksi)            AS jumlah_transaksi,
                SUM(t.total)                     AS total_penjualan
            FROM transaksi t
            WHERE t.status = 'selesai'
              AND t.created_at BETWEEN @dari AND @sampai
            GROUP BY ROLLUP(
                EXTRACT(YEAR  FROM t.created_at),
                EXTRACT(MONTH FROM t.created_at),
                EXTRACT(DAY   FROM t.created_at)
            )
            ORDER BY tahun, bulan, hari";

            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@dari",   tanggalMulai),
                new NpgsqlParameter("@sampai", tanggalAkhir)
            };

            // FIX: db bukan db, params bukan params
            return db.ExecuteQuery(sql, params);
        }

        public void CetakLaporan()
        {
            Console.WriteLine("Laporan Harian ApotekSmart");
        }
    }
}