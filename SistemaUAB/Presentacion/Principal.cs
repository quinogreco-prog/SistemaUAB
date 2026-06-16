using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion
{
    public partial class Principal : Form
    {
        private string tipo_usuario;

        public Principal(string tipo_usuario) // Recibe: 'Admin', 'Docente', 'Estudiante', 'Miembro de Iglesia'
        {
            InitializeComponent();
            this.tipo_usuario = tipo_usuario;

            // Configuración básica de la ventana principal
            this.Text = $"Sistema UAB - Panel Principal [{this.tipo_usuario}]";
            CargarTarjetaSegunRol();
        }


        private void CargarTarjetaSegunRol()
        {
            // Limpiar el panel
            this.tableLayoutPanel1.Controls.Clear();

            // Evaluamos el rol e inyectamos el User Control correspondiente
            switch (tipo_usuario)
            {
                case "Admin":
                    UcTarjetaAdmin tarjetaAdmin = new UcTarjetaAdmin();
                    tarjetaAdmin.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(tarjetaAdmin);
                    break;

                case "Docente":
                    UcTarjetaDocente tarjetaDocente = new UcTarjetaDocente();
                    tarjetaDocente.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(tarjetaDocente);
                    break;

                case "Estudiante":
                    UcTarjetaEstudiante tarjetaEstudiante = new UcTarjetaEstudiante();
                    tarjetaEstudiante.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(tarjetaEstudiante);
                    break;

                case "Miembro de Iglesia":
                    UcTarjetaMiembro tarjetaMiembro = new UcTarjetaMiembro();
                    tarjetaMiembro.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(tarjetaMiembro);
                    break;

                default:
                    MessageBox.Show("Rol de usuario no reconocido.", "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}