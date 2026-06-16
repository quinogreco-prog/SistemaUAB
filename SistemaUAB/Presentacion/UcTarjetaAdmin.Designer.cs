namespace SistemaUAB.Presentacion
{
    partial class UcTarjetaAdmin
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Panel lateral de navegación
            this.panelNav = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnReservas = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnPanel = new System.Windows.Forms.Button();
            this.panelContentPestana = new System.Windows.Forms.Panel();

            // panelNav
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelNav.Controls.Add(this.btnCerrarSesion);
            this.panelNav.Controls.Add(this.btnReportes);
            this.panelNav.Controls.Add(this.btnReservas);
            this.panelNav.Controls.Add(this.btnUsuarios);
            this.panelNav.Controls.Add(this.btnPanel);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(200, 600);
            this.panelNav.TabIndex = 0;

            // btnCerrarSesion
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(20, 530);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(160, 40);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);

            // btnReportes
            this.btnReportes.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(20, 170);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(160, 40);
            this.btnReportes.TabIndex = 3;
            this.btnReportes.Text = "📊 Reportes";
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);

            // btnReservas
            this.btnReservas.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnReservas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReservas.ForeColor = System.Drawing.Color.White;
            this.btnReservas.Location = new System.Drawing.Point(20, 130);
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Size = new System.Drawing.Size(160, 40);
            this.btnReservas.TabIndex = 2;
            this.btnReservas.Text = "📅 Reservas";
            this.btnReservas.UseVisualStyleBackColor = false;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);

            // btnUsuarios
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(20, 90);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(160, 40);
            this.btnUsuarios.TabIndex = 1;
            this.btnUsuarios.Text = "👥 Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);

            // btnPanel
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPanel.ForeColor = System.Drawing.Color.White;
            this.btnPanel.Location = new System.Drawing.Point(20, 50);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(160, 40);
            this.btnPanel.TabIndex = 0;
            this.btnPanel.Text = "🏠 Panel";
            this.btnPanel.UseVisualStyleBackColor = false;
            this.btnPanel.Click += new System.EventHandler(this.btnPanel_Click);

            // panelContentPestana
            this.panelContentPestana.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelContentPestana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentPestana.Location = new System.Drawing.Point(200, 0);
            this.panelContentPestana.Name = "panelContentPestana";
            this.panelContentPestana.Size = new System.Drawing.Size(800, 600);
            this.panelContentPestana.TabIndex = 1;
            this.panelContentPestana.Visible = false;

            // UcTarjetaAdmin
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelContentPestana);
            this.Controls.Add(this.panelNav);
            this.Name = "UcTarjetaAdmin";
            this.Size = new System.Drawing.Size(1000, 600);
            this.panelNav.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Controles visuales
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnReservas;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnPanel;
        private System.Windows.Forms.Panel panelContentPestana;
    }
}