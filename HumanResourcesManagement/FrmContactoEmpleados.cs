
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmContactoEmpleados : Form
    {
        private Panel panelEncabezado;
        private Panel panelAcciones;
        private Panel panelBusqueda;

        private Label lblTitulo;
        private Label lblDescripcion;
        private Label lblTotal;
        private Label lblFiltro;

        private TextBox txtFiltro;
        private ComboBox cmbEstado;

        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnActualizar;
        private Button btnExportar;
        private Button btnVolver;

        private DataGridView dgvContacto;

        public FrmContactoEmpleados()
        {
            ConstruirInterfaz();
            ConfigurarDataGridView();
            CargarContactoEmpleados();
        }

        // ==========================================
        // CONSTRUCCION DE LA INTERFAZ
        // ==========================================

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de empleados - AdventureWorks";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
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

            lblTitulo = new Label
            {
                Text = "Gestión de empleados",
                Location = new Point(30, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 45, 65)
            };

            lblDescripcion = new Label
            {
                Text = "Consulta y organiza la información personal y laboral del personal.",
                Location = new Point(34, 68),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Gray
            };

            lblTotal = new Label
            {
                Text = "Empleados encontrados: 0",
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

            btnActualizar = CrearBoton(
                "Actualizar",
                20, 17, 110,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            btnExportar = CrearBoton(
                "Exportar CSV",
                140, 17, 130,
                Color.FromArgb(40, 145, 95),
                Color.White);

            btnVolver = CrearBoton(
                "Volver al menú principal",
                970, 17, 195,
                Color.FromArgb(30, 45, 65),
                Color.White);

            btnActualizar.Click += (s, e) =>
                CargarContactoEmpleados(txtFiltro.Text.Trim());

            btnExportar.Click += btnExportar_Click;
            btnVolver.Click += btnVolver_Click;

            panelAcciones.Controls.Add(btnActualizar);
            panelAcciones.Controls.Add(btnExportar);
            panelAcciones.Controls.Add(btnVolver);

            // PANEL DE BUSQUEDA
            panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White
            };

            lblFiltro = new Label
            {
                Text = "Buscar empleado:",
                Location = new Point(25, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 70)
            };

            txtFiltro = new TextBox
            {
                Location = new Point(25, 40),
                Size = new Size(330, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnBuscar = CrearBoton(
                "Buscar",
                365, 37, 100,
                Color.FromArgb(52, 120, 180),
                Color.White);

            btnLimpiar = CrearBoton(
                "Limpiar",
                475, 37, 100,
                Color.FromArgb(230, 233, 238),
                Color.FromArgb(30, 45, 65));

            Label lblEstado = new Label
            {
                Text = "Estado:",
                Location = new Point(600, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            cmbEstado = new ComboBox
            {
                Location = new Point(600, 40),
                Size = new Size(180, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };

            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Activos");
            cmbEstado.Items.Add("Inactivos");
            cmbEstado.SelectedIndex = 0;

            btnBuscar.Click += (s, e) =>
                CargarContactoEmpleados(txtFiltro.Text.Trim());

            btnLimpiar.Click += (s, e) =>
            {
                txtFiltro.Clear();
                cmbEstado.SelectedIndex = 0;
                CargarContactoEmpleados();
            };

            cmbEstado.SelectedIndexChanged += (s, e) =>
                CargarContactoEmpleados(txtFiltro.Text.Trim());

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            panelBusqueda.Controls.Add(lblFiltro);
            panelBusqueda.Controls.Add(txtFiltro);
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(btnLimpiar);
            panelBusqueda.Controls.Add(lblEstado);
            panelBusqueda.Controls.Add(cmbEstado);

            // TABLA
            dgvContacto = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(15)
            };

            // ORDEN DE LOS CONTROLES
            this.Controls.Add(dgvContacto);
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
                Size = new Size(ancho, 40),
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
            dgvContacto.ReadOnly = true;
            dgvContacto.AllowUserToAddRows = false;
            dgvContacto.AllowUserToDeleteRows = false;
            dgvContacto.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvContacto.MultiSelect = false;
            dgvContacto.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvContacto.RowHeadersVisible = false;
            dgvContacto.EnableHeadersVisualStyles = false;

            dgvContacto.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 61, 89);

            dgvContacto.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvContacto.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvContacto.ColumnHeadersHeight = 42;
            dgvContacto.RowTemplate.Height = 35;

            dgvContacto.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F);

            dgvContacto.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(210, 228, 245);

            dgvContacto.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(30, 45, 65);

            dgvContacto.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);
        }

        // ==========================================
        // CONSULTAR EMPLEADOS
        // ==========================================

        private void CargarContactoEmpleados(string filtro = "")
        {
            string consulta = @"
                SELECT TOP 500
                    BusinessEntityID AS [ID Empleado],
                    NationalIDNumber AS [Documento],
                    ISNULL(FirstName, '') AS [Nombres],
                    ISNULL(LastName, '') AS [Apellidos],
                    LoginID AS [Usuario laboral],
                    JobTitle AS [Cargo],
                    HireDate AS [Fecha de contratación],
                    CASE
                        WHEN CurrentFlag = 1 THEN 'Activo'
                        ELSE 'Inactivo'
                    END AS [Estado]
                FROM HumanResources.Employee
                WHERE
                    (
                        @Filtro = ''
                        OR CAST(BusinessEntityID AS VARCHAR(20))
                            LIKE '%' + @Filtro + '%'
                        OR NationalIDNumber LIKE '%' + @Filtro + '%'
                        OR FirstName LIKE '%' + @Filtro + '%'
                        OR LastName LIKE '%' + @Filtro + '%'
                        OR LoginID LIKE '%' + @Filtro + '%'
                        OR JobTitle LIKE '%' + @Filtro + '%'
                    )
                    AND
                    (
                        @Estado = -1
                        OR CurrentFlag = @Estado
                    )
                ORDER BY BusinessEntityID DESC;";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.Add(
                        "@Filtro", SqlDbType.NVarChar, 256
                    ).Value = filtro;

                    int estado = -1;

                    if (cmbEstado.SelectedIndex == 1)
                        estado = 1;
                    else if (cmbEstado.SelectedIndex == 2)
                        estado = 0;

                    cmd.Parameters.Add(
                        "@Estado", SqlDbType.Int
                    ).Value = estado;

                    DataTable tabla = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }

                    dgvContacto.DataSource = tabla;

                    lblTotal.Text =
                        "Empleados encontrados: " + tabla.Rows.Count;

                    if (dgvContacto.Columns["Fecha de contratación"] != null)
                    {
                        dgvContacto.Columns["Fecha de contratación"]
                            .DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la información de los empleados.\n\n" +
                    ex.Message,
                    "Error SQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // EXPORTAR INFORMACION
        // ==========================================

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvContacto.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay información para exportar.",
                    "Sin datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                ExportadorCSV.ExportarDataGridView(
                    dgvContacto,
                    "Reporte_Gestion_Empleados");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo exportar el reporte.\n\n" + ex.Message,
                    "Error de exportación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // REGRESAR AL MENU PRINCIPAL
        // ==========================================

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

            if (principal == null ||
                principal.IsDisposed)
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
                    "No se encontró el menú principal.",
                    "Error de navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
