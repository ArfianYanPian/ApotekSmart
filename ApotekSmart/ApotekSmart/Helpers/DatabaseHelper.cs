using System;
using System.Data;
using Npgsql;

namespace ApotekSmart.Helpers
{
    public class DatabaseHelper
    {
        private static DatabaseHelper _instance;
        private string _connectionString;

        private DatabaseHelper()
        {
            _connectionString = "Host=localhost;Port=5432;Database=apotek_smart;Username=postgres;Password=190727";
        }

        // Singleton — hanya ada 1 instance DatabaseHelper
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseHelper();
                return _instance;
            }
        }

        // Buka koneksi
        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        // Untuk SELECT — return DataTable
        public DataTable ExecuteQuery(string sql, NpgsqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Untuk INSERT, UPDATE, DELETE — return jumlah baris terpengaruh
        public int ExecuteNonQuery(string sql, NpgsqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Untuk memanggil Stored Procedure
        public void ExecuteProcedure(string procedureName, NpgsqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}