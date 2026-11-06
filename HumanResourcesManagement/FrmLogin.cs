
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmLogin : Form
    {
        private Panel panelDerecho;
        private LinkLabel linkRegistro;

        public FrmLogin()
        {
            InitializeComponent();

            ConstruirInterfaz();
            ConfigurarRegistro();
        }

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();

            this.Text = "Human Resources Management | Acceso";
            this.Size = new Size(1050, 680);
            this.MinimumSize = new Size(950, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            // PANEL IZQUIERDO
            Panel panelIzquierdo = new Panel();
            panelIzquierdo.Dock = DockStyle.Left;
            panelIzquierdo.Width = 390;
            panelIzquierdo.BackColor =
                Color.FromArgb(30, 45, 65);

            Label lblMarca = new Label();
            lblMarca.Text = "HUMAN RESOURCES";
            lblMarca.Font =
                new Font("Segoe UI", 19F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(35, 85);
            lblMarca.AutoSize = true;

            Label lblSubmarca = new Label();
            lblSubmarca.Text = "MANAGEMENT SYSTEM";
            lblSubmarca.Font = new Font("Segoe UI", 9F);
            lblSubmarca.ForeColor = Color.LightGray;
            lblSubmarca.Location = new Point(39, 125);
            lblSubmarca.AutoSize = true;

            Label lblBienvenida = new Label();
            lblBienvenida.Text =
                "Gestiona el talento.\nImpulsa el futuro.";
            lblBienvenida.Font =
                new Font("Segoe UI", 25F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.Location = new Point(35, 245);
            lblBienvenida.Size = new Size(320, 125);

            Label lblDescripcion = new Label();
            lblDescripcion.Text =
                "Una plataforma para administrar empleados,\n" +
                "departamentos y procesos de recursos humanos.";
            lblDescripcion.Font = new Font("Segoe UI", 10F);
            lblDescripcion.ForeColor =
                Color.FromArgb(210, 220, 230);
            lblDescripcion.Location = new Point(39, 390);
            lblDescripcion.Size = new Size(320, 60);

            Label lblVersion = new Label();
            lblVersion.Text =
                "ADVENTURE WORKS | SISTEMA DE GESTIÓN";
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.Location = new Point(35, 605);
            lblVersion.AutoSize = true;
            lblVersion.Anchor =
                AnchorStyles.Bottom | AnchorStyles.Left;

            panelIzquierdo.Controls.Add(lblMarca);
            panelIzquierdo.Controls.Add(lblSubmarca);
            panelIzquierdo.Controls.Add(lblBienvenida);
            panelIzquierdo.Controls.Add(lblDescripcion);
            panelIzquierdo.Controls.Add(lblVersion);

            // PANEL DERECHO
            panelDerecho = new Panel();
            panelDerecho.Dock = DockStyle.Fill;
            panelDerecho.BackColor = Color.White;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Bienvenido";
            lblTitulo.Font =
                new Font("Segoe UI", 25F, FontStyle.Bold);
            lblTitulo.ForeColor =
                Color.FromArgb(30, 45, 65);
            lblTitulo.Location = new Point(85, 105);
            lblTitulo.AutoSize = true;

            Label lblInstruccion = new Label();
            lblInstruccion.Text =
                "Ingresa tus datos para acceder al sistema.";
            lblInstruccion.Font = new Font("Segoe UI", 10F);
            lblInstruccion.ForeColor = Color.Gray;
            lblInstruccion.Location = new Point(90, 155);
            lblInstruccion.AutoSize = true;

            Label lblUsuario = new Label();
            lblUsuario.Text = "USUARIO";
            lblUsuario.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsuario.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblUsuario.Location = new Point(90, 220);
            lblUsuario.AutoSize = true;

            // CAMPO USUARIO
            txtUsuario.Location = new Point(90, 250);
            txtUsuario.Size = new Size(15, 36);
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.BackColor =
                Color.FromArgb(248, 249, 251);
            txtUsuario.BorderStyle =
                BorderStyle.FixedSingle;
            txtUsuario.Text = "";
            txtUsuario.Anchor =
                AnchorStyles.Top | AnchorStyles.Left |
                AnchorStyles.Right;

            Label lblContrasena = new Label();
            lblContrasena.Text = "CONTRASEÑA";
            lblContrasena.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContrasena.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblContrasena.Location = new Point(90, 315);
            lblContrasena.AutoSize = true;

            // CAMPO CONTRASEÑA
            txtContrasena.Location = new Point(90, 345);
            txtContrasena.Size = new Size(15, 36);
            txtContrasena.Font = new Font("Segoe UI", 11F);
            txtContrasena.BackColor =
                Color.FromArgb(248, 249, 251);
            txtContrasena.BorderStyle =
                BorderStyle.FixedSingle;
            txtContrasena.UseSystemPasswordChar = true;
            txtContrasena.Text = "";
            txtContrasena.Anchor =
                AnchorStyles.Top | AnchorStyles.Left |
                AnchorStyles.Right;

            // BOTON INICIAR SESION
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.Location = new Point(90, 420);
            btnIniciarSesion.Size = new Size(210, 45);
            btnIniciarSesion.BackColor =
                Color.FromArgb(52, 120, 180);
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            btnIniciarSesion.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIniciarSesion.Cursor = Cursors.Hand;

            // BOTON SALIR
            btnSalir.Text = "Salir";
            btnSalir.Location = new Point(320, 420);
            btnSalir.Size = new Size(110, 45);
            btnSalir.BackColor = Color.White;
            btnSalir.ForeColor =
                Color.FromArgb(80, 90, 105);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.FlatAppearance.BorderColor =
                Color.FromArgb(210, 215, 220);
            btnSalir.Font = new Font("Segoe UI", 10F);
            btnSalir.Cursor = Cursors.Hand;

            // AGREGAR CONTROLES
            panelDerecho.Controls.Add(lblTitulo);
            panelDerecho.Controls.Add(lblInstruccion);
            panelDerecho.Controls.Add(lblUsuario);
            panelDerecho.Controls.Add(txtUsuario);
            panelDerecho.Controls.Add(lblContrasena);
            panelDerecho.Controls.Add(txtContrasena);
            panelDerecho.Controls.Add(btnIniciarSesion);
            panelDerecho.Controls.Add(btnSalir);

            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);

          
            this.AcceptButton = btnIniciarSesion;
        }

        private void ConfigurarRegistro()
        {
            linkRegistro = new LinkLabel();

            linkRegistro.Text =
                "¿No tienes una cuenta? Regístrate aquí";

            linkRegistro.Font =
                new Font("Segoe UI", 9F);

            linkRegistro.LinkColor =
                Color.FromArgb(52, 120, 180);

            linkRegistro.ActiveLinkColor =
                Color.FromArgb(30, 45, 65);

            linkRegistro.AutoSize = true;
            linkRegistro.Location = new Point(90, 505);

            linkRegistro.LinkClicked += (s, e) =>
            {
                using (FrmRegistro registro = new FrmRegistro())
                {
                    registro.ShowDialog(this);
                }
            };

            panelDerecho.Controls.Add(linkRegistro);
            linkRegistro.BringToFront();
        }
        //Iniciar Sesión
        private void btnIniciarSesion_Click(
            object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show(
                    "Debes ingresar tu usuario y contraseña.",
                    "Campos obligatorios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (SqlConnection conexion =
                    ConexionSQL.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT IdUsuario, Usuario, Contrasena, Rol
                        FROM dbo.Usuarios
                        WHERE Usuario = @Usuario
                          AND Activo = 1";

                    int idUsuario = 0;
                    string usuarioBD = "";
                    string contrasenaBD = "";
                    string rol = "";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@Usuario", usuario);

                        using (SqlDataReader lector =
                            comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                idUsuario = Convert.ToInt32(
                                    lector["IdUsuario"]);

                                usuarioBD =
                                    lector["Usuario"].ToString();

                                contrasenaBD =
                                    lector["Contrasena"].ToString();

                                rol = lector["Rol"].ToString();
                            }
                        }
                    }

                    if (idUsuario == 0)
                    {
                        MessageBox.Show(
                            "El usuario o la contraseña son incorrectos.",
                            "Acceso denegado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    bool contrasenaValida;

                    // VERIFICAR CONTRASEÑA CON HASH
                    if (SeguridadContrasenas.EsHash(contrasenaBD))
                    {
                        contrasenaValida =
                            SeguridadContrasenas.Verificar(
                                contrasena, contrasenaBD);
                    }
                    else
                    {
                        // COMPATIBILIDAD CON CUENTAS ANTIGUAS
                        contrasenaValida =
                            string.Equals(
                                contrasena,
                                contrasenaBD,
                                StringComparison.Ordinal);

                        // ACTUALIZAR CONTRASEÑA A HASH
                        if (contrasenaValida)
                        {
                            string nuevoHash =
                                SeguridadContrasenas.CrearHash(
                                    contrasena);

                            using (SqlCommand actualizar =
                                new SqlCommand(@"
                                    UPDATE dbo.Usuarios
                                    SET Contrasena = @Contrasena
                                    WHERE IdUsuario = @IdUsuario",
                                    conexion))
                            {
                                actualizar.Parameters.AddWithValue(
                                    "@Contrasena", nuevoHash);

                                actualizar.Parameters.AddWithValue(
                                    "@IdUsuario", idUsuario);

                                actualizar.ExecuteNonQuery();
                            }
                        }
                    }

                    if (!contrasenaValida)
                    {
                        MessageBox.Show(
                            "El usuario o la contraseña son incorrectos.",
                            "Acceso denegado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    // GUARDAR SESION
                    SesionUsuario.IdUsuario = idUsuario;
                    SesionUsuario.Usuario = usuarioBD;
                    SesionUsuario.Rol = rol;

                    FrmMain formularioPrincipal =
                        new FrmMain(this);

                    txtContrasena.Clear();

                    this.Hide();
                    formularioPrincipal.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible iniciar sesión.\n\n" +
                    ex.Message,
                    "Error de autenticación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        //Salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas salir de la aplicación?",
                "Salir",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
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
    }
}
