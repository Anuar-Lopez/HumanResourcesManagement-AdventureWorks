using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmDepartamentos : Form
    {
        private Panel panelFiltros;
        private Label lblTituloFiltro;
        private TextBox txtFiltro;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnSalir;
        private DataGridView dgvDepartamentos;

        public FrmDepartamentos()
        {
            InitializeComponent();
            ConstruirInterfaz();
            ConfigurarDataGridView();
        }

        private void FrmDepartamentos_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Departamentos - AdventureWorks";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            panelFiltros = new Panel { Dock = DockStyle.Top, Height = 75, BackColor = Color.FromArgb(240, 240, 240) };

            lblTituloFiltro = new Label
            {
                Text = "Filtrar por Nombre o Grupo:",
                Location = new Point(20, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            txtFiltro = new TextBox { Location = new Point(20, 32), Width = 250, Font = new Font("Segoe UI", 10F) };
            btnBuscar = new Button { Text = "Buscar", Location = new Point(280, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat };
            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(380, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat };
            btnSalir = new Button { Text = "Salir", Location = new Point(720, 30), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White };

            btnBuscar.Click += (s, e) => CargarDepartamentos(txtFiltro.Text.Trim());
            btnLimpiar.Click += (s, e) => { txtFiltro.Clear(); CargarDepartamentos(); };
            btnSalir.Click += (s, e) => this.Close();

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            panelFiltros.Controls.AddRange(new Control[] { lblTituloFiltro, txtFiltro, btnBuscar, btnLimpiar, btnSalir });

            dgvDepartamentos = new DataGridView { Dock = DockStyle.Fill };

            this.Controls.Add(dgvDepartamentos);
            this.Controls.Add(panelFiltros);
            this.Load += FrmDepartamentos_Load;
        }

        private void ConfigurarDataGridView()
        {
            dgvDepartamentos.ReadOnly = true;
            dgvDepartamentos.AllowUserToAddRows = false;
            dgvDepartamentos.AllowUserToDeleteRows = false;
            dgvDepartamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartamentos.MultiSelect = false;
            dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDepartamentos.EnableHeadersVisualStyles = false;
            dgvDepartamentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 61, 89);
            dgvDepartamentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDepartamentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvDepartamentos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            dgvDepartamentos.DataBindingComplete += (s, e) =>
            {
                if (dgvDepartamentos.Columns["Fecha Modificación"] != null)
                    dgvDepartamentos.Columns["Fecha Modificación"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            };
        }

        private void CargarDepartamentos(string filtro = "")
        {
            string query = @"SELECT 
                                DepartmentID AS [ID],
                                Name AS [Nombre Departamento],
                                GroupName AS [Grupo / Área],
                                ModifiedDate AS [Fecha Modificación]
                             FROM HumanResources.Department
                             WHERE (@Filtro = '' OR Name LIKE '%' + @Filtro + '%' OR GroupName LIKE '%' + @Filtro + '%')
                             ORDER BY DepartmentID ASC";

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
                        dgvDepartamentos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar departamentos: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}