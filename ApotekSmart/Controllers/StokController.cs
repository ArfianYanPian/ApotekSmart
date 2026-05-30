using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class StokController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // Update stok masuk — panggil sp_update_stok
        public bool UpdateStok(int idObat, int qtyMasuk, string keterangan, int idUser)
        {
            try
            {
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("p_obat_id", idObat),
                    new NpgsqlParameter("p_qty_masuk", qtyMasuk),
                    new NpgsqlParameter("p_keterangan", keterangan),
                    new NpgsqlParameter("p_user_id", idUser)
                };
                _db.ExecuteProcedure("sp_update_stok", params_);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal update stok: " + ex.Message);
            }
        }

        // Ambil obat dengan stok kritis (stok <= stok_minimum)
        public DataTable GetStokKritis()
        {
            string sql = @"SELECT id_obat, nama_obat, nama_kategori, stok, stok_minimum 
                          FROM v_stok_obat 
                          WHERE status_stok IN ('KRITIS', 'HABIS')
                          ORDER BY stok ASC";
            return _db.ExecuteQuery(sql);
        }

        // Ambil obat hampir kadaluarsa dan kadaluarsa
        public DataTable GetObatKadaluarsa()
        {
            return _db.ExecuteQuery("SELECT * FROM v_obat_kadaluarsa");
        }

        // Ambil log perubahan stok
        public DataTable GetLogStok(int? idObat = null)
        {
            string sql;
            if (idObat.HasValue)
            {
                sql = @"SELECT l.*, o.nama_obat, u.nama AS nama_user 
                        FROM log_stok l
                        JOIN obat o ON l.id_obat = o.id_obat
                        LEFT JOIN users u ON l.id_user = u.id_user
                        WHERE l.id_obat = @idObat
                        ORDER BY l.created_at DESC";
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idObat", idObat.Value)
                };
                return _db.ExecuteQuery(sql, params_);
            }
            else
            {
                sql = @"SELECT l.*, o.nama_obat, u.nama AS nama_user 
                        FROM log_stok l
                        JOIN obat o ON l.id_obat = o.id_obat
                        LEFT JOIN users u ON l.id_user = u.id_user
                        ORDER BY l.created_at DESC";
                return _db.ExecuteQuery(sql);
            }
        }

        // Ambil alert stok yang belum dibaca
        public DataTable GetAlertStok()
        {
            string sql = @"SELECT a.*, o.nama_obat 
                          FROM alert_stok a
                          JOIN obat o ON a.id_obat = o.id_obat
                          WHERE a.is_read = false
                          ORDER BY a.created_at DESC";
            return _db.ExecuteQuery(sql);
        }

        // Tandai alert sudah dibaca
        public bool TandaiAlertDibaca(int idAlert)
        {
            string sql = "UPDATE alert_stok SET is_read = true WHERE id_alert = @id";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", idAlert)
            };
            return _db.ExecuteNonQuery(sql, params_) > 0;
        }
    }
}