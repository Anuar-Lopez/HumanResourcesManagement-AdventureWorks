
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public class FrmRegistroEmpleado : Form
    {
        private int? idEmpleado;

        private TextBox txtNombres;
        private TextBox txtApellidos;
        private TextBox txtDocumento;
        private TextBox txtLogin;
        private TextBox txtCargo;

        private DateTimePicker dtpNacimiento;
        private DateTimePicker dtpContratacion;

        private ComboBox cmbGenero;
        private ComboBox cmbEstadoCivil;
        private ComboBox cmbDepartamento;
        private ComboBox cmbTurno;

        private CheckBox chkSalarioFijo;
        private NumericUpDown nudVacaciones;
        private NumericUpDown nudIncapacidad;

        private Button btnGuardar;
        private Button btnCancelar;

        public FrmRegistroEmpleado() : this(null)
        {
        }

        public FrmRegistroEmpleado(int? empleadoId)
        {
            idEmpleado = empleadoId;

            ConstruirInterfaz();
            CargarListas();

            if (idEmpleado.HasValue)
            {
                CargarEmpleado();
                this.Text = "Modificar empleado";
                btnGuardar.Text = "Guardar cambios";
            }
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Registrar empleado";
            this.Size = new Size(900, 750);
            this.MinimumSize = new Size(850, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9F);

            Panel encabezado = new Panel();
            encabezado.Dock = DockStyle.Top;
            encabezado.Height = 105;
            encabezado.BackColor = Color.FromArgb(30, 45, 65);

            Label lblTitulo = new Label();
            lblTitulo.Text = idEmpleado.HasValue
                ? "Modificar empleado"
                : "Registrar nuevo empleado";

            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(25, 20);
            lblTitulo.AutoSize = true;

            Label lblDescripcion = new Label();
            lblDescripcion.Text =
                "Ingresa la información personal y laboral del empleado.";

            lblDescripcion.Font = new Font("Segoe UI", 9F);
            lblDescripcion.ForeColor = Color.Gainsboro;
            lblDescripcion.Location = new Point(28, 65);
            lblDescripcion.AutoSize = true;

            encabezado.Controls.Add(lblTitulo);
            encabezado.Controls.Add(lblDescripcion);

            Panel contenido = new Panel();
            contenido.Dock = DockStyle.Fill;
            contenido.AutoScroll = true;
            contenido.Padding = new Padding(25);

            GroupBox grupoPersonal = new GroupBox();
            grupoPersonal.Text = "  Información personal  ";
            grupoPersonal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grupoPersonal.ForeColor = Color.FromArgb(30, 45, 65);
            grupoPersonal.Location = new Point(25, 20);
            grupoPersonal.Size = new Size(800, 220);

            txtNombres = CrearTexto(25, 45, 340);
            txtApellidos = CrearTexto(420, 45, 340);
            txtDocumento = CrearTexto(25, 125, 340);
            dtpNacimiento = CrearFecha(420, 125);

            cmbGenero = CrearCombo(25, 185, 340);
            cmbEstadoCivil = CrearCombo(420, 185, 340);

            AgregarEtiqueta(grupoPersonal, "Nombres", 25, 25);
            AgregarEtiqueta(grupoPersonal, "Apellidos", 420, 25);
            AgregarEtiqueta(grupoPersonal, "Documento de identidad", 25, 105);
            AgregarEtiqueta(grupoPersonal, "Fecha de nacimiento", 420, 105);
            AgregarEtiqueta(grupoPersonal, "Género", 25, 165);
            AgregarEtiqueta(grupoPersonal, "Estado civil", 420, 165);

            grupoPersonal.Controls.Add(txtNombres);
            grupoPersonal.Controls.Add(txtApellidos);
            grupoPersonal.Controls.Add(txtDocumento);
            grupoPersonal.Controls.Add(dtpNacimiento);
            grupoPersonal.Controls.Add(cmbGenero);
            grupoPersonal.Controls.Add(cmbEstadoCivil);

            GroupBox grupoLaboral = new GroupBox();
            grupoLaboral.Text = "  Información laboral  ";
            grupoLaboral.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grupoLaboral.ForeColor = Color.FromArgb(30, 45, 65);
            grupoLaboral.Location = new Point(25, 255);
            grupoLaboral.Size = new Size(800, 230);

            txtLogin = CrearTexto(25, 45, 340);
            txtCargo = CrearTexto(420, 45, 340);
            dtpContratacion = CrearFecha(25, 125);
            cmbDepartamento = CrearCombo(420, 125, 340);
            cmbTurno = CrearCombo(25, 185, 340);

            AgregarEtiqueta(grupoLaboral, "Usuario laboral (LoginID)", 25, 25);
            AgregarEtiqueta(grupoLaboral, "Cargo", 420, 25);
            AgregarEtiqueta(grupoLaboral, "Fecha de contratación", 25, 105);
            AgregarEtiqueta(grupoLaboral, "Departamento", 420, 105);
            AgregarEtiqueta(grupoLaboral, "Turno", 25, 165);

            grupoLaboral.Controls.Add(txtLogin);
            grupoLaboral.Controls.Add(txtCargo);
            grupoLaboral.Controls.Add(dtpContratacion);
            grupoLaboral.Controls.Add(cmbDepartamento);
            grupoLaboral.Controls.Add(cmbTurno);

            GroupBox grupoAdicional = new GroupBox();
            grupoAdicional.Text = "  Información adicional  ";
            grupoAdicional.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grupoAdicional.ForeColor = Color.FromArgb(30, 45, 65);
            grupoAdicional.Location = new Point(25, 500);
            grupoAdicional.Size = new Size(800, 120);

            chkSalarioFijo = new CheckBox();
            chkSalarioFijo.Text = "El empleado recibe salario fijo";
            chkSalarioFijo.Location = new Point(25, 40);
            chkSalarioFijo.AutoSize = true;
            chkSalarioFijo.Font = new Font("Segoe UI", 9F);

            nudVacaciones = CrearNumero(420, 40);
            nudIncapacidad = CrearNumero(25, 90);

            AgregarEtiqueta(grupoAdicional, "Horas de vacaciones", 420, 20);
            AgregarEtiqueta(grupoAdicional, "Horas de incapacidad", 25, 70);

            grupoAdicional.Controls.Add(chkSalarioFijo);
            grupoAdicional.Controls.Add(nudVacaciones);
            grupoAdicional.Controls.Add(nudIncapacidad);

            contenido.Controls.Add(grupoPersonal);
            contenido.Controls.Add(grupoLaboral);
            contenido.Controls.Add(grupoAdicional);

            Panel pie = new Panel();
            pie.Dock = DockStyle.Bottom;
            pie.Height = 75;
            pie.BackColor = Color.White;
            pie.Padding = new Padding(20);

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar empleado";
            btnGuardar.Size = new Size(180, 42);
            btnGuardar.Location = new Point(480, 15);
            btnGuardar.BackColor = Color.FromArgb(52, 120, 180);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(130, 42);
            btnCancelar.Location = new Point(675, 15);
            btnCancelar.BackColor = Color.FromArgb(230, 233, 238);
            btnCancelar.ForeColor = Color.FromArgb(30, 45, 65);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => this.Close();

            pie.Controls.Add(btnGuardar);
            pie.Controls.Add(btnCancelar);

            this.Controls.Add(contenido);
            this.Controls.Add(pie);
            this.Controls.Add(encabezado);
        }

        private TextBox CrearTexto(int x, int y, int ancho)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 256
            };
        }

        private DateTimePicker CrearFecha(int x, int y)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(340, 30),
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today,
                Value = DateTime.Today.AddYears(-20)
            };
        }

        private ComboBox CrearCombo(int x, int y, int ancho)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 30),

                // Permite escribir para buscar, sin aceptar valores inventados.
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,

                Font = new Font("Segoe UI", 10F),
                IntegralHeight = true
            };
        }

        private NumericUpDown CrearNumero(int x, int y)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 32000,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private void AgregarEtiqueta(
            Control contenedor, string texto, int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.AutoSize = true;
            etiqueta.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            etiqueta.ForeColor = Color.FromArgb(65, 75, 90);

            contenedor.Controls.Add(etiqueta);
        }

        private void CargarListas()
        {
            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    CargarComboSQL(
                        con,
                        cmbDepartamento,
                        @"SELECT DepartmentID AS Id, Name AS Nombre
                          FROM HumanResources.Department
                          ORDER BY Name");

                    CargarComboSQL(
                        con,
                        cmbTurno,
                        @"SELECT ShiftID AS Id, Name AS Nombre
                          FROM HumanResources.Shift
                          ORDER BY Name");
                }

                DataTable generos = new DataTable();
                generos.Columns.Add("Id");
                generos.Columns.Add("Nombre");
                generos.Rows.Add("M", "Masculino");
                generos.Rows.Add("F", "Femenino");

                cmbGenero.DataSource = generos;
                cmbGenero.ValueMember = "Id";
                cmbGenero.DisplayMember = "Nombre";
                cmbGenero.SelectedIndex = -1;

                DataTable estados = new DataTable();
                estados.Columns.Add("Id");
                estados.Columns.Add("Nombre");
                estados.Rows.Add("S", "Soltero(a)");
                estados.Rows.Add("M", "Casado(a)");

                cmbEstadoCivil.DataSource = estados;
                cmbEstadoCivil.ValueMember = "Id";
                cmbEstadoCivil.DisplayMember = "Nombre";
                cmbEstadoCivil.SelectedIndex = -1;

                if (cmbDepartamento.Items.Count == 0)
                {
                    MessageBox.Show(
                        "No hay departamentos registrados en la base de datos.",
                        "Departamentos no disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                if (cmbTurno.Items.Count == 0)
                {
                    MessageBox.Show(
                        "No hay turnos registrados en la base de datos.",
                        "Turnos no disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los departamentos y turnos.\n\n" +
                    ex.Message,
                    "Error de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarComboSQL(
            SqlConnection con, ComboBox combo, string consulta)
        {
            DataTable tabla = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(consulta, con))
            {
                da.Fill(tabla);
            }

            combo.DataSource = null;
            combo.DisplayMember = "Nombre";
            combo.ValueMember = "Id";
            combo.DataSource = tabla;

            // Obliga a elegir una opción válida, en lugar de asumir la primera.
            combo.SelectedIndex = -1;
        }

        private void CargarEmpleado()
        {
            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT
                            E.FirstName,
                            E.LastName,
                            E.NationalIDNumber,
                            E.LoginID,
                            E.JobTitle,
                            E.BirthDate,
                            E.MaritalStatus,
                            E.Gender,
                            E.HireDate,
                            E.SalariedFlag,
                            E.VacationHours,
                            E.SickLeaveHours,
                            H.DepartmentID,
                            H.ShiftID
                        FROM HumanResources.Employee E
                        OUTER APPLY
                        (
                            SELECT TOP 1 DepartmentID, ShiftID
                            FROM HumanResources.EmployeeDepartmentHistory
                            WHERE BusinessEntityID = E.BusinessEntityID
                              AND EndDate IS NULL
                            ORDER BY StartDate DESC
                        ) H
                        WHERE E.BusinessEntityID = @Id";

                    using (SqlCommand cmd = new SqlCommand(consulta, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", idEmpleado.Value);

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (!r.Read())
                            {
                                MessageBox.Show("No se encontró el empleado.");
                                this.Close();
                                return;
                            }

                            txtNombres.Text = r["FirstName"].ToString();
                            txtApellidos.Text = r["LastName"].ToString();
                            txtDocumento.Text = r["NationalIDNumber"].ToString();
                            txtLogin.Text = r["LoginID"].ToString();
                            txtCargo.Text = r["JobTitle"].ToString();

                            dtpNacimiento.Value = Convert.ToDateTime(r["BirthDate"]);
                            dtpContratacion.Value = Convert.ToDateTime(r["HireDate"]);

                            cmbGenero.SelectedValue = r["Gender"].ToString();
                            cmbEstadoCivil.SelectedValue = r["MaritalStatus"].ToString();

                            chkSalarioFijo.Checked = Convert.ToBoolean(r["SalariedFlag"]);
                            nudVacaciones.Value = Convert.ToDecimal(r["VacationHours"]);
                            nudIncapacidad.Value = Convert.ToDecimal(r["SickLeaveHours"]);

                            if (r["DepartmentID"] != DBNull.Value)
                                cmbDepartamento.SelectedValue = r["DepartmentID"];

                            if (r["ShiftID"] != DBNull.Value)
                                cmbTurno.SelectedValue = r["ShiftID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el empleado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show(
                    "Completa todos los campos obligatorios.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (txtDocumento.Text.Trim().Length > 15)
            {
                MessageBox.Show(
                    "El documento no puede superar 15 caracteres.",
                    "Documento no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (dtpNacimiento.Value.Date >= DateTime.Today)
            {
                MessageBox.Show(
                    "La fecha de nacimiento debe ser anterior a hoy.",
                    "Fecha no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (dtpContratacion.Value.Date < dtpNacimiento.Value.Date)
            {
                MessageBox.Show(
                    "La fecha de contratación no puede ser anterior al nacimiento.",
                    "Fecha no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // En los ComboBox editables, SelectedIndex solo será válido
            // cuando el usuario elija una opción real de la lista.
            if (cmbGenero.SelectedIndex < 0 ||
                cmbEstadoCivil.SelectedIndex < 0 ||
                cmbDepartamento.SelectedIndex < 0 ||
                cmbTurno.SelectedIndex < 0 ||
                cmbDepartamento.SelectedValue == null ||
                cmbTurno.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona una opción válida de género, estado civil, departamento y turno.\n\n" +
                    "Puedes escribir para buscar, pero debes elegir una opción de la lista.",
                    "Selección obligatoria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            string accion = idEmpleado.HasValue
                ? "actualizar la información de este empleado"
                : "registrar este nuevo empleado";

            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas " + accion + "?\n\n" +
                txtNombres.Text.Trim() + " " + txtApellidos.Text.Trim(),
                "Confirmar operación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            GuardarEmpleado();
        }

        private void GuardarEmpleado()
        {
            using (SqlConnection con = ConexionSQL.ObtenerConexion())
            using (SqlTransaction transaccion = con.BeginTransaction())
            {
                try
                {
                    int id;

                    if (idEmpleado.HasValue)
                    {
                        id = idEmpleado.Value;

                        string actualizar = @"
                            UPDATE HumanResources.Employee
                            SET FirstName = @Nombres,
                                LastName = @Apellidos,
                                NationalIDNumber = @Documento,
                                LoginID = @Login,
                                JobTitle = @Cargo,
                                BirthDate = @Nacimiento,
                                MaritalStatus = @EstadoCivil,
                                Gender = @Genero,
                                HireDate = @Contratacion,
                                SalariedFlag = @SalarioFijo,
                                VacationHours = @Vacaciones,
                                SickLeaveHours = @Incapacidad,
                                ModifiedDate = GETDATE()
                            WHERE BusinessEntityID = @Id";

                        using (SqlCommand cmd = new SqlCommand(
                            actualizar, con, transaccion))
                        {
                            AgregarParametrosEmpleado(cmd);
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }

                        ActualizarAsignacion(con, transaccion, id);
                    }
                    else
                    {
                        string insertar = @"
                            INSERT INTO HumanResources.Employee
                            (
                                NationalIDNumber, LoginID, JobTitle,
                                BirthDate, MaritalStatus, Gender, HireDate,
                                SalariedFlag, VacationHours, SickLeaveHours,
                                CurrentFlag, FirstName, LastName
                            )
                            OUTPUT INSERTED.BusinessEntityID
                            VALUES
                            (
                                @Documento, @Login, @Cargo,
                                @Nacimiento, @EstadoCivil, @Genero, @Contratacion,
                                @SalarioFijo, @Vacaciones, @Incapacidad,
                                1, @Nombres, @Apellidos
                            )";

                        using (SqlCommand cmd = new SqlCommand(
                            insertar, con, transaccion))
                        {
                            AgregarParametrosEmpleado(cmd);
                            id = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        InsertarAsignacion(
                            con,
                            transaccion,
                            id,
                            dtpContratacion.Value.Date);
                    }

                    transaccion.Commit();

                    MessageBox.Show(
                        idEmpleado.HasValue
                            ? "Los datos del empleado se actualizaron correctamente."
                            : "El empleado se registró correctamente.\nID asignado: " + id,
                        "Operación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException ex)
                {
                    transaccion.Rollback();

                    if (ex.Number == 2601 || ex.Number == 2627)
                    {
                        MessageBox.Show(
                            "El documento o el usuario laboral ya están registrados.",
                            "Registro duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error de SQL al guardar el empleado:\n" + ex.Message,
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show(
                        "No se pudo guardar el empleado:\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void AgregarParametrosEmpleado(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Nombres", txtNombres.Text.Trim());
            cmd.Parameters.AddWithValue("@Apellidos", txtApellidos.Text.Trim());
            cmd.Parameters.AddWithValue("@Documento", txtDocumento.Text.Trim());
            cmd.Parameters.AddWithValue("@Login", txtLogin.Text.Trim());
            cmd.Parameters.AddWithValue("@Cargo", txtCargo.Text.Trim());
            cmd.Parameters.AddWithValue("@Nacimiento", dtpNacimiento.Value.Date);
            cmd.Parameters.AddWithValue("@EstadoCivil", cmbEstadoCivil.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Genero", cmbGenero.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Contratacion", dtpContratacion.Value.Date);
            cmd.Parameters.AddWithValue("@SalarioFijo", chkSalarioFijo.Checked);
            cmd.Parameters.AddWithValue("@Vacaciones", Convert.ToInt16(nudVacaciones.Value));
            cmd.Parameters.AddWithValue("@Incapacidad", Convert.ToInt16(nudIncapacidad.Value));
        }

        private void InsertarAsignacion(
            SqlConnection con,
            SqlTransaction transaccion,
            int id,
            DateTime fecha)
        {
            string consulta = @"
                INSERT INTO HumanResources.EmployeeDepartmentHistory
                (
                    BusinessEntityID,
                    DepartmentID,
                    ShiftID,
                    StartDate
                )
                VALUES
                (
                    @Id,
                    @Departamento,
                    @Turno,
                    @Fecha
                )";

            using (SqlCommand cmd = new SqlCommand(
                consulta, con, transaccion))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Departamento", cmbDepartamento.SelectedValue);
                cmd.Parameters.AddWithValue("@Turno", cmbTurno.SelectedValue);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.ExecuteNonQuery();
            }
        }

        private void ActualizarAsignacion(
            SqlConnection con,
            SqlTransaction transaccion,
            int id)
        {
            string consultaActual = @"
                SELECT TOP 1 DepartmentID, ShiftID, StartDate
                FROM HumanResources.EmployeeDepartmentHistory
                WHERE BusinessEntityID = @Id
                  AND EndDate IS NULL
                ORDER BY StartDate DESC";

            int departamentoActual = -1;
            int turnoActual = -1;
            DateTime fechaInicioActual = DateTime.MinValue;
            bool existeAsignacion = false;

            using (SqlCommand cmd = new SqlCommand(
                consultaActual, con, transaccion))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        existeAsignacion = true;
                        departamentoActual = Convert.ToInt32(r["DepartmentID"]);
                        turnoActual = Convert.ToInt32(r["ShiftID"]);
                        fechaInicioActual = Convert.ToDateTime(r["StartDate"]);
                    }
                }
            }

            int nuevoDepartamento = Convert.ToInt32(cmbDepartamento.SelectedValue);
            int nuevoTurno = Convert.ToInt32(cmbTurno.SelectedValue);

            if (existeAsignacion &&
                departamentoActual == nuevoDepartamento &&
                turnoActual == nuevoTurno)
            {
                return;
            }

            DateTime nuevaFecha = DateTime.Today;

            if (existeAsignacion && nuevaFecha <= fechaInicioActual)
                nuevaFecha = fechaInicioActual.AddDays(1);

            if (existeAsignacion)
            {
                string cerrar = @"
                    UPDATE HumanResources.EmployeeDepartmentHistory
                    SET EndDate = @Fin,
                        ModifiedDate = GETDATE()
                    WHERE BusinessEntityID = @Id
                      AND EndDate IS NULL";

                using (SqlCommand cmd = new SqlCommand(
                    cerrar, con, transaccion))
                {
                    cmd.Parameters.AddWithValue("@Fin", nuevaFecha.AddDays(-1));
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            InsertarAsignacion(con, transaccion, id, nuevaFecha);
        }
    }
}
