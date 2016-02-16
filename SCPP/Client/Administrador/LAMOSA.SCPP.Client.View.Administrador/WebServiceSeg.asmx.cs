using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using LAMOSA.SCPP.Server.BusinessEntity.Server;

namespace LAMOSA.SCPP.Client.View.Administrador
{
    /// <summary>
    /// Summary description for WebServiceSeg
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceSeg : System.Web.Services.WebService
    {
        [WebMethod]
        public string LoadcmbMachine(int cod_CT, int cod_machine)
        {
            String options = "";
            DataTable dt = new Combos().ObtenerMaquinaCbo(-1, cod_CT, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                options += "<option " + (dr["codMaquina"].ToString().Equals(cod_machine.ToString()) ? "selected" : "") + " value=\"" + dr["codMaquina"] + "\">" + dr["DesMaquina"] + "</option> ";
            }
            return options;
        }

        [WebMethod]
        public string ObtenerMaquinas(int codigoArea, int codigoCentroTrabajo, int codigoPlanta, int codigoProceso, int codigoMaquina)
        {
            String options = "";
            DataTable dt;
            if (codigoProceso == -1 || codigoProceso == null)
            {
                dt = new Combos().ObtenerProceso(codigoCentroTrabajo);
                if (dt == null || dt.Rows.Count <= 0) return string.Empty;
                codigoProceso = Convert.ToInt32(dt.Rows[0]["CodigoProceso"]);
            }
            dt = new Combos().ObtenerMaquinaCbo(codigoArea, codigoCentroTrabajo, codigoPlanta, codigoProceso);
            foreach (DataRow dr in dt.Rows)
            {
                options += "<option " + (dr["codMaquina"].ToString().Equals(codigoMaquina.ToString()) ? "selected" : "") + " value=\"" + dr["codMaquina"] + "\">" + dr["DesMaquina"] + "</option> ";
            }
            return options;
        }
    }
}
