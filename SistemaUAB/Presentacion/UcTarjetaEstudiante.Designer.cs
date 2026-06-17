namespace SistemaUAB.Presentacion
{
    partial class UcTarjetaEstudiante
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Componentes

        /// <summary> 
        /// Método requerido para soporte del diseñador. No modificar 
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelNav = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnBuscarAmbientes = new System.Windows.Forms.Button();
            this.btnNuevaReserva = new System.Windows.Forms.Button();
            this.btnMisReservas = new System.Windows.Forms.Button();
            this.panelContentEstudiante = new System.Windows.Forms.Panel();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelNav.Controls.Add(this.btnCerrarSesion);
            this.panelNav.Controls.Add(this.btnBuscarAmbientes);
            this.panelNav.Controls.Add(this.btnNuevaReserva);
            this.panelNav.Controls.Add(this.btnMisReservas);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(309, 800);
            this.panelNav.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(57, 610);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(198, 82);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "🚪 Cerrar Sesión";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnBuscarAmbientes
            // 
            this.btnBuscarAmbientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnBuscarAmbientes.FlatAppearance.BorderSize = 0;
            this.btnBuscarAmbientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarAmbientes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAmbientes.ForeColor = System.Drawing.Color.White;
            this.btnBuscarAmbientes.Location = new System.Drawing.Point(39, 122);
            this.btnBuscarAmbientes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscarAmbientes.Name = "btnBuscarAmbientes";
            this.btnBuscarAmbientes.Size = new System.Drawing.Size(309, 82);
            this.btnBuscarAmbientes.TabIndex = 1;
            this.btnBuscarAmbientes.Text = "🔍 Buscar Ambientes";
            this.btnBuscarAmbientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarAmbientes.UseVisualStyleBackColor = false;
            this.btnBuscarAmbientes.Click += new System.EventHandler(this.btnBuscarAmbientes_Click);
            // 
            // btnNuevaReserva
            // 
            this.btnNuevaReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnNuevaReserva.FlatAppearance.BorderSize = 0;
            this.btnNuevaReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaReserva.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaReserva.ForeColor = System.Drawing.Color.White;
            this.btnNuevaReserva.Location = new System.Drawing.Point(39, 212);
            this.btnNuevaReserva.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNuevaReserva.Name = "btnNuevaReserva";
            this.btnNuevaReserva.Size = new System.Drawing.Size(309, 82);
            this.btnNuevaReserva.TabIndex = 2;
            this.btnNuevaReserva.Text = "📅 Nueva Reserva";
            this.btnNuevaReserva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevaReserva.UseVisualStyleBackColor = false;
            this.btnNuevaReserva.Click += new System.EventHandler(this.btnNuevaReserva_Click);
            // 
            // btnMisReservas
            // 
            this.btnMisReservas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnMisReservas.FlatAppearance.BorderSize = 0;
            this.btnMisReservas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisReservas.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMisReservas.ForeColor = System.Drawing.Color.White;
            this.btnMisReservas.Location = new System.Drawing.Point(39, 31);
            this.btnMisReservas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMisReservas.Name = "btnMisReservas";
            this.btnMisReservas.Size = new System.Drawing.Size(309, 82);
            this.btnMisReservas.TabIndex = 0;
            this.btnMisReservas.Text = "📋 Mis Reservas";
            this.btnMisReservas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMisReservas.UseVisualStyleBackColor = false;
            this.btnMisReservas.Click += new System.EventHandler(this.btnMisReservas_Click);
            // 
            // panelContentEstudiante
            // 
            this.panelContentEstudiante.AutoScroll = true;
            this.panelContentEstudiante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelContentEstudiante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentEstudiante.Location = new System.Drawing.Point(309, 0);
            this.panelContentEstudiante.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelContentEstudiante.Name = "panelContentEstudiante";
            this.panelContentEstudiante.Size = new System.Drawing.Size(1141, 800);
            this.panelContentEstudiante.TabIndex = 1;
            // 
            // UcTarjetaEstudiante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContentEstudiante);
            this.Controls.Add(this.panelNav);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UcTarjetaEstudiante";
            this.Size = new System.Drawing.Size(1450, 800);
            this.panelNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button btnMisReservas;
        private System.Windows.Forms.Button btnBuscarAmbientes;
        private System.Windows.Forms.Button btnNuevaReserva;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelContentEstudiante;
    }
}