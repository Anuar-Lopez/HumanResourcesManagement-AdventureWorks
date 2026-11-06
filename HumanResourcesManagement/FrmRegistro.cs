
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public class FrmRegistro : Form
    {
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private TextBox txtConfirmar;
        private Button btnRegistrar;
        private Button btnCancelar;

        public FrmRegistro()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Crear cuenta";
            this.Size = new Size(440, 490);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Crear una cuenta";
            lblTitulo.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(30, 45, 65);
            lblTitulo.Location = new Point(35, 25);
            lblTitulo.AutoSize = true;

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Completa los siguientes datos para registrarte.";
            lblDescripcion.Font = new Font("Segoe UI", 9);
            lblDescripcion.ForeColor = Color.Gray;
            lblDescripcion.Location = new Point(38, 70);
            lblDescripcion.AutoSize = true;

            CrearEtiqueta("Nombre de usuario", 38, 115);

            txtUsuario = new TextBox();
            txtUsuario.Location = new Point(40, 140);
            txtUsuario.Size = new Size(340, 30);
            txtUsuario.Font = new Font("Segoe UI", 11);
            txtUsuario.MaxLength = 100;

            CrearEtiqueta("Contraseña", 38, 185);

            txtContrasena = new TextBox();
            txtContrasena.Location = new Point(40, 210);
            txtContrasena.Size = new Size(340, 30);
            txtContrasena.Font = new Font("Segoe UI", 11);
            txtContrasena.UseSystemPasswordChar = true;

            CrearEtiqueta("Confirmar contraseña", 38, 255);

            txtConfirmar = new TextBox();
            txtConfirmar.Location = new Point(40, 280);
            txtConfirmar.Size = new Size(340, 30);
            txtConfirmar.Font = new Font("Segoe UI", 11);
            txtConfirmar.UseSystemPasswordChar = true;

            btnRegistrar = new Button();
            btnRegistrar.Text = "Crear cuenta";
            btnRegistrar.Location = new Point(40, 345);
            btnRegistrar.Size = new Size(160, 42);
            btnRegistrar.BackColor = Color.FromArgb(40, 110, 170);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRegistrar.Cursor = Cursors.Hand;
            btnRegistrar.Click += btnRegistrar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(220, 345);
            btnCancelar.Size = new Size(160, 42);
            btnCancelar.BackColor = Color.FromArgb(230, 233, 238);
            btnCancelar.ForeColor = Color.FromArgb(30, 45, 65);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblDescripcion);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(txtConfirmar);
            this.Controls.Add(btnRegistrar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnRegistrar;
            this.CancelButton = btnCancelar;
        }

        private void CrearEtiqueta(string texto, int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            etiqueta.ForeColor = Color.FromArgb(50, 60, 75);
            etiqueta.Location = new Point(x, y);
            etiqueta.AutoSize = true;

            this.Controls.Add(etiqueta);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;
            string confirmar = txtConfirmar.Text;

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(confirmar))
            {
                MessageBox.Show(
                    "Todos los campos son obligatorios.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (usuario.Length < 4)
            {
                MessageBox.Show(
                    "El nombre de usuario debe tener al menos 4 caracteres.",
                    "Usuario inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (usuario.Contains(" "))
            {
                MessageBox.Show(
                    "El nombre de usuario no puede contener espacios.",
                    "Usuario inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (contrasena.Length < 8)
            {
                MessageBox.Show(
                    "La contraseña debe tener al menos 8 caracteres.",
                    "Contraseña insegura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (contrasena != confirmar)
            {
                MessageBox.Show(
                    "Las contraseñas no coinciden.",
                    "Verifica tu contraseña",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hash = SeguridadContrasenas.CrearHash(contrasena);

                using (SqlConnection conexion =
                    ConexionSQL.ObtenerConexion())
                {
                    string consulta = @"
                        INSERT INTO dbo.Usuarios
                            (Usuario, Contrasena, Rol, Activo)
                        VALUES
                            (@Usuario, @Contrasena, @Rol, 1)";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Usuario", usuario);
                        comando.Parameters.AddWithValue("@Contrasena", hash);
                        comando.Parameters.AddWithValue("@Rol", "Usuario");

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "¡Tu cuenta se creó correctamente!\nYa puedes iniciar sesión.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    MessageBox.Show(
                        "Ese nombre de usuario ya está registrado. Elige otro.",
                        "Usuario existente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "No fue posible registrar la cuenta.\n\n" + ex.Message,
                        "Error de base de datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al registrar la cuenta.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
