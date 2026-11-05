using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HumanResourcesManagement
{
    public static class ExportadorCSV
    {
        public static void ExportarDataGridView(DataGridView dgv, string nombreArchivoSugerido)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos disponibles en la tabla para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*",
                FileName = nombreArchivoSugerido + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    // Encabezados
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            sb.Append("\"" + dgv.Columns[i].HeaderText.Replace("\"", "\"\"") + "\"");
                            if (i < dgv.Columns.Count - 1) sb.Append(",");
                        }
                    }
                    sb.AppendLine();

                    // Filas
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible)
                                {
                                    string valor = row.Cells[i].Value?.ToString() ?? "";
                                    sb.Append("\"" + valor.Replace("\"", "\"\"") + "\"");
                                    if (i < dgv.Columns.Count - 1) sb.Append(",");
                                }
                            }
                            sb.AppendLine();
                        }
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("¡Exportación completada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}