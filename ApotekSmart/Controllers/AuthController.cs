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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username dan password tidak boleh kosong.");

            string sql = @"SELECT * FROM users 
                          WHERE username = @user 
                            AND password = @pass 
                            AND is_active = true";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@user", username),
                new NpgsqlParameter("@pass", password)
            };

            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0) return null;

            try
            {
                return MapRowToUser(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Data user tidak valid: " + ex.Message);
            }
        }

        // Helper reusable — dipakai juga oleh UserController
        public static BaseUser MapRowToUser(DataRow row)
        {
            string role = row["role"].ToString().ToLower();

            if (role == "apoteker")
            {
                var apoteker = new Apoteker();
                apoteker.IdUser = Convert.ToInt32(row["id_user"]);
                apoteker.Nama = row["nama"].ToString();
                apoteker.Username = row["username"].ToString();
                apoteker.Role = role;
                apoteker.IsActive = Convert.ToBoolean(row["is_active"]);
                apoteker.SetPassword(row["password"].ToString());

                // NomorIdentitas boleh null (apoteker lama mungkin belum isi)
                string nomor = row["nomor_identitas"] != DBNull.Value
                    ? row["nomor_identitas"].ToString()
                    : null;
                if (!string.IsNullOrWhiteSpace(nomor))
                    apoteker.NomorIdentitas = nomor;

                apoteker.CreatedAt = Convert.ToDateTime(row["created_at"]);
                return apoteker;
            }
            else if (role == "kasir")
            {
                var kasir = new Kasir();
                kasir.IdUser = Convert.ToInt32(row["id_user"]);
                kasir.Nama = row["nama"].ToString();
                kasir.Username = row["username"].ToString();
                kasir.Role = role;
                kasir.IsActive = Convert.ToBoolean(row["is_active"]);
                kasir.SetPassword(row["password"].ToString());

                // NomorShift boleh null
                string shift = row["nomor_shift"] != DBNull.Value
                    ? row["nomor_shift"].ToString()
                    : null;
                if (!string.IsNullOrWhiteSpace(shift))
                    kasir.NomorShift = shift;

                kasir.CreatedAt = Convert.ToDateTime(row["created_at"]);
                return kasir;
            }

            throw new Exception($"Role '{role}' tidak dikenali.");
        }
    }
}