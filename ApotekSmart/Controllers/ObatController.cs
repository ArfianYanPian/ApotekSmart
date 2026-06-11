using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class ObatController
    {
        // [ASSOCIATION] ObatController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa ObatController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [ENCAPSULATION] Detail query VIEW v_stok_obat tersembunyi
        // Pemanggil cukup dapat DataTable semua obat
        public DataTable GetAllObat()
        {
            return _db.ExecuteQuery("SELECT * FROM v_stok_obat ORDER BY nama_obat");
        }

        // [ENCAPSULATION] Detail query dengan parameter tersembunyi
        public DataTable GetObatById(int idObat)
        {
            if (idObat <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");

            string sql = "SELECT * FROM v_stok_obat WHERE id_obat = @id";
            return _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                new NpgsqlParameter("@id", idObat)
            });
        }

        // [ENCAPSULATION] SearchObat() menyembunyikan kompleksitas query
        // dengan 4 filter sekaligus di balik 1 method yang mudah dipanggil
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

        // [POLYMORPHISM] obat bertipe BaseObat — bisa ObatBebas atau ObatResep
        // [ENCAPSULATION] obat.Validate() — validasi bisnis dipanggil dari dalam model
        // bukan dari form/UI — sesuai prinsip business rule validation
        public bool TambahObat(BaseObat obat)
        {
            if (obat == null)
                throw new ArgumentNullException("obat", "Obat tidak boleh null.");

            // [ENCAPSULATION] Validate() di BaseObat/ObatResep menjaga aturan bisnis
            obat.Validate();

            try
            {
                // [POLYMORPHISM] Cek tipe asli obat — ObatResep atau ObatBebas
                // ObatResep punya GolonganObat, ObatBebas tidak
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

        // [POLYMORPHISM] obat bertipe BaseObat — bisa ObatBebas atau ObatResep
        // [ENCAPSULATION] Detail SQL UPDATE tersembunyi, pemanggil cukup kirim object
        public bool EditObat(BaseObat obat)
        {
            if (obat == null)
                throw new ArgumentNullException("obat", "Obat tidak boleh null.");

            // [ENCAPSULATION] Validate() memastikan aturan bisnis tetap terjaga saat edit
            obat.Validate();

            try
            {
                // [POLYMORPHISM] Cek tipe asli obat untuk ambil GolonganObat
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

        // [ENCAPSULATION] Soft delete — is_active = false
        // Detail implementasi tersembunyi, pemanggil tidak tahu cara teknisnya
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

        // [POLYMORPHISM] MapRowToObat() mengembalikan BaseObat
        // tapi tipe aslinya bisa ObatBebas atau ObatResep tergantung kolom jenis
        // [ENCAPSULATION] Detail mapping DataRow ke object tersembunyi
        // Pemanggil cukup dapat BaseObat tanpa tahu cara mapping-nya
        public static BaseObat MapRowToObat(DataRow row)
        {
            string jenis = row["jenis"].ToString();
            BaseObat obat;

            // [POLYMORPHISM] Instansiasi tipe yang tepat berdasarkan jenis
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

            // Set property BaseObat — berlaku untuk ObatBebas maupun ObatResep
            obat.IdObat = Convert.ToInt32(row["id_obat"]);
            obat.IdKategori = Convert.ToInt32(row["id_kategori"]);
            obat.NamaObat = row["nama_obat"].ToString();
            obat.Jenis = jenis;
            obat.Satuan = row["satuan"].ToString();
            obat.HargaBeli = Convert.ToDecimal(row["harga_beli"]);
            obat.HargaJual = Convert.ToDecimal(row["harga_jual"]);
            obat.Stok = Convert.ToInt32(row["stok"]);
            obat.StokMinimum = Convert.ToInt32(row["stok_minimum"]);
            obat.TanggalExp = row["tanggal_exp"] is DateOnly d
                ? d.ToDateTime(TimeOnly.MinValue)
                : Convert.ToDateTime(row["tanggal_exp"]);
            obat.IsActive = Convert.ToBoolean(row["is_active"]);
            obat.Deskripsi = row["deskripsi"] != DBNull.Value
                ? row["deskripsi"].ToString()
                : null;
            return obat;
        }

        // [ENCAPSULATION] Detail query kategori tersembunyi
        public DataTable GetAllKategori()
        {
            return _db.ExecuteQuery("SELECT * FROM kategori ORDER BY nama_kategori");
        }
    }
}