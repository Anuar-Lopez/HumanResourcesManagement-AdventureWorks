using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    /// <summary>
    /// Consulta de empleados con búsqueda por ID, nombre, apellido,
    /// cargo y estado. Reemplaza a las antiguas pantallas Form1 y BusEmpl.
    /// </summary>
    public partial class FrmConEmpl : Form
    {
        private TextBox txtId;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtCargo;
        private ComboBox cmbEstado;
        private Label lblTotal;
        private DataGridView dgvEmpleados;

        public FrmConEmpl()
        {
            InitializeComponent();

            ConstruirInterfaz();
        }

        private void FrmConEmpl_Load(object sender, EventArgs e)
        {
            BuscarEmpleados();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Consulta de empleados - AdventureWorks";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = EstiloUI.Fondo;
            this.Font = new Font("Segoe UI", 9F);

            Panel encabezado = EstiloUI.CrearEncabezado(
                "Consulta de empleados",
                "Busca empleados por ID, nombre, apellido, cargo o estado.",
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
                    dgvEmpleados, "Consulta_Empleados");

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

            txtId = EstiloUI.CrearTexto(25, 40, 90);
            txtNombre = EstiloUI.CrearTexto(125, 40, 170);
            txtApellido = EstiloUI.CrearTexto(305, 40, 170);
            txtCargo = EstiloUI.CrearTexto(485, 40, 190);
            cmbEstado = EstiloUI.CrearCombo(685, 40, 130);

            txtId.MaxLength = 9;
            txtId.KeyPress += (s, e) =>
            {
                // Solo números en el campo ID.
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };

            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Activos");
            cmbEstado.Items.Add("Inactivos");
            cmbEstado.SelectedIndex = 0;

            Button btnBuscar = EstiloUI.CrearBoton(
                "Buscar", 830, 36, 100,
                EstiloUI.Acento, Color.White);

            Button btnLimpiar = EstiloUI.CrearBoton(
                "Limpiar", 940, 36, 100,
                EstiloUI.Gris, EstiloUI.Marino);

            btnBuscar.Click += (s, e) => BuscarEmpleados();

            btnLimpiar.Click += (s, e) =>
            {
                txtId.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtCargo.Clear();
                cmbEstado.SelectedIndex = 0;
                BuscarEmpleados();
            };

            KeyEventHandler buscarConEnter = (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BuscarEmpleados();
                    e.SuppressKeyPress = true;
                }
            };

            txtId.KeyDown += buscarConEnter;
            txtNombre.KeyDown += buscarConEnter;
            txtApellido.KeyDown += buscarConEnter;
            txtCargo.KeyDown += buscarConEnter;

            panelFiltros.Controls.Add(EstiloUI.CrearEtiqueta("ID:", 25, 12));
            panelFiltros.Controls.Add(EstiloUI.CrearEtiqueta("Nombre:", 125, 12));
            panelFiltros.Controls.Add(EstiloUI.CrearEtiqueta("Apellido:", 305, 12));
            panelFiltros.Controls.Add(EstiloUI.CrearEtiqueta("Cargo:", 485, 12));
            panelFiltros.Controls.Add(EstiloUI.CrearEtiqueta("Estado:", 685, 12));
            panelFiltros.Controls.Add(txtId);
            panelFiltros.Controls.Add(txtNombre);
            panelFiltros.Controls.Add(txtApellido);
            panelFiltros.Controls.Add(txtCargo);
            panelFiltros.Controls.Add(cmbEstado);
            panelFiltros.Controls.Add(btnBuscar);
            panelFiltros.Controls.Add(btnLimpiar);

            // TABLA
            dgvEmpleados = new DataGridView();
            EstiloUI.ConfigurarGrid(dgvEmpleados);

            dgvEmpleados.DataBindingComplete += (s, e) =>
            {
                if (dgvEmpleados.Columns["Fecha de nacimiento"] != null)
                    dgvEmpleados.Columns["Fecha de nacimiento"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvEmpleados.Columns["Fecha de contratación"] != null)
                    dgvEmpleados.Columns["Fecha de contratación"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
            };

            this.Controls.Add(dgvEmpleados);
            this.Controls.Add(panelFiltros);
            this.Controls.Add(panelAcciones);
            this.Controls.Add(encabezado);
        }

        private void BuscarEmpleados()
        {
            int? id = null;
            string textoId = txtId.Text.Trim();

            if (textoId.Length > 0)
            {
                int valor;

                if (!int.TryParse(textoId, out valor))
                {
                    MessageBox.Show(
                        "El ID debe ser un número.",
                        "ID no válido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                id = valor;
            }

            int estado = -1;

            if (cmbEstado.SelectedIndex == 1)
                estado = 1;
            else if (cmbEstado.SelectedIndex == 2)
                estado = 0;

            string consulta = @"
                SELECT TOP 500
                    BusinessEntityID AS [ID Empleado],
                    NationalIDNumber AS [Documento],
                    CONCAT(FirstName, ' ', LastName) AS [Nombre completo],
                    JobTitle AS [Cargo],
                    BirthDate AS [Fecha de nacimiento],
                    HireDate AS [Fecha de contratación],
                    CASE
                        WHEN CurrentFlag = 1 THEN 'Activo'
                        ELSE 'Inactivo'
                    END AS [Estado]
                FROM HumanResources.Employee
                WHERE (@Id IS NULL OR BusinessEntityID = @Id)
                  AND (@Nombre = '' OR FirstName LIKE '%' + @Nombre + '%')
                  AND (@Apellido = '' OR LastName LIKE '%' + @Apellido + '%')
                  AND (@Cargo = '' OR JobTitle LIKE '%' + @Cargo + '%')
                  AND (@Estado = -1 OR CurrentFlag = @Estado)
                ORDER BY BusinessEntityID DESC;";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value =
                        id.HasValue ? (object)id.Value : DBNull.Value;

                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50)
                        .Value = txtNombre.Text.Trim();

                    cmd.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50)
                        .Value = txtApellido.Text.Trim();

                    cmd.Parameters.Add("@Cargo", SqlDbType.NVarChar, 50)
                        .Value = txtCargo.Text.Trim();

                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = estado;

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
                    "No se pudo realizar la búsqueda.\n\n" + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
