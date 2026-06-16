namespace SistemaUAB.Presentacion.admin_tools
{
    partial class PanelControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutTarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.panelTotalReservas = new System.Windows.Forms.Panel();
            this.lblTotalReservasNumero = new System.Windows.Forms.Label();
            this.lblTotalReservas = new System.Windows.Forms.Label();
            this.panelReservasActivas = new System.Windows.Forms.Panel();
            this.lblReservasActivasNumero = new System.Windows.Forms.Label();
            this.lblReservasActivas = new System.Windows.Forms.Label();
            this.panelAmbientesOcupados = new System.Windows.Forms.Panel();
            this.lblAmbientesOcupadosNumero = new System.Windows.Forms.Label();
            this.lblAmbientesOcupados = new System.Windows.Forms.Label();
            this.panelUsuariosActivos = new System.Windows.Forms.Panel();
            this.lblUsuariosActivosNumero = new System.Windows.Forms.Label();
            this.lblUsuariosActivos = new System.Windows.Forms.Label();
            this.dgvProximasReservas = new System.Windows.Forms.DataGridView();
            this.lblTituloReservas = new System.Windows.Forms.Label();
            this.panelCarga = new System.Windows.Forms.Panel();
            this.lblCargando = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutTarjetas.SuspendLayout();
            this.panelTotalReservas.SuspendLayout();
            this.panelReservasActivas.SuspendLayout();
            this.panelAmbientesOcupados.SuspendLayout();
            this.panelUsuariosActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasReservas)).BeginInit();
            this.panelCarga.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutTarjetas
            // 
            this.tableLayoutTarjetas.ColumnCount = 4;
            this.tableLayoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTarjetas.Controls.Add(this.panelTotalReservas, 0, 0);
            this.tableLayoutTarjetas.Controls.Add(this.panelReservasActivas, 1, 0);
            this.tableLayoutTarjetas.Controls.Add(this.panelAmbientesOcupados, 2, 0);
            this.tableLayoutTarjetas.Controls.Add(this.panelUsuariosActivos, 3, 0);
            this.tableLayoutTarjetas.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutTarjetas.Name = "tableLayoutTarjetas";
            this.tableLayoutTarjetas.RowCount = 1;
            this.tableLayoutTarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTarjetas.Size = new System.Drawing.Size(680, 120);
            this.tableLayoutTarjetas.TabIndex = 0;
            // 
            // panelTotalReservas
            // 
            this.panelTotalReservas.BackColor = System.Drawing.Color.White;
            this.panelTotalReservas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotalReservas.Controls.Add(this.lblTotalReservasNumero);
            this.panelTotalReservas.Controls.Add(this.lblTotalReservas);
            this.panelTotalReservas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTotalReservas.Location = new System.Drawing.Point(5, 5);
            this.panelTotalReservas.Margin = new System.Windows.Forms.Padding(5);
            this.panelTotalReservas.Name = "panelTotalReservas";
            this.panelTotalReservas.Size = new System.Drawing.Size(160, 110);
            this.panelTotalReservas.TabIndex = 0;
            // 
            // lblTotalReservasNumero
            // 
            this.lblTotalReservasNumero.AutoSize = true;
            this.lblTotalReservasNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReservasNumero.Location = new System.Drawing.Point(10, 45);
            this.lblTotalReservasNumero.Name = "lblTotalReservasNumero";
            this.lblTotalReservasNumero.Size = new System.Drawing.Size(36, 37);
            this.lblTotalReservasNumero.TabIndex = 1;
            this.lblTotalReservasNumero.Text = "0";
            // 
            // lblTotalReservas
            // 
            this.lblTotalReservas.AutoSize = true;
            this.lblTotalReservas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReservas.Location = new System.Drawing.Point(10, 15);
            this.lblTotalReservas.Name = "lblTotalReservas";
            this.lblTotalReservas.Size = new System.Drawing.Size(124, 17);
            this.lblTotalReservas.TabIndex = 0;
            this.lblTotalReservas.Text = "📊 Total Reservas";
            // 
            // panelReservasActivas
            // 
            this.panelReservasActivas.BackColor = System.Drawing.Color.White;
            this.panelReservasActivas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReservasActivas.Controls.Add(this.lblReservasActivasNumero);
            this.panelReservasActivas.Controls.Add(this.lblReservasActivas);
            this.panelReservasActivas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReservasActivas.Location = new System.Drawing.Point(175, 5);
            this.panelReservasActivas.Margin = new System.Windows.Forms.Padding(5);
            this.panelReservasActivas.Name = "panelReservasActivas";
            this.panelReservasActivas.Size = new System.Drawing.Size(160, 110);
            this.panelReservasActivas.TabIndex = 1;
            // 
            // lblReservasActivasNumero
            // 
            this.lblReservasActivasNumero.AutoSize = true;
            this.lblReservasActivasNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservasActivasNumero.Location = new System.Drawing.Point(10, 45);
            this.lblReservasActivasNumero.Name = "lblReservasActivasNumero";
            this.lblReservasActivasNumero.Size = new System.Drawing.Size(36, 37);
            this.lblReservasActivasNumero.TabIndex = 1;
            this.lblReservasActivasNumero.Text = "0";
            // 
            // lblReservasActivas
            // 
            this.lblReservasActivas.AutoSize = true;
            this.lblReservasActivas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservasActivas.Location = new System.Drawing.Point(10, 15);
            this.lblReservasActivas.Name = "lblReservasActivas";
            this.lblReservasActivas.Size = new System.Drawing.Size(137, 17);
            this.lblReservasActivas.TabIndex = 0;
            this.lblReservasActivas.Text = "✅ Reservas Activas";
            // 
            // panelAmbientesOcupados
            // 
            this.panelAmbientesOcupados.BackColor = System.Drawing.Color.White;
            this.panelAmbientesOcupados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAmbientesOcupados.Controls.Add(this.lblAmbientesOcupadosNumero);
            this.panelAmbientesOcupados.Controls.Add(this.lblAmbientesOcupados);
            this.panelAmbientesOcupados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAmbientesOcupados.Location = new System.Drawing.Point(345, 5);
            this.panelAmbientesOcupados.Margin = new System.Windows.Forms.Padding(5);
            this.panelAmbientesOcupados.Name = "panelAmbientesOcupados";
            this.panelAmbientesOcupados.Size = new System.Drawing.Size(160, 110);
            this.panelAmbientesOcupados.TabIndex = 2;
            // 
            // lblAmbientesOcupadosNumero
            // 
            this.lblAmbientesOcupadosNumero.AutoSize = true;
            this.lblAmbientesOcupadosNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmbientesOcupadosNumero.Location = new System.Drawing.Point(10, 45);
            this.lblAmbientesOcupadosNumero.Name = "lblAmbientesOcupadosNumero";
            this.lblAmbientesOcupadosNumero.Size = new System.Drawing.Size(36, 37);
            this.lblAmbientesOcupadosNumero.TabIndex = 1;
            this.lblAmbientesOcupadosNumero.Text = "0";
            // 
            // lblAmbientesOcupados
            // 
            this.lblAmbientesOcupados.AutoSize = true;
            this.lblAmbientesOcupados.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmbientesOcupados.Location = new System.Drawing.Point(10, 15);
            this.lblAmbientesOcupados.Name = "lblAmbientesOcupados";
            this.lblAmbientesOcupados.Size = new System.Drawing.Size(163, 17);
            this.lblAmbientesOcupados.TabIndex = 0;
            this.lblAmbientesOcupados.Text = "🔴 Ambientes Ocupados";
            // 
            // panelUsuariosActivos
            // 
            this.panelUsuariosActivos.BackColor = System.Drawing.Color.White;
            this.panelUsuariosActivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUsuariosActivos.Controls.Add(this.lblUsuariosActivosNumero);
            this.panelUsuariosActivos.Controls.Add(this.lblUsuariosActivos);
            this.panelUsuariosActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUsuariosActivos.Location = new System.Drawing.Point(515, 5);
            this.panelUsuariosActivos.Margin = new System.Windows.Forms.Padding(5);
            this.panelUsuariosActivos.Name = "panelUsuariosActivos";
            this.panelUsuariosActivos.Size = new System.Drawing.Size(160, 110);
            this.panelUsuariosActivos.TabIndex = 3;
            // 
            // lblUsuariosActivosNumero
            // 
            this.lblUsuariosActivosNumero.AutoSize = true;
            this.lblUsuariosActivosNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuariosActivosNumero.Location = new System.Drawing.Point(10, 45);
            this.lblUsuariosActivosNumero.Name = "lblUsuariosActivosNumero";
            this.lblUsuariosActivosNumero.Size = new System.Drawing.Size(36, 37);
            this.lblUsuariosActivosNumero.TabIndex = 1;
            this.lblUsuariosActivosNumero.Text = "0";
            // 
            // lblUsuariosActivos
            // 
            this.lblUsuariosActivos.AutoSize = true;
            this.lblUsuariosActivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuariosActivos.Location = new System.Drawing.Point(10, 15);
            this.lblUsuariosActivos.Name = "lblUsuariosActivos";
            this.lblUsuariosActivos.Size = new System.Drawing.Size(131, 17);
            this.lblUsuariosActivos.TabIndex = 0;
            this.lblUsuariosActivos.Text = "👤 Usuarios Activos";
            // 
            // dgvProximasReservas
            // 
            this.dgvProximasReservas.AllowUserToAddRows = false;
            this.dgvProximasReservas.AllowUserToDeleteRows = false;
            this.dgvProximasReservas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProximasReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProximasReservas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvProximasReservas.Location = new System.Drawing.Point(15, 180);
            this.dgvProximasReservas.Name = "dgvProximasReservas";
            this.dgvProximasReservas.ReadOnly = true;
            this.dgvProximasReservas.Size = new System.Drawing.Size(680, 220);
            this.dgvProximasReservas.TabIndex = 1;
            // 
            // lblTituloReservas
            // 
            this.lblTituloReservas.AutoSize = true;
            this.lblTituloReservas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloReservas.Location = new System.Drawing.Point(15, 150);
            this.lblTituloReservas.Name = "lblTituloReservas";
            this.lblTituloReservas.Size = new System.Drawing.Size(217, 20);
            this.lblTituloReservas.TabIndex = 2;
            this.lblTituloReservas.Text = "📋 Próximas Reservas Hoy";
            // 
            // panelCarga
            // 
            this.panelCarga.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelCarga.Controls.Add(this.lblCargando);
            this.panelCarga.Location = new System.Drawing.Point(0, 0);
            this.panelCarga.Name = "panelCarga";
            this.panelCarga.Size = new System.Drawing.Size(710, 450);
            this.panelCarga.TabIndex = 3;
            this.panelCarga.Visible = false;
            // 
            // lblCargando
            // 
            this.lblCargando.AutoSize = true;
            this.lblCargando.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargando.Location = new System.Drawing.Point(280, 200);
            this.lblCargando.Name = "lblCargando";
            this.lblCargando.Size = new System.Drawing.Size(128, 24);
            this.lblCargando.TabIndex = 0;
            this.lblCargando.Text = "⏳ Cargando...";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(620, 148);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(75, 23);
            this.btnRefrescar.TabIndex = 4;
            this.btnRefrescar.Text = "🔄 Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ambiente";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Hora Inicio";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Usuario";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Estado";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // PanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.dgvProximasReservas);
            this.Controls.Add(this.lblTituloReservas);
            this.Controls.Add(this.tableLayoutTarjetas);
            this.Controls.Add(this.panelCarga);
            this.Name = "PanelControl";
            this.Size = new System.Drawing.Size(710, 450);
            this.tableLayoutTarjetas.ResumeLayout(false);
            this.panelTotalReservas.ResumeLayout(false);
            this.panelTotalReservas.PerformLayout();
            this.panelReservasActivas.ResumeLayout(false);
            this.panelReservasActivas.PerformLayout();
            this.panelAmbientesOcupados.ResumeLayout(false);
            this.panelAmbientesOcupados.PerformLayout();
            this.panelUsuariosActivos.ResumeLayout(false);
            this.panelUsuariosActivos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasReservas)).EndInit();
            this.panelCarga.ResumeLayout(false);
            this.panelCarga.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Declaración de controles UI
        private System.Windows.Forms.TableLayoutPanel tableLayoutTarjetas;
        private System.Windows.Forms.Panel panelTotalReservas;
        private System.Windows.Forms.Panel panelReservasActivas;
        private System.Windows.Forms.Panel panelAmbientesOcupados;
        private System.Windows.Forms.Panel panelUsuariosActivos;
        private System.Windows.Forms.Label lblTotalReservas;
        private System.Windows.Forms.Label lblTotalReservasNumero;
        private System.Windows.Forms.Label lblReservasActivas;
        private System.Windows.Forms.Label lblReservasActivasNumero;
        private System.Windows.Forms.Label lblAmbientesOcupados;
        private System.Windows.Forms.Label lblAmbientesOcupadosNumero;
        private System.Windows.Forms.Label lblUsuariosActivos;
        private System.Windows.Forms.Label lblUsuariosActivosNumero;
        private System.Windows.Forms.DataGridView dgvProximasReservas;
        private System.Windows.Forms.Label lblTituloReservas;
        private System.Windows.Forms.Panel panelCarga;
        private System.Windows.Forms.Label lblCargando;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.ToolTip toolTip1; // ← DECLARADO CORRECTAMENTE
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}