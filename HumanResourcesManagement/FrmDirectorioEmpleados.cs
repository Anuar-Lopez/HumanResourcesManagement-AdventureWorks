
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmDirectorioEmpleados : Form
    {
        private TextBox txtFiltro;
        private DataGridView dgvEmpleados;
        private Label lblTotal;
        private Label lblDescripcion;

        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnNuevo;
        private Button btnModificar;
        private Button btnBaja;
        private Button btnActualizar;
        private Button btnVolver;

        public FrmDirectorioEmpleados()
        {
            InitializeComponent();

            ConstruirInterfaz();
            ConfigurarTabla();

            this.Load += FrmDirectorioEmpleados_Load;
        }

        private void FrmDirectorioEmpleados_Load(object sender, EventArgs e)
        {
            CargarDirectorioEmpleados();
        }

        // construccion de la interfaz

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();

            this.Text = "Directorio de empleados - Human Resources";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1050, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9F);

            // ENCABEZADO
            Panel panelEncabezado = new Panel();
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Height = 130;
            panelEncabezado.BackColor = Color.White;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Directorio de empleados";
            lblTitulo.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(30, 45, 65);
            lblTitulo.Location = new Point(30, 20);
            lblTitulo.AutoSize = true;

            lblDescripcion = new Label();
            lblDescripcion.Text =
                "Consulta, registra y administra la información del personal.";
            lblDescripcion.Font = new Font("Segoe UI", 10F);
            lblDescripcion.ForeColor = Color.Gray;
            lblDescripcion.Location = new Point(34, 70);
            lblDescripcion.AutoSize = true;

            lblTotal = new Label();
            lblTotal.Text = "Empleados registrados: 0";
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(52, 120, 180);
            lblTotal.Location = new Point(34, 100);
            lblTotal.AutoSize = true;

            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(lblTotal);

            // PANEL DE ACCIONES
            Panel panelAcciones = new Panel();
            panelAcciones.Dock = DockStyle.Top;
            panelAcciones.Height = 85;
            panelAcciones.BackColor = Color.FromArgb(245, 247, 250);

            btnNuevo = CrearBoton(
                " + Nuevo empleado",
                20, 20, 165,
                Color.FromArgb(40, 145, 95),
                Color.White);

            btnModificar = CrearBoton(
                "Modificar",
                195, 20, 125,
                Color.FromArgb(52, 120, 180),
                Color.White);

            btnBaja = CrearBoton(
                "Dar de baja",
                330, 20, 125,
                Color.FromArgb(180, 65, 75),
                Color.White);

            btnActualizar = CrearBoton(
                "Actualizar",
                465, 20, 120,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            btnVolver = CrearBoton(
                "Volver al menú principal",
                970, 20, 195,
                Color.FromArgb(30, 45, 65),
                Color.White);

            btnNuevo.Click += btnNuevo_Click;
            btnModificar.Click += btnModificar_Click;
            btnBaja.Click += btnBaja_Click;
            btnActualizar.Click += (s, e) =>
                CargarDirectorioEmpleados(txtFiltro.Text.Trim());
            btnVolver.Click += btnVolver_Click;

            panelAcciones.Controls.Add(btnNuevo);
            panelAcciones.Controls.Add(btnModificar);
            panelAcciones.Controls.Add(btnBaja);
            panelAcciones.Controls.Add(btnActualizar);
            panelAcciones.Controls.Add(btnVolver);

            // PANEL DE BUSQUEDA
            Panel panelBusqueda = new Panel();
            panelBusqueda.Dock = DockStyle.Top;
            panelBusqueda.Height = 75;
            panelBusqueda.BackColor = Color.White;

            Label lblBuscar = new Label();
            lblBuscar.Text = "Buscar empleado:";
            lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBuscar.ForeColor = Color.FromArgb(45, 55, 70);
            lblBuscar.Location = new Point(25, 10);
            lblBuscar.AutoSize = true;

            txtFiltro = new TextBox();
            txtFiltro.Location = new Point(25, 35);
            txtFiltro.Size = new Size(390, 30);
            txtFiltro.Font = new Font("Segoe UI", 10F);
            txtFiltro.BorderStyle = BorderStyle.FixedSingle;

            btnBuscar = CrearBoton(
                "Buscar",
                430, 32, 100,
                Color.FromArgb(52, 120, 180),
                Color.White);

            btnLimpiar = CrearBoton(
                "Limpiar",
                540, 32, 100,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            btnBuscar.Click += (s, e) =>
                CargarDirectorioEmpleados(txtFiltro.Text.Trim());

            btnLimpiar.Click += (s, e) =>
            {
                txtFiltro.Clear();
                CargarDirectorioEmpleados();
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
            dgvEmpleados = new DataGridView();
            dgvEmpleados.Dock = DockStyle.Fill;
            dgvEmpleados.Margin = new Padding(20);
            dgvEmpleados.BackgroundColor = Color.White;
            dgvEmpleados.BorderStyle = BorderStyle.None;

            // AGREGAR CONTROLES
            this.Controls.Add(dgvEmpleados);
            this.Controls.Add(panelBusqueda);
            this.Controls.Add(panelAcciones);
            this.Controls.Add(panelEncabezado);
        }

        private Button CrearBoton(
            string texto,
            int x,
            int y,
            int ancho,
            Color fondo,
            Color textoColor)
        {
            Button boton = new Button();

            boton.Text = texto;
            boton.Location = new Point(x, y);
            boton.Size = new Size(ancho, 42);
            boton.BackColor = fondo;
            boton.ForeColor = textoColor;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;

            return boton;
        }

        //configuracion tabla

        private void ConfigurarTabla()
        {
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
            dgvEmpleados.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleados.RowHeadersVisible = false;
            dgvEmpleados.EnableHeadersVisualStyles = false;

            dgvEmpleados.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 61, 89);

            dgvEmpleados.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvEmpleados.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvEmpleados.ColumnHeadersHeight = 42;
            dgvEmpleados.RowTemplate.Height = 35;

            dgvEmpleados.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F);

            dgvEmpleados.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(210, 228, 245);

            dgvEmpleados.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(30, 45, 65);

            dgvEmpleados.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            dgvEmpleados.DataBindingComplete += (s, e) =>
            {
                if (dgvEmpleados.Columns["Fecha Nacimiento"] != null)
                    dgvEmpleados.Columns["Fecha Nacimiento"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvEmpleados.Columns["Fecha Contratación"] != null)
                    dgvEmpleados.Columns["Fecha Contratación"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
            };

            dgvEmpleados.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    btnModificar.PerformClick();
            };
        }

        
        // Consultar y administrar empleados    

        private void CargarDirectorioEmpleados(string filtro = "")
        {
            string consulta = @"
                SELECT TOP 500
                    BusinessEntityID AS [ID Empleado],
                    NationalIDNumber AS [Documento],
                    FirstName AS [Nombres],
                    LastName AS [Apellidos],
                    LoginID AS [Usuario laboral],
                    JobTitle AS [Cargo],
                    BirthDate AS [Fecha Nacimiento],
                    HireDate AS [Fecha Contratación],
                    VacationHours AS [Horas Vacaciones],
                    CASE
                        WHEN CurrentFlag = 1 THEN 'Activo'
                        ELSE 'Inactivo'
                    END AS [Estado]
                FROM HumanResources.Employee
                WHERE
                    (@Filtro = '')
                    OR CAST(BusinessEntityID AS VARCHAR(20))
                        LIKE '%' + @Filtro + '%'
                    OR NationalIDNumber LIKE '%' + @Filtro + '%'
                    OR FirstName LIKE '%' + @Filtro + '%'
                    OR LastName LIKE '%' + @Filtro + '%'
                    OR LoginID LIKE '%' + @Filtro + '%'
                    OR JobTitle LIKE '%' + @Filtro + '%'
                ORDER BY BusinessEntityID DESC";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.Add(
                        "@Filtro", SqlDbType.NVarChar, 256).Value = filtro;

                    DataTable tabla = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }

                    dgvEmpleados.DataSource = tabla;

                    lblTotal.Text =
                        "Empleados encontrados: " + tabla.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar el directorio.\n\n" + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //registrar empleado
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmRegistroEmpleado formulario =
                new FrmRegistroEmpleado())
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CargarDirectorioEmpleados(txtFiltro.Text.Trim());
                }
            }
        }
        //modificar empleado

        private int? ObtenerEmpleadoSeleccionado()
        {
            if (dgvEmpleados.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecciona un empleado de la tabla.",
                    "Selección requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return null;
            }

            return Convert.ToInt32(
                dgvEmpleados.CurrentRow.Cells["ID Empleado"].Value);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            int? id = ObtenerEmpleadoSeleccionado();

            if (!id.HasValue)
                return;

            using (FrmRegistroEmpleado formulario =
                new FrmRegistroEmpleado(id.Value))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CargarDirectorioEmpleados(txtFiltro.Text.Trim());
                }
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            int? id = ObtenerEmpleadoSeleccionado();

            if (!id.HasValue)
                return;

            string nombre = dgvEmpleados.CurrentRow
                .Cells["Nombres"].Value.ToString();

            string apellido = dgvEmpleados.CurrentRow
                .Cells["Apellidos"].Value.ToString();

            string estado = dgvEmpleados.CurrentRow
                .Cells["Estado"].Value.ToString();

            if (estado == "Inactivo")
            {
                MessageBox.Show(
                    "Este empleado ya se encuentra inactivo.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas dar de baja a:\n\n" +
                nombre + " " + apellido + "?\n\n" +
                "El registro se conservará en la base de datos.",
                "Confirmar baja de empleado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            using (SqlConnection con = ConexionSQL.ObtenerConexion())
            using (SqlTransaction transaccion = con.BeginTransaction())
            {
                try
                {
                    string consulta = @"
                        UPDATE HumanResources.Employee
                        SET CurrentFlag = 0,
                            ModifiedDate = GETDATE()
                        WHERE BusinessEntityID = @Id;

                        UPDATE HumanResources.EmployeeDepartmentHistory
                        SET EndDate =
                            CASE
                                WHEN StartDate > CAST(GETDATE() AS date)
                                THEN StartDate
                                ELSE CAST(GETDATE() AS date)
                            END,
                            ModifiedDate = GETDATE()
                        WHERE BusinessEntityID = @Id
                          AND EndDate IS NULL;";

                    using (SqlCommand cmd = new SqlCommand(
                        consulta, con, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@Id", id.Value);
                        cmd.ExecuteNonQuery();
                    }

                    transaccion.Commit();

                    MessageBox.Show(
                        "El empleado fue dado de baja correctamente.",
                        "Operación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarDirectorioEmpleados(txtFiltro.Text.Trim());
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show(
                        "No fue posible dar de baja al empleado.\n\n" +
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // VOLVER AL MENU PRINCIPAL
     
        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas regresar al menú principal?",
                "Volver al menú",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            Form principal = Application.OpenForms["FrmMain"];

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
                    "Navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
