using System.Data.SqlClient;
using System.Configuration;
namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class ConnectionLamosa
    {
        public static SqlConnection getConnection(){
            SqlConnection sqlconnection = new SqlConnection();
            sqlconnection.ConnectionString = ConfigurationManager.ConnectionStrings["lamosaConnectionString"].ToString();
            return sqlconnection;
        }
    }
}