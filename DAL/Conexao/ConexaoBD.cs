using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.DAL
{
    public class ConexaoBD
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static SqlConnection GetConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}
