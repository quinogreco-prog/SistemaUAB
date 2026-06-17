using System;
using System.Data;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class FormUsuario : Form
    {
        // Propiedades públicas para acceder a los datos desde fuera
        public string NombreCompleto => txtNombre.Text.Trim();
        public string NombreUsuario => txtUsuario.Text.Trim();
        public string Contrasena => txtPass.Text;
        public string TipoUsuario => cmbTipo.SelectedItem?.ToString();
        public string Estado => cmbEstado.SelectedItem?.ToString();

        // Modo de operación
        private bool isEditMode = false;
        private int? idUsuario = null;

        // Constructor para NUEVO usuario
        public FormUsuario()
        {
            InitializeComponent();
            this.Text = "Nuevo Usuario";
            cmbTipo.SelectedIndex = 1; // "Usuario" por defecto
            cmbEstado.SelectedIndex = 0; // "Activo" por defecto
            cmbEstado.Enabled = false; // Los nuevos usuarios siempre empiezan activos
            isEditMode = false;
        }

        // Constructor para EDITAR usuario
        public FormUsuario(int id)
        {
            InitializeComponent();
            this.Text = "Editar Usuario";
            this.idUsuario = id;
            isEditMode = true;
            cmbEstado.Enabled = true; // En edición se puede cambiar el estado
            CargarDatosUsuario(id);
        }

        private void CargarDatosUsuario(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        NombreCompleto,
                        NombreUsuario,
                        TipoUsuario,
                        Estado
                    FROM Usuario 
                    WHERE IdUsuario = @IdUsuario";

                var parametros = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@IdUsuario", id }
                };

                DataTable dt = Helpers.EjecutarQuery(query, parametros);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtNombre.Text = row["NombreCompleto"].ToString();
                    txtUsuario.Text = row["NombreUsuario"].ToString();

                    // Seleccionar el Tipo en el ComboBox
                    string tipo = row["TipoUsuario"].ToString();
                    if (cmbTipo.Items.Contains(tipo))
                        cmbTipo.SelectedItem = tipo;
                    else
                        cmbTipo.SelectedIndex = 1; // Default a "Usuario"

                    // Seleccionar el Estado en el ComboBox
                    string estado = row["Estado"].ToString();
                    if (cmbEstado.Items.Contains(estado))
                        cmbEstado.SelectedItem = estado;
                    else
                        cmbEstado.SelectedIndex = 0; // Default a "Activo"

                    // En modo edición, la contraseña es opcional
                    txtPass.Text = "";
                    txtPass.PasswordChar = '*';
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (!ValidarCampos())
                    return;

                if (isEditMode)
                {
                    // ACTUALIZAR usuario existente
                    ActualizarUsuario();
                }
                else
                {
                    // CREAR nuevo usuario
                    CrearUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre completo es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario es requerido.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            // Solo validar contraseña si es nuevo o si se ingresó una nueva en edición
            if (!isEditMode && string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("La contraseña es requerida.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return false;
            }

            // Verificar unicidad del nombre de usuario
            int idExcluir = idUsuario ?? 0;
            if (!Validaciones.ValidarUsuarioUnico(txtUsuario.Text.Trim(), idExcluir))
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor, elija otro.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                txtUsuario.SelectAll();
                return false;
            }

            return true;
        }

        private void CrearUsuario()
        {
            string query = @"
                INSERT INTO Usuario (NombreCompleto, NombreUsuario, Contrasena, TipoUsuario, Estado)
                VALUES (@Nombre, @Usuario, @Pass, @Tipo, @Estado)";

            var parametros = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@Nombre", txtNombre.Text.Trim() },
                { "@Usuario", txtUsuario.Text.Trim() },
                { "@Pass", txtPass.Text },
                { "@Tipo", cmbTipo.SelectedItem.ToString() },
                { "@Estado", "Activo" } // Siempre activo al crear
            };

            int resultado = Helpers.EjecutarNonQuery(query, parametros);

            if (resultado > 0)
            {
                MessageBox.Show("Usuario creado exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear el usuario.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarUsuario()
        {
            // Construir la consulta dinámicamente según si se cambió la contraseña
            string query = @"
                UPDATE Usuario 
                SET NombreCompleto = @Nombre, 
                    NombreUsuario = @Usuario, 
                    TipoUsuario = @Tipo,
                    Estado = @Estado";

            var parametros = new System.Collections.Generic.Dictionary<string, object>
            {
                { "@Nombre", txtNombre.Text.Trim() },
                { "@Usuario", txtUsuario.Text.Trim() },
                { "@Tipo", cmbTipo.SelectedItem.ToString() },
                { "@Estado", cmbEstado.SelectedItem.ToString() },
                { "@Id", idUsuario.Value }
            };

            // Si se ingresó una nueva contraseña, actualizarla
            if (!string.IsNullOrWhiteSpace(txtPass.Text))
            {
                query += ", Contrasena = @Pass";
                parametros.Add("@Pass", txtPass.Text);
            }

            query += " WHERE IdUsuario = @Id";

            int resultado = Helpers.EjecutarNonQuery(query, parametros);

            if (resultado > 0)
            {
                MessageBox.Show("Usuario actualizado exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Propiedad para saber si se editó correctamente
        public bool IsSaved { get; private set; } = false;
    }
}