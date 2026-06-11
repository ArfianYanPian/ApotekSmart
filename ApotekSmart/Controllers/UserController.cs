using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;
using ApotekSmart.Interfaces;

namespace ApotekSmart.Controllers
{
    // [INTERFACE] UserController mengimplementasikan ICRUDService<BaseUser>
    // Wajib mengimplementasikan semua 5 method CRUD
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class UserController : ICRUDService<BaseUser>
    {
        // [ASSOCIATION] UserController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa UserController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [INTERFACE] Implementasi Create() dari ICRUDService<BaseUser>
        // [POLYMORPHISM] entity bertipe BaseUser — bisa Apoteker atau Kasir
        // [ENCAPSULATION] Detail SQL query tersembunyi di dalam method
        public bool Create(BaseUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity tidak boleh null.");
            try
            {
                string sql = @"INSERT INTO users 
                    (nama, username, password, role, nomor_identitas, nomor_shift)
                    VALUES (@nama, @user, @pass, @role, @identitas, @shift)";

                // [POLYMORPHISM] Cek tipe asli entity — Apoteker atau Kasir
                // is pattern matching untuk membedakan subclass
                string identitas = null, shift = null;
                if (entity is Apoteker a) identitas = a.NomorIdentitas;
                if (entity is Kasir k) shift = k.NomorShift;

                NpgsqlParameter[] parameters = {
                    new NpgsqlParameter("@nama",      entity.Nama),
                    new NpgsqlParameter("@user",      entity.Username),
                    new NpgsqlParameter("@pass",      entity.GetPassword()),
                    new NpgsqlParameter("@role",      entity.Role),
                    new NpgsqlParameter("@identitas", (object)identitas ?? DBNull.Value),
                    new NpgsqlParameter("@shift",     (object)shift     ?? DBNull.Value)
                };
                return _db.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal tambah user: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi ReadAll() dari ICRUDService<BaseUser>
        // [POLYMORPHISM] Mengembalikan List<BaseUser> yang bisa berisi Apoteker dan Kasir
        // [ENCAPSULATION] Detail query dan mapping tersembunyi dari pemanggil
        public List<BaseUser> ReadAll()
        {
            var users = new List<BaseUser>();
            DataTable dt = _db.ExecuteQuery("SELECT * FROM users ORDER BY nama");

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    // [POLYMORPHISM] MapRowToUser() mengembalikan Apoteker atau Kasir
                    // tergantung kolom role di database
                    users.Add(AuthController.MapRowToUser(row));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WARN] Skip user id {row["id_user"]}: {ex.Message}");
                }
            }
            return users;
        }

        // [INTERFACE] Implementasi ReadById() dari ICRUDService<BaseUser>
        // [POLYMORPHISM] Mengembalikan BaseUser — bisa Apoteker atau Kasir
        public BaseUser ReadById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id harus lebih dari 0.");

            string sql = "SELECT * FROM users WHERE id_user = @id";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@id", id)
            };
            DataTable dt = _db.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0) return null;

            try
            {
                // [POLYMORPHISM] MapRowToUser() otomatis kembalikan tipe yang tepat
                return AuthController.MapRowToUser(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception($"Data user id {id} tidak valid: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi Update() dari ICRUDService<BaseUser>
        // [POLYMORPHISM] entity bertipe BaseUser — bisa Apoteker atau Kasir
        // [ENCAPSULATION] Detail SQL tersembunyi, pemanggil cukup kirim entity
        public bool Update(BaseUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity tidak boleh null.");
            try
            {
                string sql = @"UPDATE users SET 
                    nama            = @nama,
                    username        = @user,
                    password        = @pass,
                    role            = @role,
                    is_active       = @isactive,
                    nomor_identitas = @identitas,
                    nomor_shift     = @shift
                    WHERE id_user   = @id";

                // [POLYMORPHISM] Cek tipe asli entity untuk ambil field spesifik
                string identitas = null, shift = null;
                if (entity is Apoteker a) identitas = a.NomorIdentitas;
                if (entity is Kasir k) shift = k.NomorShift;

                NpgsqlParameter[] parameters = {
                    new NpgsqlParameter("@nama",      entity.Nama),
                    new NpgsqlParameter("@user",      entity.Username),
                    new NpgsqlParameter("@pass",      entity.GetPassword()),
                    new NpgsqlParameter("@role",      entity.Role),
                    new NpgsqlParameter("@isactive",  entity.IsActive),
                    new NpgsqlParameter("@identitas", (object)identitas ?? DBNull.Value),
                    new NpgsqlParameter("@shift",     (object)shift     ?? DBNull.Value),
                    new NpgsqlParameter("@id",        entity.IdUser)
                };
                return _db.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal update user: " + ex.Message);
            }
        }

        // [INTERFACE] Implementasi Delete() dari ICRUDService<BaseUser>
        // [ENCAPSULATION] Soft delete — is_active = false, data tidak benar-benar dihapus
        // Detail implementasi tersembunyi dari pemanggil
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id harus lebih dari 0.");
            try
            {
                string sql = "UPDATE users SET is_active = false WHERE id_user = @id";
                return _db.ExecuteNonQuery(sql, new NpgsqlParameter[] {
                    new NpgsqlParameter("@id", id)
                }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal hapus user: " + ex.Message);
            }
        }
    }
}