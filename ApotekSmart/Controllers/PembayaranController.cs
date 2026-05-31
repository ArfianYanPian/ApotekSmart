using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;
using ApotekSmart.Models;

namespace ApotekSmart.Controllers
{
    public class PembayaranController
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public DataTable AmbilObatUntukCombo()
        {
            string sql = @"
                SELECT
                    o.id_obat,
                    o.nama_obat,
                    o.jenis,
                    o.golongan,
                    k.nama_kategori,
                    o.satuan,
                    o.harga_jual AS harga,
                    o.stok,
                    o.stok_minimum,
                    o.tanggal_exp
                FROM obat o
                JOIN kategori k ON k.id_kategori = o.id_kategori
                WHERE o.is_active = TRUE
                  AND o.stok > 0
                  AND o.tanggal_exp > CURRENT_DATE
                ORDER BY o.nama_obat;
            ";

            return _db.ExecuteQuery(sql);
        }

        public decimal HitungTotalSementara(List<ItemTransaksi> items)
        {
            decimal total = 0;

            foreach (ItemTransaksi item in items)
                total += item.Subtotal;

            return total;
        }

        public int SimpanTransaksiDanPembayaran(
            List<ItemTransaksi> items,
            string jenisTransaksi,
            string metodeBayar,
            decimal jumlahBayar,
            int? idKasir = null
        )
        {
            if (items == null || items.Count == 0)
                throw new Exception("Item transaksi masih kosong.");

            decimal total = HitungTotalSementara(items);

            if (jumlahBayar < total)
                throw new Exception("Jumlah bayar kurang dari total pembayaran.");

            int idKasirDipakai = idKasir ?? AmbilIdKasirDefault();
            int idTransaksiBaru;
            decimal kembalian = jumlahBayar - total;

            using (NpgsqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                using (NpgsqlTransaction dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlInsertTransaksi = @"
                            INSERT INTO transaksi (id_kasir, jenis_transaksi, status, total)
                            VALUES (@id_kasir, @jenis_transaksi, 'menunggu', 0)
                            RETURNING id_transaksi;
                        ";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlInsertTransaksi, conn, dbTrans))
                        {
                            cmd.Parameters.AddWithValue("@id_kasir", idKasirDipakai);
                            cmd.Parameters.AddWithValue("@jenis_transaksi", jenisTransaksi.ToLower());
                            idTransaksiBaru = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        foreach (ItemTransaksi item in items)
                        {
                            CekDanKurangiStok(conn, dbTrans, item.IdObat, item.Jumlah);

                            string sqlInsertDetail = @"
                                INSERT INTO detail_transaksi
                                    (id_transaksi, id_obat, qty, harga_satuan, subtotal)
                                VALUES
                                    (@id_transaksi, @id_obat, @qty, @harga_satuan, @subtotal);
                            ";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlInsertDetail, conn, dbTrans))
                            {
                                cmd.Parameters.AddWithValue("@id_transaksi", idTransaksiBaru);
                                cmd.Parameters.AddWithValue("@id_obat", item.IdObat);
                                cmd.Parameters.AddWithValue("@qty", item.Jumlah);
                                cmd.Parameters.AddWithValue("@harga_satuan", item.HargaSatuan);
                                cmd.Parameters.AddWithValue("@subtotal", item.Subtotal);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string sqlInsertPembayaran = @"
                            INSERT INTO pembayaran
                                (id_transaksi, jumlah_bayar, kembalian, metode)
                            VALUES
                                (@id_transaksi, @jumlah_bayar, @kembalian, @metode)
                            ON CONFLICT (id_transaksi)
                            DO UPDATE SET
                                jumlah_bayar = EXCLUDED.jumlah_bayar,
                                kembalian = EXCLUDED.kembalian,
                                metode = EXCLUDED.metode,
                                created_at = CURRENT_TIMESTAMP;
                        ";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlInsertPembayaran, conn, dbTrans))
                        {
                            cmd.Parameters.AddWithValue("@id_transaksi", idTransaksiBaru);
                            cmd.Parameters.AddWithValue("@jumlah_bayar", jumlahBayar);
                            cmd.Parameters.AddWithValue("@kembalian", kembalian);
                            cmd.Parameters.AddWithValue("@metode", metodeBayar.ToLower());
                            cmd.ExecuteNonQuery();
                        }

                        string sqlUpdateTransaksi = @"
                            UPDATE transaksi
                            SET total = @total,
                                status = 'selesai'
                            WHERE id_transaksi = @id_transaksi;
                        ";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlUpdateTransaksi, conn, dbTrans))
                        {
                            cmd.Parameters.AddWithValue("@total", total);
                            cmd.Parameters.AddWithValue("@id_transaksi", idTransaksiBaru);
                            cmd.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }

            return idTransaksiBaru;
        }

        private int AmbilIdKasirDefault()
        {
            string sql = @"
                SELECT id_user
                FROM users
                WHERE role = 'kasir'
                  AND is_active = TRUE
                ORDER BY id_user
                LIMIT 1;
            ";

            DataTable table = _db.ExecuteQuery(sql);

            if (table.Rows.Count == 0)
                throw new Exception("Data kasir aktif belum ada di tabel users.");

            return Convert.ToInt32(table.Rows[0]["id_user"]);
        }

        private void CekDanKurangiStok(NpgsqlConnection conn, NpgsqlTransaction dbTrans, int idObat, int jumlah)
        {
            int stokSekarang;

            string sqlCekStok = @"
                SELECT stok
                FROM obat
                WHERE id_obat = @id_obat
                FOR UPDATE;
            ";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCekStok, conn, dbTrans))
            {
                cmd.Parameters.AddWithValue("@id_obat", idObat);
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    throw new Exception("Obat tidak ditemukan.");

                stokSekarang = Convert.ToInt32(result);
            }

            if (stokSekarang < jumlah)
                throw new Exception("Stok obat tidak cukup. Stok tersedia: " + stokSekarang + ", jumlah diminta: " + jumlah);

            string sqlKurangiStok = @"
                UPDATE obat
                SET stok = stok - @jumlah
                WHERE id_obat = @id_obat;
            ";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlKurangiStok, conn, dbTrans))
            {
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@id_obat", idObat);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
