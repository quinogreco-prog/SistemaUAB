using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class ReservasControl : UserControl
    {
        private DataTable reservasDataTable;
        private int? usuarioSeleccionadoId = null;
        private Timer buscarTimer;
        private string busquedaActual = "";

        public ReservasControl()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            ConfigurarPlaceholders();
            CargarComboUsuarios();
            CargarReservas(DateTime.Today);
        }

        #region Configuración Inicial

        private void ConfigurarDataGridView()
        {
            dgvReservas.Columns.Clear();

            // Columna ID (oculta)
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "colId";
            colId.HeaderText = "ID";
            colId.Visible = false;
            colId.DataPropertyName = "IdReserva";
            dgvReservas.Columns.Add(colId);

            // Columna Número
            DataGridViewTextBoxColumn colNumero = new DataGridViewTextBoxColumn();
            colNumero.Name = "colNumero";
            colNumero.HeaderText = "#";
            colNumero.Width = 50;
            colNumero.DataPropertyName = "Numero";
            colNumero.ReadOnly = true;
            dgvReservas.Columns.Add(colNumero);

            // Columna Ambiente
            DataGridViewTextBoxColumn colAmbiente = new DataGridViewTextBoxColumn();
            colAmbiente.Name = "colAmbiente";
            colAmbiente.HeaderText = "Ambiente";
            colAmbiente.Width = 120;
            colAmbiente.DataPropertyName = "Ambiente";
            colAmbiente.ReadOnly = true;
            dgvReservas.Columns.Add(colAmbiente);

            // Columna Usuario
            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn();
            colUsuario.Name = "colUsuario";
            colUsuario.HeaderText = "Usuario";
            colUsuario.Width = 180;
            colUsuario.DataPropertyName = "Usuario";
            colUsuario.ReadOnly = true;
            dgvReservas.Columns.Add(colUsuario);

            // Columna Fecha
            DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
            colFecha.Name = "colFecha";
            colFecha.HeaderText = "Fecha";
            colFecha.Width = 100;
            colFecha.DataPropertyName = "Fecha";
            colFecha.ReadOnly = true;
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvReservas.Columns.Add(colFecha);

            // Columna Hora Inicio
            DataGridViewTextBoxColumn colHoraInicio = new DataGridViewTextBoxColumn();
            colHoraInicio.Name = "colHoraInicio";
            colHoraInicio.HeaderText = "Hora Inicio";
            colHoraInicio.Width = 100;
            colHoraInicio.DataPropertyName = "HoraInicio";
            colHoraInicio.ReadOnly = true;
            colHoraInicio.DefaultCellStyle.Format = "HH:mm";
            dgvReservas.Columns.Add(colHoraInicio);

            // Columna Hora Fin
            DataGridViewTextBoxColumn colHoraFin = new DataGridViewTextBoxColumn();
            colHoraFin.Name = "colHoraFin";
            colHoraFin.HeaderText = "Hora Fin";
            colHoraFin.Width = 100;
            colHoraFin.DataPropertyName = "HoraFin";
            colHoraFin.ReadOnly = true;
            colHoraFin.DefaultCellStyle.Format = "HH:mm";
            dgvReservas.Columns.Add(colHoraFin);

            // Columna Estado
            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
            colEstado.Name = "colEstado";
            colEstado.HeaderText = "Estado";
            colEstado.Width = 120;
            colEstado.DataPropertyName = "Estado";
            colEstado.ReadOnly = true;
            dgvReservas.Columns.Add(colEstado);

            // Columna Motivo
            DataGridViewTextBoxColumn colMotivo = new DataGridViewTextBoxColumn();
            colMotivo.Name = "colMotivo";
            colMotivo.HeaderText = "Motivo";
            colMotivo.Width = 200;
            colMotivo.DataPropertyName = "Motivo";
            colMotivo.ReadOnly = true;
            dgvReservas.Columns.Add(colMotivo);

            // Configurar el DataGridView
            dgvReservas.CellFormatting += dgvReservas_CellFormatting;
            dgvReservas.CellToolTipTextNeeded += dgvReservas_CellToolTipTextNeeded;
        }

        private void ConfigurarPlaceholders()
        {
            txtBuscar.Text = "Buscar por ambiente, usuario o motivo...";
            txtBuscar.ForeColor = Color.Gray;
            txtBuscar.GotFocus += (s, e) =>
            {
                if (txtBuscar.Text == "Buscar por ambiente, usuario o motivo...")
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.Black;
                }
            };
            txtBuscar.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = "Buscar por ambiente, usuario o motivo...";
                    txtBuscar.ForeColor = Color.Gray;
                }
            };
        }

        private void CargarComboUsuarios()
        {
            try
            {
                string query = @"SELECT IdUsuario, NombreCompleto 
                                FROM Usuario 
                                WHERE Estado = 'Activo' 
                                ORDER BY NombreCompleto ASC";

                DataTable usuarios = Helpers.EjecutarQuery(query);
                cmbUsuarios.DataSource = usuarios;
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "IdUsuario";
                cmbUsuarios.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos de Carga

        private void CargarReservas(DateTime fecha)
        {
            MostrarCarga(true);
            try
            {
                string query = @"SELECT 
                                    r.IdReserva,
                                    a.Codigo AS Ambiente,
                                    u.NombreCompleto AS Usuario,
                                    r.Fecha,
                                    r.HoraInicio,
                                    r.HoraFin,
                                    r.Estado,
                                    r.CantidadAsistentes,
                                    r.Motivo
                                FROM Reserva r
                                INNER JOIN Ambiente a ON r.IdAmbiente = a.IdAmbiente
                                INNER JOIN Usuario u ON r.IdUsuario = u.IdUsuario
                                WHERE r.Fecha = @fecha
                                ORDER BY r.HoraInicio ASC";

                var parameters = new Dictionary<string, object>
                {
                    { "@fecha", fecha.Date }
                };

                reservasDataTable = Helpers.EjecutarQuery(query, parameters);

                // Agregar columna de número de fila
                if (reservasDataTable.Columns["Numero"] == null)
                {
                    reservasDataTable.Columns.Add("Numero", typeof(int));
                }

                for (int i = 0; i < reservasDataTable.Rows.Count; i++)
                {
                    reservasDataTable.Rows[i]["Numero"] = i + 1;
                }

                dgvReservas.DataSource = reservasDataTable;
                AplicarColoresEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCarga(false);
            }
        }

        private void BuscarReservas()
        {
            if (string.IsNullOrWhiteSpace(busquedaActual) || busquedaActual == "Buscar por ambiente, usuario o motivo...")
            {
                CargarReservas(dtpFecha.Value);
                return;
            }

            MostrarCarga(true);
            try
            {
                string query = @"SELECT 
                                    r.IdReserva,
                                    a.Codigo AS Ambiente,
                                    u.NombreCompleto AS Usuario,
                                    r.Fecha,
                                    r.HoraInicio,
                                    r.HoraFin,
                                    r.Estado,
                                    r.CantidadAsistentes,
                                    r.Motivo
                                FROM Reserva r
                                INNER JOIN Ambiente a ON r.IdAmbiente = a.IdAmbiente
                                INNER JOIN Usuario u ON r.IdUsuario = u.IdUsuario
                                WHERE r.Fecha = @fecha
                                  AND (
                                      a.Codigo LIKE @busqueda 
                                      OR u.NombreCompleto LIKE @busqueda 
                                      OR r.Motivo LIKE @busqueda
                                  )
                                ORDER BY r.HoraInicio ASC";

                var parameters = new Dictionary<string, object>
                {
                    { "@fecha", dtpFecha.Value.Date },
                    { "@busqueda", $"%{busquedaActual}%" }
                };

                reservasDataTable = Helpers.EjecutarQuery(query, parameters);

                // Agregar columna de número de fila
                if (reservasDataTable.Columns["Numero"] == null)
                {
                    reservasDataTable.Columns.Add("Numero", typeof(int));
                }

                for (int i = 0; i < reservasDataTable.Rows.Count; i++)
                {
                    reservasDataTable.Rows[i]["Numero"] = i + 1;
                }

                dgvReservas.DataSource = reservasDataTable;
                AplicarColoresEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCarga(false);
            }
        }

        private void MostrarCarga(bool mostrar)
        {
            if (panelCarga.InvokeRequired)
            {
                panelCarga.Invoke(new Action(() => MostrarCarga(mostrar)));
                return;
            }

            panelCarga.Visible = mostrar;
            panelCarga.BringToFront();
            Application.DoEvents();
        }

        private void RefrescarDatos()
        {
            if (string.IsNullOrWhiteSpace(busquedaActual) || busquedaActual == "Buscar por ambiente, usuario o motivo...")
            {
                CargarReservas(dtpFecha.Value);
            }
            else
            {
                BuscarReservas();
            }
        }

        private void AplicarColoresEstado()
        {
            foreach (DataGridViewRow row in dgvReservas.Rows)
            {
                if (row.Cells["colEstado"].Value != null)
                {
                    string estado = row.Cells["colEstado"].Value.ToString();
                    switch (estado)
                    {
                        case "Activa":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                            break;
                        case "Cancelada":
                            row.DefaultCellStyle.BackColor = Color.LightPink;
                            row.DefaultCellStyle.ForeColor = Color.DarkRed;
                            break;
                        case "Finalizada":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            break;
                    }
                }
            }
        }

        #endregion

        #region Métodos de Ayuda

        private int? ObtenerIdReservaSeleccionada()
        {
            if (dgvReservas.SelectedRows.Count > 0)
            {
                DataRowView rowView = (DataRowView)dgvReservas.SelectedRows[0].DataBoundItem;
                if (rowView != null && rowView.Row["IdReserva"] != DBNull.Value)
                {
                    return Convert.ToInt32(rowView.Row["IdReserva"]);
                }
            }
            return null;
        }

        private DataRow ObtenerFilaSeleccionada()
        {
            if (dgvReservas.SelectedRows.Count > 0)
            {
                DataRowView rowView = (DataRowView)dgvReservas.SelectedRows[0].DataBoundItem;
                if (rowView != null)
                {
                    return rowView.Row;
                }
            }
            return null;
        }

        #endregion

        #region Eventos de Filtros

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarReservas(dtpFecha.Value);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Cancelar timer anterior si existe
            if (buscarTimer != null)
            {
                buscarTimer.Stop();
                buscarTimer.Dispose();
                buscarTimer = null;
            }

            // Crear nuevo timer para búsqueda después de 500ms
            buscarTimer = new Timer();
            buscarTimer.Interval = 500;
            buscarTimer.Tick += (s, ev) =>
            {
                buscarTimer.Stop();
                buscarTimer.Dispose();
                buscarTimer = null;

                if (txtBuscar.Text == "Buscar por ambiente, usuario o motivo...")
                {
                    busquedaActual = "";
                }
                else
                {
                    busquedaActual = txtBuscar.Text.Trim();
                }
                BuscarReservas();
            };
            buscarTimer.Start();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar por ambiente, usuario o motivo...")
            {
                busquedaActual = "";
            }
            else
            {
                busquedaActual = txtBuscar.Text.Trim();
            }
            BuscarReservas();
        }

        #endregion

        #region Eventos de Acciones

        private void btnNueva_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Por favor, seleccione un usuario para crear la reserva.", "Usuario requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = MostrarDialogoReserva(null);
            if (resultado == DialogResult.OK)
            {
                RefrescarDatos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int? idReserva = ObtenerIdReservaSeleccionada();
            if (idReserva == null)
            {
                MessageBox.Show("Por favor, seleccione una reserva para modificar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = ObtenerFilaSeleccionada();
            if (row != null && row["Estado"].ToString() == "Cancelada")
            {
                MessageBox.Show("No se puede modificar una reserva cancelada.", "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = MostrarDialogoReserva(idReserva);
            if (resultado == DialogResult.OK)
            {
                RefrescarDatos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int? idReserva = ObtenerIdReservaSeleccionada();
            if (idReserva == null)
            {
                MessageBox.Show("Por favor, seleccione una reserva para cancelar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = ObtenerFilaSeleccionada();
            if (row != null && row["Estado"].ToString() == "Cancelada")
            {
                MessageBox.Show("Esta reserva ya está cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Está seguro de cancelar esta reserva?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string query = "UPDATE Reserva SET Estado = 'Cancelada' WHERE IdReserva = @idReserva";
                    var parameters = new Dictionary<string, object> { { "@idReserva", idReserva.Value } };
                    Helpers.EjecutarNonQuery(query, parameters);
                    RefrescarDatos();
                    MessageBox.Show("Reserva cancelada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cancelar reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            int? idReserva = ObtenerIdReservaSeleccionada();
            if (idReserva == null)
            {
                MessageBox.Show("Por favor, seleccione una reserva para ver detalles.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = ObtenerFilaSeleccionada();
            if (row != null)
            {
                string detalles = $"Reserva #{row["Numero"]}\n\n" +
                                 $"Ambiente: {row["Ambiente"]}\n" +
                                 $"Usuario: {row["Usuario"]}\n" +
                                 $"Fecha: {Convert.ToDateTime(row["Fecha"]):dd/MM/yyyy}\n" +
                                 $"Hora Inicio: {row["HoraInicio"]}\n" +
                                 $"Hora Fin: {row["HoraFin"]}\n" +
                                 $"Estado: {row["Estado"]}\n" +
                                 $"Asistentes: {row["CantidadAsistentes"]}\n" +
                                 $"Motivo: {row["Motivo"]}";

                MessageBox.Show(detalles, "Detalles de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Evento de ComboBox

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null && cmbUsuarios.SelectedValue is int)
            {
                usuarioSeleccionadoId = (int)cmbUsuarios.SelectedValue;
            }
            else
            {
                usuarioSeleccionadoId = null;
            }
        }

        #endregion

        #region Eventos del DataGridView

        private void dgvReservas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Formatear hora
            if (e.ColumnIndex == dgvReservas.Columns["colHoraInicio"].Index ||
                e.ColumnIndex == dgvReservas.Columns["colHoraFin"].Index)
            {
                if (e.Value != null && e.Value is TimeSpan)
                {
                    TimeSpan hora = (TimeSpan)e.Value;
                    e.Value = hora.ToString(@"hh\:mm");
                    e.FormattingApplied = true;
                }
            }

            // Formatear fecha
            if (e.ColumnIndex == dgvReservas.Columns["colFecha"].Index)
            {
                if (e.Value != null && e.Value is DateTime)
                {
                    DateTime fecha = (DateTime)e.Value;
                    e.Value = fecha.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvReservas_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex == dgvReservas.Columns["colMotivo"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReservas.Rows[e.RowIndex];
                if (row.Cells["colMotivo"].Value != null)
                {
                    string motivo = row.Cells["colMotivo"].Value.ToString();
                    if (motivo.Length > 30)
                    {
                        e.ToolTipText = motivo;
                    }
                }
            }
        }

        #endregion

        #region Método para Formulario de Diálogo

        private DialogResult MostrarDialogoReserva(int? idReserva = null)
        {
            using (FormReservas dialogo = new FormReservas(idReserva))
            {
                // Pasar el usuario seleccionado si existe
                if (usuarioSeleccionadoId.HasValue)
                {
                    dialogo.UsuarioSeleccionadoId = usuarioSeleccionadoId.Value;
                }

                return dialogo.ShowDialog(this);
            }
        }
        #endregion
    }
}