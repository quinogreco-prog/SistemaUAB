using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.estudiante_tools
{
    /// <summary>
    /// Control que permite buscar ambientes disponibles y realizar reservas
    /// </summary>
    public partial class BuscarAmbientesControl : UserControl
    {
        #region Variables Privadas

        private int idUsuarioActual;
        private DataTable resultadosDataTable;
        private const string COLUMN_ACCION = "Accion";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que recibe el ID del usuario
        /// </summary>
        /// <param name="idUsuario">ID del estudiante logueado</param>
        public BuscarAmbientesControl(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActual = idUsuario;

            // Configurar fecha por defecto
            dtpFecha.Value = DateTime.Now.Date;

            // Cargar horas en los ComboBox
            CargarHorasDisponibles();

            // Establecer hora por defecto (08:00 - 10:00)
            cmbHoraInicio.SelectedItem = "08:00";
            cmbHoraFin.SelectedItem = "10:00";
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

                // Generar horas desde 07:00 hasta 21:00 (última hora de inicio permitida)
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
        /// Valida los filtros de búsqueda
        /// </summary>
        private bool ValidarFiltros()
        {
            try
            {
                // Validar fecha
                if (dtpFecha.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("No se puede buscar en fechas pasadas. Seleccione una fecha futura.",
                        "Fecha Inválida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                // Validar horas
                if (string.IsNullOrEmpty(cmbHoraInicio.Text) || string.IsNullOrEmpty(cmbHoraFin.Text))
                {
                    MessageBox.Show("Seleccione un rango de horas válido.",
                        "Horas Inválidas",
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

                // Validar capacidad
                if (numCapacidad.Value <= 0)
                {
                    MessageBox.Show("La capacidad debe ser mayor a 0.",
                        "Capacidad Inválida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar filtros: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Construye la consulta SQL dinámica para buscar ambientes disponibles
        /// </summary>
        private string ConstruirConsulta()
        {
            try
            {
                string query = @"
                    SELECT 
                        a.IdAmbiente,
                        a.Codigo,
                        b.NombreBloque AS Bloque,
                        t.NombreTipo AS TipoAmbiente,
                        a.CapacidadMaxima AS Capacidad,
                        a.TieneComputadoras,
                        a.TieneEnchufes,
                        a.TieneProyector,
                        a.OtrasCaracteristicas,
                        a.Estado AS EstadoAmbiente,
                        CASE 
                            WHEN EXISTS (
                                SELECT 1 
                                FROM Reserva r 
                                WHERE r.IdAmbiente = a.IdAmbiente 
                                AND r.Fecha = @Fecha
                                AND r.Estado != 'Cancelada'
                                AND (
                                    (@HoraInicio < r.HoraFin AND @HoraFin > r.HoraInicio)
                                )
                            ) THEN 'Ocupado'
                            ELSE 'Disponible'
                        END AS EstadoDisponibilidad
                    FROM Ambiente a
                    INNER JOIN Bloque b ON a.IdBloque = b.IdBloque
                    INNER JOIN TipoAmbiente t ON a.IdTipo = t.IdTipo
                    WHERE 
                        a.Estado = 'Disponible'
                        AND a.CapacidadMaxima >= @Capacidad";

                // Agregar filtros de características
                if (chkComputadoras.Checked)
                    query += " AND a.TieneComputadoras = 1";

                if (chkEnchufes.Checked)
                    query += " AND a.TieneEnchufes = 1";

                if (chkProyector.Checked)
                    query += " AND a.TieneProyector = 1";

                // Solo mostrar ambientes disponibles en el horario seleccionado
                query += @"
                    AND NOT EXISTS (
                        SELECT 1 
                        FROM Reserva r 
                        WHERE r.IdAmbiente = a.IdAmbiente 
                        AND r.Fecha = @Fecha
                        AND r.Estado != 'Cancelada'
                        AND (
                            (@HoraInicio < r.HoraFin AND @HoraFin > r.HoraInicio)
                        )
                    )
                    ORDER BY b.NombreBloque, a.Codigo";

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al construir consulta: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Busca ambientes disponibles según los filtros
        /// </summary>
        private void BuscarAmbientes()
        {
            try
            {
                // Validar filtros
                if (!ValidarFiltros())
                    return;

                // Obtener valores de los filtros
                DateTime fecha = dtpFecha.Value.Date;
                TimeSpan horaInicio = TimeSpan.Parse(cmbHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(cmbHoraFin.Text);
                int capacidad = (int)numCapacidad.Value;

                // Construir consulta
                string query = ConstruirConsulta();

                // Preparar parámetros
                var parametros = new Dictionary<string, object>
                {
                    { "@Fecha", fecha },
                    { "@HoraInicio", horaInicio },
                    { "@HoraFin", horaFin },
                    { "@Capacidad", capacidad }
                };

                // Ejecutar consulta
                resultadosDataTable = Helpers.EjecutarQuery(query, parametros);

                // Configurar DataGridView
                ConfigurarDataGridView();

                // Asignar datos
                dgvResultados.DataSource = resultadosDataTable;

                // Mostrar mensaje si no hay resultados
                if (resultadosDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron ambientes disponibles.\n" +
                                  "Sugerencia: Intente con menos capacidad, cambie el horario o verifique las características.",
                        "Sin Resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Agregar columna de acción
                    AgregarColumnaAccion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar ambientes: {ex.Message}",
                    "Error de Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Configura las propiedades visuales del DataGridView
        /// </summary>
        private void ConfigurarDataGridView()
        {
            try
            {
                if (dgvResultados.Columns.Count > 0)
                {
                    // Ocultar columnas internas
                    if (dgvResultados.Columns.Contains("IdAmbiente"))
                        dgvResultados.Columns["IdAmbiente"].Visible = false;
                    if (dgvResultados.Columns.Contains("TieneComputadoras"))
                        dgvResultados.Columns["TieneComputadoras"].Visible = false;
                    if (dgvResultados.Columns.Contains("TieneEnchufes"))
                        dgvResultados.Columns["TieneEnchufes"].Visible = false;
                    if (dgvResultados.Columns.Contains("TieneProyector"))
                        dgvResultados.Columns["TieneProyector"].Visible = false;
                    if (dgvResultados.Columns.Contains("OtrasCaracteristicas"))
                        dgvResultados.Columns["OtrasCaracteristicas"].Visible = false;
                    if (dgvResultados.Columns.Contains("EstadoAmbiente"))
                        dgvResultados.Columns["EstadoAmbiente"].Visible = false;

                    // Renombrar columnas
                    if (dgvResultados.Columns.Contains("Codigo"))
                        dgvResultados.Columns["Codigo"].HeaderText = "Código";
                    if (dgvResultados.Columns.Contains("Bloque"))
                        dgvResultados.Columns["Bloque"].HeaderText = "Bloque";
                    if (dgvResultados.Columns.Contains("TipoAmbiente"))
                        dgvResultados.Columns["TipoAmbiente"].HeaderText = "Tipo";
                    if (dgvResultados.Columns.Contains("Capacidad"))
                        dgvResultados.Columns["Capacidad"].HeaderText = "Capacidad";
                    if (dgvResultados.Columns.Contains("EstadoDisponibilidad"))
                        dgvResultados.Columns["EstadoDisponibilidad"].HeaderText = "Disponibilidad";

                    // Aplicar formato condicional
                    AplicarFormatoCondicional();
                }

                // Eliminar columna de acción si existe para evitar duplicados
                if (dgvResultados.Columns.Contains(COLUMN_ACCION))
                    dgvResultados.Columns.Remove(COLUMN_ACCION);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al configurar DataGridView: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Aplica formato condicional a las filas según disponibilidad
        /// </summary>
        private void AplicarFormatoCondicional()
        {
            if (dgvResultados.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                if (row.Cells["EstadoDisponibilidad"].Value != null)
                {
                    string estado = row.Cells["EstadoDisponibilidad"].Value.ToString();

                    if (estado == "Disponible")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(209, 231, 221);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(0, 70, 30);
                    }
                    else if (estado == "Ocupado")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(245, 204, 204);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(139, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Agrega columna de acción con botón "Reservar"
        /// </summary>
        private void AgregarColumnaAccion()
        {
            try
            {
                if (dgvResultados.Columns.Contains(COLUMN_ACCION))
                    return;

                DataGridViewButtonColumn btnColumna = new DataGridViewButtonColumn();
                btnColumna.Name = COLUMN_ACCION;
                btnColumna.HeaderText = "Acción";
                btnColumna.Text = "📅 Reservar";
                btnColumna.UseColumnTextForButtonValue = true;
                btnColumna.FlatStyle = FlatStyle.Flat;
                btnColumna.DefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                btnColumna.DefaultCellStyle.ForeColor = Color.White;
                btnColumna.Width = 100;

                dgvResultados.Columns.Add(btnColumna);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar columna de acción: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Verifica si una reserva es válida (fecha y hora)
        /// </summary>
        private bool ValidarReserva(int idAmbiente, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, int capacidad)
        {
            try
            {
                // Validar capacidad
                int capacidadDisponible;
                if (!Validaciones.ValidarCapacidad(idAmbiente, capacidad, out capacidadDisponible))
                {
                    MessageBox.Show($"La capacidad solicitada ({capacidad}) excede la capacidad del ambiente ({capacidadDisponible}).",
                        "Capacidad Insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                // Validar horario
                if (!Validaciones.ValidarHorario(fecha, horaInicio, horaFin, idAmbiente))
                {
                    MessageBox.Show("El horario seleccionado no está disponible. El ambiente ya tiene una reserva en ese horario.",
                        "Horario Ocupado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar la reserva: {ex.Message}",
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Realiza la inserción de la reserva en la base de datos
        /// </summary>
        private bool InsertarReserva(int idAmbiente, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, int capacidad, string motivo)
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
                    { "@CantidadAsistentes", capacidad },
                    { "@Motivo", motivo }
                };

                int filasAfectadas = Helpers.EjecutarNonQuery(query, parametros);
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar reserva: {ex.Message}", ex);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento Load del control
        /// </summary>
        private void BuscarAmbientesControl_Load(object sender, EventArgs e)
        {
            // Buscar automáticamente al cargar
            btnBuscar_Click(sender, e);
        }

        /// <summary>
        /// Evento Click del botón Buscar
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarAmbientes();
        }

        /// <summary>
        /// Evento CellClick del DataGridView para manejar el botón de reservar
        /// </summary>
        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que se haya hecho clic en la columna de acción
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                if (dgvResultados.Columns[e.ColumnIndex].Name != COLUMN_ACCION)
                    return;

                // Obtener la fila seleccionada
                DataGridViewRow row = dgvResultados.Rows[e.RowIndex];

                // Validar que la fila tenga datos
                if (row.Cells["IdAmbiente"].Value == null)
                {
                    MessageBox.Show("No se pudo obtener la información del ambiente.",
                        "Error de Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Obtener datos de la reserva
                int idAmbiente = Convert.ToInt32(row.Cells["IdAmbiente"].Value);
                string codigo = row.Cells["Codigo"].Value.ToString();
                DateTime fecha = dtpFecha.Value.Date;
                TimeSpan horaInicio = TimeSpan.Parse(cmbHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(cmbHoraFin.Text);
                int capacidad = (int)numCapacidad.Value;

                // Validar que el ambiente esté disponible
                string estadoDisponibilidad = row.Cells["EstadoDisponibilidad"].Value.ToString();
                if (estadoDisponibilidad != "Disponible")
                {
                    MessageBox.Show("Este ambiente ya no está disponible en el horario seleccionado.",
                        "No Disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    BuscarAmbientes(); // Recargar resultados
                    return;
                }

                // Validar reserva (doble verificación)
                if (!ValidarReserva(idAmbiente, fecha, horaInicio, horaFin, capacidad))
                    return;

                // Confirmar reserva
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Confirmar reserva del ambiente {codigo}?\n" +
                    $"Fecha: {fecha:dd/MM/yyyy}\n" +
                    $"Horario: {horaInicio:hh\\:mm} - {horaFin:hh\\:mm}\n" +
                    $"Capacidad: {capacidad} personas",
                    "Confirmar Reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                // Solicitar motivo de la reserva
                string motivo = InputBox.Show("Motivo de la Reserva",
                    "Ingrese el motivo de la reserva (opcional):",
                    "Reserva de ambiente para estudio");

                // Si el usuario cancela el InputBox, no proceder
                if (motivo == null)
                    return;

                // Si el motivo está vacío, usar uno por defecto
                if (string.IsNullOrWhiteSpace(motivo))
                {
                    motivo = "Reserva de ambiente para estudio";
                }

                // Realizar la reserva
                bool reservaExitosa = InsertarReserva(idAmbiente, fecha, horaInicio, horaFin, capacidad, motivo);

                if (reservaExitosa)
                {
                    MessageBox.Show("¡Reserva realizada con éxito!\n" +
                                  $"Ambiente: {codigo}\n" +
                                  $"Fecha: {fecha:dd/MM/yyyy}\n" +
                                  $"Horario: {horaInicio:hh\\:mm} - {horaFin:hh\\:mm}",
                        "Reserva Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Recargar resultados
                    BuscarAmbientes();
                }
                else
                {
                    MessageBox.Show("No se pudo realizar la reserva. Intente nuevamente.",
                        "Error de Reserva",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la reserva: {ex.Message}",
                    "Error de Reserva",
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