using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    // [CLASS LIBRARY] Bagian dari Controllers library dalam namespace ApotekSmart.Controllers
    public class ResepController
    {
        // [ASSOCIATION] ResepController menggunakan DatabaseHelper
        // DatabaseHelper bisa hidup tanpa ResepController
        // [ENCAPSULATION] _db private — akses database tersembunyi dari luar
        private DatabaseHelper _db = DatabaseHelper.Instance;

        // [ENCAPSULATION] Detail query resep menunggu tersembunyi dari pemanggil
        // Pemanggil cukup dapat DataTable resep yang statusnya 'menunggu'
        public DataTable GetResepMenunggu()
        {
            try
            {
                return _db.ExecuteQuery(
                    "SELECT * FROM resep WHERE status_validasi = 'menunggu' ORDER BY created_at ASC");
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal ambil daftar resep: " + ex.Message);
            }
        }

        // [ENCAPSULATION] Detail pemanggilan stored procedure sp_validasi_resep tersembunyi
        // Validasi input dilakukan di dalam method sebelum data dikirim ke DB
        // Pemanggil tidak perlu tahu cara kerja SP di PostgreSQL
        // [ASSOCIATION] ResepController berinteraksi dengan data Apoteker lewat apotekerId
        // Tidak menyimpan objek Apoteker langsung — hanya ID-nya
        public bool ValidasiResep(int idResep, string status,
                                   int apotekerId, string catatan)
        {
            if (idResep <= 0)
                throw new ArgumentException("IdResep harus lebih dari 0.");
            if (apotekerId <= 0)
                throw new ArgumentException("ApotekerId harus lebih dari 0.");

            // [ENCAPSULATION] Validasi nilai status dilakukan di dalam method
            // Hanya 2 nilai yang diizinkan: disetujui atau ditolak
            if (status != "disetujui" && status != "ditolak")
                throw new ArgumentException("Status validasi harus 'disetujui' atau 'ditolak'.");
            try
            {
                NpgsqlParameter[] parameters = {
                    new NpgsqlParameter("p_resep_id",    idResep),
                    new NpgsqlParameter("p_apoteker_id", apotekerId),
                    new NpgsqlParameter("p_status",      status),
                    new NpgsqlParameter("p_catatan",     catatan ?? "")
                };
                _db.ExecuteProcedure("sp_validasi_resep", parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal validasi resep: " + ex.Message);
            }
        }
    }
}