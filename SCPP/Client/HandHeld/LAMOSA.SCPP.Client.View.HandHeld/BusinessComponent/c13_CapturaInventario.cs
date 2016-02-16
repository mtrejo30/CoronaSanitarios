using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c13_CapturaInventario
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c13_CapturaInventario()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c13_CapturaInventario()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_ObtenerPiezaInventario
        public static string query_ObtenerPiezaInventario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append(" ");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezaInventario
        #region query_ActualizarPiezaInventario
        public static string query_ActualizarPiezaInventario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("update InventarioProceso set Existencia = 1");
            queryString.Append(" ,actualizado = 1 "); //Esta lienea es para indicar que se ha insertado fuera de linea.
            queryString.Append("where IdPieza = @CodPieza and IdInventarioProceso = @IdInventarioProceso;");
            return queryString.ToString();
        }
        #endregion query_ActualizarPiezaInventario
        #region query_InsertarPiezaInventario
        public static string query_InsertarPiezaInventario()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into InventarioProcesoPieza(cod_barras, ");
            queryString.Append("cod_planta, cod_proceso, cod_articulo, cod_color, cod_calidad, cod_ultimo_estado, fecha_registro, nuevo) ");
            queryString.Append("values(''''+ @CodBarras +'''', @CodPlanta, @CodProceso, @CodModelo, @CodColor, ");
            queryString.Append("@CodCalidad, @CodUltimoEstado, @FechaRegistro, 1)");

            return queryString.ToString();
        }
        #endregion query_InsertarPiezaInventario
        #region query_ExisteInventarioProcesoActivo
        public static string query_ExisteInventarioProcesoActivo()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select ip.IdInventarioProceso  ");
            queryString.Append("from InventarioProcesoEstatus ip ");
            queryString.Append("where FechaTermino is null;");
            return queryString.ToString();
        }
        #endregion query_ExisteInventarioProcesoActivo

        #region ObtenerPiezaInventario
        public int ObtenerPiezaInventario(int iCodPieza)
        {
            int iRes = -1;
            bool bCodProcesoRes = false;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ObtenerPiezaInventario(iCodPieza, true, out iRes, out bCodProcesoRes);
                }
                else
                {
                    iRes = ExisteInventarioProcesoActivo();
                    if (iRes > -1)
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[1];
                        pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                        pars[0].Value = iCodPieza;
                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(query_ObtenerPiezaInventario(), pars);
                        if (dtRes.Rows.Count > 0)
                        {
                            iRes = Convert.ToInt32(dtRes.Rows[0][0]);
                        }
                        else
                        {
                            iRes = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaInventario: " + ex.Message);
            }
            return iRes;
        }
        #endregion ObtenerPiezaInventario
        #region ActualizarPiezaInventario
        public int ActualizarPiezaInventario(int iCodPieza)
        {
            int iRes = -1;
            bool bRes = true;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ActualizarPiezaInventario(iCodPieza, true, out iRes, out bRes);
                }
                else
                {
                    iRes = ExisteInventarioProcesoActivo();
                    if (iRes > -1)
                    {
                        // Parameters
                        SqlCeParameter[] pars = new SqlCeParameter[2];
                        pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                        pars[0].Value = iCodPieza;
                        pars[1] = new SqlCeParameter("@IdInventarioProceso", SqlDbType.Int);
                        pars[1].Value = iRes;

                        // Query Execution
                        DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(query_ActualizarPiezaInventario(), pars);
                        if (dtRes.Rows.Count > 0)
                        {
                            iRes = Convert.ToInt32(dtRes.Rows[0][0]);
                        }
                        else
                        {
                            iRes = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarPiezaInventario: " + ex.Message);
            }
            return iRes;
        }
        #endregion ActualizarPiezaInventario
        #region InsertarPiezaInventario
        public int InsertarPiezaInventario( string sCodBarras, int iCodPlanta, int iCodProceso, int iCodModelo, 
                                            int iCodColor, int iCodCalidad, int iCodUltimoEstado)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.InsertarPiezaInventario(  sCodBarras, iCodPlanta, true, iCodProceso, true, -1, true, -1, true, -1, true,
                                                    iCodModelo, true, iCodColor, true, iCodCalidad, true, 1, true,
                                                    out iRes, out bRes);
                }
                else
                {
                         // Parameters
                         SqlCeParameter[] pars = new SqlCeParameter[8];
                         int i = 0;

                         pars[i] = new SqlCeParameter("@CodBarras", SqlDbType.NVarChar, 15);
                         pars[i++].Value = sCodBarras;
                         pars[i] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                         pars[i++].Value = iCodPlanta;
                         pars[i] = new SqlCeParameter("@CodProceso", SqlDbType.Int);
                         pars[i++].Value = iCodProceso;
                         pars[i] = new SqlCeParameter("@CodModelo", SqlDbType.Int);
                         pars[i++].Value = iCodModelo;
                         pars[i] = new SqlCeParameter("@CodColor", SqlDbType.Int);
                         pars[i++].Value = iCodColor;
                         pars[i] = new SqlCeParameter("@CodCalidad", SqlDbType.Int);
                         pars[i++].Value = iCodCalidad;
                         pars[i] = new SqlCeParameter("@CodUltimoEstado", SqlDbType.Int);
                         pars[i++].Value = iCodUltimoEstado;
                         pars[i] = new SqlCeParameter("@FechaRegistro", SqlDbType.DateTime);
                         pars[i].Value = DateTime.Now;

                         // Query Execution
                         DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c13_CapturaInventario.query_InsertarPiezaInventario(), pars);
                         iRes = 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarPiezaInventario: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarPiezaInventario
        #region ExisteInventarioProcesoActivo
        public int ExisteInventarioProcesoActivo()
        {
            int iRes = -1;
            Boolean bRes = true;

            try
            {
                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    proxy.ExisteInventarioProcesoActivo(out iRes, out bRes);
                }
                else
                {
                    // Parameters
                    SqlCeParameter[] pars = pars = new SqlCeParameter[0];

                    // Query Execution
                    DataTable dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(c13_CapturaInventario.query_ExisteInventarioProcesoActivo(), pars);
                    if (dtRes.Rows.Count > 0)
                    {
                        iRes = Convert.ToInt32(dtRes.Rows[0][0]);
                    }
                    else
                    {
                        iRes = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaInventario: " + ex.Message);
            }
            return iRes;
        }
        #endregion ExisteInventarioProcesoActivo

        #endregion Common

        #endregion methods

    }
}
