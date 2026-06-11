using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class StokController
    {
        // [ASSOCIATION] StokController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa StokController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [ENCAPSULATION] Detail pemanggilan stored procedure sp_update_stok tersembunyi
        // Trigger trg_log_perubahan_stok dan trg_cek_stok_minimum
        // jalan otomatis di DB — pemanggil tidak perlu tahu
        public bool UpdateStok(int idObat, int qtyMasuk,
                               string keterangan, int idUser)
        {
            if (idObat <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");
            if (qtyMasuk <= 0)
                throw new ArgumentException("Qty masuk harus lebih dari 0.");
            if (string.IsNullOrWhiteSpace(keterangan))
                throw new ArgumentException("Keterangan tidak boleh kosong.");
            if (idUser <= 0)
                throw new ArgumentException("IdUser harus lebih dari 0.");
            try
            {
                var params_ = new NpgsqlParameter[] {
                    new NpgsqlParameter("p_obat_id",    idObat),
                    new NpgsqlParameter("p_qty_masuk",  qtyMasuk),
                    new NpgsqlParameter("p_keterangan", keterangan),
                    new NpgsqlParameter("p_user_id",    idUser)
                };
                _db.ExecuteProcedure("sp_update_stok", params_);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal update stok: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail query VIEW v_stok_obat tersembunyi
        // Pemanggil cukup dapat DataTable stok kritis
        public DataTable GetStokKritis()
        {
            try
            {
                return _db.ExecuteQuery(@"SELECT id_obat, nama_obat, nama_kategori, 
                                              stok, stok_minimum 
                                          FROM v_stok_obat 
                                          WHERE status_stok IN ('KRITIS','HABIS')
                                          ORDER BY stok ASC");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil stok kritis: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail query VIEW v_obat_kadaluarsa tersembunyi
        // Pemanggil tidak perlu tahu struktur view-nya
        public DataTable GetObatKadaluarsa()
        {
            try
            {
                return _db.ExecuteQuery("SELECT * FROM v_obat_kadaluarsa");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil obat kadaluarsa: " + ex.Message);
            }
        }

        // [ENCAPSULATION] GetLogStok() menyembunyikan 2 query berbeda
        // (filter by idObat vs semua) di balik 1 method dengan parameter opsional
        // Pemanggil tidak perlu tahu ada 2 query berbeda di dalamnya
        public DataTable GetLogStok(int? idObat = null)
        {
            if (idObat.HasValue && idObat.Value <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");
            try
            {
                if (idObat.HasValue)
                {
                    string sql = @"SELECT l.*, o.nama_obat, u.nama AS nama_user 
                                  FROM log_stok l
                                  JOIN obat  o ON l.id_obat  = o.id_obat
                                  LEFT JOIN users u ON l.id_user = u.id_user
                                  WHERE l.id_obat = @idObat
                                  ORDER BY l.created_at DESC";
                    return _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                        new NpgsqlParameter("@idObat", idObat.Value)
                    });
                }
                return _db.ExecuteQuery(@"SELECT l.*, o.nama_obat, u.nama AS nama_user 
                                          FROM log_stok l
                                          JOIN obat  o ON l.id_obat  = o.id_obat
                                          LEFT JOIN users u ON l.id_user = u.id_user
                                          ORDER BY l.created_at DESC");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil log stok: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail query alert_stok tersembunyi
        // Pemanggil cukup dapat DataTable alert yang belum dibaca
        public DataTable GetAlertStok()
        {
            try
            {
                return _db.ExecuteQuery(@"SELECT a.*, o.nama_obat 
                                          FROM alert_stok a
                                          JOIN obat o ON a.id_obat = o.id_obat
                                          WHERE a.is_read = false
                                          ORDER BY a.created_at DESC");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil alert stok: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail UPDATE alert_stok tersembunyi dari pemanggil
        // Pemanggil cukup kirim idAlert, tidak perlu tahu SQL-nya
        public bool TandaiAlertDibaca(int idAlert)
        {
            if (idAlert <= 0)
                throw new ArgumentException("IdAlert harus lebih dari 0.");
            try
            {
                string sql = "UPDATE alert_stok SET is_read = true WHERE id_alert = @id";
                return _db.ExecuteNonQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@id", idAlert)
                }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal tandai alert: " + ex.Message);
            }
        }
    }
}