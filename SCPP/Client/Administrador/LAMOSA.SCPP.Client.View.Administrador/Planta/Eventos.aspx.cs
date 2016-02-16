using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using System.Collections.Generic;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using System.ComponentModel;

using Infragistics.Shared;
using Infragistics.Excel;

namespace LAMOSA.SCPP.Client.View.Administrador.Planta
{
    public partial class Eventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //Metodo para Generar el Reporte
            if (ddlSeleccion.SelectedItem.Text.ToString() == "MS Excel (XLS)")
            {
                Workbook Reporte = new Workbook();
                uwgEventos.Export(UltraWebGrid1, Reporte, 0, 0);
            }
            else
            {


            }

            //////Reports.DataSet.dsUnidadAdmin dsUnidadA = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdmin();
            //////Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter ts = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter();
            //////ts.Fill(dsUnidadA.PI_unidadadmin_sucursal, Convert.ToInt32(cmbSucursal.SelectedValue));
            //GenerarReporte(ddlSeleccion.SelectedItem.Text.ToString(), null, new DataSet(), new Reportes.RPTurnos());
        }
    }
}
