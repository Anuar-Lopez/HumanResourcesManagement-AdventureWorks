using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    /// <summary>
    /// Elementos visuales compartidos para que todos los módulos
    /// tengan el mismo aspecto.
    /// </summary>
    internal static class EstiloUI
    {
        public static readonly Color Marino = Color.FromArgb(30, 45, 65);
        public static readonly Color Acento = Color.FromArgb(52, 120, 180);
        public static readonly Color Fondo = Color.FromArgb(245, 247, 250);
        public static readonly Color Verde = Color.FromArgb(40, 145, 95);
        public static readonly Color Gris = Color.FromArgb(230, 233, 238);

        public static Button CrearBoton(
            string texto, int x, int y, int ancho,
            Color fondo, Color colorTexto)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 40),
                BackColor = fondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            boton.FlatAppearance.BorderSize = 0;

            return boton;
        }

        public static Label CrearEtiqueta(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 70)
            };
        }

        public static TextBox CrearTexto(int x, int y, int ancho)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 30),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        public static ComboBox CrearCombo(int x, int y, int ancho)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
        }

        /// <summary>
        /// Panel superior blanco con título, descripción y contador.
        /// </summary>
        public static Panel CrearEncabezado(
            string titulo, string descripcion, out Label lblTotal)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 125,
                BackColor = Color.White
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Location = new Point(30, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Marino
            };

            Label lblDescripcion = new Label
            {
                Text = descripcion,
                Location = new Point(34, 68),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Gray
            };

            lblTotal = new Label
            {
                Text = "",
                Location = new Point(34, 98),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Acento
            };

            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(lblDescripcion);
            panel.Controls.Add(lblTotal);

            return panel;
        }

        public static void ConfigurarGrid(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 61, 89);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 42;
            dgv.RowTemplate.Height = 35;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(210, 228, 245);
            dgv.DefaultCellStyle.SelectionForeColor = Marino;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Fondo;
        }
    }
}
