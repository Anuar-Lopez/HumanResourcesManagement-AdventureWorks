# HumanResourcesManagement-AdventureWorks
Sistema de gestión de recursos humanos para Adventure Works desarrollado en C# Windows Forms y SQL Server utilizando Scrum.

### 1. Centralización de Conexión a Base de Datos (`ConexionSQL.cs`)
- Creación de la clase helper `ConexionSQL.cs` con el método `ObtenerConexion()` para estandarizar el acceso a `AdventureWorks2008`.
- Optimización del manejo de recursos y autenticación integrada mediante bloques `using`.

### 2. Directorio de Empleados Multi-Criterio (`FrmDirectorioEmpleados.cs`)
- Implementación de filtro dinámico que permite buscar por **ID**, **Cédula/Documento**, **Usuario Login** y **Cargo**.
- Búsqueda automática al presionar la tecla **Enter** (`KeyDown`).
- Rediseño de `DataGridView` con encabezados corporativos (`#1E3D59`), filas alternadas y formato de fecha `dd/MM/yyyy`.
- Incorporación de botón de navegación para salir/regresar.

### 3. Módulo de Gestión de Departamentos (`FrmDepartamentos.cs`)
- Módulo para consultar la tabla `HumanResources.Department`.
- Filtro parametrizado por **Nombre de Departamento** y **Grupo/Área**.
- Diseño visual alineado con los estándares del proyecto.