using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c03_ConfiguracionInicial
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c03_ConfiguracionInicial()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c03_ConfiguracionInicial()
        {

        }
        #endregion Constructors and Destructor

        #region common

        #region query_ObtenerTurnos
        public static string query_ObtenerTurnos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	t.cod_turno as CodTurno, ");
            queryString.Append("		t.des_turno as DesTurno ");
            queryString.Append("from	turno t ");
            queryString.Append("order by	t.des_turno asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerTurnos
        #region query_ObtenerProcesos2
        public static string query_ObtenerProcesos2()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_proceso as CodProceso, ");
            queryString.Append("		p.des_proceso as DesProceso ");
            queryString.Append("from	proceso p ");
            queryString.Append("where		p.cod_proceso not in (0, 6) ");
            queryString.Append("order by	p.cod_proceso asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerProcesos2
        #region query_ObtenerProcesosPorRol
        public static string query_ObtenerProcesosPorRol()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_proceso as CodProceso, ");
            queryString.Append("p.des_proceso as DesProceso ");
            queryString.Append("from proceso p, ");
            queryString.Append("permisoPantalla pp ");
            queryString.Append("where pp.CodRol = @Rol ");
            queryString.Append("and p.Cod_Proceso = pp.codProceso ");
            queryString.Append("order by p.cod_proceso asc;");
            return queryString.ToString();
        }
        #endregion 
        #region query_ObtenerPantallasProceso
        public static string query_ObtenerPantallasProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pp.cod_pantalla as CodPantalla, ");
            queryString.Append("		p.des_pantalla as DesPantalla ");
            queryString.Append("from	proceso_pantalla pp, ");
            queryString.Append("		HHpantalla p ");
            queryString.Append("where		pp.cod_pantalla = p.cod_pantalla ");
            queryString.Append("		and	pp.cod_proceso = @CodProceso ");
            queryString.Append("		and p.cod_pantalla not in (5, 6) ");
            queryString.Append("order by	p.cod_pantalla asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPantallasProceso
        #region query_ObtenerSigCodConfigHandHeld
        public static string query_ObtenerSigCodConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	((case when max(ch.cod_config_handheld) is null then 0 else max(ch.cod_config_handheld) end) + 1) as CodConfigHandHeld ");
            queryString.Append("from	config_handheld ch ");
            queryString.Append("where		ch.cod_config_handheld between 1 and 49999;");
            return queryString.ToString();
        }
        #endregion query_ObtenerSigCodConfigHandHeld
        #region query_InsertarConfigHandHeld
        public static string query_InsertarConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into config_handheld ");
            queryString.Append("(cod_config_handheld, cod_usuario, cod_operador, cod_supervisor, fecha, cod_turno, cod_planta, cod_proceso, cod_config_banco, fecha_registro) ");
            queryString.Append("values (@CodConfigHandHeld, @CodUsuario, @CodOperador, @CodSupervisor, @Fecha , @CodTurno, @CodPlanta, @CodProceso, @CodConfigBanco, getdate());");
            return queryString.ToString();
        }
        #endregion query_InsertarConfigHandHeld

        #region ObtenerTurnos
        public DataTable ObtenerTurnos()
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerTurnos();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c03_ConfiguracionInicial.query_ObtenerTurnos(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTurnos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerTurnos
        #region ObtenerProcesos2
        public DataTable ObtenerProcesos2()
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerProcesos2();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c03_ConfiguracionInicial.query_ObtenerProcesos2(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerProcesos2: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerProcesos2
        #region ObtenerProcesosPorRol
        public DataTable ObtenerProcesosPorRol(int rol)
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerProcesosPorRol(rol, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@Rol", SqlDbType.Int);
                    pars[0].Value = rol;
                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c03_ConfiguracionInicial.query_ObtenerProcesosPorRol(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerProcesosPorRol: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region ObtenerPantallasProceso
        public DataTable ObtenerPantallasProceso(int iCodProceso)
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPantallasProceso(iCodProceso, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[0].Value = iCodProceso;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c03_ConfiguracionInicial.query_ObtenerPantallasProceso(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPantallasProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPantallasProceso
        #region ObtenerSigCodConfigHandHeld
        private long ObtenerSigCodConfigHandHeld()
        {
            long lCodConfigHandHeld = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c03_ConfiguracionInicial.query_ObtenerSigCodConfigHandHeld(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    lCodConfigHandHeld = Convert.ToInt64(dtRes.Rows[0]["CodConfigHandHeld"]);
                }
                else
                {
                    lCodConfigHandHeld = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSigCodConfigHandHeld: " + ex.Message);
            }
            return lCodConfigHandHeld;
        }
        #endregion ObtenerSigCodConfigHandHeld
        #region InsertarConfigHandHeld
        public long InsertarConfigHandHeld(DA.eTipoConexion tc, int iCodUsuario, int iCodOperador, int iCodSupervisor, DateTime dtFecha, int iCodTurno, int iCodPlanta, int iCodProceso, int? iCodConfigBanco, DateTime? dtFechaRegistro)
        {
            long lCodConfigHandHeld = -1;
            bool bCodConfigHandHeld = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    lCodConfigHandHeld = this.ObtenerSigCodConfigHandHeld();

                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[9];
                    pars[0] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                    pars[0].Value = lCodConfigHandHeld;
                    pars[1] = new SqlCeParameter("@CodUsuario", SqlDbType.Int);
                    pars[1].Value = iCodUsuario;
                    pars[2] = new SqlCeParameter("@CodOperador", SqlDbType.Int);
                    pars[2].Value = iCodOperador;
                    pars[3] = new SqlCeParameter("@CodSupervisor", SqlDbType.Int);
                    pars[3].Value = iCodSupervisor;
                    pars[4] = new SqlCeParameter("@Fecha", SqlDbType.DateTime);
                    pars[4].Value = dtFecha;
                    pars[5] = new SqlCeParameter("@CodTurno", SqlDbType.Int);
                    pars[5].Value = iCodTurno;
                    pars[6] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[6].Value = iCodPlanta;
                    pars[7] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[7].Value = iCodProceso;
                    pars[8] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    if (iCodConfigBanco == null) pars[8].Value = DBNull.Value; else pars[8].Value = iCodConfigBanco;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c03_ConfiguracionInicial.query_InsertarConfigHandHeld(), pars);
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        //dtFecha = this.oDA0.ObtenerFechaServidor();

                        proxy.InsertarConfigHandHeld(iCodUsuario, true, iCodOperador, true, iCodSupervisor, true,
                                                        dtFecha, true, iCodTurno, true, iCodPlanta, true,
                                                        iCodProceso, true, iCodConfigBanco.Value, true, dtFechaRegistro.Value, true,
                                                        out lCodConfigHandHeld, out bCodConfigHandHeld);

                        if (!bCodConfigHandHeld)
                        {
                            lCodConfigHandHeld = -1;
                        }
                    }
                    else
                    {
                        lCodConfigHandHeld = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarConfigHandHeld: " + ex.Message);
            }
            return lCodConfigHandHeld;
        }
        #endregion InsertarConfigHandHeld
        #region ExisteInventarioProcesoActivo
        public int ExisteInventarioProcesoActivo()
        {
            int iCodInventarioProceso = -1;
            bool bCodInventarioProceso = false;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExisteInventarioProcesoActivo(out iCodInventarioProceso, out bCodInventarioProceso);

                    if (!bCodInventarioProceso)
                    {
                        iCodInventarioProceso = -1;
                    }
                }
                else
                {
                    iCodInventarioProceso = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteInventarioProcesoActivo: " + ex.Message);
            }
            return iCodInventarioProceso;
        }
        #endregion ExisteInventarioProcesoActivo

        #endregion common

        #endregion methods

    }
}
