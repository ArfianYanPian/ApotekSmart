using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class ObatController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // GET ALL — ambil semua obat dari view v_stok_obat
        public DataTable GetAllObat()
        {
            string sql = "SELECT * FROM v_stok_obat ORDER BY nama_obat";
            return _db.ExecuteQuery(sql);
        }

        // GET BY ID
        public DataTable GetObatById(int idObat)
        {
            string sql = "SELECT * FROM v_stok_obat WHERE id_obat = @id";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id", idObat)
            };
            return _db.ExecuteQuery(sql, params_);
        }

        // SEARCH — cari berdasarkan nama, kategori, rentang harga
        public DataTable SearchObat(string nama = "", string kategori = "", decimal hargaMin = 0, decimal hargaMax = 999999999)
        {
            string sql = @"SELECT * FROM v_stok_obat 
                          WHERE LOWER(nama_obat) LIKE LOWER(@nama)
                          AND (@kategori = '' OR LOWER(nama_kategori) = LOWER(@kategori))
                          AND harga_jual BETWEEN @hargaMin AND @hargaMax
                          ORDER BY nama_obat";
            var params_ = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@nama", "%" + nama + "%"),
                new NpgsqlParameter("@kategori", kategori),
                new NpgsqlParameter("@hargaMin", hargaMin),
                new NpgsqlParameter("@hargaMax", hargaMax)
            };
            return _db.ExecuteQuery(sql, params_);
        }

        // TAMBAH obat baru
        public bool TambahObat(BaseObat obat)
        {
            try
            {
                string sql = @"INSERT INTO obat 
                    (id_kategori, nama_obat, jenis, golongan, satuan, harga_beli, harga_jual, stok, stok_minimum, tanggal_exp, deskripsi)
                    VALUES (@idKategori, @nama, @jenis, @golongan, @satuan, @hargaBeli, @hargaJual, @stok, @stokMin, @exp, @deskripsi)";
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@idKategori", obat.IdKategori),
                    new NpgsqlParameter("@nama", obat.NamaObat),
                    new NpgsqlParameter("@jenis", obat.Jenis),
                    new NpgsqlParameter("@golongan", obat is ObatResep ? ((ObatResep)obat).GolonganObat ?? (object)DBNull.Value : (object)DBNull.Value),
                    new NpgsqlParameter("@satuan", obat.Satuan),
                    new NpgsqlParameter("@hargaBeli", obat.HargaBeli),
                    new NpgsqlParameter("@hargaJual", obat.HargaJual),
                    new NpgsqlParameter("@stok", obat.Stok),
                    new NpgsqlParameter("@stokMin", obat.StokMinimum),
                    new NpgsqlParameter("@exp", obat.TanggalExp),
                    new NpgsqlParameter("@deskripsi", obat.Deskripsi ?? (object)DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal tambah obat: " + ex.Message);
            }
        }

        // EDIT obat
        public bool EditObat(BaseObat obat)
        {
            try
            {
                string sql = @"UPDATE obat SET
                    id_kategori = @idKategori,
                    nama_obat = @nama,
                    jenis = @jenis,
                    golongan = @golongan,
                    satuan = @satuan,
                    harga_beli = @hargaBeli,
                    harga_jual = @hargaJual,
                    stok_minimum = @stokMin,
                    tanggal_exp = @exp,
                    deskripsi = @deskripsi
                    WHERE id_obat = @id";
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id", obat.IdObat),
                    new NpgsqlParameter("@idKategori", obat.IdKategori),
                    new NpgsqlParameter("@nama", obat.NamaObat),
                    new NpgsqlParameter("@jenis", obat.Jenis),
                    new NpgsqlParameter("@golongan", obat is ObatResep ? ((ObatResep)obat).GolonganObat ?? (object)DBNull.Value : (object)DBNull.Value),
                    new NpgsqlParameter("@satuan", obat.Satuan),
                    new NpgsqlParameter("@hargaBeli", obat.HargaBeli),
                    new NpgsqlParameter("@hargaJual", obat.HargaJual),
                    new NpgsqlParameter("@stokMin", obat.StokMinimum),
                    new NpgsqlParameter("@exp", obat.TanggalExp),
                    new NpgsqlParameter("@deskripsi", obat.Deskripsi ?? (object)DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal edit obat: " + ex.Message);
            }
        }

        // HAPUS obat (soft delete — set is_active = false)
        public bool HapusObat(int idObat)
        {
            try
            {
                string sql = "UPDATE obat SET is_active = false WHERE id_obat = @id";
                var params_ = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("@id", idObat)
                };
                return _db.ExecuteNonQuery(sql, params_) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal hapus obat: " + ex.Message);
            }
        }

        // GET ALL KATEGORI — untuk ComboBox di form
        public DataTable GetAllKategori()
        {
            return _db.ExecuteQuery("SELECT * FROM kategori ORDER BY nama_kategori");
        }
    }
}