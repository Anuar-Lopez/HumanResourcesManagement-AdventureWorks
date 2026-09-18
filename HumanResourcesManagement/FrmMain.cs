using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmMain : Form
    {
        private FrmLogin formularioLogin;
        public FrmMain(FrmLogin login)
        {
            InitializeComponent();

            formularioLogin = login;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Usuario: " + SesionUsuario.Usuario;
            lblRol.Text = "Rol: " + SesionUsuario.Rol;
        }



        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
        
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // Limpiar los datos de la sesión
                SesionUsuario.IdUsuario = 0;
                SesionUsuario.Usuario = null;
                SesionUsuario.Rol = null;

                // Mostrar nuevamente el Login
                formularioLogin.Show();

                // Cerrar el formulario principal
                this.Close();
            }
        }
    }
}
