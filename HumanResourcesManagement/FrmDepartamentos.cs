
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmDepartamentos : Form
    {
        private Panel panelEncabezado;
        private Panel panelAcciones;
        private Panel panelBusqueda;

        private Label lblTotal;
        private TextBox txtFiltro;

        private Button btnNuevo;
        private Button btnEditar;
        private Button btnActualizar;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnVolver;

        private DataGridView dgvDepartamentos;

        public FrmDepartamentos()
        {
            InitializeComponent();

            ConstruirInterfaz();
            ConfigurarDataGridView();

            // Evita registrar dos veces el mismo evento Load.
            this.Load -= FrmDepartamentos_Load;
            this.Load += FrmDepartamentos_Load;
        }

        private void FrmDepartamentos_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
        }

        // CONSTRUCCION DE LA INTERFAZ
      

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();

            this.Text = "Gestión de departamentos - AdventureWorks";
            this.Size = new Size(1150, 700);
            this.MinimumSize = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9F);

            // ENCABEZADO
            panelEncabezado = new Panel
            {
                Dock = DockStyle.Top,
                Height = 125,
                BackColor = Color.White
            };

            Label lblTitulo = new Label
            {
                Text = "Gestión de departamentos",
                Location = new Point(30, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 45, 65)
            };

            Label lblDescripcion = new Label
            {
                Text = "Administra las áreas y grupos de trabajo de la organización.",
                Location = new Point(34, 68),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Gray
            };

            lblTotal = new Label
            {
                Text = "Departamentos registrados: 0",
                Location = new Point(34, 98),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 120, 180)
            };

            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(lblTotal);

            // PANEL DE ACCIONES
            panelAcciones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            btnNuevo = CrearBoton(
                "+ Nuevo departamento",
                20, 17, 180,
                Color.FromArgb(40, 145, 95),
                Color.White);

            btnEditar = CrearBoton(
                "Modificar",
                210, 17, 120,
                Color.FromArgb(52, 120, 180),
                Color.White);

            btnActualizar = CrearBoton(
                "Actualizar",
                470, 17, 120,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            btnVolver = CrearBoton(
                "Volver al menú principal",
                925, 17, 195,
                Color.FromArgb(30, 45, 65),
                Color.White);

            btnNuevo.Click += btnNuevo_Click;
            btnEditar.Click += btnEditar_Click;

            btnActualizar.Click += (s, e) =>
                CargarDepartamentos(txtFiltro.Text.Trim());

            btnVolver.Click += btnVolver_Click;

            panelAcciones.Controls.Add(btnNuevo);
            panelAcciones.Controls.Add(btnEditar);
            panelAcciones.Controls.Add(btnActualizar);
            panelAcciones.Controls.Add(btnVolver);

            // PANEL DE BUSQUEDA
            panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.White
            };

            Label lblBuscar = new Label
            {
                Text = "Buscar por nombre o grupo:",
                Location = new Point(25, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 70)
            };

            txtFiltro = new TextBox
            {
                Location = new Point(25, 37),
                Size = new Size(330, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnBuscar = CrearBoton(
                "Buscar",
                365, 34, 100,
                Color.FromArgb(52, 120, 180),
                Color.White);

            btnLimpiar = CrearBoton(
                "Limpiar",
                475, 34, 100,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            btnBuscar.Click += (s, e) =>
                CargarDepartamentos(txtFiltro.Text.Trim());

            btnLimpiar.Click += (s, e) =>
            {
                txtFiltro.Clear();
                CargarDepartamentos();
            };

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            panelBusqueda.Controls.Add(lblBuscar);
            panelBusqueda.Controls.Add(txtFiltro);
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(btnLimpiar);

            // TABLA
            dgvDepartamentos = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(15)
            };

            this.Controls.Add(dgvDepartamentos);
            this.Controls.Add(panelBusqueda);
            this.Controls.Add(panelAcciones);
            this.Controls.Add(panelEncabezado);
        }

        // ==========================================
        // CREAR BOTONES
        // ==========================================

        private Button CrearBoton(
            string texto,
            int x,
            int y,
            int ancho,
            Color fondo,
            Color colorTexto)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 42),
                BackColor = fondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            boton.FlatAppearance.BorderSize = 0;

            return boton;
        }

        // ==========================================
        // CONFIGURACION DE LA TABLA
        // ==========================================

        private void ConfigurarDataGridView()
        {
            dgvDepartamentos.ReadOnly = true;
            dgvDepartamentos.AllowUserToAddRows = false;
            dgvDepartamentos.AllowUserToDeleteRows = false;

            dgvDepartamentos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDepartamentos.MultiSelect = false;

            dgvDepartamentos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvDepartamentos.RowHeadersVisible = false;
            dgvDepartamentos.EnableHeadersVisualStyles = false;

            dgvDepartamentos.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 61, 89);

            dgvDepartamentos.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvDepartamentos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvDepartamentos.ColumnHeadersHeight = 42;
            dgvDepartamentos.RowTemplate.Height = 35;

            dgvDepartamentos.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F);

            dgvDepartamentos.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            dgvDepartamentos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(210, 228, 245);

            dgvDepartamentos.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(30, 45, 65);

            dgvDepartamentos.DataBindingComplete += (s, e) =>
            {
                if (dgvDepartamentos.Columns["Fecha Modificación"] != null)
                {
                    dgvDepartamentos.Columns["Fecha Modificación"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            };

            dgvDepartamentos.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    btnEditar.PerformClick();
            };
        }

        // ==========================================
        // CONSULTAR DEPARTAMENTOS
        // ==========================================

        private void CargarDepartamentos(string filtro = "")
        {
            string consulta = @"
                SELECT
                    DepartmentID AS [ID],
                    Name AS [Nombre Departamento],
                    GroupName AS [Grupo / Área],
                    ModifiedDate AS [Fecha Modificación]
                FROM HumanResources.Department
                WHERE
                    @Filtro = ''
                    OR Name LIKE '%' + @Filtro + '%'
                    OR GroupName LIKE '%' + @Filtro + '%'
                ORDER BY DepartmentID ASC;";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.Add(
                        "@Filtro", SqlDbType.NVarChar, 150
                    ).Value = filtro;

                    DataTable tabla = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }

                    dgvDepartamentos.DataSource = tabla;

                    lblTotal.Text =
                        "Departamentos registrados: " + tabla.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los departamentos.\n\n" +
                    ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // REGISTRAR DEPARTAMENTO
        // ==========================================

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmDepartamentoEditor formulario =
                new FrmDepartamentoEditor())
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    GuardarDepartamento(
                        formulario.NombreDepartamento,
                        formulario.GrupoDepartamento,
                        null);
                }
            }
        }

        // ==========================================
        // MODIFICAR DEPARTAMENTO
        
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDepartamentos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecciona un departamento de la tabla.",
                    "Selección requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int id = Convert.ToInt32(
                dgvDepartamentos.CurrentRow.Cells["ID"].Value);

            string nombre = Convert.ToString(
                dgvDepartamentos.CurrentRow.Cells["Nombre Departamento"].Value);

            string grupo = Convert.ToString(
                dgvDepartamentos.CurrentRow.Cells["Grupo / Área"].Value);

            using (FrmDepartamentoEditor formulario =
                new FrmDepartamentoEditor(nombre, grupo))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    GuardarDepartamento(
                        formulario.NombreDepartamento,
                        formulario.GrupoDepartamento,
                        id);
                }
            }
        }

        // GUARDAR EN SQL SERVER
        private void GuardarDepartamento(
            string nombre,
            string grupo,
            int? id)
        {
            nombre = nombre.Trim();
            grupo = grupo.Trim();

            DialogResult confirmacion = MessageBox.Show(
                id.HasValue
                    ? "¿Confirmas que deseas modificar este departamento?"
                    : "¿Confirmas que deseas registrar este departamento?",
                "Confirmar operación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    // Verificar nombres duplicados
                    string consultaDuplicados = @"
                        SELECT COUNT(*)
                        FROM HumanResources.Department
                        WHERE Name = @Nombre
                        AND (@Id IS NULL OR DepartmentID <> @Id);";

                    using (SqlCommand cmdDuplicados =
                        new SqlCommand(consultaDuplicados, con))
                    {
                        cmdDuplicados.Parameters.Add(
                            "@Nombre", SqlDbType.NVarChar, 50
                        ).Value = nombre;

                        cmdDuplicados.Parameters.Add(
                            "@Id", SqlDbType.SmallInt
                        ).Value = id.HasValue
                            ? (object)id.Value
                            : DBNull.Value;

                        int existentes = Convert.ToInt32(
                            cmdDuplicados.ExecuteScalar());

                        if (existentes > 0)
                        {
                            MessageBox.Show(
                                "Ya existe un departamento con ese nombre.",
                                "Departamento duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    string consulta;

                    if (id.HasValue)
                    {
                        consulta = @"
                            UPDATE HumanResources.Department
                            SET Name = @Nombre,
                                GroupName = @Grupo,
                                ModifiedDate = GETDATE()
                            WHERE DepartmentID = @Id;";
                    }
                    else
                    {
                        consulta = @"
                            INSERT INTO HumanResources.Department
                                (Name, GroupName)
                            VALUES
                                (@Nombre, @Grupo);";
                    }

                    using (SqlCommand cmd = new SqlCommand(consulta, con))
                    {
                        cmd.Parameters.Add(
                            "@Nombre", SqlDbType.NVarChar, 50
                        ).Value = nombre;

                        cmd.Parameters.Add(
                            "@Grupo", SqlDbType.NVarChar, 100
                        ).Value = grupo;

                        if (id.HasValue)
                        {
                            cmd.Parameters.Add(
                                "@Id", SqlDbType.SmallInt
                            ).Value = id.Value;
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    id.HasValue
                        ? "El departamento fue modificado correctamente."
                        : "El departamento fue registrado correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarDepartamentos(txtFiltro.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el departamento.\n\n" +
                    ex.Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        //Boton eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDepartamentos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecciona el departamento que deseas eliminar.",
                    "Selección requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int id = Convert.ToInt32(
                dgvDepartamentos.CurrentRow.Cells["ID"].Value);

            string nombre = Convert.ToString(
                dgvDepartamentos.CurrentRow.Cells["Nombre Departamento"].Value);

            DialogResult confirmacion = MessageBox.Show(
                "¿Estás segura de que deseas eliminar este departamento?\n\n" +
                "Departamento: " + nombre +
                "\n\nEsta operación no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    // Comprobar si el departamento tiene historial de empleados
                    string verificar = @"
                SELECT COUNT(*)
                FROM HumanResources.EmployeeDepartmentHistory
                WHERE DepartmentID = @Id;";

                    using (SqlCommand cmdVerificar =
                        new SqlCommand(verificar, con))
                    {
                        cmdVerificar.Parameters.Add(
                            "@Id", SqlDbType.SmallInt).Value = id;

                        int asignaciones = Convert.ToInt32(
                            cmdVerificar.ExecuteScalar());

                        if (asignaciones > 0)
                        {
                            MessageBox.Show(
                                "No se puede eliminar este departamento porque " +
                                "tiene asignaciones de empleados registradas.\n\n" +
                                "Para conservar el historial laboral, el registro " +
                                "se mantendrá en la base de datos.",
                                "Departamento en uso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    // Eliminar únicamente si no tiene relaciones
                    string eliminar = @"
                DELETE FROM HumanResources.Department
                WHERE DepartmentID = @Id;";

                    using (SqlCommand cmdEliminar =
                        new SqlCommand(eliminar, con))
                    {
                        cmdEliminar.Parameters.Add(
                            "@Id", SqlDbType.SmallInt).Value = id;

                        int filas = cmdEliminar.ExecuteNonQuery();

                        if (filas == 0)
                        {
                            MessageBox.Show(
                                "No se encontró el departamento para eliminar.",
                                "Registro no encontrado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }
                    }
                }

                MessageBox.Show(
                    "El departamento fue eliminado correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarDepartamentos(txtFiltro.Text.Trim());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar el departamento porque existen " +
                    "restricciones o relaciones en la base de datos.\n\n" +
                    ex.Message,
                    "Error de eliminación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al eliminar el departamento.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // REGRESAR AL MENU PRINCIPAL

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Deseas regresar al menú principal?",
                "Volver al menú",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            Form principal = this.Owner;

            if (principal == null || principal.IsDisposed)
            {
                principal = Application.OpenForms["FrmMain"];
            }

            if (principal != null)
            {
                principal.Show();
                principal.Activate();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se encontró el menú principal abierto.",
                    "Error de navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

      
        // FORMULARIO INTERNO PARA REGISTRAR Y EDITAR
    
        private class FrmDepartamentoEditor : Form
        {
            private TextBox txtNombre;
            private TextBox txtGrupo;

            public string NombreDepartamento
            {
                get { return txtNombre.Text.Trim(); }
            }

            public string GrupoDepartamento
            {
                get { return txtGrupo.Text.Trim(); }
            }

            public FrmDepartamentoEditor(
                string nombre = "",
                string grupo = "")
            {
                ConstruirFormulario(nombre, grupo);
            }

            private void ConstruirFormulario(
                string nombre,
                string grupo)
            {
                this.Text = string.IsNullOrWhiteSpace(nombre)
                    ? "Nuevo departamento"
                    : "Modificar departamento";

                this.Size = new Size(500, 350);
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.BackColor = Color.FromArgb(245, 247, 250);
                this.Font = new Font("Segoe UI", 9F);

                Label lblTitulo = new Label
                {
                    Text = string.IsNullOrWhiteSpace(nombre)
                        ? "Registrar departamento"
                        : "Modificar departamento",
                    Location = new Point(25, 20),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 45, 65)
                };

                Label lblDescripcion = new Label
                {
                    Text = "Ingresa la información del área de trabajo.",
                    Location = new Point(28, 60),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };

                Label lblNombre = new Label
                {
                    Text = "Nombre del departamento:",
                    Location = new Point(30, 110),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                txtNombre = new TextBox
                {
                    Location = new Point(30, 135),
                    Size = new Size(420, 30),
                    Font = new Font("Segoe UI", 10F),
                    MaxLength = 50,
                    Text = nombre
                };

                Label lblGrupo = new Label
                {
                    Text = "Grupo o área:",
                    Location = new Point(30, 180),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                txtGrupo = new TextBox
                {
                    Location = new Point(30, 205),
                    Size = new Size(420, 30),
                    Font = new Font("Segoe UI", 10F),
                    MaxLength = 100,
                    Text = grupo
                };

                Button btnGuardar = new Button
                {
                    Text = "Guardar",
                    Location = new Point(190, 265),
                    Size = new Size(125, 38),
                    BackColor = Color.FromArgb(52, 120, 180),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                btnGuardar.FlatAppearance.BorderSize = 0;

                Button btnCancelar = new Button
                {
                    Text = "Cancelar",
                    Location = new Point(325, 265),
                    Size = new Size(125, 38),
                    BackColor = Color.FromArgb(230, 233, 238),
                    ForeColor = Color.FromArgb(30, 45, 65),
                    FlatStyle = FlatStyle.Flat
                };

                btnCancelar.FlatAppearance.BorderSize = 0;

                btnGuardar.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                        string.IsNullOrWhiteSpace(txtGrupo.Text))
                    {
                        MessageBox.Show(
                            "Debes completar el nombre y el grupo del departamento.",
                            "Datos incompletos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };

                btnCancelar.Click += (s, e) =>
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                };

                this.AcceptButton = btnGuardar;
                this.CancelButton = btnCancelar;

                this.Controls.Add(lblTitulo);
                this.Controls.Add(lblDescripcion);
                this.Controls.Add(lblNombre);
                this.Controls.Add(txtNombre);
                this.Controls.Add(lblGrupo);
                this.Controls.Add(txtGrupo);
                this.Controls.Add(btnGuardar);
                this.Controls.Add(btnCancelar);
            }
        }
    }
}
