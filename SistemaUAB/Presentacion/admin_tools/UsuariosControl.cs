using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaUAB.DataLayers;

namespace SistemaUAB.Presentacion.admin_tools
{
    public partial class UsuariosControl : UserControl
    {
        private DataTable tablaUsuarios;

        public UsuariosControl()
        {
            InitializeComponent();

            // ✅ Asegurar que los controles estén visibles
            this.panelSuperior.Visible = true;
            this.dgvUsuarios.Visible = true;
            this.panelCarga.Visible = false;

            ConfigurarColumnasDataGridView();
            CargarUsuarios();
        }

        #region Configuración del DataGridView

        private void ConfigurarColumnasDataGridView()
        {
            dgvUsuarios.Columns.Clear();

            // CheckBox de selección
            DataGridViewCheckBoxColumn colSeleccion = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Seleccionar",
                Name = "Seleccionar",
                Width = 80,
                FillWeight = 8
            };
            dgvUsuarios.Columns.Add(colSeleccion);

            // ID (oculto)
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                Name = "IdUsuario",
                Visible = false
            };
            dgvUsuarios.Columns.Add(colId);

            // Nombre Completo
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre Completo",
                Name = "NombreCompleto",
                Width = 250,
                FillWeight = 25
            };
            dgvUsuarios.Columns.Add(colNombre);

            // Nombre Usuario
            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre de Usuario",
                Name = "NombreUsuario",
                Width = 180,
                FillWeight = 18
            };
            dgvUsuarios.Columns.Add(colUsuario);

            // Tipo
            DataGridViewTextBoxColumn colTipo = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tipo de Usuario",
                Name = "TipoUsuario",
                Width = 150,
                FillWeight = 15
            };
            dgvUsuarios.Columns.Add(colTipo);

            // Estado
            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                Name = "Estado",
                Width = 120,
                FillWeight = 12
            };
            dgvUsuarios.Columns.Add(colEstado);

            // Contraseña (oculta)
            DataGridViewTextBoxColumn colPass = new DataGridViewTextBoxColumn
            {
                HeaderText = "Contrasena",
                Name = "Contrasena",
                Visible = false
            };
            dgvUsuarios.Columns.Add(colPass);
        }

        #endregion

        #region Carga de Datos

        public void CargarUsuarios()
        {
            try
            {
                MostrarCarga(true);

                string query = @"
                    SELECT 
                        IdUsuario,
                        NombreCompleto,
                        NombreUsuario,
                        TipoUsuario,
                        Estado,
                        Contrasena
                    FROM Usuario
                    ORDER BY Estado DESC, NombreCompleto ASC";

                tablaUsuarios = Helpers.EjecutarQuery(query);

                if (tablaUsuarios == null || tablaUsuarios.Rows.Count == 0)
                {
                    dgvUsuarios.Rows.Clear();
                    dgvUsuarios.Rows.Add(false, 0, "No hay usuarios registrados", "", "", "", "");
                    dgvUsuarios.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvUsuarios.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    CargarDataGridView(tablaUsuarios);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCarga(false);
            }
        }

        private void CargarDataGridView(DataTable data)
        {
            dgvUsuarios.Rows.Clear();

            foreach (DataRow row in data.Rows)
            {
                string estado = row["Estado"].ToString();
                bool isActive = estado.Equals("Activo", StringComparison.OrdinalIgnoreCase);

                int rowIndex = dgvUsuarios.Rows.Add(
                    false,
                    row["IdUsuario"],
                    row["NombreCompleto"],
                    row["NombreUsuario"],
                    row["TipoUsuario"],
                    estado,
                    row["Contrasena"]
                );

                // Colorear según estado
                dgvUsuarios.Rows[rowIndex].DefaultCellStyle.BackColor = isActive ? Color.LightGreen : Color.LightPink;
                dgvUsuarios.Rows[rowIndex].DefaultCellStyle.ForeColor = isActive ? Color.DarkGreen : Color.DarkRed;
            }

            dgvUsuarios.ClearSelection();
        }

        #endregion

        #region Búsqueda

        private void BuscarUsuarios(string filtro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filtro) || filtro == "Buscar por nombre o usuario...")
                {
                    CargarUsuarios();
                    return;
                }

                MostrarCarga(true);

                string query = @"
                    SELECT 
                        IdUsuario,
                        NombreCompleto,
                        NombreUsuario,
                        TipoUsuario,
                        Estado,
                        Contrasena
                    FROM Usuario
                    WHERE NombreCompleto LIKE @filtro 
                       OR NombreUsuario LIKE @filtro
                    ORDER BY Estado DESC, NombreCompleto ASC";

                var parametros = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@filtro", $"%{filtro}%" }
                };

                tablaUsuarios = Helpers.EjecutarQuery(query, parametros);

                if (tablaUsuarios.Rows.Count == 0)
                {
                    dgvUsuarios.Rows.Clear();
                    dgvUsuarios.Rows.Add(false, 0, "No se encontraron resultados", "", "", "", "");
                    dgvUsuarios.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvUsuarios.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    CargarDataGridView(tablaUsuarios);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCarga(false);
            }
        }

        #endregion

        #region CRUD con Diálogos Rápidos

        // ✅ NUEVO USUARIO - Diálogo rápido
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FormUsuario frm = new FormUsuario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    CargarUsuarios();
            }
        }

        // ✅ EDITAR USUARIO - Diálogo rápido con datos cargados
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idUsuario = ObtenerIdUsuarioSeleccionado();
                if (!idUsuario.HasValue)
                {
                    MessageBox.Show("Seleccione un usuario para editar.", "Sin selección",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Usar FormUsuario con el ID del usuario seleccionado
                using (FormUsuario frm = new FormUsuario(idUsuario.Value))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarUsuarios(); // Recargar después de editar
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ DESHABILITAR
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idUsuario = ObtenerIdUsuarioSeleccionado();
                if (!idUsuario.HasValue)
                {
                    MessageBox.Show("Seleccione un usuario.", "Sin selección",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Deshabilitar este usuario?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CambiarEstadoUsuario(idUsuario.Value, "Inactivo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HABILITAR
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idUsuario = ObtenerIdUsuarioSeleccionado();
                if (!idUsuario.HasValue)
                {
                    MessageBox.Show("Seleccione un usuario.", "Sin selección",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Habilitar este usuario?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CambiarEstadoUsuario(idUsuario.Value, "Activo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CambiarEstadoUsuario(int idUsuario, string nuevoEstado)
        {
            try
            {
                string query = "UPDATE Usuario SET Estado = @Estado WHERE IdUsuario = @Id";
                var parametros = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@Estado", nuevoEstado },
                    { "@Id", idUsuario }
                };

                if (Helpers.EjecutarNonQuery(query, parametros) > 0)
                {
                    string mensaje = nuevoEstado == "Activo" ? "habilitado" : "deshabilitado";
                    MessageBox.Show($"Usuario {mensaje} correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos Auxiliares

        private int? ObtenerIdUsuarioSeleccionado()
        {
            if (dgvUsuarios.SelectedRows.Count == 0) return null;

            object value = dgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value;
            if (value == null || value == DBNull.Value) return null;

            return Convert.ToInt32(value);
        }

        private DataRow ObtenerFilaSeleccionada()
        {
            if (tablaUsuarios == null) return null;

            int? id = ObtenerIdUsuarioSeleccionado();
            if (!id.HasValue) return null;

            DataRow[] rows = tablaUsuarios.Select($"IdUsuario = {id.Value}");
            return rows.Length > 0 ? rows[0] : null;
        }

        private void MostrarCarga(bool mostrar)
        {
            panelCarga.Visible = mostrar;
            panelSuperior.Enabled = !mostrar;
            dgvUsuarios.Enabled = !mostrar;
        }

        #endregion

        #region Eventos UI

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar por nombre o usuario...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar por nombre o usuario...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            BuscarUsuarios(filtro);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            txtBuscar.Text = "Buscar por nombre o usuario...";
            txtBuscar.ForeColor = Color.Gray;
        }

        #endregion

        private void panelSuperior_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}