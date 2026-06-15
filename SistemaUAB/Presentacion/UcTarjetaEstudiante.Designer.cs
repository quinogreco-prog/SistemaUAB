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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ESTUDIANTE = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ESTUDIANTE
            // 
            this.ESTUDIANTE.AutoSize = true;
            this.ESTUDIANTE.Location = new System.Drawing.Point(50, 67);
            this.ESTUDIANTE.Name = "ESTUDIANTE";
            this.ESTUDIANTE.Size = new System.Drawing.Size(69, 16);
            this.ESTUDIANTE.TabIndex = 1;
            this.ESTUDIANTE.Text = "estudiante";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(482, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 131);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // UcTarjetaEstudiante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ESTUDIANTE);
            this.Name = "UcTarjetaEstudiante";
            this.Size = new System.Drawing.Size(753, 482);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ESTUDIANTE;
        private System.Windows.Forms.Button button1;
    }
}
