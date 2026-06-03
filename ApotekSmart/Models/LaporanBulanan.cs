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

        public bool ProsesBayar(int idKasir, string jenisTransaksi,
                                 string itemsJson, decimal jumlahBayar)
        {
            try
            {
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("p_kasir_id",    idKasir),
                    new NpgsqlParameter("p_jenis",       jenisTransaksi),
                    new NpgsqlParameter("p_items",       itemsJson),
                    new NpgsqlParameter("p_jumlah_bayar", jumlahBayar)
                };
                // FIX: db bukan db, params bukan params
                db.ExecuteProcedure("sp_proses_transaksi", params);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal proses transaksi: " + ex.Message);
            }
        }

        public DataTable GetRiwayatTransaksi()
        {
            return _db.ExecuteQuery(
                "SELECT * FROM v_transaksi_detail ORDER BY tanggal DESC");
        }

        public DataTable GetTransaksiById(int idTransaksi)
        {
            string sql = "SELECT * FROM v_transaksi_detail WHERE id_transaksi = @id";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", idTransaksi)
            };
            // FIX: db bukan db, params bukan params
            return db.ExecuteQuery(sql, params);
        }

        public bool BuatTransaksiResep(int idKasir, string nomorResep,
                                        string namaPasien, string namaDokter)
        {
            try
            {
                string sqlTransaksi = @"INSERT INTO transaksi 
                    (id_kasir, jenis_transaksi, status, total)
                    VALUES (@idKasir, 'resep', 'menunggu', 0)
                    RETURNING id_transaksi";
                var paramsT = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idKasir", idKasir)
                };
                DataTable dt = _db.ExecuteQuery(sqlTransaksi, paramsT);
                int idTransaksi = Convert.ToInt32(dt.Rows[0]["id_transaksi"]);

                string sqlResep = @"INSERT INTO resep 
                    (id_transaksi, nomor_resep, nama_pasien, nama_dokter, status_validasi)
                    VALUES (@idTr, @nomor, @pasien, @dokter, 'menunggu')";
                var paramsR = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idTr",   idTransaksi),
                    new NpgsqlParameter("@nomor",  nomorResep),
                    new NpgsqlParameter("@pasien", namaPasien),
                    new NpgsqlParameter("@dokter", namaDokter)
                };
                return _db.ExecuteNonQuery(sqlResep, paramsR) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal buat transaksi resep: " + ex.Message);
            }
        }

        public string CekStatusResep(string nomorResep)
        {
            string sql = "SELECT status_validasi FROM resep WHERE nomor_resep = @nomor";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@nomor", nomorResep)
            };
            // FIX: db bukan db, params bukan params
            DataTable dt = db.ExecuteQuery(sql, params);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["status_validasi"].ToString();
            return null;
        }
    }
}