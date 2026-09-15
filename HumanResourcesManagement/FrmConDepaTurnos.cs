using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HumanResourcesManagement
{
    public partial class FrmConDepaTurnos : Form
    {
        private string cadenaConexion = "Server=localhost;Database=AdventureWorks2019;Integrated Security=True;Encrypt=False;";

        public FrmConDepaTurnos()
        {
            InitializeComponent();
        }
        private void frmConDepaTurnos_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
        }
        private void CargarDepartamentos()
        {
            string consulta = "SELECT DepartmentID, Name FROM HumanResources.Department ORDER BY Name";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    cmbDepartamento.DataSource = dt;
                    cmbDepartamento.DisplayMember = "Name";
                    cmbDepartamento.ValueMember = "DepartmentID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar departamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartamento.SelectedValue != null && int.TryParse(cmbDepartamento.SelectedValue.ToString(), out int idDepartamento))
            {
                CargarEmpleadosPorDepartamento(idDepartamento);
            }
        }

        private void CargarEmpleadosPorDepartamento(int idDepartamento)
        {
            string consultaSql = @"
                SELECT 
                    e.BusinessEntityID AS [ID Empleado],
                    CONCAT(p.FirstName, ' ', ISNULL(p.MiddleName + ' ', ''), p.LastName) AS [Nombre Completo],
                    d.Name AS [Departamento],
                    s.Name AS [Turno],
                    CONVERT(varchar(8), s.StartTime, 108) + ' - ' + CONVERT(varchar(8), s.EndTime, 108) AS [Horario]
                FROM HumanResources.Employee e
                INNER JOIN Person.Person p 
                    ON e.BusinessEntityID = p.BusinessEntityID
                INNER JOIN HumanResources.EmployeeDepartmentHistory edh 
                    ON e.BusinessEntityID = edh.BusinessEntityID
                INNER JOIN HumanResources.Department d 
                    ON edh.DepartmentID = d.DepartmentID
                INNER JOIN HumanResources.Shift s 
                    ON edh.ShiftID = s.ShiftID
                WHERE edh.DepartmentID = @DepartmentID
                  AND edh.EndDate IS NULL";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand(consultaSql, conexion);
                    comando.Parameters.AddWithValue("@DepartmentID", idDepartamento);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    tablaNombre.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los empleados del departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}