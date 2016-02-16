using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DT.CE;
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class clsLogin
    {
        public clsLogin()
        {
        }

        public static string query_login_s()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	u.cod_usuario as CodUsuario, ");
            queryString.Append("		u.login as Login, ");
            queryString.Append("		u.password as Password, ");
            queryString.Append("		u.cod_empleado as CodEmpleado, ");
            queryString.Append("		(e.nombre + ' ' + e.ap_paterno + ' ' + e.ap_materno) as NomEmpleado, ");
            queryString.Append("		u.cod_rol as CodRol, ");
            queryString.Append("		r.des_rol as DesRol, ");
            queryString.Append("		e.cod_puesto as CodPuesto, ");
            queryString.Append("		p.des_puesto as DesPuesto, ");
            queryString.Append("		u.bloqueado as Bloqueado, ");
            queryString.Append("		u.fecha_vig_password as FechaVigPassword, ");
            queryString.Append("		u.cod_supervisor  ");
            queryString.Append("from	usuario u, ");
            queryString.Append("		empleado e, ");
            queryString.Append("		rol r, ");
            queryString.Append("		puesto p ");
            queryString.Append("where		u.cod_empleado = e.cod_empleado ");
            queryString.Append("		and	u.cod_rol = r.cod_rol ");
            queryString.Append("		and	e.cod_puesto = p.cod_puesto ");
            queryString.Append("		and	u.fecha_baja is null ");
            queryString.Append("		and	u.login = '{0}';");


            return queryString.ToString();
        }

        public static string query_config1_s()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            queryString.Append("select	c.valor_configuracion as DiasAvisoVigPass ");
            queryString.Append("from	configuracion c ");
            queryString.Append("where	c.cod_configuracion = 3;");

            return queryString.ToString();

        }

        public static string query_config2_s()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            queryString.Append("select	c.valor_configuracion as NumIntentosConfig ");
            queryString.Append("from	configuracion c ");
            queryString.Append("where	c.cod_configuracion = 1;");
            
            return queryString.ToString();
        }

        public static string query_num_intentos_s()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            queryString.Append("select	u.num_intentos as NumIntentosUsuario ");
            queryString.Append("from	usuario u ");
            queryString.Append("where	login = '{0}'");
            
            return queryString.ToString();
        }

        public static string query_num_intentos1_upd()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos = 0 ");
            queryString.Append("where	login = '{0}' and password = '{1}'");

            return queryString.ToString();

        }

        public static string query_num_intentos2_upd()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos = num_intentos + 1 ");
            queryString.Append("where	login = '{0}' and password <> '{1}'");

            return queryString.ToString();
        }

        public static string query_bloquearUsuario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	usuario ");
            queryString.Append("set		num_intentos	= 0, ");
            queryString.Append("		bloqueado		= 1 ");
            queryString.Append("where	login = '{0}'");
            return queryString.ToString();
        }

        public LoginUsuario Login(string sLogin, string sPassword)
        {
            
            LoginUsuario lu = new LoginUsuario();
            //
            try
            {

                clsQuery oquery = new clsQuery(clsConfig.getConection());
                System.Data.DataSet ods  = new System.Data.DataSet();
                
                ods.Tables.Add(oquery.exec(String.Format(clsLogin.query_login_s(),sLogin,sPassword)).Tables[0].Copy());
                ods.Tables[0].TableName = "Tabla1";
                ods.Tables.Add(oquery.exec(String.Format(clsLogin.query_config1_s(), sLogin, sPassword)).Tables[0].Copy());
                ods.Tables[1].TableName = "Tabla2";
                ods.Tables.Add(oquery.exec(String.Format(clsLogin.query_config2_s(), sLogin, sPassword)).Tables[0].Copy());
                ods.Tables[2].TableName = "Tabla3";
                ods.Tables.Add(oquery.exec(String.Format(clsLogin.query_num_intentos_s(), sLogin, sPassword)).Tables[0].Copy());
                ods.Tables[3].TableName = "Tabla4";
                oquery.exec(String.Format(clsLogin.query_num_intentos1_upd(), sLogin, sPassword));
                oquery.exec(String.Format(clsLogin.query_num_intentos2_upd(), sLogin, sPassword));
                

                if (ods.Tables.Count == 0 || ods.Tables[0].Rows.Count == 0)
                {
                    lu.CodUsuario = -1;
                    lu.IsLogin = false;
                    lu.Mensaje = "El usuario no existe. Favor de verificar.";
                    return lu; 
                }


                lu.CodUsuario = Convert.ToInt32(ods.Tables[0].Rows[0]["CodUsuario"]);
                lu.Login = Convert.ToString(ods.Tables[0].Rows[0]["Login"]);
                lu.Password = Convert.ToString(ods.Tables[0].Rows[0]["Password"]);
                lu.CodEmpleado = Convert.ToInt32(ods.Tables[0].Rows[0]["CodEmpleado"]);
                lu.NomEmpleado = Convert.ToString(ods.Tables[0].Rows[0]["NomEmpleado"]);
                lu.CodRol = Convert.ToInt32(ods.Tables[0].Rows[0]["CodRol"]);
                lu.DesRol = Convert.ToString(ods.Tables[0].Rows[0]["DesRol"]);
                lu.CodPuesto = Convert.ToInt32(ods.Tables[0].Rows[0]["CodPuesto"]);
                lu.DesPuesto = Convert.ToString(ods.Tables[0].Rows[0]["DesPuesto"]);
                lu.Bloqueado = Convert.ToBoolean(ods.Tables[0].Rows[0]["Bloqueado"]);
                lu.FechaVigPassword = Convert.ToDateTime(ods.Tables[0].Rows[0]["FechaVigPassword"]);
                lu.CodSupervisor = Convert.ToInt32(ods.Tables[0].Rows[0]["cod_supervisor"]);

                // Verificar que el usuario existe.
                if (lu.CodUsuario == -1)
                {
                    lu.IsLogin = false;
                    lu.Mensaje = "El usuario no existe. Favor de verificar.";
                    return lu;
                }

                // Verificar si el usuario está bloqueado.
                if (lu.Bloqueado)
                {
                    lu.IsLogin = false;
                    lu.Mensaje = "Usuario bloqueado. Favor de comunicarse con el administrador del sistema.";
                    return lu;
                }

                // Obtener los dias para expirar password.
                TimeSpan ts = lu.FechaVigPassword - DateTime.Today;
                int iDiasVencimiento = ts.Days;

                // Verificar si la contraseña ya expiro.
                if (iDiasVencimiento <= 0)
                {
                    lu.IsLogin = false;
                    lu.Mensaje = "La contraseña ya expiró. Favor de cambiar contraseña.";
                    return lu;
                }

                // Verificar password.
                if (lu.Password == sPassword)
                {
                    // Obtener dias aviso de vigencia de password.
                    int iDiasAvisoVigPass = Convert.ToInt32(ods.Tables[1].Rows[0]["DiasAvisoVigPass"]);

                    // Informar los días que faltan para el vencimiento del password.
                    if (iDiasVencimiento <= iDiasAvisoVigPass)
                    {
                        lu.Mensaje = "Faltan " + iDiasVencimiento.ToString() + " día(s) para vencimiento de contraseña. Favor de cambiarla.";
                    }

                    lu.IsLogin = true;
                    return lu;
                }
                else
                {
                    // Obtener intentos configurados.
                    int iNumIntentosConfigurados = Convert.ToInt32(ods.Tables[2].Rows[0]["NumIntentosConfig"]);

                    // Obtener número de intentos que tiene el usuario.
                    int iNumIntentosUsuario = Convert.ToInt32(ods.Tables[3].Rows[0]["NumIntentosUsuario"]);

                    // Bloquear usuario y restablecer el contador de intentos.
                    if (iNumIntentosUsuario == iNumIntentosConfigurados)
                    {
                        oquery.exec(String.Format(clsLogin.query_bloquearUsuario(), sLogin));
                        lu.IsLogin = false;
                        lu.Mensaje = "Usuario bloqueado. Favor de comunicarse con el administrador del sistema.";
                        return lu;
                    }
                    else
                    {
                        lu.IsLogin = false;
                        lu.Mensaje = "El password es incorrecto. Favor de verificar.";
                        return lu;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("clsLogin" + ", Login: " + ex.Message);
            }

        }

    }
}
