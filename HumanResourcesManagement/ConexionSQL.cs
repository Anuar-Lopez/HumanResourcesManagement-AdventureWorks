using System.Data.SqlClient;

namespace HumanResourcesManagement
{
    public static class ConexionSQL
    {
        private static readonly string CadenaConexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=AdventureWorks2008;Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}