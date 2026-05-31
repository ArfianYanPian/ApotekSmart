using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class TransaksiController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // Proses transaksi biasa — panggil sp_proses_transaksi
        public bool ProsesBayar(int idKasir, string jenisTransaksi, string itemsJson, decimal jumlahBayar)
        {
            try
            {
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("p_kasir_id", idKasir),
                    new NpgsqlParameter("p_jenis", jenisTransaksi),
                    new NpgsqlParameter("p_items", itemsJson),
                    new NpgsqlParameter("p_jumlah_bayar", jumlahBayar)
                };
                db.ExecuteProcedure("sp_proses_transaksi", params);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal proses transaksi: " + ex.Message);
            }
        }

        // Ambil riwayat transaksi
        public DataTable GetRiwayatTransaksi()
        {
            return _db.ExecuteQuery(@"SELECT * FROM v_transaksi_detail 
                                      ORDER BY tanggal DESC");
        }

        // Ambil transaksi berdasarkan ID
        public DataTable GetTransaksiById(int idTransaksi)
        {
            string sql = "SELECT * FROM v_transaksi_detail WHERE id_transaksi = @id";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", idTransaksi)
            };
            return db.ExecuteQuery(sql, params);
        }

        // Buat transaksi resep baru (status menunggu)
        public bool BuatTransaksiResep(int idKasir, string nomorResep, string namaPasien, string namaDokter)
        {
            try
            {
                // Insert transaksi dulu
                string sqlTransaksi = @"INSERT INTO transaksi (id_kasir, jenis_transaksi, status, total)
                                        VALUES (@idKasir, 'resep', 'menunggu', 0)
                                        RETURNING id_transaksi";
                var paramsTransaksi = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idKasir", idKasir)
                };
                DataTable dt = _db.ExecuteQuery(sqlTransaksi, paramsTransaksi);
                int idTransaksi = Convert.ToInt32(dt.Rows[0]["id_transaksi"]);

                // Insert resep
                string sqlResep = @"INSERT INTO resep (id_transaksi, nomor_resep, nama_pasien, nama_dokter, status_validasi)
                                    VALUES (@idTransaksi, @nomorResep, @namaPasien, @namaDokter, 'menunggu')";
                var paramsResep = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idTransaksi", idTransaksi),
                    new NpgsqlParameter("@nomorResep", nomorResep),
                    new NpgsqlParameter("@namaPasien", namaPasien),
                    new NpgsqlParameter("@namaDokter", namaDokter)
                };
                return _db.ExecuteNonQuery(sqlResep, paramsResep) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal buat transaksi resep: " + ex.Message);
            }
        }

        // Cek status validasi resep
        public string CekStatusResep(string nomorResep)
        {
            string sql = "SELECT status_validasi FROM resep WHERE nomor_resep = @nomor";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@nomor", nomorResep)
            };
            DataTable dt = db.ExecuteQuery(sql, params);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["status_validasi"].ToString();
            return null;
        }
    }
}