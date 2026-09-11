using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public partial class FrmPayHistory : Form
    {
        private Panel panelSuperior;
        private Label lblBusinessEntityID;
        private TextBox txtBusinessEntityID;
        private Button btnBuscar;

        private GroupBox gbNuevoRegistro;
        private Label lblRateChangeDate;
        private DateTimePicker dtpRateChangeDate;
        private Label lblRate;
        private TextBox txtRate;
        private Label lblPayFrequency;
        private ComboBox cbPayFrequency;
        private Button btnGuardar;

        private DataGridView dgvPayHistory;
        private Button btnSalir;

        public FrmPayHistory()
        {
            InitializeComponent();
            ConstruirInterfaz();
            ConfigurarDataGridView();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Historial Salarial (Pay History)";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel Superior: Búsqueda
            panelSuperior = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(240, 240, 240) };

            lblBusinessEntityID = new Label { Text = "ID Empleado:", Location = new Point(15, 20), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtBusinessEntityID = new TextBox { Location = new Point(105, 17), Width = 100, Font = new Font("Segoe UI", 9.5F) };
            btnBuscar = new Button { Text = "Consultar", Location = new Point(215, 15), Height = 30, Width = 90, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(30, 61, 89), ForeColor = Color.White };

            btnBuscar.Click += (s, e) => CargarHistorial();
            txtBusinessEntityID.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnBuscar.PerformClick(); e.SuppressKeyPress = true; } };

            panelSuperior.Controls.AddRange(new Control[] { lblBusinessEntityID, txtBusinessEntityID, btnBuscar });

            // GroupBox: Nuevo Registro
            gbNuevoRegistro = new GroupBox
            {
                Text = " Registrar Nuevo Cambio Salarial ",
                Dock = DockStyle.Top,
                Height = 100,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            lblRateChangeDate = new Label { Text = "Fecha Cambio:", Location = new Point(15, 30), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Regular) };
            dtpRateChangeDate = new DateTimePicker { Location = new Point(15, 52), Width = 130, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9F, FontStyle.Regular) };

            lblRate = new Label { Text = "Tarifa (Rate):", Location = new Point(160, 30), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Regular) };
            txtRate = new TextBox { Location = new Point(160, 52), Width = 110, Font = new Font("Segoe UI", 9F, FontStyle.Regular) };

            lblPayFrequency = new Label { Text = "Frecuencia de Pago:", Location = new Point(285, 30), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Regular) };
            cbPayFrequency = new ComboBox { Location = new Point(285, 52), Width = 160, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9F, FontStyle.Regular) };

            // Frecuencias según AdventureWorks (1 = Mensual, 2 = Quincenal/Bimensual)
            cbPayFrequency.Items.Add(new { Text = "1 - Mensual", Value = 1 });
            cbPayFrequency.Items.Add(new { Text = "2 - Quincenal", Value = 2 });
            cbPayFrequency.DisplayMember = "Text";
            cbPayFrequency.ValueMember = "Value";
            cbPayFrequency.SelectedIndex = 0;

            btnGuardar = new Button { Text = "Guardar Registro", Location = new Point(460, 48), Height = 30, Width = 130, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnGuardar.Click += (s, e) => GuardarNuevoHistorial();

            btnSalir = new Button { Text = "Salir", Location = new Point(760, 48), Height = 30, Width = 100, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSalir.Click += (s, e) => this.Close();

            gbNuevoRegistro.Controls.AddRange(new Control[] { lblRateChangeDate, dtpRateChangeDate, lblRate, txtRate, lblPayFrequency, cbPayFrequency, btnGuardar, btnSalir });

            // DataGridView
            dgvPayHistory = new DataGridView { Dock = DockStyle.Fill };

            this.Controls.Add(dgvPayHistory);
            this.Controls.Add(gbNuevoRegistro);
            this.Controls.Add(panelSuperior);
        }

        private void ConfigurarDataGridView()
        {
            dgvPayHistory.ReadOnly = true;
            dgvPayHistory.AllowUserToAddRows = false;
            dgvPayHistory.AllowUserToDeleteRows = false;
            dgvPayHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayHistory.MultiSelect = false;
            dgvPayHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPayHistory.EnableHeadersVisualStyles = false;
            dgvPayHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 61, 89);
            dgvPayHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPayHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvPayHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
        }

        private void CargarHistorial()
        {
            if (!int.TryParse(txtBusinessEntityID.Text.Trim(), out int empId))
            {
                MessageBox.Show("Ingrese un ID de empleado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"SELECT 
                                BusinessEntityID AS [ID Empleado],
                                RateChangeDate AS [Fecha de Cambio],
                                Rate AS [Tarifa Salarial],
                                PayFrequency AS [Frecuencia Pago],
                                ModifiedDate AS [Última Modificación]
                             FROM HumanResources.EmployeePayHistory
                             WHERE BusinessEntityID = @BusinessEntityID
                             ORDER BY RateChangeDate DESC";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BusinessEntityID", empId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvPayHistory.DataSource = dt;

                        if (dgvPayHistory.Columns["Tarifa Salarial"] != null)
                            dgvPayHistory.Columns["Tarifa Salarial"].DefaultCellStyle.Format = "C2";
                        if (dgvPayHistory.Columns["Fecha de Cambio"] != null)
                            dgvPayHistory.Columns["Fecha de Cambio"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        if (dgvPayHistory.Columns["Última Modificación"] != null)
                            dgvPayHistory.Columns["Última Modificación"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No se encontraron registros salariales para este empleado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar historial: {ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarNuevoHistorial()
        {
            if (!int.TryParse(txtBusinessEntityID.Text.Trim(), out int empId))
            {
                MessageBox.Show("Por favor consulte un ID de empleado válido antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtRate.Text.Trim(), out decimal rate) || rate <= 0)
            {
                MessageBox.Show("Ingrese una tarifa salarial válida mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaCambio = dtpRateChangeDate.Value.Date;
            dynamic selectedFreq = cbPayFrequency.SelectedItem;
            byte payFreq = (byte)selectedFreq.Value;

            // Validar Duplicados
            string checkQuery = @"SELECT COUNT(1) FROM HumanResources.EmployeePayHistory 
                                  WHERE BusinessEntityID = @BusinessEntityID AND RateChangeDate = @RateChangeDate";

            string insertQuery = @"INSERT INTO HumanResources.EmployeePayHistory (BusinessEntityID, RateChangeDate, Rate, PayFrequency, ModifiedDate)
                                   VALUES (@BusinessEntityID, @RateChangeDate, @Rate, @PayFrequency, GETDATE())";

            try
            {
                using (SqlConnection con = ConexionSQL.ObtenerConexion())
                {
                    // 1. Check de duplicado
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@BusinessEntityID", empId);
                        checkCmd.Parameters.AddWithValue("@RateChangeDate", fechaCambio);

                        int existe = (int)checkCmd.ExecuteScalar();
                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe un registro salarial para este empleado en la fecha seleccionada.", "Duplicado Detectado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }

                    // 2. Inserción
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@BusinessEntityID", empId);
                        insertCmd.Parameters.AddWithValue("@RateChangeDate", fechaCambio);
                        insertCmd.Parameters.AddWithValue("@Rate", rate);
                        insertCmd.Parameters.AddWithValue("@PayFrequency", payFreq);

                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("¡Registro de tarifa salarial guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Limpiar campo de texto y refrescar la vista de inmediato
                txtRate.Clear();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el registro salarial: {ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmPayHistory_Load(object sender, EventArgs e)
        {
            // Método para satisfacer la referencia del diseñador
        }
    }
}