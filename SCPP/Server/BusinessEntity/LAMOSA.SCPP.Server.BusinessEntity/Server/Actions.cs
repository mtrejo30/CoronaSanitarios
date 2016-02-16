using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class Actions
    {
        public int DeleteActionSreens(int cod_rol, int cod_modulo)
        {
            SqlConnection sqlC = new SqlConnection();
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_deleteActionsScreens";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                DataTable dt = new DataTable();
                sqlC.Open();
                command.ExecuteReader();
                return 1;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                sqlC.Close();
            }
        }
       
        public int InsertActionSreens(int cod_accion,int cod_pantalla,int permiso_pantalla,int cod_rol, int cod_modulo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_insertActionsScreens";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_accion", cod_accion);
                command.Parameters.AddWithValue("@cod_pantalla", cod_pantalla);
                command.Parameters.AddWithValue("@permiso_pantalla", permiso_pantalla);
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@cod_modulo", cod_modulo);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return Convert.ToInt32(dr["ID"]);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                sqlC.Close();
            }
            return -1;
        }
        
        public List<ScreenPermission> GetActionBySreen(int cod_rol, String localPath)
        {
            String uri = localPath.Substring(1);
            uri = uri.Substring(uri.IndexOf("/"));
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getActionsByScreen";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@uri", uri);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                List<ScreenPermission> spl = new List<ScreenPermission>();
                ScreenPermission sp;
                while (dr.Read()) {
                    sp = new ScreenPermission();
                    sp.ActionCode = Convert.ToInt32(dr["cod_accion"]);
                    sp.ScreenCode = Convert.ToInt32(dr["cod_pantalla"]);
                    sp.DescriptionAction = dr["Descripcion"].ToString();
                    spl.Add(sp);
                }
                return spl;
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
            return null;
        }

        public int ObtenerNumImpresionesMolde(int codMolde)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_GetNumeroImpresionesMolde";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_molde", codMolde);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return Convert.ToInt32(dr[0]);
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
            return 0;
        }
        public String DesactivarConfigBanco(int codConfigBanco)
        {
            String msg = "";
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_DesactivarConfigBanco";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@codConfigBanco", codConfigBanco);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    msg = dr[0].ToString();
                }
            }
            catch (Exception err)
            {
                msg = err.Message;
            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return msg;
        }
    }
}
