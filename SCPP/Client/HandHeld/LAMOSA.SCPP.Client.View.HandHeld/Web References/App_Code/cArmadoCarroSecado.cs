using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class cArmadoCarroSecado
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region constructors and destructor
        public cArmadoCarroSecado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~cArmadoCarroSecado()
        {

        }
        #endregion constructors and destructor

        #region common

        #region query_ObtenerPiezasCarro
        public static string query_ObtenerPiezasCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cp.cod_pieza as CodPieza ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_ObtenerPiezasCarro
        #region query_ObtenerCodPieza
        public static string query_ObtenerCodPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cod_pieza as CodPieza ");
            queryString.Append("from	pieza p ");
            queryString.Append("where		p.cod_barras = @CodBarras;");
            return queryString.ToString();
        }
        #endregion query_ObtenerCodPieza
        #region query_ExistePiezaEnCarro
        public static string query_ExistePiezaEnCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("select	cp.cod_carro as CodCarro ");
            queryString.Append("from	carro_pieza cp ");
            queryString.Append("where		cp.cod_pieza = @CodPieza;");
            return queryString.ToString();
        }
        #endregion query_ExistePiezaEnCarro
        #region query_InsertarCarroPieza
        public static string query_InsertarCarroPieza()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("insert into carro_pieza ");
            queryString.Append("(cod_carro, cod_pieza) ");
            queryString.Append("values (@CodCarro, @CodPieza);");
            return queryString.ToString();
        }
        #endregion query_InsertarCarroPieza
        #region query_EliminarCarro
        public static string query_EliminarCarro()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("delete ");
            queryString.Append("from	carro_pieza ");
            queryString.Append("where		cod_carro = @CodCarro;");
            return queryString.ToString();
        }
        #endregion query_EliminarCarro

        #region ObtenerPiezasCarro
        public DataTable ObtenerPiezasCarro(int iCodCarro)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[0].Value = iCodCarro;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cArmadoCarroSecado.query_ObtenerPiezasCarro(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasCarro: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerPiezasCarro
        #region ObtenerCodPieza
        public DataTable ObtenerCodPieza(int iCodBarras)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodBarras", SqlDbType.Int);
                pars[0].Value = iCodBarras;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cArmadoCarroSecado.query_ObtenerCodPieza(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodPieza: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ObtenerCodPieza
        #region ExistePiezaEnCarro
        public DataTable ExistePiezaEnCarro(int iCodPieza)
        {
            SqlCeParameter[] pars = null;
            DataTable dtRes = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtRes = DA.ConfigDataAccess.ObtenerConexion().ObtenerRegistros(cArmadoCarroSecado.query_ExistePiezaEnCarro(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExistePiezaEnCarro: " + ex.Message);
            }
            return dtRes;
        }
        #endregion ExistePiezaEnCarro
        #region InsertarCarroPieza
        public void InsertarCarroPieza(int iCodCarro, int iCodPieza)
        {
            SqlCeParameter[] pars = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[2];
                pars[0] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[0].Value = iCodCarro;
                pars[1] = new SqlCeParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(cArmadoCarroSecado.query_InsertarCarroPieza(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarCarroPieza: " + ex.Message);
            }
        }
        #endregion InsertarCarroPieza
        #region EliminarCarro
        public void EliminarCarro(int iCodCarro)
        {
            SqlCeParameter[] pars = null;

            try
            {

                // Parameters
                pars = new SqlCeParameter[1];
                pars[0] = new SqlCeParameter("@CodCarro", SqlDbType.Int);
                pars[0].Value = iCodCarro;

                // Query Execution
                DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(cArmadoCarroSecado.query_EliminarCarro(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EliminarCarro: " + ex.Message);
            }
        }
        #endregion EliminarCarro

        #endregion common

        #endregion methods

    }
}
