using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    
    public class cEntradaCarroSecador
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public cEntradaCarroSecador()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~cEntradaCarroSecador()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerSecadores
        public static string query_ObtenerSecadores()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	m.cod_maquina as CodMaquina, ");
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
        #endregion query_ObtenerSecadores

        #region ObtenerSecadores
        public DataTable ObtenerSecadores(int iCodPlanta, int iCodProceso)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[3];
                pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlCeParameter("@CodTipoMaquina", SqlDbType.Int);
                pars[2].Value = 2;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cEntradaCarroSecador.query_ObtenerSecadores(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerSecadores: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerSecadores

        #endregion common

        #endregion methods

    }

}
