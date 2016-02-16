using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c14_ReemplazoEtiqueta
    {
        
        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c14_ReemplazoEtiqueta()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c14_ReemplazoEtiqueta()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_InsertaPiezaReemplazo
        public static string query_InsertaPiezaReemplazo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into pieza_reemplazo ");
            queryString.Append("(cod_pieza, cod_proceso, cod_pieza_anterior, fecha_registro) values( ");
            queryString.Append("@CodPieza, @CodProcesoPiezaReem, null, GETDATE());");
            return queryString.ToString();
        }
        #endregion query_InsertaPiezaReemplazo

        #region InsertarEtiquetaReemplazo
        public int InsertarEtiquetaReemplazo(DA.eTipoConexion tc, int iCodPlanta, string sCodBarras, int iCodModelo, int iCodColor, int iCodCalidad,
                                                int iCodUltimoProceso, int iCodUltimoEstado, long lCodConfigHandheld,
                                                DateTime dtFechaRegistro, int iCodProcesoPiezaReem, DateTime dtFechaInicio)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    int iCodPieza = this.oDA0.InsertarPieza(DA.eTipoConexion.Local,
                                                            iCodPlanta,
                                                            sCodBarras,
                                                            -1,
                                                            -1,
                                                            -1,
                                                            iCodModelo,
                                                            iCodUltimoProceso,
                                                            iCodUltimoEstado,
                                                            dtFechaRegistro,
                                                            -1,
                                                            -1);
                    new c08_CapturaEsmaltado().ActualizarColorPieza(DA.eTipoConexion.Local,
                                                                    iCodPieza,
                                                                    iCodColor);
                    new c11_CapturaEmpaque().ActualizarCalidadPieza(DA.eTipoConexion.Local,
                                                                    iCodPieza,
                                                                    iCodCalidad);
                    long iPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                lCodConfigHandheld,
                                                                                iCodPieza, dtFechaInicio);
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    int i = 0;
                    pars[i] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[i++].Value = iCodPieza;
                    pars[i] = new SqlCeParameter("@CodProcesoPiezaReem", SqlDbType.Int);
                    pars[i++].Value = iCodProcesoPiezaReem;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(query_InsertaPiezaReemplazo(), pars);
                    iRes = (int)iPiezaTransaccion;
                }
                else
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.InsertarEtiquetaReemplazo(iCodPlanta, true, sCodBarras, iCodModelo, true, iCodColor, true, iCodCalidad, true,
                                                    iCodUltimoProceso, true, iCodUltimoEstado, true, lCodConfigHandheld, true,
                                                    dtFechaRegistro, true, iCodProcesoPiezaReem, true, out iRes, out bRes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarEtiquetaReemplazo: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarEtiquetaReemplazo

        #endregion Common

        #endregion methods

    }
}
