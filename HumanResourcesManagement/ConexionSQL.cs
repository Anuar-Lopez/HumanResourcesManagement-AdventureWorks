using System.Data;
using System.Data.SqlClient;

namespace HumanResourcesManagement
{
    public static class ConexionSQL
    {
        
        private static readonly string CadenaConexion = @"Server=.\SQLEXPRESS;Database=AdventureWorks2008;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection con = new SqlConnection(CadenaConexion);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }
    }
}