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
    public partial class BusEmpl : Form
    {
        private string cadenaConexion = "Server=localhost;Database=AdventureWorksLT2022;Integrated Security=True;Encrypt=False;";

        public BusEmpl()
        {
            InitializeComponent();
        }

        // Cargar todos los empleados al abrir el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            BuscarEmpleados();
        }

        // Evento del botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarEmpleados();
        }

        // Evento del botón Limpiar / Restablecer
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar las cajas de texto
            txtIdEmpleado.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCargo.Clear();

            // Recargar la lista sin filtros
            BuscarEmpleados();
        }

        // Método principal para consultar y filtrar los datos
        private void BuscarEmpleados()
        {
            // Consulta SQL con parámetros dinámicos sencillos
            string consultaSql = @"
                SELECT 
                    e.BusinessEntityID AS [ID Empleado],
                    CONCAT(p.FirstName, ' ', ISNULL(p.MiddleName + ' ', ''), p.LastName) AS [Nombre Completo],
                    e.JobTitle AS [Cargo],
                    e.BirthDate AS [Fecha Nacimiento],
                    e.HireDate AS [Fecha Contratación]
                FROM HumanResources.Employee e
                INNER JOIN Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
                WHERE 
                    (@ID IS NULL OR e.BusinessEntityID = @ID) AND
                    (@Nombre = '' OR p.FirstName LIKE '%' + @Nombre + '%') AND
                    (@Apellido = '' OR p.LastName LIKE '%' + @Apellido + '%') AND
                    (@Cargo = '' OR e.JobTitle LIKE '%' + @Cargo + '%')";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand(consultaSql, conexion);

                    // Validar si se ingresó un ID numérico
                    if (int.TryParse(txtIdEmpleado.Text.Trim(), out int idEmpleado))
                    {
                        comando.Parameters.AddWithValue("@ID", idEmpleado);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@ID", DBNull.Value);
                    }

                    // Pasar los demás texto como parámetros
                    comando.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    comando.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                    comando.Parameters.AddWithValue("@Cargo", txtCargo.Text.Trim());

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();

                    // Llenar la tabla con el resultado filtrado
                    adaptador.Fill(dt);

                    // Actualizar el DataGridView
                    tablaNombre.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolverMP_Click(object sender, EventArgs e)
        {
          //  Form2 nuevoFormulario = new Form2();

            //nuevoFormulario.Show();

            this.Hide();
        }
    }
}