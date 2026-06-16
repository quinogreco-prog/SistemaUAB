using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaUAB.DataLayers
{
    /// <summary>
    /// Clase estática para manejar la conexión a la base de datos
    /// </summary>
    public static class Conexion
    {
        // Cadena de conexión desde app.config o Settings
        private static string cadenaConexion = "Data Source=DESKTOP-L1RCIQB\\SQLEXPRESS;Initial Catalog=SistemaReservasUAB;Integrated Security=True";

        static Conexion()
        {
            // Intenta obtener la cadena de conexión desde app.config

            // Si no está configurada en app.config, usa la cadena por defecto
            if (string.IsNullOrEmpty(cadenaConexion))
            {
                cadenaConexion = "Data Source=.;Initial Catalog=SistemaReservasUAB;Integrated Security=True";
            }
        }

        /// <summary>
        /// Obtiene una nueva instancia de SqlConnection
        /// </summary>
        /// <returns>SqlConnection configurada</returns>
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }

    /// <summary>
    /// Clase estática con métodos auxiliares para operaciones de base de datos
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Ejecuta una consulta SELECT y retorna un DataTable
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="parametros">Diccionario de parámetros</param>
        /// <returns>DataTable con los resultados</returns>
        public static DataTable EjecutarQuery(string query, Dictionary<string, object> parametros = null)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        comando.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                conexion.Open();
                DataTable dt = new DataTable();
                dt.Load(comando.ExecuteReader());
                return dt;
            }
        }

        /// <summary>
        /// Ejecuta una consulta INSERT, UPDATE o DELETE
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="parametros">Diccionario de parámetros</param>
        /// <returns>Número de filas afectadas</returns>
        public static int EjecutarNonQuery(string query, Dictionary<string, object> parametros = null)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        comando.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                conexion.Open();
                return comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Ejecuta una consulta que retorna un valor escalar (COUNT, SUM, etc.)
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <param name="parametros">Diccionario de parámetros</param>
        /// <returns>Valor escalar como objeto</returns>
        public static object ObtenerEscalar(string query, Dictionary<string, object> parametros = null)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        comando.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                conexion.Open();
                return comando.ExecuteScalar();
            }
        }

        /// <summary>
        /// Ejecuta una consulta que retorna un valor escalar tipado (COUNT, SUM, etc.)
        /// </summary>
        /// <typeparam name="T">Tipo de dato esperado</typeparam>
        /// <param name="query">Consulta SQL</param>
        /// <param name="parametros">Diccionario de parámetros</param>
        /// <returns>Valor escalar tipado</returns>
        public static T ObtenerEscalar<T>(string query, Dictionary<string, object> parametros = null)
        {
            object resultado = ObtenerEscalar(query, parametros);
            if (resultado == DBNull.Value || resultado == null)
                return default(T);

            return (T)Convert.ChangeType(resultado, typeof(T));
        }
    }

    /// <summary>
    /// Clase estática con métodos de validación para el sistema de reservas
    /// </summary>
    public static class Validaciones
    {
        /// <summary>
        /// Valida si el horario solicitado está disponible para un ambiente específico
        /// </summary>
        /// <param name="fecha">Fecha de la reserva</param>
        /// <param name="horaInicio">Hora de inicio</param>
        /// <param name="horaFin">Hora de fin</param>
        /// <param name="idAmbiente">ID del ambiente</param>
        /// <returns>True si el horario está disponible, False en caso contrario</returns>
        public static bool ValidarHorario(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, int idAmbiente)
        {
            // Validar que la hora de inicio sea menor que la hora de fin
            if (horaInicio >= horaFin)
                return false;

            // Validar que el horario esté dentro del rango permitido (ej. 7:00 - 22:00)
            if (horaInicio < new TimeSpan(7, 0, 0) || horaFin > new TimeSpan(22, 0, 0))
                return false;

            // Validar que la fecha no sea en el pasado
            if (fecha.Date < DateTime.Now.Date)
                return false;

            string query = @"
                SELECT COUNT(*) 
                FROM Reservas 
                WHERE IdAmbiente = @IdAmbiente 
                AND Fecha = @Fecha 
                AND Estado != 'Cancelada'
                AND (
                    (@HoraInicio < HoraFin AND @HoraFin > HoraInicio)
                )";

            var parametros = new Dictionary<string, object>
            {
                { "@IdAmbiente", idAmbiente },
                { "@Fecha", fecha.Date },
                { "@HoraInicio", horaInicio },
                { "@HoraFin", horaFin }
            };

            int count = Helpers.ObtenerEscalar<int>(query, parametros);
            return count == 0;
        }

        /// <summary>
        /// Valida si un nombre de usuario es único en el sistema
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a validar</param>
        /// <param name="idUsuarioExcluir">ID de usuario a excluir (para ediciones)</param>
        /// <returns>True si el nombre de usuario está disponible, False en caso contrario</returns>
        public static bool ValidarUsuarioUnico(string nombreUsuario, int? idUsuarioExcluir = null)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return false;

            string query = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            var parametros = new Dictionary<string, object>
            {
                { "@NombreUsuario", nombreUsuario.Trim() }
            };

            if (idUsuarioExcluir.HasValue)
            {
                query += " AND IdUsuario != @IdUsuarioExcluir";
                parametros.Add("@IdUsuarioExcluir", idUsuarioExcluir.Value);
            }

            int count = Helpers.ObtenerEscalar<int>(query, parametros);
            return count == 0;
        }

        /// <summary>
        /// Valida que la cantidad de asistentes no exceda la capacidad del ambiente
        /// </summary>
        /// <param name="idAmbiente">ID del ambiente</param>
        /// <param name="cantidadAsistentes">Cantidad de asistentes</param>
        /// <returns>True si la capacidad es suficiente, False en caso contrario</returns>
        public static bool ValidarCapacidad(int idAmbiente, int cantidadAsistentes)
        {
            if (cantidadAsistentes <= 0)
                return false;

            string query = "SELECT Capacidad FROM Ambientes WHERE IdAmbiente = @IdAmbiente";
            var parametros = new Dictionary<string, object>
            {
                { "@IdAmbiente", idAmbiente }
            };

            object resultado = Helpers.ObtenerEscalar(query, parametros);

            if (resultado == null || resultado == DBNull.Value)
                return false;

            int capacidad = Convert.ToInt32(resultado);
            return cantidadAsistentes <= capacidad;
        }

        /// <summary>
        /// Valida que la cantidad de asistentes no exceda la capacidad del ambiente
        /// (Versión que retorna la capacidad del ambiente)
        /// </summary>
        /// <param name="idAmbiente">ID del ambiente</param>
        /// <param name="cantidadAsistentes">Cantidad de asistentes</param>
        /// <param name="capacidadDisponible">Capacidad del ambiente (parámetro de salida)</param>
        /// <returns>True si la capacidad es suficiente, False en caso contrario</returns>
        public static bool ValidarCapacidad(int idAmbiente, int cantidadAsistentes, out int capacidadDisponible)
        {
            capacidadDisponible = 0;

            if (cantidadAsistentes <= 0)
                return false;

            string query = "SELECT Capacidad FROM Ambientes WHERE IdAmbiente = @IdAmbiente";
            var parametros = new Dictionary<string, object>
            {
                { "@IdAmbiente", idAmbiente }
            };

            object resultado = Helpers.ObtenerEscalar(query, parametros);

            if (resultado == null || resultado == DBNull.Value)
                return false;

            capacidadDisponible = Convert.ToInt32(resultado);
            return cantidadAsistentes <= capacidadDisponible;
        }
    }
}