using System;
using System.Data;
using Npgsql;

namespace ApotekSmart.Helpers
{
    public class DatabaseHelper
    {
        private static DatabaseHelper _instance;
        // FIX: tambahkan lock object untuk thread-safety
        private static readonly object _lock = new object();
        private readonly string _connectionString;

        private DatabaseHelper()
        {
            _connectionString =
                "Host=localhost;Port=5432;Database=apotek_smart;" +
                "Username=postgres;Password=190727";
        }

        // FIX: double-checked locking — aman diakses dari multiple thread
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new DatabaseHelper();
                    }
                }
                return _instance;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

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

        public void ExecuteProcedure(string procedureName,
                                     NpgsqlParameter[] parameters = null)
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

        // Helper untuk transaksi manual (digunakan BuatTransaksiResep, dll.)
        public NpgsqlTransaction BeginTransaction(NpgsqlConnection conn)
        {
            return conn.BeginTransaction();
        }
    }
}