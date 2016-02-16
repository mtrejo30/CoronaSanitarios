using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c04_Defectos
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c04_Defectos()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c04_Defectos()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerDefectos
        public static string query_ObtenerDefectos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	d.cod_defecto as CodDefecto, ");
            queryString.Append("		d.clave_defecto as ClaveDefecto, ");
            queryString.Append("		(d.clave_defecto + ' - ' + d.des_defecto) as DesDefecto ");
            queryString.Append("from	defecto d ");
            queryString.Append("where		d.cod_proceso = @CodProceso ");
            queryString.Append("order by	d.clave_defecto asc;");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerTipoArticuloPieza
        public static string query_ObtenerTipoArticuloPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT ");
            queryString.Append("	ta.cod_tipo_articulo, ");
            queryString.Append("	ta.clave_tipo_articulo, ");
            queryString.Append("	ta.des_tipo_articulo, ");
            queryString.Append("	ta.fecha_registro ");
            queryString.Append("FROM           ");
            queryString.Append("	pieza p ");
            queryString.Append("	INNER JOIN articulo a ");
            queryString.Append("		ON a.cod_articulo = p.cod_articulo ");
            queryString.Append("	INNER JOIN tipo_articulo ta ");
            queryString.Append("		ON	ta.cod_tipo_articulo = a.cod_tipo_articulo ");
            queryString.Append("WHERE           ");
            queryString.Append("	p.cod_pieza = @CodigoPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerDefectos
        #region query_ObtenerZonasDefecto
        public static string query_ObtenerZonasDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	zd.cod_zona_defecto as CodZonaDefecto, ");
            queryString.Append("		zd.clave_zona_defecto as ClaveZonaDefecto, ");
            queryString.Append("		(zd.clave_zona_defecto + ' - ' + zd.des_zona_defecto) as DesZonaDefecto ");
            queryString.Append("from	zona_defecto zd ");
            queryString.Append("    	INNER JOIN TipoArticuloZona taz ");
            queryString.Append("    	    ON taz.CodigoZona = zd.cod_zona_defecto ");
            queryString.Append("WHERE	taz.CodigoTipoArticulo = @CodigoTipoArticulo ");
            queryString.Append("order by	zd.clave_zona_defecto asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerZonasDefecto
        #region query_ObtenerEstadosDefecto
        public static string query_ObtenerEstadosDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ed.cod_estado_defecto as CodEstadoDefecto, ");
            queryString.Append("		ed.des_estado_defecto as DesEstadoDefecto ");
            queryString.Append("from	estado_defecto ed ");
            queryString.Append("where		(ed.cod_estado_defecto = @CodEstadoDefecto1 ");
            queryString.Append("		or	ed.cod_estado_defecto = @CodEstadoDefecto2) ");
            queryString.Append("order by	ed.cod_estado_defecto asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerEstadosDefecto
        #region query_ObtenerDefectosPiezaProceso
        public static string query_ObtenerDefectosPiezaProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pd.cod_defecto as CodDefecto, ");
            queryString.Append("		(d.clave_defecto + ' - ' + d.des_defecto) as DesDefecto, ");
            queryString.Append("		pd.cod_zona_defecto as CodZonaDefecto, ");
            queryString.Append("		(zd.clave_zona_defecto + ' - ' + zd.des_zona_defecto) as DesZonaDefecto, ");
            queryString.Append("		pd.cod_estado_defecto as CodEstadoDefecto, ");
            queryString.Append("		ed.des_estado_defecto as DesEstadoDefecto ");
            queryString.Append("from	pieza_defecto pd, ");
            queryString.Append("		defecto d, ");
            queryString.Append("		zona_defecto zd, ");
            queryString.Append("		estado_defecto ed ");
            queryString.Append("where		pd.eliminado = 0 ");
            queryString.Append("		and	pd.cod_defecto = d.cod_defecto ");
            queryString.Append("		and	pd.cod_zona_defecto = zd.cod_zona_defecto ");
            queryString.Append("		and	pd.cod_estado_defecto = ed.cod_estado_defecto ");
            queryString.Append("		and	pd.cod_pieza = @CodPieza ");
            queryString.Append("		and	pd.cod_proceso = @CodProceso ");
            queryString.Append("order by	pd.cod_estado_defecto asc, ");
            queryString.Append("			pd.cod_zona_defecto asc, ");
            queryString.Append("			pd.cod_defecto asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerDefectosPiezaProceso
        #region query_InsertarPiezaDefecto
        public static string query_InsertarPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into pieza_defecto ");
            queryString.Append("(cod_pieza, cod_proceso, cod_defecto, cod_zona_defecto, cod_estado_defecto, cod_empleado, fecha_ultimo_movimiento, fecha_registro, nuevo) ");
            queryString.Append("values (@CodPieza, @CodProceso, @CodDefecto, @CodZonaDefecto, @CodEstadoDefecto, @CodEmpleado, @FechaUltimoMovimiento, @FechaRegistro, 1);");
            return queryString.ToString();
        }
        #endregion query_InsertarPiezaDefecto
        #region query_MarcarEliminadaPiezaDefecto
        public static string query_MarcarEliminadaPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		eliminado = 1 ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_MarcarEliminadaPiezaDefecto
        #region query_EliminarPiezaDefecto
        public static string query_EliminarPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	pieza_defecto ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_EliminarPiezaDefecto
        #region query_ActualizarPiezaDefecto
        public static string query_ActualizarPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		cod_estado_defecto = @CodEstadoDefecto, ");
            queryString.Append("		cod_empleado = @CodEmpleado, ");
            queryString.Append("		fecha_ultimo_movimiento = @FechaUltimoMovimiento, ");
            queryString.Append("		modificado = 1");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_ActualizarPiezaDefecto
        #region query_ActualizarPiezaUltimoEstado
        public static string query_ActualizarPiezaUltimoEstado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_ultimo_estado = @CodUltimoEstado, ");
            queryString.Append("		modificado_estado = 1 ");
            queryString.Append("where   modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarPiezaUltimoEstado

        #region ObtenerDefectos
        public DataTable ObtenerDefectos(int iCodProceso, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[0].Value = iCodProceso;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerDefectos(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerDefectos(iCodProceso, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                        pars[0].Value = iCodProceso;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerDefectos(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDefectos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerDefectos
        #region ObtenerZonasDefecto
        public DataTable ObtenerZonasDefecto(int iTipoArticulo, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodigoTipoArticulo", SqlDbType.Int);
                    pars[0].Value = iTipoArticulo;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerZonasDefecto(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerZonasDefecto(iTipoArticulo, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodigoTipoArticulo", SqlDbType.Int);
                        pars[0].Value = iTipoArticulo;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerZonasDefecto(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerZonasDefecto: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerZonasDefecto
        #region ObtenerEstadosDefecto
        public DataTable ObtenerEstadosDefecto(int iCodEstadoDefecto1, int iCodEstadoDefecto2, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodEstadoDefecto1", SqlDbType.Int);
                    pars[0].Value = iCodEstadoDefecto1;
                    pars[1] = new SqlCeParameter("@CodEstadoDefecto2", SqlDbType.Int);
                    pars[1].Value = iCodEstadoDefecto2;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerEstadosDefecto(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerEstadosDefecto(iCodEstadoDefecto1, true, iCodEstadoDefecto2, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[2];
                        pars[0] = new SqlCeParameter("@CodEstadoDefecto1", SqlDbType.Int);
                        pars[0].Value = iCodEstadoDefecto1;
                        pars[1] = new SqlCeParameter("@CodEstadoDefecto2", SqlDbType.Int);
                        pars[1].Value = iCodEstadoDefecto2;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerEstadosDefecto(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerEstadosDefecto: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerEstadosDefecto
        #region ObtenerDefectosPiezaProceso
        public DataTable ObtenerDefectosPiezaProceso(DA.eTipoConexion tc, int iCodPieza, int iCodProceso)
        {
            DataTable dtRes = null;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerDefectosPiezaProceso(), pars);
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    dtRes = new DataTable();
                    dtRes.Columns.Add("CodDefecto", typeof(int));
                    dtRes.Columns.Add("DesDefecto", typeof(string));
                    dtRes.Columns.Add("CodZonaDefecto", typeof(int));
                    dtRes.Columns.Add("DesZonaDefecto", typeof(string));
                    dtRes.Columns.Add("CodEstadoDefecto", typeof(int));
                    dtRes.Columns.Add("DesEstadoDefecto", typeof(string));

                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ObtenerDefectosPiezaProceso(iCodPieza, true, iCodProceso, true);

                        HHsvc.HHDefecto elemento = null;
                        DataRow dr = null;
                        foreach (HHsvc.BSE e in res)
                        {
                            elemento = (HHsvc.HHDefecto)e;
                            dr = dtRes.NewRow();
                            dr["CodDefecto"] = elemento.CodDefecto;
                            dr["DesDefecto"] = elemento.DesDefecto;
                            dr["CodZonaDefecto"] = elemento.CodZonaDefecto;
                            dr["DesZonaDefecto"] = elemento.DesZonaDefecto;
                            dr["CodEstadoDefecto"] = elemento.CodEstadoDefecto;
                            dr["DesEstadoDefecto"] = elemento.DesEstadoDefecto;
                            dtRes.Rows.Add(dr);
                        }
                        dtRes.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDefectosPiezaProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerDefectosPiezaProceso
        #region InsertarPiezaDefecto
        public int InsertarPiezaDefecto(DA.eTipoConexion tc, int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto, int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento, DateTime dtFechaRegistro)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[8];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = iCodDefecto;
                    pars[3] = new SqlCeParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = iCodZonaDefecto;
                    pars[4] = new SqlCeParameter("@CodEstadoDefecto", SqlDbType.Int);
                    pars[4].Value = iCodEstadoDefecto;
                    pars[5] = new SqlCeParameter("@CodEmpleado", SqlDbType.Int);
                    pars[5].Value = iCodEmpleado;
                    pars[6] = new SqlCeParameter("@FechaUltimoMovimiento", SqlDbType.DateTime);
                    pars[6].Value = dtFechaUltimoMovimiento;
                    pars[7] = new SqlCeParameter("@FechaRegistro", SqlDbType.DateTime);
                    pars[7].Value = dtFechaRegistro;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_Defectos.query_InsertarPiezaDefecto(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarPiezaDefecto( iCodPieza, true, iCodProceso, true, iCodDefecto, true, iCodZonaDefecto, true,
                                                    iCodEstadoDefecto, true, iCodEmpleado, true, dtFechaUltimoMovimiento, true, dtFechaRegistro, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", InsertarPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarPiezaDefecto
        #region MarcarEliminadaPiezaDefecto
        public int MarcarEliminadaPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodDefecto", SqlDbType.Int);
                pars[2].Value = iCodDefecto;
                pars[3] = new SqlCeParameter("@CodZonaDefecto", SqlDbType.Int);
                pars[3].Value = iCodZonaDefecto;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_Defectos.query_MarcarEliminadaPiezaDefecto(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", MarcarEliminadaPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion MarcarEliminadaPiezaDefecto
        #region EliminarPiezaDefecto
        public int EliminarPiezaDefecto(DA.eTipoConexion tc, int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = iCodDefecto;
                    pars[3] = new SqlCeParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = iCodZonaDefecto;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_Defectos.query_EliminarPiezaDefecto(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.EliminarPiezaDefecto(iCodPieza, true, iCodProceso, true, iCodDefecto, true, iCodZonaDefecto, true,
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
                throw new Exception(this.sClassName + ", EliminarPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarPiezaDefecto
        #region ActualizarPiezaDefecto
        public int ActualizarPiezaDefecto(DA.eTipoConexion tc, int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto, int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[7];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = iCodDefecto;
                    pars[3] = new SqlCeParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = iCodZonaDefecto;
                    pars[4] = new SqlCeParameter("@CodEstadoDefecto", SqlDbType.Int);
                    pars[4].Value = iCodEstadoDefecto;
                    pars[5] = new SqlCeParameter("@CodEmpleado", SqlDbType.Int);
                    pars[5].Value = iCodEmpleado;
                    pars[6] = new SqlCeParameter("@FechaUltimoMovimiento", SqlDbType.DateTime);
                    pars[6].Value = dtFechaUltimoMovimiento;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_Defectos.query_ActualizarPiezaDefecto(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarPiezaDefecto(   iCodPieza, true, iCodProceso, true, iCodDefecto, true, iCodZonaDefecto, true,
                                                        iCodEstadoDefecto, true, iCodEmpleado, true, dtFechaUltimoMovimiento, true,
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
                throw new Exception(this.sClassName + ", ActualizarPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarPiezaDefecto
        #region ActualizarPiezaUltimoEstado
        public int ActualizarPiezaUltimoEstado(DA.eTipoConexion tc, int iCodPieza, int iCodUltimoEstado)
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
                    pars[1] = new SqlCeParameter("@CodUltimoEstado", SqlDbType.Int);
                    pars[1].Value = iCodUltimoEstado;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c04_Defectos.query_ActualizarPiezaUltimoEstado(), pars);

                    iRes = 0;
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarPiezaUltimoEstado(  iCodPieza, true, iCodUltimoEstado, true,
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
                throw new Exception(this.sClassName + ", ActualizarPiezaUltimoEstado: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarPiezaUltimoEstado
        #region ObtenerTipoArticuloPieza
        public DataTable ObtenerTipoArticuloPieza(int iCodigoPieza, bool bForzarOffine)
        {
            DataTable dtRes = null;
            try
            {
                if (iCodigoPieza < 1) throw new Exception("No se pueden cargar los defectos, no se proporciono una pieza Valida.");
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodigoPieza", SqlDbType.Int);
                    pars[0].Value = iCodigoPieza;
                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerTipoArticuloPieza(), pars);
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        dtRes = proxy.ObtenerTipoArticuloPieza(iCodigoPieza, true);
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodigoPieza", SqlDbType.Int);
                        pars[0].Value = iCodigoPieza;
                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c04_Defectos.query_ObtenerTipoArticuloPieza(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTipoArticuloPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #endregion common

        #endregion methods

    }
}
