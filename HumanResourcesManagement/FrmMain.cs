
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmMain : Form
    {
        private FrmLogin formularioLogin;

        private Panel panelMenu;
        private Panel panelContenido;
        private Panel panelSuperior;
        private Label lblBienvenida;
        private Label lblUsuarioSesion;
        private Label lblRolSesion;

        private readonly Color colorMenu = Color.FromArgb(30, 45, 65);
        private readonly Color colorFondo = Color.FromArgb(245, 247, 250);
        private readonly Color colorAcento = Color.FromArgb(52, 120, 180);

        public FrmMain(FrmLogin login)
        {
            InitializeComponent();

            formularioLogin = login;

            ConstruirInterfaz();
            this.Load += FrmMain_Load;
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Human Resources Management";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorFondo;

            // Ocultar los controles anteriores del diseñador.
            this.Controls.Clear();

            // MENU LATERAL
            panelMenu = new Panel();
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 245;
            panelMenu.BackColor = colorMenu;

            Label lblLogo = new Label();
            lblLogo.Text = "HUMAN RESOURCES";
            lblLogo.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(18, 25);
            lblLogo.Size = new Size(215, 35);

            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "MANAGEMENT SYSTEM";
            lblSubtitulo.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.LightGray;
            lblSubtitulo.Location = new Point(20, 60);
            lblSubtitulo.Size = new Size(210, 25);

            panelMenu.Controls.Add(lblLogo);
            panelMenu.Controls.Add(lblSubtitulo);

            int posicion = 115;

            AgregarBotonMenu("Inicio", null, ref posicion);
            AgregarBotonMenu("Directorio de empleados",
                "FrmDirectorioEmpleados", ref posicion);
            AgregarBotonMenu("Gestión de empleados",
                "FrmContactoEmpleados", ref posicion);
            AgregarBotonMenu("Departamentos",
                "FrmDepartamentos", ref posicion);
            AgregarBotonMenu("Departamentos y turnos",
                "FrmConDepaTurnos", ref posicion);
            AgregarBotonMenu("Consulta de empleados",
                "FrmConEmpl", ref posicion);
            AgregarBotonMenu("Historial salarial",
                "FrmPayHistory", ref posicion);


            // SECCION INFERIOR DEL MENU
            Panel panelPie = new Panel();
            panelPie.Dock = DockStyle.Bottom;
            panelPie.Height = 75;
            panelPie.BackColor = colorMenu;
            panelPie.Padding = new Padding(15, 12, 15, 12);

            // BOTON CERRAR SESION
            Button btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.Dock = DockStyle.Fill;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.BackColor = Color.FromArgb(170, 55, 65);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Click += btnCerrarSesion_Click;

            // Agregar boton a la seccion inferior
            panelPie.Controls.Add(btnCerrarSesion);

            // Agregar seccion inferior al menu
            panelMenu.Controls.Add(panelPie);

            // PANEL CENTRAL
            panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.BackColor = colorFondo;

            // ENCABEZADO
            panelSuperior = new Panel();
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Height = 85;
            panelSuperior.BackColor = Color.White;

            lblUsuarioSesion = new Label();
            lblUsuarioSesion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUsuarioSesion.ForeColor = colorMenu;
            lblUsuarioSesion.Location = new Point(25, 18);
            lblUsuarioSesion.AutoSize = true;

            lblRolSesion = new Label();
            lblRolSesion.Font = new Font("Segoe UI", 9);
            lblRolSesion.ForeColor = Color.Gray;
            lblRolSesion.Location = new Point(25, 45);
            lblRolSesion.AutoSize = true;

            panelSuperior.Controls.Add(lblUsuarioSesion);
            panelSuperior.Controls.Add(lblRolSesion);

            // BIENVENIDA
            Panel panelBienvenida = new Panel();
            panelBienvenida.Location = new Point(30, 115);
            panelBienvenida.Size = new Size(850, 130);
            panelBienvenida.BackColor = Color.White;

            lblBienvenida = new Label();
            lblBienvenida.Text = "¡Bienvenido al sistema!";
            lblBienvenida.Font = new Font("Segoe UI", 23, FontStyle.Bold);
            lblBienvenida.ForeColor = colorMenu;
            lblBienvenida.Location = new Point(25, 25);
            lblBienvenida.AutoSize = true;

            Label lblDescripcion = new Label();
            lblDescripcion.Text =
                "Administra la información y los procesos del talento humano.";
            lblDescripcion.Font = new Font("Segoe UI", 11);
            lblDescripcion.ForeColor = Color.Gray;
            lblDescripcion.Location = new Point(28, 75);
            lblDescripcion.AutoSize = true;

            panelBienvenida.Controls.Add(lblBienvenida);
            panelBienvenida.Controls.Add(lblDescripcion);

            // TITULO DE ACCESOS
            Label lblAccesos = new Label();
            lblAccesos.Text = "Acceso a módulos";
            lblAccesos.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblAccesos.ForeColor = colorMenu;
            lblAccesos.Location = new Point(30, 275);
            lblAccesos.AutoSize = true;

            // TARJETAS DE ACCESO
            FlowLayoutPanel panelTarjetas = new FlowLayoutPanel();
            panelTarjetas.Location = new Point(25, 320);
            panelTarjetas.Size = new Size(900, 300);
            panelTarjetas.Anchor = AnchorStyles.Top |
                                   AnchorStyles.Left |
                                   AnchorStyles.Right;
            panelTarjetas.BackColor = colorFondo;
            panelTarjetas.WrapContents = true;
            panelTarjetas.AutoScroll = true;

            AgregarTarjeta(panelTarjetas, "Empleados",
                "Consultar el directorio",
                "FrmDirectorioEmpleados");

            AgregarTarjeta(panelTarjetas, "Gestión de empleados",
                "Administrar información",
                "FrmContactoEmpleados");

            AgregarTarjeta(panelTarjetas, "Departamentos",
                "Consultar áreas de trabajo",
                "FrmDepartamentos");

            AgregarTarjeta(panelTarjetas, "Turnos",
                "Consultar departamentos y turnos",
                "FrmConDepartamentoTurnos");

            AgregarTarjeta(panelTarjetas, "Consultas",
                "Consultar información de empleados",
                "FrmConEmpl");

            AgregarTarjeta(panelTarjetas, "Historial salarial",
                "Consultar historial de pagos",
                "FrmPayHistory");

            panelContenido.Controls.Add(panelTarjetas);
            panelContenido.Controls.Add(lblAccesos);
            panelContenido.Controls.Add(panelBienvenida);
            panelContenido.Controls.Add(panelSuperior);

            this.Controls.Add(panelContenido);
            this.Controls.Add(panelMenu);
        }

        private void AgregarBotonMenu(
            string texto, string nombreFormulario, ref int posicion)
        {
            Button boton = new Button();

            boton.Text = "   " + texto;
            boton.Size = new Size(235, 45);
            boton.Location = new Point(5, posicion);
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = colorMenu;
            boton.ForeColor = Color.White;
            boton.Font = new Font("Segoe UI", 9.5F);

            boton.Click += (s, e) =>
            {
                if (nombreFormulario == null)
                {
                    MostrarInicio();
                }
                else
                {
                    AbrirFormulario(nombreFormulario);
                }
            };

            panelMenu.Controls.Add(boton);
            posicion += 49;
        }

        private void AgregarTarjeta(
            FlowLayoutPanel contenedor,
            string titulo,
            string descripcion,
            string nombreFormulario)
        {
            Panel tarjeta = new Panel();
            tarjeta.Size = new Size(270, 125);
            tarjeta.BackColor = Color.White;
            tarjeta.Margin = new Padding(10);

            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitulo.ForeColor = colorMenu;
            lblTitulo.Location = new Point(15, 15);
            lblTitulo.AutoSize = true;

            Label lblDescripcion = new Label();
            lblDescripcion.Text = descripcion;
            lblDescripcion.Font = new Font("Segoe UI", 9);
            lblDescripcion.ForeColor = Color.Gray;
            lblDescripcion.Location = new Point(15, 45);
            lblDescripcion.AutoSize = true;

            Button btnAbrir = new Button();
            btnAbrir.Text = "Abrir módulo";
            btnAbrir.Location = new Point(15, 78);
            btnAbrir.Size = new Size(115, 32);
            btnAbrir.BackColor = colorAcento;
            btnAbrir.ForeColor = Color.White;
            btnAbrir.FlatStyle = FlatStyle.Flat;
            btnAbrir.FlatAppearance.BorderSize = 0;

            btnAbrir.Click += (s, e) =>
            {
                AbrirFormulario(nombreFormulario);
            };

            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Controls.Add(lblDescripcion);
            tarjeta.Controls.Add(btnAbrir);

            contenedor.Controls.Add(tarjeta);
        }

        private void AbrirFormulario(string nombreFormulario)
        {
            try
            {
                Type tipo = Assembly.GetExecutingAssembly().GetType(
                    "HumanResourcesManagement." + nombreFormulario);

                if (tipo == null)
                {
                    MessageBox.Show(
                        "No se encontró el formulario: " + nombreFormulario,
                        "Formulario no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                Form formulario = Activator.CreateInstance(tipo) as Form;

                if (formulario == null)
                {
                    MessageBox.Show(
                        "No se pudo crear el formulario solicitado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);
                formulario.Dispose();
            }
            catch (Exception ex)
            {
                Exception error = ex;

                while (error.InnerException != null)
                {
                    error = error.InnerException;
                }

                MessageBox.Show(
                    "No se pudo abrir " + nombreFormulario +
                    "\n\nDetalle: " + error.Message,
                    "Error al abrir módulo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarInicio()
        {
            MessageBox.Show(
                "Ya te encuentras en el menú principal.",
                "Inicio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblUsuarioSesion.Text =
                "Usuario: " + SesionUsuario.Usuario;

            lblRolSesion.Text =
                "Rol: " + SesionUsuario.Rol;

            lblBienvenida.Text =
                "¡Bienvenido, " + SesionUsuario.Usuario + "!";
        }

        private void btnGestionEmpleados_Click(object sender, EventArgs e)
        {
            FrmContactoEmpleados formulario =
                new FrmContactoEmpleados();

            formulario.Show(this);
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            FrmDepartamentos formulario = new FrmDepartamentos();
            formulario.Show(this);
        }
        private void btnConDepaTurnos_Click(object sender, EventArgs e)
        {
            FrmConDepaTurnos formulario = new FrmConDepaTurnos();
            formulario.Show(this);
        }
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                SesionUsuario.IdUsuario = 0;
                SesionUsuario.Usuario = null;
                SesionUsuario.Rol = null;

                formularioLogin.Show();
                this.Close();
            }
        }
    }
}
