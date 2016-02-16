using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class cCapturaInicial
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public cCapturaInicial()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~cCapturaInicial()
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
            queryString.Append("where		e.cod_empleado = @CodEmpleado ");
            queryString.Append("		and e.fecha_baja is null;");
            return queryString.ToString();
        }
        #endregion query_ObtenerClaveEmpleadoMFG
        #region query_ValidarEmpleadoMFG
        public static string query_ValidarEmpleadoMFG()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	e.cod_empleado as CodEmpleado ");
            queryString.Append("from	empleado e ");
            queryString.Append("where	e.clave_empleado_MFG = @ClaveEmpleadoMFG ");
            queryString.Append("		and e.fecha_baja is null;");
            return queryString.ToString();
        }
        #endregion query_ValidarEmpleadoMFG
        #region query_ObtenerCentrosTrabajo
        public static string query_ObtenerCentrosTrabajo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	ct.cod_centro_trabajo as CodCentroTrabajo, ");
            queryString.Append("		ct.des_centro_trabajo as DesCentroTrabajo ");
            queryString.Append("from	centro_trabajo ct ");
            queryString.Append("where		ct.cod_planta = @CodPlanta ");
            queryString.Append("		and	ct.cod_proceso = @CodProceso ");
            queryString.Append("		and	ct.fecha_baja is null ");
            queryString.Append("order by	ct.des_centro_trabajo asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCentrosTrabajo
        #region query_ObtenerBancos
        public static string query_ObtenerBancos()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cb.cod_config_banco as CodConfigBanco, ");
            queryString.Append("		m.cod_maquina as CodMaquina, ");
            queryString.Append("		m.des_maquina as DesMaquina ");
            queryString.Append("from	centro_trabajo ct, ");
            queryString.Append("		area a, ");
            queryString.Append("		maquina m, ");
            queryString.Append("		config_banco cb ");
            queryString.Append("where		ct.cod_centro_trabajo = a.cod_centro_trabajo ");
            queryString.Append("		and	a.cod_area = m.cod_area ");
            queryString.Append("		and	m.cod_maquina = cb.cod_maquina ");
            queryString.Append("		and	ct.cod_planta = @CodPlanta ");
            queryString.Append("		and	ct.cod_proceso = @CodProceso ");
            queryString.Append("		and	ct.cod_centro_trabajo = @CodCentroTrabajo ");
            queryString.Append("		and ct.fecha_baja is null ");
            queryString.Append("		and a.fecha_baja is null ");
            queryString.Append("		and	m.cod_tipo_maquina = @CodTipoMaquina ");
            queryString.Append("		and m.fecha_baja is null ");
            queryString.Append("		and cb.cod_usuario_autoriza is not null ");
            queryString.Append("		and cb.fecha_inicio <= getdate() ");
            queryString.Append("		and cb.fecha_fin is null ");
            queryString.Append("order by	m.des_maquina asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerBancos
        #region query_ActualizarConfigHandHeld
        public static string query_ActualizarConfigHandHeld()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	config_handheld ");
            queryString.Append("set		cod_supervisor = @CodSupervisor, ");
            queryString.Append("		cod_config_banco = @CodConfigBanco ");
            queryString.Append("where		cod_config_handheld = @CodConfigHandHeld;");
            return queryString.ToString();
        }
        #endregion query_ActualizarConfigHandHeld

        #region ObtenerClaveEmpleadoMFG
        public DataTable ObtenerClaveEmpleadoMFG(int iCodEmpleado)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodEmpleado", SqlDbType.Int);
                pars[0].Value = iCodEmpleado;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cCapturaInicial.query_ObtenerClaveEmpleadoMFG(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerClaveEmpleadoMFG: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerClaveEmpleadoMFG
        #region ValidarEmpleadoMFG
        public DataTable ValidarEmpleadoMFG(int iClaveEmpleadoMFG)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                pars[0].Value = iClaveEmpleadoMFG;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cCapturaInicial.query_ValidarEmpleadoMFG(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ValidarEmpleadoMFG: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ValidarEmpleadoMFG
        #region ObtenerCentrosTrabajo
        public DataTable ObtenerCentrosTrabajo(int iCodPlanta, int iCodProceso)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cCapturaInicial.query_ObtenerCentrosTrabajo(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCentrosTrabajo: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCentrosTrabajo
        #region ObtenerBancos
        public DataTable ObtenerBancos(int iCodPlanta, int iCodProceso, int iCodCentroTrabajo)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[4];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodCentroTrabajo", SqlDbType.Int);
                pars[2].Value = iCodCentroTrabajo;
                pars[3] = new SqlCeParameter("@CodTipoMaquina", SqlDbType.Int);
                pars[3].Value = 1;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cCapturaInicial.query_ObtenerBancos(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerBancos: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerBancos
        #region ActualizarConfigHandHeld
        public void ActualizarConfigHandHeld(int iCodSupervisor, int iCodConfigBanco, long lCodConfigHandHeld)
        {
            SqlCeParameter[] pars = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[3];
                pars[0] = new SqlCeParameter("@CodSupervisor", SqlDbType.Int);
                pars[0].Value = iCodSupervisor;
                pars[1] = new SqlCeParameter("@CodConfigBanco", SqlDbType.Int);
                pars[1].Value = iCodConfigBanco;
                pars[2] = new SqlCeParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                pars[2].Value = lCodConfigHandHeld;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(cCapturaInicial.query_ActualizarConfigHandHeld(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarConfigHandHeld: " + ex.Message);
            }
        }
        #endregion ActualizarConfigHandHeld

        #endregion common

        #endregion methods

    }
}
