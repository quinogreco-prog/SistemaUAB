using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion
{
    public partial class UcTarjetaAdmin : UserControl
    {
        private string pestanaActual = "";
        private System.Windows.Forms.Timer timerAutoRefresh;

        public UcTarjetaAdmin()
        {
            InitializeComponent();

            if (!EstaEnModoDisenio())
            {
                ConfigurarTimer();
            }
        }

        private bool EstaEnModoDisenio()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   (Site != null && Site.DesignMode);
        }

        /// <summary>
        /// Configura el timer para auto-refresco del dashboard
        /// </summary>
        private void ConfigurarTimer()
        {
            timerAutoRefresh = new System.Windows.Forms.Timer();
            timerAutoRefresh.Interval = 30000; // 30 segundos
            timerAutoRefresh.Tick += TimerAutoRefresh_Tick;
            timerAutoRefresh.Enabled = false; // Deshabilitado por defecto
        }

        /// <summary>
        /// Evento Tick del timer - refresca el panel si está activo
        /// </summary>
        private void TimerAutoRefresh_Tick(object sender, EventArgs e)
        {
            if (pestanaActual == "Panel")
            {
                try
                {
                    // Llamar al método RefrescarPanel() del PanelControl
                    if (panelContentPestana.Controls.Count > 0)
                    {
                        var panelControl = panelContentPestana.Controls[0] as admin_tools.PanelControl;
                        if (panelControl != null)
                        {
                            panelControl.RefrescarPanel();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores silenciosamente para no interrumpir la experiencia
                    System.Diagnostics.Debug.WriteLine($"Error al refrescar Panel: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Evento Click del botón Cerrar Sesión
        /// </summary>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Detener el timer SOLO si existe
                if (timerAutoRefresh != null)
                {
                    timerAutoRefresh.Enabled = false;
                    timerAutoRefresh.Stop();
                }

                // Cerrar el formulario contenedor (Principal)
                Form parentForm = this.FindForm();
                if (parentForm != null && !parentForm.IsDisposed)
                {
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                // Si algo falla, al menos mostramos el error
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click del botón Panel
        /// </summary>
        private void btnPanel_Click(object sender, EventArgs e)
        {
            if (pestanaActual == "Panel")
                ContraerContenedor();
            else
                ExpandirContenedor(new admin_tools.PanelControl(), "Panel");
        }

        /// <summary>
        /// Evento Click del botón Usuarios
        /// </summary>
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (pestanaActual == "Usuarios")
                ContraerContenedor();
            else
                ExpandirContenedor(new admin_tools.UsuariosControl(), "Usuarios");
        }

        /// <summary>
        /// Evento Click del botón Reservas
        /// </summary>
        private void btnReservas_Click(object sender, EventArgs e)
        {
            if (pestanaActual == "Reservas")
                ContraerContenedor();
            else
                ExpandirContenedor(new admin_tools.ReservasControl(), "Reservas");
        }

        /// <summary>
        /// Evento Click del botón Reportes
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (pestanaActual == "Reportes")
                ContraerContenedor();
            else
                ExpandirContenedor(new admin_tools.ReportesControl(), "Reportes");
        }

        /// <summary>
        /// Contrae el contenedor ocultando el panel y limpiando la selección
        /// </summary>
        private void ContraerContenedor()
        {
            // Limpiar el panel
            panelContentPestana.Controls.Clear();
            panelContentPestana.Visible = false;

            // Resetear estado
            pestanaActual = "";

            // Restaurar colores de botones
            RestaurarColoresBotones();

            // Detener el timer
            timerAutoRefresh.Enabled = false;
        }

        /// <summary>
        /// Expande el contenedor mostrando el control seleccionado
        /// </summary>
        private void ExpandirContenedor(UserControl nuevoControl, string nombrePestana)
        {
            // Limpiar y agregar el nuevo control
            panelContentPestana.Controls.Clear();
            nuevoControl.Dock = DockStyle.Fill;
            panelContentPestana.Controls.Add(nuevoControl);
            panelContentPestana.Visible = true;

            // Actualizar pestaña actual
            pestanaActual = nombrePestana;

            // Resaltar el botón activo
            RestaurarColoresBotones();
            switch (nombrePestana)
            {
                case "Panel":
                    btnPanel.BackColor = Color.LightBlue;
                    break;
                case "Usuarios":
                    btnUsuarios.BackColor = Color.LightBlue;
                    break;
                case "Reservas":
                    btnReservas.BackColor = Color.LightBlue;
                    break;
                case "Reportes":
                    btnReportes.BackColor = Color.LightBlue;
                    break;
            }

            // Control del Timer: solo para la pestaña Panel
            timerAutoRefresh.Enabled = (nombrePestana == "Panel");
        }

        /// <summary>
        /// Restaura el color de fondo de todos los botones al color predeterminado del sistema
        /// </summary>
        private void RestaurarColoresBotones()
        {
            btnPanel.BackColor = SystemColors.Control;
            btnUsuarios.BackColor = SystemColors.Control;
            btnReservas.BackColor = SystemColors.Control;
            btnReportes.BackColor = SystemColors.Control;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Detener y liberar el timer
                if (timerAutoRefresh != null)
                {
                    timerAutoRefresh.Enabled = false;
                    timerAutoRefresh.Dispose();
                    timerAutoRefresh = null;
                }

                // Liberar componentes del diseñador
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}