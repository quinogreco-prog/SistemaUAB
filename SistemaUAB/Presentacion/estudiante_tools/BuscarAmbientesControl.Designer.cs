namespace SistemaUAB.Presentacion.estudiante_tools
{
    partial class BuscarAmbientesControl
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
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.chkProyector = new System.Windows.Forms.CheckBox();
            this.chkEnchufes = new System.Windows.Forms.CheckBox();
            this.chkComputadoras = new System.Windows.Forms.CheckBox();
            this.numCapacidad = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbHoraFin = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHoraInicio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelResultados = new System.Windows.Forms.Panel();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).BeginInit();
            this.panelResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelFiltros.Controls.Add(this.btnBuscar);
            this.panelFiltros.Controls.Add(this.chkProyector);
            this.panelFiltros.Controls.Add(this.chkEnchufes);
            this.panelFiltros.Controls.Add(this.chkComputadoras);
            this.panelFiltros.Controls.Add(this.numCapacidad);
            this.panelFiltros.Controls.Add(this.label5);
            this.panelFiltros.Controls.Add(this.cmbHoraFin);
            this.panelFiltros.Controls.Add(this.label4);
            this.panelFiltros.Controls.Add(this.cmbHoraInicio);
            this.panelFiltros.Controls.Add(this.label3);
            this.panelFiltros.Controls.Add(this.dtpFecha);
            this.panelFiltros.Controls.Add(this.label2);
            this.panelFiltros.Controls.Add(this.label1);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1200, 231);
            this.panelFiltros.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(940, 140);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(195, 62);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkProyector
            // 
            this.chkProyector.AutoSize = true;
            this.chkProyector.Location = new System.Drawing.Point(630, 163);
            this.chkProyector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkProyector.Name = "chkProyector";
            this.chkProyector.Size = new System.Drawing.Size(102, 24);
            this.chkProyector.TabIndex = 11;
            this.chkProyector.Text = "Proyector";
            this.chkProyector.UseVisualStyleBackColor = true;
            // 
            // chkEnchufes
            // 
            this.chkEnchufes.AutoSize = true;
            this.chkEnchufes.Location = new System.Drawing.Point(495, 163);
            this.chkEnchufes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkEnchufes.Name = "chkEnchufes";
            this.chkEnchufes.Size = new System.Drawing.Size(103, 24);
            this.chkEnchufes.TabIndex = 10;
            this.chkEnchufes.Text = "Enchufes";
            this.chkEnchufes.UseVisualStyleBackColor = true;
            // 
            // chkComputadoras
            // 
            this.chkComputadoras.AutoSize = true;
            this.chkComputadoras.Location = new System.Drawing.Point(315, 163);
            this.chkComputadoras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkComputadoras.Name = "chkComputadoras";
            this.chkComputadoras.Size = new System.Drawing.Size(140, 24);
            this.chkComputadoras.TabIndex = 9;
            this.chkComputadoras.Text = "Computadoras";
            this.chkComputadoras.UseVisualStyleBackColor = true;
            // 
            // numCapacidad
            // 
            this.numCapacidad.Location = new System.Drawing.Point(150, 160);
            this.numCapacidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numCapacidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCapacidad.Name = "numCapacidad";
            this.numCapacidad.Size = new System.Drawing.Size(120, 26);
            this.numCapacidad.TabIndex = 8;
            this.numCapacidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 163);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Capacidad:";
            // 
            // cmbHoraFin
            // 
            this.cmbHoraFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoraFin.FormattingEnabled = true;
            this.cmbHoraFin.Location = new System.Drawing.Point(855, 92);
            this.cmbHoraFin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbHoraFin.Name = "cmbHoraFin";
            this.cmbHoraFin.Size = new System.Drawing.Size(148, 28);
            this.cmbHoraFin.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(765, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Hora Fin:";
            // 
            // cmbHoraInicio
            // 
            this.cmbHoraInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoraInicio.FormattingEnabled = true;
            this.cmbHoraInicio.Location = new System.Drawing.Point(585, 92);
            this.cmbHoraInicio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbHoraInicio.Name = "cmbHoraInicio";
            this.cmbHoraInicio.Size = new System.Drawing.Size(148, 28);
            this.cmbHoraInicio.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hora Inicio:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(150, 92);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(298, 26);
            this.dtpFecha.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "🔍 Buscar Ambientes";
            // 
            // panelResultados
            // 
            this.panelResultados.Controls.Add(this.dgvResultados);
            this.panelResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultados.Location = new System.Drawing.Point(0, 231);
            this.panelResultados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelResultados.Name = "panelResultados";
            this.panelResultados.Size = new System.Drawing.Size(1200, 538);
            this.panelResultados.TabIndex = 1;
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.BackgroundColor = System.Drawing.Color.White;
            this.dgvResultados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 0);
            this.dgvResultados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvResultados.MultiSelect = false;
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.RowHeadersWidth = 62;
            this.dgvResultados.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultados.Size = new System.Drawing.Size(1200, 538);
            this.dgvResultados.TabIndex = 0;
            this.dgvResultados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellClick);
            // 
            // BuscarAmbientesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelResultados);
            this.Controls.Add(this.panelFiltros);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BuscarAmbientesControl";
            this.Size = new System.Drawing.Size(1200, 769);
            this.Load += new System.EventHandler(this.BuscarAmbientesControl_Load);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).EndInit();
            this.panelResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkProyector;
        private System.Windows.Forms.CheckBox chkEnchufes;
        private System.Windows.Forms.CheckBox chkComputadoras;
        private System.Windows.Forms.NumericUpDown numCapacidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbHoraFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbHoraInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelResultados;
        private System.Windows.Forms.DataGridView dgvResultados;
    }
}