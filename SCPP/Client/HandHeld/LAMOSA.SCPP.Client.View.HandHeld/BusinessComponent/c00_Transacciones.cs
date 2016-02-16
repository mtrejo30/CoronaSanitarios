using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c00_Transacciones
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;
        LoginUsuario lu = new LoginUsuario();
        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c00_Transacciones()
        {
            this.sClassName = this.GetType().FullName;
        }
        public c00_Transacciones(LoginUsuario lu)
        {
            this.lu = lu;
        }
        ~c00_Transacciones()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_EstablecerActualizacionTarimaPiezaMod
        public static string query_EstablecerActualizacionTarimaPiezaMod()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	TarimaPieza ");
            queryString.Append("set		Modificado = 0 ");
            queryString.Append("where CodTarima = @CodTarima AND CodPieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionTarimaPieza2
        #region query_ObtenerInventarioProceso
        public static string query_ObtenerInventarioProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ip.IdPieza as IdPieza ");
            queryString.Append("from	InventarioProceso ip ");
            queryString.Append("where		ip.actualizado = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerInventarioProceso
        #region query_DeleteInventarioProceso
        public static string query_DeleteInventarioProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete from	InventarioProceso ");
            queryString.Append("where actualizado = 1 and IdPieza = @IdPieza;");
            return queryString.ToString();
        }
        #endregion query_DeleteInventarioProceso
        #region query_ObtenerInsertarPiezaInventario
        public static string query_ObtenerInsertarPiezaInventario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ip.cod_barras as cod_barras ");
            queryString.Append("    	,ip.cod_planta as cod_planta ");
            queryString.Append("    	,ip.cod_proceso as cod_proceso ");
            queryString.Append("    	,ip.cod_articulo as cod_articulo ");
            queryString.Append("    	,ip.cod_color as cod_color ");
            queryString.Append("    	,ip.cod_calidad as cod_calidad ");
            queryString.Append("    	,ip.cod_ultimo_estado as cod_ultimo_estado ");

            queryString.Append("from	InventarioProcesoPieza ip ");
            queryString.Append("where		ip.NUEVO = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerInsertarPiezaInventario
        #region query_DeleteInsertarPiezaInventario
        public static string query_DeleteInsertarPiezaInventario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete from	InventarioProcesoPieza ");
            queryString.Append("where nuevo = 1 and cod_barras = ''''+@CodBarras+'''';");
            return queryString.ToString();
        }
        #endregion query_DeleteInsertarPiezaInventario
        #region query_ObtenerActualizacionTarimaPieza
        public static string query_ObtenerActualizacionTarimaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select CodTarima, CodPieza, Paletizado, Rechazada, FechaRegistro, Modificado ");
            queryString.Append("from TarimaPieza ");
            queryString.Append("where Modificado = 1;");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerReemplazoEtiquetas
        public static string query_ObtenerReemplazoEtiquetas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza as cod_pieza ");
            queryString.Append("	    ,p.cod_proceso as cod_proceso ");
            queryString.Append("	    ,p.fecha_registro as fecha_registro ");
            queryString.Append("from	pieza_reemplazo p ");
            return queryString.ToString();
        }
        #endregion query_ObtenerReemplazoEtiquetas
        #region query_DeleteReemplazoEtiquetas
        public static string query_DeleteReemplazoEtiquetas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete from	pieza_reemplazo ");
            queryString.Append("where cod_pieza = @IdPieza and cod_proceso = @IdProceso;");
            return queryString.ToString();
        }
        #endregion query_DeleteReemplazoEtiquetas

        #region ObtenerTarimaPieza
        public DataTable ObtenerActualizacionTarimaPieza()
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerActualizacionTarimaPieza(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerActualizacionTarimaPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region EstablecerActualizacionTarimaPiezaMod
        public int EstablecerActualizacionTarimaPiezaMod(int iCodTarima, int iCodPieza)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                pars[0].Value = iCodTarima;
                pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionTarimaPiezaMod(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionTarimaPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionTarimaPieza
        #region ObtenerInventarioProceso
        public DataTable ObtenerInventarioProceso()
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerInventarioProceso(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerInventarioProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerInventarioProceso
        #region DeleteInventarioProceso
        public DataTable DeleteInventarioProceso(int idPieza)
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@IdPieza", SqlDbType.Int);
                pars[0].Value = idPieza;
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_DeleteInventarioProceso(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", DeleteInventarioProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion DeleteInventarioProceso
        #region ObtenerInsertarPiezaInventario
        public DataTable ObtenerInsertarPiezaInventario()
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerInsertarPiezaInventario(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerInventarioProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerInsertarPiezaInventario
        #region DeleteInsertarPiezaInventario
        public DataTable DeleteInsertarPiezaInventario(String codBarras)
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodBarras", SqlDbType.Int);
                pars[0].Value = codBarras;
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_DeleteInsertarPiezaInventario(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", DeleteInsertarPiezaInventario: " + ex.Message);
            }
            return dtRes;
        }
        #endregion DeleteInsertarPiezaInventario
        #region ObtenerReemplazoEtiquetas
        public DataTable ObtenerReemplazoEtiquetas()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerReemplazoEtiquetas(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerReemplazoEtiquetas: " + ex.Message);
            }
            return dtRes;
        }
        #endregion DeleteReemplazoEtiquetas
        #region DeleteReemplazoEtiquetas
        public DataTable DeleteReemplazoEtiquetas(int pieza, int proceso)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@IdPieza", SqlDbType.Int);
                pars[0].Value = pieza;
                pars[1] = new SqlCeParameter("@IdProceso", SqlDbType.Int);
                pars[1].Value = proceso;
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_DeleteReemplazoEtiquetas(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfigHandHeldLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion DeleteReemplazoEtiquetas

        //////////////Erwin/////////////
        // ConfigHandHeld
        #region query_ObtenerConfigHandHeldLocal
        public static string query_ObtenerUnConfigHandHeldLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ch.cod_config_handheld as CodConfigHandheld, ");
            queryString.Append("		ch.cod_usuario as CodUsuario, ");
            queryString.Append("		ch.cod_operador as CodOperador, ");
            queryString.Append("		ch.cod_supervisor as CodSupervisor, ");
            queryString.Append("		ch.fecha as Fecha, ");
            queryString.Append("		ch.cod_turno as CodTurno, ");
            queryString.Append("		ch.cod_planta as CodPlanta, ");
            queryString.Append("		ch.cod_proceso as CodProceso, ");
            queryString.Append("		ch.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		ch.fecha_registro as FechaRegistro ");
            queryString.Append("from	config_handheld ch ");
            queryString.Append("where		ch.cod_config_handheld between 1 and 50000 ");
            queryString.Append("		and	ch.cod_config_handheld = @CodConfigHandHeld ");
            queryString.Append("order by	ch.cod_config_handheld asc;");
            return queryString.ToString();
        }
        public static string query_ObtenerConfigHandHeldLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ch.cod_config_handheld as CodConfigHandheld, ");
            queryString.Append("		ch.cod_usuario as CodUsuario, ");
            queryString.Append("		ch.cod_operador as CodOperador, ");
            queryString.Append("		ch.cod_supervisor as CodSupervisor, ");
            queryString.Append("		ch.fecha as Fecha, ");
            queryString.Append("		ch.cod_turno as CodTurno, ");
            queryString.Append("		ch.cod_planta as CodPlanta, ");
            queryString.Append("		ch.cod_proceso as CodProceso, ");
            queryString.Append("		ch.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		ch.fecha_registro as FechaRegistro ");
            queryString.Append("from	config_handheld ch ");
            queryString.Append("where		ch.cod_config_handheld between 1 and 50000 ");
            queryString.Append("		and	(ch.cod_proceso = @CodProceso or @CodProceso = -1)");
            queryString.Append("order by	ch.cod_config_handheld asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerConfigHandHeldLocal
        #region query_ActualizarCodConfigHandheld
        public static string query_ActualizarCodConfigHandheld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	config_handheld ");
            queryString.Append("set		cod_config_handheld = @CodConfigHandheldSvr ");
            queryString.Append("where		cod_config_handheld = @CodConfigHandheldLocal;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodConfigHandheld
        #region query_ActualizarCodConfigHandheldPT
        public static string query_ActualizarCodConfigHandheldPT()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_transaccion ");
            queryString.Append("set		cod_config_handheld = @CodConfigHandheldSvr ");
            queryString.Append("where		cod_config_handheld = @CodConfigHandheldLocal ");
            queryString.Append(" and cod_pieza >-1 ");
            queryString.Append(" and cod_pieza_transaccion > -1;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodConfigHandheldPT

        #region ObtenerConfigHandHeldLocal
        private DataTable ObtenerUnConfigHandHeldLocal(long iCodConfigHandHeld)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                pars[0].Value = iCodConfigHandHeld;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerUnConfigHandHeldLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerUnConfigHandHeldLocal: " + ex.Message);
            }
            return dtRes;
        }
        private DataTable ObtenerConfigHandHeldLocal(int iCodProceso)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerConfigHandHeldLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfigHandHeldLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerConfigHandHeldLocal
        #region ActualizarCodConfigHandheld
        private int ActualizarCodConfigHandheld(long CodConfigHandheldLocal, long CodConfigHandheldSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodConfigHandheldLocal", SqlDbType.BigInt);
                pars[0].Value = CodConfigHandheldLocal;
                pars[1] = new SqlCeParameter("@CodConfigHandheldSvr", SqlDbType.BigInt);
                pars[1].Value = CodConfigHandheldSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodConfigHandheld(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodConfigHandheld: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodConfigHandheld
        #region ActualizarCodConfigHandheldPT
        private int ActualizarCodConfigHandheldPT(long CodConfigHandheldLocal, long CodConfigHandheldSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodConfigHandheldLocal", SqlDbType.BigInt);
                pars[0].Value = CodConfigHandheldLocal;
                pars[1] = new SqlCeParameter("@CodConfigHandheldSvr", SqlDbType.BigInt);
                pars[1].Value = CodConfigHandheldSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodConfigHandheldPT(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodConfigHandheldPT: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodConfigHandheldPT
        #region EnviarConfigHandHeld
        public long EnviarUnConfigHandHeld(long codConfigHandHeld)
        {
            c03_ConfiguracionInicial cCI = new c03_ConfiguracionInicial();
            long lCodConfigHandheldSvr = -1;
            long lCodConfigHandheldLocal = -1;
            int iCodUsuario = -1;
            int iCodOperador = -1;
            int iCodSupervisor = -1;
            DateTime dtFecha = DateTime.MinValue;
            int iCodTurno = -1;
            int iCodPlanta = -1;
            int iCodProceso = -1;
            int iCodConfigBanco = -1;
            DateTime dtFechaRegistro = DateTime.MinValue;
            DataTable dtConfigHandHeld = this.ObtenerUnConfigHandHeldLocal(codConfigHandHeld);
            foreach (DataRow dr in dtConfigHandHeld.Rows)
            {
                lCodConfigHandheldLocal = Convert.ToInt64(dr["CodConfigHandheld"]);
                iCodUsuario = Convert.ToInt32(dr["CodUsuario"]);
                iCodOperador = Convert.ToInt32(dr["CodOperador"]);
                iCodSupervisor = Convert.ToInt32(dr["CodSupervisor"]);
                dtFecha = Convert.ToDateTime(dr["Fecha"]);
                iCodTurno = Convert.ToInt32(dr["CodTurno"]);
                iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                if (Convert.IsDBNull(dr["CodConfigBanco"]))
                {
                    iCodConfigBanco = -1;
                }
                else
                {
                    iCodConfigBanco = Convert.ToInt32(dr["CodConfigBanco"]);
                }
                dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                lCodConfigHandheldSvr = cCI.InsertarConfigHandHeld(DA.eTipoConexion.Servicio, iCodUsuario, iCodOperador,
                                                                        iCodSupervisor, dtFecha, iCodTurno, iCodPlanta,
                                                                        iCodProceso, iCodConfigBanco, dtFechaRegistro);

                if (lCodConfigHandheldSvr != -1)
                {
                    this.ActualizarCodConfigHandheld(lCodConfigHandheldLocal, lCodConfigHandheldSvr);
                    this.ActualizarCodConfigHandheldPT(lCodConfigHandheldLocal, lCodConfigHandheldSvr);
                }
            }
            return lCodConfigHandheldSvr;
        }
        private void EnviarConfigHandHeld(int CodProceso)
        {
            c03_ConfiguracionInicial cCI = new c03_ConfiguracionInicial();

            long lCodConfigHandheldLocal = -1;
            int iCodUsuario = -1;
            int iCodOperador = -1;
            int iCodSupervisor = -1;
            DateTime dtFecha = DateTime.MinValue;
            int iCodTurno = -1;
            int iCodPlanta = -1;
            int iCodProceso = -1;
            int iCodConfigBanco = -1;
            DateTime dtFechaRegistro = DateTime.MinValue;
            DataTable dtConfigHandHeld = this.ObtenerConfigHandHeldLocal(CodProceso);
            foreach (DataRow dr in dtConfigHandHeld.Rows)
            {
                lCodConfigHandheldLocal = Convert.ToInt64(dr["CodConfigHandheld"]);
                iCodUsuario = Convert.ToInt32(dr["CodUsuario"]);
                iCodOperador = Convert.ToInt32(dr["CodOperador"]);
                iCodSupervisor = Convert.ToInt32(dr["CodSupervisor"]);
                dtFecha = Convert.ToDateTime(dr["Fecha"]);
                iCodTurno = Convert.ToInt32(dr["CodTurno"]);
                iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                if (Convert.IsDBNull(dr["CodConfigBanco"]))
                {
                    iCodConfigBanco = -1;
                }
                else
                {
                    iCodConfigBanco = Convert.ToInt32(dr["CodConfigBanco"]);
                }
                dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                long lCodConfigHandheldSvr = cCI.InsertarConfigHandHeld(DA.eTipoConexion.Servicio, iCodUsuario, iCodOperador,
                                                                        iCodSupervisor, dtFecha, iCodTurno, iCodPlanta,
                                                                        iCodProceso, iCodConfigBanco, dtFechaRegistro);

                if (lCodConfigHandheldSvr != -1)
                {
                    this.ActualizarCodConfigHandheld(lCodConfigHandheldLocal, lCodConfigHandheldSvr);
                    this.ActualizarCodConfigHandheldPT(lCodConfigHandheldLocal, lCodConfigHandheldSvr);
                }
            }
        }
        #endregion EnviarConfigHandHeld

        // Defectos
        #region query_ObtenerPiezaDefectoLocales
        public static string query_ObtenerPiezaDefectoLocales()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pd.cod_pieza as CodPieza, ");
            queryString.Append("		pd.cod_proceso as CodProceso, ");
            queryString.Append("		pd.cod_defecto as CodDefecto, ");
            queryString.Append("		pd.cod_zona_defecto as CodZonaDefecto, ");
            queryString.Append("		pd.cod_estado_defecto as CodEstadoDefecto, ");
            queryString.Append("		pd.cod_empleado as CodEmpleado, ");
            queryString.Append("		pd.fecha_ultimo_movimiento as FechaUltimoMovimiento, ");
            queryString.Append("		pd.fecha_registro as FechaRegistro, ");
            queryString.Append("		pd.nuevo as Nuevo, ");
            queryString.Append("		pd.modificado as Modificado, ");
            queryString.Append("		pd.eliminado as Eliminado ");
            queryString.Append("from	pieza_defecto pd ");
            queryString.Append("where	cod_zona_defecto > -1 ");
            queryString.Append(" and	cod_defecto > -1  ");
            queryString.Append(" and cod_proceso = @CodProceso ");
            queryString.Append(" and cod_pieza > -1 ");
            queryString.Append("		and (nuevo = 1 ");
            queryString.Append("		or	modificado = 1 ");
            queryString.Append("		or	eliminado = 1);");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaDefectoLocales
        #region query_ObtenerPiezaUltimoEstadoLocales
        public static string query_ObtenerPiezaUltimoEstadoLocales()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza as CodPieza, ");
            queryString.Append(" 		p.cod_ultimo_estado as CodUltimoEstado ");
            queryString.Append("from	pieza p ");
            queryString.Append("where		p.modificado_estado = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaUltimoEstadoLocales
        #region query_QuitarMarcaNuevoPiezaDefecto
        public static string query_QuitarMarcaNuevoPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		nuevo = 0 ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_QuitarMarcaNuevoPiezaDefecto
        #region query_QuitarMarcaModificadoPiezaDefecto
        public static string query_QuitarMarcaModificadoPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		modificado = 0 ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_QuitarMarcaModificadoPiezaDefecto
        #region query_QuitarMarcaEliminadoPiezaDefecto
        public static string query_QuitarMarcaEliminadoPiezaDefecto()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		eliminado = 0 ");
            queryString.Append("where		cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_defecto = @CodDefecto ");
            queryString.Append("		and	cod_zona_defecto = @CodZonaDefecto;");
            return queryString.ToString();
        }
        #endregion query_QuitarMarcaEliminadoPiezaDefecto
        #region query_QuitarMarcaModificadoEstadoPieza
        public static string query_QuitarMarcaModificadoEstadoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		modificado_estado = 0 ");
            queryString.Append("where		modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_QuitarMarcaModificadoEstadoPieza

        #region ObtenerPiezaDefectoLocales
        private DataTable ObtenerPiezaDefectoLocales(int iCodProceso)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerPiezaDefectoLocales(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaDefectoLocales: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaDefectoLocales
        #region ObtenerPiezaUltimoEstadoLocales
        private DataTable ObtenerPiezaUltimoEstadoLocales()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerPiezaUltimoEstadoLocales(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaUltimoEstadoLocales: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaUltimoEstadoLocales
        #region QuitarMarcaNuevoPiezaDefecto
        private int QuitarMarcaNuevoPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
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
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_QuitarMarcaNuevoPiezaDefecto(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", QuitarMarcaNuevoPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion QuitarMarcaNuevoPiezaDefecto
        #region QuitarMarcaModificadoPiezaDefecto
        private int QuitarMarcaModificadoPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
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
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_QuitarMarcaModificadoPiezaDefecto(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", QuitarMarcaModificadoPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion QuitarMarcaModificadoPiezaDefecto
        #region QuitarMarcaEliminadoPiezaDefecto
        private int QuitarMarcaEliminadoPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
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
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_QuitarMarcaEliminadoPiezaDefecto(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", QuitarMarcaEliminadoPiezaDefecto: " + ex.Message);
            }
            return iRes;
        }
        #endregion QuitarMarcaEliminadoPiezaDefecto
        #region QuitarMarcaModificadoEstadoPieza
        private int QuitarMarcaModificadoEstadoPieza(int iCodPieza)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_QuitarMarcaModificadoEstadoPieza(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", QuitarMarcaModificadoEstadoPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion QuitarMarcaModificadoEstadoPieza
        #region EnviarDefectos
        public bool EnviarDefectos(int CodProceso, DataTable dtPiezaDefecto, DataTable dtPiezaUltimoEstado, DataTable dtPiezaUltimoProceso, DataSet dsProcesoVaciado)
        {
            bool bEnvioExitoso = false;
            c04_Defectos cD = new c04_Defectos();
            try
            {
                // pieza_defecto
                //int iCodPieza = -1;
                //int iCodProceso = -1;
                //int iCodDefecto = -1;
                //int iCodZonaDefecto = -1;
                //int iCodEstadoDefecto = -1;
                //int iCodEmpleado = -1;
                DateTime dtFechaUltimoMovimiento = DateTime.MinValue;
                DateTime dtFechaRegistro = DateTime.MinValue;
                //bool bNuevo = false;
                //bool bModificado = false;
                //bool bEliminado = false;
                //int iRes = -1;
                if (dsProcesoVaciado == null) return false;
                if (dsProcesoVaciado.Tables.Count != 8) return false;
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZADEFECTO-------------------------------------------------
                //DataTable dtPiezaDefecto = this.ObtenerPiezaDefectoLocales(CodProceso);
                //foreach (DataRow dr in dtPiezaDefecto.Rows)
                //{
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                //    iCodDefecto = Convert.ToInt32(dr["CodDefecto"]);
                //    iCodZonaDefecto = Convert.ToInt32(dr["CodZonaDefecto"]);
                //    iCodEstadoDefecto = Convert.ToInt32(dr["CodEstadoDefecto"]);
                //    iCodEmpleado = Convert.ToInt32(dr["CodEmpleado"]);
                //    dtFechaUltimoMovimiento = Convert.ToDateTime(dr["FechaUltimoMovimiento"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                //    bNuevo = Convert.ToBoolean(dr["Nuevo"]);
                //    bModificado = Convert.ToBoolean(dr["Modificado"]);
                //    bEliminado = Convert.ToBoolean(dr["Eliminado"]);

                //    if (bNuevo)
                //    {
                //        iRes = cD.InsertarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto,
                //                                        iCodZonaDefecto, iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento,
                //                                        dtFechaRegistro);

                //        if (iRes != -1)
                //        {
                //            this.QuitarMarcaNuevoPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                //        }
                //    }
                //    else if (bEliminado)
                //    {
                //        iRes = cD.EliminarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);

                //        if (iRes != -1)
                //        {
                //            cD.EliminarPiezaDefecto(DA.eTipoConexion.Local, iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                //        }
                //    }
                //    else if (bModificado)
                //    {
                //        iRes = cD.ActualizarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto,
                //                                                iCodZonaDefecto, iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento);

                //        if (iRes != -1)
                //        {
                //            this.QuitarMarcaModificadoPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                //        }
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZADEFECTO-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //PiezaDefecto
                foreach (DataRow row in dsProcesoVaciado.Tables[4].Rows)
                {
                    DataRow[] rows = null;
                    if (Convert.ToBoolean(row["Nuevo"]))
                    {
                        this.QuitarMarcaNuevoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        rows = dtPiezaDefecto.Select("CodPieza = " + row["CodPieza"].ToString() + " AND CodProceso = " + row["CodProceso"].ToString() + " AND CodDefecto = " + row["CodDefecto"].ToString() + " AND CodZonaDefecto = " + row["CodZonaDefecto"].ToString() + " AND Nuevo = " + row["Nuevo"].ToString());
                    }
                    else if (Convert.ToBoolean(row["Modificado"]))
                    {
                        this.QuitarMarcaModificadoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        rows = dtPiezaDefecto.Select("CodPieza = " + row["CodPieza"].ToString() + " AND CodProceso = " + row["CodProceso"].ToString() + " AND CodDefecto = " + row["CodDefecto"].ToString() + " AND CodZonaDefecto = " + row["CodZonaDefecto"].ToString() + " AND Modificado = " + row["Modificado"].ToString());
                    }
                    else if (Convert.ToBoolean(row["Eliminado"]))
                    {
                        cD.EliminarPiezaDefecto(DA.eTipoConexion.Local, Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        rows = dtPiezaDefecto.Select("CodPieza = " + row["CodPieza"].ToString() + " AND CodProceso = " + row["CodProceso"].ToString() + " AND CodDefecto = " + row["CodDefecto"].ToString() + " AND CodZonaDefecto = " + row["CodZonaDefecto"].ToString() + " AND Eliminado = " + row["Eliminado"].ToString());
                    }
                    //Depurar tabla en memoria
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtPiezaDefecto.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtPiezaDefecto.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZAULTIMOESTADO-------------------------------------------------
                //// pieza - Ultimo Estado
                //int iCodUltimoEstado = -1;
                //DataTable dtPiezaUltimoEstado = this.ObtenerPiezaUltimoEstadoLocales();
                //foreach (DataRow dr in dtPiezaUltimoEstado.Rows)
                //{
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                //    iRes = cD.ActualizarPiezaUltimoEstado(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoEstado);

                //    if (iRes != -1)
                //    {
                //        this.QuitarMarcaModificadoEstadoPieza(iCodPieza);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZAULTIMOESTADO-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //PiezaUltimoEstado
                foreach (DataRow row in dsProcesoVaciado.Tables[5].Rows)
                {
                    this.QuitarMarcaModificadoEstadoPieza(Convert.ToInt32(row["CodPieza"].ToString()));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtPiezaUltimoEstado.Select("CodPieza = " + row["CodPieza"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtPiezaUltimoEstado.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtPiezaUltimoEstado.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZAULTIMOPROCESO-------------------------------------------------
                //// Pieza - Ultimo Proceso (Se agrego porque cuando la pieza se envia a Desperdicio se actualiza el ultimo proceso).
                //c00_Common cC = new c00_Common();
                //int iCodUltimoProceso = -1;
                //DataTable dtPieza = this.ObtenerActualizacionPieza();
                //foreach (DataRow dr in dtPieza.Rows)
                //{
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                //    iRes = cC.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoProceso);

                //    if (iRes != -1)
                //    {
                //        this.EstablecerActualizacionPieza(iCodPieza);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZAULTIMOPROCESO-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //PiezaUltimoProceso
                foreach (DataRow row in dsProcesoVaciado.Tables[6].Rows)
                {
                    this.EstablecerActualizacionPieza(Convert.ToInt32(row["CodPieza"].ToString()));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtPiezaUltimoProceso.Select("CodPieza = " + row["CodPieza"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtPiezaUltimoProceso.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtPiezaUltimoProceso.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                bEnvioExitoso = true;
            }
            catch (Exception)
            {
                bEnvioExitoso = false;
            }
            return bEnvioExitoso;
        }
        public bool EnviarDefectos(int CodProceso)
        {
            bool bEnvioExitoso = false;
            c04_Defectos cD = new c04_Defectos();

            try
            {
                // pieza_defecto
                int iCodPieza = -1;
                int iCodProceso = -1;
                int iCodDefecto = -1;
                int iCodZonaDefecto = -1;
                int iCodEstadoDefecto = -1;
                int iCodEmpleado = -1;
                DateTime dtFechaUltimoMovimiento = DateTime.MinValue;
                DateTime dtFechaRegistro = DateTime.MinValue;
                bool bNuevo = false;
                bool bModificado = false;
                bool bEliminado = false;
                int iRes = -1;
                DataTable dtPiezaDefecto = this.ObtenerPiezaDefectoLocales(CodProceso);
                foreach (DataRow dr in dtPiezaDefecto.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                    iCodDefecto = Convert.ToInt32(dr["CodDefecto"]);
                    iCodZonaDefecto = Convert.ToInt32(dr["CodZonaDefecto"]);
                    iCodEstadoDefecto = Convert.ToInt32(dr["CodEstadoDefecto"]);
                    iCodEmpleado = Convert.ToInt32(dr["CodEmpleado"]);
                    dtFechaUltimoMovimiento = Convert.ToDateTime(dr["FechaUltimoMovimiento"]);
                    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                    bNuevo = Convert.ToBoolean(dr["Nuevo"]);
                    bModificado = Convert.ToBoolean(dr["Modificado"]);
                    bEliminado = Convert.ToBoolean(dr["Eliminado"]);

                    if (bNuevo)
                    {
                        iRes = cD.InsertarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto,
                                                        iCodZonaDefecto, iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento,
                                                        dtFechaRegistro);

                        if (iRes != -1)
                        {
                            this.QuitarMarcaNuevoPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                        }
                    }
                    else if (bEliminado)
                    {
                        iRes = cD.EliminarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);

                        if (iRes != -1)
                        {
                            cD.EliminarPiezaDefecto(DA.eTipoConexion.Local, iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                        }
                    }
                    else if (bModificado)
                    {
                        iRes = cD.ActualizarPiezaDefecto(DA.eTipoConexion.Servicio, iCodPieza, iCodProceso, iCodDefecto,
                                                                iCodZonaDefecto, iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento);

                        if (iRes != -1)
                        {
                            this.QuitarMarcaModificadoPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
                        }
                    }
                }

                // pieza - Ultimo Estado
                int iCodUltimoEstado = -1;
                DataTable dtPiezaUltimoEstado = this.ObtenerPiezaUltimoEstadoLocales();
                foreach (DataRow dr in dtPiezaUltimoEstado.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                    iRes = cD.ActualizarPiezaUltimoEstado(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoEstado);

                    if (iRes != -1)
                    {
                        this.QuitarMarcaModificadoEstadoPieza(iCodPieza);
                    }
                }

                // Pieza - Ultimo Proceso (Se agrego porque cuando la pieza se envia a Desperdicio se actualiza el ultimo proceso).
                c00_Common cC = new c00_Common();
                int iCodUltimoProceso = -1;

                DataTable dtPieza = this.ObtenerActualizacionPieza();
                foreach (DataRow dr in dtPieza.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);
                    iRes = cC.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoProceso);
                    if (iRes != -1)
                    {
                        this.EstablecerActualizacionPieza(iCodPieza);
                    }
                }
                bEnvioExitoso = true;
            }
            catch (Exception)
            {
                bEnvioExitoso = false;
            }
            return bEnvioExitoso;
        }
        #endregion EnviarDefectos

        // Procesos
        #region query_ObtenerPiezaLocal
        public static string query_ObtenerPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_planta as CodPlanta, ");
            queryString.Append("		p.cod_pieza as CodPieza, ");
            queryString.Append("		p.cod_barras as CodBarras, ");
            queryString.Append("		p.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		p.cod_consecutivo as CodConsecutivo, ");
            queryString.Append("		p.posicion as Posicion, ");
            queryString.Append("		p.cod_articulo as CodModelo, ");
            queryString.Append("		p.cod_ultimo_proceso as CodUltimoProceso, ");
            queryString.Append("		p.cod_ultimo_estado as CodUltimoEstado, ");
            queryString.Append("		p.CodMolde, ");
            queryString.Append("		p.IdBase, ");
            queryString.Append("		p.fecha_registro as FechaRegistro ");
            queryString.Append("from	pieza p ");
            queryString.Append("where		p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza between 1 and 50000 ");
            queryString.Append("order by	p.cod_pieza asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaLocal
        #region query_ActualizarCodPieza
        public static string query_ActualizarCodPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_pieza = @CodPiezaSvr ");
            queryString.Append("where		modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPiezaLocal;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPieza
        #region query_ActualizarCodPiezaT
        public static string query_ActualizarCodPiezaT()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_transaccion ");
            queryString.Append("set		cod_pieza = @CodPiezaSvr ");
            queryString.Append("where		cod_pieza = @CodPiezaLocal ");
            queryString.Append(" and 		cod_config_handheld > -1 ");
            queryString.Append(" and cod_pieza_transaccion > -1;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPiezaT
        #region query_ActualizarCodPiezaCP
        public static string query_ActualizarCodPiezaCP()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	carro_pieza ");
            queryString.Append("set		cod_pieza = @CodPiezaSvr ");
            queryString.Append("where		cod_pieza = @CodPiezaLocal; ");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPiezaCP
        #region query_ActualizarPiezaReemplazo
        public static string query_ActualizarPiezaReemplazo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_reemplazo ");
            queryString.Append("set		cod_pieza = @CodPiezaSvr ");
            queryString.Append("where		cod_pieza = @CodPiezaLocal; ");
            return queryString.ToString();
        }
        #endregion query_ActualizarPiezaReemplazo
        #region query_ActualizarCodPiezaDefectos
        public static string query_ActualizarCodPiezaDefectos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_defecto ");
            queryString.Append("set		cod_pieza = @CodPiezaSvr ");
            queryString.Append("where		cod_pieza = @CodPiezaLocal ");
            queryString.Append(" and cod_proceso > -1  ");
            queryString.Append(" and	cod_defecto > -1  ");
            queryString.Append(" and	cod_zona_defecto > -1;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPiezaDefectos
        #region query_ObtenerPiezaTransaccionLocal
        public static string query_ObtenerPiezaTransaccionLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pt.cod_pieza_transaccion as CodPiezaTransaccion, ");
            queryString.Append("		pt.cod_config_handheld as CodConfigHandheld, ");
            queryString.Append("		pt.cod_pieza as CodPieza, ");
            queryString.Append("		pt.fecha_registro as FechaRegistro ");
            queryString.Append("from	pieza_transaccion pt ");
            queryString.Append("where		pt.cod_pieza_transaccion between 1 and 50000 ");
            queryString.Append("            and	pt.cod_config_handheld > 50000 ");
            queryString.Append("order by	pt.cod_pieza_transaccion asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaTransaccionLocal
        #region query_ActualizarCodPiezaTransaccion
        public static string query_ActualizarCodPiezaTransaccion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_transaccion ");
            queryString.Append("set		cod_pieza_transaccion = @CodPiezaTransaccionSvr ");
            queryString.Append("where	cod_config_handheld > -1 ");
            queryString.Append(" and cod_pieza > -1 ");
            queryString.Append(" and cod_pieza_transaccion = @CodPiezaTransaccionLocal;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPiezaTransaccion
        #region query_ObtenerActualizacionVaciadasAcumuladas
        public static string query_ObtenerActualizacionVaciadasAcumuladas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cb.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		cb.vaciadas_acumuladas as VaciadasAcumuladas ");
            queryString.Append("from	config_banco cb ");
            queryString.Append("where		cb.actualizacion = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerActualizacionVaciadasAcumuladas
        #region query_EstablecerActualizacionVaciadasAcumuladas
        public static string query_EstablecerActualizacionVaciadasAcumuladas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	config_banco ");
            queryString.Append("set		actualizacion = 0 ");
            queryString.Append("where		cod_config_banco = @CodConfigBanco;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionVaciadasAcumuladas
        #region query_ObtenerCarroPiezaLocal
        public static string query_ObtenerCarroPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cp.cod_planta as CodPlanta, ");
            queryString.Append("		cp.cod_proceso as CodProceso, ");
            queryString.Append("		cp.cod_carro as CodCarro, ");
            queryString.Append("		cp.cod_pieza as CodPieza, ");
            queryString.Append("		cp.fecha_registro as FechaRegistro, ");
            queryString.Append("		cp.tipoTransporte Transporte ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.actualizacion = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCarroPiezaLocal
        #region query_EstablecerActualizacionCarroPieza
        public static string query_EstablecerActualizacionCarroPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	carro_pieza ");
            queryString.Append("set		actualizacion = 0 ");
            queryString.Append("where		cod_planta = @CodPlanta ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_carro = @CodCarro ");
            queryString.Append("		and	cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionCarroPieza
        #region query_ActualizarCodPiezaTransaccionSecador
        public static string query_ActualizarCodPiezaTransaccionSecador()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_transaccion_secador ");
            queryString.Append("set		cod_pieza_transaccion = @CodPiezaTransaccionSvr ");
            queryString.Append("where		cod_pieza_transaccion = @CodPiezaTransaccionLocal;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCodPiezaTransaccionSecador
        #region query_ObtenerActualizacionPieza
        public static string query_ObtenerActualizacionPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza as CodPieza, ");
            queryString.Append("		p.cod_articulo as CodModelo, ");
            queryString.Append("		p.cod_color as CodColor, ");
            queryString.Append("		p.cod_calidad as CodCalidad, ");
            queryString.Append("		p.cod_ultimo_proceso as CodUltimoProceso, ");
            queryString.Append("		p.Auditada as Auditada, ");
            queryString.Append("		p.cod_ultimo_estado as CodUltimoEstado ");
            queryString.Append("from	pieza p ");
            queryString.Append("where	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and 	p.actualizacion = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerActualizacionPieza
        #region query_EstablecerActualizacionPieza
        public static string query_EstablecerActualizacionPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		actualizacion = 0 ");
            queryString.Append("where		modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionPieza
        #region query_ObtenerPiezaTransaccionSecadorLocal
        public static string query_ObtenerPiezaTransaccionSecadorLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pts.cod_pieza_transaccion as CodPiezaTransaccion, ");
            queryString.Append("		pts.hora_inicio as HoraInicio, ");
            queryString.Append("		pts.horas_secado as HorasSecado, ");
            queryString.Append("		pts.fecha_registro as FechaRegistro ");
            queryString.Append("from	pieza_transaccion_secador pts ");
            queryString.Append("where		pts.actualizacion = 1; ");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaTransaccionSecadorLocal
        #region query_EstablecerActualizacionPiezaTransaccionSecador
        public static string query_EstablecerActualizacionPiezaTransaccionSecador()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza_transaccion_secador ");
            queryString.Append("set		actualizacion = 0 ");
            queryString.Append("where		cod_pieza_transaccion = @CodPiezaTransaccion;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionPiezaTransaccionSecador
        #region query_ObtenerCarroPiezaEliminados
        public static string query_ObtenerCarroPiezaEliminados()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	distinct ");
            queryString.Append("		cp.cod_planta as CodPlanta, ");
            queryString.Append("		cp.cod_proceso as CodProceso, ");
            queryString.Append("		cp.cod_carro as CodCarro ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.eliminado = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCarroPiezaEliminados
        #region query_ObtenerCarroPiezaQuemadoLocal
        public static string query_ObtenerCarroPiezaQuemadoLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cpq.cod_planta as CodPlanta, ");
            queryString.Append("		cpq.cod_pieza as CodPieza, ");
            queryString.Append("		cpq.cod_carro as CodCarro, ");
            queryString.Append("		cpq.cod_zona as CodZona ");
            queryString.Append("from	carro_pieza_quemado cpq ");
            queryString.Append("where		cpq.actualizacion = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCarroPiezaQuemadoLocal
        #region query_EstablecerActualizacionCarroPiezaQuemado
        public static string query_EstablecerActualizacionCarroPiezaQuemado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	carro_pieza_quemado ");
            queryString.Append("set		actualizacion = 0 ");
            queryString.Append("where		cod_planta = @CodPlanta ");
            queryString.Append("		and	cod_pieza = @CodPieza ");
            queryString.Append("		and	cod_carro = @CodCarro ");
            queryString.Append("		and	cod_zona = @CodZona;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionCarroPiezaQuemado
        #region query_ObtenerTarimaPiezaLocal
        public static string query_ObtenerTarimaPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	tp.CodTarima as CodTarima, ");
            queryString.Append("		tp.CodPieza as CodPieza, ");
            queryString.Append("		tp.Paletizado as Paletizado, ");
            queryString.Append("		tp.Rechazada as Rechazada, ");
            queryString.Append("		tp.FechaRegistro as FechaRegistro ");
            queryString.Append("from	TarimaPieza tp ");
            queryString.Append("where		tp.nuevo = 1;");
            return queryString.ToString();
        }
        #endregion query_ObtenerTarimaPiezaLocal
        #region query_EstablecerActualizacionTarimaPieza
        public static string query_EstablecerActualizacionTarimaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	TarimaPieza ");
            queryString.Append("set	nuevo = 0, Paletizado = @Paletizada, Rechazada = @Rechazada ");
            queryString.Append("where		CodTarima = @CodTarima ");
            queryString.Append("		and	CodPieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_EstablecerActualizacionTarimaPieza
        #region query_ObtenerConfigVaciado
        public static string query_ObtenerConfigVaciado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	IdConfigVaciado, Planta, HoraEntrada, TiempoEstimado, ConfigBanco ");
            queryString.Append("from ConfigVaciado ");
            queryString.Append("where	planta = @Planta ");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerConfigBancoCasetaTanque
        public static string query_ObtenerConfigBancoCasetaTanque()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT cb.cod_config_banco ");
            queryString.Append("FROM CasetaTanque ct JOIN config_banco cb ON ct.CodMaquina = cb.cod_maquina ");
            queryString.Append("WHERE ct.CodCaseta = @CodMaquina ");
            queryString.Append("AND ct.CodTanque = @CodTanque AND ct.CodProceso = @CodProceso AND ct.CodPlanta = @CodPlanta;");
            return queryString.ToString();
        }
        #endregion
        
        #region ObtenerPiezaLocal
        public DataTable ObtenerPiezaLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerPiezaLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaLocal
        #region ActualizarCodPieza
        private int ActualizarCodPieza(int CodPiezaLocal, int CodPiezaSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaLocal", SqlDbType.Int);
                pars[0].Value = CodPiezaLocal;
                pars[1] = new SqlCeParameter("@CodPiezaSvr", SqlDbType.Int);
                pars[1].Value = @CodPiezaSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPieza(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPieza
        #region ActualizarCodPiezaT
        private int ActualizarCodPiezaT(int CodPiezaLocal, int CodPiezaSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaLocal", SqlDbType.Int);
                pars[0].Value = CodPiezaLocal;
                pars[1] = new SqlCeParameter("@CodPiezaSvr", SqlDbType.Int);
                pars[1].Value = CodPiezaSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPiezaT(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaT: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPiezaT
        #region ActualizarCodPiezaCP
        private int ActualizarCodPiezaCP(int CodPiezaLocal, int CodPiezaSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaLocal", SqlDbType.Int);
                pars[0].Value = CodPiezaLocal;
                pars[1] = new SqlCeParameter("@CodPiezaSvr", SqlDbType.Int);
                pars[1].Value = CodPiezaSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPiezaCP(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaCP: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPiezaCP
        #region ActualizarPiezaReemplazo
        private int ActualizarPiezaReemplazo(int CodPiezaLocal, int CodPiezaSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaLocal", SqlDbType.Int);
                pars[0].Value = CodPiezaLocal;
                pars[1] = new SqlCeParameter("@CodPiezaSvr", SqlDbType.Int);
                pars[1].Value = CodPiezaSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarPiezaReemplazo(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaCP: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarPiezaReemplazo
        #region ActualizarCodPiezaDefectos
        private int ActualizarCodPiezaDefectos(int CodPiezaLocal, int CodPiezaSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaLocal", SqlDbType.Int);
                pars[0].Value = CodPiezaLocal;
                pars[1] = new SqlCeParameter("@CodPiezaSvr", SqlDbType.Int);
                pars[1].Value = CodPiezaSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPiezaDefectos(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaDefectos: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPiezaDefectos
        #region ObtenerPiezaTransaccionLocal
        public DataTable ObtenerPiezaTransaccionLocal()
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerPiezaTransaccionLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaTransaccionLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaTransaccionLocal
        #region ActualizarCodPiezaTransaccion
        private int ActualizarCodPiezaTransaccion(long CodPiezaTransaccionLocal, long CodPiezaTransaccionSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaTransaccionLocal", SqlDbType.BigInt);
                pars[0].Value = CodPiezaTransaccionLocal;
                pars[1] = new SqlCeParameter("@CodPiezaTransaccionSvr", SqlDbType.BigInt);
                pars[1].Value = CodPiezaTransaccionSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPiezaTransaccion(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaTransaccion: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPiezaTransaccion
        #region ObtenerActualizacionVaciadasAcumuladas
        public DataTable ObtenerActualizacionVaciadasAcumuladas()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerActualizacionVaciadasAcumuladas(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerActualizacionVaciadasAcumuladas: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerActualizacionVaciadasAcumuladas
        #region EstablecerActualizacionVaciadasAcumuladas
        private int EstablecerActualizacionVaciadasAcumuladas(int iCodConfigBanco)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                pars[0].Value = iCodConfigBanco;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionVaciadasAcumuladas(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionVaciadasAcumuladas: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionVaciadasAcumuladas
        #region ObtenerCarroPiezaLocal
        public DataTable ObtenerCarroPiezaLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerCarroPiezaLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCarroPiezaLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCarroPiezaLocal
        #region EstablecerActualizacionCarroPieza
        private int EstablecerActualizacionCarroPieza(int CodPlanta, int CodProceso, int CodCarro, int CodPieza)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = CodProceso;
                pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = CodCarro;
                pars[3] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[3].Value = CodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionCarroPieza(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionCarroPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionCarroPieza
        #region ActualizarCodPiezaTransaccionSecador
        private int ActualizarCodPiezaTransaccionSecador(long CodPiezaTransaccionLocal, long CodPiezaTransaccionSvr)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPiezaTransaccionLocal", SqlDbType.BigInt);
                pars[0].Value = CodPiezaTransaccionLocal;
                pars[1] = new SqlCeParameter("@CodPiezaTransaccionSvr", SqlDbType.BigInt);
                pars[1].Value = CodPiezaTransaccionSvr;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_ActualizarCodPiezaTransaccionSecador(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCodPiezaTransaccionSecador: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarCodPiezaTransaccionSecador
        #region ObtenerActualizacionPieza
        public DataTable ObtenerActualizacionPieza()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerActualizacionPieza(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerActualizacionPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerActualizacionPieza
        #region EstablecerActualizacionPieza
        private int EstablecerActualizacionPieza(int CodPieza)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = CodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionPieza(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionPieza
        #region ObtenerPiezaTransaccionSecadorLocal
        public DataTable ObtenerPiezaTransaccionSecadorLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerPiezaTransaccionSecadorLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaTransaccionSecadorLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaTransaccionSecadorLocal
        #region EstablecerActualizacionPiezaTransaccionSecador
        private int EstablecerActualizacionPiezaTransaccionSecador(long CodPiezaTransaccion)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                pars[0].Value = CodPiezaTransaccion;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionPiezaTransaccionSecador(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionPiezaTransaccionSecador: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionPiezaTransaccionSecador
        private int ObtenerSupervisorPorDefecto(int empleado)
        {
            int iCodSupervisor = 2;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            DataTable dtSupervisor = proxy.ObtenerSupervisorPorDefecto(this.lu.CodUsuario, true);
            if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
            {
                try
                {
                    iCodSupervisor = Convert.ToInt32(dtSupervisor.Rows[0]["CodEmpleado"]);
                }
                catch (Exception e) { }
            }

            return iCodSupervisor;
        }
        #region ObtenerCarroPiezaEliminados
        public DataTable ObtenerCarroPiezaEliminados()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerCarroPiezaEliminados(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCarroPiezaEliminados: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCarroPiezaEliminados
        #region ObtenerCarroPiezaQuemadoLocal
        public DataTable ObtenerCarroPiezaQuemadoLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerCarroPiezaQuemadoLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCarroPiezaQuemadoLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCarroPiezaQuemadoLocal
        #region EstablecerActualizacionCarroPiezaQuemado
        private int EstablecerActualizacionCarroPiezaQuemado(int CodPlanta, int CodPieza, int CodCarro, string CodZona)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = CodPieza;
                pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = CodCarro;
                pars[3] = new SqlCeParameter("@CodZona", SqlDbType.NVarChar, 5);
                pars[3].Value = CodZona;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionCarroPiezaQuemado(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionCarroPiezaQuemado: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionCarroPiezaQuemado
        #region ObtenerTarimaPiezaLocal
        public DataTable ObtenerTarimaPiezaLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerTarimaPiezaLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarimaPiezaLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerTarimaPiezaLocal
        #region EstablecerActualizacionTarimaPieza
        public int EstablecerActualizacionTarimaPieza(int iCodTarima, int iCodPieza, int iPaletizado, int iRechazada)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                pars[0].Value = iCodTarima;
                pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;
                pars[2] = new SqlCeParameter("@Paletizada", SqlDbType.Bit);
                pars[2].Value = iPaletizado;
                pars[3] = new SqlCeParameter("@Rechazada", SqlDbType.Bit);
                pars[3].Value = iRechazada;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Transacciones.query_EstablecerActualizacionTarimaPieza(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerActualizacionTarimaPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerActualizacionTarimaPieza
        #region ObtenerConfigVaciado
        public DataTable ObtenerConfigVaciado(int iPlanta)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@Planta", SqlDbType.Int);
                pars[0].Value = iPlanta;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerConfigVaciado(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfigVaciado: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region ObtenerConfigBancoCasetaTanque
        public int ObtenerConfigBancoCasetaTanque(int iCodMaquina, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            
            int iCodConfigBanco = -1;
            bool bCodConfigBanco = false;
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerConfigBancoCasetaTanque(iCodMaquina, true, iCodTanque, true, iCodProceso, true, iCodPlanta,true, out iCodConfigBanco, out bCodConfigBanco);
                }
                else {
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    int i = 0;
                    pars[i] = new SqlCeParameter("@CodMaquina", SqlDbType.Int);
                    pars[i++].Value = iCodMaquina;
                    pars[i] = new SqlCeParameter("@CodTanque", SqlDbType.Int);
                    pars[i++].Value = iCodTanque;
                    pars[i] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[i++].Value = iCodProceso;
                    pars[i] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[i++].Value = iCodPlanta;
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Transacciones.query_ObtenerConfigBancoCasetaTanque(), pars);
                    if (dtRes != null && dtRes.Rows.Count > 0) { 
                        iCodConfigBanco = Convert.ToInt32(dtRes.Rows[0]["CodConfigBanco"]);
                    }
                }
                if (iCodConfigBanco < 1)
                    throw new Exception("No hay configuracion disponible para esta Caseta Tanque. Revise configuracion.");
            }
            catch (Exception e) {
               throw new Exception(this.sClassName + ", ObtenerConfigBancoCasetaTanque: "+ e.Message);
            }
            return iCodConfigBanco;
        }
        #endregion


        #region EnviarLog
        public void EnviarLog()
        {
            try
            {
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                DataTable dtLog = oDA0.ObtenerLogLocal();
                DataSet dsLog = new DataSet();
                dsLog.Tables.Add(dtLog);
                DataSet dsRes = proxy.LogIns(dsLog);
                if(dsRes != null && dsRes.Tables.Count > 0)
                    foreach (DataRow row in dsRes.Tables[0].Rows) { 
                        oDA0.LogLocalDel(Convert.ToInt64(row["Codigo"]));
                    }
            }
            catch(Exception e) { }
        }
        #endregion

        #region EnviarDatosVaciado
        public bool EnviarDatosVaciado()
        {
            EnviarLog();
            bool bCompletado = false;
            //Validar si el armado de carro fue por canastilla
            DataTable dtCarroPieza = this.ObtenerCarroPiezaLocal();
            Boolean chhTransporte = false;
            long lCodPiezaTransaccion;
            int procesoSecado = oDA0.ObtenerCodProcesoSecado();
            DateTime dtHoraEntrada = DateTime.Now;
            Double dTiempoSecado = 0D;
            int iConfigBanco = -1;
            foreach (DataRow dr in dtCarroPieza.Rows)
            {
                int iTranspote = Convert.ToInt32(dr["Transporte"]);

                if (iTranspote == 3)
                {
                    int iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                    int iCodCarro = Convert.ToInt32(dr["CodCarro"]);
                    int iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    DateTime dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                    if (!chhTransporte)
                    {
                        DataTable dtConfigVaciado = ObtenerConfigVaciado(iCodPlanta);
                        if (dtConfigVaciado != null && dtConfigVaciado.Rows.Count > 0)
                        {
                            this.lu.CodSupervisor = this.ObtenerSupervisorPorDefecto(this.lu.CodUsuario);
                            dtHoraEntrada = Convert.ToDateTime(dtConfigVaciado.Rows[0]["HoraEntrada"]);
                            dTiempoSecado = Convert.ToDouble(dtConfigVaciado.Rows[0]["TiempoEstimado"]);
                            iConfigBanco = Convert.ToInt32(dtConfigVaciado.Rows[0]["ConfigBanco"]);
                            this.lu.CodConfigHandHeld = new c03_ConfiguracionInicial().InsertarConfigHandHeld(DA.eTipoConexion.Local,
                                                                                this.lu.CodUsuario,
                                                                                this.lu.CodEmpleado,
                                                                                this.lu.CodSupervisor,
                                                                                 this.lu.Fecha,
                                                                                this.lu.CodTurno,
                                                                                iCodPlanta,
                                                                                procesoSecado,
                                                                                iConfigBanco,
                                                                                null);
                            chhTransporte = true;
                        }
                    }
                    if (chhTransporte)
                    {
                        lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                    this.lu.CodConfigHandHeld,
                                                                                    iCodPieza,
                                                                                this.lu.Fecha);

                        this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, procesoSecado);

                        new c06_EntradaCarroSecador().InsertarPiezaTransaccionSecador(DA.eTipoConexion.Local, lCodPiezaTransaccion, dtHoraEntrada, dTiempoSecado);

                        this.oDA0.EliminarCarroTemp(iCodPlanta, oDA0.ObtenerProcesoAnterior(procesoSecado), iCodCarro);
                    }
                }
            }
            //////////////////////////////////////////////////


            try
            {
                this.EnviarConfigHandHeld(-1);

                // Pieza
                c00_Common cC = new c00_Common();
                int iCodPlanta = -1;
                int iCodPiezaLocal = -1;
                string sCodBarras = string.Empty;
                int iCodConfigBanco = -1;
                int iCodConsecutivo = -1;
                int iPosicion = -1;
                int iCodModelo = -1;
                int iCodUltimoProceso = -1;
                int iCodUltimoEstado = -1;
                int iCodMolde = -1;
                int iCodBase = -1;
                DateTime dtFechaRegistro = DateTime.MinValue;
                int iCodPiezaSvr = -1;
                /*******************************************************************************************************/
                /////////////////////////////MODIFICACION PARA ENVIO DE CARGA MASIVA/////////////////////////////////////
                DataTable dtPieza = null, dtPiezaTransaccion = null, dtVaciadasAcumuladas = null, dtPiezaDefecto = null, dtPiezaUltimoEstado = null, dtPiezaUltimoProceso = null,
                            dtPruebaProceso = null;
                dtPieza = this.ObtenerPiezaLocal();
                dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                dtVaciadasAcumuladas = this.ObtenerActualizacionVaciadasAcumuladas();
                dtCarroPieza = this.ObtenerCarroPiezaLocal();
                dtPiezaDefecto = this.ObtenerPiezaDefectoLocales(this.oDA0.ObtenerCodProcesoVaciado());
                dtPiezaUltimoEstado = this.ObtenerPiezaUltimoEstadoLocales();
                dtPiezaUltimoProceso = this.ObtenerActualizacionPieza();
                dtPruebaProceso = this.oDA0.PruebaProcesoSel();
                DataSet dsProcesoVaciado = ProcesarBatchVaciadoPieza(dtPieza, dtPiezaTransaccion, dtVaciadasAcumuladas, dtCarroPieza, dtPiezaDefecto, dtPiezaUltimoEstado, dtPiezaUltimoProceso, dtPruebaProceso);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                /*******************************************************************************************************/
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZA-------------------------------------------------
                //dtPieza = this.ObtenerPiezaLocal();
                //foreach (DataRow dr in dtPieza.Rows)
                //{
                //    iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                //    iCodPiezaLocal = Convert.ToInt32(dr["CodPieza"]);
                //    sCodBarras = Convert.ToString(dr["CodBarras"]);
                //    iCodConfigBanco = Convert.ToInt32(dr["CodConfigBanco"]);
                //    iCodConsecutivo = Convert.ToInt32(dr["CodConsecutivo"]);
                //    iPosicion = Convert.ToInt32(dr["Posicion"]);
                //    iCodModelo = Convert.ToInt32(dr["CodModelo"]);
                //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                //    iCodMolde = Convert.ToInt32(dr["CodMolde"]);
                //    iCodBase = Convert.ToInt32(dr["IdBase"]);

                //    iCodPiezaSvr = cC.InsertarPieza(DA.eTipoConexion.Servicio, iCodPlanta, sCodBarras, iCodConfigBanco, iCodConsecutivo, iPosicion,
                //                        iCodModelo, iCodUltimoProceso, iCodUltimoEstado, dtFechaRegistro, iCodMolde, iCodBase);

                //    if (iCodPiezaSvr != -1)
                //    {
                //        this.ActualizarCodPieza(iCodPiezaLocal, iCodPiezaSvr);
                //        this.ActualizarCodPiezaT(iCodPiezaLocal, iCodPiezaSvr);
                //        this.ActualizarCodPiezaCP(iCodPiezaLocal, iCodPiezaSvr);
                //        this.ActualizarPiezaReemplazo(iCodPiezaLocal, iCodPiezaSvr);
                //        this.ActualizarCodPiezaDefectos(iCodPiezaLocal, iCodPiezaSvr);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZA-------------------------------------------------
                if (dsProcesoVaciado == null) return false;
                if (dsProcesoVaciado.Tables.Count != 8) return false;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Pieza
                foreach (DataRow row in dsProcesoVaciado.Tables[0].Rows)
                {
                    this.ActualizarCodPieza(Convert.ToInt32(row["CodPiezaLocal"].ToString()), Convert.ToInt32(row["CodPieza"].ToString()));
                    this.ActualizarCodPiezaT(Convert.ToInt32(row["CodPiezaLocal"].ToString()), Convert.ToInt32(row["CodPieza"].ToString()));
                    this.ActualizarCodPiezaCP(Convert.ToInt32(row["CodPiezaLocal"].ToString()), Convert.ToInt32(row["CodPieza"].ToString()));
                    this.ActualizarPiezaReemplazo(Convert.ToInt32(row["CodPiezaLocal"].ToString()), Convert.ToInt32(row["CodPieza"].ToString()));
                    this.ActualizarCodPiezaDefectos(Convert.ToInt32(row["CodPiezaLocal"].ToString()), Convert.ToInt32(row["CodPieza"].ToString()));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtPieza.Select("CodPieza = " + row["CodPiezaLocal"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtPieza.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtPieza.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRANSACCION-------------------------------------------------
                //// PiezaTransaccion
                //long lCodPiezaTransaccionLocal = -1;
                //long lCodConfigHandheld = -1;
                //int iCodPieza = -1;
                ////DateTime dtFechaRegistro = DateTime.MinValue;
                //long lCodPiezaTransaccionSvr = -1;

                //dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                //foreach (DataRow dr in dtPiezaTransaccion.Rows)
                //{
                //    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                //    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                //    lCodPiezaTransaccionSvr = cC.InsertarPiezaTransaccion(DA.eTipoConexion.Servicio, lCodConfigHandheld, iCodPieza);

                //    if (lCodPiezaTransaccionSvr != -1)
                //    {
                //        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                //        this.ActualizarCodPiezaTransaccionSecador(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRANSACCION-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //PiezaTransaccion
                foreach (DataRow row in dsProcesoVaciado.Tables[1].Rows)
                {
                    this.ActualizarCodPiezaTransaccion(Convert.ToInt32(row["CodPiezaTransaccionLocal"].ToString()), Convert.ToInt32(row["CodPiezaTransaccion"].ToString()));
                    this.ActualizarCodPiezaTransaccionSecador(Convert.ToInt32(row["CodPiezaTransaccionLocal"].ToString()), Convert.ToInt32(row["CodPiezaTransaccion"].ToString()));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtPiezaTransaccion.Select("CodPiezaTransaccion = " + row["CodPiezaTransaccionLocal"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtPiezaTransaccion.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtPiezaTransaccion.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA CONFIGBANCO-------------------------------------------------
                //// ConfigBanco
                ////int iCodConfigBanco = -1;
                //int iVaciadasAcumuladas = -1;
                //int iRes = -1;
                //bool bRes = false;

                //dtVaciadasAcumuladas = this.ObtenerActualizacionVaciadasAcumuladas();
                //foreach (DataRow dr in dtVaciadasAcumuladas.Rows)
                //{
                //    iCodConfigBanco = Convert.ToInt32(dr["CodConfigBanco"]);
                //    iVaciadasAcumuladas = Convert.ToInt32(dr["VaciadasAcumuladas"]);

                //    proxy.ActualizarVaciadasAcumuladas2(iCodConfigBanco, true, iVaciadasAcumuladas, true, out iRes, out bRes);

                //    if (bRes == true && iRes != -1)
                //    {
                //        this.EstablecerActualizacionVaciadasAcumuladas(iCodConfigBanco);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA CONFIGBANCO-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //ConfigBanco para Vaciadas Acumuladas
                foreach (DataRow row in dsProcesoVaciado.Tables[2].Rows)
                {
                    this.EstablecerActualizacionVaciadasAcumuladas(Convert.ToInt32(row["CodConfigBanco"]));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtVaciadasAcumuladas.Select("CodConfigBanco = " + row["CodConfigBanco"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtVaciadasAcumuladas.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtVaciadasAcumuladas.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA CARROPIEZA-------------------------------------------------
                //// CarroPieza
                //c05_ArmadoCarroSecado cACS = new c05_ArmadoCarroSecado();
                ////int iCodPlanta = -1;
                //int iCodProceso = -1;
                //int iCodCarro = -1;
                ////int iCodPieza = -1;
                ////DateTime dtFechaRegistro = DateTime.MinValue;
                ////int iRes = -1;
                ////bool bRes = false;
                //dtCarroPieza.Clear();
                //dtCarroPieza = this.ObtenerCarroPiezaLocal();
                //foreach (DataRow dr in dtCarroPieza.Rows)
                //{
                //    iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                //    iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                //    iCodCarro = Convert.ToInt32(dr["CodCarro"]);
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                //    iRes = cACS.InsertarCarroPieza(DA.eTipoConexion.Servicio, iCodPlanta, iCodProceso, iCodCarro, iCodPieza, dtFechaRegistro, 0);

                //    if (iRes != -1)
                //    {
                //        this.EstablecerActualizacionCarroPieza(iCodPlanta, iCodProceso, iCodCarro, iCodPieza);
                //    }
                //}
                //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA CARROPIEZA-------------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //CarroPieza
                foreach (DataRow row in dsProcesoVaciado.Tables[3].Rows)
                {
                    this.EstablecerActualizacionCarroPieza(Convert.ToInt32(row["CodPlanta"]), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodCarro"]), Convert.ToInt32(row["CodPieza"].ToString()));
                    //Depurar tabla en memoria
                    DataRow[] rows = dtCarroPieza.Select("CodPlanta = " + row["CodPlanta"].ToString() + " AND CodProceso = " + row["CodProceso"].ToString() + " AND CodCarro = " + row["CodCarro"].ToString() + " AND CodPieza = " + row["CodPieza"].ToString());
                    if (rows == null) continue;
                    for (int i = 0; i < rows.Length; i++)
                        dtCarroPieza.Rows.Remove(rows[i]);
                }
                //Depurar tabla Local
                //foreach (DataRow row in dtCarroPieza.Rows) { continue; }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoVaciado()))
                if (dtPiezaDefecto.Rows.Count > 0 || dtPiezaUltimoEstado.Rows.Count > 0 || dtPiezaUltimoProceso.Rows.Count > 0 || dsProcesoVaciado.Tables.Count > 0)
                {
                    if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoVaciado(), dtPiezaDefecto, dtPiezaUltimoEstado, dtPiezaUltimoProceso, dsProcesoVaciado))
                    {
                        throw new Exception("Error al enviar Defectos");
                    }
                }
                //PruebaProceso 
                foreach (DataRow row in dsProcesoVaciado.Tables[7].Rows)
                {
                    this.oDA0.PruebaProcesoDel(Convert.ToInt32(row["Codigo"]));
                }

                EnviarDatosSecado();
                bCompletado = true;
            }
            catch (Exception e)
            {
                bCompletado = false;
            }
            return bCompletado;
        }
        #endregion EnviarDatosVaciado
        #region EnviarDatosSecado
        public bool EnviarDatosSecado()
        {
            bool bCompletado = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

            try
            {
                this.EnviarConfigHandHeld(-1);
                // PiezaTransaccion
                c00_Common cC = new c00_Common();
                //long lCodPiezaTransaccionLocal = -1;
                //long lCodConfigHandheld = -1;
                //int iCodPieza = -1;
                DateTime dtFechaRegistro = DateTime.MinValue;
                //long lCodPiezaTransaccionSvr = -1;
                /*******************************************************************************************************/
                /////////////////////////////MODIFICACION PARA ENVIO DE CARGA MASIVA/////////////////////////////////////
                DataTable dtPieza = null, dtPiezaTransaccion = null, dtPiezaTransaccionSecador = null, dtCarroPieza = null;
                dtPieza = this.ObtenerActualizacionPieza();
                dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                dtPiezaTransaccionSecador = this.ObtenerPiezaTransaccionSecadorLocal();
                dtCarroPieza = this.ObtenerCarroPiezaEliminados();

                if (dtPieza.Rows.Count > 0 || dtPiezaTransaccion.Rows.Count > 0 || dtPiezaTransaccionSecador.Rows.Count > 0 || dtCarroPieza.Rows.Count > 0)
                {
                    DataSet dsProcesoSecado = ProcesarBatchSecadoPieza(dtPieza, dtPiezaTransaccion, dtPiezaTransaccionSecador, dtCarroPieza);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (dsProcesoSecado == null) return false;
                    if (dsProcesoSecado.Tables.Count != 4) return false;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRANSACCION-------------------------------------------------
                    //DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                    //foreach (DataRow dr in dtPiezaTransaccion.Rows)
                    //{
                    //    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                    //    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                    //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    //    lCodPiezaTransaccionSvr = cC.InsertarPiezaTransaccion(DA.eTipoConexion.Servicio, lCodConfigHandheld, iCodPieza);

                    //    if (lCodPiezaTransaccionSvr != -1)
                    //    {
                    //        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                    //        this.ActualizarCodPiezaTransaccionSecador(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                    //    }
                    //}
                    //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRANSACCION-------------------------------------------------
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //PiezaTransaccion
                    foreach (DataRow row in dsProcesoSecado.Tables[1].Rows)
                    {
                        this.ActualizarCodPiezaTransaccion(Convert.ToInt32(row["CodPiezaTransaccionLocal"].ToString()), Convert.ToInt32(row["CodPiezaTransaccion"].ToString()));
                        this.ActualizarCodPiezaTransaccionSecador(Convert.ToInt32(row["CodPiezaTransaccionLocal"].ToString()), Convert.ToInt32(row["CodPiezaTransaccion"].ToString()));
                        //Depurar tabla en memoria
                        DataRow[] rows = dtPiezaTransaccion.Select("CodPiezaTransaccion = " + row["CodPiezaTransaccionLocal"].ToString());
                        if (rows == null) continue;
                        for (int i = 0; i < rows.Length; i++)
                            dtPiezaTransaccion.Rows.Remove(rows[i]);
                    }
                    //Depurar tabla Local
                    //foreach (DataRow row in dtPiezaTransaccion.Rows) { continue; }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZA-------------------------------------------------
                    //// Pieza
                    ////int iCodPieza = -1;
                    //int iCodUltimoProceso = -1;
                    //int iCodUltimoEstado = -1;
                    //int iRes = -1;
                    //DataTable dtPieza = this.ObtenerActualizacionPieza();
                    //foreach (DataRow dr in dtPieza.Rows)
                    //{
                    //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                    //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                    //    iRes = cC.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoProceso);

                    //    if (iRes != -1)
                    //    {
                    //        this.EstablecerActualizacionPieza(iCodPieza);
                    //    }
                    //}
                    //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZA-------------------------------------------------
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //Pieza
                    foreach (DataRow row in dsProcesoSecado.Tables[0].Rows)
                    {
                        this.EstablecerActualizacionPieza(Convert.ToInt32(row["CodPieza"].ToString()));
                        //Depurar tabla en memoria
                        DataRow[] rows = dtPieza.Select("CodPieza = " + row["CodPieza"].ToString());
                        if (rows == null) continue;
                        for (int i = 0; i < rows.Length; i++)
                            dtPieza.Rows.Remove(rows[i]);
                    }
                    //Depurar tabla Local
                    //foreach (DataRow row in dtPieza.Rows) { continue; }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRASACCIONSECADOR-----------------------------------------
                    //// PiezaTransaccionSecador
                    //c06_EntradaCarroSecador cECS = new c06_EntradaCarroSecador();
                    //long lCodPiezaTransaccion = -1;
                    //DateTime dtHoraInicio = DateTime.MinValue;
                    //double dHorasSecado = -1;
                    ////DateTime dtFechaRegistro = DateTime.MinValue;
                    ////int iRes = -1;
                    //DataTable dtPiezaTransaccionSecador = this.ObtenerPiezaTransaccionSecadorLocal();
                    //foreach (DataRow dr in dtPiezaTransaccionSecador.Rows)
                    //{
                    //    lCodPiezaTransaccion = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                    //    dtHoraInicio = Convert.ToDateTime(dr["HoraInicio"]);
                    //    dHorasSecado = Convert.ToDouble(dr["HorasSecado"]);
                    //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    //iRes = cECS.InsertarPiezaTransaccionSecador(DA.eTipoConexion.Servicio, lCodPiezaTransaccion, dtHoraInicio, dHorasSecado);

                    //    if (iRes != -1)
                    //    {
                    //        this.EstablecerActualizacionPiezaTransaccionSecador(lCodPiezaTransaccion);
                    //    }
                    //}
                    //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA PIEZATRASACCIONSECADOR-----------------------------------------
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //PiezaTransaccionSecador
                    foreach (DataRow row in dsProcesoSecado.Tables[2].Rows)
                    {
                        this.EstablecerActualizacionPiezaTransaccionSecador(Convert.ToInt32(row["CodPiezaTransaccion"].ToString()));
                        //Depurar tabla en memoria
                        DataRow[] rows = dtPiezaTransaccionSecador.Select("CodPiezaTransaccion = " + row["CodPiezaTransaccion"].ToString());
                        if (rows == null) continue;
                        for (int i = 0; i < rows.Length; i++)
                            dtPiezaTransaccionSecador.Rows.Remove(rows[i]);
                    }
                    //Depurar tabla Local
                    //foreach (DataRow row in dtPiezaTransaccionSecador.Rows) { continue; }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //--------------------INICIO: CODIGO ELIMINADO POR REINGENIERIA PARA CARROPIEZA-------------------------------------------------
                    //// CarroPieza
                    //int iCodPlanta = -1;
                    //int iCodProceso = -1;
                    //int iCodCarro = -1;
                    ////int iRes = -1;
                    ////bool bRes = false;
                    //DataTable dtCarroPieza = this.ObtenerCarroPiezaEliminados();
                    //foreach (DataRow dr in dtCarroPieza.Rows)
                    //{
                    //    iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                    //    iCodProceso = Convert.ToInt32(dr["CodProceso"]);
                    //    iCodCarro = Convert.ToInt32(dr["CodCarro"]);

                    //iRes = cC.EliminarCarro(DA.eTipoConexion.Servicio, iCodPlanta, iCodProceso, iCodCarro);

                    //    if (iRes != -1)
                    //    {
                    //        cC.EliminarCarro(DA.eTipoConexion.Local, iCodPlanta, iCodProceso, iCodCarro);
                    //    }
                    //}
                    //--------------------FIN: CODIGO ELIMINADO POR REINGENIERIA PARA CARROPIEZA-------------------------------------------------
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //CarroPieza
                    foreach (DataRow row in dsProcesoSecado.Tables[3].Rows)
                    {
                        cC.EliminarCarro(DA.eTipoConexion.Local, Convert.ToInt32(row["CodPlanta"]), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodCarro"]));
                        //Depurar tabla en memoria
                        DataRow[] rows = dtCarroPieza.Select("CodPlanta = " + row["CodPlanta"].ToString() + " AND CodProceso = " + row["CodProceso"].ToString() + " AND CodCarro = " + row["CodCarro"].ToString());
                        if (rows == null) continue;
                        for (int i = 0; i < rows.Length; i++)
                            dtCarroPieza.Rows.Remove(rows[i]);
                    }
                    //Depurar tabla Local
                    //foreach (DataRow row in dtCarroPieza.Rows) { continue; }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                bCompletado = true;
            }
            catch (Exception e)
            {
                bCompletado = false;
            }
            return bCompletado;
        }
        #endregion EnviarDatosSecado
        #region EnviarDatosRevisado
        public bool EnviarDatosRevisado()
        {
            bool bCompletado = false;
            bool bEnvioIncompleto = false;
            int iCodAnt = 0;
            int iCodNuevo = 0;
            int iCodPieza = 0;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            DataSet dsRes = new DataSet();
            int iRes = -1;
            long lCodConfigHandHeldEsmaltado = 0;
            try
            {
                //////////////////////////////INSERTAR PARA ESMALTADO
                if (this.lu.CodUsuario != -1)
                {
                    DataTable dtPiezaEsmaltado = this.ObtenerActualizacionPieza();
                    long lCodPiezaTransaccion;
                    int iCodConfigBanco = this.ObtenerConfigBancoCasetaTanque(this.lu.CodMaquina,0,this.lu.CodProceso, this.lu.CodPlanta);
                    int procesoEsmaltado = oDA0.ObtenerCodProcesoEsmaltado();
                    this.lu.CodSupervisor = this.ObtenerSupervisorPorDefecto(this.lu.CodUsuario);
                    lCodConfigHandHeldEsmaltado = new c03_ConfiguracionInicial().InsertarConfigHandHeld(DA.eTipoConexion.Local,
                                                                                        this.lu.CodUsuario,
                                                                                        this.lu.CodEmpleado,
                                                                                        this.lu.CodSupervisor,
                                                                                         this.lu.Fecha,
                                                                                        this.lu.CodTurno,
                                                                                        this.lu.CodPlanta,
                                                                                        procesoEsmaltado,
                                                                                        iCodConfigBanco,
                                                                                        null);
                    foreach (DataRow dr in dtPiezaEsmaltado.Rows)
                    {
                        if (dr["CodColor"] == DBNull.Value) continue;
                        iRes = -1;
                        lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local, lCodConfigHandHeldEsmaltado, Convert.ToInt32(dr["codpieza"]),this.lu.Fecha);
                        iRes = this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, Convert.ToInt32(dr["codpieza"]), procesoEsmaltado);
                    }
                }
                /////////////////////////////INSERTAR PARA ESMALTADO

                this.EnviarConfigHandHeld(-1);
                ////////////////////////////////////////////Envio de informacion Revisado/////////////////////////////////////////////////////////////
                DataTable dtPiezaRes = new DataTable();
                DataTable dtPiezaTransaccionRes = new DataTable();
                DataTable dtPiezaDefectoRes = new DataTable();
                DataTable dtPiezaUltimoEstadoRes = new DataTable();
                DataTable dtPruebaProcesoRes = new DataTable();


                DataTable dtPieza = this.ObtenerActualizacionPieza();
                DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                DataTable dtPiezaDefecto = this.ObtenerPiezaDefectoLocales(this.oDA0.ObtenerCodProcesoRevisado());
                DataTable dtPiezaUltimoEstado = this.ObtenerPiezaUltimoEstadoLocales();
                dtPiezaUltimoEstado.TableName = "PiezaUltimoEstado";
                DataTable dtPruebaProceso = this.oDA0.PruebaProcesoSel();
                DataSet dsTransaccion = new DataSet();
                dsTransaccion.Tables.Add(dtPieza);
                dsTransaccion.Tables.Add(dtPiezaTransaccion);
                dsTransaccion.Tables.Add(dtPiezaDefecto);
                dsTransaccion.Tables.Add(dtPiezaUltimoEstado);
                dsTransaccion.Tables.Add(dtPruebaProceso);
                dsRes = proxy.ProcesarBatchRevisadoPieza(dsTransaccion);

                if (dsRes != null && dsRes.Tables.Count > 0)
                {
                    dtPiezaRes = dsRes.Tables[0];
                    dtPiezaTransaccionRes = dsRes.Tables[1];
                    dtPiezaDefectoRes = dsRes.Tables[2];
                    dtPiezaUltimoEstadoRes = dsRes.Tables[3];
                    dtPruebaProcesoRes = dsRes.Tables[4];

                    if (dtPieza.Rows.Count != dtPiezaRes.Rows.Count || dtPiezaTransaccion.Rows.Count != dtPiezaTransaccionRes.Rows.Count
                        || dtPiezaDefecto.Rows.Count != dtPiezaDefectoRes.Rows.Count || dtPiezaUltimoEstado.Rows.Count != dtPiezaUltimoEstadoRes.Rows.Count
                        || dtPruebaProceso.Rows.Count != dtPruebaProcesoRes.Rows.Count)
                        bEnvioIncompleto = true;
                    //////////////////Pieza Ultimo Proceso////////////////////////////////////
                    foreach (DataRow dr in dtPiezaRes.Rows)
                    {
                        try
                        {
                            iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                            this.EstablecerActualizacionPieza(iCodPieza);
                        }
                        catch { }
                    }
                    //////////////////Pieza Transaccion///////////////////////////////////////
                    foreach (DataRow dr in dtPiezaTransaccionRes.Rows)
                    {
                        try
                        {
                            iCodAnt = Convert.ToInt32(dr["CodAnt"]);
                            iCodNuevo = Convert.ToInt32(dr["CodNuevo"]);
                            this.ActualizarCodPiezaTransaccion(iCodAnt, iCodNuevo);
                        }
                        catch { }
                    }
                    //////////////////Pieza Defecto///////////////////////////////////////////
                    foreach (DataRow row in dtPiezaDefectoRes.Rows)
                    {
                        if (Convert.ToBoolean(row["Nuevo"]))
                        {
                            this.QuitarMarcaNuevoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        }
                        else if (Convert.ToBoolean(row["Modificado"]))
                        {
                            this.QuitarMarcaModificadoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        }
                        else if (Convert.ToBoolean(row["Eliminado"]))
                        {
                            new c04_Defectos().EliminarPiezaDefecto(DA.eTipoConexion.Local, Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                        }
                    }
                    //////////////////Pieza Ultimo Estado/////////////////////////////////////
                    foreach (DataRow row in dtPiezaUltimoEstado.Rows)
                    {
                        this.QuitarMarcaModificadoEstadoPieza(Convert.ToInt32(row["CodPieza"].ToString()));
                    }
                    //////////////////Prueba Proceso//////////////////////////////////////////
                    foreach (DataRow row in dtPruebaProceso.Rows)
                    {
                        this.oDA0.PruebaProcesoDel(Convert.ToInt32(row["Codigo"]));
                    }
                }


                ////////////////////////////////////////////Fin Envio de informacion de Revisado//////////////////////////////////////////////////////
                #region Codigo Anterior
                // PiezaTransaccion
                //c00_Common cC = new c00_Common();
                //long lCodPiezaTransaccionLocal = -1;
                //long lCodConfigHandheld = -1;
                //DateTime dtFechaRegistro = DateTime.MinValue;
                //long lCodPiezaTransaccionSvr = -1;

                //DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                //foreach (DataRow dr in dtPiezaTransaccion.Rows)
                //{
                //    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                //    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                //    lCodPiezaTransaccionSvr = cC.InsertarPiezaTransaccion(DA.eTipoConexion.Servicio, lCodConfigHandheld, iCodPieza);

                //    if (lCodPiezaTransaccionSvr != -1)
                //    {
                //        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                //    }
                //}

                //// Pieza
                //int iCodUltimoProceso = -1;
                //int iCodUltimoEstado = -1;
                ////int iRes = -1;
                //iRes = -1;
                //DataTable dtPieza = this.ObtenerActualizacionPieza();
                //foreach (DataRow dr in dtPieza.Rows)
                //{
                //    iRes = -1;
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                //    iRes = cC.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoProceso);

                //    if (dr["CodColor"] != DBNull.Value)
                //        cC.ActualizarColorPieza(DA.eTipoConexion.Servicio, iCodPieza, Convert.ToInt32(dr["CodColor"]));
                //    if (iRes != -1)
                //    {
                //        this.EstablecerActualizacionPieza(iCodPieza);
                //    }
                //}

                //if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoRevisado()))
                //{
                //    throw new Exception("Error al enviar Defectos");
                //}
                #endregion 
                if (lCodConfigHandHeldEsmaltado != 0 & this.lu.CodUsuario != -1)
                    this.EnviarDatosEsmaltado();
                if (!bEnvioIncompleto)
                    bCompletado = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return bCompletado;
        }
        #endregion EnviarDatosRevisado
        #region EnviarDatosEsmaltado
        public bool EnviarDatosEsmaltado()
        {
            bool bCompletado = false;
            bool bEnvioIncompleto = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            DataSet dsRes = new DataSet();
            int iCodAnt = 0;
            int iCodNuevo = 0;
            int iCodPieza = 0;
            try
            {
                this.EnviarConfigHandHeld(this.oDA0.ObtenerCodProcesoEsmaltado());
                DataTable dtPiezaRes = new DataTable();
                DataTable dtPiezaTransaccionRes = new DataTable();
                DataTable dtPruebaProcesoRes = new DataTable();

                DataTable dtPieza = this.ObtenerActualizacionPieza();
                DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                DataTable dtPruebaProceso = this.oDA0.PruebaProcesoSel();
                DataSet dsTransaccion = new DataSet();
                dsTransaccion.Tables.Add(dtPieza);
                dsTransaccion.Tables.Add(dtPiezaTransaccion);
                dsTransaccion.Tables.Add(dtPruebaProceso);
                dsRes = proxy.ProcesarBatchEsmaltadoPiezas(dsTransaccion);

                if (dsRes != null && dsRes.Tables.Count > 0)
                {
                    dtPiezaRes = dsRes.Tables[0];
                    dtPiezaTransaccionRes = dsRes.Tables[1];
                    dtPruebaProcesoRes = dsRes.Tables[2];
                    if (dtPieza.Rows.Count != dtPiezaRes.Rows.Count 
                        || dtPiezaTransaccion.Rows.Count != dtPiezaTransaccionRes.Rows.Count
                        || dtPruebaProceso.Rows.Count != dtPruebaProcesoRes.Rows.Count)
                        bEnvioIncompleto = true;
                    foreach (DataRow dr in dtPiezaRes.Rows)
                    {
                        try
                        {
                            iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                            this.EstablecerActualizacionPieza(iCodPieza);
                        }
                        catch { }
                    }
                    foreach (DataRow dr in dtPiezaTransaccionRes.Rows)
                    {
                        try
                        {
                            iCodAnt = Convert.ToInt32(dr["CodAnt"]);
                            iCodNuevo = Convert.ToInt32(dr["CodNuevo"]);
                            this.ActualizarCodPiezaTransaccion(iCodAnt, iCodNuevo);
                        }
                        catch { }
                    }
                    //////////////////Prueba Proceso//////////////////////////////////////////
                    foreach (DataRow row in dtPruebaProceso.Rows)
                    {
                        this.oDA0.PruebaProcesoDel(Convert.ToInt32(row["Codigo"]));
                    }

                    //// PiezaTransaccion
                    //long lCodPiezaTransaccionLocal = -1;
                    //long lCodConfigHandheld = -1;
                    //int iCodPieza = -1;
                    //DateTime dtFechaRegistro = DateTime.MinValue;
                    //long lCodPiezaTransaccionSvr = -1;
                    //bool bCodPiezaTransaccionSvr = false;

                    //DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                    //foreach (DataRow dr in dtPiezaTransaccion.Rows)
                    //{
                    //    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                    //    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                    //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    //    proxy.InsertarPiezaTransaccion(lCodConfigHandheld, true, iCodPieza, true, dtFechaRegistro, true,
                    //                                    out lCodPiezaTransaccionSvr, out bCodPiezaTransaccionSvr);

                    //    if (bCodPiezaTransaccionSvr == true && lCodPiezaTransaccionSvr != -1)
                    //    {
                    //        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                    //    }
                    //}

                    //// Pieza
                    ////int iCodPieza = -1;
                    //int iCodColor = -1;
                    //int iCodUltimoProceso = -1;
                    //int iCodUltimoEstado = -1;
                    //int iResUltimoProceso = -1;
                    //bool bResUltimoProceso = false;
                    //int iResColor = -1;
                    //bool bResColor = false;

                    //DataTable dtPieza = this.ObtenerActualizacionPieza();
                    //foreach (DataRow dr in dtPieza.Rows)
                    //{
                    //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    //    iCodColor = Convert.ToInt32(dr["CodColor"]);
                    //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                    //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                    //    proxy.ActulizarUltimoProcesoPieza(iCodPieza, true, iCodUltimoProceso, true,
                    //                                        out iResUltimoProceso, out bResUltimoProceso);
                    //    proxy.ActualizarColorPieza(iCodPieza, true, iCodColor, true, out iResColor, out bResColor);

                    //    if ((bResUltimoProceso == true && iResUltimoProceso != -1) && (bResColor == true && iResColor != -1))
                    //    {
                    //        this.EstablecerActualizacionPieza(iCodPieza);
                    //    }
                    //}

                    if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoEsmaltado()))
                    {
                        throw new Exception("Error al enviar Defectos");
                    }
                    if (!bEnvioIncompleto)
                        bCompletado = true;
                }
            }
            catch (Exception e)
            {
                bCompletado = false;
            }
            return bCompletado;
        }
        #endregion EnviarDatosEsmaltado
        #region EnviarDatosHornos
        public bool EnviarDatosHornos()
        {
            bool bEnvioIncompleto = false;
            int iCodAnt = 0;
            int iCodNuevo = 0;
            int iCodPieza = 0;
            bool bCompletado = false;
            DataSet dsRes = new DataSet();
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

            try
            {
                this.EnviarConfigHandHeld(this.oDA0.ObtenerCodProcesoHornos());

                ////////////////////////////////////////////Envio de informacion Hornos///////////////////////////////////////////////////////////////
                DataTable dtPiezaRes = new DataTable();
                DataTable dtPiezaTransaccionRes = new DataTable();
                DataTable dtPiezaDefectoRes = new DataTable();
                DataTable dtPiezaUltimoEstadoRes = new DataTable();
                DataTable dtCarroPiezaQuemadoRes = new DataTable();
                DataTable dtPruebaProcesoRes = new DataTable();

                DataTable dtPieza = this.ObtenerActualizacionPieza();
                DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                DataTable dtPiezaDefecto = this.ObtenerPiezaDefectoLocales(this.oDA0.ObtenerCodProcesoRevisado());
                DataTable dtPiezaUltimoEstado = this.ObtenerPiezaUltimoEstadoLocales();
                DataTable dtCarroPiezaQuemado = this.ObtenerCarroPiezaQuemadoLocal();
                DataTable dtPruebaProceso = this.oDA0.PruebaProcesoSel();
                dtPiezaUltimoEstado.TableName = "PiezaUltimoEstado";
                DataSet dsTransaccion = new DataSet();
                dsTransaccion.Tables.Add(dtPieza);
                dsTransaccion.Tables.Add(dtPiezaTransaccion);
                dsTransaccion.Tables.Add(dtPiezaDefecto);
                dsTransaccion.Tables.Add(dtPiezaUltimoEstado);
                dsTransaccion.Tables.Add(dtCarroPiezaQuemado);
                dsTransaccion.Tables.Add(dtPruebaProceso);

                dsRes = proxy.ProcesarBatchHornosPieza(dsTransaccion);

                if (dsRes != null && dsRes.Tables.Count > 0)
                {
                    dtPiezaRes = dsRes.Tables[0];
                    dtPiezaTransaccionRes = dsRes.Tables[1];
                    dtPiezaDefectoRes = dsRes.Tables[2];
                    dtPiezaUltimoEstadoRes = dsRes.Tables[3];
                    dtCarroPiezaQuemadoRes = dsRes.Tables[4];
                    dtPruebaProcesoRes = dsRes.Tables[5];
                    if (dtPieza.Rows.Count != dtPiezaRes.Rows.Count || dtPiezaTransaccion.Rows.Count != dtPiezaTransaccionRes.Rows.Count
                        || dtPiezaDefecto.Rows.Count != dtPiezaDefectoRes.Rows.Count || dtPiezaUltimoEstado.Rows.Count != dtPiezaUltimoEstadoRes.Rows.Count
                        || dtCarroPiezaQuemado.Rows.Count != dtCarroPiezaQuemadoRes.Rows.Count || dtPruebaProceso.Rows.Count != dtPruebaProcesoRes.Rows.Count)
                        bEnvioIncompleto = true;
                    //////////////////Pieza Ultimo Proceso////////////////////////////////////
                    foreach (DataRow dr in dtPiezaRes.Rows)
                    {
                        try
                        {
                            iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                            this.EstablecerActualizacionPieza(iCodPieza);
                        }
                        catch { }
                    }
                    //////////////////Pieza Transaccion///////////////////////////////////////
                    foreach (DataRow dr in dtPiezaTransaccionRes.Rows)
                    {
                        try
                        {
                            iCodAnt = Convert.ToInt32(dr["CodAnt"]);
                            iCodNuevo = Convert.ToInt32(dr["CodNuevo"]);
                            this.ActualizarCodPiezaTransaccion(iCodAnt, iCodNuevo);
                        }
                        catch { }
                    }
                    //////////////////Pieza Defecto///////////////////////////////////////////
                    foreach (DataRow row in dtPiezaDefectoRes.Rows)
                    {
                        try
                        {
                            if (Convert.ToBoolean(row["Nuevo"]))
                            {
                                this.QuitarMarcaNuevoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                            }
                            else if (Convert.ToBoolean(row["Modificado"]))
                            {
                                this.QuitarMarcaModificadoPiezaDefecto(Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                            }
                            else if (Convert.ToBoolean(row["Eliminado"]))
                            {
                                new c04_Defectos().EliminarPiezaDefecto(DA.eTipoConexion.Local, Convert.ToInt32(row["CodPieza"].ToString()), Convert.ToInt32(row["CodProceso"]), Convert.ToInt32(row["CodDefecto"]), Convert.ToInt32(row["CodZonaDefecto"]));
                            }
                        }
                        catch { }
                    }
                    //////////////////Pieza Ultimo Estado/////////////////////////////////////
                    foreach (DataRow row in dtPiezaUltimoEstado.Rows)
                    {
                        this.QuitarMarcaModificadoEstadoPieza(Convert.ToInt32(row["CodPieza"].ToString()));
                    }
                    //////////////////Carro Pieza Quemado/////////////////////////////////////
                    foreach (DataRow dr in dtCarroPiezaQuemado.Rows)
                    {
                        try
                        {
                            this.EstablecerActualizacionCarroPiezaQuemado(Convert.ToInt32(dr["CodPlanta"]), Convert.ToInt32(dr["CodPieza"]),
                                                                        Convert.ToInt32(dr["CodCarro"]), Convert.ToString(dr["CodZona"]));
                        }
                        catch { }
                    }
                    //////////////////Prueba Proceso//////////////////////////////////////////
                    foreach (DataRow row in dtPruebaProceso.Rows)
                    {
                        this.oDA0.PruebaProcesoDel(Convert.ToInt32(row["Codigo"]));
                    }
                }
                ////////////////////////////////////////////Fin Envio de informacion de Hornos////////////////////////////////////////////////////////
                #region CodigoAnterior
                //// PiezaTransaccion
                //c00_Common cC = new c00_Common();
                //long lCodPiezaTransaccionLocal = -1;
                //long lCodConfigHandheld = -1;
                //int iCodPieza = -1;
                //DateTime dtFechaRegistro = DateTime.MinValue;
                //long lCodPiezaTransaccionSvr = -1;

                //DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                //foreach (DataRow dr in dtPiezaTransaccion.Rows)
                //{
                //    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                //    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                //    lCodPiezaTransaccionSvr = cC.InsertarPiezaTransaccion(DA.eTipoConexion.Servicio, lCodConfigHandheld, iCodPieza);

                //    if (lCodPiezaTransaccionSvr != -1)
                //    {
                //        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                //    }
                //}

                //// Pieza
                //int iCodColor = -1;
                //int iCodUltimoProceso = -1;
                //int iCodUltimoEstado = -1;
                //int iRes = -1;

                //DataTable dtPieza = this.ObtenerActualizacionPieza();
                //foreach (DataRow dr in dtPieza.Rows)
                //{
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    //iCodColor = Convert.ToInt32(dr["CodColor"]);
                //    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                //    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                //    iRes = cC.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, iCodPieza, iCodUltimoProceso);

                //    if (iRes != -1)
                //    {
                //        this.EstablecerActualizacionPieza(iCodPieza);
                //    }
                //}

                //// CarroPiezaQuemado
                //c09_CapturaHornos cCH = new c09_CapturaHornos();
                //int iCodPlanta = -1;
                //int iCodCarro = -1;
                //string sCodZona = string.Empty;

                //DataTable dtCarroPiezaQuemado = this.ObtenerCarroPiezaQuemadoLocal();
                //foreach (DataRow dr in dtCarroPiezaQuemado.Rows)
                //{
                //    iCodPlanta = Convert.ToInt32(dr["CodPlanta"]);
                //    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                //    iCodCarro = Convert.ToInt32(dr["CodCarro"]);
                //    sCodZona = Convert.ToString(dr["CodZona"]);

                //    iRes = cCH.InsertarCarroZonaPieza(DA.eTipoConexion.Servicio, iCodPlanta, iCodPieza, iCodCarro, sCodZona);

                //    if (iRes != -1)
                //    {
                //        this.EstablecerActualizacionCarroPiezaQuemado(iCodPlanta, iCodPieza, iCodCarro, sCodZona);
                //    }
                //}

                //if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoHornos()))
                //{
                //    throw new Exception("Error al enviar Defectos");
                //}
                #endregion
                if (!bEnvioIncompleto)
                    bCompletado = true;
            }
            catch (Exception)
            {
                bCompletado = false;
            }
            return bCompletado;
        }
        #endregion EnviarDatosHornos
        #region EnviarDatosEmpaque
        public bool EnviarDatosEmpaque()
        {
            bool bCompletado = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

            try
            {
                this.EnviarConfigHandHeld(this.oDA0.ObtenerCodProcesoEmpaque());

                // PiezaTransaccion
                long lCodPiezaTransaccionLocal = -1;
                long lCodConfigHandheld = -1;
                int iCodPieza = -1;
                DateTime dtFechaRegistro = DateTime.MinValue;
                long lCodPiezaTransaccionSvr = -1;
                bool bCodPiezaTransaccionSvr = false;

                DataTable dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                foreach (DataRow dr in dtPiezaTransaccion.Rows)
                {
                    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    proxy.InsertarPiezaTransaccion(lCodConfigHandheld, true, iCodPieza, true, dtFechaRegistro, true,
                                                    out lCodPiezaTransaccionSvr, out bCodPiezaTransaccionSvr);

                    if (bCodPiezaTransaccionSvr == true && lCodPiezaTransaccionSvr != -1)
                    {
                        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                    }
                }

                // Pieza
                //int iCodPieza = -1;
                int iCodModelo = -1;
                int iCodColor = -1;
                int iCodCalidad = -1;
                int iCodUltimoProceso = -1;
                int iCodUltimoEstado = -1;
                int iResUltimoProceso = -1;
                bool bResUltimoProceso = false;
                int iResModelo = -1;
                bool bResModelo = false;
                int iResCalidad = -1;
                bool bResCalidad = false;

                DataTable dtPieza = this.ObtenerActualizacionPieza();
                foreach (DataRow dr in dtPieza.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodModelo = Convert.ToInt32(dr["CodModelo"]);
                    iCodColor = Convert.ToInt32(dr["CodColor"]);
                    iCodCalidad = Convert.ToInt32(dr["CodCalidad"]);
                    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);

                    proxy.ActulizarUltimoProcesoPieza(iCodPieza, true, iCodUltimoProceso, true,
                                                        out iResUltimoProceso, out bResUltimoProceso);
                    proxy.ActualizarModeloPieza(iCodPieza, true, iCodModelo, true, out iResModelo, out bResModelo);
                    proxy.ActualizarCalidadPieza(iCodPieza, true, iCodCalidad, true, out iResCalidad, out bResCalidad);

                    if ((bResUltimoProceso == true && iResUltimoProceso != -1) && (bResModelo == true && iResModelo != -1) && (bResCalidad == true && iResCalidad != -1))
                    {
                        this.EstablecerActualizacionPieza(iCodPieza);
                    }
                }

                // TarimaPieza
                int iCodTarima = -1;
                //int iCodPieza = -1;
                bool bPaletizado = false;
                bool bRechazada = false;
                //DateTime dtFechaRegistro = DateTime.MinValue;
                int iRes = -1;
                bool bRes = false;

                DataTable dtTarimaPieza = this.ObtenerTarimaPiezaLocal();
                foreach (DataRow dr in dtTarimaPieza.Rows)
                {
                    iCodTarima = Convert.ToInt32(dr["CodTarima"]);
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    bPaletizado = Convert.ToBoolean(dr["Paletizado"]);
                    bRechazada = Convert.ToBoolean(dr["Rechazada"]);
                    bPaletizado = (!bRechazada) ? true : false;
                    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    proxy.InsertarTarimaPieza(iCodTarima, true, iCodPieza, true, bPaletizado, true, bRechazada, true,
                                                dtFechaRegistro, true, out iRes, out bRes);

                    if (bRes == true && iRes != -1)
                    {
                        this.EstablecerActualizacionTarimaPieza(iCodTarima, iCodPieza, (bPaletizado) ? 1 : 0, (bRechazada) ? 1 : 0);
                    }
                }

                if (!this.EnviarDefectos(this.oDA0.ObtenerCodProcesoEmpaque()))
                {
                    throw new Exception("Error al enviar Defectos");
                }

                bCompletado = true;
            }
            catch (Exception)
            {
                bCompletado = false;
            }
            return bCompletado;
        }
        #endregion EnviarDatosEmpaque
        //////////////Erwin/////////////

        #region EnviarDatosAuditoria
        public bool EnviarDatosAuditoria()
        {
            bool bCompletado = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            long lCodPiezaTransaccionLocal = -1;
            long lCodConfigHandheld = -1;
            int iCodPieza = -1;
            DateTime dtFechaRegistro = DateTime.MinValue;
            long lCodPiezaTransaccionSvr = -1;
            bool bCodPiezaTransaccionSvr = false;
            DataTable dtPiezaTransaccion = null;
            int iCodColor = -1;
            int iCodCalidad = -1;
            int iCodUltimoProceso = -1;
            int iCodUltimoEstado = -1;
            int iRes = -1;
            bool bRes = false;
            bool bPiezaAuditada = false;
            DataTable dtPieza = null;

            DataTable dtTarimaPieza = null;
            try
            {
                //ConfigHandHeld
                this.EnviarConfigHandHeld(this.oDA0.ObtenerCodProcesoAuditoria());
                // PiezaTransaccion
                dtPiezaTransaccion = this.ObtenerPiezaTransaccionLocal();
                if (dtPiezaTransaccion == null) return true;
                foreach (DataRow dr in dtPiezaTransaccion.Rows)
                {
                    lCodPiezaTransaccionLocal = Convert.ToInt64(dr["CodPiezaTransaccion"]);
                    lCodConfigHandheld = Convert.ToInt64(dr["CodConfigHandheld"]);
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                    proxy.InsertarPiezaTransaccion(lCodConfigHandheld, true, iCodPieza, true, dtFechaRegistro, true,
                                                    out lCodPiezaTransaccionSvr, out bCodPiezaTransaccionSvr);

                    if (bCodPiezaTransaccionSvr == true && lCodPiezaTransaccionSvr != -1)
                    {
                        this.ActualizarCodPiezaTransaccion(lCodPiezaTransaccionLocal, lCodPiezaTransaccionSvr);
                    }
                }
                // Pieza
                dtPieza = this.ObtenerActualizacionPieza();
                if (dtPieza == null) return true;
                foreach (DataRow dr in dtPieza.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodColor = Convert.ToInt32(dr["CodColor"]);
                    iCodCalidad = (dr["CodCalidad"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CodCalidad"]);
                    iCodUltimoProceso = Convert.ToInt32(dr["CodUltimoProceso"]);
                    iCodUltimoEstado = Convert.ToInt32(dr["CodUltimoEstado"]);
                    bPiezaAuditada = Convert.ToBoolean(dr["Auditada"]);
                    bPiezaAuditada = true;
                    proxy.ActulizarUltimoProcesoPieza(iCodPieza, true, iCodUltimoProceso, true, out iRes, out bRes);
                    if (bRes)
                        proxy.ActualizarPiezaAuditada(iCodPieza, true, bPiezaAuditada, true, out iRes, out bRes);
                    if (bRes == true && iRes != -1)
                    {
                        this.EstablecerActualizacionPieza(iCodPieza);
                    }
                }
                // TarimaPieza
                dtTarimaPieza = this.ObtenerActualizacionTarimaPieza();
                if (dtTarimaPieza == null) return true;
                foreach (DataRow row in dtTarimaPieza.Rows)
                {
                    if (Convert.ToBoolean(row["Paletizado"]))
                        proxy.ActualizarTarimaPaletizado(Convert.ToInt32(row["CodTarima"]), true, Convert.ToBoolean(row["Paletizado"]), true, out iRes, out bRes);
                    else if (Convert.ToBoolean(row["Rechazada"]))
                        proxy.RechazarTarimaPieza(Convert.ToInt32(row["CodTarima"]), true, out iRes, out bRes);
                    if (bRes == true && iRes != -1)
                    {
                        this.EstablecerActualizacionTarimaPiezaMod(Convert.ToInt32(row["CodTarima"]), Convert.ToInt32(row["CodPieza"]));
                    }
                }
                //CodTarima, CodPieza, Paletizado, Rechazada, FechaRegistro, Modificado
                bCompletado = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EnviarDatosAuditoria: " + ex.Message);
            }
            return bCompletado;
        }
        #endregion EnviarDatosAuditoria
        #region EnviarDatosInventario
        public bool EnviarDatosInventario()
        {
            bool bCompletado = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

            try
            {
                /////////////////////////////////////////////////////
                long idPieza = -1;
                int iRes = -1;
                bool bRes = true;
                int iCodPieza = 0;
                DataTable dtPiezaTransaccion = this.ObtenerInventarioProceso();
                foreach (DataRow dr in dtPiezaTransaccion.Rows)
                {
                    idPieza = Convert.ToInt64(dr["IdPieza"]);

                    proxy.ActualizarPiezaInventario(iCodPieza, true, out iRes, out bRes);

                    if (iRes != -1)
                    {
                        this.DeleteInventarioProceso(iCodPieza);
                    }
                }
                //------------------------------------------------------
                DataTable dtInsertarPiezaInventario = this.ObtenerInsertarPiezaInventario();
                string sCodBarras;
                int iCodPlanta;
                int iCodProceso;
                int iCodModelo;
                int iCodColor;
                int iCodCalidad;
                int iCodUltimoEstado;
                foreach (DataRow dr in dtInsertarPiezaInventario.Rows)
                {
                    sCodBarras = dr["cod_barras"].ToString();
                    iCodPlanta = Convert.ToInt32(dr["cod_planta"]);
                    iCodProceso = Convert.ToInt32(dr["cod_proceso"]);
                    iCodModelo = Convert.ToInt32(dr["cod_articulo"]);
                    iCodColor = Convert.ToInt32(dr["cod_color"]);
                    iCodCalidad = Convert.ToInt32(dr["cod_calidad"]);
                    iCodUltimoEstado = Convert.ToInt32(dr["cod_ultimo_estado"]);

                    proxy.InsertarPiezaInventario(sCodBarras, iCodPlanta, true, iCodProceso, true, -1, true, -1, true, -1, true,
                                                     iCodModelo, true, iCodColor, true, iCodCalidad, true, iCodUltimoEstado, true,
                                                     out iRes, out bRes);

                    if (iRes != -1)
                    {
                        this.DeleteInsertarPiezaInventario(sCodBarras);
                    }
                }

                /////////////////////////////////////////////////////

                bCompletado = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EnviarDatosInventario: " + ex.Message);
            }
            return bCompletado;
        }
        #endregion EnviarDatosInventario
        #region EnviarDatosReemplazoEtiqueta
        public bool EnviarDatosReemplazoEtiqueta()
        {
            bool bCompletado = false;
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

            try
            {
                this.EnviarConfigHandHeld(-1);
                EnviarDatosVaciado();
                /////////////////////////////////////////////////////
                int proceso = -1;
                DateTime fechaRegistro = DateTime.Now;

                int iRes = -1;
                bool bRes = true;
                int iCodPieza = 0;
                DataTable dtPiezaTransaccion = this.ObtenerReemplazoEtiquetas();
                foreach (DataRow dr in dtPiezaTransaccion.Rows)
                {
                    iRes = Convert.ToInt32(dr["cod_pieza"]);
                    proceso = Convert.ToInt32(dr["cod_proceso"]);
                    fechaRegistro = Convert.ToDateTime(dr["fecha_registro"]);
                    proxy.InsertarEtiquetaReemplazo(-1, true, iRes.ToString(), -1, true, -1, true, -1, true,
                                                    -1, true, -1, true, -1, true,
                                                    fechaRegistro, true, proceso, true, out iRes, out bRes);

                    DataTable dtPieza = oDA0.ObtenerPiezaLocal(iRes);
                    if (dtPieza != null)
                        if (dtPieza.Rows.Count > 0)
                        {
                            int iCodCalidad = Convert.ToInt32(dtPieza.Rows[0]["cod_calidad"].ToString());
                            new c11_CapturaEmpaque().ActualizarCalidadPieza(DA.eTipoConexion.Servicio,
                                                                    iRes,
                                                                    iCodCalidad);
                            int iCodColor = Convert.ToInt32(dtPieza.Rows[0]["cod_color"].ToString());
                            new c08_CapturaEsmaltado().ActualizarColorPieza(DA.eTipoConexion.Servicio,
                                                                                                iRes,
                                                                                                iCodColor);
                        }
                    EstablecerActualizacionPieza(iRes);
                    if (iRes != -1)
                    {
                        this.DeleteReemplazoEtiquetas(iCodPieza, proceso);
                    }
                }
                //------------------------------------------------------
                bCompletado = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EnviarDatosReemplazoEtiqueta: " + ex.Message);
            }
            return bCompletado;
        }
        #endregion EnviarDatosReemplazoEtiqueta

        #endregion Common

        private DataSet ProcesarBatchVaciadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtVaciadas, DataTable dtCarroPieza, DataTable dtPiezaDefecto, DataTable dtEstadoPieza, DataTable dtProcesoPieza, DataTable dtPruebaProceso)
        {
            HHsvc.SCPP_HH svcPieza = null;
            try
            {
                svcPieza = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH();
                return svcPieza.ProcesarBatchVaciadoPieza(dtPieza, dtPiezaTransaccion, dtVaciadas, dtCarroPieza, dtPiezaDefecto, dtEstadoPieza, dtProcesoPieza, dtPruebaProceso);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                svcPieza = null;
            }
        }

        private DataSet ProcesarBatchSecadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtPiezaTransaccionSecador, DataTable dtCarroPieza)
        {
            HHsvc.SCPP_HH svcPieza = null;
            try
            {
                svcPieza = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH();
                return svcPieza.ProcesarBatchSecadoPieza(dtPieza, dtPiezaTransaccion, dtPiezaTransaccionSecador, dtCarroPieza);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                svcPieza = null;
            }
        }
        #endregion methods

    }
}
