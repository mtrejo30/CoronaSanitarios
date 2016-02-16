using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c11_CapturaEmpaque
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c11_CapturaEmpaque()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c11_CapturaEmpaque()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerModelos2
        public static string query_ObtenerModelos2()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodModelo, ");
            queryString.Append("		a.clave_articulo as ClaveModelo, ");
            queryString.Append("		(a.clave_articulo + ' - ' + a.des_articulo) as DesModelo, ");
            queryString.Append("		a.cod_tipo_articulo as CodTipoArticulo, ");
            queryString.Append("		ta.clave_tipo_articulo as ClaveTipoArticulo, ");
            queryString.Append("		(ta.clave_tipo_articulo + ' - ' + ta.des_tipo_articulo) as DesTipoArticulo ");
            queryString.Append("from	articulo a, ");
            queryString.Append("		tipo_articulo ta ");
            queryString.Append("where		a.cod_tipo_articulo = ta.cod_tipo_articulo ");
            queryString.Append("		and len(a.clave_articulo) = 4 ");
            queryString.Append("		and	a.cod_molde = @CodMolde ");
            queryString.Append("order by	a.clave_articulo asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerModelos2
        #region query_ObtenerCodMoldePieza
        public static string query_ObtenerCodMoldePieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_molde as CodMolde ");
            queryString.Append("from	pieza p, ");
            queryString.Append("		articulo a ");
            queryString.Append("where		p.cod_articulo = a.cod_articulo ");
            queryString.Append("		and	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCodMoldePieza
        #region query_ActualizarModeloPieza
        public static string query_ActualizarModeloPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_articulo = @CodModelo, ");
            queryString.Append("		actualizacion = 1 ");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarModeloPieza
        #region query_ActualizarCalidadPieza
        public static string query_ActualizarCalidadPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_calidad = @CodCalidad, ");
            queryString.Append("		actualizacion = 1 ");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCalidadPieza

        #region ObtenerModelos2
        public DataTable ObtenerModelos2(int iCodMolde)
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerModelos2(iCodMolde, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodMolde", SqlDbType.Int);
                    pars[0].Value = iCodMolde;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c11_CapturaEmpaque.query_ObtenerModelos2(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModelos2: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerModelos2
        #region ObtenerCodMoldePieza
        public int ObtenerCodMoldePieza(int iCodPieza)
        {
            int iCodMolde = -1;
            bool bCodMolde = false;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerCodMoldePieza(iCodPieza, true, out iCodMolde, out bCodMolde);

                    if (!bCodMolde)
                    {
                        iCodMolde = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c11_CapturaEmpaque.query_ObtenerCodMoldePieza(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodMolde = Convert.ToInt32(dtRes.Rows[0]["CodMolde"]);
                    }
                    else
                    {
                        iCodMolde = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodMoldePieza: " + ex.Message);
            }
            return iCodMolde;
        }
        #endregion ObtenerCodMoldePieza
        #region ActualizarModeloPieza
        public int ActualizarModeloPieza(DA.eTipoConexion tc, int iCodPieza, int iCodModelo)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodModelo", SqlDbType.Int);
                    pars[1].Value = iCodModelo;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c11_CapturaEmpaque.query_ActualizarModeloPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarModeloPieza(iCodPieza, true, iCodModelo, true,
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
                throw new Exception(this.sClassName + ", ActualizarModeloPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarModeloPieza
        #region ActualizarCalidadPieza
        public int ActualizarCalidadPieza(DA.eTipoConexion tc, int iCodPieza, int iCodCalidad)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodCalidad", SqlDbType.Int);
                    pars[1].Value = iCodCalidad;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c11_CapturaEmpaque.query_ActualizarCalidadPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarCalidadPieza(   iCodPieza, true, iCodCalidad, true,
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
                throw new Exception(this.sClassName + ", ActualizarCalidadPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCalidadPieza
        public bool HabilitarImpresionEtiqueta(int CodigoPieza)
        {
            bool bResult = false;
            bool bResultSpecified = true;
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.HabilitarImpresionEtiqueta(CodigoPieza, true, out bResult, out bResultSpecified);
                    return bResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", HabilitarImpresionEtiqueta: " + ex.Message);
            }
            return bResult;
        }
        #endregion common

        #endregion methods

    }
}
