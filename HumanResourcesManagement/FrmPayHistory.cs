using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    /// <summary>
    /// Consulta del historial salarial de un empleado y registro
    /// de nuevos cambios de tarifa.
    /// </summary>
    public partial class FrmPayHistory : Form
    {
        // Frecuencias de pago de AdventureWorks: 1 = mensual, 2 = quincenal.
        private class ItemFrecuencia
        {
            public string Texto;
            public byte Valor;

            public override string ToString()
            {
                return Texto;
            }
        }

        private TextBox txtId;
        private Label lblResumen;
        private Label lblTotal;

        private DateTimePicker dtpFecha;
        private TextBox txtTarifa;
        private ComboBox cmbFrecuencia;

        private DataGridView dgvHistorial;

        public FrmPayHistory()
        {
            InitializeComponent();

            ConstruirInterfaz();
        }

        private void FrmPayHistory_Load(object sender, EventArgs e)
        {
            txtId.Focus();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Historial salarial - AdventureWorks";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = EstiloUI.Fondo;
            this.Font = new Font("Segoe UI", 9F);

            Panel encabezado = EstiloUI.CrearEncabezado(
                "Historial salarial",
                "Consulta los cambios de tarifa de un empleado y registra nuevos.",
                out lblTotal);

            lblTotal.Text = "Registros encontrados: 0";

            // PANEL DE ACCIONES
            Panel panelAcciones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = EstiloUI.Fondo
            };

            Button btnExportar = EstiloUI.CrearBoton(
                "Exportar CSV", 20, 17, 130,
                EstiloUI.Verde, Color.White);

            Button btnVolver = EstiloUI.CrearBoton(
                "Volver al menú principal", 970, 17, 195,
                EstiloUI.Marino, Color.White);

            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnExportar.Click += (s, e) =>
            {
                string sufijo = txtId.Text.Trim();

                ExportadorCSV.ExportarDataGridView(
                    dgvHistorial,
                    "Historial_Salarial_" + sufijo);
            };

            btnVolver.Click += (s, e) => this.Close();

            panelAcciones.Controls.Add(btnExportar);
            panelAcciones.Controls.Add(btnVolver);

            // PANEL DE CONSULTA
            Panel panelConsulta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White
            };

            txtId = EstiloUI.CrearTexto(25, 40, 120);
            txtId.MaxLength = 9;

            txtId.KeyPress += (s, e) =>
            {
                // Solo números en el campo ID.
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };

            txtId.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CargarHistorial();
                    e.SuppressKeyPress = true;
                }
            };

            Button btnConsultar = EstiloUI.CrearBoton(
                "Consultar", 160, 36, 110,
                EstiloUI.Acento, Color.White);

            btnConsultar.Click += (s, e) => CargarHistorial();

            lblResumen = new Label
            {
                Location = new Point(295, 36),
                Size = new Size(850, 45),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = EstiloUI.Marino,
                Anchor = AnchorStyles.Top | AnchorStyles.Left |
                         AnchorStyles.Right,
                Text = "Ingresa el ID de un empleado y presiona Consultar."
            };

            panelConsulta.Controls.Add(
                EstiloUI.CrearEtiqueta("ID del empleado:", 25, 12));
            panelConsulta.Controls.Add(txtId);
            panelConsulta.Controls.Add(btnConsultar);
            panelConsulta.Controls.Add(lblResumen);

            // PANEL DE NUEVO REGISTRO
            Panel panelNuevo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = EstiloUI.Fondo
            };

            dtpFecha = new DateTimePicker
            {
                Location = new Point(25, 45),
                Size = new Size(160, 30),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F),
                Value = DateTime.Today
            };

            txtTarifa = EstiloUI.CrearTexto(205, 45, 150);

            cmbFrecuencia = EstiloUI.CrearCombo(375, 45, 190);

            cmbFrecuencia.Items.Add(new ItemFrecuencia
            {
                Texto = "1 - Mensual",
                Valor = 1
            });

            cmbFrecuencia.Items.Add(new ItemFrecuencia
            {
                Texto = "2 - Quincenal",
                Valor = 2
            });

            cmbFrecuencia.SelectedIndex = 0;

            Button btnGuardar = EstiloUI.CrearBoton(
                "Guardar registro", 585, 40, 160,
                EstiloUI.Acento, Color.White);

            btnGuardar.Click += (s, e) => GuardarNuevoHistorial();

            txtTarifa.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GuardarNuevoHistorial();
                    e.SuppressKeyPress = true;
                }
            };

            Label lblNuevo = new Label
            {
                Text = "Registrar nuevo cambio salarial",
                Location = new Point(25, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = EstiloUI.Marino
            };

            panelNuevo.Controls.Add(lblNuevo);
            panelNuevo.Controls.Add(
                EstiloUI.CrearEtiqueta("Fecha del cambio", 25, 28));
            panelNuevo.Controls.Add(
                EstiloUI.CrearEtiqueta("Tarifa", 205, 28));
            panelNuevo.Controls.Add(
                EstiloUI.CrearEtiqueta("Frecuencia de pago", 375, 28));
            panelNuevo.Controls.Add(dtpFecha);
            panelNuevo.Controls.Add(txtTarifa);
            panelNuevo.Controls.Add(cmbFrecuencia);
            panelNuevo.Controls.Add(btnGuardar);

            // TABLA
            dgvHistorial = new DataGridView();
            EstiloUI.ConfigurarGrid(dgvHistorial);

            dgvHistorial.DataBindingComplete += (s, e) =>
            {
                if (dgvHistorial.Columns["Tarifa"] != null)
                    dgvHistorial.Columns["Tarifa"]
                        .DefaultCellStyle.Format = "N2";

                if (dgvHistorial.Columns["Fecha del cambio"] != null)
                    dgvHistorial.Columns["Fecha del cambio"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvHistorial.Columns["Última modificación"] != null)
                    dgvHistorial.Columns["Última modificación"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            };

            this.Controls.Add(dgvHistorial);
            this.Controls.Add(panelNuevo);
            this.Controls.Add(panelConsulta);
            this.Controls.Add(panelAcciones);
            this.Controls.Add(encabezado);
        }

        private bool TryObtenerIdEmpleado(out int id)
        {
            if (!int.TryParse(txtId.Text.Trim(), out id) || id <= 0)
            {
                MessageBox.Show(
                    "Ingresa un ID de empleado válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // Acepta "25.50" y "25,50" sin confundir el punto con separador
        // de miles.
        private static bool TryParseTarifa(string texto, out decimal tarifa)
        {
            texto = (texto ?? "").Trim();

            return decimal.TryParse(
                       texto,
                       NumberStyles.AllowDecimalPoint,
                       CultureInfo.CurrentCulture,
                       out tarifa)
                   || decimal.TryParse(
                       texto,
                       NumberStyles.AllowDecimalPoint,
                       CultureInfo.InvariantCulture,
                       out tarifa);
        }

        // Devuelve null si el empleado no existe.
        private string ObtenerResumenEmpleado(SqlConnection con, int id)
        {
            string consulta = @"
                SELECT
                    E.FirstName,
                    E.LastName,
                    E.JobTitle,
                    E.CurrentFlag,
                    P.Rate,
                    P.RateChangeDate
                FROM HumanResources.Employee E
                OUTER APPLY
                (
                    SELECT TOP 1 Rate, RateChangeDate
                    FROM HumanResources.EmployeePayHistory
                    WHERE BusinessEntityID = E.BusinessEntityID
                    ORDER BY RateChangeDate DESC
                ) P
                WHERE E.BusinessEntityID = @Id;";

            using (SqlCommand cmd = new SqlCommand(consulta, con))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return null;

                    string resumen =
                        r["FirstName"] + " " + r["LastName"] +
                        "  |  " + r["JobTitle"] +
                        "  |  " +
                        (Convert.ToBoolean(r["CurrentFlag"])
                            ? "Activo"
                            : "Inactivo");

                    if (r["Rate"] != DBNull.Value)
                    {
                        resumen +=
                            "\nTarifa vigente: " +
                            Convert.ToDecimal(r["Rate"]).ToString("N2") +
                            " (desde " +
                            Convert.ToDateTime(r["RateChangeDate"])
                                .ToString("dd/MM/yyyy") + ")";
                    }
                    else
                    {
                        resumen += "\nEste empleado aún no tiene tarifas registradas.";
                    }

                    return resumen;
                }
            }
        }

        private void CargarHistorial()
        {
            int id;

            if (!TryObtenerIdEmpleado(out id))
                return;

            string consulta = @"
                SELECT
                    BusinessEntityID AS [ID Empleado],
                    RateChangeDate AS [Fecha del cambio],
                    Rate AS [Tarifa],
                    CASE PayFrequency
                        WHEN 1 THEN 'Mensual'
                        WHEN 2 THEN 'Quincenal'
                        ELSE CAST(PayFrequency AS VARCHAR(10))
                    END AS [Frecuencia de pago],
                    ModifiedDate AS [Última modificación]
                FROM HumanResources.EmployeePayHistory
                WHERE BusinessEntityID = @Id
                ORDER BY RateChangeDate DESC;";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    string resumen = ObtenerResumenEmpleado(con, id);

                    if (resumen == null)
                    {
                        dgvHistorial.DataSource = null;
                        lblResumen.Text = "";
                        lblTotal.Text = "Registros encontrados: 0";

                        MessageBox.Show(
                            "No existe un empleado con el ID " + id + ".",
                            "Empleado no encontrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return;
                    }

                    lblResumen.Text = resumen;

                    using (SqlCommand cmd = new SqlCommand(consulta, con))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        DataTable tabla = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tabla);
                        }

                        dgvHistorial.DataSource = tabla;

                        lblTotal.Text =
                            "Registros encontrados: " + tabla.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo consultar el historial.\n\n" + ex.Message,
                    "Error SQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GuardarNuevoHistorial()
        {
            int id;

            if (!TryObtenerIdEmpleado(out id))
                return;

            decimal tarifa;

            if (!TryParseTarifa(txtTarifa.Text, out tarifa) || tarifa <= 0)
            {
                MessageBox.Show(
                    "Ingresa una tarifa válida mayor a 0.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ItemFrecuencia frecuencia =
                cmbFrecuencia.SelectedItem as ItemFrecuencia;

            if (frecuencia == null)
            {
                MessageBox.Show(
                    "Selecciona la frecuencia de pago.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DateTime fecha = dtpFecha.Value.Date;

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    string resumen = ObtenerResumenEmpleado(con, id);

                    if (resumen == null)
                    {
                        MessageBox.Show(
                            "No existe un empleado con el ID " + id + ".",
                            "Empleado no encontrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    DialogResult respuesta = MessageBox.Show(
                        "¿Registrar una tarifa de " + tarifa.ToString("N2") +
                        " (" + frecuencia.Texto + ") con fecha " +
                        fecha.ToString("dd/MM/yyyy") + " para:\n\n" +
                        resumen.Split('\n')[0] + "?",
                        "Confirmar operación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (respuesta != DialogResult.Yes)
                        return;

                    // Evitar duplicados por empleado y fecha.
                    using (SqlCommand check = new SqlCommand(@"
                        SELECT COUNT(1)
                        FROM HumanResources.EmployeePayHistory
                        WHERE BusinessEntityID = @Id
                          AND RateChangeDate = @Fecha;", con))
                    {
                        check.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        check.Parameters.Add("@Fecha", SqlDbType.DateTime)
                            .Value = fecha;

                        if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show(
                                "Ya existe un registro salarial para este " +
                                "empleado en la fecha seleccionada.",
                                "Registro duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    using (SqlCommand insertar = new SqlCommand(@"
                        INSERT INTO HumanResources.EmployeePayHistory
                            (BusinessEntityID, RateChangeDate, Rate,
                             PayFrequency, ModifiedDate)
                        VALUES
                            (@Id, @Fecha, @Tarifa, @Frecuencia, GETDATE());",
                        con))
                    {
                        insertar.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        insertar.Parameters.Add("@Fecha", SqlDbType.DateTime)
                            .Value = fecha;
                        insertar.Parameters.Add("@Tarifa", SqlDbType.Money)
                            .Value = tarifa;
                        insertar.Parameters.Add("@Frecuencia", SqlDbType.TinyInt)
                            .Value = frecuencia.Valor;

                        insertar.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "El cambio salarial se registró correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtTarifa.Clear();
                CargarHistorial();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "La base de datos rechazó la tarifa porque no cumple " +
                        "sus reglas (por ejemplo, un rango permitido).\n\n" +
                        ex.Message,
                        "Tarifa no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo guardar el registro salarial.\n\n" +
                        ex.Message,
                        "Error SQL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el registro salarial.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
