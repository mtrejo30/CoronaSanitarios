using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAMOSA.SCPP.Server.BusinessEntity;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using System.Drawing;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.WebUI.UltraWebChart;
using System.Web.Security;
using LAMOSA.SCPP.Server.BusinessEntity.Server;

namespace LAMOSA.SCPP.Client.View.Administrador.Dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["UserLogged"];
            if (user != null)
            {
                if (!this.IsPostBack)
                {
                    ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);//svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1);
                    ddlPlanta.DataTextField = "descripcionPlanta";
                    ddlPlanta.DataValueField = "ClavePlanta";
                    ddlPlanta.DataBind();
                    ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                }
                //? Como se obtendra la planta? porque se puede filtrar por una planta o por todas
                DataSet ds = new svcSCPP.SCPPClient().ObtenerInfoDashboard(Convert.ToInt32(ddlPlanta.SelectedValue), user.CodRol);//Como ejemplo le pondremos una fija
                FillGraph(ucEsmaltado, ds.Tables[0], "Esmalte");
                FillGraph(ucEmpaque, ds.Tables[1], "Empaque");
                FillGraph(ucFuego1, ds.Tables[2], "Fuego 1");
                FillGraph(ucFuego2, ds.Tables[3], "Fuego 2");
                FillGraph(ucDefectos, ds.Tables[4], "Perdida Verde");
                FillGraph(ucDefectos, ds.Tables[5], "Perdida Quemado");
                uwgSituacionActual.DataSource = ds.Tables[6];
                uwgSituacionActual.DataBind();
                uwgCapProdAvanceAcum.DataSource = ds.Tables[7];
                uwgCapProdAvanceAcum.DataBind();
                string sList = "";
                foreach (DataRow r in ds.Tables[8].Rows)
                    sList += "<li><span>" + r["Evento"] + "</span><p>" + r["Descripcion"] + "</p></li>";
                uEventos.InnerHtml = sList;
                if (ds.Tables[9] != null && ds.Tables[9].Rows.Count > 0)
                {
                    DataTable dtTendencias = FillTable(ds.Tables[9].Rows[0]["Esmaltado"].ToString());
                    FillGraph(ucEsmaltado, dtTendencias, "Meta");
                    dtTendencias = FillTable(ds.Tables[9].Rows[0]["Empaque"].ToString());
                    FillGraph(ucEmpaque, dtTendencias, "Meta");
                    dtTendencias = FillTable(ds.Tables[9].Rows[0]["Fuego 1"].ToString());
                    FillGraph(ucFuego1, dtTendencias, "Meta");
                    dtTendencias = FillTable(ds.Tables[9].Rows[0]["Fuego 2"].ToString());
                    FillGraph(ucFuego2, dtTendencias, "Meta");
                    dtTendencias = FillTable(ds.Tables[9].Rows[0]["Verde"].ToString());
                    FillGraph(ucDefectos, dtTendencias, "Meta Verde");
                    dtTendencias = FillTable(ds.Tables[9].Rows[0]["Quemado"].ToString());
                    FillGraph(ucDefectos, dtTendencias, "Meta Quemado");
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        private DataTable FillTable(string sValue)
        {
            DataTable dtRes = new DataTable();
            dtRes.Columns.Add("Hora", typeof(string));
            dtRes.Columns.Add("Cantidad", typeof(string));
            try
            {
                DataRow row;
                for (int i = 0; i < 24; i++)
                {
                    row = dtRes.NewRow();
                    row["Hora"] = i < 10 ? "0" : "" + i.ToString();
                    row["Cantidad"] = sValue;
                    dtRes.Rows.Add(row);
                }
            }
            catch { }
            return dtRes;
        }

        private static void FillGraph(UltraChart ucControl, DataTable dtSource, string sLabel)
        {
            XYSeries series1 = new XYSeries();
            series1.Label = sLabel;
            foreach (DataRow r in dtSource.Rows)
            {
                series1.Points.Add(new XYDataPoint(Convert.ToDouble(r[0].ToString()), Convert.ToDouble(r[1].ToString()), "", false));
            }
            ucControl.Series.Add(series1);
        }
    }
}
