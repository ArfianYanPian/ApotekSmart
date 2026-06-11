using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;
using NpgsqlTypes;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class TransaksiController
    {
        // [ASSOCIATION] TransaksiController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa TransaksiController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [ENCAPSULATION] Detail pemanggilan stored procedure tersembunyi
        // Pemanggil cukup kirim parameter, tidak perlu tahu cara kerja SP di PostgreSQL
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
                    new NpgsqlParameter("p_items",        NpgsqlTypes.NpgsqlDbType.Json) { Value = itemsJson },
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

        // [ENCAPSULATION] Detail query VIEW v_transaksi_detail tersembunyi
        // Pemanggil cukup dapat DataTable tanpa tahu struktur query-nya
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

        // [COMPOSITION] BuatTransaksiResep menggunakan NpgsqlTransaction
        // Transaction dibuat dan dimiliki sepenuhnya oleh method ini
        // Jika method selesai/gagal, transaction ikut selesai/di-rollback
        // [ENCAPSULATION] Detail atomicity (commit/rollback) tersembunyi dari pemanggil
        // Pemanggil hanya tahu: berhasil = true, gagal = exception
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

            // [COMPOSITION] conn dan tx dibuat di sini, hidup dan mati di dalam method ini
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: insert transaksi
                        // Jika step ini gagal → tx.Rollback() → resep tidak ikut tersimpan
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
                        // Jika step ini gagal → tx.Rollback() → transaksi di step 1 ikut dibatalkan
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

                        // Kedua step berhasil → commit
                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Salah satu step gagal → rollback semua
                        tx.Rollback();
                        throw new Exception("Gagal buat transaksi resep: " + ex.Message);
                    }
                }
            }
        }

        // [ENCAPSULATION] Detail query status resep tersembunyi
        // Pemanggil cukup kirim nomor resep, dapat string status
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