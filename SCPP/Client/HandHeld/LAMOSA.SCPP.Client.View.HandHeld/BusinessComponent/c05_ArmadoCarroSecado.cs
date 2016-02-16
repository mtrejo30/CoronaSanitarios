using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c05_ArmadoCarroSecado
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public c05_ArmadoCarroSecado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c05_ArmadoCarroSecado()
        {

        }
        #endregion constructors and destructor

        #region common

        #region query_ExistePiezaEnCarro
        public static string query_ExistePiezaEnCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cp.cod_carro as CodCarro ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.cod_planta = @CodPlanta ");
            queryString.Append("		and	cp.cod_proceso = @CodProceso ");
            queryString.Append("		and	cp.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ExistePiezaEnCarro
        #region query_InsertarCarroPieza
        public static string query_InsertarCarroPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into carro_pieza ");
            queryString.Append("(cod_planta, cod_proceso, cod_carro, cod_pieza, fecha_registro, actualizacion, tipoTransporte) ");
            queryString.Append("values (@CodPlanta, @CodProceso, @CodCarro, @CodPieza, getdate(), 1,@TiopoTransporte);");
            return queryString.ToString();
        }
        #endregion query_InsertarCarroPieza
        #region query_ObtenerTransporte
        public static string query_ObtenerTransporte()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select idTrasporte Cod, Descripcion ");
            queryString.Append("from Trasporte order by idTrasporte desc; ");
            return queryString.ToString();
        }
        #endregion 


        #region ExistePiezaEnCarro
        public int ExistePiezaEnCarro(int iCodPlanta, int iCodProceso, int iCodPieza, bool bForzarOffine)
        {
            int iCodCarro = -1;
            bool bCodCarro = false;

            try
            {
                if (bForzarOffine)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[3];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[2].Value = iCodPieza;

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_ArmadoCarroSecado.query_ExistePiezaEnCarro(), pars);

                    if (dtRes.Rows.Count > 0)
                    {
                        iCodCarro = Convert.ToInt32(dtRes.Rows[0]["CodCarro"]);
                    }
                    else
                    {
                        iCodCarro = -1;
                    }
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.ExistePiezaEnCarro(iCodPlanta, true, iCodProceso, true, iCodPieza, true, out iCodCarro, out bCodCarro);

                        if (!bCodCarro)
                        {
                            iCodCarro = -1;
                        }
                    }
                    else
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[3];
                        pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                        pars[0].Value = iCodPlanta;
                        pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                        pars[1].Value = iCodProceso;
                        pars[2] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                        pars[2].Value = iCodPieza;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_ArmadoCarroSecado.query_ExistePiezaEnCarro(), pars);

                        if (dtRes.Rows.Count > 0)
                        {
                            iCodCarro = Convert.ToInt32(dtRes.Rows[0]["CodCarro"]);
                        }
                        else
                        {
                            iCodCarro = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExistePiezaEnCarro: " + ex.Message);
            }
            return iCodCarro;
        }
        #endregion ExistePiezaEnCarro
        #region InsertarCarroPieza
        public int InsertarCarroPieza(DA.eTipoConexion tc, int iCodPlanta, int iCodProceso, int iCodCarro, int iCodPieza, DateTime? dtFechaRegistro, int tipoTransporte)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[5];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                    pars[2].Value = iCodCarro;
                    pars[3] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[3].Value = iCodPieza;
                    pars[4] = new SqlCeParameter("@TiopoTransporte", SqlDbType.Int);
                    pars[4].Value = tipoTransporte;

                    // Query Execution
                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c05_ArmadoCarroSecado.query_InsertarCarroPieza(), pars);

                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarCarroPieza(iCodPlanta, true, iCodProceso, true, iCodCarro, true, iCodPieza, true,
                                                    dtFechaRegistro.Value, true, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", InsertarCarroPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarCarroPieza
        #region ObtenerTransporte
        public DataTable ObtenerTransporte()
        {
            DataTable dtRes = null;
            try
            {
                // Parameters
                SqlCeParameter[] pars = new SqlCeParameter[0];
                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c05_ArmadoCarroSecado.query_ObtenerTransporte(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarCarroPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion
        #endregion common

        #endregion methods

    }
}
