using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c00_Common
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c00_Common()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c00_Common()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        // Control
        #region query_ObtenerPeriodoActualizacion
        public static string query_ObtenerPeriodoActualizacion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ca.PeriodoActualizacion as PeriodoActualizacion ");
            queryString.Append("from	ControlActualizacion ca ");
            queryString.Append("where		ca.CodProceso = @CodProceso ");
            queryString.Append("		and	ca.CodPantalla = @CodPantalla;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPeriodoActualizacion
        #region query_ObtenerConfigBaco
        public static string query_ObtenerConfigBaco()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cb.cod_config_banco CodConfigBanco ");
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
            queryString.Append("		and m.cod_maquina = @CodMaquina ");
            queryString.Append("		and cb.cod_usuario_autoriza is not null ");
            queryString.Append("		and cb.fecha_inicio <= getdate() ");
            queryString.Append("		and cb.fecha_fin is null ");
            queryString.Append("order by	m.clave_maquina asc;");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerFechaUltimaActualizacion
        public static string query_ObtenerFechaUltimaActualizacion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	top (1) ");
            queryString.Append("		ca.FechaUltimaActualizacion as FechaUltimaActualizacion ");
            queryString.Append("from	ControlActualizacion ca ");
            queryString.Append("where		ca.CodProceso = @CodProceso ");
            queryString.Append("		and	(ca.CodPantalla = @CodPantalla or @CodPantalla = -1) ");
            queryString.Append("order by	ca.FechaUltimaActualizacion asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerFechaUltimaActualizacion
        #region query_ObtenerFechaActualizacionAnterior
        public static string query_ObtenerFechaActualizacionAnterior()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	top (1) ");
            queryString.Append("		ca.FechaActualizacionAnterior as FechaActualizacionAnterior ");
            queryString.Append("from	ControlActualizacion ca ");
            queryString.Append("where		ca.CodProceso = @CodProceso ");
            queryString.Append("		and	(ca.CodPantalla = @CodPantalla or @CodPantalla = -1) ");
            queryString.Append("order by	ca.FechaUltimaActualizacion asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerFechaUltimaActualizacion
        #region query_EstablecerFechaUltimaActualizacion
        public static string query_EstablecerFechaUltimaActualizacion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	ControlActualizacion ");
            queryString.Append("set		FechaUltimaActualizacion = @FechaUltimaActualizacion ");
            queryString.Append("where		CodProceso = @CodProceso ");
            queryString.Append("		and	CodPantalla = @CodPantalla;");
            return queryString.ToString();
        }
        #endregion query_EstablecerFechaUltimaActualizacion
        #region query_ObtenerPiezasPendientesRevision
        public static string query_ObtenerPiezasPendientesRevision()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" select top (10000) cod_pieza as CodPieza");
            /*queryString.Append(" ,CodMolde as CodModelo");
            queryString.Append(" ,cod_color as CodColor");
            queryString.Append(" ,cod_calidad as CodCalidad");
            queryString.Append(" ,cod_ultimo_proceso as CodUltimoProceso");
            queryString.Append(" ,Auditada as Auditada");
            queryString.Append(" ,cod_ultimo_estado as CodUltimoEstado");*/
            queryString.Append(" from  Pieza ");
            queryString.Append(" where	cod_ultimo_proceso = 1 and cod_pieza > 50000 order by fechamod desc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezasPendientesRevision
        #region query_ObtenerPiezasPendientesRevision
        public static string query_ObtenerCarroPiezaPendientesRevision()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" select distinct top (1000) cod_pieza as CodPieza");
            queryString.Append(" ,1 as CodModelo");
            queryString.Append(" ,1 as CodColor");
            queryString.Append(" ,1 as CodCalidad");
            queryString.Append(" ,1 as CodUltimoProceso");
            queryString.Append(" ,1 as Auditada");
            queryString.Append(" ,1 as CodUltimoEstado");
            queryString.Append(" from  Carro_Pieza ");
            queryString.Append(" where	cod_proceso = 1 and cod_pieza > 50000;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCarroPiezaPendientesRevision
        #region query_ObtenerPiezasPendientesRevision
        public static string query_ObtenerPiezaDefectoPendientesRevision()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" select distinct top (5000) cod_pieza as CodPieza");
            queryString.Append(" ,1 as CodModelo");
            queryString.Append(" ,1 as CodColor");
            queryString.Append(" ,1 as CodCalidad");
            queryString.Append(" ,1 as CodUltimoProceso");
            queryString.Append(" ,1 as Auditada");
            queryString.Append(" ,1 as CodUltimoEstado");
            queryString.Append(" from  Pieza_Defecto ");
            queryString.Append(" where	cod_proceso = 1 and cod_pieza > 50000;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaDefectoPendientesRevision
        #region EstaServicioDisponible
        public bool EstaServicioDisponible()
        {
            bool bEstaServicioDisponibleRes = false;
            bool bEstaServicioDisponible = false;

            try
            {
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                proxy.EstaServicioDisponible(out bEstaServicioDisponibleRes, out bEstaServicioDisponible);
            }
            catch (System.Net.WebException ex1)
            {
                string EM = ex1.Message;
                bEstaServicioDisponibleRes = false;
            }
            catch (Exception ex2)
            {
                string EM = ex2.Message;
                bEstaServicioDisponibleRes = false;
            }
            return bEstaServicioDisponibleRes;
        }
        #endregion EstaServicioDisponible
        #region ObtenerPeriodoActualizacion
        public int ObtenerPeriodoActualizacion(int iCodProceso, int iCodPantalla)
        {
            int iPeriodoActualizacion = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                pars[1] = new SqlCeParameter("@CodPantalla", SqlDbType.Int);
                pars[1].Value = iCodPantalla;

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPeriodoActualizacion(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    iPeriodoActualizacion = Convert.ToInt32(dtRes.Rows[0]["PeriodoActualizacion"]);
                }
                else
                {
                    iPeriodoActualizacion = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPeriodoActualizacion: " + ex.Message);
            }
            return iPeriodoActualizacion;
        }
        #endregion ObtenerPeriodoActualizacion
        #region ObtenerConfigBaco
        public int ObtenerConfigBaco(int iCodPlanta, int iCodProceso, int iCodCentroTrabajo, int iCodMaquina)
        {
            int iCodConfigBanco = -1;
            try
            {
                SqlCeParameter[] pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodCentroTrabajo", SqlDbType.Int);
                pars[2].Value = iCodCentroTrabajo;
                pars[3] = new SqlCeParameter("@CodMaquina", SqlDbType.Int);
                pars[3].Value = iCodMaquina;
                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerConfigBaco(), pars);
                if (dtRes.Rows.Count > 0)
                {
                    iCodConfigBanco = Convert.ToInt32(dtRes.Rows[0]["CodConfigBanco"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfigBaco: " + ex.Message);
            }
            return iCodConfigBanco;
        }
        #endregion

        #region ObtenerFechaUltimaActualizacion
        public DateTime ObtenerFechaUltimaActualizacion(int iCodProceso, int iCodPantalla)
        {
            DateTime dtFechaUltimaActualizacion = DateTime.MinValue;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                pars[1] = new SqlCeParameter("@CodPantalla", SqlDbType.Int);
                pars[1].Value = iCodPantalla;

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerFechaUltimaActualizacion(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    dtFechaUltimaActualizacion = Convert.ToDateTime(dtRes.Rows[0]["FechaUltimaActualizacion"]);
                }
                else
                {
                    dtFechaUltimaActualizacion = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerFechaUltimaActualizacion: " + ex.Message);
            }
            return dtFechaUltimaActualizacion;
        }
        #endregion ObtenerFechaUltimaActualizacion

        #region EstablecerFechaUltimaActualizacion
        public int EstablecerFechaUltimaActualizacion(int iCodProceso, int iCodPantalla, DateTime dtFechaUltimaActualizacion)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[3];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                pars[1] = new SqlCeParameter("@CodPantalla", SqlDbType.Int);
                pars[1].Value = iCodPantalla;
                pars[2] = new SqlCeParameter("@FechaUltimaActualizacion", SqlDbType.DateTime);
                pars[2].Value = dtFechaUltimaActualizacion;


                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_EstablecerFechaUltimaActualizacion(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EstablecerFechaUltimaActualizacion: " + ex.Message);
            }
            return iRes;
        }
        #endregion EstablecerFechaUltimaActualizacion
        #region ExisteCambioEnProcesoPantalla
        public bool ExisteCambioEnProcesoPantalla(int iCodProceso, int iCodPantalla, DateTime dtFechaUltimaActualizacion)
        {
            bool bExisteCambioEnProcesoPantallaRes = false;
            bool bExisteCambioEnProcesoPantalla = true;

            try
            {
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                proxy.ExisteCambioEnProceso(iCodProceso, true, iCodPantalla, true, dtFechaUltimaActualizacion, true, out bExisteCambioEnProcesoPantallaRes, out bExisteCambioEnProcesoPantalla);
                if (!bExisteCambioEnProcesoPantalla)
                {
                    bExisteCambioEnProcesoPantallaRes = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteCambioEnProcesoPantalla: " + ex.Message);
            }
            return bExisteCambioEnProcesoPantallaRes;
        }
        #endregion ExisteCambioEnProceso

        #region ObtenerPiezasPendientes
        public DataTable ObtenerPendientesRevision(string tabla)
        {

            DataTable dtRes = null;
            try
            {
                //// Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];


                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezasPendientesRevision(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerFechaUltimaActualizacion: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerFechaUltimaActualizacion
        // Carga Datos
        #region ObtenerPlantas
        public DataTable ObtenerPlantas()
        {
            DataTable dtRes = null;

            try
            {
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                dtRes = proxy.ObtenerPlantas();
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPlantas: " + ex.Message + "  ---- " + ex.InnerException);
            }
            return dtRes;
        }
        #endregion ObtenerPlantas

        // Varios
        #region query_ObtenerPiezasCarro
        public static string query_ObtenerPiezasCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cp.cod_pieza as CodPieza ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.cod_planta = @CodPlanta ");
            queryString.Append("		and	cp.cod_proceso = @CodProceso ");
            queryString.Append("		and	cp.cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezasCarro
        #region query_ObtenerColores
        public static string query_ObtenerColores()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	c.cod_color as CodColor, ");
            queryString.Append("		c.clave_color as ClaveColor, ");
            queryString.Append("		(c.clave_color + ' - ' + c.des_color) as DesColor ");
            queryString.Append("from	color c ");
            queryString.Append("order by	c.clave_color asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerColores
        #region query_ObtenerPruebas
        public static string query_ObtenerPruebas()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_prueba as CodPrueba, ");
            queryString.Append("		p.des_prueba as DesPrueba ");
            queryString.Append("from	prueba p ");
            queryString.Append("where		p.cod_planta = @CodPlanta ");
            queryString.Append("		and	p.cod_proceso = @CodProceso ");
            queryString.Append("order by	p.des_prueba asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPruebas
        #region query_ObtenerSigCodPieza
        public static string query_ObtenerSigCodPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	((case when max(p.cod_pieza) is null then 0 else max(p.cod_pieza) end) + 1) as CodPieza ");
            queryString.Append("from	pieza p ");
            queryString.Append("where		p.cod_pieza between 1 and 49999;");
            return queryString.ToString();
        }
        #endregion query_ObtenerSigCodPieza
        #region query_InsertarPieza
        public static string query_InsertarPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into pieza ");
            queryString.Append("(cod_planta, cod_pieza, cod_barras, cod_config_banco, cod_consecutivo, posicion, cod_articulo, cod_ultimo_proceso, cod_ultimo_estado, fecha_registro, CodMolde, IdBase) ");
            queryString.Append("values (@CodPlanta, @CodPieza, @CodBarras, @CodConfigBanco, @CodConsecutivo, @Posicion, @CodArticulo, @CodUltimoProceso, @CodUltimoEstado, @FechaRegistro, @CodMolde, @IdBase);");
            return queryString.ToString();
        }
        #endregion query_InsertarPieza
        #region query_ObtenerSigCodPiezaTransaccion
        public static string query_ObtenerSigCodPiezaTransaccion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	((case when max(pt.cod_pieza_transaccion) is null then 0 else max(pt.cod_pieza_transaccion) end) + 1) as CodPiezaTransaccion ");
            queryString.Append("from	pieza_transaccion pt ");
            queryString.Append("where		pt.cod_pieza_transaccion between 1 and 49999;");
            return queryString.ToString();
        }
        #endregion query_ObtenerSigCodPiezaTransaccion
        #region query_InsertarPiezaTransaccion
        public static string query_InsertarPiezaTransaccion()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into pieza_transaccion ");
            queryString.Append("(cod_pieza_transaccion, cod_config_handheld, cod_pieza, fecha_registro) ");
            queryString.Append("values (@CodPiezaTransaccion, @CodConfigHandheld, @CodPieza, @FechaRegistro);");
            return queryString.ToString();
        }
        #endregion query_InsertarPiezaTransaccion
        #region query_ActulizarUltimoProcesoPieza
        public static string query_ActulizarUltimoProcesoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_ultimo_proceso = @CodUltimoProceso, ");
            queryString.Append("		actualizacion = 1 ");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and	cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActulizarUltimoProcesoPieza
        #region query_EliminarCarro
        public static string query_EliminarCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	carro_pieza ");
            queryString.Append("where		cod_planta = @CodPlanta ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_EliminarCarro
        #region query_EliminarCarroTemp
        public static string query_EliminarCarroTemp()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	carro_pieza ");
            queryString.Append("set		eliminado = 1 ");
            queryString.Append("where		cod_planta = @CodPlanta ");
            queryString.Append("		and	cod_proceso = @CodProceso ");
            queryString.Append("		and	cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_EliminarCarroTemp
        #region query_ObtenerCodModeloPieza
        public static string query_ObtenerCodModeloPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_articulo as CodModelo ");
            queryString.Append("from	pieza p ");
            queryString.Append("where p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCodModeloPieza
        #region query_ObtenerModeloTipoPieza
        public static string query_ObtenerModeloTipoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_tipo_articulo as CodTipoModelo, ");
            queryString.Append("		ta.clave_tipo_articulo as ClaveTipoModelo, ");
            queryString.Append("		(ta.clave_tipo_articulo + ' - ' + ta.des_tipo_articulo) as DesTipoModelo, ");
            queryString.Append("		a.cod_articulo as CodModelo, ");
            queryString.Append("		a.clave_articulo as ClaveModelo, ");
            queryString.Append("		(a.clave_articulo + ' - ' + a.des_articulo) as DesModelo ");
            queryString.Append("from	articulo a, ");
            queryString.Append("		tipo_articulo ta ");
            queryString.Append("where		a.cod_tipo_articulo = ta.cod_tipo_articulo ");
            queryString.Append("		and	a.cod_articulo = @CodArticulo;");
            return queryString.ToString();
        }
        #endregion query_ObtenerModeloTipoPieza
        #region query_ObtenerCalidadPieza
        public static string query_ObtenerCalidadPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_calidad as CodCalidad, ");
            queryString.Append("		c.clave_calidad as ClaveCalidad, ");
            queryString.Append("		(c.clave_calidad + ' - ' + c.des_calidad) as DesCalidad ");
            queryString.Append("from	pieza p, ");
            queryString.Append("		calidad c ");
            queryString.Append("where		p.cod_calidad = c.cod_calidad ");
            queryString.Append("		and	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCalidadPieza
        #region query_ObtenerCalidades
        public static string query_ObtenerCalidades()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	c.cod_calidad as CodCalidad, ");
            queryString.Append("		c.clave_calidad as ClaveCalidad, ");
            queryString.Append("		(c.clave_calidad + ' - ' + c.des_calidad) as DesCalidad ");
            queryString.Append("from	calidad c ");
            queryString.Append("order by	c.clave_calidad asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCalidades
        #region query_ExisteModelo
        public static string query_ExisteModelo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodModelo ");
            queryString.Append("from	articulo a ");
            queryString.Append("where		a.cod_molde is null ");
            queryString.Append("		and	a.clave_articulo = @ClaveModelo;");
            return queryString.ToString();
        }
        #endregion query_ExisteModelo
        #region query_ObtenerDesProceso
        public static string query_ObtenerDesProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.des_proceso as DesProceso ");
            queryString.Append("from	proceso p ");
            queryString.Append("where		p.cod_proceso = @CodProceso;");
            return queryString.ToString();
        }
        #endregion query_ObtenerDesProceso
        #region query_ObtenerTiposModelo
        public static string query_ObtenerTiposModelo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ta.cod_tipo_articulo as CodTipoModelo, ");
            queryString.Append("		ta.clave_tipo_articulo as ClaveTipoModelo, ");
            queryString.Append("		(ta.clave_tipo_articulo + ' - ' + ta.des_tipo_articulo) as DesTipoModelo ");
            queryString.Append("from	tipo_articulo ta ");
            queryString.Append("order by	ta.clave_tipo_articulo asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerTiposModelo
        #region query_ObtenerModelos
        public static string query_ObtenerModelos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodModelo, ");
            queryString.Append("		a.clave_articulo as ClaveModelo, ");
            queryString.Append("		(a.clave_articulo + ' - ' + a.des_articulo) as DesModelo ");
            queryString.Append("from	articulo a ");
            queryString.Append("where		a.cod_molde is not null ");
            queryString.Append("		and	a.cod_tipo_articulo = @CodTipoModelo ");
            queryString.Append("order by	a.clave_articulo asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerModelos
        #region query_ExisteModeloHastaRevisado
        public static string query_ExisteModeloHastaRevisado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodModelo ");
            queryString.Append("from	articulo a ");
            queryString.Append("where		a.fecha_baja is null ");
            queryString.Append("		and	a.cod_molde is not null ");
            queryString.Append("		and	a.clave_articulo = @ClaveModelo;");
            return queryString.ToString();
        }
        #endregion query_ExisteModeloHastaRevisado
        #region query_ExisteModeloDesdeEsmaltado
        public static string query_ExisteModeloDesdeEsmaltado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_articulo as CodModelo ");
            queryString.Append("from	articulo a ");
            queryString.Append("where		a.fecha_baja is null ");
            queryString.Append("		and	a.cod_molde is null ");
            queryString.Append("		and	a.clave_articulo = @ClaveModelo;");
            return queryString.ToString();
        }
        #endregion query_ExisteModeloDesdeEsmaltado
        #region query_ObtenerProcesos
        public static string query_ObtenerProcesos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_proceso as CodProceso, ");
            queryString.Append("		p.des_proceso as DesProceso ");
            queryString.Append("from	proceso p ");
            queryString.Append("where		p.cod_proceso not in (0) ");
            queryString.Append("order by	p.cod_proceso asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerProcesos
        #region query_ObtenerPiezasTarima
        public static string query_ObtenerPiezasTarima()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	tp.CodPieza as CodPieza, ");
            queryString.Append("		p.Auditada as Auditada, ");
            queryString.Append("		tp.Paletizado as Paletizado ");
            queryString.Append("from	TarimaPieza tp, Pieza p ");
            queryString.Append("where   tp.CodPieza = p.cod_pieza ");
            queryString.Append("        AND tp.CodTarima = @CodTarima;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezasTarima
        #region query_ObtenerPiezaLocal
        public static string query_ObtenerPiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza, ");
            queryString.Append("		p.cod_barras, ");
            queryString.Append("		p.cod_config_banco, ");
            queryString.Append("		p.cod_consecutivo, ");
            queryString.Append("		p.posicion, ");
            queryString.Append("		p.cod_articulo, ");
            queryString.Append("		p.cod_color, ");
            queryString.Append("		p.cod_calidad, ");
            queryString.Append("		p.cod_planta, ");
            queryString.Append("		p.cod_ultimo_proceso, ");
            queryString.Append("		p.cod_ultimo_estado, ");
            queryString.Append("		p.Auditada, ");
            queryString.Append("		p.fecha_registro, ");
            queryString.Append("		p.actualizacion, ");
            queryString.Append("		p.reemplazo_etiqueta, ");
            queryString.Append("		p.modificado_estado ");
            queryString.Append("from	pieza p ");
            queryString.Append("where	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaLocal
        #region query_EstaEnInventarioPocesoPieza
        public static string query_EstaEnInventarioPocesoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.IdInventarioProceso as CodPieza ");
            queryString.Append("from	InventarioProcesoPieza p ");
            queryString.Append("where		p.cod_barras = @CodBarras;");
            return queryString.ToString();
        }
        #endregion query_EstaEnInventarioPocesoPieza

        #region query_ActualizarColorPieza
        public static string query_ActualizarColorPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		cod_color = @CodColor ");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarColorPieza

        #region query_ObtenerMoldes
        public static string query_ObtenerMoldes()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select 	ClaveMolde, ");
            queryString.Append("		Descripcion ");
            queryString.Append("from	MoldeMaquina;");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerBase
        public static string query_ObtenerBase()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	IdBase, ");
            queryString.Append("		ClaveBase ");
            queryString.Append("FROM    Base ");
            queryString.Append("where	ClaveMolde = @ClaveMolde;");
            return queryString.ToString();
        }
        #endregion
        #region query_BorrarInfoTablaTransaccional
        public static string query_BorrarInfoTablaTransaccional(string sTabla)
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete from  " + sTabla + "  where FechaMod < @FechaBorrar;");
            return queryString.ToString();
        }
        #endregion query_BorrarInfoTablaTransaccional

        #region query_PruebaProcesoDel
        public static string query_PruebaProcesoDel()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("DELETE ");
            queryString.Append("FROM	PruebaProceso ");
            queryString.Append("WHERE 	Codigo = @Codigo ");
            return queryString.ToString();
        }
        #endregion
        #region query_PruebaProcesoIns
        public static string query_PruebaProcesoIns()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("INSERT INTO PruebaProceso");
            queryString.Append("(CodPlanta, CodPrueba, CodProceso, CodPieza, FechaRegistro) ");
            queryString.Append("VALUES ");
            queryString.Append("(@CodPlanta, @CodPrueba, @CodProceso, @CodPieza, @FechaRegistro);");
            return queryString.ToString();
        }
        #endregion
        #region query_PruebaProcesoSel
        public static string query_PruebaProcesoSel()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT 	Codigo, ");
            queryString.Append("		CodPlanta, ");
            queryString.Append("		CodPrueba, ");
            queryString.Append("		CodProceso, ");
            queryString.Append("		CodPieza, ");
            queryString.Append("		FechaRegistro ");
            queryString.Append("FROM	PruebaProceso;");
            return queryString.ToString();
        }
        #endregion
        #region PruebaProcesoDel
        public bool PruebaProcesoDel(int iCodigo)
        {
            bool bRes = false;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@Codigo", SqlDbType.Int);
                pars[0].Value = iCodigo;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_PruebaProcesoDel(), pars);
                bRes = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarPruebaProceso: " + ex.Message);
            }
            return bRes;
        }
        #endregion
        #region PruebaProcesoIns
        public bool PruebaProcesoIns(int iCodPlanta, int iCodPrueba, int iCodProceso, int iCodPieza, DateTime dtFechaRegistro)
        {
            bool bRes = false;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[5];
                pars[0] = new SqlCeParameter("@CodPrueba", SqlDbType.Int);
                pars[0].Value = iCodPrueba;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[2].Value = iCodPieza;
                pars[3] = new SqlCeParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[3].Value = dtFechaRegistro;
                pars[4] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[4].Value = iCodPlanta;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_PruebaProcesoIns(), pars);
                bRes = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarPruebaProceso: " + ex.Message);
            }
            return bRes;
        }
        #endregion
        #region PruebaProcesoSel
        public DataTable PruebaProcesoSel()
        {
            DataTable dtRes = null;

            try
            {
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_PruebaProcesoSel(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", PruebaProcesoSel: " + ex.Message);
            }
            return dtRes;
        }
        #endregion

        #region BorrarInfoTablaTransaccional
        public static string query_TieneColumnaFechaModificacion(string sTabla)
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select * from  " + sTabla + "  where 1 = 2;");
            return queryString.ToString();
        }
        private bool TieneColumnaFechaModificacion(string sNombreTabla)
        {
            bool bRes = false;
            DataTable dt;
            try
            {
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dt = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_TieneColumnaFechaModificacion(sNombreTabla), pars);
                if (dt != null)
                {
                    if (dt.Columns.Contains("FechaMod"))
                        bRes = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", TieneColumnaFechaModificacion: " + ex.Message);
            }
            return bRes;
        }
        public bool BorrarInfoTablaTransaccional(string tabla, DateTime dtFechaBorrar)
        {
            bool bRes = false;
            if (TieneColumnaFechaModificacion(tabla))
            {
                try
                {
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@FechaBorrar", SqlDbType.DateTime);
                    pars[0].Value = dtFechaBorrar;
                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_BorrarInfoTablaTransaccional(tabla), pars);
                    bRes = true;
                }
                catch (Exception ex)
                {
                    throw new Exception(this.sClassName + ", BorrarInfoTablaTransaccional: " + ex.Message);
                }
            }
            return bRes;
        }
        #endregion
        #region ObtenerFechaDepuracionHistoria
        public DateTime ObtenerFechaDepuracionHistoria()
        {
            bool bRes = false;
            DateTime dtRes = DateTime.Now.AddMonths(-2);

            try
            {

                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerFechaDepuracionHistoria(out dtRes, out bRes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarCarro: " + ex.Message);
            }
            return dtRes;
        }
        public DateTime ObtenerFechaDepuracionHistoria(int iCodigoPlanta, int iCodigoProceso)
        {
            bool bRes = false;
            DateTime dtRes = DateTime.Now.AddMonths(-2);

            try
            {

                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerFechaDepuracionHistoria2(iCodigoPlanta, true, iCodigoProceso, true, out dtRes, out bRes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarCarro: " + ex.Message);
            }
            return dtRes;
        }
        #endregion

        #region ObtenerMoldes
        public DataTable ObtenerMoldes()
        {
            DataTable dtRes = null;

            try
            {
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerMoldes(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerMoldes: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region ObtenerBase
        public DataTable ObtenerBase(int iMolde)
        {
            DataTable dtRes = null;

            try
            {
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@ClaveMolde", SqlDbType.Int);
                pars[0].Value = iMolde;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerBase(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerBase: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region ObtenerPiezasCarro
        public DataTable ObtenerPiezasCarro(int iCodPlanta, int iCodProceso, int iCodCarro, bool bForzarOffine)
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
                    pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                    pars[2].Value = iCodCarro;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezasCarro(), pars);
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ObtenerPiezasCarro(iCodPlanta, true, iCodProceso, true, iCodCarro, true);

                        HHsvc.HHPieza elemento = null;
                        DataRow dr = null;
                        dtRes = new DataTable();
                        dtRes.Columns.Add("CodPieza", typeof(int));
                        foreach (HHsvc.BSE e in res)
                        {
                            elemento = (HHsvc.HHPieza)e;
                            dr = dtRes.NewRow();
                            dr["CodPieza"] = elemento.CodPieza;
                            dtRes.Rows.Add(dr);
                        }
                        dtRes.AcceptChanges();
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[3];
                        pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                        pars[0].Value = iCodPlanta;
                        pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                        pars[1].Value = iCodProceso;
                        pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                        pars[2].Value = iCodCarro;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezasCarro(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasCarro: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezasCarro
        #region ObtenerColores
        public DataTable ObtenerColores()
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerColores();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerColores(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerColores: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerColores
        #region ObtenerPruebas
        public DataTable ObtenerPruebas(int iCodPlanta, int iCodProceso)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPruebas(iCodPlanta, true, iCodProceso, true);
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
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPruebas(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPruebas: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPruebas
        #region ObtenerSigCodPieza
        public int ObtenerSigCodPieza()
        {
            int iCodPieza = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerSigCodPieza(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    iCodPieza = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);
                }
                else
                {
                    iCodPieza = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSigCodPieza: " + ex.Message);
            }
            return iCodPieza;
        }
        #endregion ObtenerSigCodPieza
        #region InsertarPieza
        public int InsertarPieza(DA.eTipoConexion tc, int iCodPlanta, string sCodBarras, int iCodConfigBanco, int iCodConsecutivo,
                                    int iPosicion, int iCodArticulo, int iCodUltimoProceso, int iCodUltimoEstado, DateTime dtFechaRegistro, int iCodMolde, int iIdBase)
        {
            int iCodPieza = -1;
            bool bCodPieza = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    iCodPieza = this.ObtenerSigCodPieza();

                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[12];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[1].Value = iCodPieza;
                    pars[2] = new SqlCeParameter("@CodBarras", SqlDbType.NVarChar, 15);
                    pars[2].Value = sCodBarras;
                    pars[3] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                    pars[3].Value = iCodConfigBanco;
                    pars[4] = new SqlCeParameter("@CodConsecutivo", SqlDbType.Int);
                    pars[4].Value = iCodConsecutivo;
                    pars[5] = new SqlCeParameter("@Posicion", SqlDbType.Int);
                    pars[5].Value = iPosicion;
                    pars[6] = new SqlCeParameter("@CodArticulo", SqlDbType.Int);
                    pars[6].Value = iCodArticulo;
                    pars[7] = new SqlCeParameter("@CodUltimoProceso", SqlDbType.Int);
                    pars[7].Value = iCodUltimoProceso;
                    pars[8] = new SqlCeParameter("@CodUltimoEstado", SqlDbType.Int);
                    pars[8].Value = iCodUltimoEstado;
                    pars[9] = new SqlCeParameter("@CodMolde", SqlDbType.Int);
                    pars[9].Value = iCodMolde;
                    pars[10] = new SqlCeParameter("@IdBase", SqlDbType.Int);
                    pars[10].Value = iIdBase;
                    pars[11] = new SqlCeParameter("@FechaRegistro", SqlDbType.DateTime);
                    pars[11].Value = dtFechaRegistro;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_InsertarPieza(), pars);
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();

                        proxy.InsertarPieza(iCodPlanta, true, sCodBarras, iCodConfigBanco, true, iCodConsecutivo, true,
                                                iPosicion, true, iCodArticulo, true, iCodUltimoProceso, true, iCodUltimoEstado, true,
                                                dtFechaRegistro, true, iCodMolde, true, iIdBase, true, out iCodPieza, out bCodPieza);
                        if (!bCodPieza)
                        {
                            iCodPieza = -1;
                        }
                    }
                    else
                    {
                        iCodPieza = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarPieza: " + ex.Message);
            }
            return iCodPieza;
        }
        #endregion InsertarPieza
        #region ObtenerSigCodPiezaTransaccion
        public long ObtenerSigCodPiezaTransaccion()
        {
            long lCodPiezaTransaccion = -1;

            try
            {

                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerSigCodPiezaTransaccion(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    lCodPiezaTransaccion = Convert.ToInt64(dtRes.Rows[0]["CodPiezaTransaccion"]);
                }
                else
                {
                    lCodPiezaTransaccion = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSigCodPiezaTransaccion: " + ex.Message);
            }
            return lCodPiezaTransaccion;
        }
        #endregion ObtenerSigCodPiezaTransaccion
        #region InsertarPiezaTransaccion
        public long InsertarPiezaTransaccion(DA.eTipoConexion tc, long lCodConfigHandheld, int iCodPieza, DateTime dtFecha)
        {
            long lCodPiezaTransaccion = -1;
            bool bCodPiezaTransaccion = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    lCodPiezaTransaccion = this.ObtenerSigCodPiezaTransaccion();

                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    pars[0] = new SqlCeParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                    pars[0].Value = lCodPiezaTransaccion;
                    pars[1] = new SqlCeParameter("@CodConfigHandheld", SqlDbType.BigInt);
                    pars[1].Value = lCodConfigHandheld;
                    pars[2] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[2].Value = iCodPieza;
                    pars[3] = new SqlCeParameter("@FechaRegistro", SqlDbType.DateTime);
                    pars[3].Value = dtFecha;
                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_InsertarPiezaTransaccion(), pars);
                }
                else if (tc == DA.eTipoConexion.Servicio)
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarPiezaTransaccion(lCodConfigHandheld, true, iCodPieza, true, dtFecha, true,
                                                        out lCodPiezaTransaccion, out bCodPiezaTransaccion);
                        if (!bCodPiezaTransaccion)
                        {
                            lCodPiezaTransaccion = -1;
                        }
                    }
                    else
                    {
                        lCodPiezaTransaccion = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarPiezaTransaccion: " + ex.Message);
            }
            return lCodPiezaTransaccion;
        }
        #endregion InsertarPiezaTransaccion
        #region ActulizarUltimoProcesoPieza
        public int ActulizarUltimoProcesoPieza(DA.eTipoConexion tc, int iCodPieza, int iCodUltimoProceso)
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
                    pars[1] = new SqlCeParameter("@CodUltimoProceso", SqlDbType.Int);
                    pars[1].Value = iCodUltimoProceso;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_ActulizarUltimoProcesoPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActulizarUltimoProcesoPieza(iCodPieza, true, iCodUltimoProceso, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", ActulizarUltimoProcesoPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActulizarUltimoProcesoPieza

        #region ActualizarColorPieza
        public int ActualizarColorPieza(DA.eTipoConexion tc, int iCodPieza, int iCodColor)
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
                    pars[1] = new SqlCeParameter("@CodColor", SqlDbType.Int);
                    pars[1].Value = iCodColor;
                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_ActualizarColorPieza(), pars);
                    iRes = 0;
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarColorPieza(iCodPieza, true, iCodColor, true, out iRes, out bRes);
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
                throw new Exception(this.sClassName + ", ActualizarColorPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarColorPieza

        #region EliminarCarro
        public int EliminarCarro(DA.eTipoConexion tc, int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[3];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                    pars[2].Value = iCodCarro;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_EliminarCarro(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.EliminarCarro(iCodPlanta, true, iCodProceso, true, iCodCarro, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", EliminarCarro: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarCarro
        #region EliminarCarroTemp
        public int EliminarCarroTemp(int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[3];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = iCodCarro;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_EliminarCarroTemp(), pars);

                iRes = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarCarroTemp: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarCarroTemp
        #region ObtenerCodModeloPieza
        public int ObtenerCodModeloPieza(int iCodPieza)
        {
            int iCodModelo = -1;
            bool bCodModelo = false;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerCodModeloPieza(iCodPieza, true, out iCodModelo, out bCodModelo);

                    if (!bCodModelo)
                    {
                        iCodModelo = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCodModeloPieza(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodModelo = Convert.ToInt32(dtRes.Rows[0]["CodModelo"]);
                    }
                    else
                    {
                        iCodModelo = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodModeloPieza: " + ex.Message);
            }
            return iCodModelo;
        }
        #endregion ObtenerCodModeloPieza
        #region ObtenerModeloTipoPieza
        public DataTable ObtenerModeloTipoPieza(int iCodArticulo)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerModeloTipoPieza(iCodArticulo, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodArticulo", SqlDbType.Int);
                    pars[0].Value = iCodArticulo;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerModeloTipoPieza(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModeloTipoPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerModeloTipoPieza
        #region ObtenerCalidadPieza
        public DataTable ObtenerCalidadPieza(int iCodPieza)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    HHsvc.BSE[] res = proxy.ObtenerCalidadPieza(iCodPieza, true);

                    HHsvc.HHCalidad elemento = null;
                    DataRow dr = null;
                    dtRes = new DataTable();
                    dtRes.Columns.Add("CodCalidad", typeof(int));
                    dtRes.Columns.Add("ClaveCalidad", typeof(string));
                    dtRes.Columns.Add("DesCalidad", typeof(string));
                    foreach (HHsvc.BSE e in res)
                    {
                        elemento = (HHsvc.HHCalidad)e;
                        dr = dtRes.NewRow();
                        dr["CodCalidad"] = elemento.CodCalidad;
                        dr["ClaveCalidad"] = elemento.ClaveCalidad;
                        dr["DesCalidad"] = elemento.DesCalidad;
                        dtRes.Rows.Add(dr);
                    }
                    dtRes.AcceptChanges();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCalidadPieza(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCalidadPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCalidadPieza
        #region ObtenerCalidades
        public DataTable ObtenerCalidades()
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerCalidades();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCalidades(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCalidades: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCalidades
        #region ExisteModelo
        public int ExisteModelo(string sClaveArticulo)
        {
            int iCodModelo = -1;
            bool bCodModelo = false;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExisteModelo(sClaveArticulo, out iCodModelo, out bCodModelo);

                    if (!bCodModelo)
                    {
                        iCodModelo = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@ClaveArticulo", SqlDbType.NVarChar, 10);
                    pars[0].Value = sClaveArticulo;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ExisteModelo(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodModelo = Convert.ToInt32(dtRes.Rows[0]["CodModelo"]);
                    }
                    else
                    {
                        iCodModelo = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteModelo: " + ex.Message);
            }
            return iCodModelo;
        }
        #endregion ExisteModelo
        #region ObtenerDesProceso
        public string ObtenerDesProceso(int iCodProceso)
        {
            string sDesProceso = string.Empty;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    sDesProceso = proxy.ObtenerDesProceso(iCodProceso, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[0].Value = iCodProceso;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerDesProceso(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        sDesProceso = Convert.ToString(dtRes.Rows[0]["DesProceso"]);
                    }
                    else
                    {
                        sDesProceso = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDesProceso: " + ex.Message);
            }
            return sDesProceso;
        }
        #endregion ObtenerDesProceso
        #region ObtenerTiposModelo
        public DataTable ObtenerTiposModelo()
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerTiposModelo();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerTiposModelo(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTiposModelo: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerTiposModelo
        #region ObtenerModelos
        public DataTable ObtenerModelos(int iCodTipoModelo)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerModelos(iCodTipoModelo, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodTipoModelo", SqlDbType.Int);
                    pars[0].Value = iCodTipoModelo;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerModelos(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModelos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerModelos
        #region ExisteModeloHastaRevisado
        public int ExisteModeloHastaRevisado(string sClaveModelo)
        {
            int iCodModelo = -1;
            bool bCodModelo = false;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExisteModeloHastaRevisado(sClaveModelo, out iCodModelo, out bCodModelo);

                    if (!bCodModelo)
                    {
                        iCodModelo = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@ClaveModelo", SqlDbType.NVarChar, 10);
                    pars[0].Value = sClaveModelo;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ExisteModeloHastaRevisado(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodModelo = Convert.ToInt32(dtRes.Rows[0]["CodModelo"]);
                    }
                    else
                    {
                        iCodModelo = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteModeloHastaRevisado: " + ex.Message);
            }
            return iCodModelo;
        }
        #endregion ExisteModeloHastaRevisado
        #region ExisteModeloDesdeEsmaltado
        public int ExisteModeloDesdeEsmaltado(string sClaveModelo)
        {
            int iCodModelo = -1;
            bool bCodModelo = false;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExisteModeloDesdeEsmaltado(sClaveModelo, out iCodModelo, out bCodModelo);

                    if (!bCodModelo)
                    {
                        iCodModelo = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@ClaveModelo", SqlDbType.NVarChar, 10);
                    pars[0].Value = sClaveModelo;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ExisteModeloDesdeEsmaltado(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodModelo = Convert.ToInt32(dtRes.Rows[0]["CodModelo"]);
                    }
                    else
                    {
                        iCodModelo = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteModeloDesdeEsmaltado: " + ex.Message);
            }
            return iCodModelo;
        }
        #endregion ExisteModeloDesdeEsmaltado
        #region ObtenerProcesos
        public DataTable ObtenerProcesos()
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerProcesos();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[0];

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerProcesos(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerProcesos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerProcesos
        #region ObtenerPiezasTarima
        public DataTable ObtenerPiezasTarima(int iCodTarima, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                    pars[0].Value = iCodTarima;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezasTarima(), pars);
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ObtenerPiezasTarima(iCodTarima, true);

                        HHsvc.HHTarimaPieza elemento = null;
                        DataRow dr = null;
                        dtRes = new DataTable();
                        dtRes.Columns.Add("CodPieza", typeof(int));
                        dtRes.Columns.Add("Auditada", typeof(bool));
                        dtRes.Columns.Add("Paletizado", typeof(bool));
                        foreach (HHsvc.BSE e in res)
                        {
                            elemento = (HHsvc.HHTarimaPieza)e;
                            dr = dtRes.NewRow();
                            dr["CodPieza"] = elemento.CodPieza;
                            dr["Auditada"] = elemento.Auditada;
                            dr["Paletizado"] = elemento.Paletizado;
                            dtRes.Rows.Add(dr);
                        }
                        dtRes.AcceptChanges();
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                        pars[0].Value = iCodTarima;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezasTarima(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasTarima: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezasTarima
        #region ObtenerPiezaLocal
        public DataTable ObtenerPiezaLocal(int iCodPieza)
        {
            DataTable dtRes = null;

            try
            {

                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerPiezaLocal(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerColorPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezaLocal
        #region EstaEnInventarioPocesoPieza
        public int EstaEnInventarioPocesoPieza(string sCodBarras)
        {
            int iCodPieza = -1;
            bool bCodPieza = false;

            try
            {

                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.EstaEnInventarioPocesoPieza(sCodBarras, out iCodPieza, out bCodPieza);
                    if (!bCodPieza)
                    {
                        iCodPieza = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodBarras", SqlDbType.NVarChar, 15);
                    pars[0].Value = sCodBarras;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_EstaEnInventarioPocesoPieza(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodPieza = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);
                    }
                    else
                    {
                        iCodPieza = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodPiezaCodBarras: " + ex.Message);
            }
            return iCodPieza;
        }
        #endregion EstaEnInventarioPocesoPieza

        #region ObtenerCodProcesoVaciado
        public int ObtenerCodProcesoVaciado()
        {
            return (int)1;
        }
        #endregion ObtenerCodProcesoVaciado
        #region ObtenerCodProcesoSecado
        public int ObtenerCodProcesoSecado()
        {
            return (int)2;
        }
        #endregion ObtenerCodProcesoSecado
        #region ObtenerCodProcesoRevisado
        public int ObtenerCodProcesoRevisado()
        {
            return (int)3;
        }
        #endregion ObtenerCodProcesoRevisado
        #region ObtenerCodProcesoEsmaltado
        public int ObtenerCodProcesoEsmaltado()
        {
            return (int)4;
        }
        #endregion ObtenerCodProcesoEsmaltado
        #region ObtenerCodProcesoHornos
        public int ObtenerCodProcesoHornos()
        {
            return (int)5;
        }
        #endregion ObtenerCodProcesoHornos
        #region ObtenerCodProcesoClasificado
        public int ObtenerCodProcesoClasificado()
        {
            return (int)6;
        }
        #endregion ObtenerCodProcesoClasificado
        #region ObtenerCodProcesoEmpaque
        public int ObtenerCodProcesoEmpaque()
        {
            return (int)7;
        }
        #endregion ObtenerCodProcesoEmpaque
        #region ObtenerCodProcesoAuditoria
        public int ObtenerCodProcesoAuditoria()
        {
            return (int)8;
        }
        #endregion ObtenerCodProcesoAuditoria
        #region ObtenerCodProcesoInventario
        public int ObtenerCodProcesoInventario()
        {
            return (int)9;
        }
        #endregion ObtenerCodProcesoInventario


        #region query_ObtenerModeloTipoPieza2
        public static string query_ObtenerModeloTipoPieza2()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	a.cod_tipo_articulo as CodTipoModelo, ");
            queryString.Append("		ta.clave_tipo_articulo as ClaveTipoModelo, ");
            queryString.Append("		(ta.clave_tipo_articulo + ' - ' + ta.des_tipo_articulo) as DesTipoModelo, ");
            queryString.Append("		a.cod_articulo as CodModelo, ");
            queryString.Append("		a.clave_articulo as ClaveModelo, ");
            queryString.Append("		(a.clave_articulo + ' - ' + a.des_articulo) as DesModelo ");
            queryString.Append("from	pieza p, ");
            queryString.Append("		articulo a, ");
            queryString.Append("		tipo_articulo ta ");
            queryString.Append("where		p.cod_articulo = a.cod_articulo ");
            queryString.Append("		and	a.cod_tipo_articulo = ta.cod_tipo_articulo ");
            queryString.Append("		and	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerModeloTipoPieza2
        #region query_ObtenerColorPieza
        public static string query_ObtenerColorPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_color as CodColor, ");
            queryString.Append("		c.clave_color as ClaveColor, ");
            queryString.Append("		(c.clave_color + ' - ' + c.des_color) as DesColor ");
            queryString.Append("from	pieza p, ");
            queryString.Append("		color c ");
            queryString.Append("where		p.cod_color = c.cod_color ");
            queryString.Append("		and	p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerColorPieza
        #region query_ObtenerCodPiezaCodBarras
        public static string query_ObtenerCodPiezaCodBarras()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza as CodPieza ");
            queryString.Append("from	pieza p ");
            queryString.Append("where		p.cod_barras = @CodBarras;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCodPiezaCodBarras
        #region query_ObtenerEstadoPieza
        public static string query_ObtenerEstadoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_ultimo_estado as CodEstadoPieza, ");
            queryString.Append("		ed.des_estado_defecto as DesEstadoPieza ");
            queryString.Append("from	pieza p, ");
            queryString.Append("		estado_defecto ed ");
            queryString.Append("where	    p.cod_ultimo_estado = ed.cod_estado_defecto ");
            queryString.Append("		and	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerEstadoPieza
        #region query_ObtenerUltimoProcesoPieza
        public static string query_ObtenerUltimoProcesoPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_ultimo_proceso as CodProceso, ");
            queryString.Append("		CASE ISNULL(pr.des_proceso) WHEN 1 THEN '' ELSE pr.des_proceso END AS DesProceso ");
            queryString.Append("from	pieza p left outer join ");
            queryString.Append("		proceso pr ");
            queryString.Append("		on p.cod_ultimo_proceso = pr.cod_proceso ");
            queryString.Append("where	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ObtenerUltimoProcesoPieza
        #region ObtenerCalidadCasificado
        public static string query_ObtenerCalidadCasificado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select p.Calidad ");
            queryString.Append("from	pieza p ");
            queryString.Append("where	p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion
        #region query_ExistePiezaLocal
        public static string query_ExistePiezaLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_pieza as CodPieza ");
            queryString.Append("from	pieza p ");
            queryString.Append("where	p.modificado_estado > -1 ");
            queryString.Append(" and p.cod_planta > -1 ");
            queryString.Append(" and p.cod_ultimo_proceso > -1 ");
            queryString.Append(" and p.cod_ultimo_estado > -1 ");
            queryString.Append(" and p.cod_articulo > -1 ");
            queryString.Append(" and p.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion
        #region ExistePiezaLocal
        public bool ExistePiezaLocal(int iCodPieza)
        {
            bool bRes = false;
            try
            {
                // Parameters
                SqlCeParameter[] pars = pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ExistePiezaLocal(), pars);

                if (dtRes.Rows.Count > 0)
                {
                    bRes = true;
                }
            }
            catch (Exception e) { }
            return bRes;
        }
        #endregion
        #region ObtenerModeloTipoPieza2
        public DataTable ObtenerModeloTipoPieza2(int iCodPieza)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerModeloTipoPieza2(iCodPieza, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerModeloTipoPieza2(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModeloTipoPieza2: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerModeloTipoPieza2
        #region ObtenerColorPieza
        public DataTable ObtenerColorPieza(int iCodPieza)
        {
            DataTable dtRes = null;

            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    HHsvc.BSE[] res = proxy.ObtenerColorPieza(iCodPieza, true);

                    HHsvc.HHColor elemento = null;
                    DataRow dr = null;
                    dtRes = new DataTable();
                    dtRes.Columns.Add("CodColor", typeof(int));
                    dtRes.Columns.Add("ClaveColor", typeof(string));
                    dtRes.Columns.Add("DesColor", typeof(string));
                    foreach (HHsvc.BSE e in res)
                    {
                        elemento = (HHsvc.HHColor)e;
                        dr = dtRes.NewRow();
                        dr["CodColor"] = elemento.CodColor;
                        dr["ClaveColor"] = elemento.ClaveColor;
                        dr["DesColor"] = elemento.DesColor;
                        dtRes.Rows.Add(dr);
                    }
                    dtRes.AcceptChanges();
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerColorPieza(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerColorPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerColorPieza
        #region ObtenerCodPiezaCodBarras
        public int ObtenerCodPiezaCodBarras(string sCodBarras, bool bForzarOffine)
        {
            int iCodPieza = -1;
            bool bCodPieza = false;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodBarras", SqlDbType.NVarChar, 15);
                    pars[0].Value = sCodBarras;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCodPiezaCodBarras(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodPieza = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);
                    }
                    else
                    {
                        iCodPieza = -1;
                    }
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ObtenerCodPiezaCodBarras(sCodBarras, out iCodPieza, out bCodPieza);
                        if (!bCodPieza)
                        {
                            iCodPieza = -1;
                        }
                        if (iCodPieza != -1)
                        {

                            this.ValidarExistenciaLocal(iCodPieza);
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodBarras", SqlDbType.NVarChar, 15);
                        pars[0].Value = sCodBarras;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCodPiezaCodBarras(), pars);

                        if (dtRes.Rows.Count > 0)
                        {
                            iCodPieza = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);
                        }
                        else
                        {
                            iCodPieza = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodPiezaCodBarras: " + ex.Message);
            }
            return iCodPieza;
        }
        #endregion ObtenerCodPiezaCodBarras
        #region ObtenerEstadoPieza
        public DataTable ObtenerEstadoPieza(int iCodPieza, bool bForzarOffine)
        {
            DataTable dtRes = null;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerEstadoPieza(), pars);
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ObtenerEstadoPieza(iCodPieza, true);

                        HHsvc.HHEstadoPieza elemento = null;
                        DataRow dr = null;
                        dtRes = new DataTable();
                        dtRes.Columns.Add("CodEstadoPieza", typeof(int));
                        dtRes.Columns.Add("DesEstadoPieza", typeof(string));
                        foreach (HHsvc.BSE e in res)
                        {
                            elemento = (HHsvc.HHEstadoPieza)e;
                            dr = dtRes.NewRow();
                            dr["CodEstadoPieza"] = elemento.CodEstadoPieza;
                            dr["DesEstadoPieza"] = elemento.DesEstadoPieza;
                            dtRes.Rows.Add(dr);
                        }
                        dtRes.AcceptChanges();
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                        pars[0].Value = iCodPieza;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerEstadoPieza(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerEstadoPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerEstadoPieza
        #region ObtenerUltimoProcesoPieza
        public DataTable ObtenerUltimoProcesoPieza(int iCodPieza, bool bForzarOffine)
        {
            DataTable dtRes = new DataTable();

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;
                    dtRes.Rows.Clear();
                    dtRes.Columns.Clear();
                    DataSet ds = new DataSet();
                    ds.EnforceConstraints = false;
                    ds.Tables.Add(DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerUltimoProcesoPieza(), pars));
                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerUltimoProcesoPieza(), pars);
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ObtenerUltimoProcesoPieza(iCodPieza, true);

                        HHsvc.HHProceso elemento = null;
                        DataRow dr = null;
                        dtRes = new DataTable();
                        dtRes.Columns.Add("CodProceso", typeof(int));
                        dtRes.Columns.Add("DesProceso", typeof(string));
                        dtRes.Columns.Add("Calidad", typeof(string));
                        foreach (HHsvc.BSE e in res)
                        {
                            elemento = (HHsvc.HHProceso)e;
                            dr = dtRes.NewRow();
                            dr["CodProceso"] = elemento.CodProceso;
                            dr["DesProceso"] = elemento.DesProceso;
                            dr["Calidad"] = elemento.Calidad;
                            dtRes.Rows.Add(dr);
                        }
                        dtRes.AcceptChanges();
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                        pars[0].Value = iCodPieza;

                        // Query Execution
                        dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerUltimoProcesoPieza(), pars);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerUltimoProcesoPieza: " + ex.Message);
            }
            return dtRes;
        }

        public void ObtenerUltimoProcesoPieza(int iCodPieza, bool bForzaOffline, out int iCodUltimoProcesoPieza, out string sDesUltimoProcesoPieza)
        {
            iCodUltimoProcesoPieza = -1;
            sDesUltimoProcesoPieza = string.Empty;
            DataTable dtObj = this.ObtenerUltimoProcesoPieza(iCodPieza, bForzaOffline);
            if (dtObj != null)
            {
                if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0)
                {
                    if (dtObj.Columns.Contains("CodProceso"))
                        iCodUltimoProcesoPieza = Convert.ToInt32(dtObj.Rows[0]["CodProceso"]);
                    if (dtObj.Columns.Contains("DesProceso"))
                        sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);
                }
            }
        }
        #endregion ObtenerUltimoProcesoPieza
        #region ObtenerProcesoAnterior
        public int ObtenerProcesoAnterior(int iProcesoAct)
        {
            int iCodProcesoAnt = -1;
            if (iProcesoAct > 1)
            {
                iCodProcesoAnt = iProcesoAct - 1;
            }
            return iCodProcesoAnt;
        }
        #endregion ObtenerProcesoAnterior
        #region ObtenerCalidadCasificado
        public string ObtenerCalidadCasificado(int iCodPieza)
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerCalidadCasificado(), pars);
                if (dtRes == null) return string.Empty;
                if (dtRes.Rows.Count <= 0) return string.Empty;
                if (!dtRes.Columns.Contains("Calidad")) return string.Empty;
                return Convert.ToString(dtRes.Rows[0]["Calidad"]);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        //#region ObtenerCodProcesoClasificado
        //public int ObtenerCodProcesoClasificado()
        //{
        //    return 6;
        //}
        //#endregion
        #region ObtenerCodEstadoPiezaEnReparacion
        public int ObtenerCodEstadoPiezaEnReparacion()
        {
            return (int)2;
        }
        #endregion ObtenerCodEstadoPiezaEnReparacion
        #region ObtenerCodEstadoPiezaEnDesperdicio
        public int ObtenerCodEstadoPiezaEnDesperdicio()
        {
            return (int)4;
        }
        #endregion ObtenerCodEstadoPiezaEnDesperdicio
        #region ValidarPiezaLocal
        public ValidacionPieza ValidarPiezaLocal(string sCodBarras, int iCodProceso)
        {
            ValidacionPieza val = new ValidacionPieza(false, false, false, string.Empty, -1);
            DataTable dtObj = null;

            // Validar exista la pieza.
            val.CodPieza = this.ObtenerCodPiezaCodBarras(sCodBarras, true);
            if (val.CodPieza == -1)
            {
                val.MensajeValidacion = "Pieza no existe";
                return val;
            }

            // Obtener el ultimo proceso de la pieza.
            int iCodUltimoProcesoPieza = -1;
            string sDesUltimoProcesoPieza = string.Empty;
            dtObj = this.ObtenerUltimoProcesoPieza(val.CodPieza, true);
            if (dtObj != null)
            {
                if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0)
                {
                    if (dtObj.Columns.Contains("CodProceso"))
                        iCodUltimoProcesoPieza = Convert.ToInt32(dtObj.Rows[0]["CodProceso"]);
                    if (dtObj.Columns.Contains("DesProceso"))
                        sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);
                }
            }

            // Obtener el estado de la pieza.
            int iCodEstadoPieza = -1;
            string sDesEstadoPieza = string.Empty;
            dtObj = this.ObtenerEstadoPieza(val.CodPieza, true);
            if (dtObj != null)
            {
                if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0)
                {
                    if (dtObj.Columns.Contains("CodEstadoPieza"))
                        iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
                    if (dtObj.Columns.Contains("DesEstadoPieza"))
                        sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);
                }
            }

            int iCodProcesoClasificado = this.ObtenerCodProcesoClasificado();
            string sCalidad = this.ObtenerCalidadCasificado(val.CodPieza);
            int iCodProcesoAnt = this.ObtenerProcesoAnterior(iCodUltimoProcesoPieza);
            //--Validar que la pieza este en clasificado tenga Calidad de Clasificado de Requeme y el proceso que solicita es Hornos
            if (iCodUltimoProcesoPieza == iCodProcesoClasificado & sCalidad.ToLower() == "Requeme".ToLower() & iCodProcesoAnt == 5 & iCodProceso == 5)
            {
                val.ValProcesoExitosa = true;
                val.ValNoDefDespExitosa = true;
                val.MensajeValidacion = string.Empty;
                return val;
            }

            // Validar que la pieza solo haya pasado por el proceso anterior al actual.
            if (iCodUltimoProcesoPieza == this.ObtenerProcesoAnterior(iCodProceso))
            {
                val.ValProcesoExitosa = true;
                //string sCalidad = this.ObtenerCalidadCasificado(val.CodPieza);
                //int iCodProcesoClasificado = this.ObtenerCodProcesoClasificado();
                //--Validar que la pieza este en clasificado pero no tenga calidad de requeme para los procesos de empaque y auditoria
                if (iCodUltimoProcesoPieza == iCodProcesoClasificado & sCalidad.ToLower() == "Requeme".ToLower() & (iCodProceso == 7 | iCodProceso == 8))
                {
                    val.ValNoDefDespExitosa = false;
                    val.MensajeValidacion = "Pieza " + sDesEstadoPieza + " con calidad " + sCalidad;
                    return val;
                }

                // Validar que la pieza no este En Reparacion o En Desperdicio.
                if (iCodEstadoPieza == this.ObtenerCodEstadoPiezaEnReparacion()
                    || iCodEstadoPieza == this.ObtenerCodEstadoPiezaEnDesperdicio())
                {
                    val.MensajeValidacion = "Pieza " + sDesEstadoPieza;
                    val.ValNoDefDespExitosa = false;
                }
                else
                {
                    val.MensajeValidacion = string.Empty;
                    val.ValNoDefDespExitosa = true;
                }
            }
            else
            {
                if (iCodUltimoProcesoPieza == iCodProceso)
                {
                    val.MensajeValidacion = "Pieza recien procesada";
                }
                else
                {
                    val.MensajeValidacion = "Pieza en proceso de " + sDesUltimoProcesoPieza;
                }
                val.ValProcesoExitosa = false;
            }
            return val;
        }
        #endregion ValidarPiezaLocal
        #region ValidarPieza
        public ValidacionPieza ValidarPieza(string sCodBarras, int iCodProceso, bool bForzarOffine)
        {
            ValidacionPieza val = null;

            // Validar el codigo de barras no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodBarras))
            {
                val = new ValidacionPieza(false, false, false, "Capture Etiqueta", -1);
            }
            else
            {
                if (bForzarOffine)
                {
                    val = this.ValidarPiezaLocal(sCodBarras, iCodProceso);
                }
                else
                {
                    if (this.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        HHsvc.BSE[] res = proxy.ValidarPieza(sCodBarras, iCodProceso, true);

                        HHsvc.HHValidarPieza e = (HHsvc.HHValidarPieza)res[0];
                        val = new ValidacionPieza(false, e.VPE, e.VNDDE, e.MV, e.CP, e.Proceso);
                        this.ValidarExistenciaLocal(val.CodPieza);
                    }
                    else
                    {
                        val = this.ValidarPiezaLocal(sCodBarras, iCodProceso);
                    }
                }
            }
            return val;
        }
        #endregion ValidarPieza
        #region ValidarTarimaPieza
        public ValidacionPieza ValidarTarimaPieza(int iTarima, int sCodBarras, int sCodBarrasPadre)
        {
            ValidacionPieza val = null;

            if (this.EstaServicioDisponible())
            {
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                HHsvc.BSE[] res = proxy.ValidarTarimaPieza(iTarima, true, sCodBarras, true, sCodBarrasPadre, true);

                HHsvc.HHValidarPieza e = (HHsvc.HHValidarPieza)res[0];
                val = new ValidacionPieza();
                val.ValidacionExitosa = e.VPE;
                val.ValNoDefDespExitosa = e.VNDDE;//Variable se utiliza para saber si fue insertada o no
                val.MensajeValidacion = e.MV;
            }
            return val;
        }
        #endregion
        #region ValidarExistenciaLocal
        /// <summary>
        /// Este Metodo verifica si la pieza existe en la base de datos local, si no existe la crea con la informacion que existe en el Servidor.
        /// </summary>
        /// <param name="iCodPieza"></param>
        public void ValidarExistenciaLocal(int iCodPieza)
        {

            if (!ExistePiezaLocal(iCodPieza))
            {
                if (this.EstaServicioDisponible())
                {
                    new c00_CargaDatos().InsertarPiezaLocal(iCodPieza);
                }
            }
        }
        #endregion
        public void LimpiarControl(Control control)
        {
            try
            {
                if (control.Controls.Count == 0)
                {
                    Type tipoControl = control.GetType();
                    if (tipoControl.Name == "TextBox")
                    {
                        (control as TextBox).Text = "";
                    }
                    else if (tipoControl.Name == "ComboBox")
                    {
                        (control as ComboBox).SelectedIndex = -1;
                    }
                    else if (tipoControl.Name == "DateTimePicker")
                    {
                        (control as DateTimePicker).Value = DateTime.Now;
                        (control as DateTimePicker).CustomFormat = "dd-MMM-yyyyy";
                    }
                    else if (tipoControl.Name == "DataGrid")
                    {
                        (control as DataGrid).DataSource = null;
                        (control as DataGrid).Refresh();
                    }
                    return;
                }
                foreach (Control item in control.Controls)
                    this.LimpiarControl(item);
            }
            catch (Exception ex)
            {

            }
        }
        public Validacion ValidarEntero(String sVal)
        {
            Validacion validacion = new Validacion();
            try
            {
                Convert.ToInt32(sVal);
                validacion.ValidacionExitosa = true;
                validacion.MensajeValidacion = string.Empty;
            }
            catch
            {
                validacion.ValidacionExitosa = false;
                validacion.MensajeValidacion = "Ingrese un valor numerico correcto no mayor a 2,147,483,647";
            }
            return validacion;
        }

        #region query_LogLocalIns
        public static string query_LogLocalIns()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("INSERT INTO Log(Clave, Descripcion)  ");
            queryString.Append("VALUES(@Clave, @Descripcion)");
            return queryString.ToString();
        }
        #endregion
        #region query_ObtenerLogLocal
        public static string query_ObtenerLogLocal()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT l.Codigo, l.Clave, l.Descripcion  ");
            queryString.Append("FROM Log l");
            return queryString.ToString();
        }
        #endregion
        #region query_LogLocalDel
        public static string query_LogLocalDel()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	Log ");
            queryString.Append("where	codigo = @Codigo ");
            return queryString.ToString();
        }
        #endregion
        #region ObtenerLogLocal
        public DataTable ObtenerLogLocal()
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c00_Common.query_ObtenerLogLocal(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerLogLocal: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region LogLocalIns
        public DataTable LogLocalIns(string sClave, string sDescripcion)
        {
            DataTable dtRes = null;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@Clave", SqlDbType.NVarChar, 100);
                pars[0].Value = sClave;
                pars[1] = new SqlCeParameter("@Descripcion", SqlDbType.NVarChar, 200);
                pars[1].Value = sDescripcion;
                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_LogLocalIns(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", LogLocalIns: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #region LogLocalDel
        public bool LogLocalDel(long iCodigo)
        {
            bool bRes = false;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@Codigo", SqlDbType.BigInt);
                pars[0].Value = iCodigo;
                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_LogLocalDel(), pars);
                bRes = true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", LogLocalDel: " + ex.Message);
            }
            return bRes;
        }
        #endregion

        #region ObtenerFechaServidor
        /// <summary>
        /// Obtiene la Fecha del Servidor. En caso de que no se pueda obtener devuelve el MinValue de fecha.
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime ObtenerFechaServidor()
        {
            DateTime dtFechaServidor = DateTime.MinValue;
            bool bFechaServidor = false;
            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerFechaServidor(out dtFechaServidor, out bFechaServidor);
                }
                if (!bFechaServidor)
                    dtFechaServidor = DateTime.MinValue;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPruebas: " + ex.Message);
            }
            return dtFechaServidor;
        }
        #endregion

        #endregion Common
        #region Funciones de Encripcion y Desencripcion
        public static string Encrypt(string Data, string Key)
        {
            if (Data.Length == 0)
                throw new ArgumentException("Data must be at least 1 character in length.");
            uint[] formattedKey = FormatKey(Key);
            if (Data.Length % 2 != 0) Data += '\0'; // Make sure array is even in length.
            byte[] dataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(Data);
            string cipher = string.Empty;
            uint[] tempData = new uint[2];
            for (int i = 0; i < dataBytes.Length; i += 2)
            {
                tempData[0] = dataBytes[i];
                tempData[1] = dataBytes[i + 1];
                code(tempData, formattedKey);
                cipher += ConvertUIntToString(tempData[0]) + ConvertUIntToString(tempData[1]);
            }
            return cipher;
        }
        public static string Decrypt(string Data, string Key)
        {
            uint[] formattedKey = FormatKey(Key);
            int x = 0;
            uint[] tempData = new uint[2];
            byte[] dataBytes = new byte[Data.Length / 8 * 2];
            for (int i = 0; i < Data.Length; i += 8)
            {
                tempData[0] = ConvertStringToUInt(Data.Substring(i, 4));
                tempData[1] = ConvertStringToUInt(Data.Substring(i + 4, 4));
                decode(tempData, formattedKey);
                dataBytes[x++] = (byte)tempData[0];
                dataBytes[x++] = (byte)tempData[1];
            }
            string decipheredString = ASCIIEncoding.ASCII.GetString(dataBytes, 0, dataBytes.Length);
            // Strip the null char if it was added.
            if (decipheredString[decipheredString.Length - 1] == '\0')
                decipheredString = decipheredString.Substring(0, decipheredString.Length - 1);
            return decipheredString;
        }
        #region Tea Algorithm
        private static void code(uint[] v, uint[] k)
        {
            uint y = v[0];
            uint z = v[1];
            uint sum = 0;
            uint delta = 0x9e3779b9;
            uint n = 32;
            while (n-- > 0)
            {
                y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
                sum += delta;
                z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
            }
            v[0] = y;
            v[1] = z;
        }
        private static void decode(uint[] v, uint[] k)
        {
            uint n = 32;
            uint sum;
            uint y = v[0];
            uint z = v[1];
            uint delta = 0x9e3779b9;
            sum = delta << 5;
            while (n-- > 0)
            {
                z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
                sum -= delta;
                y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
            }
            v[0] = y;
            v[1] = z;
        }
        #endregion
        #region Funciones de Formatos
        private static string ConvertUIntToString(uint Input)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            output.Append((char)((Input & 0xFF)));
            output.Append((char)((Input >> 8) & 0xFF));
            output.Append((char)((Input >> 16) & 0xFF));
            output.Append((char)((Input >> 24) & 0xFF));
            return output.ToString();
        }
        private static uint ConvertStringToUInt(string Input)
        {
            uint output;
            output = ((uint)Input[0]);
            output += ((uint)Input[1] << 8);
            output += ((uint)Input[2] << 16);
            output += ((uint)Input[3] << 24);
            return output;
        }
        private static uint[] FormatKey(string Key)
        {
            if (Key.Length == 0)
                throw new ArgumentException("Key must be between 1 and 16 characters in length");
            Key = Key.PadRight(16, ' ').Substring(0, 16); // Ensure that the key is 16 chars in length.
            uint[] formattedKey = new uint[4];
            // Get the key into the correct format for TEA usage.
            int j = 0;
            for (int i = 0; i < Key.Length; i += 4)
                formattedKey[j++] = ConvertStringToUInt(Key.Substring(i, 4));
            return formattedKey;
        }
        #endregion
        #endregion
        public bool ActualizarCalidadPieza(int iCodPieza, string sCalidad)
        {
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlCeParameter("@DescCalidad", SqlDbType.NVarChar);
                pars[1].Value = sCalidad;
                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c00_Common.query_ActualizarCalidadPieza(), pars);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarCalidadPieza: " + ex.Message);
            }
        }
        public static string query_ActualizarCalidadPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update pieza");
            queryString.Append(" ");
            queryString.Append("set	Calidad = @DescCalidad");
            queryString.Append(" ");
            queryString.Append("where cod_pieza = @CodPieza");
            return queryString.ToString();
        }
        public int ObtenerTiempoEnMinutosCapturaColor()
        {
            int iTiempoEnMinutosCapturaColor = 20, TiempoEnMinutosCapturaColorResult = 0;
            bool TiempoEnMinutosCapturaColorResultSpecified;
            try
            {
                if (this.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerTiempoEnMinutosCapturaColor(out TiempoEnMinutosCapturaColorResult, out TiempoEnMinutosCapturaColorResultSpecified);
                    return (TiempoEnMinutosCapturaColorResult > 0) ? TiempoEnMinutosCapturaColorResult : iTiempoEnMinutosCapturaColor;
                }
                return iTiempoEnMinutosCapturaColor;
            }
            catch 
            {
                return iTiempoEnMinutosCapturaColor;
                //throw new Exception(this.sClassName + ", ObtenerTiempoEnMinutosCapturaColor: " + ex.Message);
            }
        }
        #endregion methods

    }
}