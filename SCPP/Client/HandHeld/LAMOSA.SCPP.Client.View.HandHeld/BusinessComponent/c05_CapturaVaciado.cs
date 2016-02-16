using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c05_CapturaVaciado
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public c05_CapturaVaciado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c05_CapturaVaciado()
        {

        }
        #endregion constructors and destructor

        #region common

        #region query_ObtenerPosicionesBanco
        public static string query_ObtenerPosicionesBanco()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cbmd.cod_consecutivo as CodConsecutivo, ");
            queryString.Append("		cbm.posicion as Posicion, ");
            queryString.Append("		cbmd.cod_molde as CodMolde ");
            queryString.Append("from	config_banco_molde_det cbmd, ");
            queryString.Append("		config_banco_molde cbm ");
            queryString.Append("where		cbmd.cod_consecutivo = cbm.cod_consecutivo ");
            queryString.Append("		and	cbmd.cod_config_banco = @CodConfigBanco ");
            queryString.Append("order by	cbm.posicion asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPosicionesBanco
        #region query_ObtenerArticulosMolde
        public static string query_ObtenerArticulosMolde()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodArticulo, ");
            queryString.Append("		a.clave_articulo as ClaveArticulo, ");
            queryString.Append("		a.clave_articulo + ' - ' + a.des_articulo as DesArticulo, ");
            queryString.Append("		ta.cod_tipo_articulo as CodTipoArticulo, ");
            queryString.Append("		ta.clave_tipo_articulo as ClaveTipoArticulo, ");
            queryString.Append("		ta.clave_tipo_articulo + ' - ' + ta.des_tipo_articulo as DesTipoArticulo ");
            queryString.Append("from	articulo a, ");
            queryString.Append("		tipo_articulo ta ");
            queryString.Append("where		a.cod_tipo_articulo = ta.cod_tipo_articulo ");
            queryString.Append("		and	a.cod_molde = @CodMolde ");
            queryString.Append("order by	a.clave_articulo asc; ");
            return queryString.ToString();
        }
        #endregion query_ObtenerArticulosMolde
        #region query_ActualizarVaciadasAcumuladas
        public static string query_ActualizarVaciadasAcumuladas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	config_banco ");
            queryString.Append("set		vaciadas_acumuladas = vaciadas_acumuladas + 1, ");
            queryString.Append("		actualizacion = 1 ");
            queryString.Append("where		cod_config_banco = @CodConfigBanco;");
            return queryString.ToString();
        }
        #endregion query_ActualizarVaciadasAcumuladas
        #region query_EliminarDefectosPiezaLocal
        public static string query_EliminarDefectosPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	pieza_defecto ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append(" and	cod_defecto > -1 ");
            queryString.Append(" and	cod_zona_defecto > -1;"); 
            return queryString.ToString();
        }
        #endregion query_EliminarDefectosPiezaLocal
        #region query_EliminarPiezaLocal
        public static string query_EliminarPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	pieza ");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_EliminarPiezaLocal

        #region ObtenerPosicionesBanco
        public DataTable ObtenerPosicionesBanco(int iCodConfigBanco, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[0].Value = iCodConfigBanco;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_CapturaVaciado.query_ObtenerPosicionesBanco(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerPosicionesBanco(iCodConfigBanco, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                        pars[0].Value = iCodConfigBanco;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_CapturaVaciado.query_ObtenerPosicionesBanco(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPosicionesBanco: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPosicionesBanco
        #region ObtenerArticulosMolde
        public DataTable ObtenerArticulosMolde(int iCodMolde, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodMolde", SqlDbType.Int);
                    pars[0].Value = iCodMolde;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_CapturaVaciado.query_ObtenerArticulosMolde(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerArticulosMolde(iCodMolde, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodMolde", SqlDbType.Int);
                        pars[0].Value = iCodMolde;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_CapturaVaciado.query_ObtenerArticulosMolde(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerArticulosMolde: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerArticulosMolde
        #region ActualizarVaciadasAcumuladas
        public int ActualizarVaciadasAcumuladas(DA.eTipoConexion tc, int iCodConfigBanco)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[0].Value = iCodConfigBanco;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c05_CapturaVaciado.query_ActualizarVaciadasAcumuladas(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarVaciadasAcumuladas(iCodConfigBanco, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", ActualizarVaciadasAcumuladas: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarVaciadasAcumuladas
        #region EliminarDefectosPiezaLocal
        public int EliminarDefectosPiezaLocal(int iCodPieza, int iCodProceso)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c05_CapturaVaciado.query_EliminarDefectosPiezaLocal(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarDefectosPiezaLocal: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarDefectosPiezaLocal
        #region EliminarPiezaLocal
        public int EliminarPiezaLocal(int iCodPieza)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c05_CapturaVaciado.query_EliminarPiezaLocal(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarPiezaLocal: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarPiezaLocal

        #endregion common

        #endregion methods

    }
}
