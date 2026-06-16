using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;
using ApotekSmart.Interfaces;

namespace ApotekSmart.Controllers
{
    // [INTERFACE] ObatController mengimplementasikan ICRUDService<BaseObat>
    // Wajib mengimplementasikan semua 5 method CRUD
    public class ObatController : ICRUDService<BaseObat>
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [INTERFACE] Implementasi Create() dari ICRUDService<BaseObat>
        // [POLYMORPHISM] entity bertipe BaseObat — bisa ObatBebas atau ObatResep
        public bool Create(BaseObat entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Obat tidak boleh null.");

            entity.Validate();

            try
            {
                var obatResep = entity as ObatResep;
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
                    new NpgsqlParameter("@idKat",     entity.IdKategori),
                    new NpgsqlParameter("@nama",      entity.NamaObat),
                    new NpgsqlParameter("@jenis",     entity.Jenis),
                    new NpgsqlParameter("@golongan",  golongan),
                    new NpgsqlParameter("@satuan",    entity.Satuan),
                    new NpgsqlParameter("@hargaBeli", entity.HargaBeli),
                    new NpgsqlParameter("@hargaJual", entity.HargaJual),
                    new NpgsqlParameter("@stok",      entity.Stok),
                    new NpgsqlParameter("@stokMin",   entity.StokMinimum),
                    new NpgsqlParameter("@exp",       entity.TanggalExp),
                    new NpgsqlParameter("@desk",
                        (object)entity.Deskripsi ?? DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal tambah obat: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi ReadAll() dari ICRUDService<BaseObat>
        // [POLYMORPHISM] Mengembalikan List<BaseObat> yang bisa berisi ObatBebas dan ObatResep
        public List<BaseObat> ReadAll()
        {
            var obatList = new List<BaseObat>();
            DataTable dt = _db.ExecuteQuery("SELECT * FROM v_stok_obat ORDER BY nama_obat");

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    // [POLYMORPHISM] MapRowToObat() kembalikan ObatBebas atau ObatResep
                    obatList.Add(MapRowToObat(row));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WARN] Skip obat id {row["id_obat"]}: {ex.Message}");
                }
            }
            return obatList;
        }

        // [INTERFACE] Implementasi ReadById() dari ICRUDService<BaseObat>
        // [POLYMORPHISM] Mengembalikan BaseObat — bisa ObatBebas atau ObatResep
        public BaseObat ReadById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");

            string sql = "SELECT * FROM v_stok_obat WHERE id_obat = @id";
            DataTable dt = _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                new NpgsqlParameter("@id", id)
            });

            if (dt.Rows.Count == 0) return null;

            try
            {
                return MapRowToObat(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception($"Data obat id {id} tidak valid: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi Update() dari ICRUDService<BaseObat>
        // [POLYMORPHISM] entity bertipe BaseObat — bisa ObatBebas atau ObatResep
        public bool Update(BaseObat entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Obat tidak boleh null.");

            entity.Validate();

            try
            {
                var obatResep = entity as ObatResep;
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
                    new NpgsqlParameter("@id",        entity.IdObat),
                    new NpgsqlParameter("@idKat",     entity.IdKategori),
                    new NpgsqlParameter("@nama",      entity.NamaObat),
                    new NpgsqlParameter("@jenis",     entity.Jenis),
                    new NpgsqlParameter("@golongan",  golongan),
                    new NpgsqlParameter("@satuan",    entity.Satuan),
                    new NpgsqlParameter("@hargaBeli", entity.HargaBeli),
                    new NpgsqlParameter("@hargaJual", entity.HargaJual),
                    new NpgsqlParameter("@stokMin",   entity.StokMinimum),
                    new NpgsqlParameter("@exp",       entity.TanggalExp),
                    new NpgsqlParameter("@desk",
                        (object)entity.Deskripsi ?? DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal edit obat: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi Delete() dari ICRUDService<BaseObat>
        // [ENCAPSULATION] Soft delete — is_active = false
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");
            try
            {
                string sql = "UPDATE obat SET is_active = false WHERE id_obat = @id";
                return _db.ExecuteNonQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@id", id)
                }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal hapus obat: " + ex.Message);
            }
        }

        // ── Method tambahan di luar ICRUDService ─────────────

        // [ENCAPSULATION] SearchObat() menyembunyikan kompleksitas query
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

        // [ENCAPSULATION] GetAllObat() wrapper ReadAll() yang kembalikan DataTable
        // Dipakai oleh panel yang butuh DataTable bukan List<BaseObat>
        public DataTable GetAllObat()
        {
            return _db.ExecuteQuery("SELECT * FROM v_stok_obat ORDER BY nama_obat");
        }

        // [ENCAPSULATION] GetObatById() wrapper ReadById() yang kembalikan DataTable
        // Dipakai oleh panel yang butuh DataTable bukan BaseObat
        public DataTable GetObatById(int idObat)
        {
            if (idObat <= 0)
                throw new ArgumentException("IdObat harus lebih dari 0.");

            string sql = "SELECT * FROM v_stok_obat WHERE id_obat = @id";
            return _db.ExecuteQuery(sql, new NpgsqlParameter[] {
                new NpgsqlParameter("@id", idObat)
            });
        }

        // [ENCAPSULATION] Detail kategori tersembunyi
        public DataTable GetAllKategori()
        {
            return _db.ExecuteQuery("SELECT * FROM kategori ORDER BY nama_kategori");
        }

        // [POLYMORPHISM] MapRowToObat() mengembalikan BaseObat
        // tipe aslinya ObatBebas atau ObatResep ditentukan saat runtime
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
            obat.TanggalExp = row["tanggal_exp"] is DateOnly d
                ? d.ToDateTime(TimeOnly.MinValue)
                : Convert.ToDateTime(row["tanggal_exp"]);
            obat.IsActive = Convert.ToBoolean(row["is_active"]);
            obat.Deskripsi = row["deskripsi"] != DBNull.Value
                ? row["deskripsi"].ToString()
                : null;
            return obat;
        }

        // ── Alias untuk backward compatibility dengan panel lama ─
        public bool TambahObat(BaseObat obat) => Create(obat);
        public bool EditObat(BaseObat obat) => Update(obat);
        public bool HapusObat(int idObat) => Delete(idObat);
    }
}