using System;
using System.Data;
using Npgsql;
using ApotekSmart.Models;
using ApotekSmart.Helpers;

namespace ApotekSmart.Controllers
{
    public class ResepController
    {
        public DataTable GetResepMenunggu()
        {
            return DatabaseHelper.Instance.ExecuteQuery("SELECT * FROM resep WHERE status_validasi = 'menunggu'");
        }

        public bool ValidasiResep(int idResep, string status, int apotekerId, string catatan)
        {
            try
            {
                NpgsqlParameter[] parameters = {
                new NpgsqlParameter("p_resep_id", idResep),
                new NpgsqlParameter("p_apoteker_id", apotekerId),
                new NpgsqlParameter("p_status", status),
                new NpgsqlParameter("p_catatan", catatan)
            };
                DatabaseHelper.Instance.ExecuteProcedure("sp_validasi_resep", parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal validasi resep: " + ex.Message);
            }
        }
    }
}