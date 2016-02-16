using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
   public class CodigoBarras
    {
        public DataTable ObtenerCodigoBarras(int planta, int centroTrabajo, int codigoProceso, int banco, int empleado)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try 
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Codigo_Barras_Lis";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@planta", planta);
                command.Parameters.AddWithValue("@centroTrabajo", centroTrabajo);
                command.Parameters.AddWithValue("@banco", banco);
                command.Parameters.AddWithValue("@empleado", empleado);
                command.Parameters.AddWithValue("@CodigoProceso", codigoProceso);
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
        public String InsertCodigoBarras(String id, string planta, string centroTrabajo, string banco, string empleado, String cod_desde, String cod_hasta)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_CodigoBarras_Ins";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@cod_planta", planta);
                command.Parameters.AddWithValue("@cod_ct", centroTrabajo);
                command.Parameters.AddWithValue("@cod_maquina", banco);
                command.Parameters.AddWithValue("@cod_empleado", empleado);
                command.Parameters.AddWithValue("@cod_desde", cod_desde);
                command.Parameters.AddWithValue("@cod_hasta", cod_hasta);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
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
            return "La operacion no se pudo completar.";
        }
        public String DeleteCodigoBarras(String id)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_CodigoBarras_Del";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
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
            return "La operacion no se pudo completar.";
        }
    }
}
