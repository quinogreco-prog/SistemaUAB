using SistemaUAB.DataLayers;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class PanelControl : UserControl
    {
        // Timer para actualización automática (cada 30 segundos)
        private Timer timerActualizacion;

        public PanelControl()
        {
            InitializeComponent();

            if (!EstaEnModoDisenio())
            {
                InicializarTimer();
                CargarDashboard();
            }
        }

        private bool EstaEnModoDisenio()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   (Site != null && Site.DesignMode);
        }

        /// <summary>
        /// Inicializa el timer para actualización automática
        /// </summary>
        private void InicializarTimer()
        {
            timerActualizacion = new Timer();
            timerActualizacion.Interval = 30000; // 30 segundos
            timerActualizacion.Tick += TimerActualizacion_Tick;
            timerActualizacion.Start();
        }

        /// <summary>
        /// Evento del timer para recargar el dashboard
        /// </summary>
        private void TimerActualizacion_Tick(object sender, EventArgs e)
        {
            // Solo recargar si el control está visible
            if (this.Visible && this.Parent != null)
            {
                CargarDashboard();
            }
        }

        /// <summary>
        /// Método público para recargar el dashboard (usado por el Timer o eventos externos)
        /// </summary>
        public void CargarDashboard()
        {
            if (EstaEnModoDisenio())
                return;

            // Mostrar indicador de carga
            MostrarCarga(true);

            try
            {
                // Cargar estadísticas en paralelo (usando Task para mejor rendimiento)
                Task.Run(() =>
                {
                    try
                    {
                        // Obtener todos los datos en paralelo
                        var totalReservas = ObtenerTotalReservas();
                        var reservasActivas = ObtenerReservasActivas();
                        var ambientesOcupados = ObtenerAmbientesOcupados();
                        var usuariosActivos = ObtenerUsuariosActivos();
                        var proximasReservas = ObtenerProximasReservas();

                        // Actualizar UI en el hilo principal
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                ActualizarTarjetas(totalReservas, reservasActivas, ambientesOcupados, usuariosActivos);
                                ActualizarGridProximasReservas(proximasReservas);
                                MostrarCarga(false);
                            }));
                        }
                        else
                        {
                            ActualizarTarjetas(totalReservas, reservasActivas, ambientesOcupados, usuariosActivos);
                            ActualizarGridProximasReservas(proximasReservas);
                            MostrarCarga(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar error en el hilo secundario
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MostrarCarga(false);
                                MessageBox.Show($"Error al cargar el dashboard: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }
                        else
                        {
                            MostrarCarga(false);
                            MessageBox.Show($"Error al cargar el dashboard: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MostrarCarga(false);
                MessageBox.Show($"Error al iniciar la carga del dashboard: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método público para refrescar el panel (llamado desde UcTarjetaAdmin)
        /// Este método es un alias de CargarDashboard() para mantener compatibilidad
        /// </summary>
        public void RefrescarPanel()
        {
            // Si el timer está activo, lo reiniciamos para evitar múltiples refrescos
            if (timerActualizacion != null)
            {
                timerActualizacion.Stop();
                timerActualizacion.Start();
            }

            // Cargar el dashboard
            CargarDashboard();
        }

        #region Métodos de Consulta a la Base de Datos

        /// <summary>
        /// Obtiene el total de reservas en el sistema
        /// </summary>
        private int ObtenerTotalReservas()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Reserva";
                object resultado = Helpers.ObtenerEscalar(query);
                return resultado != DBNull.Value && resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener total de reservas: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las reservas activas (estado = 'Activa')
        /// </summary>
        private int ObtenerReservasActivas()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Reserva WHERE Estado = 'Activa'";
                object resultado = Helpers.ObtenerEscalar(query);
                return resultado != DBNull.Value && resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener reservas activas: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los ambientes ocupados (con reservas activas para hoy)
        /// </summary>
        private int ObtenerAmbientesOcupados()
        {
            try
            {
                string query = @"
                    SELECT COUNT(DISTINCT r.IdAmbiente) 
                    FROM Reserva r
                    WHERE r.Estado = 'Activa' 
                    AND r.Fecha = CAST(GETDATE() AS DATE)
                    AND CAST(GETDATE() AS TIME) BETWEEN r.HoraInicio AND r.HoraFin";

                object resultado = Helpers.ObtenerEscalar(query);
                return resultado != DBNull.Value && resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ambientes ocupados: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los usuarios activos en el sistema
        /// </summary>
        private int ObtenerUsuariosActivos()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE Estado = 'Activo'";
                object resultado = Helpers.ObtenerEscalar(query);
                return resultado != DBNull.Value && resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios activos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las próximas 5 reservas activas para hoy
        /// </summary>
        private DataTable ObtenerProximasReservas()
        {
            try
            {
                string query = @"
                    SELECT TOP 5 
                        a.Codigo AS Ambiente,
                        FORMAT(r.HoraInicio, 'HH:mm') AS HoraInicio,
                        u.NombreCompleto AS Usuario,
                        r.Estado
                    FROM Reserva r
                    INNER JOIN Ambiente a ON r.IdAmbiente = a.IdAmbiente
                    INNER JOIN Usuario u ON r.IdUsuario = u.IdUsuario
                    WHERE r.Fecha = CAST(GETDATE() AS DATE)
                    AND r.Estado = 'Activa'
                    ORDER BY r.HoraInicio ASC";

                DataTable resultado = Helpers.EjecutarQuery(query);
                return resultado ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener próximas reservas: {ex.Message}");
            }
        }

        #endregion

        #region Métodos de Actualización de UI

        /// <summary>
        /// Actualiza las tarjetas con los valores obtenidos
        /// </summary>
        private void ActualizarTarjetas(int total, int activas, int ocupados, int usuarios)
        {
            // Actualizar cada tarjeta con su valor
            lblTotalReservasNumero.Text = total.ToString("N0");
            lblReservasActivasNumero.Text = activas.ToString("N0");
            lblAmbientesOcupadosNumero.Text = ocupados.ToString("N0");
            lblUsuariosActivosNumero.Text = usuarios.ToString("N0");

            // Cambiar colores según valores (ejemplo)
            ConfigurarColorTarjeta(panelTotalReservas, total > 0 ? Color.LightBlue : Color.LightGray);
            ConfigurarColorTarjeta(panelReservasActivas, activas > 0 ? Color.LightGreen : Color.LightGray);
            ConfigurarColorTarjeta(panelAmbientesOcupados, ocupados > 0 ? Color.LightCoral : Color.LightGray);
            ConfigurarColorTarjeta(panelUsuariosActivos, usuarios > 0 ? Color.LightYellow : Color.LightGray);

            // Actualizar tooltips con información adicional
            // Verificar que toolTip1 no sea null antes de usarlo
            if (toolTip1 != null)
            {
                toolTip1.SetToolTip(lblTotalReservasNumero, $"Total de reservas en el sistema: {total}");
                toolTip1.SetToolTip(lblReservasActivasNumero, $"Reservas activas actualmente: {activas}");
                toolTip1.SetToolTip(lblAmbientesOcupadosNumero, $"Ambientes ocupados en este momento: {ocupados}");
                toolTip1.SetToolTip(lblUsuariosActivosNumero, $"Usuarios activos en el sistema: {usuarios}");
            }
        }

        /// <summary>
        /// Configura el color de fondo de una tarjeta
        /// </summary>
        private void ConfigurarColorTarjeta(Panel panel, Color color)
        {
            panel.BackColor = color;

            // Si el color es muy claro, ajustar el borde para mejor visibilidad
            if (color == Color.LightGray)
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        /// <summary>
        /// Actualiza el DataGridView con las próximas reservas
        /// </summary>
        private void ActualizarGridProximasReservas(DataTable data)
        {
            // Limpiar grid
            dgvProximasReservas.Rows.Clear();

            if (data == null || data.Rows.Count == 0)
            {
                // Mostrar mensaje cuando no hay reservas
                dgvProximasReservas.Rows.Add("No hay reservas para hoy", "", "", "");
                dgvProximasReservas.Rows[0].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Italic);
                dgvProximasReservas.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                dgvProximasReservas.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                return;
            }

            // Llenar grid con los datos
            foreach (DataRow row in data.Rows)
            {
                string ambiente = row["Ambiente"]?.ToString() ?? "";
                string hora = row["HoraInicio"]?.ToString() ?? "";
                string usuario = row["Usuario"]?.ToString() ?? "";
                string estado = row["Estado"]?.ToString() ?? "";

                int rowIndex = dgvProximasReservas.Rows.Add(ambiente, hora, usuario, estado);

                // Color según estado
                if (estado.Equals("Activa", StringComparison.OrdinalIgnoreCase))
                {
                    dgvProximasReservas.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
                else if (estado.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
                {
                    dgvProximasReservas.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (estado.Equals("Finalizada", StringComparison.OrdinalIgnoreCase))
                {
                    dgvProximasReservas.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkBlue;
                }
            }

            // Ajustar altura de filas
            dgvProximasReservas.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            // Seleccionar la primera fila si existe
            if (dgvProximasReservas.Rows.Count > 0)
            {
                dgvProximasReservas.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Muestra u oculta el panel de carga
        /// </summary>
        private void MostrarCarga(bool mostrar)
        {
            // Verificar que los controles existan antes de usarlos
            if (panelCarga != null)
                panelCarga.Visible = mostrar;

            if (tableLayoutTarjetas != null)
                tableLayoutTarjetas.Enabled = !mostrar;

            if (dgvProximasReservas != null)
                dgvProximasReservas.Enabled = !mostrar;

            if (btnRefrescar != null)
                btnRefrescar.Enabled = !mostrar;

            // Cambiar cursor según estado
            this.Cursor = mostrar ? Cursors.WaitCursor : Cursors.Default;
        }

        #endregion

        #region Eventos del Control

        /// <summary>
        /// Evento para recargar manualmente
        /// </summary>
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            RefrescarPanel();
        }

        /// <summary>
        /// Evento cuando el control se está descargando
        /// </summary>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Detener el timer para liberar recursos
            if (timerActualizacion != null)
            {
                timerActualizacion.Stop();
                timerActualizacion.Dispose();
                timerActualizacion = null;
            }
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// Evento cuando el control se está cargando
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Recargar cuando el control se hace visible
            if (!EstaEnModoDisenio())
            {
                CargarDashboard();
            }
        }

        #endregion
    }
}