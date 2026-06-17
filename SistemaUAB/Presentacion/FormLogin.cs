using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña.", "Campos Vacíos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar credenciales contra la base de datos
            string tipo_usuario = null;
            bool credencialesValidas = false;

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string query = @"SELECT TipoUsuario, Estado 
                           FROM Usuario 
                           WHERE NombreUsuario = @usuario AND Contrasena = @contrasena";
                        
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contrasena", contrasena);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string estado = reader["Estado"].ToString();

                                if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                                {
                                    credencialesValidas = true;
                                    tipo_usuario = reader["TipoUsuario"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("El usuario está inactivo. Contacte al administrador.",
                                        "Usuario Inactivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }

                if (credencialesValidas)
                {
                    this.Hide(); // Ocultar FormLogin
                    Principal ventanaPrincipal = new Principal(tipo_usuario);

                    // Suscribirse al evento de cierre de Principal
                    ventanaPrincipal.FormClosed += (s, args) =>
                    {
                        this.Show(); // Mostrar nuevamente FormLogin
                        this.BringToFront(); // Traer al frente
                        txtUsuario.Clear(); // Limpiar campos
                        txtContrasena.Clear();
                        txtUsuario.Focus(); // Enfocar en usuario
                    };

                    ventanaPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos. Intente nuevamente.",
                        "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Limpiar campos
                    txtUsuario.Clear();
                    txtContrasena.Clear();
                    txtUsuario.Focus();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}",
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
