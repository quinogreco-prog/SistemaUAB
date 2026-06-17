using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.Presentacion.estudiante_tools;

namespace SistemaUAB.Presentacion
{
    /// <summary>
    /// Control principal para el estudiante en el sistema de reservas
    /// </summary>
    public partial class UcTarjetaEstudiante : UserControl
    {
        #region Variables Globales

        private int idUsuarioActual;
        private string pestanaActual;
        private Panel panelContent;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor principal que recibe el ID del usuario
        /// </summary>
        /// <param name="idUsuario">ID del estudiante logueado</param>
        public UcTarjetaEstudiante(int idUsuario, Panel panelContent)
        {
            InitializeComponent();
            this.idUsuarioActual = idUsuario;
            this.pestanaActual = string.Empty;
            this.panelContent = panelContent;

            // Cargar la vista por defecto (Mis Reservas)
            CargarVistaPorDefecto();
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Carga la vista por defecto al iniciar el control
        /// </summary>
        private void CargarVistaPorDefecto()
        {
            try
            {
                // Seleccionar "Mis Reservas" por defecto
                var misReservas = new MisReservasControl(this.idUsuarioActual);
                ExpandirContenedor(misReservas, "MisReservas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la vista por defecto: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Expande un control en el panel de contenido y actualiza la navegación
        /// </summary>
        /// <param name="nuevoControl">Control a mostrar</param>
        /// <param name="nombrePestana">Identificador de la pestaña</param>
        private void ExpandirContenedor(UserControl nuevoControl, string nombrePestana)
        {
            try
            {
                // Validar parámetros
                if (nuevoControl == null)
                    throw new ArgumentNullException(nameof(nuevoControl), "El control no puede ser nulo");

                // Limpiar el panel de contenido
                panelContentEstudiante.Controls.Clear();

                // Configurar el nuevo control
                nuevoControl.Dock = DockStyle.Fill;
                nuevoControl.Visible = true;
                nuevoControl.AutoSize = false;
                nuevoControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                nuevoControl.Margin = new Padding(0); // Eliminar márgenes
                nuevoControl.Padding = new Padding(0); // Eliminar padding

                // Agregar al panel
                panelContentEstudiante.Controls.Add(nuevoControl);

                // Forzar el redibujado para aplicar el tamaño correcto
                nuevoControl.Refresh();
                panelContentEstudiante.Refresh();

                // Actualizar pestaña actual
                this.pestanaActual = nombrePestana;

                // Restaurar colores de todos los botones
                RestaurarColoresBotones();

                // Resaltar el botón correspondiente a la pestaña actual
                ResaltarBotonSeleccionado(nombrePestana);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al expandir el contenedor: {ex.Message}",
                    "Error de Navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restaura el color de fondo de todos los botones del menú a su color por defecto
        /// </summary>
        private void RestaurarColoresBotones()
        {
            try
            {
                Color colorDefault = Color.FromArgb(52, 73, 94);

                btnMisReservas.BackColor = colorDefault;
                btnBuscarAmbientes.BackColor = colorDefault;
                btnNuevaReserva.BackColor = colorDefault;
                // btnCerrarSesion mantiene su color rojo fijo
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al restaurar colores de botones: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Resalta el botón correspondiente a la pestaña activa
        /// </summary>
        /// <param name="nombrePestana">Identificador de la pestaña</param>
        private void ResaltarBotonSeleccionado(string nombrePestana)
        {
            try
            {
                Color colorSeleccionado = Color.LightBlue;

                switch (nombrePestana)
                {
                    case "MisReservas":
                        btnMisReservas.BackColor = colorSeleccionado;
                        break;
                    case "BuscarAmbientes":
                        btnBuscarAmbientes.BackColor = colorSeleccionado;
                        break;
                    case "NuevaReserva":
                        btnNuevaReserva.BackColor = colorSeleccionado;
                        break;
                    default:
                        // Si no coincide con ninguna, no resaltar ninguno
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al resaltar botón seleccionado: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Eventos Click de Navegación

        /// <summary>
        /// Evento Click para el botón "Mis Reservas"
        /// </summary>
        private void btnMisReservas_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el ID de usuario sea válido
                if (this.idUsuarioActual <= 0)
                {
                    MessageBox.Show("ID de usuario no válido. Por favor, inicie sesión nuevamente.",
                        "Error de Autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Crear instancia del control MisReservas pasando el ID del usuario
                var misReservas = new MisReservasControl(this.idUsuarioActual);
                ExpandirContenedor(misReservas, "MisReservas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Mis Reservas: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click para el botón "Buscar Ambientes"
        /// </summary>
        private void btnBuscarAmbientes_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear instancia del control BuscarAmbientes
                var buscarAmbientes = new BuscarAmbientesControl(this.idUsuarioActual);
                ExpandirContenedor(buscarAmbientes, "BuscarAmbientes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Buscar Ambientes: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click para el botón "Nueva Reserva"
        /// </summary>
        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el ID de usuario sea válido
                if (this.idUsuarioActual <= 0)
                {
                    MessageBox.Show("ID de usuario no válido. Por favor, inicie sesión nuevamente.",
                        "Error de Autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Crear instancia del control NuevaReserva pasando el ID del usuario
                var nuevaReserva = new NuevaReservaControl(this.idUsuarioActual);
                ExpandirContenedor(nuevaReserva, "NuevaReserva");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Nueva Reserva: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click para el botón "Cerrar Sesión"
        /// </summary>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirmar con el usuario antes de cerrar sesión
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro que desea cerrar sesión?",
                    "Confirmar Cierre de Sesión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Cerrar el formulario principal
                    Form formularioPrincipal = this.FindForm();
                    if (formularioPrincipal != null)
                    {
                        formularioPrincipal.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el formulario principal para cerrar sesión.",
                            "Error de Cierre",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error de Cierre de Sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obtiene el ID del usuario actual
        /// </summary>
        /// <returns>ID del usuario actual</returns>
        public int ObtenerIdUsuarioActual()
        {
            return this.idUsuarioActual;
        }

        /// <summary>
        /// Obtiene la pestaña actualmente activa
        /// </summary>
        /// <returns>Nombre de la pestaña activa</returns>
        public string ObtenerPestanaActual()
        {
            return this.pestanaActual;
        }

        /// <summary>
        /// Actualiza el ID del usuario (útil para cambiar de usuario sin recrear el control)
        /// </summary>
        /// <param name="idUsuario">Nuevo ID de usuario</param>
        public void ActualizarIdUsuario(int idUsuario)
        {
            if (idUsuario > 0)
            {
                this.idUsuarioActual = idUsuario;
            }
        }

        #endregion
    }
}