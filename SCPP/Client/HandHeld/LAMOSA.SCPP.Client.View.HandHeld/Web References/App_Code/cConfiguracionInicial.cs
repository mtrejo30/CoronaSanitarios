using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class cConfiguracionInicial
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public cConfiguracionInicial()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~cConfiguracionInicial()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerTurnos
        public static string query_ObtenerTurnos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	t.cod_turno as CodTurno, ");
            queryString.Append("		t.des_turno as DesTurno ");
            queryString.Append("from	turno t ");
            queryString.Append("where		t.fecha_baja is null ");
            queryString.Append("order by	t.des_turno asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerTurnos
        #region query_ObtenerProcesos
        public static string query_ObtenerProcesos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	p.cod_proceso as CodProceso, ");
            queryString.Append("		p.des_proceso as DesProceso ");
            queryString.Append("from	proceso p ");
            queryString.Append("where		p.fecha_baja is null ");
            queryString.Append("order by	p.cod_proceso asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerProcesos
        #region query_ObtenerPantallasProceso
        public static string query_ObtenerPantallasProceso()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	pp.cod_pantalla as CodPantalla, ");
            queryString.Append("		p.des_pantalla as DesPantalla ");
            queryString.Append("from	proceso_pantalla pp, ");
            queryString.Append("		SCPP_pantalla p ");
            queryString.Append("where		pp.cod_pantalla = p.cod_pantalla ");
            queryString.Append("		and	pp.cod_proceso = @CodProceso ");
            queryString.Append("		and p.fecha_baja is null ");
            queryString.Append("order by	p.cod_pantalla asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPantallasProceso
        #region query_ObtenerSigCodConfigHandHeld
        public static string query_ObtenerSigCodConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	((case when max(ch.cod_config_handheld) is null then 0 else max(ch.cod_config_handheld) end) + 1) as CodConfigHandHeld ");
            queryString.Append("from	config_handheld ch;");
            return queryString.ToString();
        }
        #endregion query_ObtenerSigCodConfigHandHeld
        #region query_InsertarConfigHandHeld
        public static string query_InsertarConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into config_handheld ");
            queryString.Append("(cod_config_handheld, cod_usuario, cod_operador, cod_supervisor, fecha, cod_turno, cod_proceso) ");
            queryString.Append("values (@CodConfigHandHeld, @CodUsuario, @CodOperador, @CodSupervisor, @Fecha , @CodTurno, @CodProceso);");
            return queryString.ToString();
        }
        #endregion query_InsertarConfigHandHeld

        #region ObtenerTurnos
        public DataTable ObtenerTurnos()
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cConfiguracionInicial.query_ObtenerTurnos(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTurnos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerTurnos
        #region ObtenerProcesos
        public DataTable ObtenerProcesos()
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cConfiguracionInicial.query_ObtenerProcesos(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerProcesos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerProcesos
        #region ObtenerPantallasProceso
        public DataTable ObtenerPantallasProceso(int iCodProceso)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cConfiguracionInicial.query_ObtenerPantallasProceso(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPantallasProceso: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPantallasProceso
        #region ObtenerSigCodConfigHandHeld
        public DataTable ObtenerSigCodConfigHandHeld()
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[0];

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cConfiguracionInicial.query_ObtenerSigCodConfigHandHeld(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSigCodConfigHandHeld: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerSigCodConfigHandHeld
        #region InsertarConfigHandHeld
        public void InsertarConfigHandHeld(long iCodConfigHandHeld, int iCodUsuario, int iCodOperador, int iCodSupervisor, DateTime dtFecha, int iCodTurno, int iCodProceso)
        {
            SqlCeParameter[] pars = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[7];
                pars[0] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                pars[0].Value = iCodConfigHandHeld;
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
                pars[6] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[6].Value = iCodProceso;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(cConfiguracionInicial.query_InsertarConfigHandHeld(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarConfigHandHeld: " + ex.Message);
            }
        }
        #endregion InsertarConfigHandHeld

        #endregion common

        #endregion methods

    }
}
