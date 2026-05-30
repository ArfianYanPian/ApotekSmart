using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class AuthController
    {
        public BaseUser Login(string username, string password)
        {
            string sql = "SELECT * FROM users WHERE username = @user AND password = @pass AND is_active = true";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@user", username),
                new NpgsqlParameter("@pass", password)
            };

            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string role = row["role"].ToString().ToLower();

                if (role == "apoteker")
                {
                    return new Apoteker
                    {
                        IdUser = Convert.ToInt32(row["id_user"]),
                        Nama = row["nama"].ToString(),
                        Username = row["username"].ToString(),
                        Role = role,
                        NomorIdentitas = row["nomor_identitas"]?.ToString(),
                        IsActive = true
                    };
                }
                else if (role == "kasir")
                {
                    return new Kasir
                    {
                        IdUser = Convert.ToInt32(row["id_user"]),
                        Nama = row["nama"].ToString(),
                        Username = row["username"].ToString(),
                        Role = role,
                        NomorShift = row["nomor_shift"]?.ToString(),
                        IsActive = true
                    };
                }
            }
            return null; // Login gagal
        }
    }
}