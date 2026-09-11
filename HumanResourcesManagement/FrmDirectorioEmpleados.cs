using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmDirectorioEmpleados : Form
    {
        private Panel panelFiltros;
        private Label lblTituloFiltro;
        private TextBox txtFiltro;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnSalir;
        private DataGridView dgvEmpleados;

        public FrmDirectorioEmpleados()
        {
            InitializeComponent();
            ConstruirInterfaz();
            ConfigurarDataGridView();
        }

        private void FrmDirectorioEmpleados_Load(object sender, EventArgs e)
        {
            CargarDirectorioEmpleados();
        }

        // T-05: Construcción visual organizada del formulario
        private void ConstruirInterfaz()
        {
            this.Text = "Directorio de Empleados - AdventureWorks";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel de Filtros con altura ajustada para el título
            panelFiltros = new Panel { Dock = DockStyle.Top, Height = 75, BackColor = Color.FromArgb(240, 240, 240) };

            // Título/Etiqueta del Filtro
            lblTituloFiltro = new Label
            {
                Text = "Filtrar por ID, Cédula, Cargo o Usuario:",
                Location = new Point(20, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            // Controles de entrada y botones
            txtFiltro = new TextBox { Location = new Point(20, 32), Width = 280, Font = new Font("Segoe UI", 10F) };
            btnBuscar = new Button { Text = "Buscar", Location = new Point(310, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat };
            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(410, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat };
            btnSalir = new Button { Text = "Salir", Location = new Point(820, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White };

            // Eventos
            btnBuscar.Click += (s, e) => CargarDirectorioEmpleados(txtFiltro.Text.Trim());
            btnLimpiar.Click += (s, e) => { txtFiltro.Clear(); CargarDirectorioEmpleados(); };
            btnSalir.Click += (s, e) => this.Close();

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            // Agregar controles al panel
            panelFiltros.Controls.AddRange(new Control[] { lblTituloFiltro, txtFiltro, btnBuscar, btnLimpiar, btnSalir });

            // DataGridView principal
            dgvEmpleados = new DataGridView { Dock = DockStyle.Fill };

            this.Controls.Add(dgvEmpleados);
            this.Controls.Add(panelFiltros);
        }

        // T-08: Configuración y Optimización del DataGridView
        private void ConfigurarDataGridView()
        {
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Optimización visual de alto rendimiento
            dgvEmpleados.EnableHeadersVisualStyles = false;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 61, 89);
            dgvEmpleados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvEmpleados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Formato de fechas limpias (Sin hora)
            dgvEmpleados.DataBindingComplete += (s, e) =>
            {
                if (dgvEmpleados.Columns["Fecha Nacimiento"] != null)
                    dgvEmpleados.Columns["Fecha Nacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvEmpleados.Columns["Fecha Contratación"] != null)
                    dgvEmpleados.Columns["Fecha Contratación"].DefaultCellStyle.Format = "dd/MM/yyyy";
            };
        }

        // Consulta SQL multi-criterio integrada
        private void CargarDirectorioEmpleados(string filtro = "")
        {
            string query = @"SELECT TOP 100 
                                BusinessEntityID AS [ID Empleado],
                                NationalIDNumber AS [Cédula / Documento],
                                LoginID AS [Usuario Login],
                                JobTitle AS [Cargo],
                                BirthDate AS [Fecha Nacimiento],
                                HireDate AS [Fecha Contratación],
                                VacationHours AS [Horas Vacaciones]
                             FROM HumanResources.Employee
                             WHERE (@Filtro = '' 
                                OR JobTitle LIKE '%' + @Filtro + '%' 
                                OR LoginID LIKE '%' + @Filtro + '%'
                                OR NationalIDNumber LIKE '%' + @Filtro + '%'
                                OR CAST(BusinessEntityID AS VARCHAR) LIKE '%' + @Filtro + '%')
                             ORDER BY BusinessEntityID ASC";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Filtro", filtro);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvEmpleados.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el directorio: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}