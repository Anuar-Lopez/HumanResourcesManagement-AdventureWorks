using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    /// <summary>
    /// Empleados con su asignación vigente de departamento y turno.
    /// Permite filtrar por departamento, turno y texto libre.
    /// </summary>
    public partial class FrmConDepaTurnos : Form
    {
        private ComboBox cmbDepartamento;
        private ComboBox cmbTurno;
        private TextBox txtFiltro;
        private Label lblTotal;
        private DataGridView dgvEmpleados;

        // Evita consultar varias veces mientras se cargan las listas.
        private bool cargandoListas = true;

        public FrmConDepaTurnos()
        {
            InitializeComponent();

            ConstruirInterfaz();
        }

        private void FrmConDepaTurnos_Load(object sender, EventArgs e)
        {
            CargarListas();
            CargarEmpleados();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Departamentos y turnos - AdventureWorks";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = EstiloUI.Fondo;
            this.Font = new Font("Segoe UI", 9F);

            Panel encabezado = EstiloUI.CrearEncabezado(
                "Departamentos y turnos",
                "Consulta qué empleados trabajan en cada departamento y en qué turno.",
                out lblTotal);

            lblTotal.Text = "Empleados encontrados: 0";

            // PANEL DE ACCIONES
            Panel panelAcciones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = EstiloUI.Fondo
            };

            Button btnExportar = EstiloUI.CrearBoton(
                "Exportar CSV", 20, 17, 130,
                EstiloUI.Verde, Color.White);

            Button btnVolver = EstiloUI.CrearBoton(
                "Volver al menú principal", 970, 17, 195,
                EstiloUI.Marino, Color.White);

            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnExportar.Click += (s, e) =>
                ExportadorCSV.ExportarDataGridView(
                    dgvEmpleados, "Departamentos_y_Turnos");

            btnVolver.Click += (s, e) => this.Close();

            panelAcciones.Controls.Add(btnExportar);
            panelAcciones.Controls.Add(btnVolver);

            // PANEL DE FILTROS
            Panel panelFiltros = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White
            };

            cmbDepartamento = EstiloUI.CrearCombo(25, 40, 260);
            cmbTurno = EstiloUI.CrearCombo(300, 40, 190);
            txtFiltro = EstiloUI.CrearTexto(505, 40, 240);

            Button btnBuscar = EstiloUI.CrearBoton(
                "Buscar", 760, 36, 100,
                EstiloUI.Acento, Color.White);

            Button btnLimpiar = EstiloUI.CrearBoton(
                "Limpiar", 870, 36, 100,
                EstiloUI.Gris, EstiloUI.Marino);

            cmbDepartamento.SelectedIndexChanged += (s, e) =>
            {
                if (!cargandoListas)
                    CargarEmpleados();
            };

            cmbTurno.SelectedIndexChanged += (s, e) =>
            {
                if (!cargandoListas)
                    CargarEmpleados();
            };

            btnBuscar.Click += (s, e) => CargarEmpleados();

            btnLimpiar.Click += (s, e) =>
            {
                cargandoListas = true;
                txtFiltro.Clear();

                if (cmbDepartamento.Items.Count > 0)
                    cmbDepartamento.SelectedIndex = 0;

                if (cmbTurno.Items.Count > 0)
                    cmbTurno.SelectedIndex = 0;

                cargandoListas = false;
                CargarEmpleados();
            };

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CargarEmpleados();
                    e.SuppressKeyPress = true;
                }
            };

            panelFiltros.Controls.Add(
                EstiloUI.CrearEtiqueta("Departamento:", 25, 12));
            panelFiltros.Controls.Add(
                EstiloUI.CrearEtiqueta("Turno:", 300, 12));
            panelFiltros.Controls.Add(
                EstiloUI.CrearEtiqueta("Nombre o cargo:", 505, 12));
            panelFiltros.Controls.Add(cmbDepartamento);
            panelFiltros.Controls.Add(cmbTurno);
            panelFiltros.Controls.Add(txtFiltro);
            panelFiltros.Controls.Add(btnBuscar);
            panelFiltros.Controls.Add(btnLimpiar);

            // TABLA
            dgvEmpleados = new DataGridView();
            EstiloUI.ConfigurarGrid(dgvEmpleados);

            dgvEmpleados.DataBindingComplete += (s, e) =>
            {
                if (dgvEmpleados.Columns["Desde"] != null)
                    dgvEmpleados.Columns["Desde"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
            };

            this.Controls.Add(dgvEmpleados);
            this.Controls.Add(panelFiltros);
            this.Controls.Add(panelAcciones);
            this.Controls.Add(encabezado);
        }

        private void CargarListas()
        {
            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    CargarCombo(
                        con,
                        cmbDepartamento,
                        @"SELECT Id, Nombre
                          FROM
                          (
                              SELECT 0 AS Id,
                                     'Todos los departamentos' AS Nombre,
                                     0 AS Orden
                              UNION ALL
                              SELECT DepartmentID, Name, 1
                              FROM HumanResources.Department
                          ) T
                          ORDER BY Orden, Nombre");

                    CargarCombo(
                        con,
                        cmbTurno,
                        @"SELECT Id, Nombre
                          FROM
                          (
                              SELECT 0 AS Id,
                                     'Todos los turnos' AS Nombre,
                                     0 AS Orden
                              UNION ALL
                              SELECT ShiftID, Name, 1
                              FROM HumanResources.Shift
                          ) T
                          ORDER BY Orden, Nombre");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los departamentos y turnos.\n\n" +
                    ex.Message,
                    "Error de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cargandoListas = false;
            }
        }

        private void CargarCombo(
            SqlConnection con, ComboBox combo, string consulta)
        {
            DataTable tabla = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(consulta, con))
            {
                da.Fill(tabla);
            }

            combo.DataSource = null;
            combo.DisplayMember = "Nombre";
            combo.ValueMember = "Id";
            combo.DataSource = tabla;

            if (tabla.Rows.Count > 0)
                combo.SelectedIndex = 0;
        }

        private static int ObtenerId(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return 0;

            int id;

            return int.TryParse(combo.SelectedValue.ToString(), out id)
                ? id
                : 0;
        }

        private void CargarEmpleados()
        {
            string consulta = @"
                SELECT
                    E.BusinessEntityID AS [ID Empleado],
                    CONCAT(E.FirstName, ' ', E.LastName)
                        AS [Nombre completo],
                    E.JobTitle AS [Cargo],
                    D.Name AS [Departamento],
                    S.Name AS [Turno],
                    CONVERT(varchar(5), S.StartTime, 108)
                        + ' - ' +
                    CONVERT(varchar(5), S.EndTime, 108) AS [Horario],
                    EDH.StartDate AS [Desde]
                FROM HumanResources.Employee E
                INNER JOIN HumanResources.EmployeeDepartmentHistory EDH
                    ON E.BusinessEntityID = EDH.BusinessEntityID
                INNER JOIN HumanResources.Department D
                    ON EDH.DepartmentID = D.DepartmentID
                INNER JOIN HumanResources.Shift S
                    ON EDH.ShiftID = S.ShiftID
                WHERE EDH.EndDate IS NULL
                  AND (@Departamento = 0
                       OR EDH.DepartmentID = @Departamento)
                  AND (@Turno = 0
                       OR EDH.ShiftID = @Turno)
                  AND (@Filtro = ''
                       OR E.FirstName LIKE '%' + @Filtro + '%'
                       OR E.LastName LIKE '%' + @Filtro + '%'
                       OR E.JobTitle LIKE '%' + @Filtro + '%')
                ORDER BY D.Name, E.FirstName, E.LastName;";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.Add("@Departamento", SqlDbType.Int).Value =
                        ObtenerId(cmbDepartamento);

                    cmd.Parameters.Add("@Turno", SqlDbType.Int).Value =
                        ObtenerId(cmbTurno);

                    cmd.Parameters.Add("@Filtro", SqlDbType.NVarChar, 100)
                        .Value = txtFiltro.Text.Trim();

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
                    "No se pudieron obtener los empleados.\n\n" + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
