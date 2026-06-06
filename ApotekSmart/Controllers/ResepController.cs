using System;
using System.Data;
using Npgsql;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class ResepController
    {
        private DatabaseHelper _db = DatabaseHelper.Instance;

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

        public bool ValidasiResep(int idResep, string status,
                                   int apotekerId, string catatan)
        {
            if (idResep <= 0)
                throw new ArgumentException("IdResep harus lebih dari 0.");
            if (apotekerId <= 0)
                throw new ArgumentException("ApotekerId harus lebih dari 0.");
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