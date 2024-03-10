using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebPicKnockout.DAL
{
    public class ConexaoBD
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["dbCadastroPessoa"].ConnectionString;

        public static SqlConnection GetConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}