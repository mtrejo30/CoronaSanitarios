using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c04_CapturaInicial
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public c04_CapturaInicial()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c04_CapturaInicial()
        {

        }
        #endregion constructors and destructor

        #region common

        #region query_ObtenerClaveEmpleadoMFG
        public static string query_ObtenerClaveEmpleadoMFG()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	e.clave_empleado_MFG as ClaveEmpleadoMFG ");
            queryString.Append("from	empleado e ");
            queryString.Append("where		e.cod_empleado = @CodEmpleado;");
            return queryString.ToString();
        }
        #endregion query_ObtenerClaveEmpleadoMFG
        #region query_ObtenerSupervisorPorDefecto
        public static string query_ObtenerSupervisorPorDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	e.cod_empleado as CodEmpleado, ");
            queryString.Append("		e.clave_empleado_MFG as ClaveEmpleadoMFG, ");
            queryString.Append("		(e.nombre + ' ' + e.ap_paterno + ' ' + e.ap_materno) as NomEmpleado ");
            queryString.Append("from	usuario u, ");
            queryString.Append("		empleado e ");
            queryString.Append("where		u.cod_supervisor = e.cod_empleado ");
            queryString.Append("		and	u.cod_usuario = @CodUsuario;");
            return queryString.ToString();
        }
        #endregion query_ObtenerSupervisorPorDefecto
        #region query_ValidarEmpleadoMFG
        public static string query_ValidarEmpleadoMFG()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	e.cod_empleado as CodEmpleado ");
            queryString.Append("from	empleado e ");
            queryString.Append("where	e.clave_empleado_MFG = @ClaveEmpleadoMFG;");
            return queryString.ToString();
        }
        #endregion query_ValidarEmpleadoMFG
        #region query_ObtenerCentrosTrabajo
        public static string query_ObtenerCentrosTrabajo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ct.cod_centro_trabajo as CodCentroTrabajo, ");
            queryString.Append("		ct.clave_centro_trabajo as ClaveCentroTrabajo, ");
            queryString.Append("		(ct.clave_centro_trabajo + ' - ' + ct.des_centro_trabajo) as DesCentroTrabajo ");
            queryString.Append("from	centro_trabajo ct ");
            queryString.Append("where		ct.cod_planta = @CodPlanta ");
            queryString.Append("		and	ct.cod_proceso = @CodProceso ");
            queryString.Append("order by	ct.clave_centro_trabajo asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCentrosTrabajo
        #region query_ObtenerMaquinas
        public static string query_ObtenerMaquinas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cb.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		m.cod_maquina as CodMaquina, ");
            queryString.Append("		m.clave_maquina as ClaveMaquina, ");
            queryString.Append("		m.clave_maquina + ' - ' + m.des_maquina as DesMaquina, ");
            queryString.Append("		tm.cod_tipo_maquina as CodTipoMaquina, ");
            queryString.Append("		tm.des_tipo_maquina as DesTipoMaquina ");
            queryString.Append("from	centro_trabajo ct, ");
            queryString.Append("		area a, ");
            queryString.Append("		maquina m, ");
            queryString.Append("		config_banco cb, ");
            queryString.Append("		tipo_maquina tm ");
            queryString.Append("where		ct.cod_centro_trabajo = a.cod_centro_trabajo ");
            queryString.Append("		and	a.cod_area = m.cod_area ");
            queryString.Append("		and	m.cod_maquina = cb.cod_maquina ");
            queryString.Append("		and m.cod_tipo_maquina = tm.cod_tipo_maquina ");
            queryString.Append("		and	ct.cod_planta = @CodPlanta ");
            queryString.Append("		and	ct.cod_proceso = @CodProceso ");
            queryString.Append("		and	ct.cod_centro_trabajo = @CodCentroTrabajo ");
            queryString.Append("		and cb.cod_usuario_autoriza is not null ");
            queryString.Append("		and cb.fecha_inicio <= getdate() ");
            queryString.Append("		and cb.fecha_fin is null ");
            queryString.Append("order by	m.clave_maquina asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerMaquinas
        #region query_ObtenerNumPosicionesBanco
        public static string query_ObtenerNumPosicionesBanco()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	count(*) as NumPosBanco ");
            queryString.Append("from	config_banco_molde_det cbmd, ");
            queryString.Append("		config_banco_molde cbm ");
            queryString.Append("where		cbmd.cod_consecutivo = cbm.cod_consecutivo ");
            queryString.Append("		and	cbmd.cod_config_banco = @CodConfigBanco;");
            return queryString.ToString();
        }
        #endregion query_ObtenerNumPosicionesBanco
        #region query_SuperoLimiteVaciadas
        public static string query_SuperoLimiteVaciadas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	(case when (cb.vaciadas_acumuladas >= cb.limite_vaciadas) then 1 else 0 end) as SuperoLimiteVaciadas ");
            queryString.Append("from	config_banco cb ");
            queryString.Append("where		cb.cod_config_banco = @CodConfigBanco;");
            return queryString.ToString();
        }
        #endregion query_SuperoLimiteVaciadas
        #region query_ActualizarConfigHandHeld
        public static string query_ActualizarConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	config_handheld ");
            queryString.Append("set		cod_supervisor = @CodSupervisor, ");
            queryString.Append("		cod_operador = @CodOperador, ");
            queryString.Append("		cod_config_banco = @CodConfigBanco ");
            queryString.Append("where		cod_config_handheld = @CodConfigHandHeld;");
            return queryString.ToString();
        }
        #endregion query_ActualizarConfigHandHeld
        #region query_ObtenerPiezasCarroHornos
        public static string query_ObtenerPiezasCarroHornos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cpq.cod_pieza as CodPieza ");
            queryString.Append("from	carro_pieza_quemado cpq ");
            queryString.Append("where		cpq.cod_planta = @CodPlanta ");
            queryString.Append("		and	cpq.cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezasCarroHornos
        #region query_TieneConfiguracionCapturaVaciado
        public static string query_TieneConfiguracionCapturaVaciado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	1 ");
            queryString.Append("from	ConfigCapturaVaciado cv ");
            queryString.Append("where		cv.CodPlanta = @CodPlanta ");
            queryString.Append("		and	cv.CodMaquina = @CodMaquina;");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerOperador
        public static string query_ObtenerOperador()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cod_empleado, ");
            queryString.Append("        clave_empleado_MFG, ");
            queryString.Append("        clave_empleado_nomina,");
            queryString.Append("        nombre, ");
            queryString.Append("        ap_paterno, ");
            queryString.Append("        ap_materno, ");
            queryString.Append("        cod_puesto, ");
            queryString.Append("        cod_centro_trabajo ");
            queryString.Append("from	empleado e ");
            queryString.Append("where		e.cod_empleado = @CodOperador;");
            return queryString.ToString();
        }
        #endregion

        #region TieneConfiguracionCapturaVaciado
        public bool TieneConfiguracionCapturaVaciado(int iPlanta, int iMaquina)
        {
            bool bRes = false;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iPlanta;
                pars[1] = new SqlCeParameter("@CodMaquina", SqlDbType.Int);
                pars[1].Value = iMaquina;


                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_TieneConfiguracionCapturaVaciado(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    bRes = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", TieneConfiguracionCapturaVaciado: " + ex.Message);
            }
            return bRes;
        }
        #endregion
        #region ObtenerClaveEmpleadoMFG
        public int ObtenerClaveEmpleadoMFG(int iCodEmpleado, bool bForzarOffine)
        {
            int iClaveEmpleadoMFG = -1;
            bool bClaveEmpleadoMFG = false;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodEmpleado", SqlDbType.Int);
                    pars[0].Value = iCodEmpleado;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerClaveEmpleadoMFG(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iClaveEmpleadoMFG = Convert.ToInt32(dtRes.Rows[0]["ClaveEmpleadoMFG"]);
                    }
                    else
                    {
                        iClaveEmpleadoMFG = -1;
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ObtenerClaveEmpleadoMFG(iCodEmpleado, true, out iClaveEmpleadoMFG, out bClaveEmpleadoMFG);

                        if (!bClaveEmpleadoMFG)
                        {
                            iClaveEmpleadoMFG = -1;
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodEmpleado", SqlDbType.Int);
                        pars[0].Value = iCodEmpleado;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerClaveEmpleadoMFG(), pars);

                        if (dtRes.Rows.Count > 0)
                        {
                            iClaveEmpleadoMFG = Convert.ToInt32(dtRes.Rows[0]["ClaveEmpleadoMFG"]);
                        }
                        else
                        {
                            iClaveEmpleadoMFG = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerClaveEmpleadoMFG: " + ex.Message);
            }
            return iClaveEmpleadoMFG;
        }
        #endregion ObtenerClaveEmpleadoMFG
        #region ObtenerSupervisorPorDefecto
        public DataTable ObtenerSupervisorPorDefecto(int iCodUsuario, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                    pars[0].Value = iCodUsuario;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerSupervisorPorDefecto(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerSupervisorPorDefecto(iCodUsuario, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                        pars[0].Value = iCodUsuario;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerSupervisorPorDefecto(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSupervisorPorDefecto: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerSupervisorPorDefecto
        #region ValidarEmpleadoMFG
        public int ValidarEmpleadoMFG(int iClaveEmpleadoMFG, bool bForzarOffine)
        {
            int iCodEmpleado = -1;
            bool bCodEmpleado = true;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                    pars[0].Value = iClaveEmpleadoMFG;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ValidarEmpleadoMFG(), pars);
                    if (dtRes.Rows.Count > 0)
                    {
                        iCodEmpleado = Convert.ToInt32(dtRes.Rows[0]["CodEmpleado"]);
                    }
                    else
                    {
                        iCodEmpleado = -1;
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ValidarEmpleadoMFG(iClaveEmpleadoMFG, true, out iCodEmpleado, out bCodEmpleado);

                        if (!bCodEmpleado)
                        {
                            iCodEmpleado = -1;
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                        pars[0].Value = iClaveEmpleadoMFG;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ValidarEmpleadoMFG(), pars);
                        if (dtRes.Rows.Count > 0)
                        {
                            iCodEmpleado = Convert.ToInt32(dtRes.Rows[0]["CodEmpleado"]);
                        }
                        else
                        {
                            iCodEmpleado = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ValidarEmpleadoMFG: " + ex.Message);
            }
            return iCodEmpleado;
        }
        #endregion ValidarEmpleadoMFG
        #region ObtenerCentrosTrabajo
        public DataTable ObtenerCentrosTrabajo(int iCodPlanta, int iCodProceso, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerCentrosTrabajo(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerCentrosTrabajo(iCodPlanta, true, iCodProceso, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[2];
                        pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                        pars[0].Value = iCodPlanta;
                        pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                        pars[1].Value = iCodProceso;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerCentrosTrabajo(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCentrosTrabajo: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCentrosTrabajo
        #region ObtenerMaquinas
        public DataTable ObtenerMaquinas(int iCodPlanta, int iCodProceso, int iCodCentroTrabajo, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[3];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodCentroTrabajo", SqlDbType.Int);
                    pars[2].Value = iCodCentroTrabajo;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerMaquinas(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerMaquinas(iCodPlanta, true, iCodProceso, true, iCodCentroTrabajo, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[3];
                        pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                        pars[0].Value = iCodPlanta;
                        pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                        pars[1].Value = iCodProceso;
                        pars[2] = new SqlCeParameter("@CodCentroTrabajo", SqlDbType.Int);
                        pars[2].Value = iCodCentroTrabajo;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerMaquinas(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerMaquinas: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerMaquinas
        #region ObtenerNumPosicionesBanco
        public int ObtenerNumPosicionesBanco(int iCodConfigBanco, bool bForzarOffine)
        {
            int iNumPosBanco = -1;
            bool bNumPosBanco = false;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[0].Value = iCodConfigBanco;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerNumPosicionesBanco(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iNumPosBanco = Convert.ToInt32(dtRes.Rows[0]["NumPosBanco"]);
                    }
                    else
                    {
                        iNumPosBanco = -1;
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ObtenerNumPosicionesBanco(iCodConfigBanco, true, out iNumPosBanco, out bNumPosBanco);

                        if (!bNumPosBanco)
                        {
                            iNumPosBanco = -1;
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                        pars[0].Value = iCodConfigBanco;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerNumPosicionesBanco(), pars);

                        if (dtRes.Rows.Count > 0)
                        {
                            iNumPosBanco = Convert.ToInt32(dtRes.Rows[0]["NumPosBanco"]);
                        }
                        else
                        {
                            iNumPosBanco = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumPosicionesBanco: " + ex.Message);
            }
            return iNumPosBanco;
        }
        #endregion ObtenerNumPosicionesBanco
        #region SuperoLimiteVaciadas
        public bool SuperoLimiteVaciadas(int iCodConfigBanco, bool bForzarOffine)
        {
            bool bSuperoLimiteVaciadasRes = false;
            bool bSuperoLimiteVaciadas = false;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[0].Value = iCodConfigBanco;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_SuperoLimiteVaciadas(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtRes.Rows[0]["SuperoLimiteVaciadas"]) == 1)
                        {
                            bSuperoLimiteVaciadas = true;
                        }
                        else
                        {
                            bSuperoLimiteVaciadas = false;
                        }
                    }
                    else
                    {
                        bSuperoLimiteVaciadas = false;
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.SuperoLimiteVaciadas(iCodConfigBanco, true, out bSuperoLimiteVaciadasRes, out bSuperoLimiteVaciadas);

                        if (!bSuperoLimiteVaciadas)
                        {
                            bSuperoLimiteVaciadasRes = false;
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                        pars[0].Value = iCodConfigBanco;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_SuperoLimiteVaciadas(), pars);

                        if (dtRes.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtRes.Rows[0]["SuperoLimiteVaciadas"]) == 1)
                            {
                                bSuperoLimiteVaciadas = true;
                            }
                            else
                            {
                                bSuperoLimiteVaciadas = false;
                            }
                        }
                        else
                        {
                            bSuperoLimiteVaciadas = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", SuperoLimiteVaciadas: " + ex.Message);
            }
            return bSuperoLimiteVaciadasRes;
        }
        #endregion SuperoLimiteVaciadas
        #region ActualizarConfigHandHeld
        public int ActualizarConfigHandHeld(DA.eTipoConexion tc, int iCodSupervisor, int iCodOperador, int iCodConfigBanco, long lCodConfigHandHeld)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    pars[0] = new SqlCeParameter("@CodSupervisor", SqlDbType.Int);
                    pars[0].Value = iCodSupervisor;
                    pars[1] = new SqlCeParameter("@CodOperador", SqlDbType.Int);
                    pars[1].Value = iCodOperador;
                    pars[2] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[2].Value = iCodConfigBanco;
                    pars[3] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                    pars[3].Value = lCodConfigHandHeld;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_CapturaInicial.query_ActualizarConfigHandHeld(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarConfigHandHeld(lCodConfigHandHeld, true, iCodSupervisor, true,
                                                        iCodOperador, true, iCodConfigBanco, true,
                                                        out iRes, out bRes);

                        if (!bRes)
                        {
                            iRes = -1;
                        }
                    }
                    else
                    {
                        iRes = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarConfigHandHeld: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarConfigHandHeld
        #region ObtenerPiezasCarroHornos
        public DataTable ObtenerPiezasCarroHornos(int iCodPlanta, int iCodCarro)
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPiezaCarroHornos(iCodPlanta, true, iCodCarro, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                    pars[1].Value = iCodCarro;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerPiezasCarroHornos(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasCarroHornos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezasCarroHornos

        #region EsCasetaTanque
        public bool EsCasetaTanque(int iCodCaseta, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            DataTable dtRes = null;
            bool bEsCasetaTanque = false;
            bool bEsCasetaTanqueResult = true;
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.EsCasetaTanque(iCodCaseta, true, iCodTanque, true, iCodProceso, true, iCodPlanta, true, out bEsCasetaTanque, out bEsCasetaTanqueResult);
                    return bEsCasetaTanque;
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    pars[0] = new SqlCeParameter("@CodCaseta", SqlDbType.Int);
                    pars[0].Value = iCodCaseta;
                    pars[1] = new SqlCeParameter("@CodTanque", SqlDbType.Int);
                    pars[1].Value = iCodTanque;
                    pars[2] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[2].Value = iCodProceso;
                    pars[3] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[3].Value = iCodPlanta;
                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_EsCasetaTanque(), pars);
                }
                if (dtRes == null) return false;
                if (dtRes.Rows.Count <= 0)
                {
                    dtRes.Dispose();
                    return false;
                }
                dtRes.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EsCasetaTanque: " + ex.Message);
            }
        }
        #endregion EsCasetaTanque

        #region TieneReglaPlanta
        public bool TieneReglaPlanta(int iCodPlanta)
        {
            DataTable dtRes = null;
            string sFiltro = "'%BJ%'";
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPlantas();
                    if (dtRes == null) return false;
                    DataRow[] rows = dtRes.Select("CodPlanta = " + iCodPlanta.ToString() + " AND DesPlanta LIKE " + sFiltro);
                    return (rows.Length > 0) ? true : false;
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@cod_planta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@des_planta", SqlDbType.NVarChar);
                    pars[1].Value = sFiltro;
                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_TieneReglaPlanta(), pars);
                }
                if (dtRes == null) return false;
                if (dtRes.Rows.Count <= 0)
                {
                    dtRes.Dispose();
                    return false;
                }
                dtRes.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", TieneReglaPlanta: " + ex.Message);
            }
        }
        #endregion TieneReglaPlanta
        #region ObtenerPiezasCarroHornos
        public DataTable ObtenerOperador(int iCodOperador, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                //if (!bForzarOffine && this.oDA0.EstaServicioDisponible())
                //{
                //    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                //    dtRes = proxy.ObtenerOperador(iCodOperador, true);
                //}
                //else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodOperador", SqlDbType.Int);
                    pars[0].Value = iCodOperador;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_CapturaInicial.query_ObtenerOperador(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasCarroHornos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion

        #region query_ObtenerEsCasetaTanque
        public static string query_EsCasetaTanque()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT CT.CodCaseta, CT.CodTanque ");
            queryString.Append("FROM CasetaTanque CT ");
            queryString.Append("where CT.CodCaseta = @CodCaseta OR CT.CodTanque = @CodTanque ");
            queryString.Append(" AND CT.CodProceso = @CodProceso AND CT.CodPlanta = @CodPlanta;");
            return queryString.ToString();
        }
        #endregion query_ObtenerEsCasetaTanque
        #region query_TieneReglaPlanta
        public static string query_TieneReglaPlanta()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT P.cod_planta, P.des_planta ");
            queryString.Append("FROM planta P ");
            queryString.Append("WHERE P.cod_planta = @cod_planta AND P.des_planta LIKE @des_planta");
            return queryString.ToString();
        }
        #endregion query_TieneReglaPlanta

        #endregion common
        #endregion methods

    }
}
