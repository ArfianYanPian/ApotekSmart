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
            if (idKasir <= 0)
                throw new ArgumentException("IdKasir harus lebih dari 0.");
            if (string.IsNullOrWhiteSpace(itemsJson))
                throw new ArgumentException("Items tidak boleh kosong.");
            if (jumlahBayar <= 0)
                throw new ArgumentException("Jumlah bayar harus lebih dari 0.");
            try
            {
                var params_ = new NpgsqlParameter[] {
                    new NpgsqlParameter("p_kasir_id",     idKasir),
                    new NpgsqlParameter("p_jenis",        jenisTransaksi),
                    new NpgsqlParameter("p_items",        itemsJson),
                    new NpgsqlParameter("p_jumlah_bayar", jumlahBayar)
                };
                _db.ExecuteProcedure("sp_proses_transaksi", params_);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal proses transaksi: " + ex.Message);
            }
        }

        public DataTable GetRiwayatTransaksi()
        {
            try
            {
                return _db.ExecuteQuery(
                    "SELECT * FROM v_transaksi_detail ORDER BY tanggal DESC");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil riwayat transaksi: " + ex.Message);
            }
        }

        public DataTable GetTransaksiById(int idTransaksi)
        {
            if (idTransaksi <= 0)
                throw new ArgumentException("IdTransaksi harus lebih dari 0.");
            try
            {
                string sql = "SELECT * FROM v_transaksi_detail WHERE id_transaksi = @id";
                return _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@id", idTransaksi)
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil transaksi: " + ex.Message);
            }
        }

        // FIX: bungkus dalam NpgsqlTransaction agar insert transaksi + resep atomic
        public bool BuatTransaksiResep(int idKasir, string nomorResep,
                                        string namaPasien, string namaDokter)
        {
            if (idKasir <= 0)
                throw new ArgumentException("IdKasir harus lebih dari 0.");
            if (string.IsNullOrWhiteSpace(nomorResep))
                throw new ArgumentException("Nomor resep tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(namaPasien))
                throw new ArgumentException("Nama pasien tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(namaDokter))
                throw new ArgumentException("Nama dokter tidak boleh kosong.");

            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: insert transaksi
                        string sqlTransaksi = @"INSERT INTO transaksi 
                            (id_kasir, jenis_transaksi, status, total)
                            VALUES (@idKasir, 'resep', 'menunggu', 0)
                            RETURNING id_transaksi";
                        int idTransaksi;
                        using (var cmd = new NpgsqlCommand(sqlTransaksi, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@idKasir", idKasir);
                            idTransaksi = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Step 2: insert resep
                        string sqlResep = @"INSERT INTO resep 
                            (id_transaksi, nomor_resep, nama_pasien, nama_dokter, status_validasi)
                            VALUES (@idTr, @nomor, @pasien, @dokter, 'menunggu')";
                        using (var cmd = new NpgsqlCommand(sqlResep, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@idTr", idTransaksi);
                            cmd.Parameters.AddWithValue("@nomor", nomorResep);
                            cmd.Parameters.AddWithValue("@pasien", namaPasien);
                            cmd.Parameters.AddWithValue("@dokter", namaDokter);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw new Exception("Gagal buat transaksi resep: " + ex.Message);
                    }
                }
            }
        }

        public string CekStatusResep(string nomorResep)
        {
            if (string.IsNullOrWhiteSpace(nomorResep))
                throw new ArgumentException("Nomor resep tidak boleh kosong.");
            try
            {
                string sql = "SELECT status_validasi FROM resep WHERE nomor_resep = @nomor";
                DataTable dt = _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@nomor", nomorResep)
                });
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["status_validasi"].ToString();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal cek status resep: " + ex.Message);
            }
        }
    }
}