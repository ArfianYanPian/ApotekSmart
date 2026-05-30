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

        public void ValidasiResep(int idResep, string status, int apotekerId, string catatan)
        {
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@p_id_resep", idResep),
                new NpgsqlParameter("@p_status", status),
                new NpgsqlParameter("@p_id_apoteker", apotekerId),
                new NpgsqlParameter("@p_catatan", catatan)
            };
            DatabaseHelper.Instance.ExecuteProcedure("sp_validasi_resep", parameters); //
        }
    }
}