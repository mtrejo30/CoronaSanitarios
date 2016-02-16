using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class CodigoReemplazo
    {
        public DataTable GetReplacementCodes(int planta, int process, int article_type, int model)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getReplacementCodes";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Planta", planta);
                command.Parameters.AddWithValue("@Proceso", process);
                command.Parameters.AddWithValue("@TipoArticulo", article_type);
                command.Parameters.AddWithValue("@Modelo", model);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
        }
        public DataTable GetDetaineesCodes(int planta, int process, int article_type, int model)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getDetaineesCodes";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Planta", planta);
                command.Parameters.AddWithValue("@Proceso", process);
                command.Parameters.AddWithValue("@TipoArticulo", article_type);
                command.Parameters.AddWithValue("@Modelo", model);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
        }
        public String GenerateReplacementCodes(int cod_reemplazo, int cod_detenido)
        {
            String ret = "";
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_GenerateReplacementCodes";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodPiezaReemplazo", cod_reemplazo);
                command.Parameters.AddWithValue("@CodPiezaDetenido", cod_detenido);
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read()) {
                    ret = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                ret = "Hubo un problema al generar la accion, intente nuevamente";
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return ret;
        }
    }
}
