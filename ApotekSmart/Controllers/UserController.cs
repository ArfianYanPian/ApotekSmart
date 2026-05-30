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
        public bool Create(BaseUser entity)
        {
            string sql = "INSERT INTO users (nama, username, password, role) VALUES (@nama, @user, @pass, @role)";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@nama", entity.Nama),
                new NpgsqlParameter("@user", entity.Username),
                
                // Method GetPassword() menarik data
                new NpgsqlParameter("@pass", entity.GetPassword()),

                new NpgsqlParameter("@role", entity.Role)
            };
            return DatabaseHelper.Instance.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<BaseUser> ReadAll()
        {
            List<BaseUser> users = new List<BaseUser>();
            DataTable dt = DatabaseHelper.Instance.ExecuteQuery("SELECT * FROM users");
            foreach (DataRow row in dt.Rows)
            {
                if (row["role"].ToString().ToLower() == "kasir")
                    users.Add(new Kasir { IdUser = Convert.ToInt32(row["id"]), Nama = row["nama"].ToString(), Role = "Kasir" });
                else
                    users.Add(new Apoteker { IdUser = Convert.ToInt32(row["id"]), Nama = row["nama"].ToString(), Role = "Apoteker" });
            }
            return users;
        }

        public BaseUser ReadById(int id) { /* Implementasi Mirip Obat */ return null; }

        public bool Update(BaseUser entity) { /* Implementasi Update Mirip Obat */ return false; }

        public bool Delete(int id)
        {
            string sql = "UPDATE users SET is_active = false WHERE id = @id"; // Soft delete
            return DatabaseHelper.Instance.ExecuteNonQuery(sql, new NpgsqlParameter[] { new NpgsqlParameter("@id", id) }) > 0;
        }
    }
}