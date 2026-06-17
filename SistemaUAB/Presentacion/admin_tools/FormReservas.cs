using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class FormReservas : Form
    {
        // Propiedades públicas
        public int? ReservaId { get; private set; }
        public int? UsuarioSeleccionadoId { get; set; }

        public FormReservas()
            : this(null)
        {
        }

        public FormReservas(int? idReserva = null)
        {
            InitializeComponent();

            // Configurar el formulario
            ReservaId = idReserva;
            this.Text = idReserva.HasValue ? "✏️ Editar Reserva" : "📝 Nueva Reserva";

            // Cargar datos
            CargarDatosIniciales();

            if (idReserva.HasValue)
            {
                CargarDatosReserva(idReserva.Value);
            }
            else
            {
                ConfigurarValoresPorDefecto();
            }

            // Configurar eventos
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            this.cmbAmbiente.SelectedIndexChanged += OnCampoCambiado;
            this.dtpFecha.ValueChanged += OnCampoCambiado;
            this.dtpHoraInicio.ValueChanged += OnHoraCambiada;
            this.dtpHoraFin.ValueChanged += OnHoraCambiada;
            this.nudAsistentes.ValueChanged += OnAsistentesCambiados;
            this.btnGuardar.Click += BtnGuardar_Click;
            this.btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        private void CargarDatosIniciales()
        {
            try
            {
                // Cargar ambientes
                DataTable ambientes = CargarAmbientes();
                this.cmbAmbiente.DataSource = ambientes;
                this.cmbAmbiente.DisplayMember = "Codigo";
                this.cmbAmbiente.ValueMember = "IdAmbiente";

                // Cargar usuarios activos
                DataTable usuarios = CargarUsuariosActivos();
                this.cmbUsuario.DataSource = usuarios;
                this.cmbUsuario.DisplayMember = "NombreCompleto";
                this.cmbUsuario.ValueMember = "IdUsuario";

                // Si hay un usuario seleccionado, ponerlo por defecto
                if (UsuarioSeleccionadoId.HasValue)
                {
                    this.cmbUsuario.SelectedValue = UsuarioSeleccionadoId.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarValoresPorDefecto()
        {
            this.dtpFecha.Value = DateTime.Today;
            this.dtpHoraInicio.Value = DateTime.Today.AddHours(8);
            this.dtpHoraFin.Value = DateTime.Today.AddHours(9);
            this.nudAsistentes.Value = 1;
        }

        private void CargarDatosReserva(int idReserva)
        {
            try
            {
                string query = @"SELECT IdAmbiente, IdUsuario, Fecha, HoraInicio, HoraFin, CantidadAsistentes, Motivo 
                                FROM Reserva WHERE IdReserva = @idReserva";
                var parameters = new Dictionary<string, object> { { "@idReserva", idReserva } };
                DataTable data = Helpers.EjecutarQuery(query, parameters);

                if (data.Rows.Count > 0)
                {
                    DataRow row = data.Rows[0];
                    this.cmbAmbiente.SelectedValue = row["IdAmbiente"];
                    this.cmbUsuario.SelectedValue = row["IdUsuario"];
                    this.dtpFecha.Value = Convert.ToDateTime(row["Fecha"]);
                    this.dtpHoraInicio.Value = DateTime.Today.Add((TimeSpan)row["HoraInicio"]);
                    this.dtpHoraFin.Value = DateTime.Today.Add((TimeSpan)row["HoraFin"]);
                    this.nudAsistentes.Value = Convert.ToDecimal(row["CantidadAsistentes"]);
                    this.txtMotivo.Text = row["Motivo"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CargarAmbientes()
        {
            string query = "SELECT IdAmbiente, Codigo, CapacidadMaxima FROM Ambiente WHERE Estado = 'Disponible' ORDER BY Codigo ASC";
            return Helpers.EjecutarQuery(query);
        }

        private DataTable CargarUsuariosActivos()
        {
            string query = "SELECT IdUsuario, NombreCompleto FROM Usuario WHERE Estado = 'Activo' ORDER BY NombreCompleto ASC";
            return Helpers.EjecutarQuery(query);
        }

        #region Eventos de Validación

        private void OnCampoCambiado(object sender, EventArgs e)
        {
            ActualizarInformacion();
        }

        private void OnHoraCambiada(object sender, EventArgs e)
        {
            ActualizarInformacion();
            ValidarHorario();
        }

        private void OnAsistentesCambiados(object sender, EventArgs e)
        {
            if (this.cmbAmbiente.SelectedValue != null)
            {
                int idAmbiente = (int)this.cmbAmbiente.SelectedValue;
                int capacidad = ObtenerCapacidadAmbiente(idAmbiente);
                int asistentes = (int)this.nudAsistentes.Value;

                if (asistentes > capacidad)
                {
                    this.lblEstado.Text = "⚠️ Excede capacidad máxima";
                    this.lblEstado.ForeColor = Color.Red;
                }
                else if (asistentes >= 1)
                {
                    this.lblEstado.Text = "✅ Capacidad adecuada";
                    this.lblEstado.ForeColor = Color.Green;
                }
            }
        }

        private void ActualizarInformacion()
        {
            if (this.cmbAmbiente.SelectedValue == null)
            {
                this.lblCapacidad.Text = "Capacidad: -- personas";
                this.lblEstado.Text = "Estado: Seleccione un ambiente";
                this.lbHorariosOcupados.Items.Clear();
                return;
            }

            int idAmbiente = (int)this.cmbAmbiente.SelectedValue;
            int capacidad = ObtenerCapacidadAmbiente(idAmbiente);
            this.lblCapacidad.Text = $"Capacidad: {capacidad} personas";
            this.nudAsistentes.Maximum = capacidad;

            DateTime fecha = this.dtpFecha.Value;
            TimeSpan horaInicio = this.dtpHoraInicio.Value.TimeOfDay;
            TimeSpan horaFin = this.dtpHoraFin.Value.TimeOfDay;

            // Validar disponibilidad
            if (horaInicio < horaFin && fecha.Date >= DateTime.Today)
            {
                bool disponible = ValidarDisponibilidad(idAmbiente, fecha, horaInicio, horaFin);
                this.lblEstado.Text = disponible ? "✅ Disponible" : "❌ Ocupado en este horario";
                this.lblEstado.ForeColor = disponible ? Color.Green : Color.Red;
            }
            else
            {
                this.lblEstado.Text = "⚠️ Horario inválido";
                this.lblEstado.ForeColor = Color.Orange;
            }

            // Mostrar horarios ocupados
            MostrarHorariosOcupados(idAmbiente, fecha);
        }

        private void ValidarHorario()
        {
            TimeSpan inicio = this.dtpHoraInicio.Value.TimeOfDay;
            TimeSpan fin = this.dtpHoraFin.Value.TimeOfDay;
            TimeSpan minHora = new TimeSpan(7, 0, 0);
            TimeSpan maxHora = new TimeSpan(22, 0, 0);

            if (inicio < minHora || inicio > maxHora || fin < minHora || fin > maxHora)
            {
                this.lblEstado.Text = "⚠️ Horario permitido: 7:00 - 22:00";
                this.lblEstado.ForeColor = Color.Orange;
                return;
            }

            if (inicio >= fin)
            {
                this.lblEstado.Text = "⚠️ Hora Inicio debe ser menor a Hora Fin";
                this.lblEstado.ForeColor = Color.Orange;
                return;
            }
        }

        private void MostrarHorariosOcupados(int idAmbiente, DateTime fecha)
        {
            try
            {
                string query = @"SELECT HoraInicio, HoraFin 
                                FROM Reserva 
                                WHERE IdAmbiente = @idAmbiente 
                                  AND Fecha = @fecha
                                  AND Estado != 'Cancelada'";

                var parameters = new Dictionary<string, object>
                {
                    { "@idAmbiente", idAmbiente },
                    { "@fecha", fecha.Date }
                };

                if (ReservaId.HasValue)
                {
                    query += " AND IdReserva != @idReservaExcluir";
                    parameters.Add("@idReservaExcluir", ReservaId.Value);
                }

                query += " ORDER BY HoraInicio ASC";
                DataTable horarios = Helpers.EjecutarQuery(query, parameters);

                this.lbHorariosOcupados.Items.Clear();
                foreach (DataRow row in horarios.Rows)
                {
                    TimeSpan inicio = (TimeSpan)row["HoraInicio"];
                    TimeSpan fin = (TimeSpan)row["HoraFin"];
                    this.lbHorariosOcupados.Items.Add($"• {inicio:hh\\:mm} - {fin:hh\\:mm}");
                }

                if (this.lbHorariosOcupados.Items.Count == 0)
                {
                    this.lbHorariosOcupados.Items.Add("No hay reservas en este día");
                }
            }
            catch
            {
                this.lbHorariosOcupados.Items.Clear();
                this.lbHorariosOcupados.Items.Add("Error al cargar horarios");
            }
        }

        #endregion

        #region Métodos de Validación

        private bool ValidarDisponibilidad(int idAmbiente, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            try
            {
                string query = @"SELECT COUNT(*) 
                                FROM Reserva 
                                WHERE IdAmbiente = @idAmbiente 
                                  AND Fecha = @fecha
                                  AND Estado != 'Cancelada'
                                  AND ((HoraInicio < @horaFin AND HoraFin > @horaInicio))";

                var parameters = new Dictionary<string, object>
                {
                    { "@idAmbiente", idAmbiente },
                    { "@fecha", fecha.Date },
                    { "@horaInicio", horaInicio },
                    { "@horaFin", horaFin }
                };

                if (ReservaId.HasValue)
                {
                    query += " AND IdReserva != @idReservaExcluir";
                    parameters.Add("@idReservaExcluir", ReservaId.Value);
                }

                int count = Helpers.ObtenerEscalar<int>(query, parameters);
                return count == 0;
            }
            catch
            {
                return false;
            }
        }

        private int ObtenerCapacidadAmbiente(int idAmbiente)
        {
            try
            {
                string query = "SELECT CapacidadMaxima FROM Ambiente WHERE IdAmbiente = @idAmbiente";
                var parameters = new Dictionary<string, object> { { "@idAmbiente", idAmbiente } };
                return Helpers.ObtenerEscalar<int>(query, parameters);
            }
            catch
            {
                return 0;
            }
        }

        private bool ValidarFormulario()
        {
            // Validar selecciones
            if (this.cmbAmbiente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un ambiente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cmbAmbiente.Focus();
                return false;
            }

            if (this.cmbUsuario.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cmbUsuario.Focus();
                return false;
            }

            // Validar fecha
            if (this.dtpFecha.Value.Date < DateTime.Today)
            {
                MessageBox.Show("No se pueden crear reservas en días pasados.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.dtpFecha.Focus();
                return false;
            }

            // Validar horas
            TimeSpan inicio = this.dtpHoraInicio.Value.TimeOfDay;
            TimeSpan fin = this.dtpHoraFin.Value.TimeOfDay;
            TimeSpan minHora = new TimeSpan(7, 0, 0);
            TimeSpan maxHora = new TimeSpan(22, 0, 0);

            if (inicio < minHora || inicio > maxHora || fin < minHora || fin > maxHora)
            {
                MessageBox.Show("Horario permitido: 7:00 AM - 10:00 PM.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.dtpHoraInicio.Focus();
                return false;
            }

            if (inicio >= fin)
            {
                MessageBox.Show("La hora de inicio debe ser menor a la hora de fin.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.dtpHoraInicio.Focus();
                return false;
            }

            // Validar capacidad
            int idAmbiente = (int)this.cmbAmbiente.SelectedValue;
            int capacidad = ObtenerCapacidadAmbiente(idAmbiente);
            int asistentes = (int)this.nudAsistentes.Value;

            if (asistentes > capacidad)
            {
                MessageBox.Show($"La cantidad de asistentes ({asistentes}) excede la capacidad máxima del ambiente ({capacidad}).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nudAsistentes.Focus();
                return false;
            }

            if (asistentes < 1)
            {
                MessageBox.Show("La cantidad de asistentes debe ser al menos 1.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nudAsistentes.Focus();
                return false;
            }

            // Validar disponibilidad
            if (!ValidarDisponibilidad(idAmbiente, this.dtpFecha.Value.Date, inicio, fin))
            {
                MessageBox.Show("El ambiente no está disponible en el horario seleccionado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar motivo
            if (string.IsNullOrWhiteSpace(this.txtMotivo.Text))
            {
                MessageBox.Show("Ingrese un motivo para la reserva.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtMotivo.Focus();
                return false;
            }

            if (this.txtMotivo.Text.Length > 150)
            {
                MessageBox.Show("El motivo no puede exceder los 150 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtMotivo.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Guardar

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                try
                {
                    int idAmbiente = (int)this.cmbAmbiente.SelectedValue;
                    int idUsuario = (int)this.cmbUsuario.SelectedValue;
                    DateTime fecha = this.dtpFecha.Value.Date;
                    TimeSpan horaInicio = this.dtpHoraInicio.Value.TimeOfDay;
                    TimeSpan horaFin = this.dtpHoraFin.Value.TimeOfDay;
                    int cantidadAsistentes = (int)this.nudAsistentes.Value;
                    string motivo = this.txtMotivo.Text.Trim();

                    if (ReservaId.HasValue)
                    {
                        // Actualizar
                        string query = @"UPDATE Reserva 
                                        SET IdUsuario = @idUsuario,
                                            IdAmbiente = @idAmbiente,
                                            Fecha = @fecha,
                                            HoraInicio = @horaInicio,
                                            HoraFin = @horaFin,
                                            CantidadAsistentes = @cantidadAsistentes,
                                            Motivo = @motivo
                                        WHERE IdReserva = @idReserva";

                        var parameters = new Dictionary<string, object>
                        {
                            { "@idReserva", ReservaId.Value },
                            { "@idUsuario", idUsuario },
                            { "@idAmbiente", idAmbiente },
                            { "@fecha", fecha },
                            { "@horaInicio", horaInicio },
                            { "@horaFin", horaFin },
                            { "@cantidadAsistentes", cantidadAsistentes },
                            { "@motivo", motivo }
                        };

                        Helpers.EjecutarNonQuery(query, parameters);
                        MessageBox.Show("Reserva actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Crear nueva
                        string query = @"INSERT INTO Reserva (IdUsuario, IdAmbiente, Fecha, HoraInicio, HoraFin, CantidadAsistentes, Motivo, Estado)
                                        VALUES (@idUsuario, @idAmbiente, @fecha, @horaInicio, @horaFin, @cantidadAsistentes, @motivo, 'Activa')";

                        var parameters = new Dictionary<string, object>
                        {
                            { "@idUsuario", idUsuario },
                            { "@idAmbiente", idAmbiente },
                            { "@fecha", fecha },
                            { "@horaInicio", horaInicio },
                            { "@horaFin", horaFin },
                            { "@cantidadAsistentes", cantidadAsistentes },
                            { "@motivo", motivo }
                        };

                        Helpers.EjecutarNonQuery(query, parameters);
                        MessageBox.Show("Reserva creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}