
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HumanResourcesManagement
{
    public partial class FrmConDepaTurnos : Form
    {
        public FrmConDepaTurnos()
        {
            InitializeComponent();
        }

        private void frmConDepaTurnos_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
            ConfigurarTabla();
        }

        private void ConfigurarTabla()
        {
            tablaNombre.ReadOnly = true;
            tablaNombre.AllowUserToAddRows = false;
            tablaNombre.AllowUserToDeleteRows = false;
            tablaNombre.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            tablaNombre.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            tablaNombre.MultiSelect = false;
        }

        private void CargarDepartamentos()
        {
            string consulta = @"
                SELECT DepartmentID, Name
                FROM HumanResources.Department
                ORDER BY Name";

            using (SqlConnection conexion =
                ConexionSQL.ObtenerConexion())
            {
                try
                {
                    SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion);

                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    cmbDepartamento.DataSource = null;
                    cmbDepartamento.DisplayMember = "Name";
                    cmbDepartamento.ValueMember = "DepartmentID";
                    cmbDepartamento.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        cmbDepartamento.SelectedIndex = 0;
                    }
                    else
                    {
                        tablaNombre.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al cargar departamentos: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void cmbDepartamento_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (cmbDepartamento.SelectedValue == null)
                return;

            if (int.TryParse(
                cmbDepartamento.SelectedValue.ToString(),
                out int idDepartamento))
            {
                CargarEmpleadosPorDepartamento(idDepartamento);
            }
        }

        private void CargarEmpleadosPorDepartamento(
            int idDepartamento)
        {
            string consultaSql = @"
                SELECT
                    e.BusinessEntityID AS [ID Empleado],
                    CONCAT(e.FirstName, ' ', e.LastName)
                        AS [Nombre Completo],
                    d.Name AS [Departamento],
                    s.Name AS [Turno],
                    CONVERT(varchar(8), s.StartTime, 108)
                    + ' - ' +
                    CONVERT(varchar(8), s.EndTime, 108)
                        AS [Horario]
                FROM HumanResources.Employee e
                INNER JOIN HumanResources.EmployeeDepartmentHistory edh
                    ON e.BusinessEntityID = edh.BusinessEntityID
                INNER JOIN HumanResources.Department d
                    ON edh.DepartmentID = d.DepartmentID
                INNER JOIN HumanResources.Shift s
                    ON edh.ShiftID = s.ShiftID
                WHERE edh.DepartmentID = @DepartmentID
                  AND edh.EndDate IS NULL
                ORDER BY e.FirstName, e.LastName";

            using (SqlConnection conexion =
                ConexionSQL.ObtenerConexion())
            {
                try
                {
                    SqlCommand comando =
                        new SqlCommand(consultaSql, conexion);

                    comando.Parameters.Add(
                        "@DepartmentID", SqlDbType.SmallInt
                    ).Value = idDepartamento;

                    SqlDataAdapter adaptador =
                        new SqlDataAdapter(comando);

                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    tablaNombre.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        // No hay empleados asignados actualmente.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al obtener los empleados del departamento: "
                        + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
