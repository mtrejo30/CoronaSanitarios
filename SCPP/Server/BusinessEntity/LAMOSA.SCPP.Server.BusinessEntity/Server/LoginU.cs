using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class LoginU
    {
        public Usuario GetUser(int cod_user)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            Usuario user = new Usuario();
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getUsuario";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@codUsuario", cod_user);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                
                if (dr.Read())
                {
                    user.CodUsuario = cod_user;
                    user.CodEmpleado = Convert.ToInt32(dr["CodEmpleado"]);
                    user.NombreUsuario = dr["NombreUsuario"].ToString();
                    user.Nombre = dr["Nombre"].ToString();
                    user.APaterno = dr["APaterno"].ToString();
                    user.AMaterno = dr["AMaterno"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.CodRol = Convert.ToInt32(dr["CodRol"]);
                    user.DesRol = dr["DesRol"].ToString();
                    user.Bloqueado = Convert.ToBoolean(Convert.ToByte(dr["bloqueado"]));
                    user.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                    user.FechaVigPassword = Convert.ToDateTime(dr["FechaVigPassword"]);
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
            return user;
        }

        public Boolean HasScreenPermision(int cod_rol, String uri)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getScreenPermision";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@uri", uri);
                DataTable dt = new DataTable();
                sqlC.Open();
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    return true;
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
            return false;
        }
    }
}
