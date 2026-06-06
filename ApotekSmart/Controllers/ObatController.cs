using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class ObatController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public DataTable GetAllObat()
        {
            return _db.ExecuteQuery("SELECT * FROM v_stok_obat ORDER BY nama_obat");
        }

        public DataTable GetObatById(int idObat)
        {
            if (idObat <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");

            string sql = "SELECT * FROM v_stok_obat WHERE id_obat = @id";
            return _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                new NpgsqlParameter("@id", idObat)
            });
        }

        public DataTable SearchObat(string nama = "", string kategori = "",
                                    decimal hargaMin = 0, decimal hargaMax = 999999999)
        {
            if (hargaMin < 0)
                throw new ArgumentException("Harga minimum tidak boleh negatif.");
            if (hargaMax < hargaMin)
                throw new ArgumentException("Harga maksimum tidak boleh kurang dari minimum.");

            string sql = @"SELECT * FROM v_stok_obat 
                          WHERE LOWER(nama_obat) LIKE LOWER(@nama)
                            AND (@kategori = '' OR LOWER(nama_kategori) = LOWER(@kategori))
                            AND harga_jual BETWEEN @hargaMin AND @hargaMax
                          ORDER BY nama_obat";
            var params_ = new NpgsqlParameter[] {
                new NpgsqlParameter("@nama",     "%" + nama + "%"),
                new NpgsqlParameter("@kategori", kategori),
                new NpgsqlParameter("@hargaMin", hargaMin),
                new NpgsqlParameter("@hargaMax", hargaMax)
            };
            return _db.ExecuteQuery(sql, params_);
        }

        public bool TambahObat(BaseObat obat)
        {
            if (obat == null)
                throw new ArgumentNullException("obat", "Obat tidak boleh null.");

            // Validasi bisnis (hargaJual >= hargaBeli, dll.)
            obat.Validate();

            try
            {
                // FIX: ganti pattern matching 'is T varname' ke 'as T' agar
                // kompatibel dengan semua versi .NET Framework
                var obatResep = obat as ObatResep;
                object golongan = (obatResep != null)
                    ? (object)obatResep.GolonganObat
                    : DBNull.Value;

                string sql = @"INSERT INTO obat 
                    (id_kategori, nama_obat, jenis, golongan, satuan,
                     harga_beli, harga_jual, stok, stok_minimum, tanggal_exp, deskripsi)
                    VALUES 
                    (@idKat, @nama, @jenis, @golongan, @satuan,
                     @hargaBeli, @hargaJual, @stok, @stokMin, @exp, @desk)";
                var params_ = new NpgsqlParameter[] {
                    new NpgsqlParameter("@idKat",     obat.IdKategori),
                    new NpgsqlParameter("@nama",      obat.NamaObat),
                    new NpgsqlParameter("@jenis",     obat.Jenis),
                    new NpgsqlParameter("@golongan",  golongan),
                    new NpgsqlParameter("@satuan",    obat.Satuan),
                    new NpgsqlParameter("@hargaBeli", obat.HargaBeli),
                    new NpgsqlParameter("@hargaJual", obat.HargaJual),
                    new NpgsqlParameter("@stok",      obat.Stok),
                    new NpgsqlParameter("@stokMin",   obat.StokMinimum),
                    new NpgsqlParameter("@exp",       obat.TanggalExp),
                    new NpgsqlParameter("@desk",
                        (object)obat.Deskripsi ?? DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal tambah obat: " + ex.Message);
            }
        }

        public bool EditObat(BaseObat obat)
        {
            if (obat == null)
                throw new ArgumentNullException("obat", "Obat tidak boleh null.");

            obat.Validate();

            try
            {
                var obatResep = obat as ObatResep;
                object golongan = (obatResep != null)
                    ? (object)obatResep.GolonganObat
                    : DBNull.Value;

                string sql = @"UPDATE obat SET
                    id_kategori  = @idKat,
                    nama_obat    = @nama,
                    jenis        = @jenis,
                    golongan     = @golongan,
                    satuan       = @satuan,
                    harga_beli   = @hargaBeli,
                    harga_jual   = @hargaJual,
                    stok_minimum = @stokMin,
                    tanggal_exp  = @exp,
                    deskripsi    = @desk
                    WHERE id_obat = @id";
                var params_ = new NpgsqlParameter[] {
                    new NpgsqlParameter("@id",        obat.IdObat),
                    new NpgsqlParameter("@idKat",     obat.IdKategori),
                    new NpgsqlParameter("@nama",      obat.NamaObat),
                    new NpgsqlParameter("@jenis",     obat.Jenis),
                    new NpgsqlParameter("@golongan",  golongan),
                    new NpgsqlParameter("@satuan",    obat.Satuan),
                    new NpgsqlParameter("@hargaBeli", obat.HargaBeli),
                    new NpgsqlParameter("@hargaJual", obat.HargaJual),
                    new NpgsqlParameter("@stokMin",   obat.StokMinimum),
                    new NpgsqlParameter("@exp",       obat.TanggalExp),
                    new NpgsqlParameter("@desk",
                        (object)obat.Deskripsi ?? DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal edit obat: " + ex.Message);
            }
        }

        public bool HapusObat(int idObat)
        {
            if (idObat <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");
            try
            {
                string sql = "UPDATE obat SET is_active = false WHERE id_obat = @id";
                return _db.ExecuteNonQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@id", idObat)
                }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal hapus obat: " + ex.Message);
            }
        }

        public static BaseObat MapRowToObat(DataRow row)
        {
            string jenis = row["jenis"].ToString();
            BaseObat obat;

            if (jenis == "resep")
            {
                var obatResep = new ObatResep();
                string gol = row["golongan"] != DBNull.Value
                    ? row["golongan"].ToString()
                    : null;
                if (!string.IsNullOrWhiteSpace(gol))
                    obatResep.GolonganObat = gol;
                obat = obatResep;
            }
            else
            {
                obat = new ObatBebas();
            }

            obat.IdObat = Convert.ToInt32(row["id_obat"]);
            obat.IdKategori = Convert.ToInt32(row["id_kategori"]);
            obat.NamaObat = row["nama_obat"].ToString();
            obat.Jenis = jenis;
            obat.Satuan = row["satuan"].ToString();
            obat.HargaBeli = Convert.ToDecimal(row["harga_beli"]);
            obat.HargaJual = Convert.ToDecimal(row["harga_jual"]);
            obat.Stok = Convert.ToInt32(row["stok"]);
            obat.StokMinimum = Convert.ToInt32(row["stok_minimum"]);
            // FIX: TanggalExp di-set tanpa validasi masa lalu — obat kadaluarsa tetap bisa di-load
            obat.TanggalExp = Convert.ToDateTime(row["tanggal_exp"]);
            obat.IsActive = Convert.ToBoolean(row["is_active"]);
            obat.Deskripsi = row["deskripsi"] != DBNull.Value
                ? row["deskripsi"].ToString()
                : null;
            return obat;
        }

        public DataTable GetAllKategori()
        {
            return _db.ExecuteQuery("SELECT * FROM kategori ORDER BY nama_kategori");
        }
    }
}