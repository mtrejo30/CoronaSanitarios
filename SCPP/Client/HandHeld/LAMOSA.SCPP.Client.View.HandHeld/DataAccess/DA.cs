using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace LAMOSA.SCPP.Client.View.HandHeld.DataAccess
{

    public class DA
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #region Cadenas de conexion

        // Cadena de conexion SQL Server.
        private string sMSSQLServerCE_ConnectionString = string.Empty;

        #endregion Cadenas de conexion

        #endregion Fields

        #region Methods

        #region Constructors and Destructor
        public DA()
        {
            this.sClassName = this.GetType().FullName;
            string sAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            this.sMSSQLServerCE_ConnectionString = "Data Source = '" + sAppPath + @"\" + "lamosa.sdf'; Password = 'Perni_554411'; Enlist = False; max buffer size = 1024; max database size = 1024; Mode = ''; Persist Security Info = False";
        }
        ~DA()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region ObtenerUsuario
        private LoginUsuario ObtenerUsuario(MSSQLce dbObj, string sLogin)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            LoginUsuario ResObj = new LoginUsuario();
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
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
                queryString.Append("		u.fecha_vig_password as FechaVigPassword ");
                queryString.Append("from	usuario u, ");
                queryString.Append("		empleado e, ");
                queryString.Append("		rol r, ");
                queryString.Append("		puesto p ");
                queryString.Append("where		u.cod_empleado = e.cod_empleado ");
                queryString.Append("		and	u.cod_rol = r.cod_rol ");
                queryString.Append("		and	e.cod_puesto = p.cod_puesto ");
                queryString.Append("		and	u.fecha_baja is null ");
                queryString.Append("		and	u.login = @Login;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@Login", SqlDbType.NVarChar, 10);
                pars[0].Value = sLogin;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(false, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un objeto

                if (dtObj.Rows.Count > 0)
                {
                    ResObj.CodUsuario = Convert.ToInt32(dtObj.Rows[0]["CodUsuario"]);
                    ResObj.Login = Convert.ToString(dtObj.Rows[0]["Login"]);
                    ResObj.Password = Convert.ToString(dtObj.Rows[0]["Password"]);
                    ResObj.CodEmpleado = Convert.ToInt32(dtObj.Rows[0]["CodEmpleado"]);
                    ResObj.NomEmpleado = Convert.ToString(dtObj.Rows[0]["NomEmpleado"]);
                    ResObj.CodRol = Convert.ToInt32(dtObj.Rows[0]["CodRol"]);
                    ResObj.DesRol = Convert.ToString(dtObj.Rows[0]["DesRol"]);
                    ResObj.CodPuesto = Convert.ToInt32(dtObj.Rows[0]["CodPuesto"]);
                    ResObj.DesPuesto = Convert.ToString(dtObj.Rows[0]["DesPuesto"]);
                    ResObj.Bloqueado = Convert.ToBoolean(dtObj.Rows[0]["Bloqueado"]);
                    ResObj.FechaVigPassword = Convert.ToDateTime(dtObj.Rows[0]["FechaVigPassword"]);
                }

                #endregion Mapear el DataTable en un objeto

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerUsuario: " + ex.Message);
            }
            return ResObj;
        }
        #endregion ObtenerUsuario
        #region ObtenerDiasAvisoVigPass
        public int ObtenerDiasAvisoVigPass(MSSQLce dbObj)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            int iDiasAvisoVigPass = -1;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	c.valor_configuracion as DiasAvisoVigPass ");
                queryString.Append("from	configuracion c ");
                queryString.Append("where		c.cod_configuracion = 3;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(false, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un objeto

                if (dtObj.Rows.Count > 0)
                {
                    iDiasAvisoVigPass = Convert.ToInt32(dtObj.Rows[0]["DiasAvisoVigPass"]);
                }

                #endregion Mapear el DataTable en un objeto

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDiasAvisoVigPass: " + ex.Message);
            }
            return iDiasAvisoVigPass;
        }
        #endregion ObtenerDiasAvisoVigPass
        #region ReiniciarContadorIntentos
        public void ReiniciarContadorIntentos(MSSQLce dbObj, int iCodUsuario)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("update	usuario ");
                queryString.Append("set		num_intentos = 0 ");
                queryString.Append("where	    cod_usuario = @CodUsuario;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(false, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ReiniciarContadorIntentos: " + ex.Message);
            }
        }
        #endregion ReiniciarContadorIntentos
        #region IncrementarContadorIntentos
        public void IncrementarContadorIntentos(MSSQLce dbObj, int iCodUsuario)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("update	usuario ");
                queryString.Append("set		num_intentos = num_intentos + 1 ");
                queryString.Append("where		cod_usuario = @CodUsuario;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(false, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", IncrementarContadorIntentos: " + ex.Message);
            }
        }
        #endregion IncrementarContadorIntentos
        #region ObtenerNumIntentosConfigurados
        public int ObtenerNumIntentosConfigurados(MSSQLce dbObj)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            int iNumIntentosConfig = -1;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	c.valor_configuracion as NumIntentosConfig ");
                queryString.Append("from	configuracion c ");
                queryString.Append("where		c.cod_configuracion = 1;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(false, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un objeto

                if (dtObj.Rows.Count > 0)
                {
                    iNumIntentosConfig = Convert.ToInt32(dtObj.Rows[0]["NumIntentosConfig"]);
                }

                #endregion Mapear el DataTable en un objeto

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumIntentosConfigurados: " + ex.Message);
            }
            return iNumIntentosConfig;
        }
        #endregion ObtenerNumIntentosConfigurados
        #region ObtenerNumIntentosUsuario
        public int ObtenerNumIntentosUsuario(MSSQLce dbObj, int iCodUsuario)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            int iNumIntentosUsuario = -1;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	u.num_intentos as NumIntentosUsuario ");
                queryString.Append("from	usuario u ");
                queryString.Append("where		u.cod_usuario = @CodUsuario;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(false, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un objeto

                if (dtObj.Rows.Count > 0)
                {
                    iNumIntentosUsuario = Convert.ToInt32(dtObj.Rows[0]["NumIntentosUsuario"]);
                }

                #endregion Mapear el DataTable en un objeto

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumIntentosUsuario: " + ex.Message);
            }
            return iNumIntentosUsuario;
        }
        #endregion ObtenerNumIntentosUsuario
        #region BloquearUsuario
        public void BloquearUsuario(MSSQLce dbObj, int iCodUsuario)
        {
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            //
            try
            {

                #region Query

                queryString = new StringBuilder();
                queryString.Append("update	usuario ");
                queryString.Append("set		num_intentos	= 0, ");
                queryString.Append("		bloqueado		= 1 ");
                queryString.Append("where		cod_usuario = @CodUsuario;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = iCodUsuario;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(false, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", BloquearUsuario: " + ex.Message);
            }
        }
        #endregion BloquearUsuario
        #region Login
        public LoginUsuario Login(string sLogin, string sPassword)
        {
            MSSQLce dbObj = null;
            LoginUsuario lu = null;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                dbObj.AbrirConexion();

                // Verificar que el usuario existe.
                lu = this.ObtenerUsuario(dbObj, sLogin);
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
                    int iDiasAvisoVigPass = this.ObtenerDiasAvisoVigPass(dbObj);

                    // Informar los días que faltan para el vencimiento del password.
                    if (iDiasVencimiento <= iDiasAvisoVigPass)
                    {
                        lu.Mensaje = "Faltan " + iDiasVencimiento.ToString() + " día(s) para vencimiento de contraseña. Favor de cambiarla.";
                    }

                    // Reiniciar contador de intentos.
                    this.ReiniciarContadorIntentos(dbObj, lu.CodUsuario);

                    lu.IsLogin = true;
                    return lu;
                }
                else
                {
                    // Incrementar contador de intentos.
                    this.IncrementarContadorIntentos(dbObj, lu.CodUsuario);

                    // Obtener intentos configurados.
                    int iNumIntentosConfigurados = this.ObtenerNumIntentosConfigurados(dbObj);

                    // Obtener número de intentos que tiene el usuario.
                    int iNumIntentosUsuario = this.ObtenerNumIntentosUsuario(dbObj, lu.CodUsuario);

                    // Bloquear usuario y restablecer el contador de intentos.
                    if (iNumIntentosUsuario == iNumIntentosConfigurados)
                    {
                        this.BloquearUsuario(dbObj, lu.CodUsuario);
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
                throw new Exception(this.sClassName + ", Login: " + ex.Message);
            }
            finally
            {
                dbObj.CerrarConexion();
                dbObj.Dispose();
            }
        }
        #endregion Login

        #region ObtenerPlantasRol
        public DataTable ObtenerPlantasRol(int iCodRol)
        {
            MSSQLce dbObj = null;
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	rp.cod_planta as CodPlanta, ");
                queryString.Append("		p.des_planta as DesPlanta ");
                queryString.Append("from	rol_planta rp, ");
                queryString.Append("		planta p ");
                queryString.Append("where		rp.cod_planta = p.cod_planta ");
                queryString.Append("		and	rp.cod_rol = @CodRol ");
                queryString.Append("order by	p.des_planta asc;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodRol", SqlDbType.Int);
                pars[0].Value = iCodRol;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPlantasRol: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion ObtenerPlantasRol

        #region ObtenerTurnos
        public DataTable ObtenerTurnos()
        {
            MSSQLce dbObj = null;
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	t.cod_turno as CodTurno, ");
                queryString.Append("		t.des_turno as DesTurno ");
                queryString.Append("from	turno t ");
                queryString.Append("where		t.fecha_baja is null ");
                queryString.Append("order by	t.des_turno asc;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTurnos: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion ObtenerTurnos
        #region ObtenerProcesos
        public DataTable ObtenerProcesos()
        {
            MSSQLce dbObj = null;
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	p.cod_proceso as CodProceso, ");
                queryString.Append("		p.des_proceso as DesProceso ");
                queryString.Append("from	proceso p ");
                queryString.Append("where		fecha_baja is null ");
                queryString.Append("order by	p.des_proceso asc;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerProcesos: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion ObtenerProcesos
        #region ObtenerPantallasProceso
        public DataTable ObtenerPantallasProceso(int iCodProceso)
        {
            MSSQLce dbObj = null;
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	pp.cod_pantalla as CodPantalla, ");
                queryString.Append("		p.des_pantalla as DesPantalla ");
                queryString.Append("from	proceso_pantalla pp, ");
                queryString.Append("		SCPP_pantalla p ");
                queryString.Append("where		pp.cod_pantalla = p.cod_pantalla ");
                queryString.Append("		and	pp.cod_proceso = @CodProceso ");
                queryString.Append("order by	p.cod_pantalla asc;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPantallasProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion ObtenerPantallasProceso
        #region InsertarConfigHandHeld
        public long InsertarConfigHandHeld(int iCodUsuario, int iCodTurno, int iCodProceso)
        {
            MSSQLce dbObj = null;
            StringBuilder queryString1 = null;
            StringBuilder queryString2 = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            long lCodConfigHandHeld = -1;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString1 = new StringBuilder();
                queryString1.Append("select	((case when max(ch.cod_config_handheld) is null then 0 else max(ch.cod_config_handheld) end) + 1) as CodConfigHandHeld ");
                queryString1.Append("from	config_handheld ch;");

                queryString2 = new StringBuilder();
                queryString2.Append("insert into config_handheld ");
                queryString2.Append("(cod_config_handheld, cod_usuario, cod_turno, cod_proceso) ");
                queryString2.Append("values (@CodConfigHandHeld, @CodUsuario, @CodTurno, @CodProceso);");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                //pars[0].Value = lCodConfigHandHeld;
                pars[1] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                pars[1].Value = iCodUsuario;
                pars[2] = new SqlCeParameter("@CodTurno", SqlDbType.Int);
                pars[2].Value = iCodTurno;
                pars[3] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[3].Value = iCodProceso;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString1.ToString(), pars);
                lCodConfigHandHeld = Convert.ToInt64(dtObj.Rows[0]["CodConfigHandHeld"]);

                pars[0].Value = lCodConfigHandHeld;
                dbObj.EjecutarConsulta(true, queryString2.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarConfigHandHeld: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lCodConfigHandHeld;
        }
        #endregion InsertarConfigHandHeld

        #region ValidarEmpleadoMFG
        public int ValidarEmpleadoMFG(int iClaveEmpleadoMFG)
        {
            MSSQLce dbObj = null;
            StringBuilder queryString = null;
            SqlCeParameter[] pars = null;
            DataTable dtObj = null;
            int iCodEmpleado = -1;
            //
            try
            {

                #region Connection Configuration

                dbObj = new MSSQLce(this.sMSSQLServerCE_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("select	e.cod_empleado as CodEmpleado ");
                queryString.Append("from	empleado e ");
                queryString.Append("where	e.clave_empleado_MFG = @ClaveEmpleadoMFG;");

                #endregion Query

                #region Parameters

                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                pars[0].Value = iClaveEmpleadoMFG;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(true, queryString.ToString(), pars);

                #endregion Query Execution

                if (dtObj.Rows.Count > 0)
                {
                    iCodEmpleado = Convert.ToInt32(dtObj.Rows[0]["CodEmpleado"]);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ValidarEmpleadoMFG: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iCodEmpleado;
        }
        #endregion ValidarEmpleadoMFG

        #endregion Common

        #endregion Methods

    }

    #region LoginUsuario
    public class LoginUsuario
    {

        #region Fields

        private int iCodUsuario = -1;
        private string sLogin = string.Empty;
        private string sPassword = string.Empty;
        private int iCodEmpleado = -1;
        private string sNomEmpleado = string.Empty;
        private int iCodRol = -1;
        private string sDesRol = string.Empty;
        private int iCodPuesto = -1;
        private string sDesPuesto = string.Empty;
        private bool bBloqueado = false;
        private DateTime dtFechaVigPassword = DateTime.MinValue;
        private bool bIsLogin = false;
        private string sMensaje = string.Empty;

        private int iCodPlanta = -1;
        private string sDesPlanta = string.Empty;
        
        private DateTime dtFecha = DateTime.MinValue;
        private int iCodTurno = -1;
        private string sDesTurno = string.Empty;
        private int iCodProceso = -1;
        private string sDesProceso = string.Empty;
        private string sCodSupervisor = string.Empty;

        #endregion Fields

        #region Properties

        public int CodUsuario { get { return this.iCodUsuario; } set { this.iCodUsuario = value; } }
        public string Login { get { return this.sLogin; } set { this.sLogin = value; } }
        public string Password { get { return this.sPassword; } set { this.sPassword = value; } }
        public int CodEmpleado { get { return this.iCodEmpleado; } set { this.iCodEmpleado = value; } }
        public string NomEmpleado { get { return this.sNomEmpleado; } set { this.sNomEmpleado = value; } }
        public int CodRol { get { return this.iCodRol; } set { this.iCodRol = value; } }
        public string DesRol { get { return this.sDesRol; } set { this.sDesRol = value; } }
        public int CodPuesto { get { return this.iCodPuesto; } set { this.iCodPuesto = value; } }
        public string DesPuesto { get { return this.sDesPuesto; } set { this.sDesPuesto = value; } }
        public bool Bloqueado { get { return this.bBloqueado; } set { this.bBloqueado = value; } }
        public DateTime FechaVigPassword { get { return this.dtFechaVigPassword; } set { this.dtFechaVigPassword = value; } }
        public bool IsLogin { get { return this.bIsLogin; } set { this.bIsLogin = value; } }
        public string Mensaje { get { return this.sMensaje; } set { this.sMensaje = value; } }
        
        public int CodPlanta { get { return this.iCodPlanta; } set { this.iCodPlanta = value; } }
        public string DesPlanta { get { return this.sDesPlanta; } set { this.sDesPlanta = value; } }

        public DateTime Fecha { get { return this.dtFecha; } set { this.dtFecha = value; } }
        public int CodTurno { get { return this.iCodTurno; } set { this.iCodTurno = value; } }
        public string DesTurno { get { return this.sDesTurno; } set { this.sDesTurno = value; } }
        public int CodProceso { get { return this.iCodProceso; } set { this.iCodProceso = value; } }
        public string DesProceso { get { return this.sDesProceso; } set { this.sDesProceso = value; } }

        public string CodSupervisor { get { return this.sCodSupervisor; } set { this.sCodSupervisor = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor
        public LoginUsuario()
        {
            
        }
        public LoginUsuario
        (
            int iCodUsuario,
            string sLogin,
            string sPassword,
            int iCodEmpleado,
            string sNomEmpleado,
            int iCodRol,
            string sDesRol,
            int iCodPuesto,
            string sDesPuesto,
            bool bBloqueado,
            DateTime dtFechaVigPassword
        )
        {
            this.iCodUsuario = iCodUsuario;
            this.sLogin = sLogin;
            this.sPassword = sPassword;
            this.iCodEmpleado = iCodEmpleado;
            this.sNomEmpleado = sNomEmpleado;
            this.iCodRol = iCodRol;
            this.sDesRol = sDesRol;
            this.iCodPuesto = iCodPuesto;
            this.sDesPuesto = sDesPuesto;
            this.bBloqueado = bBloqueado;
            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        ~LoginUsuario()
        {

        }
        #endregion Constructors and Destructor

        #endregion Methods

    }
    #endregion LoginUsuario

}
