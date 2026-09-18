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
  
    public partial class FrmLogin : Form
    {
        private string cadenaConexion =
  @"Server=DESKTOP-68JDOQ8\SQLEXPRESS;
      Database=AdventureWorksLT2022;
      Trusted_Connection=True;";
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // 1. Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show(
                    "El usuario y la contraseña son obligatorios.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                // 2. Abrir conexión con SQL Server
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // 3. Consulta para buscar el usuario
                    string consulta = @"
                SELECT IdUsuario, Usuario, Rol
                FROM dbo.Usuarios
                WHERE Usuario = @Usuario
                  AND Contrasena = @Contrasena
                  AND Activo = 1";

                    // 4. Crear comando SQL
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // 5. Enviar los datos escritos en el formulario
                        comando.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                        comando.Parameters.AddWithValue("@Contrasena", txtContrasena.Text);

                        // 6. Ejecutar consulta
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            // 7. Verificar si encontró el usuario
                            if (lector.Read())
                            {
                                // Guardar los datos del usuario en la sesión
                                SesionUsuario.IdUsuario = Convert.ToInt32(lector["IdUsuario"]);
                                SesionUsuario.Usuario = lector["Usuario"].ToString();
                                SesionUsuario.Rol = lector["Rol"].ToString();

                                // Crear el formulario principal y enviarle una referencia al Login
                                FrmMain formularioPrincipal = new FrmMain(this);

                                // Ocultar el formulario de Login
                                this.Hide();

                                // Mostrar el formulario principal
                                formularioPrincipal.Show();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "El usuario o la contraseña son incorrectos.",
                                    "Error de autenticación",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo realizar la autenticación.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
