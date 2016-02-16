using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c08_CapturaEsmaltado
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c08_CapturaEsmaltado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c08_CapturaEsmaltado()
        {

        }
        #endregion Constructors and Destructor

        #region Common

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
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c08_CapturaEsmaltado.query_ActualizarColorPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
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

        #endregion common

        #endregion methods

    }
}
