using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;
using DT.CE;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class cVaciado
    {

        #region fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        private string strOperador = "";
        private string strSupervisor = "";
        private int int_CentroTrabajo = 0;
        private string int_Banco = "";
        private int int_Posicion = 0;
        private bool Bit_Asc = true;

        #endregion fields

        #region properties

        public string Operador { get { return this.strOperador; } set { this.strOperador = value; } }
        public string Supervisor { get { return this.strSupervisor; } set { this.strSupervisor = value; } }
        public int CentroTrabajo { get { return this.int_CentroTrabajo; } set { this.int_CentroTrabajo = value; } }
        public string Banco { get { return this.int_Banco; } set { this.int_Banco = value; } }
        public int Posicion { get { return this.int_Posicion; } set { this.int_Posicion = value; } }
        public bool Asc { get { return this.Bit_Asc; } set { this.Bit_Asc = value; } }

        #endregion properties

        public static string query_getVaciado()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT  ");
            queryString.Append("CBM.posicion as Num_Posicion, ");
            queryString.Append("TA.clave_tipo_articulo as cve_tipo_articulo, ");
            queryString.Append("A.cod_articulo as codarticulo ");
            queryString.Append("FROM  ");
            queryString.Append("config_handheld CHH ");
            queryString.Append("JOIN  ");
            queryString.Append("config_banco CB ");
            queryString.Append("ON  ");
            queryString.Append("CB.cod_config_banco = CHH.cod_config_banco ");
            queryString.Append("JOIN  ");
            queryString.Append("config_banco_molde_det CBMD ");
            queryString.Append("ON ");
            queryString.Append("CBMD.cod_config_banco = CB.cod_config_banco ");
            queryString.Append("JOIN  ");
            queryString.Append("config_banco_molde CBM ");
            queryString.Append("ON ");
            queryString.Append("CBM.cod_consecutivo = CBMD.cod_consecutivo ");
            queryString.Append("JOIN  ");
            queryString.Append("molde M ");
            queryString.Append("ON ");
            queryString.Append("M.cod_molde = CBMD.cod_molde ");
            queryString.Append("JOIN  ");
            queryString.Append("articulo A ");
            queryString.Append("ON ");
            queryString.Append("A.cod_molde = M.cod_molde ");
            queryString.Append("JOIN  ");
            queryString.Append("tipo_articulo TA ");
            queryString.Append("ON ");
            queryString.Append("TA.cod_tipo_articulo = A.cod_tipo_articulo ");
            queryString.Append("JOIN  ");
		    queryString.Append("maquina MA ");
	        queryString.Append("ON  ");
		    queryString.Append("CB.cod_maquina  = MA.cod_maquina ");
            queryString.Append("WHERE ");
            queryString.Append("    CHH.cod_operador	= '{0}' ");
            queryString.Append("AND CHH.cod_supervisor	= '{1}' ");
            queryString.Append("AND MA.clave_maquina      = '{2}' ");
            queryString.Append("ORDER BY ");
            queryString.Append("posicion ASC ");

            return queryString.ToString();

        }
        public static string query_getPrueba()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT cod_prueba as codprueba, des_prueba as desprueba FROM prueba ");
            return queryString.ToString();
        }

        public DataSet getVaciado()
        {
            DataSet ods = null;
            try
            {
                clsQuery oquery = new clsQuery(clsConfig.getConection());
                ods = oquery.exec(String.Format(cVaciado.query_getVaciado(), this.Operador, this.Supervisor, this.Banco));
            }
            catch (Exception ex)
            {
                throw new Exception("clsVaciado" + ", getVaciado: " + ex.Message);
            }
            return ods;
        }
        public DataSet getPrueba()
        {
            DataSet ods = null;
            try
            {
                clsQuery oquery = new clsQuery(clsConfig.getConection());
                ods = oquery.exec(String.Format(cVaciado.query_getPrueba()));
            }
            catch (Exception ex)
            {
                throw new Exception("clsVaciado" + ", getPrueba: " + ex.Message);
            }
            return ods;
        }

    }
}
