using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class cCapturaVaciado
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public cCapturaVaciado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~cCapturaVaciado()
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
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cCapturaVaciado.query_ObtenerClaveEmpleadoMFG(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerClaveEmpleadoMFG: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerClaveEmpleadoMFG

        #endregion common

        #endregion methods

    }
}
