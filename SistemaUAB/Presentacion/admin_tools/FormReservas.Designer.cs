using System.Drawing;
using System.Windows.Forms;

namespace SistemaUAB.Presentacion.admin_tools
{
    partial class FormReservas
    {
        private System.ComponentModel.IContainer components = null;

        // Declarar todos los controles como variables de clase
        private TableLayoutPanel tableLayout;
        private Label lblAmbiente;
        private ComboBox cmbAmbiente;
        private Label lblUsuario;
        private ComboBox cmbUsuario;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblHoraInicio;
        private DateTimePicker dtpHoraInicio;
        private Label lblHoraFin;
        private DateTimePicker dtpHoraFin;
        private Label lblAsistentes;
        private NumericUpDown nudAsistentes;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Panel panelInfo;
        private FlowLayoutPanel flowInfo;
        private Label lblEstado;
        private Label lblCapacidad;
        private Label lblHorariosOcupados;
        private ListBox lbHorariosOcupados;
        private FlowLayoutPanel flowButtons;
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblAmbiente = new System.Windows.Forms.Label();
            this.cmbAmbiente = new System.Windows.Forms.ComboBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblHoraInicio = new System.Windows.Forms.Label();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.lblHoraFin = new System.Windows.Forms.Label();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.lblAsistentes = new System.Windows.Forms.Label();
            this.nudAsistentes = new System.Windows.Forms.NumericUpDown();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.flowInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblCapacidad = new System.Windows.Forms.Label();
            this.lblHorariosOcupados = new System.Windows.Forms.Label();
            this.lbHorariosOcupados = new System.Windows.Forms.ListBox();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsistentes)).BeginInit();
            this.panelInfo.SuspendLayout();
            this.flowInfo.SuspendLayout();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.lblAmbiente, 0, 0);
            this.tableLayout.Controls.Add(this.lblUsuario, 0, 1);
            this.tableLayout.Controls.Add(this.lblFecha, 0, 2);
            this.tableLayout.Controls.Add(this.dtpFecha, 1, 2);
            this.tableLayout.Controls.Add(this.lblHoraInicio, 0, 3);
            this.tableLayout.Controls.Add(this.dtpHoraInicio, 1, 3);
            this.tableLayout.Controls.Add(this.lblHoraFin, 0, 4);
            this.tableLayout.Controls.Add(this.dtpHoraFin, 1, 4);
            this.tableLayout.Controls.Add(this.lblAsistentes, 0, 5);
            this.tableLayout.Controls.Add(this.nudAsistentes, 1, 5);
            this.tableLayout.Controls.Add(this.lblMotivo, 0, 6);
            this.tableLayout.Controls.Add(this.panelInfo, 0, 7);
            this.tableLayout.Controls.Add(this.flowButtons, 0, 8);
            this.tableLayout.Controls.Add(this.txtMotivo, 1, 6);
            this.tableLayout.Controls.Add(this.cmbAmbiente, 1, 0);
            this.tableLayout.Controls.Add(this.cmbUsuario, 1, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayout.RowCount = 9;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayout.Size = new System.Drawing.Size(700, 688);
            this.tableLayout.TabIndex = 0;
            // 
            // lblAmbiente
            // 
            this.lblAmbiente.AutoSize = true;
            this.lblAmbiente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAmbiente.Location = new System.Drawing.Point(23, 20);
            this.lblAmbiente.Name = "lblAmbiente";
            this.lblAmbiente.Size = new System.Drawing.Size(101, 28);
            this.lblAmbiente.TabIndex = 0;
            this.lblAmbiente.Text = "Ambiente:";
            // 
            // cmbAmbiente
            // 
            this.cmbAmbiente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAmbiente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAmbiente.Location = new System.Drawing.Point(173, 23);
            this.cmbAmbiente.Name = "cmbAmbiente";
            this.cmbAmbiente.Size = new System.Drawing.Size(503, 36);
            this.cmbAmbiente.TabIndex = 1;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuario.Location = new System.Drawing.Point(23, 65);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(83, 28);
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario:";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUsuario.Location = new System.Drawing.Point(173, 68);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(503, 36);
            this.cmbUsuario.TabIndex = 3;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFecha.Location = new System.Drawing.Point(23, 108);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(66, 28);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(173, 111);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 34);
            this.dtpFecha.TabIndex = 5;
            // 
            // lblHoraInicio
            // 
            this.lblHoraInicio.AutoSize = true;
            this.lblHoraInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHoraInicio.Location = new System.Drawing.Point(23, 152);
            this.lblHoraInicio.Name = "lblHoraInicio";
            this.lblHoraInicio.Size = new System.Drawing.Size(111, 28);
            this.lblHoraInicio.TabIndex = 6;
            this.lblHoraInicio.Text = "Hora Inicio:";
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.Location = new System.Drawing.Point(173, 155);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.ShowUpDown = true;
            this.dtpHoraInicio.Size = new System.Drawing.Size(200, 34);
            this.dtpHoraInicio.TabIndex = 7;
            // 
            // lblHoraFin
            // 
            this.lblHoraFin.AutoSize = true;
            this.lblHoraFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHoraFin.Location = new System.Drawing.Point(23, 239);
            this.lblHoraFin.Name = "lblHoraFin";
            this.lblHoraFin.Size = new System.Drawing.Size(90, 28);
            this.lblHoraFin.TabIndex = 8;
            this.lblHoraFin.Text = "Hora Fin:";
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.Location = new System.Drawing.Point(173, 242);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Size = new System.Drawing.Size(200, 34);
            this.dtpHoraFin.TabIndex = 9;
            // 
            // lblAsistentes
            // 
            this.lblAsistentes.AutoSize = true;
            this.lblAsistentes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAsistentes.Location = new System.Drawing.Point(23, 286);
            this.lblAsistentes.Name = "lblAsistentes";
            this.lblAsistentes.Size = new System.Drawing.Size(103, 28);
            this.lblAsistentes.TabIndex = 10;
            this.lblAsistentes.Text = "Asistentes:";
            // 
            // nudAsistentes
            // 
            this.nudAsistentes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudAsistentes.Location = new System.Drawing.Point(173, 289);
            this.nudAsistentes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAsistentes.Name = "nudAsistentes";
            this.nudAsistentes.Size = new System.Drawing.Size(120, 34);
            this.nudAsistentes.TabIndex = 11;
            this.nudAsistentes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMotivo.Location = new System.Drawing.Point(23, 328);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(80, 28);
            this.lblMotivo.TabIndex = 12;
            this.lblMotivo.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMotivo.Location = new System.Drawing.Point(173, 331);
            this.txtMotivo.MaxLength = 150;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(501, 43);
            this.txtMotivo.TabIndex = 13;
            this.txtMotivo.TextChanged += new System.EventHandler(this.txtMotivo_TextChanged);
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayout.SetColumnSpan(this.panelInfo, 2);
            this.panelInfo.Controls.Add(this.flowInfo);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(23, 380);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(654, 66);
            this.panelInfo.TabIndex = 14;
            // 
            // flowInfo
            // 
            this.flowInfo.Controls.Add(this.lblEstado);
            this.flowInfo.Controls.Add(this.lblCapacidad);
            this.flowInfo.Controls.Add(this.lblHorariosOcupados);
            this.flowInfo.Controls.Add(this.lbHorariosOcupados);
            this.flowInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowInfo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowInfo.Location = new System.Drawing.Point(0, 0);
            this.flowInfo.Name = "flowInfo";
            this.flowInfo.Padding = new System.Windows.Forms.Padding(10);
            this.flowInfo.Size = new System.Drawing.Size(652, 64);
            this.flowInfo.TabIndex = 0;
            // 
            // lblEstado
            // 
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.Green;
            this.lblEstado.Location = new System.Drawing.Point(13, 10);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(100, 23);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Estado: Disponible";
            // 
            // lblCapacidad
            // 
            this.lblCapacidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCapacidad.Location = new System.Drawing.Point(119, 10);
            this.lblCapacidad.Name = "lblCapacidad";
            this.lblCapacidad.Size = new System.Drawing.Size(100, 23);
            this.lblCapacidad.TabIndex = 1;
            this.lblCapacidad.Text = "Capacidad: -- personas";
            // 
            // lblHorariosOcupados
            // 
            this.lblHorariosOcupados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHorariosOcupados.Location = new System.Drawing.Point(225, 10);
            this.lblHorariosOcupados.Name = "lblHorariosOcupados";
            this.lblHorariosOcupados.Size = new System.Drawing.Size(100, 23);
            this.lblHorariosOcupados.TabIndex = 2;
            this.lblHorariosOcupados.Text = "Horarios ocupados:";
            // 
            // lbHorariosOcupados
            // 
            this.lbHorariosOcupados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbHorariosOcupados.ItemHeight = 25;
            this.lbHorariosOcupados.Location = new System.Drawing.Point(331, 13);
            this.lbHorariosOcupados.Name = "lbHorariosOcupados";
            this.lbHorariosOcupados.Size = new System.Drawing.Size(248, 29);
            this.lbHorariosOcupados.TabIndex = 3;
            // 
            // flowButtons
            // 
            this.tableLayout.SetColumnSpan(this.flowButtons, 2);
            this.flowButtons.Controls.Add(this.btnCancelar);
            this.flowButtons.Controls.Add(this.btnGuardar);
            this.flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowButtons.Location = new System.Drawing.Point(23, 452);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowButtons.Size = new System.Drawing.Size(654, 213);
            this.flowButtons.TabIndex = 15;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(531, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 40);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(405, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 40);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // FormReservas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 688);
            this.Controls.Add(this.tableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReservas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormReservas";
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAsistentes)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.flowInfo.ResumeLayout(false);
            this.flowButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}