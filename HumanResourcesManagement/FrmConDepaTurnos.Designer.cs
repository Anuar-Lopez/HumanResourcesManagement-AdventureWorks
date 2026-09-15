namespace HumanResourcesManagement
{
    partial class FrmConDepaTurnos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.tablaNombre = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tablaNombre)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDepartamento
            // 
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new System.Drawing.Point(30, 30);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(250, 24);
            this.cmbDepartamento.TabIndex = 0;
            this.cmbDepartamento.SelectedIndexChanged += new System.EventHandler(this.cmbDepartamento_SelectedIndexChanged);
            // 
            // tablaNombre
            // 
            this.tablaNombre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaNombre.Location = new System.Drawing.Point(30, 75);
            this.tablaNombre.Name = "tablaNombre";
            this.tablaNombre.RowHeadersWidth = 51;
            this.tablaNombre.RowTemplate.Height = 24;
            this.tablaNombre.Size = new System.Drawing.Size(740, 330);
            this.tablaNombre.TabIndex = 1;
            // 
            // FrmConDepaTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tablaNombre);
            this.Controls.Add(this.cmbDepartamento);
            this.Name = "FrmConDepaTurnos";
            this.Text = "FrmConDepaTurnos";
            this.Load += new System.EventHandler(this.frmConDepaTurnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaNombre)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDepartamento;
        private System.Windows.Forms.DataGridView tablaNombre;
    }
}