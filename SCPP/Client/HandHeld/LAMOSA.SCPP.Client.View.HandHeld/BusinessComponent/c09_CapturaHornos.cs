using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c09_CapturaHornos
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c09_CapturaHornos()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c09_CapturaHornos()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region query_InsertarCarroZonaPieza
        public static string query_InsertarCarroZonaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into carro_pieza_quemado ");
            queryString.Append("(cod_planta, cod_pieza, cod_carro, cod_zona, actualizacion) ");
            queryString.Append("values (@CodPlanta, @CodPieza, @CodCarro, @CodZona, 1);");
            return queryString.ToString();
        }
        #endregion query_InsertarCarroZonaPieza

        #region query_ValidarExisteCarroZonaPieza
        public static string query_ValidarExisteCarroZonaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT 1 AS Existe ");
            queryString.Append("FROM carro_pieza_quemado ");
            queryString.Append("WHERE cod_planta = @CodPlanta AND cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ValidarExisteCarroZonaPieza

        #region query_ActualizarCarroZonaPieza
        public static string query_ActualizarCarroZonaPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("UPDATE carro_pieza_quemado ");
            queryString.Append("SET	cod_carro = @CodCarro, cod_zona = @CodZona, actualizacion = 1 ");
            queryString.Append("WHERE cod_planta = @CodPlanta AND cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ActualizarCarroZonaPieza

        #region InsertarCarroZonaPieza
        public int InsertarCarroZonaPieza(DA.eTipoConexion tc, int iCodPlanta, int iCodPieza, int iCodCarro, string sCodZona)
        {
            int iRes = -1;
            bool bRes = false;

            try
            {
                if (tc == DA.eTipoConexion.Local)
                {
                    DataTable dt = null;
                    // Parameters
                    SqlCeParameter[] pars = new SqlCeParameter[4];
                    pars[0] = new SqlCeParameter("@CodPlanta", SqlDbType.Int);
                    pars[0].Value = iCodPlanta;
                    pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                    pars[1].Value = iCodPieza;
                    pars[2] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                    pars[2].Value = iCodCarro;
                    pars[3] = new SqlCeParameter("@CodZona", SqlDbType.NVarChar, 5);
                    pars[3].Value = sCodZona;

                    dt = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(query_ValidarExisteCarroZonaPieza(), pars);
                    if (dt == null)
                        DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c09_CapturaHornos.query_InsertarCarroZonaPieza(), pars);
                    if (!(dt.Rows.Count > 0))
                        DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(c09_CapturaHornos.query_InsertarCarroZonaPieza(), pars);
                    else if (Convert.ToInt32(dt.Rows[0]["Existe"]) == 1)
                        DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(query_ActualizarCarroZonaPieza(), pars);
                    iRes = 0;
                }
                else
                {
                    if (this.oDA0.EstaServicioDisponible())
                    {
                        HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                        proxy.InsertarCarroZonaPieza(iCodPlanta, true, iCodPieza, true, iCodCarro, true, sCodZona, out iRes, out bRes);

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
                throw new Exception(this.sClassName + ", InsertarCarroZonaPieza: " + ex.Message);
            }
            return iRes;
        }
        #endregion InsertarCarroZonaPieza

        #region PiezasReQuemado
        public DataTable ObtenerPiezasReQuemado(int iCodPlanta)
        {
            DataTable dtRes = new DataTable();

            try
            {

                if (this.oDA0.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    dtRes = proxy.ObtenerPiezasRequeme(iCodPlanta, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasReQuemado: " + ex.Message);
            }
            return dtRes;
        }
        public void InsertarPiezasRequeme(int iCodPlanta)
        {
            try
            {
                DataTable dt = this.ObtenerPiezasReQuemado(iCodPlanta);
                if (dt != null & dt.Rows.Count > 0) {
                    c00_CargaDatos cd = new c00_CargaDatos();
                    cd.InsertarInformacion(dt, "pieza");
                    cd.ActualizarInformacion(dt, "pieza");
                }
            }
            catch (Exception e) { throw new Exception(this.sClassName + ", InsertarPiezasRequeme: " + e.Message); }
        }
        #endregion

        #endregion common

        #endregion methods

    }
}
