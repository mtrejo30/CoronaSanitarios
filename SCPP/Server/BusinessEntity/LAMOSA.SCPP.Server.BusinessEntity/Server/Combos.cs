using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity.Server
{
    public class Combos
    {
        public String getUsuarios(String nick, String pass)
        {

            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                sqlC.Open();
                command.CommandText = "getUsuarios";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nick", nick);
                command.Parameters.AddWithValue("@contraseña", pass);
                dr = command.ExecuteReader();

                if (dr.Read())
                {

                    return "Ok";
                }

                return "Error";
            }


            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }

        }
        public DataTable getCetroTrabajo(int planta, int proceso)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_centrotrabajo_cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@planta", planta);
                command.Parameters.AddWithValue("@proceso", proceso);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerMaquinaCbo(int codArea, int codCT, int? iCodigoPlanta, int? iCodigoProceso)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Maquina_Lis";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_area", codArea);
                command.Parameters.AddWithValue("@cod_centro_trabajo", codCT);
                if (iCodigoPlanta.HasValue) command.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta.Value);
                if (iCodigoProceso.HasValue) command.Parameters.AddWithValue("@CodigoProceso", iCodigoProceso.Value);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerRolCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Rol_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable GetModulesCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getModules";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable GetActionSreens(int cod_rol, int cod_modulo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getActionsScreens";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@cod_modulo", cod_modulo);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable GetScreens(int cod_rol, int cod_modulo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_getScreens";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
                command.Parameters.AddWithValue("@cod_modulo", cod_modulo);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_Planta_RolCbo(int cod_rol)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Planta_Rol_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cod_rol", cod_rol);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_ProcesoCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Proceso_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerProceso(int codigoCentroTrabajo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerProcesoDeCentroTrabajo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodigoCentroTrabajo", codigoCentroTrabajo);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_TipoArticuloCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_TipoArticulo_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_ColorCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Color_Lis";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_MoldeCbo(int cod_tipo_articulo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Molde_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodTipoArticulo", cod_tipo_articulo);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_TurnoCbo()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_Turno_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable Get_EdoDefecto()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "sp_EstadoDefecto_Cbo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerArticulo(int iTipoArticulo)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerArticulos";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (iTipoArticulo > 0) command.Parameters.AddWithValue("@CodigoTipoArticulo", iTipoArticulo);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerArea(int iCentroTrabajo, int iPlanta, int iProceso)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerArea";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (iCentroTrabajo > 0) command.Parameters.AddWithValue("@CodigoCentroTrabajo", iCentroTrabajo);
                if (iPlanta > 0) command.Parameters.AddWithValue("@CodigoPlanta", iPlanta);
                if (iProceso > 0) command.Parameters.AddWithValue("@CodigoProceso", iProceso);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerProveedores()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerProveedores";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerProcesoOrigen()
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerProcesoOrigenSel";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
        public DataTable ObtenerProcesoDestino(int iCodigoProcesoOrigen)
        {
            SqlConnection sqlC = new SqlConnection();
            SqlDataReader dr = null;
            try
            {
                sqlC = ConnectionLamosa.getConnection();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                command.CommandText = "spObtenerProcesoDestinoSel";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodigoProcesoOrigen", iCodigoProcesoOrigen);
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
                if (sqlC != null) sqlC.Close();
                if (dr != null) dr.Close();
            }
        }
    }
}
