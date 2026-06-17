using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.estudiante_tools
{
    /// <summary>
    /// Control para crear nuevas reservas con soporte para recurrencia
    /// </summary>
    public partial class NuevaReservaControl : UserControl
    {
        #region Variables Privadas

        private int idUsuarioActual;
        private DataTable ambientesDataTable;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que recibe el ID del usuario
        /// </summary>
        /// <param name="idUsuario">ID del estudiante logueado</param>
        public NuevaReservaControl(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActual = idUsuario;

            // Configurar fecha por defecto
            dtpFecha.Value = DateTime.Now.Date;
            dtpFechaFin.Value = DateTime.Now.Date.AddDays(7);

            // Cargar horas en los ComboBox
            CargarHorasDisponibles();

            // Establecer hora por defecto (08:00 - 10:00)
            cmbHoraInicio.SelectedItem = "08:00";
            cmbHoraFin.SelectedItem = "10:00";

            // Cargar ambientes
            CargarAmbientes();
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Carga las horas disponibles en los ComboBox
        /// </summary>
        private void CargarHorasDisponibles()
        {
            try
            {
                // Limpiar items existentes
                cmbHoraInicio.Items.Clear();
                cmbHoraFin.Items.Clear();

                // Generar horas desde 07:00 hasta 21:00
                for (int hora = 7; hora <= 21; hora++)
                {
                    string horaStr = hora.ToString("00") + ":00";
                    cmbHoraInicio.Items.Add(horaStr);
                    cmbHoraFin.Items.Add(horaStr);
                }

                // Seleccionar por defecto
                if (cmbHoraInicio.Items.Count > 0)
                    cmbHoraInicio.SelectedIndex = 1; // 08:00
                if (cmbHoraFin.Items.Count > 0)
                    cmbHoraFin.SelectedIndex = 3; // 10:00
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las horas: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga los ambientes disponibles en el ComboBox
        /// </summary>
        private void CargarAmbientes()
        {
            try
            {
                string query = @"
                    SELECT 
                        a.IdAmbiente,
                        a.Codigo + ' - ' + b.NombreBloque + ' (' + t.NombreTipo + ')' AS NombreAmbiente,
                        a.CapacidadMaxima
                    FROM Ambiente a
                    INNER JOIN Bloque b ON a.IdBloque = b.IdBloque
                    INNER JOIN TipoAmbiente t ON a.IdTipo = t.IdTipo
                    WHERE a.Estado = 'Disponible'
                    ORDER BY b.NombreBloque, a.Codigo";

                ambientesDataTable = Helpers.EjecutarQuery(query);

                cmbAmbiente.DataSource = ambientesDataTable;
                cmbAmbiente.DisplayMember = "NombreAmbiente";
                cmbAmbiente.ValueMember = "IdAmbiente";

                if (cmbAmbiente.Items.Count == 0)
                {
                    MessageBox.Show("No hay ambientes disponibles para reservar.",
                        "Sin Ambientes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ambientes: {ex.Message}",
                    "Error de Carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida que todos los campos obligatorios estén completos
        /// </summary>
        private bool ValidarCamposObligatorios()
        {
            try
            {
                if (cmbAmbiente.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un ambiente.",
                        "Campo Obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrEmpty(cmbHoraInicio.Text) || string.IsNullOrEmpty(cmbHoraFin.Text))
                {
                    MessageBox.Show("Seleccione un rango de horas válido.",
                        "Campo Obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                TimeSpan horaInicio = TimeSpan.Parse(cmbHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(cmbHoraFin.Text);

                if (horaInicio >= horaFin)
                {
                    MessageBox.Show("La hora de inicio debe ser menor que la hora de fin.",
                        "Horas Inválidas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtMotivo.Text))
                {
                    MessageBox.Show("Ingrese el motivo de la reserva.",
                        "Campo Obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (numAsistentes.Value <= 0)
                {
                    MessageBox.Show("La cantidad de asistentes debe ser mayor a 0.",
                        "Campo Obligatorio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                // Validar fecha recurrente
                if (chkEsRecurrente.Checked)
                {
                    if (dtpFechaFin.Value.Date < dtpFecha.Value.Date)
                    {
                        MessageBox.Show("La fecha de fin debe ser igual o posterior a la fecha de inicio.",
                            "Fechas Inválidas",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }

                    if (cmbFrecuencia.SelectedItem == null)
                    {
                        MessageBox.Show("Seleccione la frecuencia de recurrencia.",
                            "Campo Obligatorio",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar campos: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Valida la capacidad del ambiente
        /// </summary>
        private bool ValidarCapacidad()
        {
            try
            {
                if (cmbAmbiente.SelectedItem == null)
                    return false;

                int idAmbiente = Convert.ToInt32(cmbAmbiente.SelectedValue);
                int capacidadRequerida = (int)numAsistentes.Value;

                int capacidadDisponible;
                bool esValida = Validaciones.ValidarCapacidad(idAmbiente, capacidadRequerida, out capacidadDisponible);

                if (!esValida)
                {
                    MessageBox.Show($"La capacidad requerida ({capacidadRequerida}) excede la capacidad máxima del ambiente ({capacidadDisponible}).",
                        "Capacidad Insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                return esValida;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar capacidad: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Valida la disponibilidad del horario
        /// </summary>
        private bool ValidarDisponibilidad(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, int idAmbiente)
        {
            try
            {
                bool disponible = Validaciones.ValidarHorario(fecha, horaInicio, horaFin, idAmbiente);

                if (!disponible)
                {
                    MessageBox.Show($"El horario no está disponible para el ambiente seleccionado en la fecha {fecha:dd/MM/yyyy}.",
                        "Horario Ocupado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                return disponible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar disponibilidad: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Inserta una reserva en la base de datos
        /// </summary>
        private bool InsertarReserva(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin,
            int idAmbiente, int cantidadAsistentes, string motivo, string observaciones)
        {
            try
            {
                string query = @"
                    INSERT INTO Reserva 
                    (IdUsuario, IdAmbiente, Fecha, HoraInicio, HoraFin, CantidadAsistentes, Motivo, Estado)
                    VALUES 
                    (@IdUsuario, @IdAmbiente, @Fecha, @HoraInicio, @HoraFin, @CantidadAsistentes, @Motivo, 'Activa')";

                var parametros = new Dictionary<string, object>
                {
                    { "@IdUsuario", idUsuarioActual },
                    { "@IdAmbiente", idAmbiente },
                    { "@Fecha", fecha },
                    { "@HoraInicio", horaInicio },
                    { "@HoraFin", horaFin },
                    { "@CantidadAsistentes", cantidadAsistentes },
                    { "@Motivo", motivo }
                };

                // Agregar observaciones si no está vacío
                if (!string.IsNullOrWhiteSpace(observaciones))
                {
                    query = query.Replace("Estado", "Observaciones, Estado");
                    query = query.Replace("'Activa'", "@Observaciones, 'Activa'");
                    parametros.Add("@Observaciones", observaciones);
                }

                int filasAfectadas = Helpers.EjecutarNonQuery(query, parametros);
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar reserva: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Procesa la creación de reservas recurrentes
        /// </summary>
        private void ProcesarReservasRecurrentes()
        {
            try
            {
                DateTime fechaInicio = dtpFecha.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;
                TimeSpan horaInicio = TimeSpan.Parse(cmbHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(cmbHoraFin.Text);
                int idAmbiente = Convert.ToInt32(cmbAmbiente.SelectedValue);
                int cantidadAsistentes = (int)numAsistentes.Value;
                string motivo = txtMotivo.Text.Trim();
                string observaciones = txtObservaciones.Text.Trim();

                string frecuencia = cmbFrecuencia.SelectedItem.ToString();
                int reservasCreadas = 0;
                int reservasFallidas = 0;
                List<DateTime> fechasConflictivas = new List<DateTime>();

                DateTime fechaActual = fechaInicio;

                while (fechaActual <= fechaFin)
                {
                    // Validar disponibilidad para cada fecha
                    if (Validaciones.ValidarHorario(fechaActual, horaInicio, horaFin, idAmbiente))
                    {
                        // Insertar reserva
                        bool exito = InsertarReserva(fechaActual, horaInicio, horaFin,
                            idAmbiente, cantidadAsistentes, motivo, observaciones);

                        if (exito)
                        {
                            reservasCreadas++;
                        }
                        else
                        {
                            reservasFallidas++;
                            fechasConflictivas.Add(fechaActual);
                        }
                    }
                    else
                    {
                        reservasFallidas++;
                        fechasConflictivas.Add(fechaActual);
                    }

                    // Avanzar según la frecuencia
                    switch (frecuencia)
                    {
                        case "Diaria":
                            fechaActual = fechaActual.AddDays(1);
                            break;
                        case "Semanal":
                            fechaActual = fechaActual.AddDays(7);
                            break;
                        case "Mensual":
                            fechaActual = fechaActual.AddMonths(1);
                            break;
                        default:
                            fechaActual = fechaActual.AddDays(1);
                            break;
                    }
                }

                // Mostrar resultado
                string mensaje = $"Proceso de reservas recurrentes completado.\n\n" +
                               $"Reservas creadas exitosamente: {reservasCreadas}\n";

                if (reservasFallidas > 0)
                {
                    mensaje += $"Reservas fallidas: {reservasFallidas}\n\n" +
                              $"Fechas con conflictos:\n" +
                              string.Join("\n", fechasConflictivas.ConvertAll(d => d.ToString("dd/MM/yyyy")));
                    MessageBox.Show(mensaje,
                        "Reservas Recurrentes - Parcialmente Exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    mensaje += $"Todas las reservas se crearon exitosamente.";
                    MessageBox.Show(mensaje,
                        "Reservas Recurrentes - Exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Limpiar campos después de un registro exitoso
                if (reservasCreadas > 0)
                {
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar reservas recurrentes: {ex.Message}",
                    "Error de Proceso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario
        /// </summary>
        private void LimpiarCampos()
        {
            try
            {
                dtpFecha.Value = DateTime.Now.Date;
                dtpFechaFin.Value = DateTime.Now.Date.AddDays(7);
                cmbHoraInicio.SelectedIndex = 1;
                cmbHoraFin.SelectedIndex = 3;
                txtMotivo.Clear();
                txtObservaciones.Clear();
                numAsistentes.Value = 1;
                chkEsRecurrente.Checked = false;
                cmbFrecuencia.SelectedIndex = -1;
                cmbAmbiente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar campos: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento Load del control
        /// </summary>
        private void NuevaReservaControl_Load(object sender, EventArgs e)
        {
            // Configurar fecha mínima para dtpFechaFin
            dtpFechaFin.MinDate = dtpFecha.Value.Date;

            // Establecer frecuencia por defecto
            cmbFrecuencia.SelectedIndex = 0; // Diaria
        }

        /// <summary>
        /// Evento Click del botón Registrar
        /// </summary>
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (!ValidarCamposObligatorios())
                    return;

                // Validar capacidad
                if (!ValidarCapacidad())
                    return;

                // Obtener datos
                DateTime fecha = dtpFecha.Value.Date;
                TimeSpan horaInicio = TimeSpan.Parse(cmbHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(cmbHoraFin.Text);
                int idAmbiente = Convert.ToInt32(cmbAmbiente.SelectedValue);
                int cantidadAsistentes = (int)numAsistentes.Value;
                string motivo = txtMotivo.Text.Trim();
                string observaciones = txtObservaciones.Text.Trim();

                // Verificar si es recurrente
                if (chkEsRecurrente.Checked)
                {
                    // Validar fechas de recurrencia
                    if (dtpFechaFin.Value.Date < fecha)
                    {
                        MessageBox.Show("La fecha de fin debe ser igual o posterior a la fecha de inicio.",
                            "Fechas Inválidas",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // Confirmar creación de reservas recurrentes
                    DialogResult confirmacion = MessageBox.Show(
                        $"Se crearán reservas recurrentes desde {fecha:dd/MM/yyyy} hasta {dtpFechaFin.Value.Date:dd/MM/yyyy}\n" +
                        $"Frecuencia: {cmbFrecuencia.SelectedItem}\n" +
                        $"Horario: {horaInicio:hh\\:mm} - {horaFin:hh\\:mm}\n\n" +
                        "¿Desea continuar?",
                        "Confirmar Reservas Recurrentes",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmacion == DialogResult.Yes)
                    {
                        ProcesarReservasRecurrentes();
                    }
                }
                else
                {
                    // Reserva simple
                    // Validar disponibilidad
                    if (!ValidarDisponibilidad(fecha, horaInicio, horaFin, idAmbiente))
                        return;

                    // Confirmar reserva
                    DialogResult confirmacion = MessageBox.Show(
                        $"¿Confirmar reserva del ambiente?\n" +
                        $"Fecha: {fecha:dd/MM/yyyy}\n" +
                        $"Horario: {horaInicio:hh\\:mm} - {horaFin:hh\\:mm}\n" +
                        $"Asistentes: {cantidadAsistentes}\n" +
                        $"Motivo: {motivo}",
                        "Confirmar Reserva",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmacion != DialogResult.Yes)
                        return;

                    // Insertar reserva
                    bool exito = InsertarReserva(fecha, horaInicio, horaFin, idAmbiente,
                        cantidadAsistentes, motivo, observaciones);

                    if (exito)
                    {
                        MessageBox.Show("¡Reserva creada exitosamente!",
                            "Reserva Exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo crear la reserva. Intente nuevamente.",
                            "Error de Reserva",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar reserva: {ex.Message}",
                    "Error de Registro",
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
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro que desea cancelar el registro?\n" +
                    "Los cambios no se guardarán.",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar: {ex.Message}",
                    "Error de Cancelación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento CheckedChanged del CheckBox de recurrencia
        /// </summary>
        private void chkEsRecurrente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool esRecurrente = chkEsRecurrente.Checked;

                // Habilitar/Deshabilitar controles de recurrencia
                cmbFrecuencia.Enabled = esRecurrente;
                dtpFechaFin.Enabled = esRecurrente;
                label8.Enabled = esRecurrente;
                label9.Enabled = esRecurrente;

                // Actualizar fecha mínima de fin
                if (esRecurrente)
                {
                    dtpFechaFin.MinDate = dtpFecha.Value.Date;
                    if (dtpFechaFin.Value.Date < dtpFecha.Value.Date)
                    {
                        dtpFechaFin.Value = dtpFecha.Value.Date.AddDays(7);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar recurrencia: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Evento ValueChanged del DateTimePicker de fecha
        /// </summary>
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Actualizar fecha mínima de dtpFechaFin
                dtpFechaFin.MinDate = dtpFecha.Value.Date;

                if (dtpFechaFin.Value.Date < dtpFecha.Value.Date)
                {
                    dtpFechaFin.Value = dtpFecha.Value.Date.AddDays(7);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar fecha: {ex.Message}",
                    "Error de Interfaz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion
    }
}