using SistemaUAB.DataLayers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion.estudiante_tools
{
    /// <summary>
    /// Control que muestra las reservas del estudiante actual
    /// </summary>
    public partial class MisReservasControl : UserControl
    {
        #region Variables Privadas

        private int idUsuarioActual;
        private DataTable reservasDataTable;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que recibe el ID del usuario
        /// </summary>
        /// <param name="idUsuario">ID del estudiante</param>
        public MisReservasControl(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActual = idUsuario;

            // Deshabilitar botones inicialmente
            HabilitarBotones(false);

            // Cargar las reservas
            CargarReservas();
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Carga las reservas del estudiante en el DataGridView
        /// </summary>
        private void CargarReservas()
        {
            try
            {
                // Validar ID de usuario
                if (idUsuarioActual <= 0)
                {
                    MessageBox.Show("ID de usuario no válido.",
                        "Error de Autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Consulta SQL con JOIN para obtener información completa
                string query = @"
                    SELECT 
                        r.IdReserva AS 'NroReserva',
                        a.Codigo AS 'Ambiente',
                        r.Fecha,
                        r.HoraInicio,
                        r.HoraFin,
                        r.Estado,
                        r.Motivo,
                        r.CantidadAsistentes
                    FROM Reserva r
                    INNER JOIN Ambiente a ON r.IdAmbiente = a.IdAmbiente
                    WHERE r.IdUsuario = @IdUsuario
                    ORDER BY r.Fecha DESC, r.HoraInicio DESC";

                var parametros = new Dictionary<string, object>
                {
                    { "@IdUsuario", idUsuarioActual }
                };

                // Ejecutar consulta
                reservasDataTable = Helpers.EjecutarQuery(query, parametros);

                // Configurar el DataGridView
                ConfigurarDataGridView();

                // Asignar datos
                dgvReservas.DataSource = reservasDataTable;

                // Aplicar formato condicional según estado
                AplicarFormatoCondicional();

                // Deshabilitar botones si no hay datos
                if (reservasDataTable.Rows.Count == 0)
                {
                    HabilitarBotones(false);
                    // Mostrar mensaje de que no hay reservas
                    MessageBox.Show("No tiene reservas registradas.",
                        "Sin Reservas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Configura las propiedades visuales del DataGridView
        /// </summary>
        private void ConfigurarDataGridView()
        {
            // Ajustar columnas
            if (dgvReservas.Columns.Count > 0)
            {
                // Ocultar columnas que no queremos mostrar
                if (dgvReservas.Columns.Contains("Motivo"))
                    dgvReservas.Columns["Motivo"].Visible = false;
                if (dgvReservas.Columns.Contains("CantidadAsistentes"))
                    dgvReservas.Columns["CantidadAsistentes"].Visible = false;

                // Configurar encabezados
                if (dgvReservas.Columns.Contains("NroReserva"))
                    dgvReservas.Columns["NroReserva"].HeaderText = "N° Reserva";
                if (dgvReservas.Columns.Contains("Ambiente"))
                    dgvReservas.Columns["Ambiente"].HeaderText = "Ambiente";
                if (dgvReservas.Columns.Contains("Fecha"))
                    dgvReservas.Columns["Fecha"].HeaderText = "Fecha";
                if (dgvReservas.Columns.Contains("HoraInicio"))
                    dgvReservas.Columns["HoraInicio"].HeaderText = "Hora Inicio";
                if (dgvReservas.Columns.Contains("HoraFin"))
                    dgvReservas.Columns["HoraFin"].HeaderText = "Hora Fin";
                if (dgvReservas.Columns.Contains("Estado"))
                    dgvReservas.Columns["Estado"].HeaderText = "Estado";

                // Configurar formato de fechas
                if (dgvReservas.Columns.Contains("Fecha"))
                {
                    dgvReservas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvReservas.Columns.Contains("HoraInicio"))
                {
                    dgvReservas.Columns["HoraInicio"].DefaultCellStyle.Format = "HH:mm";
                }
                if (dgvReservas.Columns.Contains("HoraFin"))
                {
                    dgvReservas.Columns["HoraFin"].DefaultCellStyle.Format = "HH:mm";
                }
            }
        }

        /// <summary>
        /// Aplica formato condicional a las filas según el estado
        /// </summary>
        private void AplicarFormatoCondicional()
        {
            if (dgvReservas.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvReservas.Rows)
            {
                if (row.Cells["Estado"].Value != null)
                {
                    string estado = row.Cells["Estado"].Value.ToString();

                    switch (estado.ToLower())
                    {
                        case "activa":
                        case "confirmada":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(0, 70, 30);
                            break;
                        case "cancelada":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(245, 204, 204);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(139, 0, 0);
                            break;
                        case "finalizada":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);
                            break;
                        case "pendiente":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 204);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(130, 100, 0);
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Habilita o deshabilita los botones según el estado de la reserva seleccionada
        /// </summary>
        private void ActualizarEstadoBotones()
        {
            try
            {
                if (dgvReservas.SelectedRows.Count == 0)
                {
                    HabilitarBotones(false);
                    return;
                }

                DataGridViewRow row = dgvReservas.SelectedRows[0];
                if (row.Cells["Estado"].Value == null)
                {
                    HabilitarBotones(false);
                    return;
                }

                string estado = row.Cells["Estado"].Value.ToString().ToLower();

                // Habilitar botones por defecto
                HabilitarBotones(true);

                // Deshabilitar según el estado
                switch (estado)
                {
                    case "cancelada":
                        btnCancelar.Enabled = false;
                        btnModificar.Enabled = false;
                        break;
                    case "finalizada":
                        btnCancelar.Enabled = false;
                        btnModificar.Enabled = false;
                        break;
                    case "pendiente":
                        btnCancelar.Enabled = true;
                        btnModificar.Enabled = false;
                        break;
                    case "activa":
                    case "confirmada":
                        btnCancelar.Enabled = true;
                        btnModificar.Enabled = true;
                        break;
                    default:
                        btnCancelar.Enabled = false;
                        btnModificar.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar estado de botones: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Habilita o deshabilita todos los botones
        /// </summary>
        /// <param name="habilitar">True para habilitar, False para deshabilitar</param>
        private void HabilitarBotones(bool habilitar)
        {
            btnVerDetalle.Enabled = habilitar;
            btnModificar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        /// <summary>
        /// Valida que la reserva pertenezca al usuario actual
        /// </summary>
        /// <param name="idReserva">ID de la reserva a validar</param>
        /// <returns>True si la reserva pertenece al usuario, False en caso contrario</returns>
        private bool ValidarPertenenciaReserva(int idReserva)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Reserva 
                    WHERE IdReserva = @IdReserva AND IdUsuario = @IdUsuario";

                var parametros = new Dictionary<string, object>
                {
                    { "@IdReserva", idReserva },
                    { "@IdUsuario", idUsuarioActual }
                };

                int count = Helpers.ObtenerEscalar<int>(query, parametros);
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar pertenencia de reserva: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento cuando se selecciona una fila en el DataGridView
        /// </summary>
        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        /// <summary>
        /// Evento Click del botón Ver Detalle
        /// </summary>
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una reserva para ver su detalle.",
                        "Sin Selección",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow row = dgvReservas.SelectedRows[0];
                int idReserva = Convert.ToInt32(row.Cells["NroReserva"].Value);
                string ambiente = row.Cells["Ambiente"].Value.ToString();
                string fecha = Convert.ToDateTime(row.Cells["Fecha"].Value).ToString("dd/MM/yyyy");
                string horaInicio = row.Cells["HoraInicio"].Value.ToString();
                string horaFin = row.Cells["HoraFin"].Value.ToString();
                string estado = row.Cells["Estado"].Value.ToString();

                // Mostrar detalle en un MessageBox (en producción, se usaría un formulario)
                string mensaje = $"Detalle de la Reserva\n\n" +
                               $"N° Reserva: {idReserva}\n" +
                               $"Ambiente: {ambiente}\n" +
                               $"Fecha: {fecha}\n" +
                               $"Hora: {horaInicio} - {horaFin}\n" +
                               $"Estado: {estado}";

                MessageBox.Show(mensaje,
                    "Detalle de Reserva",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar detalle: {ex.Message}",
                    "Error de Detalle",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click del botón Modificar
        /// </summary>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una reserva para modificar.",
                        "Sin Selección",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow row = dgvReservas.SelectedRows[0];
                int idReserva = Convert.ToInt32(row.Cells["NroReserva"].Value);

                // Validar pertenencia (seguridad adicional)
                if (!ValidarPertenenciaReserva(idReserva))
                {
                    MessageBox.Show("Esta reserva no pertenece al usuario actual.",
                        "Error de Seguridad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // TODO: Implementar funcionalidad de modificación
                MessageBox.Show("Funcionalidad de modificación en desarrollo.\n" +
                               "Próximamente podrá modificar sus reservas.",
                    "En Desarrollo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar reserva: {ex.Message}",
                    "Error de Modificación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click del botón Cancelar
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una reserva para cancelar.",
                        "Sin Selección",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow row = dgvReservas.SelectedRows[0];
                int idReserva = Convert.ToInt32(row.Cells["NroReserva"].Value);
                string estadoActual = row.Cells["Estado"].Value.ToString().ToLower();

                // Validar que la reserva esté activa
                if (estadoActual == "cancelada")
                {
                    MessageBox.Show("Esta reserva ya está cancelada.",
                        "Reserva Cancelada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (estadoActual == "finalizada")
                {
                    MessageBox.Show("No se puede cancelar una reserva finalizada.",
                        "Reserva Finalizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar pertenencia (seguridad adicional)
                if (!ValidarPertenenciaReserva(idReserva))
                {
                    MessageBox.Show("Esta reserva no pertenece al usuario actual.",
                        "Error de Seguridad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Confirmar cancelación
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de cancelar esta reserva?\n" +
                    $"Reserva N° {idReserva} - {row.Cells["Ambiente"].Value}\n" +
                    $"Fecha: {Convert.ToDateTime(row.Cells["Fecha"].Value):dd/MM/yyyy}",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                // Solicitar motivo de cancelación
                string motivo = InputBox.Show("Motivo de Cancelación",
                    "Por favor, indique el motivo de la cancelación:",
                    "Cancelación solicitada por el estudiante");

                // Si el usuario cancela el InputBox, no proceder
                if (motivo == null)
                    return;

                // Si el motivo está vacío, usar uno por defecto
                if (string.IsNullOrWhiteSpace(motivo))
                {
                    motivo = "Cancelación solicitada por el estudiante (sin motivo especificado)";
                }

                // Ejecutar actualización en la base de datos
                string query = @"
                    UPDATE Reserva 
                    SET Estado = 'Cancelada', 
                        Motivo = @Motivo 
                    WHERE IdReserva = @IdReserva";

                var parametros = new Dictionary<string, object>
                {
                    { "@IdReserva", idReserva },
                    { "@Motivo", motivo.Trim() }
                };

                int filasAfectadas = Helpers.EjecutarNonQuery(query, parametros);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Reserva cancelada exitosamente.",
                        "Cancelación Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Recargar las reservas
                    CargarReservas();
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar la reserva.",
                        "Error de Cancelación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar reserva: {ex.Message}",
                    "Error de Cancelación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Clase InputBox (Helper)

        /// <summary>
        /// Clase estática para mostrar un InputBox simple
        /// </summary>
        private static class InputBox
        {
            public static string Show(string title, string promptText, string defaultValue = "")
            {
                using (Form form = new Form())
                {
                    form.Text = title;
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.Size = new Size(400, 150);
                    form.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form.MaximizeBox = false;
                    form.MinimizeBox = false;

                    Label label = new Label();
                    label.Text = promptText;
                    label.Location = new Point(12, 15);
                    label.Size = new Size(360, 20);
                    form.Controls.Add(label);

                    TextBox textBox = new TextBox();
                    textBox.Text = defaultValue;
                    textBox.Location = new Point(12, 40);
                    textBox.Size = new Size(360, 20);
                    form.Controls.Add(textBox);

                    Button buttonOk = new Button();
                    buttonOk.Text = "Aceptar";
                    buttonOk.DialogResult = DialogResult.OK;
                    buttonOk.Location = new Point(230, 75);
                    buttonOk.Size = new Size(70, 25);
                    form.Controls.Add(buttonOk);

                    Button buttonCancel = new Button();
                    buttonCancel.Text = "Cancelar";
                    buttonCancel.DialogResult = DialogResult.Cancel;
                    buttonCancel.Location = new Point(310, 75);
                    buttonCancel.Size = new Size(70, 25);
                    form.Controls.Add(buttonCancel);

                    form.AcceptButton = buttonOk;
                    form.CancelButton = buttonCancel;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        return textBox.Text;
                    }
                    return null;
                }
            }
        }

        #endregion
    }
}