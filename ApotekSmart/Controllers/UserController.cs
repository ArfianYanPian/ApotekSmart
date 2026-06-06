using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;
using ApotekSmart.Interfaces;

namespace ApotekSmart.Controllers
{
    public class UserController : ICRUDService<BaseUser>
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public bool Create(BaseUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity tidak boleh null.");
            try
            {
                string sql = @"INSERT INTO users 
                    (nama, username, password, role, nomor_identitas, nomor_shift)
                    VALUES (@nama, @user, @pass, @role, @identitas, @shift)";

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

        public List<BaseUser> ReadAll()
        {
            var users = new List<BaseUser>();
            DataTable dt = _db.ExecuteQuery("SELECT * FROM users ORDER BY nama");

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    users.Add(AuthController.MapRowToUser(row));
                }
                catch (Exception ex)
                {
                    // Skip baris yang datanya tidak valid, jangan crash semua list
                    Console.WriteLine($"[WARN] Skip user id {row["id_user"]}: {ex.Message}");
                }
            }
            return users;
        }

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
                return AuthController.MapRowToUser(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception($"Data user id {id} tidak valid: " + ex.Message);
            }
        }

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