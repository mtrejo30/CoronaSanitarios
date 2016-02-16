using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c11_ArmadoTarima
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c11_ArmadoTarima()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c11_ArmadoTarima()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ExistePiezaEnTarima
        public static string query_ExistePiezaEnTarima()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	tp.CodTarima as CodTarima ");
            queryString.Append("from	TarimaPieza tp ");
            queryString.Append("where		tp.CodPieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ExistePiezaEnTarima
        #region query_InsertarTarimaPieza
        public static string query_InsertarTarimaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into TarimaPieza ");
            queryString.Append("(CodTarima, CodPieza, Paletizado, Rechazada, FechaRegistro, nuevo) ");
            queryString.Append("values (@CodTarima, @CodPieza, 0, 0, getdate(), 1);");
            return queryString.ToString();
        }
        #endregion query_InsertarTarimaPieza
        #region query_EliminarTarima
        public static string query_EliminarTarima()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	TarimaPieza ");
            queryString.Append("where		CodTarima = @CodTarima;");
            return queryString.ToString();
        }
        #endregion query_EliminarTarima
        #region query_ObtenerPiezaEnTarima
        public static string query_ObtenerPiezaEnTarima()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select top(1) CodPieza ");
            queryString.Append("from	TarimaPieza ");
            queryString.Append("where	CodTarima = @CodTarima;");
            return queryString.ToString();
        }
        #endregion

        #region ExistePiezaEnTarima
        public int ExistePiezaEnTarima(int iCodPieza)
        {
            int iCodTarima = -1;
            bool bCodTarima = false;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExistePiezaEnTarima(iCodPieza, true, out iCodTarima, out bCodTarima);

                    if (!bCodTarima)
                    {
                      return iCodTarima = -1;
                    }
                }
                if(iCodTarima == -1) 
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[1];
                    pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = iCodPieza;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c11_ArmadoTarima.query_ExistePiezaEnTarima(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodTarima = Convert.ToInt32(dtRes.Rows[0]["CodTarima"]);
                    }
                    else
                    {
                        iCodTarima = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExistePiezaEnTarima: " + ex.Message);
            }
            return iCodTarima;
        }
        #endregion ExistePiezaEnTarima
        #region InsertarTarimaPieza
        public int InsertarTarimaPieza(DA.eTipoConexion tc, int iCodTarima, int iCodPieza)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[2];
                    pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                    pars[0].Value = iCodTarima;
                    pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[1].Value = iCodPieza;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c11_ArmadoTarima.query_InsertarTarimaPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarTarimaPieza(iCodTarima, true, iCodPieza, true, false, true, false, true,
                                                    DateTime.Now, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", InsertarTarimaPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarTarimaPieza
        #region EliminarTarima
        public int EliminarTarima(DA.eTipoConexion tc, int iCodTarima, int iPieza)
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
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c11_ArmadoTarima.query_EliminarTarima(), pars);
                    iRes = 0;
                    if (iPieza != -1) {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.DeleteTarimaPieza(iCodTarima, true, iPieza, true, out iRes, out bRes);
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        //proxy.EliminarTarima(iCodTarima, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", EliminarTarima: " + ex.Message);
            }
            return iRes;
        }
        #endregion EliminarTarima

        #region ObtenerPiezaEnTarima
        public int ObtenerPiezaEnTarima(int iCodTarima)
        {
            int iRes = -1;

            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodTarima", SqlDbType.Int);
                pars[0].Value = iCodTarima;

                // Query Execution
                DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c11_ArmadoTarima.query_ObtenerPiezaEnTarima(), pars);

                if (dtRes != null && dtRes.Rows.Count > 0)
                {
                    iCodTarima = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);
                }
                else
                {
                    iCodTarima = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaEnTarima: " + ex.Message);
            }
            return iRes;
        }
        #endregion
        #endregion common

        #endregion methods

    }
}
