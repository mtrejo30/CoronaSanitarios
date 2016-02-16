using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c06_EntradaCarroSecador
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c06_EntradaCarroSecador()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c06_EntradaCarroSecador()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_InsertarPiezaTransaccionSecador
        public static string query_InsertarPiezaTransaccionSecador()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into pieza_transaccion_secador ");
            queryString.Append("(cod_pieza_transaccion, hora_inicio, horas_secado, fecha_registro, actualizacion) ");
            queryString.Append("values (@CodPiezaTransaccion, @HoraInicio, @HorasSecado, getdate(), 1);");
            return queryString.ToString();
        }
        #endregion query_InsertarPiezaTransaccionSecador

        #region InsertarPiezaTransaccionSecador
        public int InsertarPiezaTransaccionSecador(DA.eTipoConexion tc, long lCodPiezaTransaccion, DateTime dtHoraInicio, double dHorasSecado)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[3];
                    pars[0] = new SqlCeParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                    pars[0].Value = lCodPiezaTransaccion;
                    pars[1] = new SqlCeParameter("@HoraInicio", SqlDbType.DateTime);
                    pars[1].Value = dtHoraInicio;
                    pars[2] = new SqlCeParameter("@HorasSecado", SqlDbType.Float);
                    pars[2].Value = dHorasSecado;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c06_EntradaCarroSecador.query_InsertarPiezaTransaccionSecador(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarPiezaTransaccionSecador(  lCodPiezaTransaccion, true, dtHoraInicio, true, dHorasSecado, true,
                                                                out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", InsertarPiezaTransaccionSecador: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarPiezaTransaccionSecador

        #endregion common

        #endregion methods

    }
}
