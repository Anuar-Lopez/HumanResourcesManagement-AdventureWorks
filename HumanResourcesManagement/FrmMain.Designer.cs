namespace HumanResourcesManagement
{
    partial class FrmMain
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.directorioDeEmpleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directorioDeEmpleadosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.departamentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestiónDeDepartamentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historialSalarialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.candidatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarHistorialSalarialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestiónDeCandidatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.irAInicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(31, 67);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(57, 16);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(31, 98);
            this.lblRol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(31, 16);
            this.lblRol.TabIndex = 1;
            this.lblRol.Text = "Rol:";
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(880, 61);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(140, 28);
            this.btnCerrarSesion.TabIndex = 2;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directorioDeEmpleadosToolStripMenuItem,
            this.empleadosToolStripMenuItem,
            this.departamentosToolStripMenuItem,
            this.historialSalarialToolStripMenuItem,
            this.candidatosToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // directorioDeEmpleadosToolStripMenuItem
            // 
            this.directorioDeEmpleadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.irAInicioToolStripMenuItem});
            this.directorioDeEmpleadosToolStripMenuItem.Name = "directorioDeEmpleadosToolStripMenuItem";
            this.directorioDeEmpleadosToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.directorioDeEmpleadosToolStripMenuItem.Text = "🏠 Inicio";
            this.directorioDeEmpleadosToolStripMenuItem.Click += new System.EventHandler(this.FrmMain_Load);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directorioDeEmpleadosToolStripMenuItem1});
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.empleadosToolStripMenuItem.Text = "🧑‍🤝‍🧑 Empleados";
            this.empleadosToolStripMenuItem.Click += new System.EventHandler(this.empleadosToolStripMenuItem_Click);
            // 
            // directorioDeEmpleadosToolStripMenuItem1
            // 
            this.directorioDeEmpleadosToolStripMenuItem1.Name = "directorioDeEmpleadosToolStripMenuItem1";
            this.directorioDeEmpleadosToolStripMenuItem1.Size = new System.Drawing.Size(258, 26);
            this.directorioDeEmpleadosToolStripMenuItem1.Text = "Directorio de empleados";
            this.directorioDeEmpleadosToolStripMenuItem1.Click += new System.EventHandler(this.directorioDeEmpleadosToolStripMenuItem1_Click);
            // 
            // departamentosToolStripMenuItem
            // 
            this.departamentosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestiónDeDepartamentosToolStripMenuItem});
            this.departamentosToolStripMenuItem.Name = "departamentosToolStripMenuItem";
            this.departamentosToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.departamentosToolStripMenuItem.Text = "Departamentos";
            // 
            // gestiónDeDepartamentosToolStripMenuItem
            // 
            this.gestiónDeDepartamentosToolStripMenuItem.Name = "gestiónDeDepartamentosToolStripMenuItem";
            this.gestiónDeDepartamentosToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.gestiónDeDepartamentosToolStripMenuItem.Text = "Gestión de departamentos";
            this.gestiónDeDepartamentosToolStripMenuItem.Click += new System.EventHandler(this.gestiónDeDepartamentosToolStripMenuItem_Click);
            // 
            // historialSalarialToolStripMenuItem
            // 
            this.historialSalarialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultarHistorialSalarialToolStripMenuItem});
            this.historialSalarialToolStripMenuItem.Name = "historialSalarialToolStripMenuItem";
            this.historialSalarialToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.historialSalarialToolStripMenuItem.Text = "Historial salarial";
            // 
            // candidatosToolStripMenuItem
            // 
            this.candidatosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestiónDeCandidatosToolStripMenuItem});
            this.candidatosToolStripMenuItem.Name = "candidatosToolStripMenuItem";
            this.candidatosToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.candidatosToolStripMenuItem.Text = "Candidatos";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesiónToolStripMenuItem1});
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.cerrarSesiónToolStripMenuItem.Text = "Sesión";
            // 
            // cerrarSesiónToolStripMenuItem1
            // 
            this.cerrarSesiónToolStripMenuItem1.Name = "cerrarSesiónToolStripMenuItem1";
            this.cerrarSesiónToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.cerrarSesiónToolStripMenuItem1.Text = "Cerrar sesión";
            this.cerrarSesiónToolStripMenuItem1.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem1_Click);
            // 
            // consultarHistorialSalarialToolStripMenuItem
            // 
            this.consultarHistorialSalarialToolStripMenuItem.Name = "consultarHistorialSalarialToolStripMenuItem";
            this.consultarHistorialSalarialToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.consultarHistorialSalarialToolStripMenuItem.Text = "Consultar historial salarial";
            this.consultarHistorialSalarialToolStripMenuItem.Click += new System.EventHandler(this.consultarHistorialSalarialToolStripMenuItem_Click);
            // 
            // gestiónDeCandidatosToolStripMenuItem
            // 
            this.gestiónDeCandidatosToolStripMenuItem.Name = "gestiónDeCandidatosToolStripMenuItem";
            this.gestiónDeCandidatosToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.gestiónDeCandidatosToolStripMenuItem.Text = "Gestión de candidatos";
            this.gestiónDeCandidatosToolStripMenuItem.Click += new System.EventHandler(this.gestiónDeCandidatosToolStripMenuItem_Click);
            // 
            // irAInicioToolStripMenuItem
            // 
            this.irAInicioToolStripMenuItem.Name = "irAInicioToolStripMenuItem";
            this.irAInicioToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.irAInicioToolStripMenuItem.Text = "Ir a inicio";
            this.irAInicioToolStripMenuItem.Click += new System.EventHandler(this.irAInicioToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.Text = "Sistema de Gestión de Recursos Humanos - Adventure Works";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem directorioDeEmpleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historialSalarialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem candidatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directorioDeEmpleadosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gestiónDeDepartamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem consultarHistorialSalarialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestiónDeCandidatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem irAInicioToolStripMenuItem;
    }
}