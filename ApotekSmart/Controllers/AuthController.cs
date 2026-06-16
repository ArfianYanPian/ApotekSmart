using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class AuthController
    {
        // [ENCAPSULATION] Detail query login tersembunyi dari pemanggil
        // Pemanggil cukup kirim username & password, dapat objek BaseUser
        // [POLYMORPHISM] Login() mengembalikan BaseUser
        // tapi tipe aslinya bisa Apoteker atau Kasir tergantung data di DB
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

            // [ASSOCIATION] AuthController menggunakan DatabaseHelper.Instance
            // Singleton — satu instance untuk seluruh aplikasi
            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0) return null;

            try
            {
                // [POLYMORPHISM] MapRowToUser() kembalikan Apoteker atau Kasir
                // tergantung kolom role di database
                return MapRowToUser(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Data user tidak valid: " + ex.Message);
            }
        }

        // [POLYMORPHISM] MapRowToUser() mengembalikan BaseUser
        // tipe aslinya Apoteker atau Kasir ditentukan saat runtime
        // [ENCAPSULATION] Detail mapping DataRow ke object tersembunyi
        // Dipakai ulang oleh UserController — static agar bisa dipanggil tanpa instansiasi
        public static BaseUser MapRowToUser(DataRow row)
        {
            string role = row["role"].ToString().ToLower();

            if (role == "apoteker")
            {
                // [POLYMORPHISM] Instansiasi Apoteker — subclass dari BaseUser
                // [INHERITANCE] Apoteker mewarisi semua property BaseUser
                var apoteker = new Apoteker();
                apoteker.IdUser = Convert.ToInt32(row["id_user"]);
                apoteker.Nama = row["nama"].ToString();
                apoteker.Username = row["username"].ToString();
                apoteker.Role = role;
                apoteker.IsActive = Convert.ToBoolean(row["is_active"]);

                // [ENCAPSULATION] SetPassword() — password tidak bisa diset langsung
                // harus lewat method khusus di BaseUser
                apoteker.SetPassword(row["password"].ToString());

                // NomorIdentitas boleh null — apoteker lama mungkin belum isi
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
                // [POLYMORPHISM] Instansiasi Kasir — subclass dari BaseUser
                // [INHERITANCE] Kasir mewarisi semua property BaseUser
                var kasir = new Kasir();
                kasir.IdUser = Convert.ToInt32(row["id_user"]);
                kasir.Nama = row["nama"].ToString();
                kasir.Username = row["username"].ToString();
                kasir.Role = role;
                kasir.IsActive = Convert.ToBoolean(row["is_active"]);

                // [ENCAPSULATION] SetPassword() — akses password terkontrol
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