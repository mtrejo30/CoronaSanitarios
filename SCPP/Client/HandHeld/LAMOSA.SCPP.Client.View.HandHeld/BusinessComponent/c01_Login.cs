using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c01_Login
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c01_Login()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c01_Login()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerUsuario
        public static string query_ObtenerUsuario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	u.cod_usuario as CodUsuario, ");
            queryString.Append("		u.login as Login, ");
            queryString.Append("		u.password as Password, ");
            queryString.Append("		u.cod_empleado as CodEmpleado, ");
            queryString.Append("		(e.nombre + ' ' + e.ap_paterno + ' ' + e.ap_materno) as NomEmpleado, ");
            queryString.Append("        u.cod_supervisor as CodSupervisor, ");
            queryString.Append("		u.cod_rol as CodRol, ");
            queryString.Append("		r.des_rol as DesRol, ");
            queryString.Append("		e.cod_puesto as CodPuesto, ");
            queryString.Append("		p.des_puesto as DesPuesto, ");
            queryString.Append("		u.bloqueado as Bloqueado, ");
            queryString.Append("		u.fecha_vig_password as FechaVigPassword, ");
            queryString.Append("		ct.cod_Planta CodPlanta ");
            queryString.Append("from	usuario u, ");
            queryString.Append("		empleado e join centro_trabajo ct on ct.cod_centro_trabajo = e.cod_centro_trabajo, ");
            queryString.Append("		rol r, ");
            queryString.Append("		puesto p ");
            queryString.Append("where		u.cod_empleado = e.cod_empleado ");
            queryString.Append("		and	u.cod_rol = r.cod_rol ");
            queryString.Append("		and	e.cod_puesto = p.cod_puesto ");
            queryString.Append("		and	u.login = @Login;");
            return queryString.ToString();
        }
        #endregion query_ObtenerUsuario
        #region query_ObtenerDiasAvisoVigPass
        public static string query_ObtenerDiasAvisoVigPass()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	c.valor_configuracion as DiasAvisoVigPass ");
            queryString.Append("from	configuracion c ");
            queryString.Append("where		c.cod_configuracion = 3;");
            return queryString.ToString();
        }
        #endregion query_ObtenerDiasAvisoVigPass
        #region query_ReiniciarContadorIntentos
        public static string query_ReiniciarContadorIntentos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos = 0 ");
            queryString.Append("where		cod_usuario = @CodUsuario;");
            return queryString.ToString();
        }
        #endregion query_ReiniciarContadorIntentos
        #region query_IncrementarContadorIntentos
        public static string query_IncrementarContadorIntentos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos = num_intentos + 1 ");
            queryString.Append("where		cod_usuario = @CodUsuario;");
            return queryString.ToString();
        }
        #endregion query_IncrementarContadorIntentos
        #region query_ObtenerNumIntentosConfigurados
        public static string query_ObtenerNumIntentosConfigurados()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	c.valor_configuracion as NumIntentosConfig ");
            queryString.Append("from	configuracion c ");
            queryString.Append("where		c.cod_configuracion = @CodConfiguracion;");
            return queryString.ToString();
        }
        #endregion query_ObtenerNumIntentosConfigurados
        #region query_ObtenerNumIntentosUsuario
        public static string query_ObtenerNumIntentosUsuario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	u.num_intentos as NumIntentosUsuario ");
            queryString.Append("from	usuario u ");
            queryString.Append("where		u.cod_usuario = @CodUsuario;");
            return queryString.ToString();
        }
        #endregion query_ObtenerNumIntentosUsuario
        #region query_BloquearUsuario
        public static string query_BloquearUsuario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos	= 0, ");
            queryString.Append("		bloqueado		= 1 ");
            queryString.Append("where		cod_usuario = @CodUsuario;");
            return queryString.ToString();
        }
        #endregion query_BloquearUsuario

        #region ObtenerUsuario
        public DataTable ObtenerUsuario(string sLogin)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@Login", SqlDbType.NVarChar, 10);
                pars[0].Value = sLogin;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c01_Login.query_ObtenerUsuario(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerUsuario: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerUsuario
        #region ObtenerDiasAvisoVigPass
        public DataTable ObtenerDiasAvisoVigPass()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c01_Login.query_ObtenerDiasAvisoVigPass(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDiasAvisoVigPass: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerDiasAvisoVigPass
        #region ReiniciarContadorIntentos
        public void ReiniciarContadorIntentos(int iCodUsuario)
        {
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c01_Login.query_ReiniciarContadorIntentos(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ReiniciarContadorIntentos: " + ex.Message);
            }
        }
        #endregion ReiniciarContadorIntentos
        #region IncrementarContadorIntentos
        public void IncrementarContadorIntentos(int iCodUsuario)
        {
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c01_Login.query_IncrementarContadorIntentos(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", IncrementarContadorIntentos: " + ex.Message);
            }
        }
        #endregion IncrementarContadorIntentos
        #region ObtenerNumIntentosConfigurados
        public DataTable ObtenerNumIntentosConfigurados(int iCodConfiguracion)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodConfiguracion", SqlDbType.Int);
                pars[0].Value = iCodConfiguracion;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c01_Login.query_ObtenerNumIntentosConfigurados(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumIntentosConfigurados: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerNumIntentosConfigurados
        #region ObtenerNumIntentosUsuario
        public DataTable ObtenerNumIntentosUsuario(int iCodUsuario)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c01_Login.query_ObtenerNumIntentosUsuario(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumIntentosUsuario: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerNumIntentosUsuario
        #region BloquearUsuario
        public void BloquearUsuario(int iCodUsuario)
        {
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c01_Login.query_BloquearUsuario(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", BloquearUsuario: " + ex.Message);
            }
        }
        #endregion BloquearUsuario
        #region Login
        public LoginUsuario Login(string sUsuario, string sContrasena)
        {
            LoginUsuario lu = new LoginUsuario();
            DataTable dtObj = null;
            String metodo = string.Empty;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtObj = proxy.Login(sUsuario, sContrasena);

                    if (!String.IsNullOrEmpty(Convert.ToString(dtObj.Rows[0]["Mensaje"])))
                    {
                        lu.Mensaje = Convert.ToString(dtObj.Rows[0]["Mensaje"]);
                        lu.IsLogin = false;
                    }
                    else
                    {
                        if (dtObj.Rows.Count > 0)
                        {
                            lu.CodUsuario = Convert.ToInt32(dtObj.Rows[0]["CodUsuario"]);
                            lu.Login = Convert.ToString(dtObj.Rows[0]["Login"]);
                            lu.Password = Convert.ToString(dtObj.Rows[0]["Password"]);
                            lu.CodEmpleado = Convert.ToInt32(dtObj.Rows[0]["CodEmpleado"]);
                            lu.NomEmpleado = Convert.ToString(dtObj.Rows[0]["NomEmpleado"]);
                            lu.CodSupervisor = Convert.ToInt32(dtObj.Rows[0]["CodSupervisor"]);
                            lu.CodRol = Convert.ToInt32(dtObj.Rows[0]["CodRol"]);
                            lu.DesRol = Convert.ToString(dtObj.Rows[0]["DesRol"]);
                            lu.CodPuesto = Convert.ToInt32(dtObj.Rows[0]["CodPuesto"]);
                            lu.DesPuesto = Convert.ToString(dtObj.Rows[0]["DesPuesto"]);
                            lu.Bloqueado = Convert.ToBoolean(dtObj.Rows[0]["Bloqueado"]);
                            lu.FechaVigPassword = Convert.ToDateTime(dtObj.Rows[0]["FechaVigPassword"]);
                            lu.CodPlanta = Convert.ToInt32(dtObj.Rows[0]["CodPlanta"]);
                        }
                        lu.IsLogin = true;
                    }
                    return lu;
                }
                else
                {
                    // Verificar que el usuario existe.
                    dtObj = this.ObtenerUsuario(sUsuario);
                    if (dtObj.Rows.Count > 0)
                    {
                        lu.CodUsuario = Convert.ToInt32(dtObj.Rows[0]["CodUsuario"]);
                        lu.Login = Convert.ToString(dtObj.Rows[0]["Login"]);
                        lu.Password = (Convert.ToString(dtObj.Rows[0]["Password"]) == string.Empty) ? string.Empty : c00_Common.Decrypt(Convert.ToString(dtObj.Rows[0]["Password"]), "Lamosa06");
                        lu.CodEmpleado = Convert.ToInt32(dtObj.Rows[0]["CodEmpleado"]);
                        lu.NomEmpleado = Convert.ToString(dtObj.Rows[0]["NomEmpleado"]);
                        lu.CodSupervisor = Convert.ToInt32(dtObj.Rows[0]["CodSupervisor"]);
                        lu.CodRol = Convert.ToInt32(dtObj.Rows[0]["CodRol"]);
                        lu.DesRol = Convert.ToString(dtObj.Rows[0]["DesRol"]);
                        lu.CodPuesto = Convert.ToInt32(dtObj.Rows[0]["CodPuesto"]);
                        lu.DesPuesto = Convert.ToString(dtObj.Rows[0]["DesPuesto"]);
                        lu.Bloqueado = Convert.ToBoolean(dtObj.Rows[0]["Bloqueado"]);
                        lu.FechaVigPassword = Convert.ToDateTime(dtObj.Rows[0]["FechaVigPassword"]);
                        lu.CodPlanta = Convert.ToInt32(dtObj.Rows[0]["CodPlanta"]);
                    }

                    if (lu.CodUsuario == -1)
                    {
                        lu.IsLogin = false;
                        lu.Mensaje = "Usuario no existe";
                        return lu;
                    }

                    // Verificar si el usuario está bloqueado.
                    if (lu.Bloqueado)
                    {
                        lu.IsLogin = false;
                        lu.Mensaje = "Usuario bloqueado";
                        return lu;
                    }

                    // Obtener los dias para expirar password.
                    TimeSpan ts = lu.FechaVigPassword - DateTime.Today;
                    int iDiasVencimiento = ts.Days;

                    // Verificar si la contraseña ya expiro.
                    if (iDiasVencimiento <= 0)
                    {
                        lu.IsLogin = false;
                        lu.Mensaje = "Contraseña expiró. Cambiarla";
                        return lu;
                    }

                    // Verificar password.
                    if (lu.Password == sContrasena)
                    {
                        // Obtener dias aviso de vigencia de password.
                        metodo = "ObtenerDiasAvisoVigPass";
                        dtObj = this.ObtenerDiasAvisoVigPass();
                        int iDiasAvisoVigPass = Convert.ToInt32(dtObj.Rows[0]["DiasAvisoVigPass"]);

                        // Informar los días que faltan para el vencimiento del password.
                        if (iDiasVencimiento <= iDiasAvisoVigPass)
                        {
                            lu.Mensaje = iDiasVencimiento.ToString() + " día(s) vencimiento contraseña.";
                        }

                        // Reiniciar contador de intentos.
                        metodo = "ReiniciarContadorIntentos(" + lu.CodUsuario.ToString() + ")";
                        this.ReiniciarContadorIntentos(lu.CodUsuario);

                        lu.IsLogin = true;
                        return lu;
                    }
                    else
                    {
                        /*
                        // Incrementar contador de intentos.
                        metodo = "IncrementarContadorIntentos(" + lu.CodUsuario.ToString() + ")";
                        this.IncrementarContadorIntentos(lu.CodUsuario);

                        // Obtener intentos configurados.
                        metodo = "ObtenerNumIntentosConfigurados";
                        dtObj = this.ObtenerNumIntentosConfigurados(1);
                        int iNumIntentosConfigurados = Convert.ToInt32(dtObj.Rows[0]["NumIntentosConfig"]);

                        // Obtener número de intentos que tiene el usuario.
                        metodo = "ObtenerNumIntentosUsuario(" + lu.CodUsuario.ToString() + ")";
                        dtObj = this.ObtenerNumIntentosUsuario(lu.CodUsuario);
                        int iNumIntentosUsuario = Convert.ToInt32(dtObj.Rows[0]["NumIntentosUsuario"]);

                        // Bloquear usuario y restablecer el contador de intentos.
                        if (iNumIntentosUsuario == iNumIntentosConfigurados)
                        {
                            metodo = "BloquearUsuario(" + lu.CodUsuario.ToString() + ")";
                            this.BloquearUsuario(lu.CodUsuario);
                            lu.Mensaje = "Usuario bloqueado.";
                        }
                        else
                        {
                            lu.Mensaje = "Contraseña incorrecta.";
                        }
                        */
                        lu.Mensaje = "Contraseña incorrecta.";

                        lu.IsLogin = false;
                        return lu;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", Login, " + metodo + ": " + ex.Message);
            }
        }
        public string CambiarPassword(string sUsuario, string sContrasenaAnterior, string sContrasenaNueva)
        {
            HHsvc.SCPP_HH proxy = null;
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    if (!ValidarPoliticaContrasena(sContrasenaNueva))
                    {
                        string sMensaje = "La contraseña no cumple con las politicas de seguridad, por favor verifique:\n";
                        sMensaje += "- Longitud minima de la contraseña.\n";
                        sMensaje += "- La contraseña debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                        return sMensaje;
                    }
                    proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    return proxy.CambiarPassword(sUsuario, sContrasenaAnterior, sContrasenaNueva);
                }
                return "El servicio de cambio de contraseña no esta disponible, vuelva a intentar de nuevamente ó contacte al administrador.";
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CambiarPassword: " + ex.Message);
            }
            finally 
            {
                if (proxy != null) proxy.Dispose();
            }
        }
        public static bool ValidarPoliticaContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 8) return false;
            int iSumaCodigo = (from cpwd in contrasena.ToCharArray()
                               where (cpwd >= 48 & cpwd <= 57) | (cpwd >= 65 & cpwd <= 90) | (cpwd >= 97 & cpwd <= 122)
                               select ((cpwd >= 48 & cpwd <= 57) ? 1 : (cpwd >= 65 & cpwd <= 90) ? 2 : (cpwd >= 97 & cpwd <= 122) ? 3 : 0)).Distinct<int>().Sum();
            return (iSumaCodigo == 6) ? true : false;
        }
        #endregion Login

        #endregion common

        #endregion methods

    }
}
