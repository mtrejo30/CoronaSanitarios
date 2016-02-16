using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class Inventarios
    {
        public DataTable InventarioEnProceso(int iPlanta)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoList";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Planta", iPlanta);
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
        public String InventarioEnProcesoGenerar(int userId, int iPlanta)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            String result = "Hubo un problema al generar el Inventario, intente nuevamente.";
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoGenera";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", userId);
                command.Parameters.AddWithValue("@Planta", iPlanta);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                result = err.Message;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return result;
        }
        public DataTable InventarioEnProcesoDetalle(int id)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoDetalle";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
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

        public String InventarioEnProcesoTerminar(int user, int iPlanta)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            String result = "Hubo un problema al procesar la accion, intente nuevamente.";
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoTerminar";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", user);
                command.Parameters.AddWithValue("@Planta", iPlanta);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                result = err.Message;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return result;
        }
        public String InventarioEnProcesoEliminar(int id)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            String result = "Hubo un problema al procesar la accion, intente nuevamente.";
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoEliminar";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                result = err.Message;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return result;
        }
        public String InventarioEnProcesoAjusteAutomatico(int user, int idInv)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            String result = "Hubo un problema al procesar la accion, intente nuevamente.";
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spInventarioProcesoAjusteAutomatico";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", user);
                command.Parameters.AddWithValue("@ID", idInv);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                result = err.Message;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return result;
        }
    }
}
