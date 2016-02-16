using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c12_CapturaAuditoria
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c12_CapturaAuditoria()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c12_CapturaAuditoria()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ActualizarPiezaAuditada
        public static string query_ActualizarPiezaAuditada()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	pieza ");
            queryString.Append("set		Auditada = @Auditada, actualizacion = 1");
            queryString.Append("where	modificado_estado > -1 ");
            queryString.Append(" and cod_planta > -1 ");
            queryString.Append(" and cod_ultimo_proceso > -1 ");
            queryString.Append(" and cod_ultimo_estado > -1 ");
            queryString.Append(" and cod_articulo > -1 ");
            queryString.Append(" and cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarPiezaAuditada
        #region query_RechazarTarimaPieza
        public static string query_RechazarTarimaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	TarimaPieza ");
            queryString.Append("set		Rechazada = 1, ");
            queryString.Append("		Paletizado = 0, ");
            queryString.Append("		Modificado = 1 ");
            queryString.Append("where		CodTarima = @CodTarima;");
            return queryString.ToString();
        }
        #endregion query_RechazarTarimaPieza
        #region query_ActualizarTarimaPaletizado
        public static string query_ActualizarTarimaPaletizado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update	TarimaPieza ");
            queryString.Append("set		Paletizado = @Paletizado, ");
            queryString.Append("		Modificado = 1 ");
            queryString.Append("where		CodTarima = @CodTarima;");
            return queryString.ToString();
        }
        #endregion query_ActualizarTarimaPaletizado

        #region ActualizarPiezaAuditada
        public int ActualizarPiezaAuditada(DA.eTipoConexion tc, int iCodPieza, bool bAuditada)
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
                    pars[1] = new SqlCeParameter("@Auditada", SqlDbType.Bit);
                    pars[1].Value = bAuditada;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c12_CapturaAuditoria.query_ActualizarPiezaAuditada(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ActualizarPiezaAuditada(iCodPieza, true, bAuditada, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", ActualizarPiezaAuditada: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarPiezaAuditada
        #region RechazarTarimaPieza
        public int RechazarTarimaPieza(DA.eTipoConexion tc, int iCodTarima)
        {
            int iRes = -1;
            bool bRes = false;
            
            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                    pars[0].Value = iCodTarima;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c12_CapturaAuditoria.query_RechazarTarimaPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.RechazarTarimaPieza(iCodTarima, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", RechazarTarimaPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion RechazarTarimaPieza
        #region ActualizarTarimaPaletizado
        public int ActualizarTarimaPaletizado(int iCodTarima, bool bPaletizado)
        {
            int iRes = -1;
            bool bRes = false;
            
            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ActualizarTarimaPaletizado(iCodTarima, true, bPaletizado, true, out iRes, out bRes);

                    if (!bRes)
                    {
                        iRes = -1;
                    }
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                    pars[0].Value = iCodTarima;
                    pars[1] = new SqlCeParameter("@Paletizado", SqlDbType.Bit);
                    pars[1].Value = bPaletizado;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c12_CapturaAuditoria.query_ActualizarTarimaPaletizado(), pars);

                    iRes = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarTarimaPaletizado: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarTarimaPaletizado

        #endregion Common

        #endregion methods

    }
}
