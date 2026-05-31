using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class LaporanController
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public DataTable GetLaporanHarian(DateTime tanggal)
        {
            string sql = @"
                SELECT
                    CASE 
                        WHEN GROUPING(t.created_at::date) = 1 THEN NULL
                        ELSE t.created_at::date
                    END AS tanggal,

                    CASE 
                        WHEN GROUPING(p.metode) = 1 THEN 'SEMUA METODE'
                        ELSE COALESCE(p.metode, 'BELUM BAYAR')
                    END AS metode,

                    CASE 
                        WHEN GROUPING(k.nama_kategori) = 1 THEN 'SEMUA KATEGORI'
                        ELSE k.nama_kategori
                    END AS kategori,

                    SUM(d.qty) AS total_qty,
                    SUM(d.subtotal) AS total_penjualan

                FROM transaksi t
                JOIN detail_transaksi d ON d.id_transaksi = t.id_transaksi
                JOIN obat o ON o.id_obat = d.id_obat
                JOIN kategori k ON k.id_kategori = o.id_kategori
                LEFT JOIN pembayaran p ON p.id_transaksi = t.id_transaksi

                WHERE t.status = 'selesai'
                  AND t.created_at::date = @tanggal

                GROUP BY GROUPING SETS (
                    (t.created_at::date, p.metode, k.nama_kategori),
                    (t.created_at::date, p.metode),
                    (t.created_at::date),
                    ()
                )

                ORDER BY tanggal NULLS LAST, metode NULLS LAST, kategori NULLS LAST;
            ";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@tanggal", tanggal.Date)
            };

            return _db.ExecuteQuery(sql, parameters);
        }

        public DataTable GetLaporanBulanan(int tahun, int bulan)
        {
            DateTime periode = new DateTime(tahun, bulan, 1);

            string sql = @"
                SELECT
                    CASE 
                        WHEN GROUPING(k.nama_kategori) = 1 THEN 'SEMUA KATEGORI'
                        ELSE k.nama_kategori
                    END AS kategori,

                    CASE 
                        WHEN GROUPING(DATE_TRUNC('month', t.created_at)::date) = 1 THEN 'SEMUA BULAN'
                        ELSE TO_CHAR(DATE_TRUNC('month', t.created_at)::date, 'YYYY-MM')
                    END AS bulan,

                    CASE 
                        WHEN GROUPING(o.nama_obat) = 1 AND GROUPING(k.nama_kategori) = 0 THEN 'TOTAL PER KATEGORI'
                        WHEN GROUPING(o.nama_obat) = 1 AND GROUPING(k.nama_kategori) = 1 THEN 'TOTAL KESELURUHAN'
                        ELSE o.nama_obat
                    END AS nama_obat,

                    SUM(d.qty) AS total_qty,
                    SUM(d.subtotal) AS total_penjualan

                FROM transaksi t
                JOIN detail_transaksi d ON d.id_transaksi = t.id_transaksi
                JOIN obat o ON o.id_obat = d.id_obat
                JOIN kategori k ON k.id_kategori = o.id_kategori
                LEFT JOIN pembayaran p ON p.id_transaksi = t.id_transaksi

                WHERE t.status = 'selesai'
                  AND DATE_TRUNC('month', t.created_at)::date = DATE_TRUNC('month', @periode::date)::date

                GROUP BY ROLLUP (
                    DATE_TRUNC('month', t.created_at)::date,
                    k.nama_kategori,
                    o.nama_obat
                )

                ORDER BY kategori NULLS LAST, bulan NULLS LAST, nama_obat NULLS LAST;
            ";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@periode", periode)
            };

            return _db.ExecuteQuery(sql, parameters);
        }

        public DataTable GetRingkasanLaporan(DateTime tanggal)
        {
            string sql = @"
                SELECT
                    CASE 
                        WHEN GROUPING(k.nama_kategori) = 1 THEN 'SEMUA KATEGORI'
                        ELSE k.nama_kategori
                    END AS kategori,

                    CASE 
                        WHEN GROUPING(p.metode) = 1 THEN 'SEMUA METODE'
                        ELSE COALESCE(p.metode, 'BELUM BAYAR')
                    END AS metode,

                    SUM(d.qty) AS total_qty,
                    SUM(d.subtotal) AS total_penjualan

                FROM transaksi t
                JOIN detail_transaksi d ON d.id_transaksi = t.id_transaksi
                JOIN obat o ON o.id_obat = d.id_obat
                JOIN kategori k ON k.id_kategori = o.id_kategori
                LEFT JOIN pembayaran p ON p.id_transaksi = t.id_transaksi

                WHERE t.status = 'selesai'
                  AND t.created_at::date = @tanggal

                GROUP BY CUBE (k.nama_kategori, p.metode)

                ORDER BY kategori NULLS LAST, metode NULLS LAST;
            ";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@tanggal", tanggal.Date)
            };

            return _db.ExecuteQuery(sql, parameters);
        }

        public DataTable GetObatLakuBulanIni(DateTime bulanDipilih)
        {
            DateTime periode = new DateTime(bulanDipilih.Year, bulanDipilih.Month, 1);

            string sql = @"
                (
                    SELECT DISTINCT
                        o.id_obat,
                        o.nama_obat
                    FROM transaksi t
                    JOIN detail_transaksi d ON d.id_transaksi = t.id_transaksi
                    JOIN obat o ON o.id_obat = d.id_obat
                    WHERE t.status = 'selesai'
                      AND DATE_TRUNC('month', t.created_at)::date = DATE_TRUNC('month', @periode::date)::date
                )
                EXCEPT
                (
                    SELECT DISTINCT
                        o.id_obat,
                        o.nama_obat
                    FROM transaksi t
                    JOIN detail_transaksi d ON d.id_transaksi = t.id_transaksi
                    JOIN obat o ON o.id_obat = d.id_obat
                    WHERE t.status = 'selesai'
                      AND DATE_TRUNC('month', t.created_at)::date =
                          (DATE_TRUNC('month', @periode::date) - INTERVAL '1 month')::date
                )
                ORDER BY nama_obat;
            ";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@periode", periode)
            };

            return _db.ExecuteQuery(sql, parameters);
        }
    }
}