using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DT.CE;
using System.Data;


namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class clsSelPlanta
    {

        public static string query_cod_planta()
        {
            
                StringBuilder queryString = new StringBuilder();
                queryString.Append("select	rp.cod_planta as CodPlanta, ");
                queryString.Append("		p.des_planta as DesPlanta ");
                queryString.Append("from	rol_planta rp, ");
                queryString.Append("		planta p ");
                queryString.Append("where		rp.cod_planta = p.cod_planta ");
                queryString.Append("		and	rp.cod_rol = {0} ");
                queryString.Append("order by	p.des_planta asc;");
            return queryString.ToString();
        }

        public DataSet getPlantaLoad(int iCodRol)
        {
            DataSet ods;
            try
            {
                clsQuery oquery = new clsQuery(clsConfig.getConection());
                ods = oquery.exec(String.Format(clsSelPlanta.query_cod_planta(),iCodRol.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception("clsSelPlanta" + ", ObtenerPlantasRol: " + ex.Message);
            }
            return ods;
        }

        
    }
}
