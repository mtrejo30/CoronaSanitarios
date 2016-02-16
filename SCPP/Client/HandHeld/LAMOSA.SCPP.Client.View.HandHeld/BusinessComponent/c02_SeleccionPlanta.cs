using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c02_SeleccionPlanta
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c02_SeleccionPlanta()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c02_SeleccionPlanta()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerPlantasRol
        public static string query_ObtenerPlantasRol()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	rp.cod_planta as CodPlanta, ");
            queryString.Append("		p.des_planta as DesPlanta ");
            queryString.Append("from	rol_planta rp, ");
            queryString.Append("		planta p ");
            queryString.Append("where		rp.cod_planta = p.cod_planta ");
            queryString.Append("		and	rp.cod_rol = @CodRol ");
            queryString.Append("order by	p.des_planta asc;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPlantasRol

        #region ObtenerPlantasRol
        public DataTable ObtenerPlantasRol(int iCodRol)
        {
            DataTable dtRes = null;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPlantasRol(iCodRol, true);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodRol", SqlDbType.Int);
                    pars[0].Value = iCodRol;

                    // Query Execution
                    dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c02_SeleccionPlanta.query_ObtenerPlantasRol(), pars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPlantasRol: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPlantasRol

        #endregion common

        #endregion methods

    }
}
