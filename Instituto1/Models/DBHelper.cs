using Microsoft.Data.SqlClient;
using System.Data;

namespace InstitutoAcademico.Models
{
    public class DBHelper
    {
        private readonly string _connectionString;

        public DBHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

      
        public DataTable EjecutarSP(string nombreSP, object? parametros = null)
        {
            var dt = new DataTable();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(nombreSP, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            AgregarParametros(cmd, parametros);
            con.Open();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }


        public DataSet EjecutarSPMultiple(string nombreSP, object? parametros = null)
        {
            var ds = new DataSet();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(nombreSP, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            AgregarParametros(cmd, parametros);
            con.Open();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        private static void AgregarParametros(SqlCommand cmd, object? parametros)
        {
            if (parametros == null) return;
            foreach (var prop in parametros.GetType().GetProperties())
            {
                var value = prop.GetValue(parametros) ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@" + prop.Name, value);
            }
        }
    }
}
