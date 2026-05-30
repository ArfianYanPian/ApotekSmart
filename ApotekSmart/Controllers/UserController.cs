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
                    users.Add(new Kasir { IdUser = Convert.ToInt32(row["id_user"]), Nama = row["nama"].ToString(), Role = "Kasir" });
                else
                    users.Add(new Apoteker { IdUser = Convert.ToInt32(row["id_user"]), Nama = row["nama"].ToString(), Role = "Apoteker" });
            }
            return users;
        }

        public BaseUser ReadById(int id)
        {
            string sql = "SELECT * FROM users WHERE id_user = @id";
            NpgsqlParameter[] parameters = { new NpgsqlParameter("@id", id) };
            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string role = row["role"].ToString().ToLower();

                BaseUser user;
                if (role == "kasir")
                {
                    user = new Kasir();
                }
                else
                {
                    user = new Apoteker();
                }

                user.IdUser = Convert.ToInt32(row["id_user"]);
                user.Nama = row["nama"].ToString();
                user.Username = row["username"].ToString();
                user.Role = row["role"].ToString();
                user.IsActive = Convert.ToBoolean(row["is_active"]);

                user.SetPassword(row["password"].ToString());

                return user;
            }
            return null; // Mengembalikan null jika user dengan ID tersebut tidak ditemukan
        }

        public bool Update(BaseUser entity)
        {
            string sql = "UPDATE users SET nama=@nama, username=@user, password=@pass, role=@role, is_active=@isactive WHERE id_user=@id";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@nama", entity.Nama),
                new NpgsqlParameter("@user", entity.Username),
                
                // Memanggil GetPassword() untuk mengambil nilai _Password yang ter-enkapsulasi
                new NpgsqlParameter("@pass", entity.GetPassword()),

                new NpgsqlParameter("@role", entity.Role),
                new NpgsqlParameter("@isactive", entity.IsActive),
                new NpgsqlParameter("@id", entity.IdUser)
            };

            return DatabaseHelper.Instance.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool Delete(int id)
        {
            string sql = "UPDATE users SET is_active = false WHERE id_user = @id"; // Soft delete
            return DatabaseHelper.Instance.ExecuteNonQuery(sql, new NpgsqlParameter[] { new NpgsqlParameter("@id", id) }) > 0;
        }
    }
}