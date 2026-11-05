using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmContactoEmpleados : Form
    {
        private Panel panelSuperior;
        private Label lblFiltro;
        private TextBox txtFiltro;
        private Button btnBuscar;
        private Button btnExportar;
        private DataGridView dgvContacto;

        public FrmContactoEmpleados()
        {
            // Se remueve la llamada a InitializeComponent() para evitar el error CS0103
            ConstruirInterfaz();
            ConfigurarDataGridView();
            CargarContactoEmpleados();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Ficha de Contacto de Empleados y Reportes";
            this.Size = new Size(950, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            panelSuperior = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(240, 240, 240) };

            lblFiltro = new Label { Text = "Buscar Empleado:", Location = new Point(15, 20), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtFiltro = new TextBox { Location = new Point(130, 17), Width = 200, Font = new Font("Segoe UI", 9.5F) };
            btnBuscar = new Button { Text = "Buscar", Location = new Point(340, 15), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(30, 61, 89), ForeColor = Color.White };
            btnExportar = new Button { Text = "Exportar a CSV", Location = new Point(440, 15), Height = 30, Width = 130, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            btnBuscar.Click += (s, e) => CargarContactoEmpleados(txtFiltro.Text.Trim());
            txtFiltro.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnBuscar.PerformClick(); e.SuppressKeyPress = true; } };
            btnExportar.Click += (s, e) => ExportadorCSV.ExportarDataGridView(dgvContacto, "Reporte_Contacto_Empleados");

            panelSuperior.Controls.AddRange(new Control[] { lblFiltro, txtFiltro, btnBuscar, btnExportar });

            dgvContacto = new DataGridView { Dock = DockStyle.Fill };
            this.Controls.Add(dgvContacto);
            this.Controls.Add(panelSuperior);
        }

        private void ConfigurarDataGridView()
        {
            dgvContacto.ReadOnly = true;
            dgvContacto.AllowUserToAddRows = false;
            dgvContacto.AllowUserToDeleteRows = false;
            dgvContacto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContacto.MultiSelect = false;
            dgvContacto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvContacto.EnableHeadersVisualStyles = false;
            dgvContacto.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 61, 89);
            dgvContacto.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvContacto.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvContacto.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
        }

        private void CargarContactoEmpleados(string filtro = "")
        {
            string query = @"SELECT 
                                e.BusinessEntityID AS [ID],
                                p.FirstName + ' ' + ISNULL(p.MiddleName + ' ', '') + p.LastName AS [Nombre Completo],
                                pp.PhoneNumber AS [Teléfono],
                                ea.EmailAddress AS [Correo Electrónico]
                             FROM HumanResources.Employee e
                             INNER JOIN Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
                             LEFT JOIN Person.PersonPhone pp ON p.BusinessEntityID = pp.BusinessEntityID
                             LEFT JOIN Person.EmailAddress ea ON p.BusinessEntityID = ea.BusinessEntityID
                             WHERE (@Filtro = '' OR 
                                    p.FirstName LIKE '%' + @Filtro + '%' OR 
                                    p.LastName LIKE '%' + @Filtro + '%' OR 
                                    CAST(e.BusinessEntityID AS VARCHAR) = @Filtro)
                             ORDER BY e.BusinessEntityID ASC";

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

                        dgvContacto.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar contactos: {ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmContactoEmpleados_Load(object sender, EventArgs e)
        {
        }
    }
}