using System.Data.SqlClient;

namespace SistemaReservasUAB
{
    public static class Conexion
    {
        // Cambia el Data Source por el nombre de tu servidor local de SQL Server
        private static string cadena = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=SistemaReservasUAB;Integrated Security=True";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}