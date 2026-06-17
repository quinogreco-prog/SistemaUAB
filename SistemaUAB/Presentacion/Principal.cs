using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion
{
    public partial class Principal : Form
    {
        private string tipo_usuario;
        private UserControl userControlActual;
        private string idEstudiante;

        public Principal(string tipo_usuario, string idEstudiante)
        {
            InitializeComponent();
            this.tipo_usuario = tipo_usuario;
            this.idEstudiante = idEstudiante;
            this.Text = $"Sistema UAB - Panel Principal [{this.tipo_usuario}]";

            // Cargar el UserControl correspondiente
            CargarTarjetaSegunRol();
        }

        private void CargarTarjetaSegunRol()
        {
            try
            {
                // Limpiar el panel
                panelContenedor.Controls.Clear();

                if (userControlActual != null)
                {
                    userControlActual.Dispose();
                    userControlActual = null;
                }

                // Crear el control según el rol
                switch (tipo_usuario)
                {
                    case "Admin":
                        userControlActual = new UcTarjetaAdmin();
                        break;
                    case "Docente":
                        userControlActual = new UcTarjetaDocente();
                        break;
                    case "Estudiante":
                        userControlActual = new UcTarjetaEstudiante(int.Parse(this.idEstudiante), panelContenedor);
                        break;
                    case "Miembro de Iglesia":
                        userControlActual = new UcTarjetaMiembro();
                        break;
                    default:
                        MessageBox.Show("Rol de usuario no reconocido.",
                            "Error del Sistema",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                }

                if (userControlActual != null)
                {
                    // 🔥 NO usar DockStyle.Fill, mantener el tamaño original
                    userControlActual.Dock = DockStyle.None;
                    userControlActual.Location = new Point(0, 0);

                    panelContenedor.Controls.Add(userControlActual);

                    // Ajustar el tamaño del panel al del control
                    panelContenedor.Size = userControlActual.Size;

                    // Ajustar el tamaño del formulario
                    this.Size = new Size(
                        userControlActual.Width + 30,  // + margen
                        userControlActual.Height + 60   // + margen para barra de título
                    );

                    this.CenterToScreen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la interfaz: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}