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

namespace DirectorioRRHH
{
    public partial class Form1 : Form
    {
        // Ajusta "Server" según la instancia de tu SQL Server
        private string cadenaConexion = "Server=localhost;Database=AdventureWorks2019;Integrated Security=True;Encrypt=False;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarEmpleados();
        }

        private void ConsultarEmpleados()
        {
            string consultaSql = @"
                SELECT 
                    e.BusinessEntityID AS [ID Empleado],
                    CONCAT(p.FirstName, ' ', ISNULL(p.MiddleName + ' ', ''), p.LastName) AS [Nombre Completo],
                    e.JobTitle AS [Cargo],
                    e.BirthDate AS [Fecha Nacimiento],
                    e.HireDate AS [Fecha Contratación]
                FROM HumanResources.Employee e
                INNER JOIN Person.Person p 
                    ON e.BusinessEntityID = p.BusinessEntityID";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlDataAdapter adaptador = new SqlDataAdapter(consultaSql, conexion);
                    DataTable dt = new DataTable();

                    adaptador.Fill(dt);

                    tablaNombre.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}